package service

import (
	"ms-blogs/model"
	"ms-blogs/repo"
	"time"
)

type BlogCommentReportService struct {
	Repository *repo.BlogCommentReportRepository
}

func (service *BlogCommentReportService) Create(report *model.BlogCommentReport) error {
	err := service.Repository.Create(report)
	if err != nil {
		return err
	}
	return nil
}

func (service *BlogCommentReportService) GetAll() ([]model.BlogCommentReport, error) {
	reports, err := service.Repository.GetAll()
	if err != nil {
		return nil, err
	}
	return reports, nil
}

func (service *BlogCommentReportService) GetByBlogId(blogId string) ([]model.BlogCommentReport, error) {
	reports, err := service.Repository.GetByBlogId(blogId)
	if err != nil {
		return nil, err
	}
	return reports, nil
}

func (service *BlogCommentReportService) DidUserReportComment(userId string, blogId string, commentCreatedTime time.Time) (bool, error) {
	didUserReport, err := service.Repository.DidUserReportComment(userId, blogId, commentCreatedTime)
	if err != nil {
		return false, err
	}
	return didUserReport, nil
}
