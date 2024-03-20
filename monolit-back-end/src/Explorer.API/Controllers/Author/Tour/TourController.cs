using Explorer.Blog.Core.Domain;
using Explorer.Tours.API.Dtos.Tours;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Explorer.API.Controllers.Author.Tour
{
    //[Authorize(Policy = "authorPolicy")]
    [Route("api/author/tour")]
    public class TourController : BaseApiController
    {
        private readonly ITourService _tourService;
        private readonly string _msToursUrl = "http://localhost:8081/ms-tours";
        static readonly HttpClient _client = new();

        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [AllowAnonymous]
        [HttpGet("getAll")]
        public ActionResult<TourDto> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var response = CreateResponse(_tourService.GetPaged(page, pageSize));
            return response;
        }

        [HttpGet("getAllPublic")]
        public ActionResult<TourDto> GetAllPublic()
        {
            return CreateResponse(_tourService.GetAllPublic());
        }

        
        [AllowAnonymous]
        [HttpGet("getAllPointsForTours")]
        public ActionResult<List<PointDto>> GetAllPublicPointsForTours()
        {
            var result = _tourService.GetAllPublicPointsForTours();
            return CreateResponse(result);
        }

        [AllowAnonymous]
        [HttpPut("findTours")]
        public ActionResult<List<TourDto>> FindToursContainingPoints([FromBody] List<PointDto> pointsToFind)
        {
            if (pointsToFind.Count < 2)
            {
                return BadRequest("List must contain at least 2 points.");
            }

            return CreateResponse(_tourService.FindToursContainingPoints(pointsToFind));
        }

        [Authorize(Policy = "touristPolicy")]
        [HttpPut("findToursByFollowers")]
        public ActionResult<List<TourDto>> GetToursReviewedByUsersIFollow([FromQuery] int currentUserId, [FromQuery] int ratedTourId)
        {
            return CreateResponse(_tourService.GetToursReviewedByUsersIFollow(currentUserId, ratedTourId));
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<TourDto>> Create([FromBody] TourDto tour)
        {
            string uri = $"{_msToursUrl}/tours/createTour";
            string tourJson = JsonConvert.SerializeObject(tour);
            HttpContent httpContent = new StringContent(tourJson, Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await _client.PostAsync(uri, httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            string content = await response.Content.ReadAsStringAsync();
            return CreateResponse(content.ToResult());
        }

        [HttpPut("{id:int}")]
        public ActionResult<TourDto> Update([FromBody] TourDto dataIn)
        {

            return CreateResponse(_tourService.Update(dataIn));
        }

        [HttpGet("getById/{id}")]
        public ActionResult<TourDto> GetTour(long id)
        {
            return CreateResponse(_tourService.GetById(id));
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id) 
        {
            return CreateResponse(_tourService.Delete(id));
        }

        [HttpGet("publishTour/{id}")]
        public ActionResult PublishTour(long id)
        {
            return CreateResponse(_tourService.PublishTour(id));
        }

        [HttpGet("arhiveTour/{id}")]
        public ActionResult ArhiveTour(long id)
        {
            return CreateResponse(_tourService.ArhiveTour(id));
        }

        [HttpPost("rateTour/{tourId:int}")]
        [Authorize(Policy = "TouristPolicy")]

        public async Task<ActionResult<TourReviewDto>> RateTour([FromRoute] int tourId, [FromBody] TourReviewDto tourReview)
        {
            try
            {
                string payload = JsonSerializer.Serialize(tourReview);
                string uri = $"{_msTourUrl}/createReview";

                var content = new StringContent(payload, Encoding.UTF8, "application/json");

                using HttpResponseMessage response = await _client.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode);
                }

                string responseContent = await response.Content.ReadAsStringAsync();

                return CreateResponse(responseContent.ToResult());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("addProblem/{tourId:int}")]
        //[Authorize(Policy = "TouristPolicy")]

        public async Task<ActionResult<ProblemDto>> AddProblem([FromRoute] int tourId, [FromBody] ProblemDto problem)
        {
            try
            {
                string payload = JsonSerializer.Serialize(problem);
                string uri = $"{_msTourUrl}/createProblem";

                var content = new StringContent(payload, Encoding.UTF8, "application/json");

                using HttpResponseMessage response = await _client.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode);
                }

                string responseContent = await response.Content.ReadAsStringAsync();

                return CreateResponse(responseContent.ToResult());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [AllowAnonymous]
        [HttpGet("averageRating/{tourId:int}")]
        public ActionResult<double> GetAverageRating(int tourId)
        {
            return CreateResponse(_tourService.GetAverageRating(tourId));
        }


        [AllowAnonymous]
        [HttpGet("searchByPointDistance")]
        public ActionResult<TourDto> SearchByPointDistance([FromQuery] double longitude, [FromQuery] double latitude, [FromQuery] int distance)
        {
            return CreateResponse(_tourService.SearchByPointDistance(longitude, latitude, distance));
        }

        [HttpPatch("publishPoint/{id}")]
        public ActionResult PublishPoint(long id, [FromQuery] string pointName)
        {
            return CreateResponse(_tourService.PublishPoint(id, pointName));
        }

        [HttpGet("getIdByName/{name}")]
        public ActionResult<long> GetIdByName(string name)
        {
            return CreateResponse(_tourService.GetIdByName(name));
        }
        [AllowAnonymous]
        [HttpGet("getAllAuthorsTours/{idUser:int}")]
        public ActionResult<TourDto> GetAllAuthorsTours([FromRoute] int idUser)
        {
            return CreateResponse(_tourService.GetAllAuthorsTours(idUser));
        }
    }
}
