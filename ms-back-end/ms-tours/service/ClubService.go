package service

import (
	"fmt"

	"ms-tours/model"
	"ms-tours/repo"
)

type ClubService struct {
	ClubRepository *repo.ClubRepository
}

func (service *ClubService) FindClub(id string) (*model.Club, error) {
	club, err := service.ClubRepository.FindById(id)
	if err != nil {
		return nil, fmt.Errorf(fmt.Sprintf("menu item with id %s not found", id))
	}
	return &club, nil
}

func (service *ClubService) Create(club *model.Club) error {
	fmt.Print("Service creating")
	err := service.ClubRepository.Create(club)
	if err != nil {
		return err
	}
	return nil
}

func (service *ClubService) GetAll() ([]model.Club, error) {
	clubs, err := service.ClubRepository.GetAll()
	if err != nil {
		return nil, err
	}
	return clubs, nil
}
