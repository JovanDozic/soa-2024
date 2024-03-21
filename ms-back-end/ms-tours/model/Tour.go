package model

import (
	"time"

	"github.com/google/uuid"
	"gorm.io/gorm"
)

type TourStatus int

const (
	_ TourStatus = iota
	Published
	Archived
	Draft
)

type Tour struct {
	Id            uuid.UUID       `json:"id"`
	Name          string          `json:"name"`
	Description   string          `json:"description"`
	Difficult     int             `json:"difficult"`
	Status        TourStatus      `json:"status"`
	Price         float32         `json:"price"`
	AuthorId      int             `json:"authorId"`
	Length        float32         `json:"length"`
	PublishTime   time.Time       `json:"publishTime"`
	ArchiveTime   time.Time       `json:"archiveTime"`
	Points        []*Point        `json:"points" gorm:"type:jsonb;"`
	Tags          []*Tag          `json:"tags" gorm:"type:string"`
	TourReviews   []*TourReview   `json:"tourReviews" gorm:"type:jsonb;"`
	RequiredTimes []*RequiredTime `json:"requiredTimes" gorm:"type:jsonb;"`
	Problems      []*Problem      `json:"problems" gorm:"type:jsonb;"`
	MyOwn         bool            `json:"myOwn"`
}

func (tour *Tour) BeforeCreate(scope *gorm.DB) error {
	tour.Id = uuid.New()
	return nil
}
