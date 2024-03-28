package model

import (
	"github.com/google/uuid"
	"gorm.io/gorm"
)

type TransportType int

const (
	_ TransportType = iota
	Walk
	Car
	Bicycle
)

type RequiredTime struct {
	ID        uuid.UUID     `json:"id"`
	TourId    int64         `json:"tourId"`
	Transport TransportType `json:"transport"`
	Minutes   int           `json:"minutes"`
}

func (reqTime *RequiredTime) BeforeCreate(scope *gorm.DB) error {
	reqTime.ID = uuid.New()
	return nil
}
