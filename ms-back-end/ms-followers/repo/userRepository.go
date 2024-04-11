package repo

import (
	"context"
	"github.com/neo4j/neo4j-go-driver/v5/neo4j"
	"log"
	"main.go/model"
	"os"
)

type UserRepository struct {
	driver neo4j.DriverWithContext
	logger *log.Logger
}

func New(logger *log.Logger) (*UserRepository, error) {
	uri := os.Getenv("NEO4J_DB")
	user := os.Getenv("NEO4J_USERNAME")
	pass := os.Getenv("NEO4J_PASS")
	auth := neo4j.BasicAuth(user, pass, "")

	driver, err := neo4j.NewDriverWithContext(uri, auth)
	if err != nil {
		logger.Panic(err)
		return nil, err
	}
	return &UserRepository{
		driver: driver,
		logger: logger,
	}, nil
}

func (userRepo *UserRepository) CheckConnection() {
	ctx := context.Background()
	err := userRepo.driver.VerifyConnectivity(ctx)
	if err != nil {
		userRepo.logger.Panic(err)
		return
	}
	userRepo.logger.Printf(`Neo4J server address: %s`, userRepo.driver.Target().Host)
}
func (userRepo *UserRepository) CloseDriverConnection(ctx context.Context) {
	err := userRepo.driver.Close(ctx)
	if err != nil {
		return
	}
}

func (userRepo *UserRepository) CreateUser(user *model.User) error {
	ctx := context.Background()
	session := userRepo.driver.NewSession(ctx, neo4j.SessionConfig{DatabaseName: "users"})
	defer session.Close(ctx)

	savedUser, err := session.ExecuteWrite(ctx,
		func(transaction neo4j.ManagedTransaction) (any, error) {
			result, err := transaction.Run(ctx,
				"CREATE (u: User) SET u.username = $username, u.password = $password, u.role = $role, u.email = $email RETURN u.username + ', from node ' + id(u)",
				map[string]any{"username": user.Username, "password": user.Password, "role": user.Role, "email": user.Email})
			if err != nil {
				return nil, err
			}
			if result.Next(ctx) {
				return result.Record().Values[0], nil
			}
			return nil, result.Err()
		})
	if err != nil {
		userRepo.logger.Println("Error inserting User: ", err)
		return err
	}
	userRepo.logger.Println(savedUser.(string))
	return nil
}

func (userRepo *UserRepository) GetAllUsers(limit int) (model.Users, error) {
	ctx := context.Background()
	session := userRepo.driver.NewSession(ctx, neo4j.SessionConfig{DatabaseName: "users"})
	defer session.Close(ctx)

	usersResult, err := session.ExecuteRead(ctx,
		func(transation neo4j.ManagedTransaction) (any, error) {
			result, err := transation.Run(ctx,
				`MATCH (u:User)
			RETURN u.username, u.role, u.email`, nil)
			if err != nil {
				return nil, err
			}
			var users model.Users
			for result.Next(ctx) {
				record := result.Record()
				username, ok := record.Get("username")
				if !ok || username == nil {
					username = "0"
				}
				role, _ := record.Get("role")
				email, _ := record.Get("email")
				users = append(users, &model.User{
					Username: username.(string),
					Role:     role.(model.UserRole),
					Email:    email.(string),
				})
			}
			return users, nil
		})
	if err != nil {
		userRepo.logger.Println("Error querying search: ", err)
		return nil, err
	}
	return usersResult.(model.Users), nil
}
