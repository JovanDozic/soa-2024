package service

import (
	"ms-blogs/model"
	"ms-blogs/repo"
)

type BlogRatingService struct {
	BlogRatingRepository *repo.BlogRatingRepository
}

func (service *BlogRatingService) GetAll() ([]model.BlogRating, error) {
	ratings, err := service.BlogRatingRepository.GetAll()
	if err != nil {
		return nil, err
	}
	return ratings, nil
}
