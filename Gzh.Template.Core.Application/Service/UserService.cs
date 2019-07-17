using Gzh.Template.Core.Application.IService;
using Gzh.Template.Core.Repository;
using Gzh.Template.Core.Repository.Domain.MongoDBEntity;
using Gzh.Template.Core.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gzh.Template.Core.Application.Service
{
    public class UserService : IUserService
    {

        public IMongoDBRepository<User> _repository;

        public UserService(BaseRepositoryMongoDB<User> repository)
        {
            _repository = repository;
        }

        public User GetUser(string name)
        {
            //return _repository.FindSingle();
            //User user = new User { Id = "111",UserName="ttt",Birthday=DateTime.Now};
            User user = _repository.FindSingle(x => x.UserName == name, "User");
            return user;
        }

        public User InsertUser(User user)
        {
            //return _repository.FindSingle();
            //User user = new User { Id = "111",UserName="ttt",Birthday=DateTime.Now};
            //User user = _repository.FindSingle();
            _repository.Add(user, "User");
            return user;
        }
    }
}
