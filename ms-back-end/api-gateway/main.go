package main

import (
	"context"
	"log"
	"net/http"
	"os"
	"os/signal"
	"syscall"

	"github.com/grpc-ecosystem/grpc-gateway/v2/runtime"
	"google.golang.org/grpc"
	"google.golang.org/grpc/credentials/insecure"

	"api-gateway/config"
	"api-gateway/proto"
)

func main() {
	cfg := config.GetConfig()
	log.Println("Server started")
	conn, err := grpc.DialContext(
		context.Background(),
		cfg.StakeholderServiceAddress,
		grpc.WithBlock(),
		grpc.WithTransportCredentials(insecure.NewCredentials()),
	)

	if err != nil {
		log.Fatalln("Failed to dial server:", err)
	}

	gwmux := runtime.NewServeMux()

	client := proto.NewAuthenticationServiceClient(conn)
	err = proto.RegisterAuthenticationServiceHandlerClient(
		context.Background(),
		gwmux,
		client,
	)
	if err != nil {
		log.Fatalf("Failed to register gateway:", err)
	}

	gwServer := &http.Server{
		Addr:    cfg.Address,
		Handler: gwmux,
	}

	go func() {
		if err := gwServer.ListenAndServe(); err != nil {
			log.Fatal("server error: ", err)
		}
	}()

	stopCh := make(chan os.Signal, 1)
	signal.Notify(stopCh, syscall.SIGTERM)

	<-stopCh

	if err = gwServer.Close(); err != nil {
		log.Println("Error closing the API Gateway Server: ", err)
	}
}
