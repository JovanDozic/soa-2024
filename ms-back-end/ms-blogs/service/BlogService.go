package service

import (
	"fmt"
	"ms-blogs/model"
	"ms-blogs/repo"
)

type BlogService struct {
	BlogRepository *repo.BlogRepository
}

func (service *BlogService) FindBlog(id string) (*model.Blog, error) {
	blog, err := service.BlogRepository.FindById(id)
	if err != nil {
		return nil, fmt.Errorf(fmt.Sprintf("blog with id %s not found", id))
	}
	return &blog, nil
}

func (service *BlogService) Create(blog *model.Blog) error {
	err := service.BlogRepository.CreateBlog(blog)
	if err != nil {
		return err
	}
	return nil
}

func (service *BlogService) GetAll() ([]model.Blog, error) {
	blogs, err := service.BlogRepository.GetAll()
	if err != nil {
		return nil, err
	}
	return blogs, nil
}

func (service *BlogService) Rate(blogRating *model.BlogRating, ratings []model.BlogRating, blogId string) error {
	err := service.BlogRepository.Rate(blogRating, ratings, blogId)
	if err != nil {
		return err
	}
	return nil
}
