using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuceneAcco.Data.Model
{
    public class Accommodation
    {
        public int AccommodationId { get; set; }
        public string AccommodationCode { get; set; }
        public string AccommodationName { get; set; }
        public string SubPlace { get; set; }
        public string Place { get; set; }
        public string Country { get; set; }
    }
}
