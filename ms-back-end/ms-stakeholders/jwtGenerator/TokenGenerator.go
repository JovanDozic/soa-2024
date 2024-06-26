package jwtGenerator

import (
	"fmt"
	"log"
	"ms-stakeholders/model"
	auth "ms-stakeholders/proto"
	"os"
	"time"

	"github.com/golang-jwt/jwt"
	"github.com/google/uuid"
)

var key = getEnv("JWT_KEY", "explorer_secret_key")
var issuer = getEnv("JWT_ISSUER", "explorer")
var audience = getEnv("JWT_AUDIENCE", "explorer-front.com")

func GenerateAccessToken(user *model.User) (*auth.Response, error) {
	claims := jwt.MapClaims{
		"jti":      uuid.New().String(),
		"id":       user.ID,
		"username": user.Username,
		"role":     "author",
		"exp":      time.Now().Add(24 * time.Hour).Unix(),
	}

	token, err := CreateToken(claims)
	if err != nil {
		return &auth.Response{}, err
	}

	return &auth.Response{
		Id:       user.ID,
		JwtToken: token,
	}, nil
}

func CreateToken(claims jwt.MapClaims) (string, error) {
	token := jwt.NewWithClaims(jwt.SigningMethodHS256, claims)
	tokenString, err := token.SignedString([]byte(key))
	if err != nil {
		return "", err
	}
	return tokenString, nil
}

func ValidateToken(tokenString string) error {
	token, err := jwt.Parse(tokenString, func(token *jwt.Token) (interface{}, error) {
		if _, ok := token.Method.(*jwt.SigningMethodHMAC); !ok {
			return nil, fmt.Errorf("unexpected signing method: %v", token.Header["alg"])
		}
		if token.Method.Alg() != jwt.SigningMethodHS256.Alg() {
			return nil, fmt.Errorf("unexpected signing method: %v", token.Method.Alg())
		}
		return []byte(key), nil
	})

	if err != nil {
		log.Fatalf("Error parsing token: %v", err)
	}
	if claims, ok := token.Claims.(jwt.MapClaims); ok && token.Valid {
		log.Printf("Token is valid. Claims: %v\n", claims)
		return nil
	}
	log.Printf("Token is invalid")
	return err
}

func getEnv(key, defaultValue string) string {
	if value, exists := os.LookupEnv(key); exists {
		return value
	}
	return defaultValue
}
