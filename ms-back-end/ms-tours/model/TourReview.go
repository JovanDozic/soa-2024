package model

import (
	"time"

	"github.com/google/uuid"
	"gorm.io/gorm"
)

type TourReview struct {
	ID              uuid.UUID `json:"id"`
	TourId          int64     `json:"tourId"`
	Rating          int       `json:"rating"`
	Comment         string    `json:"comment"`
	TouristId       int       `json:"touristId"`
	TouristUsername string    `json:"touristUsername"`
	TourDate        time.Time `json:"tourDate"`
	CreationDate    time.Time `json:"creationDate"`
	//Images          []*string `json:"images" gorm:"type:jsonb;"`
}

func (tourReview *TourReview) BeforeCreate(scope *gorm.DB) error {
	tourReview.ID = uuid.New()
	return nil
}
