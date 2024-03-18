using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Explorer.API.Controllers.Author
{

    //[Authorize(Policy = "authorPolicy")]
    [Route("api/author/objects")]
   

    public class ObjectController: BaseApiController
    {
        private readonly IObjectService _objectService;

        public ObjectController(IObjectService objectsService)
        {
            _objectService = objectsService;
        }

        [HttpGet]
        [Authorize(Policy = "authorPolicy")]
        public ActionResult<PagedResult<ObjectDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _objectService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("getAllPublic")]
        [Authorize(Policy = "authorPolicy")]
        public ActionResult<List<ObjectDto>> GetAllPublicObjects()
        {
            var result = _objectService.GetAllPublicObjects();
            return CreateResponse(result);
        }

        [HttpPost]
        [Authorize(Policy = "authorPolicy")]
        public ActionResult<ObjectDto> Create([FromBody] ObjectDto points)
        {
            var result = _objectService.Create(points);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Policy = "authorPolicy")]
        public ActionResult<ObjectDto> Update([FromBody] ObjectDto points)
        {
            var result = _objectService.Update(points);
            return CreateResponse(result);
        }

        [HttpPatch("setPublic/{id:int}")]
        [Authorize(Policy = "administratorPolicy")]
        public ActionResult<ObjectDto> SetPublic(int id)
        {
            var result = _objectService.SetPublic(id);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "authorPolicy")]
        public ActionResult Delete(int id)
        {
            var result = _objectService.Delete(id);
            return CreateResponse(result);
        }
    }

}

