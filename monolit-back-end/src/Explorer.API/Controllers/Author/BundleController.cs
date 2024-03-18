using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    //[Authorize(Policy = "authorPolicy")]
    [Route("api/author/bundle")]
    public class BundleController : BaseApiController
    {
        private readonly IBundleService _bundleService;

        public BundleController(IBundleService bundleService) 
        { 
            _bundleService = bundleService;
        }

        [HttpPost("create")]
        public ActionResult<BundleDto> Create([FromBody] BundleDto dataIn) 
        {
            return CreateResponse(_bundleService.Create(dataIn));
        }

        [HttpPatch("update")]
        public ActionResult<BundleDto> Update([FromBody] BundleDto dataIn)
        {
            return CreateResponse(_bundleService.Update(dataIn));
        }

        [HttpDelete("delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            return CreateResponse(_bundleService.Delete(id));
        }

        [HttpGet("publish/{id}")]
        public ActionResult<BundleDto> Publish(long id)
        {
            return CreateResponse(_bundleService.Publish(id));
        }

        [HttpGet("archive/{id}")]
        public ActionResult Archive(long id)
        {
            return CreateResponse(_bundleService.Archive(id));
        }

        [HttpGet("getAll")]
        public ActionResult<PagedResult<BundleDto>> GetAll()
        {
            var page = 1;
            var pageSize = 20;
            return CreateResponse(_bundleService.GetPaged(page, pageSize));
        }
    }
}
