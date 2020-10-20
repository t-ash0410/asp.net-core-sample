namespace Lib.Books.Entity{
  public class BookOverview{
    public int Id {get; set;}
    public string Name {get;}
    public string Description{get;}
    public string Category{get;}

    public BookOverview(int id, string name, string description, string category){
      this.Id = id;
      this.Name = name;
      this.Description = description;
      this.Category = category;
    }
  }
}
