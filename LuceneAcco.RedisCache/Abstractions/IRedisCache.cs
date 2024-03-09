using LuceneAcco.Business.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuceneAcco.RedisCache.Abstractions
{
    public interface IRedisCache
    {
        Task<bool> AddAccommodationsHash();
        Task<List<AccommodationGetDto>> GetAllAccommodationsHash();

    }
}
