package model

import (
	"github.com/google/uuid"
)

type UserRole int

const (
	_ UserRole = iota
	Administrator
	Author
	Tourist
)

type User struct {
	ID       uuid.UUID `json:"id,omitempty"`
	Username string    `json:"username"`
	Password string    `json:"password"`
	Role     UserRole  `json:"role"`
	Email    string    `json:"email"`
	IsActive bool      `json:"isActive"`
}
