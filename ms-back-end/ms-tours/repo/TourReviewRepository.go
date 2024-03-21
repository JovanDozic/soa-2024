package repo

import (
	"gorm.io/gorm"

	"ms-tours/model"
)

type TourReviewRepository struct {
	DatabaseConnection *gorm.DB
}

func (repo *TourReviewRepository) FindById(id string) (model.TourReview, error) {
	tourReview := model.TourReview{}
	dbResult := repo.DatabaseConnection.First(&tourReview, "id = ?", id)
	if dbResult.Error != nil {
		return tourReview, dbResult.Error
	}
	return tourReview, nil
}

func (repo *TourReviewRepository) Create(tourReview *model.TourReview) error {
	dbResult := repo.DatabaseConnection.Create(tourReview)
	if dbResult.Error != nil {
		return dbResult.Error
	}
	println("Rows affected: ", dbResult.RowsAffected)
	return nil
}

func (repo *TourReviewRepository) GetAll() ([]model.TourReview, error) {
	tourReviews := []model.TourReview{}
	dbResult := repo.DatabaseConnection.Find(&tourReviews)
	if dbResult.Error != nil {
		return nil, dbResult.Error
	}
	return tourReviews, nil
}
