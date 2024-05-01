package config

import "os"

type Config struct {
	Address                  string
	ApiGatewayServiceAddress string
}

func GetConfig() Config {
	return Config{
		ApiGatewayServiceAddress: os.Getenv("API_GATEWAY_SERVICE_ADDRESS"),
		Address:                  os.Getenv("GATEWAY_ADDRESS"),
	}
}
