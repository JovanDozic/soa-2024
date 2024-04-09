package model

import (
	"context"
	"fmt"
	"log"
	"strconv"
	"time"

	// NoSQL: module containing Mongo api client
	"go.mongodb.org/mongo-driver/bson"
	"go.mongodb.org/mongo-driver/mongo"
	"go.mongodb.org/mongo-driver/mongo/options"
	"go.mongodb.org/mongo-driver/mongo/readpref"
)

// NoSQL: ProductRepo struct encapsulating Mongo api client
type TourRepository struct {
	cli    *mongo.Client
	logger *log.Logger
}

// NoSQL: Constructor which reads db configuration from environment
func New(ctx context.Context, logger *log.Logger) (*TourRepository, error) {
	//dburi := os.Getenv("MONGO_DB_URI")
	dburi := "mongodb://localhost:27017/"

	client, err := mongo.NewClient(options.Client().ApplyURI(dburi))
	if err != nil {
		return nil, err
	}

	err = client.Connect(ctx)
	if err != nil {
		return nil, err
	}

	return &TourRepository{
		cli:    client,
		logger: logger,
	}, nil
}

// Disconnect from database
func (pr *TourRepository) Disconnect(ctx context.Context) error {
	err := pr.cli.Disconnect(ctx)
	if err != nil {
		return err
	}
	return nil
}

// Check database connection
func (pr *TourRepository) Ping() {
	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()

	// Check connection -> if no error, connection is established
	err := pr.cli.Ping(ctx, readpref.Primary())
	if err != nil {
		pr.logger.Println(err)
	}

	// Print available databases
	databases, err := pr.cli.ListDatabaseNames(ctx, bson.M{})
	if err != nil {
		pr.logger.Println(err)
	}
	fmt.Println(databases)
}

func (pr *TourRepository) GetAll() (Tours, error) {
	// Initialise context (after 5 seconds timeout, abort operation)
	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()

	toursCollection := pr.getCollection()

	var tours Tours
	toursCursor, err := toursCollection.Find(ctx, bson.M{})
	if err != nil {
		pr.logger.Println(err)
		return nil, err
	}
	if err = toursCursor.All(ctx, &tours); err != nil {
		pr.logger.Println(err)
		return nil, err
	}
	return tours, nil
}

func (pr *TourRepository) GetById(id string) (*Tour, error) {
	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()

	toursCollection := pr.getCollection()
	num, err1 := strconv.Atoi(id)
	var tour Tour
	err := toursCollection.FindOne(ctx, bson.M{"_id": num}).Decode(&tour)
	if err1 != nil {
		pr.logger.Println(err)
		return nil, err
	}
	return &tour, nil
}

func (pr *TourRepository) Insert(tour *Tour) error {
	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()
	patientsCollection := pr.getCollection()

	result, err := patientsCollection.InsertOne(ctx, &tour)
	if err != nil {
		pr.logger.Println(err)
		return err
	}
	pr.logger.Printf("Documents ID: %v\n", result.InsertedID)
	return nil
}

func (tr *TourRepository) AddProblem(id string, problem *Problem) error {
	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()
	toursCollection := tr.getCollection()
	update := bson.M{"$push": bson.M{
		"problems": problem,
	}}
	result, err := toursCollection.UpdateOne(ctx, bson.M{"_id": problem.TourId}, update)
	tr.logger.Printf("Documents matched: %v\n", result.MatchedCount)
	tr.logger.Printf("Documents updated: %v\n", result.ModifiedCount)
	if err != nil {
		tr.logger.Println(err)
		return err
	}
	return nil
}

func (tr *TourRepository) AddReview(id string, review *TourReview) error {
	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()
	toursCollection := tr.getCollection()
	num, err := strconv.Atoi(id)
	filter := bson.M{"_id": num}
	update := bson.M{"$push": bson.M{
		"tourReviews": review,
	}}
	result, err := toursCollection.UpdateOne(ctx, filter, update)
	tr.logger.Printf("Documents matched: %v\n", result.MatchedCount)
	tr.logger.Printf("Documents updated: %v\n", result.ModifiedCount)

	if err != nil {
		tr.logger.Println(err)
		return err
	}
	return nil
}

func (pr *TourRepository) InsertClub(club *Club) error {
	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()
	clubsCollection := pr.getCollectionClubs()

	result, err := clubsCollection.InsertOne(ctx, &club)
	if err != nil {
		pr.logger.Println(err)
		return err
	}
	pr.logger.Printf("Documents ID: %v\n", result.InsertedID)
	return nil
}

func (pr *TourRepository) getCollectionClubs() *mongo.Collection {
	clubDatabase := pr.cli.Database("ms-tours")
	clubsCollection := clubDatabase.Collection("clubs")
	return clubsCollection
}

func (pr *TourRepository) getCollection() *mongo.Collection {
	patientDatabase := pr.cli.Database("ms-tours")
	patientsCollection := patientDatabase.Collection("tours")
	return patientsCollection
}

/*import (
	"log"

	"gorm.io/gorm"

	"ms-tours/model"
)

type TourRepository struct {
	DatabaseConnection *gorm.DB
}

func (repo *TourRepository) FindById(id string) (model.Tour, error) {
	tour := model.Tour{}
	dbResult := repo.DatabaseConnection.First(&tour, "id = ?", id)
	if dbResult.Error != nil {
		return tour, dbResult.Error
	}
	return tour, nil
}

func (repo *TourRepository) GetAll() ([]model.Tour, error) {
	tours := []model.Tour{}
	dbResult := repo.DatabaseConnection.Find(&tours)
	if dbResult.Error != nil {
		return nil, dbResult.Error
	}
	return tours, nil
}

func (repo *TourRepository) CreateTour(tour *model.Tour) error {
	log.Printf("Usao u tourRepo")
	dbResult := repo.DatabaseConnection.Create(tour)
	if dbResult.Error != nil {
		return dbResult.Error
	}
	println("Rows affected: ", dbResult.RowsAffected)
	return nil
}*/
