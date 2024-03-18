package service

import (
	"fmt"

	"ms-tours/model"
	"ms-tours/repo"

)

type TourService struct {
	TourRepository *repo.TourRepository
}

func (service *TourService) FindTour(id string) (*model.Tour, error) {
	tour, err := service.TourRepository.FindById(id)
	if err != nil {
		return nil, fmt.Errorf(fmt.Sprintf("menu item with id %s not found", id))
	}
	return &tour, nil
}

func (service *TourService) Create(tour *model.Tour) error {
	err := service.TourRepository.CreateTour(tour)
	if err != nil {
		return err
	}
	return nil
}
