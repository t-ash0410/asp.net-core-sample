namespace Lib.Books.Entity{
  public class BookOverview{
    public int Id {get;}
    public string Name {get;}
    public string Description{get;}

    public BookOverview(int id, string name, string description){
      this.Id = id;
      this.Name = name;
      this.Description = description;
    }
  }
}
