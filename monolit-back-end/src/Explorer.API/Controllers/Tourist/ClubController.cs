using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using Newtonsoft.Json;
using System.Text;

namespace Explorer.API.Controllers.Tourist
{
    //[Authorize(Policy = "touristPolicy")]
    [Route("api/club")]
    public class ClubController : BaseApiController
    {
        private readonly IClubService _clubService;
        private readonly string _msToursUrl = "http://localhost:8081/ms-tours";
        static readonly HttpClient _client = new();

        public ClubController(IClubService clubService)
        {
            _clubService = clubService;
    
        }

        [HttpGet("getAll")]
        public ActionResult<PagedResult<ClubRegistrationDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            return CreateResponse(_clubService.GetPaged(page, pageSize));
        }

        [HttpPost]
        public async Task<ActionResult<ClubRegistrationDto>> CreateAsync([FromBody] ClubRegistrationDto club)
        {
            string uri = $"{_msToursUrl}/create-club";
            string clubJson = JsonConvert.SerializeObject(club);
            HttpContent httpContent = new StringContent(clubJson, Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await _client.PostAsync(uri, httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            string content = await response.Content.ReadAsStringAsync();
            return CreateResponse(content.ToResult());
        }

        [HttpPut("{id:int}")]
        public ActionResult<ClubRegistrationDto> Update([FromBody] ClubRegistrationDto reg, int id)
        {
            //reg.Id = id;
            var result = _clubService.Update(reg);
            return CreateResponse(result);
        }
       

     /*   [HttpPut("members/{id:int}")]
        public ActionResult<ClubRegistrationDto> DropMember([FromBody] ClubRegistrationDto club, int id)
        {
            if (HttpContext.User.Identity != null)
            {
                var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value);
                if (userId != club.OwnerId)
                {
                    return BadRequest("You are not owner of the club.");
                }
                
            }


            if (!club.MembersId.Contains(id))
            {
                return BadRequest("User is not member of the club.");
            }

            
            ClubRegistrationDto validated=_clubService.MemberExist(club,id);
            
                var result = _clubService.Update(validated);

            return CreateResponse(result);
        }
     */
    }
}
