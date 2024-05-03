package repo

import (
	"context"
	"fmt"
	"log"
	"os"

	"github.com/google/uuid"
	"github.com/neo4j/neo4j-go-driver/v5/neo4j"
	"main.go/model"
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

/*
func (userRepo *UserRepository) CreateUser(user *model.User) error {
	ctx := context.Background()
	session := userRepo.driver.NewSession(ctx, neo4j.SessionConfig{DatabaseName: "neo4j"})
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
}*/

func (ur UserRepository) CreateUser(user *model.User) error {
	ctx := context.Background()
	session := ur.driver.NewSession(ctx, neo4j.SessionConfig{DatabaseName: "neo4j"})
	defer session.Close(ctx)

	if user.Id == uuid.Nil {
		user.Id = uuid.New()
	}

	idString := user.Id.String()

	savedUser, err := session.ExecuteWrite(ctx,
		func(transaction neo4j.ManagedTransaction) (any, error) {
			result, err := transaction.Run(ctx,
				"CREATE (u:User) SET u.id = $id, u.username = $username, u.password = $password, u.isActive = $isActive, u.role = $role, u.email = $email RETURN u.username + ', from node ' + id(u)",
				map[string]any{"id": idString, "username": user.Username, "password": user.Password,
					"isActive": user.IsActive, "role": user.Role, "email": user.Email})
			if err != nil {
				return nil, err
			}

			if result.Next(ctx) {
				return result.Record().Values[0], nil
			}

			return nil, result.Err()
		})
	if err != nil {
		ur.logger.Println("Error inserting User:", err)
		return err
	}
	ur.logger.Println(savedUser.(string))
	return nil
}

func (userRepo *UserRepository) FollowUser(userID, followedUserID string) error {
	ctx := context.Background()
	session := userRepo.driver.NewSession(ctx, neo4j.SessionConfig{DatabaseName: "neo4j"})
	defer session.Close(ctx)

	_, err := session.ExecuteWrite(ctx,
		func(transaction neo4j.ManagedTransaction) (interface{}, error) {
			_, err := transaction.Run(ctx,
				"MATCH (follower:User {username: $userID}), (followed:User {username: $followedUserID}) MERGE (follower)-[:FOLLOWS]->(followed)",
				map[string]interface{}{"userID": userID, "followedUserID": followedUserID})
			return nil, err
		})
	if err != nil {
		userRepo.logger.Println("Error following user: ", err)
		return err
	}
	return nil
}

func (userRepo *UserRepository) UnfollowUser(userID, unfollowedUserID string) error {
	ctx := context.Background()
	session := userRepo.driver.NewSession(ctx, neo4j.SessionConfig{DatabaseName: "neo4j"})
	defer session.Close(ctx)

	userRepo.logger.Println(userID)
	userRepo.logger.Println(unfollowedUserID)

	_, err := session.ExecuteWrite(ctx,
		func(transaction neo4j.ManagedTransaction) (interface{}, error) {
			_, err := transaction.Run(ctx,
				"MATCH (follower:User {username: $userID})-[rel:FOLLOWS]->(unfollowed:User {username: $unfollowedUserID}) DELETE rel",
				map[string]interface{}{"userID": userID, "unfollowedUserID": unfollowedUserID})
			return nil, err
		})
	if err != nil {
		userRepo.logger.Println("Error unfollowing user: ", err)
		return err
	}
	return nil
}

func (userRepo *UserRepository) GetAllUsers(limit int) (model.Users, error) {
	ctx := context.Background()
	session := userRepo.driver.NewSession(ctx, neo4j.SessionConfig{DatabaseName: "neo4j"})
	defer session.Close(ctx)

	usersResult, err := session.ExecuteRead(ctx,
		func(transation neo4j.ManagedTransaction) (any, error) {
			result, err := transation.Run(ctx,
				`MATCH (u:User)
			RETURN u`, nil)
			if err != nil {
				return nil, err
			}
			var users model.Users
			for result.Next(ctx) {
				node, ok := result.Record().Get("u")
				if !ok {
					return nil, fmt.Errorf("following node not found")
				}

				userNode, ok := node.(neo4j.Node)
				if !ok {
					return nil, fmt.Errorf("following node not found or not of expected type")
				}
				id, _ := userNode.Props["id"].(string)
				idConverted, _ := uuid.Parse(id)
				username, _ := userNode.Props["username"].(string)
				role, _ := userNode.Props["role"].(int64)
				roleConverted, _ := model.ConvertToRole(role)
				email, _ := userNode.Props["email"].(string)
				isActive, _ := userNode.Props["isActive"].(bool)
				users = append(users, &model.User{
					Id:       idConverted,
					Username: username,
					Role:     roleConverted,
					Email:    email,
					IsActive: isActive,
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

func (userRepo *UserRepository) GetRecommendedUsers(userID string) (model.Users, error) {
	ctx := context.Background()
	session := userRepo.driver.NewSession(ctx, neo4j.SessionConfig{DatabaseName: "neo4j"})
	defer session.Close(ctx)

	recommendedUsersResult, err := session.ExecuteRead(ctx,
		func(transaction neo4j.ManagedTransaction) (interface{}, error) {
			result, err := transaction.Run(ctx,
				`MATCH (user:User {username: $userID})-[:FOLLOWS]->(followedUser)-[:FOLLOWS]->(recommendedUser:User)
                WHERE NOT (user)-[:FOLLOWS]->(recommendedUser)
                RETURN recommendedUser`, map[string]interface{}{"userID": userID})
			if err != nil {
				return nil, err
			}
			var recommendedUsers model.Users
			for result.Next(ctx) {
				node, ok := result.Record().Get("recommendedUser")
				if !ok {
					return nil, fmt.Errorf("following node not found")
				}

				userNode, ok := node.(neo4j.Node)
				if !ok {
					return nil, fmt.Errorf("following node not found or not of expected type")
				}
				id, _ := userNode.Props["id"].(string)
				idConverted, _ := uuid.Parse(id)
				username, _ := userNode.Props["username"].(string)
				role, _ := userNode.Props["role"].(int64)
				roleConverted, _ := model.ConvertToRole(role)
				email, _ := userNode.Props["email"].(string)
				isActive, _ := userNode.Props["isActive"].(bool)

				recommendedUsers = append(recommendedUsers, &model.User{
					Id:       idConverted,
					Username: username,
					Role:     roleConverted,
					Email:    email,
					IsActive: isActive,
				})

			}
			// Provera da li postoji greška prilikom iteriranja kroz rezultate
			if err := result.Err(); err != nil {
				return nil, err
			}

			return recommendedUsers, nil
		})
	if err != nil {
		userRepo.logger.Println("Error getting recommended users: ", err)
		return nil, err
	}
	return recommendedUsersResult.(model.Users), nil
}

/*
func (ur *UserRepository) GetFollowings(userId string) ([]model.User, error) {
	ctx := context.Background()
	session := ur.driver.NewSession(ctx, neo4j.SessionConfig{DatabaseName: "neo4j"})
	defer session.Close(ctx)

	var followedUsers []model.User

	_, err := session.ExecuteWrite(ctx,
		func(transaction neo4j.ManagedTransaction) (interface{}, error) {
			cypherQuery := "MATCH (user:User {id: $userId})-[:FOLLOWS]->(following:User) RETURN following"
			intUserId, err := strconv.Atoi(userId)

			if err != nil {
				log.Printf("Error converting userId to integer: %v", err)
				return nil, err
			}

			result, err := transaction.Run(ctx, cypherQuery, map[string]interface{}{"userId": intUserId})

			if err != nil {
				return nil, err
			}

			for result.Next(ctx) {
				node, ok := result.Record().Get("following")
				if !ok {
					return nil, fmt.Errorf("following node not found")
				}

				userNode, ok := node.(neo4j.Node)
				if !ok {
					return nil, fmt.Errorf("following node not found or not of expected type")
				}

				id, _ := userNode.Props["id"].(int64)
				username, _ := userNode.Props["username"].(string)
				password, _ := userNode.Props["password"].(string)
				role, _ := userNode.Props["role"].(model.UserRole)
				profilePicture, _ := userNode.Props["profilePicture"].(string)
				isActive, _ := userNode.Props["isActive"].(bool)

				following := model.User{
					ID:             id,
					Username:       username,
					Password:       password,
					Role:           role,
					ProfilePicture: profilePicture,
					IsActive:       isActive,
				}
				followedUsers = append(followedUsers, following)
			}

			if err := result.Err(); err != nil {
				ur.logger.Println("Error while retrieving users' followings", err)
				return nil, err
			}

			return nil, nil
		})

	if err != nil {
		return nil, err
	}

	return followedUsers, nil
}
*/
