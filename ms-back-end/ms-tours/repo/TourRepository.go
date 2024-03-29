package repo

import (
	"log"

	"gorm.io/gorm"

	"ms-tours/model"
)

type TourRepository struct {
	DatabaseConnection *gorm.DB
}

func (repo *TourRepository) FindById(id string) (model.Tour, error) {
	tour := model.Tour{}
	dbResult := repo.DatabaseConnection.First(&tour, "id = ?", id)
	if dbResult.Error != nil {
		return tour, dbResult.Error
	}
	return tour, nil
}

func (repo *TourRepository) GetAll() ([]model.Tour, error) {
	tours := []model.Tour{}
	dbResult := repo.DatabaseConnection.Find(&tours)
	if dbResult.Error != nil {
		return nil, dbResult.Error
	}
	return tours, nil
}

func (repo *TourRepository) CreateTour(tour *model.Tour) error {
	log.Printf("Usao u tourRepo")
	dbResult := repo.DatabaseConnection.Create(tour)
	if dbResult.Error != nil {
		return dbResult.Error
	}
	println("Rows affected: ", dbResult.RowsAffected)
	return nil
}
