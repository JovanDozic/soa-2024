package model

import (
	"github.com/google/uuid"
	"gorm.io/gorm"
)

type Tour struct {
	Id   uuid.UUID `json:"id"`
	Name string    `json:"name"`
}

func (tour *Tour) BeforeCreate(scope *gorm.DB) error {
	tour.Id = uuid.New()
	return nil
}
