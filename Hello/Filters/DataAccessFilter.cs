using Microsoft.AspNetCore.Mvc.Filters;
using Lib.Infrastructure;

namespace Hello.Filters {
  public abstract class DataAccessFilterBase : ActionFilterAttribute
  {
  }

  public class DataAccessFilter : DataAccessFilterBase 
  {
    private readonly DataAccessContext _ctx;

    public DataAccessFilter(DataAccessContext ctx)
    {
      this._ctx = ctx;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
      this._ctx.Open();    
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
      this._ctx.Close();
    }
  }
}
