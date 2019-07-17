using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gzh.Template.Core.Application.Service;
using Gzh.Template.Core.Infrastructure;
using Gzh.Template.Core.Repository.Domain.MysqlEntity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gzh.Template.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<ApiResponse<Book>> Get()
        {
            var result = new ApiResponse<Book>();
            try
            {
                Book book = _bookService.GetBook();
                result.Result = book;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<ApiResponse<Book>> Post([FromBody]Book book)
        {
            var result = new ApiResponse<Book>();
            try
            {
                Book resBook = _bookService.InsertBook(book);
                result.Result = resBook;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
