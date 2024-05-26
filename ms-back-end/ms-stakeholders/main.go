package main

import (
	"log"
	"ms-stakeholders/config"
	"ms-stakeholders/handler"
	"ms-stakeholders/model"
	"ms-stakeholders/repo"
	"net"
	"os"
	"os/signal"
	"syscall"

	auth "ms-stakeholders/proto"

	"google.golang.org/grpc"
	"google.golang.org/grpc/reflection"
	"gorm.io/driver/postgres"
	"gorm.io/gorm"
)

func initDB() *gorm.DB {
	connectionString := "user=postgres password=super dbname=ms-stakeholders host=ms-stakeholders-database port=5432 sslmode=disable"
	database, err := gorm.Open(postgres.Open(connectionString), &gorm.Config{})
	if err != nil {
		log.Fatalf("Failed to connect to database: %v", err)
	}

	err = database.AutoMigrate(&model.User{})
	if err != nil {
		log.Fatalf("Failed to auto migrate database: %v", err)
	}

	return database
}

func main() {
	database := initDB()
	if database == nil {
		print("FAILED TO CONNECT TO DB")
		return
	}

	userRepo := &repo.UserRepository{DatabaseConnection: database}
	cfg := config.GetConfig()

	listener, err := net.Listen("tcp", cfg.Address)
	if err != nil {
		log.Fatalln(err)
	}
	defer func(listener net.Listener) {
		err := listener.Close()
		if err != nil {
			log.Fatal(err)
		}
	}(listener)

	// Bootstrap gRPC server.
	grpcServer := grpc.NewServer()
	reflection.Register(grpcServer)

	// Bootstrap gRPC service server and respond to request.
	authHandler := handler.AuthenticationHandler{
		UnimplementedAuthenticationServiceServer: auth.UnimplementedAuthenticationServiceServer{},
		Repo:                                     userRepo}
	auth.RegisterAuthenticationServiceServer(grpcServer, authHandler)

	go func() {
		if err := grpcServer.Serve(listener); err != nil {
			log.Fatal("server error: ", err)
		}
	}()

	stopCh := make(chan os.Signal)
	signal.Notify(stopCh, syscall.SIGTERM)

	<-stopCh

	grpcServer.Stop()
}
