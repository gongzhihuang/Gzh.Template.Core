using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gzh.Template.Core.Repository;
using Gzh.Template.Core.Repository.Domain.MysqlEntity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gzh.Template.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class BlogsController : Controller
    {
        public BaseRepositoryMysqlByLinq baseRepositoryMysqlByLinq;

        public BlogsController(BaseRepositoryMysqlByLinq baseRepositoryMysqlByLinq)
        {
            this.baseRepositoryMysqlByLinq = baseRepositoryMysqlByLinq;
        }

        // GET: api/values
        [HttpGet]
        public ActionResult<List<Blog>> Get()
        {
            var res = baseRepositoryMysqlByLinq.GetContext().Blogs.ToList();
            return res;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Blog blog)
        {
            var _context = baseRepositoryMysqlByLinq.GetContext();
            _context.Add(blog);
            _context.SaveChanges();
        }

    }
}
