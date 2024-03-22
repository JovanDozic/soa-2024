package handler

import (
	"encoding/json"
	"fmt"
	"net/http"

	"ms-tours/model"
	"ms-tours/service"
)

type ClubHandler struct {
	ClubService *service.ClubService
}

func (handler *ClubHandler) GetAll(writer http.ResponseWriter, request *http.Request) {
	club, err := handler.ClubService.GetAll()
	if err != nil {
		writer.WriteHeader(http.StatusNotFound)
		return
	}

	writer.Header().Set("Content-Type", "application/json")
	writer.WriteHeader(http.StatusOK)
	json.NewEncoder(writer).Encode(club)
}

func (handler *ClubHandler) Create(writer http.ResponseWriter, request *http.Request) {
	fmt.Println("Creating club...")
	var club model.Club
	err := json.NewDecoder(request.Body).Decode(&club)
	if err != nil {
		println("Error: ", err.Error())
		writer.WriteHeader(http.StatusBadRequest)
		return
	}
	err = handler.ClubService.Create(&club)
	if err != nil {
		println("Error while creating new club ", err.Error())
		writer.WriteHeader(http.StatusConflict)
		return
	}
	writer.Header().Set("Content-Typer", "application/json")
	writer.WriteHeader(http.StatusCreated)
}
