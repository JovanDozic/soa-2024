package model

type Club struct {
	Id          int    `json:"id"`
	Name        string `json:"name"`
	Description string `json:"description"`
	URL         string `json:"url"`
	OwnerId     int    `json:"owner_id"`
}
