using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public
{
    public interface ICouponService
    {
        Result<PagedResult<CouponDto>> GetPaged(int page, int pageSize);
        Result<CouponDto> Create(CouponDto couponDto);
        Result<CouponDto> Update(CouponDto couponDto);
        Result Delete(int id);
    }
}
