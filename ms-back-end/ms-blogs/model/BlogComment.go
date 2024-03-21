package model

import (
	"time"

	"github.com/google/uuid"
	"gorm.io/gorm"
)

type BlogComment struct {
	ID          uuid.UUID `json:"id"`
	UserId      int64     `json:"userId"`
	BlogId      uuid.UUID `json:"blogId"`
	Comment     string    `json:"comment"`
	TimeCreated time.Time `json:"timeCreated"`
	TimeUpdated time.Time `json:"timeUpdated"`
}

func (blog *BlogComment) BeforeCreate(scope *gorm.DB) error {
	blog.ID = uuid.New()
	return nil
}
