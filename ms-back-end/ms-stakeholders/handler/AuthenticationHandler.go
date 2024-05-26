package handler

//protoc -I ./proto --go_out ./proto --go_opt paths=source_relative --go-grpc_out ./proto --go-grpc_opt paths=source_relative --grpc-gateway_out ./proto --grpc-gateway_opt paths=source_relative ./proto/ms-stakeholders.proto
import (
	"context"
	"errors"
	"log"
	"ms-stakeholders/jwtGenerator"
	auth "ms-stakeholders/proto"
	repo "ms-stakeholders/repo"
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
	log.Printf("Username req:" + request.Username)
	if request.Password == user.Password {
		return jwtGenerator.GenerateAccessToken(&user)
	} else if err != nil {
		return nil, err
	} else {
		return nil, errors.New("password is incorrect")
	}
}

func (h AuthenticationHandler) Register(ctx context.Context, request *auth.RegisterRequest) (*auth.Response, error) {
	err := h.Repo.CreateUser(request)
	newUser, _ := h.Repo.FindByName(request.Username)
	if err != nil {
		println("Error while creating a new user")
		return nil, err
	}
	return jwtGenerator.GenerateAccessToken(&newUser)
}
