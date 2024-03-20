package model

import (
	"time"

	"github.com/google/uuid"
)

type BlogComment struct {
	ID          uuid.UUID `json:"id"`
	UserId      int64     `json:"userId"`
	BlogId      uuid.UUID `json:"blogId"`
	Comment     string    `json:"comment"`
	TimeCreated time.Time `json:"timeCreated"`
	TimeUpdated time.Time `json:"timeUpdated"`
}

// TODO: Before create
