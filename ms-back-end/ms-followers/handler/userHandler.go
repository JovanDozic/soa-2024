package handler

import (
	"context"
	"encoding/json"
	"log"
	"net/http"
	"strconv"

	"github.com/gorilla/mux"
	"main.go/model"
	"main.go/repo"
)

type KeyProduct struct{}
type UserHandler struct {
	logger *log.Logger
	repo   *repo.UserRepository
}

func NewUserHandler(logger *log.Logger, repo *repo.UserRepository) *UserHandler {
	return &UserHandler{logger, repo}
}

func (handler *UserHandler) GetAllUsers(writer http.ResponseWriter, request *http.Request) {
	vars := mux.Vars(request)
	limit, err := strconv.Atoi(vars["limit"])
	if err != nil {
		handler.logger.Printf("Expected integer, got: %d", limit)
		http.Error(writer, "Unable to convert limit to integer", http.StatusBadRequest)
		return
	}

	users, err := handler.repo.GetAllUsers(limit)
	if err != nil {
		handler.logger.Print("Database exception: ", err)
	}

	if users == nil {
		return
	}

	err = users.ToJSON(writer)
	if err != nil {
		http.Error(writer, "Unable to convert to json", http.StatusInternalServerError)
		handler.logger.Fatal("Unable to convert to json :", err)
		return
	}
}

func (handler *UserHandler) CreateUser(writer http.ResponseWriter, request *http.Request) {
	user := request.Context().Value(KeyProduct{}).(*model.User)
	err := handler.repo.CreateUser(user)
	if err != nil {
		handler.logger.Print("Database exception: ", err)
		writer.WriteHeader(http.StatusInternalServerError)
		return
	}
	writer.WriteHeader(http.StatusCreated)
}

func (handler *UserHandler) MiddlewareUserDeserialization(next http.Handler) http.Handler {
	return http.HandlerFunc(func(writer http.ResponseWriter, request *http.Request) {
		User := &model.User{}
		err := User.FromJSON(request.Body)
		if err != nil {
			http.Error(writer, "Unable to decode json", http.StatusBadRequest)
			handler.logger.Fatal(err)
			return
		}
		ctx := context.WithValue(request.Context(), KeyProduct{}, User)
		request = request.WithContext(ctx)
		next.ServeHTTP(writer, request)
	})
}

func (handler *UserHandler) MiddlewareContentTypeSet(next http.Handler) http.Handler {
	return http.HandlerFunc(func(rw http.ResponseWriter, h *http.Request) {
		handler.logger.Println("Method [", h.Method, "] - Hit path :", h.URL.Path)

		rw.Header().Add("Content-Type", "application/json")

		next.ServeHTTP(rw, h)
	})
}

func (handler *UserHandler) FollowUser(writer http.ResponseWriter, request *http.Request) {
	var requestBody map[string]string
	err := json.NewDecoder(request.Body).Decode(&requestBody)
	if err != nil {
		http.Error(writer, "Unable to decode json", http.StatusBadRequest)
		handler.logger.Fatal(err)
		return
	}

	userID := requestBody["userID"]
	followedUserID := requestBody["followedUserID"]

	// Log the userID
	handler.logger.Printf("Prvi userID: %s", userID)
	handler.logger.Printf("Drugi userID: %s", followedUserID)

	err = handler.repo.FollowUser(userID, followedUserID)
	if err != nil {
		handler.logger.Print("Database exception: ", err)
		writer.WriteHeader(http.StatusInternalServerError)
		return
	}
	writer.WriteHeader(http.StatusOK)
}

func (handler *UserHandler) UnfollowUser(writer http.ResponseWriter, request *http.Request) {
	var requestBody map[string]string
	err := json.NewDecoder(request.Body).Decode(&requestBody)
	if err != nil {
		http.Error(writer, "Unable to decode json", http.StatusBadRequest)
		handler.logger.Fatal(err)
		return
	}

	userID := requestBody["userID"]
	unfollowedUserID := requestBody["unfollowedUserID"]

	err = handler.repo.UnfollowUser(userID, unfollowedUserID)
	if err != nil {
		handler.logger.Print("Database exception: ", err)
		writer.WriteHeader(http.StatusInternalServerError)
		return
	}
	writer.WriteHeader(http.StatusOK)
}
func (handler *UserHandler) GetRecommendedUsers(writer http.ResponseWriter, request *http.Request) {
	vars := mux.Vars(request)
	userID := vars["userID"]
	recommendedUsers, err := handler.repo.GetRecommendedUsers(userID)
	if err != nil {
		handler.logger.Print("Database exception: ", err)
		writer.WriteHeader(http.StatusInternalServerError)
		return
	}

	err = recommendedUsers.ToJSON(writer)
	if err != nil {
		http.Error(writer, "Unable to convert to json", http.StatusInternalServerError)
		handler.logger.Fatal("Unable to convert to json :", err)
		return
	}
}
