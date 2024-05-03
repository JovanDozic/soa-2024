package model

import (
	"encoding/json"
	"io"

	"errors"

	"github.com/google/uuid"
)

type UserRole int

const (
	_UserRole = iota
	Tourist
	Admin
	Author
)

type User struct {
	Id       uuid.UUID `json:"id,omitempty"`
	Username string    `json:"username"`
	Password string    `json:"password"`
	Role     UserRole  `json:"role"`
	Email    string    `json:"email"`
	IsActive bool      `json:"isActive"`
	// Activated	bool			`json:"activated"`
	// Notifications	[]			`json:"notifications"`
	// IsBlogEnabled	bool		`json:"isBlogEnabled"`
}

type Users []*User

func (user *User) ToJSON(writer io.Writer) error {
	encoder := json.NewEncoder(writer)
	return encoder.Encode(user)
}

func (user *User) FromJSON(reader io.Reader) error {
	decoder := json.NewDecoder(reader)
	return decoder.Decode(user)
}

func (user *Users) ToJSON(writer io.Writer) error {
	encoder := json.NewEncoder(writer)
	return encoder.Encode(user)
}

func (user *Users) FromJSON(reader io.Reader) error {
	decoder := json.NewDecoder(reader)
	return decoder.Decode(user)
}

func ConvertToRole(num int64) (UserRole, error) {
	switch num {
	case 1:
		return Tourist, nil
	case 2:
		return Admin, nil
	case 3:
		return Author, nil
	default:
		return _UserRole, errors.New("invalid role number")
	}
}
