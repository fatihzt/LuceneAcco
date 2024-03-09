using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using LuceneAcco.Business.Contracts;
using LuceneAcco.Business.Dto;
using LuceneAcco.Data.Abstractions;
using LuceneAcco.Data.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LuceneAcco.Business.Services
{
    public class LuceneSearchEngine : ILuceneSearchEngine
    {
        private readonly ILuceneEngineRepository repository;
        private readonly IMapper mapper;
        public LuceneSearchEngine(ILuceneEngineRepository repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        List<AccommodationGetDto> ILuceneSearchEngine.FindAccommodation(string query)
        {
            var result = repository.Search("AccommodationCode",query);
            var accos = mapper.Map<IEnumerable<Accommodation>, IEnumerable<AccommodationGetDto>>(result);
            return (List<AccommodationGetDto>)accos;
        }
    }
}
