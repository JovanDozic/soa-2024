package handler

import (
	"encoding/json"
	"log"
	"ms-blogs/model"
	"ms-blogs/service"
	"net/http"

	"github.com/gorilla/mux"
)

type BlogHandler struct {
	BlogService        *service.BlogService
	BlogCommentService *service.BlogCommentService
}

func (handler *BlogHandler) Get(writer http.ResponseWriter, req *http.Request) {
	blogId := mux.Vars(req)["id"]
	log.Printf("Blog with id: %s", blogId)

	blog, err := handler.BlogService.FindBlog(blogId)
	if err != nil {
		writer.WriteHeader(http.StatusNotFound)
		return
	}

	comments, _ := handler.BlogCommentService.GetByBlogId(blogId)

	var commentPointers []*model.BlogComment
	for _, comment := range comments {
		commentPointers = append(commentPointers, &comment)
	}

	blog.BlogComments = commentPointers

	writer.Header().Set("Content-Type", "application/json")
	writer.WriteHeader(http.StatusOK)
	json.NewEncoder(writer).Encode(blog)
}

func (handler *BlogHandler) Create(writer http.ResponseWriter, req *http.Request) {
	var blog model.Blog
	err := json.NewDecoder(req.Body).Decode(&blog)
	if err != nil {
		println("Error while parsing json")
		writer.WriteHeader(http.StatusBadRequest)
		return
	}
	err = handler.BlogService.Create(&blog)
	if err != nil {
		println("Error while creating a new blog")
		writer.WriteHeader(http.StatusExpectationFailed)
		return
	}
	writer.WriteHeader(http.StatusCreated)
	writer.Header().Set("Content-Type", "application/json")
}

func (handler *BlogHandler) GetAll(writer http.ResponseWriter, req *http.Request) {
	log.Printf("Getting all blogs")
	blogs, err := handler.BlogService.GetAll()
	writer.Header().Set("Content-Type", "application/json")
	if err != nil {
		writer.WriteHeader(http.StatusNotFound)
		return
	}
	writer.WriteHeader(http.StatusOK)
	json.NewEncoder(writer).Encode(blogs)
}
