package model

import (
	"math/rand"
	"time"

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
	Id            int64           `json:"id"`
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
	Tags          []*Tag          `json:"tags" gorm:"type:jsonb;"`
	TourReviews   []*TourReview   `json:"tourReviews" gorm:"type:jsonb;"`
	RequiredTimes []*RequiredTime `json:"requiredTimes" gorm:"type:jsonb;"`
	Problems      []*Problem      `json:"problems" gorm:"type:jsonb;"`
	MyOwn         bool            `json:"myOwn"`
}

func (tour *Tour) BeforeCreate(scope *gorm.DB) error {
	//tour.Id = uuid.New()
	rand.Seed(time.Now().UnixNano())
	tour.Id = rand.Int63()
	return nil
}
