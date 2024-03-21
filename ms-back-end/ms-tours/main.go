package main

import (
	"log"
	"net/http"

	"github.com/gorilla/mux"
	"gorm.io/driver/postgres"
	"gorm.io/gorm"

	"ms-tours/handler"
	"ms-tours/model"
	"ms-tours/repo"
	"ms-tours/service"
)

func initDB() *gorm.DB {
	connectionString := "user=postgres password=super dbname=ms-tours host=localhost port=5432 sslmode=disable"
	database, err := gorm.Open(postgres.Open(connectionString), &gorm.Config{})
	if err != nil {
		log.Fatalf("Failed to connect to database: %v", err)
	}

	// AutoMigrate will create the table if it does not exist and will
	// automigrate it if there are any schema changes
	database.Exec("CREATE SCHEMA IF NOT EXISTS tours")
	err = database.AutoMigrate(&model.Tour{})
	if err != nil {
		log.Fatalf("Failed to auto migrate database: %v", err)
	}

	// Insert initial data
	//database.Exec("INSERT INTO tours (id, name) VALUES ($1, $2) ON CONFLICT DO NOTHING", "aec7e123-233d-4a09-a289-75308ea5b7e6", "Prva tura")

	return database
}

func startServer(handler *handler.TourHandler, problemHandler *handler.ProblemHandler) {
	router := mux.NewRouter().StrictSlash(true)

	router.HandleFunc("/tours/{id}", handler.Get).Methods("GET")
	router.HandleFunc("/tours", handler.Create).Methods("POST")
	router.HandleFunc("/createProblem", problemHandler.Create).Methods("POST")

	router.PathPrefix("/").Handler(http.FileServer(http.Dir("./static")))
	log.Println("Server starting")
	log.Fatal(http.ListenAndServe(":8081", router))
}

func main() {
	database := initDB()
	if database == nil {
		print("FAILED TO CONNECT TO DB")
		return
	}
	problemRepo := &repo.ProblemRepository{DatabaseConnection: database}
	problemService := &service.ProblemService{ProblemRepository: problemRepo}
	problemHandler := &handler.ProblemHandler{ProblemService: problemService}

	repo := &repo.TourRepository{DatabaseConnection: database}
	service := &service.TourService{TourRepository: repo}
	handler := &handler.TourHandler{TourService: service}

	startServer(handler, problemHandler)
}
