using System.Collections.Generic;
using Lib.Books.Entity;
using Lib.Books.Repository;

namespace Lib.Books.UseCase{
  public interface IBookInteractor{
    void Init();
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

    public void Init(){
      this._repo.Init();
      this._searchRepository.Init();

      var initialData = new List<BookOverview>(){
        new BookOverview(1, "book1", "test book", "category1"),
        new BookOverview(2, "book2", "test book2", "分類2"),
        new BookOverview(3, "本３", "テスト用の本", "category1")
      };
      initialData.ForEach(b => {
        this._repo.Register(b);
        this._searchRepository.Register(b);
      });
    }

    public IEnumerable<BookOverview> GetBookOverviews(){
      return this._repo.GetBookOverviews();
    }

    public IEnumerable<BookOverview> Search(string word){
      return this._searchRepository.Search(word);
    }
  }
}
