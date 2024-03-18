using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using Explorer.Tours.API.Dtos.Tours;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;

namespace Explorer.Payments.Core.UseCases.Shopping
{
    public class SaleService : CrudService<SaleDto, Sale>, ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        public SaleService(ICrudRepository<Sale> repository, IMapper mapper, ISaleRepository saleRepository) : base(repository, mapper)
        {
            _saleRepository = saleRepository;
        }

        public Result<SaleDto> Activate(long id)
        {
            var sale = CrudRepository.Get(id);
            sale.ActivateSale();
            CrudRepository.Update(sale);
            return MapToDto(sale);
        }

        public Result<List<TourDto>> GetAllToursOnSale()
        {
            return _saleRepository.GetAllToursOnSale();
        }
    }
}
