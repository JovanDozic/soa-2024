package model

import (
	"time"

	"github.com/google/uuid"
	"gorm.io/gorm"
)

type BlogCommentReport struct {
	ID                 uuid.UUID `json:"id"`
	TimeCommentCreated time.Time `json:"timeCommentCreated"`
	TimeReported       time.Time `json:"timeReported"`
	ReportAuthorId     int64     `json:"reportAuthorId"`
	ReportReason       int64     `json:"reportReason"`
	IsReviewed         bool      `json:"isReviewed"`
	BlogId             uuid.UUID `json:"blogId"`
	Comment            string    `json:"comment"`
	IsAccepted         bool      `json:"isAccepted"`
}

func (report *BlogCommentReport) BeforeCreate(scope *gorm.DB) error {
	report.ID = uuid.New()
	return nil
}
