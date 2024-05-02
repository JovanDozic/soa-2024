package main

import (
	"context"
	"log"
	"net/http"
	"os"
	"os/signal"
	"time"

	"github.com/gorilla/mux"
	"main.go/handler"
	"main.go/repo"

	gorillaHandlers "github.com/gorilla/handlers"
)

func main() {
	port := os.Getenv("PORT")
	if len(port) == 0 {
		port = "9090"
	}

	timeoutContext, cancel := context.WithTimeout(context.Background(), 30*time.Second)
	defer cancel()

	logger := log.New(os.Stdout, "[user-api] ", log.LstdFlags)
	storeLogger := log.New(os.Stdout, "[user-store] ", log.LstdFlags)

	store, err := repo.New(storeLogger)
	if err != nil {
		logger.Fatal(err)
	}
	defer store.CloseDriverConnection(timeoutContext)
	store.CheckConnection()

	userHandler := handler.NewUserHandler(logger, store)
	router := mux.NewRouter()
	router.Use(userHandler.MiddlewareContentTypeSet)

	getAllUsers := router.Methods(http.MethodGet).Subrouter()
	getAllUsers.HandleFunc("/users/{limit}", userHandler.GetAllUsers)

	postUserNode := router.Methods(http.MethodPost).Subrouter()
	postUserNode.HandleFunc("/user", userHandler.CreateUser)
	postUserNode.Use(userHandler.MiddlewareUserDeserialization)

	followUserRoute := router.Methods(http.MethodPost).Subrouter()
	followUserRoute.HandleFunc("/follow", userHandler.FollowUser)

	unfollowUserRoute := router.Methods(http.MethodDelete).Subrouter()
	unfollowUserRoute.HandleFunc("/unfollow", userHandler.UnfollowUser)

	getRecommendedUsersRoute := router.Methods(http.MethodGet).Subrouter()
	getRecommendedUsersRoute.HandleFunc("/recommended/{userID}", userHandler.GetRecommendedUsers)

	cors := gorillaHandlers.CORS(gorillaHandlers.AllowedOrigins([]string{"*"}))

	server := http.Server{
		Addr:         ":" + port,
		Handler:      cors(router),
		IdleTimeout:  120 * time.Second,
		ReadTimeout:  5 * time.Second,
		WriteTimeout: 5 * time.Second,
	}

	logger.Println("Server listening on port", port)
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

	if server.Shutdown(timeoutContext) != nil {
		logger.Fatal("Cannot gracefully shutdown...")
	}
	logger.Println("Server stopped")
}
