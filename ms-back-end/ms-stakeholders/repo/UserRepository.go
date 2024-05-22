package repo

import (
	"log"
	"ms-stakeholders/model"
	auth "ms-stakeholders/proto"

	"gorm.io/gorm"
)

type UserRepository struct {
	DatabaseConnection *gorm.DB
}

func (repo *UserRepository) FindByName(username string) (model.User, error) {
	user := model.User{}
	dbResult := repo.DatabaseConnection.First(&user, "username = ?", username)
	log.Printf(user.Password)
	if dbResult != nil {
		return user, dbResult.Error
	}
	return user, nil
}

func (repo *UserRepository) CreateUser(request *auth.RegisterRequest) error {
	log.Printf("Usao u userRepo")
	user := &model.User{
		ID:       0,
		Username: request.Username,
		Password: request.Password,
		Email:    request.Email,
		Name:     request.Name,
		Surname:  request.Surname,
	}
	dbResult := repo.DatabaseConnection.Create(user)
	if dbResult.Error != nil {
		return dbResult.Error
	}
	println("Rows affected: ", dbResult.RowsAffected)
	return nil
}
