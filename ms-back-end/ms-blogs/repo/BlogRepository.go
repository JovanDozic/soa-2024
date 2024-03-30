package repo

import (
	"log"
	"ms-blogs/model"

	"github.com/google/uuid"
	"gorm.io/gorm"
)

type BlogRepository struct {
	DatabaseConnection   *gorm.DB
	BlogRatingRepository *BlogRatingRepository
}

func (repo *BlogRepository) FindById(id string) (model.Blog, error) {
	blog := model.Blog{}
	dbResult := repo.DatabaseConnection.First(&blog, "id = ?", id)
	if dbResult != nil {
		return blog, dbResult.Error
	}
	return blog, nil
}

func (repo *BlogRepository) Rate(blogRating *model.BlogRating, ratings []model.BlogRating, blogId string) error {

	blog, err := repo.FindById(blogId)
	if err != nil {
		log.Println("Error:", err)
		return err
	}

	var found bool
	for _, rating := range ratings {
		if rating.BlogId == blogRating.BlogId && rating.UserId == blogRating.UserId {
			found = true
			if rating.Vote == blogRating.Vote {
				dbResult := repo.DatabaseConnection.Delete(&model.BlogRating{}, "id = ?", rating.ID)
				log.Printf("deleted rating - rate blog")
				if dbResult.Error != nil {
					return dbResult.Error
				}
				println("Rows affected: ", dbResult.RowsAffected)
			} else {
				blogRating.ID = rating.ID
				dbResult := repo.DatabaseConnection.Save(blogRating)
				log.Printf("updated rating - rate blog")
				if dbResult.Error != nil {
					return dbResult.Error
				}
			}
			break
		}
	}

	if !found {
		dbResult := repo.DatabaseConnection.Create(blogRating)
		log.Printf("kreirao blog rating ako nema pre - rate blog")
		if dbResult.Error != nil {
			return dbResult.Error
		}
	}

	blogRatings := []model.BlogRating{}
	dbResult := repo.DatabaseConnection.Find(&blogRatings)
	if dbResult.Error != nil {
		return nil
	}

	var filteredRatings []model.BlogRating

	for _, rating := range blogRatings {
		if rating.BlogId == blogRating.BlogId {
			filteredRatings = append(filteredRatings, rating)
		}
	}

	positiveRatings := 0
	for _, rating := range filteredRatings {
		if rating.Vote == 1 {
			positiveRatings++
		}
	}
	if dbResult.Error != nil {
		filteredRatings = nil
	}

	negativeRatings := 0
	for _, rating := range filteredRatings {
		if rating.Vote == 2 {
			negativeRatings++
		}
	}

	blog.NetVotes = positiveRatings - negativeRatings

	log.Printf("The number is: %d\n", blog.NetVotes)
	// Update the blog
	err = repo.Update(&blog)
	if err != nil {
		return err
	}

	return nil
}

func (repo *BlogRepository) Delete(blogId uuid.UUID) error {

	dbResult1 := repo.DatabaseConnection.Delete(&model.BlogComment{}, "blog_id = ? ", blogId)
	if dbResult1.Error != nil {
		return dbResult1.Error
	}

	dbResult := repo.DatabaseConnection.Delete(&model.Blog{}, "id = ? ", blogId)
	if dbResult.Error != nil {
		return dbResult.Error
	}
	println("Rows affected: ", dbResult.RowsAffected)
	return nil
}

func (repo *BlogRepository) CreateBlog(blog *model.Blog) error {
	log.Printf("dosao u repo - kreiranje bloga")
	dbResult := repo.DatabaseConnection.Create(blog)
	if dbResult.Error != nil {
		return dbResult.Error
	}
	println("Rows affected: ", dbResult.RowsAffected)
	return nil
}
func (repo *BlogRepository) Update(blog *model.Blog) error {
	log.Printf("Updating blog with ID: %s", blog.ID)

	// Perform the update operation
	dbResult := repo.DatabaseConnection.Save(blog)
	if dbResult.Error != nil {
		return dbResult.Error
	}

	log.Println("Blog updated successfully")
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
