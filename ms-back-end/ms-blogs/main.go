package main

import (
	"log"
	"ms-blogs/handler"
	"ms-blogs/model"
	"ms-blogs/repo"
	"ms-blogs/service"
	"net/http"
	"strconv"

	"github.com/gorilla/mux"
	"gorm.io/driver/postgres"
	"gorm.io/gorm"
)

func initDB() *gorm.DB {
	connectionString := "user=postgres password=super dbname=ms-blogs host=localhost port=5432 sslmode=disable search_path=blogs"
	database, err := gorm.Open(postgres.Open(connectionString), &gorm.Config{})
	if err != nil {
		print(err)
		return nil
	}

	database.AutoMigrate(&model.Blog{})
	database.Exec("INSERT INTO blogs VALUES ('aec7e123-233d-4a09-a289-75308ea5b7e6', 'Naslov Bloga', 'Opis Bloga', " + strconv.Itoa(int(model.BlogStatusDraft)) + ", '2021-01-01 10:10:10')")
	return database
}

func main() {
	database := initDB()
	if database == nil {
		print("FAILED TO CONNECT TO DB")
		return
	}
	repo := &repo.BlogRepository{DatabaseConnection: database}
	service := &service.BlogService{BlogRepository: repo}
	handler := &handler.BlogHandler{BlogService: service}

	startServer(handler)
}

func startServer(handler *handler.BlogHandler) {
	router := mux.NewRouter().StrictSlash(true)

	router.HandleFunc("/blogs/{id}", handler.Get).Methods("GET")
	router.HandleFunc("/blogs", handler.Create).Methods("POST")

	router.PathPrefix("/").Handler(http.FileServer(http.Dir("./static")))
	println("Server starting")
	log.Fatal(http.ListenAndServe(":8080", router))
}

// * PowerShell testi1ng:
// Invoke-WebRequest -Uri "http://127.0.0.1:8080/blogs/aec7e123-233d-4a09-a289-75308ea5b7e6"
