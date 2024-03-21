package handler

import (
	"encoding/json"
	"ms-blogs/model"
	"ms-blogs/service"
	"net/http"

	"github.com/gorilla/mux"
)

type BlogCommentReportHandler struct {
	BlogCommentReportService *service.BlogCommentReportService
}

func (handler *BlogCommentReportHandler) Create(writer http.ResponseWriter, req *http.Request) {
	var comment model.BlogCommentReport
	err := json.NewDecoder(req.Body).Decode(&comment)
	if err != nil {
		println("Error while parsing json")
		writer.WriteHeader(http.StatusBadRequest)
		return
	}
	err = handler.BlogCommentReportService.Create(&comment)
	if err != nil {
		println("Error while creating a new comment")
		writer.WriteHeader(http.StatusExpectationFailed)
		return
	}
	writer.WriteHeader(http.StatusCreated)
	writer.Header().Set("Content-Type", "application/json")
}

func (handler *BlogCommentReportHandler) GetAll(writer http.ResponseWriter, req *http.Request) {
	reports, err := handler.BlogCommentReportService.GetAll()
	writer.Header().Set("Content-Type", "application/json")
	if err != nil {
		writer.WriteHeader(http.StatusNotFound)
		return
	}
	writer.WriteHeader(http.StatusOK)
	json.NewEncoder(writer).Encode(reports)
}

func (handler *BlogCommentReportHandler) GetByBlogId(writer http.ResponseWriter, req *http.Request) {
	id := req.URL.Query().Get("blogId")
	reports, err := handler.BlogCommentReportService.GetByBlogId(id)
	writer.Header().Set("Content-Type", "application/json")
	if err != nil {
		writer.WriteHeader(http.StatusNotFound)
		return
	}
	writer.WriteHeader(http.StatusOK)
	json.NewEncoder(writer).Encode(reports)
}

// /ms-blogs/comments/reports/didUserReport/{userId}/{blogId}
func (handler *BlogCommentReportHandler) DidUserReportComment(writer http.ResponseWriter, req *http.Request) {
	userId := mux.Vars(req)["userId"]
	blogId := mux.Vars(req)["blogId"]
	var comment model.BlogComment
	err := json.NewDecoder(req.Body).Decode(&comment)
	if err != nil {
		println("Error while parsing json")
		writer.WriteHeader(http.StatusBadRequest)
		return
	}

	didUserReport, err := handler.BlogCommentReportService.DidUserReportComment(userId, blogId, comment.TimeCreated)
	writer.Header().Set("Content-Type", "application/json")
	if err != nil {
		writer.WriteHeader(http.StatusNotFound)
		return
	}
	writer.WriteHeader(http.StatusOK)
	json.NewEncoder(writer).Encode(didUserReport)
	println("Did user report comment: ", didUserReport)
}
