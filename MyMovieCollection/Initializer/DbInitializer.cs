using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyMovieCollection.Data;

namespace MyMovieCollection.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppDbContext _db;        

        public DbInitializer(AppDbContext db)
        {
            _db = db;

        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {


            }
        }
    }
}
