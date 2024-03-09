using LuceneAcco.Data.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuceneAcco.Data.Abstractions
{
    public interface ILuceneEngineRepository
    {
        void AddToIndex(IEnumerable values);
        List<Accommodation> Search(string field, string keyword);
    }
}
