using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using LuceneAcco.Data.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuceneAcco.Data.Abstractions;

namespace LuceneAcco.Data.Repositories
{
    public class LuceneEngineRepository:ILuceneEngineRepository
    {
        private readonly Analyzer _Analyzer;
        private readonly Lucene.Net.Store.Directory _Directory;
        private IndexWriter _IndexWriter;
        private IndexSearcher _IndexSearcher;
        private Document _Document;
        private QueryParser _QueryParser;
        private Query _Query;
        private string _IndexPath = @"C:\LuceneIndex";

        public LuceneEngineRepository()
        {
            _Analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            _Directory = FSDirectory.Open(_IndexPath);
        }

        public void AddToIndex(IEnumerable values)
        {
            using (_IndexWriter = new IndexWriter(_Directory, _Analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                foreach (var loopEntity in values)
                {
                    _Document = new Document();

                    foreach (var loopProperty in loopEntity.GetType().GetProperties())
                    {
                        _Document.Add(new Field(loopProperty.Name, loopProperty.GetValue(loopEntity).ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    }

                    _IndexWriter.AddDocument(_Document);
                    _IndexWriter.Optimize();
                    _IndexWriter.Commit();
                }
            }
        }
        public List<Accommodation> Search(string field, string keyword)
        {
            // Üzerinde arama yapmak istediğimiz field için bir query oluşturuyoruz.
            _QueryParser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, field, _Analyzer);
            _Query = _QueryParser.Parse(keyword);

            using (_IndexSearcher = new IndexSearcher(_Directory, true))
            {
                List<Accommodation> accos = new();
                var result = _IndexSearcher.Search(_Query, 10);

                foreach (var loopDoc in result.ScoreDocs.OrderBy(s => s.Score))
                {
                    _Document = _IndexSearcher.Doc(loopDoc.Doc);

                    accos.Add(new Accommodation() { AccommodationCode = _Document.Get("AccommodationCode"), AccommodationName = _Document.Get("AccommodationName") });
                }

                return accos;
            }
        }
    }
}
