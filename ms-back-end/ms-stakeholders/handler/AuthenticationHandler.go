package handler

//protoc -I ./proto --go_out ./proto --go_opt paths=source_relative --go-grpc_out ./proto --go-grpc_opt paths=source_relative --grpc-gateway_out ./proto --grpc-gateway_opt paths=source_relative ./proto/ms-stakeholders.proto
import (
	"context"
	"fmt"
	auth "ms-stakeholders/proto"
	repo "ms-stakeholders/repo"
)

type AuthenticationHandler struct {
	auth.UnimplementedAuthenticationServiceServer
	repo *repo.UserRepository
}

func (h AuthenticationHandler) Login(ctx context.Context, request *auth.Request) (*auth.Response, error) {
	return &auth.Response{
		IsValid:  true,
		JwtToken: fmt.Sprintf("Tokeeen"),
	}, nil
}

func (h AuthenticationHandler) Register(ctx context.Context, request *auth.RegisterRequest) (*auth.Response, error) {

	err := repo.CreateUser(&request)
	if err != nil {
		println("Error while creating a new user")
		return err
	}
	return &auth.Response{
		IsValid:  true,
		JwtToken: fmt.Sprintf(request.Username),
	}, nil
}
