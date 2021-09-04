using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCatalogueMicroservice.Model;
using Microsoft.EntityFrameworkCore;

namespace BookCatalogueMicroservice.DBContexts
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
    }
}
