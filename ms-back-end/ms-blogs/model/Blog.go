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
	ID           uuid.UUID  `json:"id"`
	Title        string     `json:"title"`
	Description  string     `json:"content"`
	Status       BlogStatus `json:"status"`
	CreationTime time.Time  `json:"creationTime"`
}

func (blog *Blog) BeforeCreate(scope *gorm.DB) error {
	blog.ID = uuid.New()
	return nil
}
