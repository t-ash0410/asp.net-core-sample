using System.Collections.Generic;
using Lib.Books.Entity;
using Lib.Books.Repository;

namespace Lib.Books.UseCase{
  public interface IBookInteractor{
    IEnumerable<BookOverview> GetBookOverviews();
    IEnumerable<BookOverview> Search(string word);
  }

  public class BookInteractor: IBookInteractor{
    private readonly IBookRepository _repo;
    private readonly IBookSearchRepository _searchRepository;

    public BookInteractor(IBookRepository repo, IBookSearchRepository search){
      this._repo = repo;
      this._searchRepository = search;
    }

    public IEnumerable<BookOverview> GetBookOverviews(){
      return this._repo.GetBookOverviews();
    }

    public IEnumerable<BookOverview> Search(string word){
      return this._searchRepository.Search(word);
    }
  }
}
