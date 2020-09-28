using System.Collections.Generic;
using Lib.Books.Entity;
using Lib.Books.Repository;

namespace Lib.Books.UseCase{
  public interface IBookInteractor{
    IEnumerable<BookOverview> GetBookOverviews();
  }

  public class BookInteractor: IBookInteractor{
    private readonly IBookRepository _repo;

    public BookInteractor(IBookRepository repo){
      this._repo = repo;
    }

    public IEnumerable<BookOverview> GetBookOverviews(){
      return this._repo.GetBookOverviews();
    }
  }
}
