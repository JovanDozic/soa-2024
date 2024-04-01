package model

import (
	"time"

	"github.com/google/uuid"
	"gorm.io/gorm"
)

type Vote int

const (
	PLUS  Vote = 1
	MINUS Vote = 2
)

type BlogRating struct {
	ID          uuid.UUID `json:"id"`
	UserId      int64     `json:"userId"`
	TimeCreated time.Time `json:"votingDate"`
	Vote        Vote      `json:"mark"`
	BlogId      uuid.UUID `json:"blogId"`
}

func (rating *BlogRating) BeforeCreate(scope *gorm.DB) error {
	rating.ID = uuid.New()
	return nil
}
