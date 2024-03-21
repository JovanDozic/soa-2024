package model

import (
	"time"

	"github.com/google/uuid"
	"gorm.io/gorm"
)

type BlogStatus int

const (
	_ BlogStatus = iota
	BlogStatusDraft
	BlogStatusPublished
	BlogStatusClosed
	BlogStatusActive
	BlogStatusFamous
)

type Blog struct {
	ID                 uuid.UUID            `json:"id"`
	Title              string               `json:"title"`
	Description        string               `json:"content"`
	Status             BlogStatus           `json:"status"`
	CreationDate       time.Time            `json:"creationDate"`
	UserId             int64                `json:"userId"`
	NetVotes           int                  `json:"netVotes"`
	BlogComments       []*BlogComment       `json:"blogComments"`
	BlogCommentReports []*BlogCommentReport `json:"reports"`
	// Ratings      []*BlogRating  `json:"ratings"`
	// Images       []string   `json:"images"`
}

func (blog *Blog) BeforeCreate(scope *gorm.DB) error {
	blog.ID = uuid.New()
	return nil
}

func (blog *Blog) AfterCreate(scope *gorm.DB) error {
	for _, comment := range blog.BlogComments {
		scope.Model(blog).Association("BlogComments").Append(comment)
	}
	return nil
}
