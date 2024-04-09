package main

import (
	"context"
	"log"
	"ms-tours/handler"
	"ms-tours/model"
	"net/http"
	"os"
	"os/signal"
	"time"

	/*"gorm.io/driver/postgres"
	"gorm.io/gorm"*/

	gorillaHandlers "github.com/gorilla/handlers"
	"github.com/gorilla/mux"
)

/*func initDB() *gorm.DB {
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
}*/

func main() {
	/*database := initDB()
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

	startServer(handler, problemHandler, reviewHandler, clubHandler)*/
	timeoutContext, cancel := context.WithTimeout(context.Background(), 30*time.Second)
	defer cancel()

	logger := log.New(os.Stdout, "[tours-api] ", log.LstdFlags)
	storeLogger := log.New(os.Stdout, "[tours-store] ", log.LstdFlags)

	// NoSQL: Initialize Product Repository store
	store, err := model.New(timeoutContext, storeLogger)
	if err != nil {
		logger.Fatal(err)
	}
	defer store.Disconnect(timeoutContext)

	// NoSQL: Checking if the connection was established
	store.Ping()

	//Initialize the handler and inject said logger
	tourHandler := handler.NewToursHandler(logger, store)

	//Initialize the router and add a middleware for all the requests
	router := mux.NewRouter()
	router.Use(tourHandler.MiddlewareContentTypeSet)

	getRouter := router.Methods(http.MethodGet).Subrouter()
	getRouter.HandleFunc("/ms-tours/get-all-tours", tourHandler.GetAllTours)

	postRouter := router.Methods(http.MethodPost).Subrouter()
	postRouter.HandleFunc("/ms-tours/tours/create-tour", tourHandler.PostTour)
	postRouter.Use(tourHandler.MiddlewareTourDeserialization)

	getByIdRouter := router.Methods(http.MethodGet).Subrouter()
	getByIdRouter.HandleFunc("/ms-tours/tours/{id}", tourHandler.GetTourById)

	postProblemRouter := router.Methods(http.MethodPost).Subrouter()
	postProblemRouter.HandleFunc("/ms-tours/create-problem", tourHandler.AddProblem)
	postProblemRouter.Use(tourHandler.MiddlewareProblemDeserialization)

	postReviewRouter := router.Methods(http.MethodPost).Subrouter()
	postReviewRouter.HandleFunc("/ms-tours/create-review/{id}", tourHandler.AddReview)
	postReviewRouter.Use(tourHandler.MiddlewareTourReviewDeserialization)

	postClubRouter := router.Methods(http.MethodPost).Subrouter()
	postClubRouter.HandleFunc("/ms-tours/create-club", tourHandler.PostClub)
	postClubRouter.Use(tourHandler.MiddlewareClubDeserialization)

	cors := gorillaHandlers.CORS(gorillaHandlers.AllowedOrigins([]string{"*"}))

	//Initialize the server
	server := http.Server{
		Addr:         ":8081",
		Handler:      cors(router),
		IdleTimeout:  120 * time.Second,
		ReadTimeout:  1 * time.Second,
		WriteTimeout: 1 * time.Second,
	}

	logger.Println("Server listening on port 8081")
	//Distribute all the connections to goroutines
	go func() {
		err := server.ListenAndServe()
		if err != nil {
			logger.Fatal(err)
		}
	}()

	sigCh := make(chan os.Signal)
	signal.Notify(sigCh, os.Interrupt)
	signal.Notify(sigCh, os.Kill)

	sig := <-sigCh
	logger.Println("Received terminate, graceful shutdown", sig)

	//Try to shutdown gracefully
	if server.Shutdown(timeoutContext) != nil {
		logger.Fatal("Cannot gracefully shutdown...")
	}
	logger.Println("Server stopped")
}
