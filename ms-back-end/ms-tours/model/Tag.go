package model

import (
	"github.com/google/uuid"
	"gorm.io/gorm"
)

type Tag struct {
	ID     uuid.UUID `json:"id"`
	TourId int64     `json:"tourId"`
	Name   string    `json:"name"`
}

func (tag *Tag) BeforeCreate(scope *gorm.DB) error {
	tag.ID = uuid.New()
	return nil
}
