package repo

import (
	"gorm.io/gorm"

	"ms-tours/model"
)

type ClubRepository struct {
	DatabaseConnection *gorm.DB
}

func (repo *ClubRepository) FindById(id string) (model.Club, error) {
	club := model.Club{}
	dbResult := repo.DatabaseConnection.First(&club, "id = ?", id)
	if dbResult.Error != nil {
		return club, dbResult.Error
	}
	return club, nil
}

func (repo *ClubRepository) Create(club *model.Club) error {
	dbResult := repo.DatabaseConnection.Create(club)
	if dbResult.Error != nil {
		return dbResult.Error
	}
	println("Rows affected: ", dbResult.RowsAffected)
	return nil
}

func (repo *ClubRepository) GetAll() ([]model.Club, error) {
	problems := []model.Club{}
	dbResult := repo.DatabaseConnection.Find(&problems)
	if dbResult.Error != nil {
		return nil, dbResult.Error
	}
	return problems, nil
}
