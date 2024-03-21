package service

import (
	"fmt"

	"ms-tours/model"
	"ms-tours/repo"

)

type ProblemService struct {
	ProblemRepository *repo.ProblemRepository
}

func (service *ProblemService) FindProblem(id string) (*model.Problem, error) {
	problem, err := service.ProblemRepository.FindById(id)
	if err != nil {
		return nil, fmt.Errorf(fmt.Sprintf("menu item with id %s not found", id))
	}
	return &problem, nil
}

func (service *ProblemService) FindProblemForTourist(id string) (*model.Problem, error) {
	problem, err := service.ProblemRepository.FindForTourist(id)
	if err != nil {
		return nil, fmt.Errorf(fmt.Sprintf("menu item with id %s not found", id))
	}
	return &problem, nil
}

func (service *ProblemService) Create(problem *model.Problem) error {
	fmt.Print("Service creating")
	err := service.ProblemRepository.Create(problem)
	if err != nil {
		return err
	}
	return nil
}

func (service *ProblemService) GetAll() ([]model.Problem, error) {
	problems, err := service.ProblemRepository.GetAll()
	if err != nil {
		return nil, err
	}
	return problems, nil
}
