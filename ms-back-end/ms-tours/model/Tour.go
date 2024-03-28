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
	ID            int64           `json:"id"`
	Name          string          `json:"name"`
	Description   string          `json:"description"`
	Difficult     int             `json:"difficult"`
	Status        TourStatus      `json:"status"`
	Price         float32         `json:"price"`
	AuthorId      int             `json:"authorId"`
	Length        float32         `json:"length"`
	PublishTime   time.Time       `json:"publishTime"`
	ArchiveTime   time.Time       `json:"archiveTime"`
	Points        []*Point        `gorm:"many2many:tour_points"`
	Tags          []*Tag          `json:"tags"`
	TourReviews   []*TourReview   `json:"tourReviews"`
	RequiredTimes []*RequiredTime `json:"requiredTimes"`
	Problems      []*Problem      `json:"problems"`
	MyOwn         bool            `json:"myOwn"`
}

func (tour *Tour) BeforeCreate(scope *gorm.DB) error {
	//tour.Id = uuid.New()
	rand.Seed(time.Now().UnixNano())
	tour.ID = rand.Int63()
	return nil
}
