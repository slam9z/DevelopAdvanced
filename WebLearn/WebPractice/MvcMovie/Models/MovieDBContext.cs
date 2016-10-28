using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class MovieDBContext : DbContext
    {

        public MovieDBContext()
            : base("name=MovieConnection")
        {
        }

        public DbSet<Movie> Movies { get; set; }
    }
}