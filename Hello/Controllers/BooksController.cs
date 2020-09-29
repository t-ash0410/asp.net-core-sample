using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Lib.Books.UseCase;
using Hello.Filters;

namespace Hello.Controllers
{
    public class BooksController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookInteractor _interactor;

        public BooksController(ILogger<HomeController> logger, IBookInteractor interactor)
        {
            this._logger = logger;
            this._interactor = interactor;
        }

        [HttpGet]
        [ServiceFilter(typeof(DataAccessFilterBase))]
        public IActionResult Get()
        {
            var books = this._interactor.GetBookOverviews();
            return new JsonResult(books);
        }
    }
}
