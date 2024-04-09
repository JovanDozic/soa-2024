package model

import (
	"encoding/json"
	"io"
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

type TransportType int

const (
	_ TransportType = iota
	Walk
	Car
	Bicycle
)

/*type Tour struct {
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
}*/

type Tour struct {
	ID            int64          `bson:"_id,omitempty" json:"id"`
	Name          string         `bson:"name,omitempty" json:"name"`
	Description   string         `bson:"description,omitempty" json:"description"`
	Difficult     int            `bson:"difficult,omitempty" json:"difficult"`
	Status        TourStatus     `bson:"status,omitempty" json:"status"`
	Price         float32        `bson:"price,omitempty" json:"price"`
	AuthorId      int            `bson:"authorId,omitempty" json:"authorId"`
	Length        float32        `bson:"length,omitempty" json:"length"`
	PublishTime   time.Time      `bson:"publishTime,omitempty" json:"publishTime"`
	ArchiveTime   time.Time      `bson:"archiveTime,omitempty" json:"archiveTime"`
	Points        []Point        `bson:"points"`
	Tags          []Tag          `bson:"tags" json:"tags"`
	TourReviews   []TourReview   `bson:"tourReviews" json:"tourReviews"`
	RequiredTimes []RequiredTime `bson:"requiredTimes" json:"requiredTimes"`
	Problems      []Problem      `bson:"problems" json:"problems"`
	MyOwn         bool           `bson:"myOwn,omitempty" json:"myOwn"`
}

type Point struct {
	Longitude   float32 `bson:"longitude,omitempty" json:"longitude"`
	Latitude    float32 `bson:"lattitude,omitempty" json:"latitude"`
	Name        string  `bson:"name,omitempty" json:"name"`
	Description string  `bson:"description,omitempty" json:"description"`
	Picture     string  `bson:"picture,omitempty" json:"picture"`
	Public      bool    `bson:"public,omitempty" json:"public"`
}

type Tag struct {
	TourId int64  `bson:"tourId,omitempty" json:"tourId"`
	Name   string `bson:"name,omitempty" json:"name"`
}

type TourReview struct {
	Rating          int       `bson:"rating,omitempty" json:"rating"`
	Comment         string    `bson:"comment,omitempty" json:"comment"`
	TouristId       int       `bson:"touristId,omitempty" json:"touristId"`
	TouristUsername string    `bson:"touristUsername,omitempty" json:"touristUsername"`
	TourDate        time.Time `bson:"tourDate,omitempty" json:"tourDate"`
	CreationDate    time.Time `bson:"creationDate,omitempty" json:"creationDate"`
	//Images          []*string `json:"images" gorm:"type:jsonb;"`
}

type RequiredTime struct {
	TourId    int64         `bson:"tourId,omitempty" json:"tourId"`
	Transport TransportType `bson:"transport,omitempty" json:"transport"`
	Minutes   int           `bson:"minutes,omitempty" json:"minutes"`
}

type Problem struct {
	Category               string    `bson:"category,omitempty" json:"category"`
	Priority               bool      `bson:"priority,omitempty" json:"priority"`
	Description            string    `bson:"description,omitempty" json:"description"`
	Time                   time.Time `bson:"time,omitempty" json:"time"`
	TourId                 int       `bson:"tourId,omitempty" json:"tourId"`
	TouristId              int       `bson:"touristId,omitempty" json:"touristId"`
	AuthorsSolution        string    `bson:"authorsSolution,omitempty" json:"authorsSolution"`
	IsSolved               bool      `bson:"isSolved,omitempty" json:"isSolved"`
	UnsolvedProblemComment string    `bson:"unsolvedProblemComment,omitempty" json:"unsolvedProblemComment"`
	DeadLine               time.Time `bson:"deadline,omitempty" json:"deadline"`
}

type Tours []*Tour

func (p *Tours) ToJSON(w io.Writer) error {
	e := json.NewEncoder(w)
	return e.Encode(p)
}

func (p *Tour) ToJSON(w io.Writer) error {
	e := json.NewEncoder(w)
	return e.Encode(p)
}

func (p *Tour) FromJSON(r io.Reader) error {
	d := json.NewDecoder(r)
	return d.Decode(p)
}

func (p *Point) ToJSON(w io.Writer) error {
	e := json.NewEncoder(w)
	return e.Encode(p)
}

func (p *Point) FromJSON(r io.Reader) error {
	d := json.NewDecoder(r)
	return d.Decode(p)
}

func (p *Tag) ToJSON(w io.Writer) error {
	e := json.NewEncoder(w)
	return e.Encode(p)
}

func (p *Tag) FromJSON(r io.Reader) error {
	d := json.NewDecoder(r)
	return d.Decode(p)
}

func (p *TourReview) ToJSON(w io.Writer) error {
	e := json.NewEncoder(w)
	return e.Encode(p)
}

func (p *TourReview) FromJSON(r io.Reader) error {
	e := json.NewDecoder(r)
	return e.Decode(p)
}

func (p *RequiredTime) ToJSON(w io.Writer) error {
	e := json.NewEncoder(w)
	return e.Encode(p)
}

func (p *RequiredTime) FromJSON(r io.Reader) error {
	d := json.NewDecoder(r)
	return d.Decode(p)
}

func (p *Problem) ToJSON(w io.Writer) error {
	e := json.NewEncoder(w)
	return e.Encode(p)
}

func (p *Problem) FromJSON(r io.Reader) error {
	d := json.NewDecoder(r)
	return d.Decode(p)
}

func (tour *Tour) BeforeCreate(scope *gorm.DB) error {
	//tour.Id = uuid.New()
	rand.Seed(time.Now().UnixNano())
	tour.ID = rand.Int63()
	return nil
}
