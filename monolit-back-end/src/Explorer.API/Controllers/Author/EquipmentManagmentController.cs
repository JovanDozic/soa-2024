using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/equipmentManagment")]
    public class EquipmentManagmentController : BaseApiController
    {
        private readonly IEquipmentManagmentService _equipmentManagmentService;

        public EquipmentManagmentController(IEquipmentManagmentService equipmentManagmentService)
        {
            _equipmentManagmentService = equipmentManagmentService;
        }

        [HttpGet]
        public ActionResult<PagedResult<EquipmentManagmentDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _equipmentManagmentService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<EquipmentManagmentDto> Create([FromBody] EquipmentManagmentDto equipmentManagment)
        {
            var result = _equipmentManagmentService.Create(equipmentManagment);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<EquipmentManagmentDto> Update([FromBody] EquipmentManagmentDto equipmentManagment)
        {
            var result = _equipmentManagmentService.Update(equipmentManagment);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _equipmentManagmentService.Delete(id);
            return CreateResponse(result);
        }
    }
}
