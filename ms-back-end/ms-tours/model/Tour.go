package model

import (
	"database/sql/driver"
	"encoding/json"
	"fmt"
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

func (tour *Tour) AfterCreate(scope *gorm.DB) error {
	for _, point := range tour.Points {
		scope.Model(tour).Association("Points").Append(point)
	}
	for _, tag := range tour.Tags {
		scope.Model(tour).Association("Tags").Append(tag)
	}
	for _, tourReview := range tour.TourReviews {
		scope.Model(tour).Association("TourReviews").Append(tourReview)
	}
	for _, requiredTime := range tour.RequiredTimes {
		scope.Model(tour).Association("RequiredTimes").Append(requiredTime)
	}
	for _, problem := range tour.Problems {
		scope.Model(tour).Association("Problems").Append(problem)
	}
	return nil
}

func (point *Point) Scan(value interface{}) error {
	if value == nil {
		*point = Point{}
		return nil
	}

	bytes, ok := value.([]byte)
	if !ok {
		return fmt.Errorf("Scan source is not []byte")
	}

	return json.Unmarshal(bytes, point)
}

func (tag *Tag) Scan(value interface{}) error {
	if value == nil {
		*tag = Tag{}
		return nil
	}

	bytes, ok := value.([]byte)
	if !ok {
		return fmt.Errorf("Scan source is not []byte")
	}

	return json.Unmarshal(bytes, tag)
}

func (tourReview *TourReview) Scan(value interface{}) error {
	if value == nil {
		*tourReview = TourReview{}
		return nil
	}

	bytes, ok := value.([]byte)
	if !ok {
		return fmt.Errorf("Scan source is not []byte")
	}

	return json.Unmarshal(bytes, tourReview)
}

func (problem *Problem) Scan(value interface{}) error {
	if value == nil {
		*problem = Problem{}
		return nil
	}

	bytes, ok := value.([]byte)
	if !ok {
		return fmt.Errorf("Scan source is not []byte")
	}

	return json.Unmarshal(bytes, problem)
}

func (requiredTime *RequiredTime) Scan(value interface{}) error {
	if value == nil {
		*requiredTime = RequiredTime{}
		return nil
	}

	bytes, ok := value.([]byte)
	if !ok {
		return fmt.Errorf("Scan source is not []byte")
	}

	return json.Unmarshal(bytes, requiredTime)
}

func (point Point) Value() (driver.Value, error) {
	return json.Marshal(point)
}

func (problem Problem) Value() (driver.Value, error) {
	return json.Marshal(problem)
}

func (reqTime RequiredTime) Value() (driver.Value, error) {
	return json.Marshal(reqTime)
}

func (tag Tag) Value() (driver.Value, error) {
	return json.Marshal(tag)
}

func (review TourReview) Value() (driver.Value, error) {
	return json.Marshal(review)
}
