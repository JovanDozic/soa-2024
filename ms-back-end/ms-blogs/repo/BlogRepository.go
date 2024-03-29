package repo

import (
	"ms-blogs/model"

	"gorm.io/gorm"
)

type BlogRepository struct {
	DatabaseConnection *gorm.DB
}

func (repo *BlogRepository) FindById(id string) (model.Blog, error) {
	blog := model.Blog{}
	dbResult := repo.DatabaseConnection.First(&blog, "id = ?", id)
	if dbResult != nil {
		return blog, dbResult.Error
	}
	return blog, nil
}

func (repo *BlogRepository) CreateBlog(blog *model.Blog) error {
	dbResult := repo.DatabaseConnection.Create(blog)
	if dbResult.Error != nil {
		return dbResult.Error
	}
	println("Rows affected: ", dbResult.RowsAffected)
	return nil
}

func (repo *BlogRepository) GetAll() ([]model.Blog, error) {
	blogs := []model.Blog{}
	dbResult := repo.DatabaseConnection.Find(&blogs)
	if dbResult.Error != nil {
		return nil, dbResult.Error
	}
	return blogs, nil
}
