using System.Linq;
using System.Collections.Generic;
using Lib.Books.Entity;
using Lib.Infrastructure;

namespace Lib.Books.Repository
{
  public interface IBookSearchRepository
  {
    void Init();
    IEnumerable<BookOverview> Search(string word);
    void Register(BookOverview book);
  }

  public class BookSearchRepository : IBookSearchRepository
  {
    private readonly ESContext _ctx;

    public BookSearchRepository(ESContext ctx)
    {
      this._ctx = ctx;
    }

    public void Init()
    {
      var name = "books";
      if (this._ctx.Client.Indices.Exists(name).Exists)
      {
        this._ctx.Client.Indices.Delete(name);
      }
      this._ctx.Client.Indices.Create("books", index => index.Map<BookOverview>(m => m.AutoMap()));
    }

    public IEnumerable<BookOverview> Search(string word)
    {
      var res = this._ctx.Client.Search<BookOverview>((s => s
          .Query(q => q
            .Bool(b => b
              .Should(
                s => s.Match(m => m
                  .Field(f => f.Name)
                  .Query(word)
                ),
                s => s.Match(m => m
                  .Field(f => f.Description)
                  .Query(word)
                ),
                s => s.Match(m => m
                  .Field(f => f.Category)
                  .Query(word)
                )
              )
              .MinimumShouldMatch(1)
            )
          )
      ));

      return res.Hits.Select(_ => _.Source);
    }

    public void Register(BookOverview book)
    {
      this._ctx.Client.IndexDocument(book);
    }

  }
}
