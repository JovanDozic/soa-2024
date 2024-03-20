package handler

import (
	"encoding/json"
	"log"
	"ms-blogs/service"
	"net/http"

	"github.com/gorilla/mux"
)

type BlogCommentHandler struct {
	BlogCommentService *service.BlogCommentService
}

func (handler *BlogCommentHandler) GetByBlogId(writer http.ResponseWriter, req *http.Request) {
	id := mux.Vars(req)["blogId"]
	log.Printf("Searching for comments for blog ID: %s", id)

	comments, err := handler.BlogCommentService.GetByBlogId(id)
	writer.Header().Set("Content-Type", "application/json")
	if err != nil {
		writer.WriteHeader(http.StatusNotFound)
		return
	}
	writer.WriteHeader(http.StatusOK)
	json.NewEncoder(writer).Encode(comments)
}
