package handler

import (
	"encoding/json"
	"fmt"
	"net/http"

	"ms-tours/model"
	"ms-tours/service"

)

type TourReviewHandler struct {
	TourReviewService *service.TourReviewService
}

func (handler *TourReviewHandler) GetAll(writer http.ResponseWriter, request *http.Request) {
	tourReview, err := handler.TourReviewService.GetAll()
	if err != nil {
		writer.WriteHeader(http.StatusNotFound)
		return
	}

	writer.Header().Set("Content-Type", "application/json")
	writer.WriteHeader(http.StatusOK)
	json.NewEncoder(writer).Encode(tourReview)
}

func (handler *TourReviewHandler) Create(writer http.ResponseWriter, request *http.Request) {
	fmt.Println("Creating tourReview...")
	var tourReview model.TourReview
	err := json.NewDecoder(request.Body).Decode(&tourReview)
	if err != nil {
		println("Error: ", err.Error())
		writer.WriteHeader(http.StatusBadRequest)
		return
	}
	err = handler.TourReviewService.Create(&tourReview)
	if err != nil {
		println("Error while creating new tourReview ", err.Error())
		writer.WriteHeader(http.StatusConflict)
		return
	}
	writer.Header().Set("Content-Typer", "application/json")
	writer.WriteHeader(http.StatusCreated)
}
