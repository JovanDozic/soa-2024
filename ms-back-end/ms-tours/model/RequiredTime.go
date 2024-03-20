package model

type TransportType int

const (
	_ TransportType = iota
	Walk
	Car
	Bicycle
)

type RequiredTime struct {
	Transport TransportType `json:"transport"`
	Minutes   int           `json:"minutes"`
}
