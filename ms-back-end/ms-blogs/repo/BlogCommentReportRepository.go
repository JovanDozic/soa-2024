package repo

import (
	"log"
	"ms-blogs/model"
	"time"

	"gorm.io/gorm"
)

type BlogCommentReportRepository struct {
	DatabaseConnection *gorm.DB
}

func (repo *BlogCommentReportRepository) Create(blogCommentReport *model.BlogCommentReport) error {
	dbResult := repo.DatabaseConnection.Create(blogCommentReport)
	if dbResult.Error != nil {
		return dbResult.Error
	}
	println("Rows affected: ", dbResult.RowsAffected)
	return nil
}

func (repo *BlogCommentReportRepository) GetAll() ([]model.BlogCommentReport, error) {
	blogCommentReports := []model.BlogCommentReport{}
	dbResult := repo.DatabaseConnection.Find(&blogCommentReports)
	if dbResult.Error != nil {
		return nil, dbResult.Error
	}
	return blogCommentReports, nil
}
func (repo *BlogCommentReportRepository) GetReviewed() ([]model.BlogCommentReport, error) {
	log.Printf("u repo sam")
	allReports, err := repo.GetAll()
	if err != nil {
		return nil, err
	}
	var filteredReports []model.BlogCommentReport

	for _, report := range allReports {
		if report.IsReviewed {
			filteredReports = append(filteredReports, report)
		}
	}
	return filteredReports, nil

}

func (repo *BlogCommentReportRepository) GetUnReviewed() ([]model.BlogCommentReport, error) {
	allReports, err := repo.GetAll()
	if err != nil {
		return nil, err
	}
	var filteredReports []model.BlogCommentReport

	for _, report := range allReports {
		if !report.IsReviewed {
			filteredReports = append(filteredReports, report)
		}
	}
	return filteredReports, nil

}

func (repo *BlogCommentReportRepository) GetByBlogId(blogId string) ([]model.BlogCommentReport, error) {
	blogCommentReports := []model.BlogCommentReport{}
	dbResult := repo.DatabaseConnection.Where("blog_id = ?", blogId).Find(&blogCommentReports)
	if dbResult.Error != nil {
		return nil, dbResult.Error
	}
	return blogCommentReports, nil
}

func (repo *BlogCommentReportRepository) DidUserReportComment(userId string, blogId string, commentCreatedTime time.Time) (bool, error) {
	var count int64
	dbResult := repo.DatabaseConnection.Model(&model.BlogCommentReport{}).Where("report_author_id = ? AND blog_id = ? AND time_comment_created = ?", userId, blogId, commentCreatedTime).Count(&count)
	if dbResult.Error != nil {
		return false, dbResult.Error
	}
	return count > 0, nil
}
