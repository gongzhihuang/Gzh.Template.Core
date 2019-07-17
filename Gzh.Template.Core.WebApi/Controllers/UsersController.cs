using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gzh.Template.Core.Application.IService;
using Gzh.Template.Core.Application.Service;
using Gzh.Template.Core.Infrastructure;
using Gzh.Template.Core.Repository.Domain.MongoDBEntity;
using Microsoft.AspNetCore.Mvc;

namespace Gzh.Template.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<ApiResponse<User>> Get()
        {
            var result = new ApiResponse<User>();
            try
            {
                User user = _userService.GetUser("");
                result.Result = user;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {

            _userService.InsertUser(user);
            return Ok(user);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
