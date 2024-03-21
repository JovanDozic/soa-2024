package repo

import (
	"fmt"

	"gorm.io/gorm"

	"ms-tours/model"
)

type ProblemRepository struct {
	DatabaseConnection *gorm.DB
}

func (repo *ProblemRepository) FindById(id string) (model.Problem, error) {
	problem := model.Problem{}
	dbResult := repo.DatabaseConnection.First(&problem, "id = ?", id)
	if dbResult.Error != nil {
		return problem, dbResult.Error
	}
	return problem, nil
}

func (repo *ProblemRepository) FindForTourist(id string) (model.Problem, error) {
	fmt.Print("Creating Repository...")
	problem := model.Problem{}
	dbResult := repo.DatabaseConnection.First(&problem, "touristId = ?", id)
	if dbResult.Error != nil {
		return problem, dbResult.Error
	}
	return problem, nil
}

func (repo *ProblemRepository) Create(problem *model.Problem) error {
	dbResult := repo.DatabaseConnection.Create(problem)
	if dbResult.Error != nil {
		return dbResult.Error
	}
	println("Rows affected: ", dbResult.RowsAffected)
	return nil
}

func (repo *ProblemRepository) GetAll() ([]model.Problem, error) {
	problems := []model.Problem{}
	dbResult := repo.DatabaseConnection.Find(&problems)
	if dbResult.Error != nil {
		return nil, dbResult.Error
	}
	return problems, nil
}
