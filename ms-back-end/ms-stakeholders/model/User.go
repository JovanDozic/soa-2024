package model

type User struct {
	ID       int64  `json:"id,omitempty"`
	Username string `json:"username"`
	Password string `json:"password"`
	Email    string `json:"email"`
	Name     string `json:"name"`
	Surname  string `json:"surname"`
}
