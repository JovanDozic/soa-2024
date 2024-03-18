using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist;
[Route("api/tourist/campaign")]
public class CampaignController : BaseApiController
{
    private readonly ICampaignService _campaignService;
    public CampaignController(ICampaignService campaignService)
    {
        _campaignService = campaignService;
    }

    [HttpPost]
    public ActionResult<CampaignDto> Create([FromBody] CampaignDto dto)
    {
        return CreateResponse(_campaignService.Create(dto));
    }

    [HttpGet("getAll")]
    public ActionResult<PagedResult<CampaignDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
    {
        var result = _campaignService.GetPaged(page, pageSize);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        else
        {
            return BadRequest("Failed to retrieve campaigns.");
        }
    }

    [HttpGet("getAll/{touristId:int}")]
    public ActionResult<PagedResult<CampaignDto>> GetAllByTouristId(int touristId)
    {
        var result = _campaignService.GetByTouristId(touristId);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        else
        {
            return BadRequest($"Failed to retrieve campaigns for tourist id: {touristId}");
        }
    }
}