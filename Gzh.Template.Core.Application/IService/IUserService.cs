using Gzh.Template.Core.Repository.Domain.MongoDBEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gzh.Template.Core.Application.IService
{
    public interface IUserService
    {
        User GetUser(string name);

        User InsertUser(User user);
    }
}
