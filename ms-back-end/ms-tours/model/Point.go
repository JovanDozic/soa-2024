package model

import (
	"github.com/google/uuid"
	"gorm.io/gorm"
)

type Point struct {
	ID          uuid.UUID `json:"id"`
	Longitude   float32   `json:"longitude"`
	Latitude    float32   `json:"latitude"`
	Name        string    `json:"name"`
	Tours       []*Tour   `gorm:"many2many:tour_points"`
	Description string    `json:"description"`
	Picture     string    `json:"picture"`
	Public      bool      `json:"public"`
}

func (point *Point) BeforeCreate(scope *gorm.DB) error {
	point.ID = uuid.New()
	return nil
}
