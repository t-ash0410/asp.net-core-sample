using System.Collections.Generic;
using Lib.Books.Entity;
using Lib.Infrastructure;

namespace Lib.Books.Repository{
  public interface IBookRepository{
    void Init();
    IEnumerable<BookOverview> GetBookOverviews();
    void Register(BookOverview book);
  }

  public class BookRepository: IBookRepository{
    private readonly DataAccessContext _ctx;
    public BookRepository(DataAccessContext ctx){
      this._ctx = ctx;
    }

    public void Init(){
      this._ctx.Command.CommandText = $@"
        DROP TABLE IF EXISTS books;
        CREATE TABLE IF NOT EXISTS books (
            `id` MEDIUMINT NOT NULL AUTO_INCREMENT,
            `name` VARCHAR(255) NOT NULL,
            `description` TEXT NOT NULL,
            `category` TEXT NOT NULL,
            PRIMARY KEY (id)
        );";
        
      this._ctx.Command.ExecuteNonQuery();
    }

    public IEnumerable<BookOverview> GetBookOverviews(){
      var result = new List<BookOverview>();
      this._ctx.Command.CommandText = "SELECT * FROM books";
      using(var reader = this._ctx.Command.ExecuteReader()){
        while(reader.Read()){
          var id = int.Parse(reader["id"].ToString());
          var name = reader["name"].ToString();
          var description = reader["description"].ToString();
          var category = reader["category"].ToString();
          result.Add(new BookOverview(id, name, description, category));
        }
      }
      return result;
    }

    public void Register(BookOverview book){
      this._ctx.Command.CommandText = $@"
        INSERT INTO books
        (
          `name`,
          `description`,
          `category`
        )
        VALUES
        (
          @name,
          @description,
          @category
        )
      ";
      this._ctx.Command.Parameters.AddWithValue("@name", book.Name);
      this._ctx.Command.Parameters.AddWithValue("@description", book.Description);
      this._ctx.Command.Parameters.AddWithValue("@category", book.Category);
      this._ctx.Command.ExecuteNonQuery();
      this._ctx.Command.Parameters.Clear();

      this._ctx.Command.CommandText = "SELECT last_insert_id();";
      book.Id = int.Parse(this._ctx.Command.ExecuteScalar().ToString());
    }
  }
}
