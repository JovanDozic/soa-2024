package service

import (
	"fmt"

	"ms-tours/model"
	"ms-tours/repo"

)

type TourReviewService struct {
	TourReviewRepository *repo.TourReviewRepository
}

func (service *TourReviewService) FindTourReview(id string) (*model.TourReview, error) {
	tourReview, err := service.TourReviewRepository.FindById(id)
	if err != nil {
		return nil, fmt.Errorf(fmt.Sprintf("menu item with id %s not found", id))
	}
	return &tourReview, nil
}

func (service *TourReviewService) Create(tourReview *model.TourReview) error {
	fmt.Print("Service creating")
	err := service.TourReviewRepository.Create(tourReview)
	if err != nil {
		return err
	}
	return nil
}

func (service *TourReviewService) GetAll() ([]model.TourReview, error) {
	tourReviews, err := service.TourReviewRepository.GetAll()
	if err != nil {
		return nil, err
	}
	return tourReviews, nil
}
