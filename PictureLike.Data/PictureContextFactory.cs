using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PictureLike.Data
{
    public class PictureContextFactory : IDesignTimeDbContextFactory<PictureDataContext>
    {
        public PictureDataContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}PictureLike.Web"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new PictureDataContext(config.GetConnectionString("ConStr"));
        }
    }
}
