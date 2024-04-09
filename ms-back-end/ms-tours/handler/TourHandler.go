package handler

import (
	"context"
	"fmt"
	"log"
	"ms-tours/model"
	"net/http"

	"github.com/gorilla/mux"
)

type KeyProduct struct{}

type TourHandler struct {
	logger *log.Logger
	// NoSQL: injecting product repository
	repo *model.TourRepository
}

// Injecting the logger makes this code much more testable.
func NewToursHandler(l *log.Logger, r *model.TourRepository) *TourHandler {
	return &TourHandler{l, r}
}

func (p *TourHandler) GetAllTours(rw http.ResponseWriter, h *http.Request) {
	tours, err := p.repo.GetAll()
	if err != nil {
		p.logger.Print("Database exception: ", err)
	}

	if tours == nil {
		return
	}

	err = tours.ToJSON(rw)
	if err != nil {
		http.Error(rw, "Unable to convert to json", http.StatusInternalServerError)
		p.logger.Fatal("Unable to convert to json :", err)
		return
	}
}

func (p *TourHandler) GetTourById(rw http.ResponseWriter, h *http.Request) {
	vars := mux.Vars(h)
	id := vars["id"]

	tours, err := p.repo.GetById(id)
	if err != nil {
		p.logger.Print("Database exception: ", err)
	}

	if tours == nil {
		http.Error(rw, "Tour with given id not found", http.StatusNotFound)
		p.logger.Printf("Tour with id: '%s' not found", id)
		return
	}

	err = tours.ToJSON(rw)
	if err != nil {
		http.Error(rw, "Unable to convert to json", http.StatusInternalServerError)
		p.logger.Fatal("Unable to convert to json :", err)
		return
	}
}

func (p *TourHandler) PostTour(rw http.ResponseWriter, h *http.Request) {
	tour := h.Context().Value(KeyProduct{}).(*model.Tour)
	p.repo.Insert(tour)
	rw.WriteHeader(http.StatusCreated)
}

func (p *TourHandler) AddProblem(rw http.ResponseWriter, h *http.Request) {
	vars := mux.Vars(h)
	id := vars["id"]
	fmt.Printf("usao u add problem")

	problem := h.Context().Value(KeyProduct{}).(*model.Problem)

	p.repo.AddProblem(id, problem)
	rw.WriteHeader(http.StatusOK)
}

func (p *TourHandler) AddReview(rw http.ResponseWriter, h *http.Request) {
	vars := mux.Vars(h)
	id := vars["id"]
	fmt.Printf("id: %s", id)
	review := h.Context().Value(KeyProduct{}).(*model.TourReview)

	p.repo.AddReview(id, review)
	rw.WriteHeader(http.StatusOK)
}

func (p *TourHandler) PostClub(rw http.ResponseWriter, h *http.Request) {
	club := h.Context().Value(KeyProduct{}).(*model.Club)
	p.repo.InsertClub(club)
	rw.WriteHeader(http.StatusCreated)
}

func (p *TourHandler) MiddlewareContentTypeSet(next http.Handler) http.Handler {
	return http.HandlerFunc(func(rw http.ResponseWriter, h *http.Request) {
		p.logger.Println("Method [", h.Method, "] - Hit path :", h.URL.Path)

		rw.Header().Add("Content-Type", "application/json")

		next.ServeHTTP(rw, h)
	})
}

func (p *TourHandler) MiddlewareTourDeserialization(next http.Handler) http.Handler {
	return http.HandlerFunc(func(rw http.ResponseWriter, h *http.Request) {
		tour := &model.Tour{}
		err := tour.FromJSON(h.Body)
		if err != nil {
			http.Error(rw, "Unable to decode json", http.StatusBadRequest)
			p.logger.Fatal(err)
			return
		}

		ctx := context.WithValue(h.Context(), KeyProduct{}, tour)
		h = h.WithContext(ctx)

		next.ServeHTTP(rw, h)
	})
}

func (p *TourHandler) MiddlewareClubDeserialization(next http.Handler) http.Handler {
	return http.HandlerFunc(func(rw http.ResponseWriter, h *http.Request) {
		club := &model.Club{}
		err := club.FromJSON(h.Body)
		if err != nil {
			http.Error(rw, "Unable to decode json", http.StatusBadRequest)
			p.logger.Fatal(err)
			return
		}

		ctx := context.WithValue(h.Context(), KeyProduct{}, club)
		h = h.WithContext(ctx)

		next.ServeHTTP(rw, h)
	})
}

func (p *TourHandler) MiddlewareProblemDeserialization(next http.Handler) http.Handler {
	return http.HandlerFunc(func(rw http.ResponseWriter, h *http.Request) {
		problem := &model.Problem{}
		err := problem.FromJSON(h.Body)
		if err != nil {
			http.Error(rw, "Unable to decode json", http.StatusBadRequest)
			p.logger.Fatal(err)
			return
		}

		ctx := context.WithValue(h.Context(), KeyProduct{}, problem)
		h = h.WithContext(ctx)

		next.ServeHTTP(rw, h)
	})
}

func (p *TourHandler) MiddlewareTourReviewDeserialization(next http.Handler) http.Handler {
	return http.HandlerFunc(func(rw http.ResponseWriter, h *http.Request) {
		review := &model.TourReview{}
		err := review.FromJSON(h.Body)
		if err != nil {
			http.Error(rw, "Unable to decode json", http.StatusBadRequest)
			p.logger.Fatal(err)
			return
		}

		ctx := context.WithValue(h.Context(), KeyProduct{}, review)
		h = h.WithContext(ctx)

		next.ServeHTTP(rw, h)
	})
}

/*import (
	"encoding/json"
	"log"
	"net/http"

	"github.com/gorilla/mux"

	"ms-tours/model"
	"ms-tours/service"
)

type TourHandler struct {
	TourService *service.TourService
}

func (handler *TourHandler) Get(writer http.ResponseWriter, req *http.Request) {
	id := mux.Vars(req)["id"]
	log.Printf("Tour with id %s", id)
	tour, err := handler.TourService.FindTour(id)
	jsonResponse, _ := json.Marshal(tour)
	if err != nil {
		log.Printf("%s", err)
		http.Error(writer, "Internal server error", http.StatusInternalServerError)
		return
	}
	writer.Header().Set("Content-Type", "application/json")
	writer.WriteHeader(http.StatusOK)
	writer.Write(jsonResponse)
}

func (handler *TourHandler) GetAll(writer http.ResponseWriter, req *http.Request) {
	log.Printf("Usao u getall")
	tours, err := handler.TourService.GetAll()
	if err != nil {
		log.Printf("%s", err)
		http.Error(writer, "Internal server error", http.StatusInternalServerError)
		return
	}
	writer.Header().Set("Content-Type", "application/json")
	writer.WriteHeader(http.StatusOK)
	json.NewEncoder(writer).Encode(tours)
}

func (handler *TourHandler) Create(writer http.ResponseWriter, request *http.Request) {
	var tour model.Tour
	err := json.NewDecoder(request.Body).Decode(&tour)

	log.Printf("%s", tour)
	log.Printf("Usao u createee")
	if err != nil {
		println("Error while parsing json")
		log.Printf(err.Error())
		writer.WriteHeader(http.StatusBadRequest)
		return
	}
	err = handler.TourService.Create(&tour)
	if err != nil {
		println("Error while creating a new tour")
		writer.WriteHeader(http.StatusExpectationFailed)
		return
	}
	writer.WriteHeader(http.StatusCreated)
	writer.Header().Set("Content-Type", "application/json")
}*/
