using System;
using Nest;

namespace Lib.Infrastructure
{
  public class ESContext
  {
    public ElasticClient Client { get; }

    public ESContext(string url)
    {
      var uri = new Uri(url);
      var setting = new ConnectionSettings(uri)
        .DefaultMappingFor<Lib.Books.Entity.BookOverview>(map => map.IndexName("books"));
      this.Client = new ElasticClient(setting);
    }
  }
}
