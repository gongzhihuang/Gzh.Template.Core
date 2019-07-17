using Gzh.Template.Core.Application.IService;
using Gzh.Template.Core.Repository;
using Gzh.Template.Core.Repository.Domain.MysqlEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gzh.Template.Core.Application.Service
{
    public class BookService : IBookService
    {
        public BaseRepositoryMysql<Book> _baseRepositoryMysql;

        public BookService(BaseRepositoryMysql<Book> baseRepositoryMysql)
        {
            _baseRepositoryMysql = baseRepositoryMysql;
        }

        public Book GetBook()
        {
            return _baseRepositoryMysql.FindSingle(x=> x.Price == "string");
        }

        public Book InsertBook(Book book)
        {
            _baseRepositoryMysql.Add(book);
            return book;
        }
    }
}
