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
	connectionString := "user=postgres password=super dbname=ms-tours host=ms-tours-database port=5432 sslmode=disable"
	database, err := gorm.Open(postgres.Open(connectionString), &gorm.Config{})
	if err != nil {
		log.Fatalf("Failed to connect to database: %v", err)
	}

	err = database.AutoMigrate(&model.Tour{}, &model.Point{}, &model.TourReview{}, &model.Tag{}, &model.RequiredTime{}, &model.Problem{}, &model.TourReview{}, &model.Club{})
	if err != nil {
		log.Fatalf("Failed to auto migrate database: %v", err)
	}

	return database
}

func startServer(handler *handler.TourHandler, problemHandler *handler.ProblemHandler, reviewHandler *handler.TourReviewHandler, clubHandler *handler.ClubHandler) {
	router := mux.NewRouter().StrictSlash(true)

	router.HandleFunc("/ms-tours/get-all-tours", handler.GetAll).Methods("GET")
	router.HandleFunc("/ms-tours/tours/{id}", handler.Get).Methods("GET")
	router.HandleFunc("/ms-tours/tours/create-tour", handler.Create).Methods("POST")
	router.HandleFunc("/ms-tours/create-problem", problemHandler.Create).Methods("POST")
	router.HandleFunc("/ms-tours/create-review", reviewHandler.Create).Methods("POST")
	router.HandleFunc("/ms-tours/create-club", clubHandler.Create).Methods("POST")

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

	clubRepo := &repo.ClubRepository{DatabaseConnection: database}
	clubService := &service.ClubService{ClubRepository: clubRepo}
	clubHandler := &handler.ClubHandler{ClubService: clubService}

	reviewRepo := &repo.TourReviewRepository{DatabaseConnection: database}
	reviewService := &service.TourReviewService{TourReviewRepository: reviewRepo}
	reviewHandler := &handler.TourReviewHandler{TourReviewService: reviewService}

	problemRepo := &repo.ProblemRepository{DatabaseConnection: database}
	problemService := &service.ProblemService{ProblemRepository: problemRepo}
	problemHandler := &handler.ProblemHandler{ProblemService: problemService}

	repo := &repo.TourRepository{DatabaseConnection: database}
	service := &service.TourService{TourRepository: repo}
	handler := &handler.TourHandler{TourService: service}

	startServer(handler, problemHandler, reviewHandler, clubHandler)
}
