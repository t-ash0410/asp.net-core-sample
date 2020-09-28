using System.Collections.Generic;
using Lib.Books.Entity;
using Lib.Infrastructure;

namespace Lib.Books.Repository{
  public interface IBookRepository{
    IEnumerable<BookOverview> GetBookOverviews();
  }

  public class BookRepository: IBookRepository{
    private readonly DataAccessContext _ctx;
    public BookRepository(DataAccessContext ctx){
      this._ctx = ctx;
    }
    public IEnumerable<BookOverview> GetBookOverviews(){
      var result = new List<BookOverview>();
      this._ctx.Command.CommandText = "SELECT * FROM books";
      using(var reader = this._ctx.Command.ExecuteReader()){
        while(reader.Read()){
          var id = int.Parse(reader["id"].ToString());
          var name = reader["name"].ToString();
          var description = reader["description"].ToString();
          result.Add(new BookOverview(id, name, description));
        }
      }
      return result;
    }
  }
}
