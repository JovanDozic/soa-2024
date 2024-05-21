package config

import "os"

type Config struct {
	Address string
}

func GetConfig() Config {
	return Config{
		Address: os.Getenv("STAKEHOLDER_SERVICE_ADDRESS"),
	}
}
