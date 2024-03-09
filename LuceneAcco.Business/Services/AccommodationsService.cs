using LuceneAcco.Business.Contracts;
using LuceneAcco.Business.Dto;
using LuceneAcco.Data.Abstractions;
using LuceneAcco.Data.Model;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuceneAcco.Business.Services
{
    public class AccommodationsService : IAccommodationsService
    {
        private readonly IAccommodationsRepository _repo;
        private readonly IMapper mapper;
        public AccommodationsService(IAccommodationsRepository repo, IMapper mapper)
        {
            _repo = repo;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<AccommodationGetDto>> GetAllAccommodations()
        {
            var result = _repo.GetAllAccommodations();
            if (result == null) throw new Exception();

            return  mapper.Map<IEnumerable<Accommodation>, IEnumerable<AccommodationGetDto>>(result);
        }
    }
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
    public class Mapper : IMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return source.Adapt<TDestination>();
        }
    }
}
