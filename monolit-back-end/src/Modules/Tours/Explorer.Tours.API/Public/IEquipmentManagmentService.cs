using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface IEquipmentManagmentService
    {
        Result<PagedResult<EquipmentManagmentDto>> GetPaged(int page, int pageSize);
        Result<EquipmentManagmentDto> Create(EquipmentManagmentDto equipmentManagment);
        Result<EquipmentManagmentDto> Update(EquipmentManagmentDto equipmentManagment);
        Result Delete(int id);
    }
}
