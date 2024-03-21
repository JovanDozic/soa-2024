package handler

import (
	"encoding/json"
	"fmt"
	"net/http"

	"github.com/gorilla/mux"

	"ms-tours/model"
	"ms-tours/service"

)

type ProblemHandler struct {
	ProblemService *service.ProblemService
}

func (handler *ProblemHandler) GetAll(writer http.ResponseWriter, request *http.Request) {
	problem, err := handler.ProblemService.GetAll()
	if err != nil {
		writer.WriteHeader(http.StatusNotFound)
		return
	}

	writer.Header().Set("Content-Type", "application/json")
	writer.WriteHeader(http.StatusOK)
	json.NewEncoder(writer).Encode(problem)
}

func (handler *ProblemHandler) GetAllForTourist(writer http.ResponseWriter, request *http.Request) {
	id := mux.Vars(request)["id"]

	problem, err := handler.ProblemService.FindProblemForTourist(id)
	if err != nil {
		writer.WriteHeader(http.StatusBadRequest)
	}
	writer.Header().Set("Content-Type", "application/json")
	writer.WriteHeader(http.StatusOK)
	json.NewEncoder(writer).Encode(problem)
}

func (handler *ProblemHandler) Create(writer http.ResponseWriter, request *http.Request) {
	fmt.Println("Creating problem...")
	var problem model.Problem
	err := json.NewDecoder(request.Body).Decode(&problem)
	if err != nil {
		println("Error: ", err.Error())
		writer.WriteHeader(http.StatusBadRequest)
		return
	}
	err = handler.ProblemService.Create(&problem)
	if err != nil {
		println("Error while creating new problem ", err.Error())
		writer.WriteHeader(http.StatusConflict)
		return
	}
	writer.Header().Set("Content-Typer", "application/json")
	writer.WriteHeader(http.StatusCreated)
}
