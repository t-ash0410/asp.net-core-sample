using System.Linq;
using System.Collections.Generic;
using Lib.Books.Entity;
using Lib.Infrastructure;

namespace Lib.Books.Repository{
  public interface IBookSearchRepository{
    IEnumerable<BookOverview> Search(string word);
  }
  
  public class BookSearchRepository : IBookSearchRepository{
    private readonly BigDataAccessContext _ctx;

    public BookSearchRepository(BigDataAccessContext ctx){
      this._ctx = ctx;
    }

    public IEnumerable<BookOverview> Search(string word){
      var res = this._ctx.Client.Search<BookOverview>((s => s
          .Query(q => q
              .Match(m => m
                  .Field(f => f.Description)
                  .Query(word)
              )
          )
      ));
      var res2 = this._ctx.Client.Search<BookOverview>();
      return res.Hits.Select(_ => _.Source);
    }

  }
}
