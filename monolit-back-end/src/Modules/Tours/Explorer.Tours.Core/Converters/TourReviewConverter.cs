using Explorer.Tours.API.Dtos.Tours;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Converters
{
    public static class TourReviewConverter
    {
        public static TourReviewDto ToDto(this TourReview tourReview)
        {
            if (tourReview == null) return null;
            return new TourReviewDto
            {
                Rating = tourReview.Rating,
                Comment = tourReview.Comment,
                TouristId = tourReview.TouristId,
                TouristUsername = tourReview.TouristUsername,
                TourDate = tourReview.TourDate,
                CreationDate = tourReview.CreationDate,
                Images = tourReview.Images,
            };
        }

        public static TourReview ToDomain(this TourReviewDto tourReviewDto)
        {
            return tourReviewDto == null ? null :
                new TourReview(tourReviewDto.Rating, tourReviewDto.Comment, tourReviewDto.TouristId, tourReviewDto.TouristUsername, tourReviewDto.TourDate, tourReviewDto.CreationDate, tourReviewDto.Images);
        }
    }
}