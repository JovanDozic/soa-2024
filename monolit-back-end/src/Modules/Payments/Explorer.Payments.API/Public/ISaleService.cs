using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public
{
    public interface ISaleService
    {
        Result<PagedResult<SaleDto>> GetPaged(int page, int pageSize);
        Result<SaleDto> Create(SaleDto sale);
        Result Delete(int id);
        Result<SaleDto> Activate(long id);
        Result<List<TourDto>> GetAllToursOnSale();
    }
}
