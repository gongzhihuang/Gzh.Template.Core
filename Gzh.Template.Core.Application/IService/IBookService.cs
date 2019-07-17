using Gzh.Template.Core.Repository.Domain.MysqlEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gzh.Template.Core.Application.IService
{
    public interface IBookService
    {
        Book GetBook();

        Book InsertBook(Book book);
    }
}
