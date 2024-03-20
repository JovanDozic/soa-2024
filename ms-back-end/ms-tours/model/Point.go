package model

type Point struct {
	Longitude   float32 `json:"longitude"`
	Latitude    float32 `json:"latitude"`
	Name        string  `json:"name"`
	Description string  `json:"description"`
	Picture     string  `json:"picture"`
	Public      bool    `json:"public"`
}
