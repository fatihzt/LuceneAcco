using LuceneAcco.Business.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuceneAcco.Business.Contracts
{
    public interface IAccommodationsService
    {
        Task<IEnumerable<AccommodationGetDto>> GetAllAccommodations();

    }
}
