package model

import (
	"time"
)

type TourReview struct {
	Rating          int       `json:"rating"`
	Comment         string    `json:"comment"`
	TouristId       int       `json:"touristId"`
	TouristUsername string    `json:"touristUsername"`
	TourDate        time.Time `json:"tourDate"`
	CreationTime    time.Time `json:"creationTime"`
	Images          []*string `json:"images" gorm:"type:jsonb;"`
}
