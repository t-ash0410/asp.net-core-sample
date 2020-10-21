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

        [HttpPost]
        [ServiceFilter(typeof(DataAccessFilterBase))]
        public IActionResult Init(){
            var books = this._interactor.Init();
            return new JsonResult(books);
        }

        [HttpGet]
        [ServiceFilter(typeof(DataAccessFilterBase))]
        public IActionResult Get()
        {
            var books = this._interactor.GetBookOverviews();
            return new JsonResult(books);
        }

        [HttpGet]
        public IActionResult Search(string word){
            var books = this._interactor.Search(word);
            this._logger.LogInformation($"検索結果 Search word:{word}");
            return new JsonResult(books);
        }

        [HttpPost]
        [ServiceFilter(typeof(DataAccessFilterBase))]
        public IActionResult Add(string name, string description, string category){
            var books = this._interactor.Add(name, description, category);
            return new JsonResult(books);
        }
    }
}
