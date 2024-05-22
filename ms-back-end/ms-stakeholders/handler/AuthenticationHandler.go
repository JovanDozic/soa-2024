package handler

//protoc -I ./proto --go_out ./proto --go_opt paths=source_relative --go-grpc_out ./proto --go-grpc_opt paths=source_relative --grpc-gateway_out ./proto --grpc-gateway_opt paths=source_relative ./proto/ms-stakeholders.proto
import (
	"context"
	"errors"
	"fmt"
	"ms-stakeholders/model"
	auth "ms-stakeholders/proto"
	repo "ms-stakeholders/repo"

	"github.com/golang-jwt/jwt/v4"
)

type AuthenticationHandler struct {
	auth.UnimplementedAuthenticationServiceServer
	Repo *repo.UserRepository
}

type AuthenticationTokens struct {
	Id          int64
	AccessToken string
}

// ActivateAccount implements auth.AuthenticationServiceServer.
// Subtle: this method shadows the method (UnimplementedAuthenticationServiceServer).ActivateAccount of AuthenticationHandler.UnimplementedAuthenticationServiceServer.
func (h AuthenticationHandler) ActivateAccount(context.Context, *auth.ActivationRequest) (*auth.ActivationResponse, error) {
	panic("unimplemented")
}

// ChangePassword implements auth.AuthenticationServiceServer.
// Subtle: this method shadows the method (UnimplementedAuthenticationServiceServer).ChangePassword of AuthenticationHandler.UnimplementedAuthenticationServiceServer.
func (h AuthenticationHandler) ChangePassword(context.Context, *auth.ChangeRequest) (*auth.Response, error) {
	panic("unimplemented")
}

// ForgotPassword implements auth.AuthenticationServiceServer.
// Subtle: this method shadows the method (UnimplementedAuthenticationServiceServer).ForgotPassword of AuthenticationHandler.UnimplementedAuthenticationServiceServer.
func (h AuthenticationHandler) ForgotPassword(context.Context, *auth.ForgottenRequest) (*auth.Response, error) {
	panic("unimplemented")
}

// mustEmbedUnimplementedAuthenticationServiceServer implements auth.AuthenticationServiceServer.
// Subtle: this method shadows the method (UnimplementedAuthenticationServiceServer).mustEmbedUnimplementedAuthenticationServiceServer of AuthenticationHandler.UnimplementedAuthenticationServiceServer.
func (h AuthenticationHandler) mustEmbedUnimplementedAuthenticationServiceServer() {
	panic("unimplemented")
}

func (h AuthenticationHandler) Login(ctx context.Context, request *auth.Request) (*auth.Response, error) {
	user, err := h.Repo.FindByName(request.Username)
	if request.Password == user.Password {
		return &auth.Response{
			IsValid:  true,
			JwtToken: h.GenerateAccessToken(user),
		}, nil
	} else if err != nil {
		return nil, err
	} else {
		return nil, errors.New("password is incorrect")
	}
}

func (h AuthenticationHandler) Register(ctx context.Context, request *auth.RegisterRequest) (*auth.Response, error) {
	err := h.Repo.CreateUser(request)
	user := &model.User{
		ID:       0,
		Username: request.Username,
		Password: request.Password,
		Email:    request.Email,
		Name:     request.Name,
		Surname:  request.Surname,
	}
	if err != nil {
		println("Error while creating a new user")
		return nil, err
	}
	return &auth.Response{
		IsValid:  true,
		JwtToken: h.GenerateAccessToken(*user),
	}, nil
}

/*func (h AuthenticationHandler) CreateToken(username string) (jwtToken string) {
	var jwtKey = []byte("secret_key")

	claims := jwt.MapClaims{
		"exp":      time.Now().Add(time.Hour * 24).Unix(),
		"jti":      uuid.New().String(),
		"id":       2,
		"username": "userrr",
		"personId": 2,
		"role":     "roleee",
	}

	token := jwt.NewWithClaims(jwt.SigningMethodHS256, claims)

	tokenString, err := token.SignedString(jwtKey)

	if err != nil {
		fmt.Println("Greska pri potpisivanju tokena:", err)
		return
	}

	return tokenString
}*/

var _key = "your_secret_key"
var _issuer = "your_issuer"
var _audience = "your_audience"

func (h AuthenticationHandler) GenerateAccessToken(user model.User) string {
	authenticationResponse := AuthenticationTokens{}

	claims := jwt.MapClaims{
		//"jti":      uuid.New().String(),
		"id":       fmt.Sprintf("%d", 2),
		"username": "userrr",
		"personId": fmt.Sprintf("%d", 3),
		//"role":     "tourist",
		//"exp":      time.Now().Add(time.Minute * 60 * 24).Unix(),
		//"iat":      time.Now().Unix(),
	}

	jwtToken := CreateToken(claims, 60*24)
	authenticationResponse.Id = 2
	//authenticationResponse.AccessToken = jwtToken

	return jwtToken
}

func CreateToken(claims jwt.MapClaims, expirationTimeInMinutes float64) string {
	jwtKey := []byte(_key)

	token := jwt.NewWithClaims(jwt.SigningMethodHS256, claims)

	tokenString, err := token.SignedString(jwtKey)
	if err != nil {
		fmt.Println("Greška pri potpisivanju tokena:", err)
		return ""
	}

	return tokenString
}
