package model

import (
	"time"

	"github.com/google/uuid"
)

type Problem struct {
	Category               string    `json:"category"`
	Priority               bool      `json:"priority"`
	Description            string    `json:"description"`
	Time                   time.Time `json:"time"`
	TourId                 uuid.UUID `json:"tourId"`
	TouristId              int       `json:"touristId"`
	AuthorsSolution        string    `json:"authorsSolution"`
	IsSolved               bool      `json:"isSolved"`
	UnsolvedProblemComment string    `json:"unsolvedProblemComment"`
	DeadLine               time.Time `json:"deadline"`
}
