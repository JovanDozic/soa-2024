using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases.Shopping
{
    public class CouponService : CrudService<CouponDto, Coupon>, ICouponService
    {
        private readonly ICouponRepository _couponRepository;
        private readonly IMapper _mapper;
        public CouponService(ICrudRepository<Coupon> repository, IMapper mapper, ICouponRepository couponRepository) : base(repository, mapper)
        {
            _couponRepository = couponRepository;
            _mapper = mapper;
        }

    }
}
