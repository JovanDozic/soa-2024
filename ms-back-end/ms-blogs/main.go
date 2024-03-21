package main

import (
	"log"
	"ms-blogs/handler"
	"ms-blogs/model"
	"ms-blogs/repo"
	"ms-blogs/service"
	"net/http"

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

	err = database.AutoMigrate(&model.Blog{}, &model.BlogComment{})
	if err != nil {
		log.Fatalf("Error migrating models: %v", err)
	}
	return database
}

func main() {
	database := initDB()
	if database == nil {
		print("FAILED TO CONNECT TO DB")
		return
	}

	blogCommentRepo := &repo.BlogCommentRepository{DatabaseConnection: database}
	blogCommentService := &service.BlogCommentService{BlogCommentRepository: blogCommentRepo}
	blogCommentHandler := &handler.BlogCommentHandler{BlogCommentService: blogCommentService}

	blogRepo := &repo.BlogRepository{DatabaseConnection: database}
	blogService := &service.BlogService{BlogRepository: blogRepo}
	blogHandler := &handler.BlogHandler{BlogService: blogService, BlogCommentService: blogCommentService}

	startServer(blogHandler, blogCommentHandler)
}

func startServer(blogHandler *handler.BlogHandler, blogCommentHandler *handler.BlogCommentHandler) {
	router := mux.NewRouter().StrictSlash(true)

	// /ms-blogs/
	//"http://localhost:8080/ms-blogs/blogs/all"
	router.HandleFunc("/ms-blogs/blogs/all", blogHandler.GetAll).Methods("GET")
	router.HandleFunc("/ms-blogs/blogs/{id}", blogHandler.Get).Methods("GET")
	router.HandleFunc("/ms-blogs/blogs", blogHandler.Create).Methods("POST")

	// /ms-blogs/comments/
	router.HandleFunc("/ms-blogs/comments/{blogId}", blogCommentHandler.GetByBlogId).Methods("GET")
	router.HandleFunc("/ms-blogs/comments/add/{blogId}", blogCommentHandler.Add).Methods("POST")
	router.HandleFunc("/ms-blogs/comments/delete/{blogId}", blogCommentHandler.Delete).Methods("PUT")

	router.PathPrefix("/").Handler(http.FileServer(http.Dir("./static")))
	println("Server starting")
	log.Fatal(http.ListenAndServe(":8080", router))
}

// * PowerShell testing:
// Invoke-WebRequest -Uri "http://127.0.0.1:8080/blogs/aec7e123-233d-4a09-a289-75308ea5b7e6"
