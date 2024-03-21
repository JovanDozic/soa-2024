package model

import (
	"time"

	"github.com/google/uuid"
	"gorm.io/gorm"

)

type Problem struct {
	ID                     uuid.UUID `json:"id"`
	Category               string    `json:"category"`
	Priority               bool      `json:"priority"`
	Description            string    `json:"description"`
	Time                   time.Time `json:"time"`
	TourId                 int       `json:"tourId"`
	TouristId              int       `json:"touristId"`
	AuthorsSolution        string    `json:"authorsSolution"`
	IsSolved               bool      `json:"isSolved"`
	UnsolvedProblemComment string    `json:"unsolvedProblemComment"`
	DeadLine               time.Time `json:"deadline"`
}

func (problem *Problem) BeforeCreate(scope *gorm.DB) error {
	problem.ID = uuid.New()
	return nil
}
