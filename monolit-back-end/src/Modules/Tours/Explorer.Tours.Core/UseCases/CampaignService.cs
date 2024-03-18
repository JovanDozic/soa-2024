using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using FluentResults;

namespace Explorer.Tours.Core.UseCases;
public class CampaignService : CrudService<CampaignDto, Campaign>, ICampaignService
{
    public CampaignService(ICrudRepository<Campaign> repository, IMapper mapper) : base(repository, mapper) { }

    public Result<List<CampaignDto>> GetByTouristId(int touristId)
    {
        try
        {
            var allCampaignsResult = GetPaged(1, int.MaxValue);

            if (allCampaignsResult.IsSuccess)
            {
                var allCampaigns = allCampaignsResult.Value.Results;
                return Result.Ok(allCampaigns.FindAll(campaign => campaign.TouristId == touristId));
            }
            else
                throw new Exception($"Tourist id: {touristId} has no campaigns.");
        }
        catch (Exception ex)
        {
            return Result.Fail("An error occurred: " + ex.Message);
        }
    }
}

