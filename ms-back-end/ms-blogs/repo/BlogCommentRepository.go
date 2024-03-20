package repo

import (
	"ms-blogs/model"

	"gorm.io/gorm"
)

type BlogCommentRepository struct {
	DatabaseConnection *gorm.DB
}

func (repo *BlogCommentRepository) FindById(id string) (model.BlogComment, error) {
	blogComment := model.BlogComment{}
	dbResult := repo.DatabaseConnection.First(&blogComment, "id = ?", id)
	if dbResult != nil {
		return blogComment, dbResult.Error
	}
	return blogComment, nil
}

func (repo *BlogCommentRepository) CreateBlogComment(blogComment *model.BlogComment) error {
	dbResult := repo.DatabaseConnection.Create(blogComment)
	if dbResult.Error != nil {
		return dbResult.Error
	}
	println("Rows affected: ", dbResult.RowsAffected)
	return nil
}

func (repo *BlogCommentRepository) GetAll() ([]model.BlogComment, error) {
	blogComments := []model.BlogComment{}
	dbResult := repo.DatabaseConnection.Find(&blogComments)
	if dbResult.Error != nil {
		return nil, dbResult.Error
	}
	return blogComments, nil
}

func (repo *BlogCommentRepository) GetByBlogId(blogId string) ([]model.BlogComment, error) {
	blogComments := []model.BlogComment{}
	dbResult := repo.DatabaseConnection.Find(&blogComments, "blog_id = ?", blogId)
	if dbResult.Error != nil {
		return nil, dbResult.Error
	}
	return blogComments, nil
}
