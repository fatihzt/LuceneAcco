using LuceneAcco.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuceneAcco.Data.Abstractions
{
    public interface IAccommodationsRepository
    {
        public IEnumerable<Accommodation> GetAllAccommodations();

    }
}
