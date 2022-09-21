using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PictureLike.Data
{
    public class PictureRepository
    {
        private string _connectionString;

        public PictureRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Add(Picture picture)
        {
            using var context = new PictureDataContext(_connectionString);
            context.Pictures.Add(picture);
            context.SaveChanges();
        }
        public List<Picture> GetAll()
        {
            using var context = new PictureDataContext(_connectionString);
            return context.Pictures.ToList();
        }
        public Picture GetById(int id)
        {
            using var context = new PictureDataContext(_connectionString);
            return context.Pictures.FirstOrDefault(p => p.Id == id);
        }
        public int GetLikes(int id)
        {
            using var context = new PictureDataContext(_connectionString);
            return context.Database.ExecuteSqlInterpolated($"Select Sum(likes) FROM Pictures WHERE Id = {id}");
        }
        public void LikeIt(int id)
        {
            using var context = new PictureDataContext(_connectionString);
           context.Database.ExecuteSqlInterpolated($"Update Pictures set likes=likes+1 WHERE Id = {id}");
            context.SaveChanges();

        }
    }
}
