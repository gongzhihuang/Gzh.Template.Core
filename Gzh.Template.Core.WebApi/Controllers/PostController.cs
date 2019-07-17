using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Gzh.Template.Core.Repository;
using Gzh.Template.Core.Repository.Domain.MysqlEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gzh.Template.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        public BaseRepositoryMysqlByLinq baseRepositoryMysqlByLinq;

        public PostController(BaseRepositoryMysqlByLinq baseRepositoryMysqlByLinq)
        {
            this.baseRepositoryMysqlByLinq = baseRepositoryMysqlByLinq;
        }



        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<List<Post>> Get(int id)
        {
            List<Post> posts = new List<Post>();
            //var res = baseRepositoryMysqlByLinq.GetContext().Posts.Where(x => x.BlogId == id).ToList();
            //posts.AddRange(res);
            //return posts;

            var _context = baseRepositoryMysqlByLinq.GetContext();
            var res = from a in _context.Blogs
                      join b in _context.Posts on a.Id equals b.BlogId
                      where a.Id == 2
                      select b;
            posts.AddRange(res.ToList());

            return posts;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Post post)
        {
            var _context = baseRepositoryMysqlByLinq.GetContext();
            _context.Add(post);
            _context.SaveChanges();
        }
    }
}
