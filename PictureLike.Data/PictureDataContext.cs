using Microsoft.EntityFrameworkCore;
using System;

namespace PictureLike.Data
{
    public class PictureDataContext : DbContext
    {
        private string _connectionString;

        public PictureDataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<Picture> Pictures { get; set; }
    }
}

