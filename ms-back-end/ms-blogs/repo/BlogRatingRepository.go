package repo

import (
	"log"
	"ms-blogs/model"

	"gorm.io/gorm"
)

type BlogRatingRepository struct {
	DatabaseConnection *gorm.DB
}

func (repo *BlogRatingRepository) GetAll() ([]model.BlogRating, error) {
	ratings := []model.BlogRating{}
	dbResult := repo.DatabaseConnection.Find(&ratings)
	if dbResult.Error != nil {
		return nil, dbResult.Error
	}
	return ratings, nil
}

func (repo *BlogRatingRepository) Update(blogRating *model.BlogRating) error {
	log.Printf("Updating blogRating with ID: %s", blogRating.ID)

	// Perform the update operation
	dbResult := repo.DatabaseConnection.Save(blogRating)
	if dbResult.Error != nil {
		return dbResult.Error
	}

	log.Println("BlogRating updated successfully")
	return nil
}
