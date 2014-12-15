namespace Music.Data
{
    using System.Data.Entity;

    using Music.Models;
    using Music.Data.Migrations;

    public class MusicDbContext : DbContext, IMusicDbContext
    {
        public MusicDbContext()
            : base("MusicSystem")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MusicDbContext, Configuration>());
        }

        public IDbSet<Song> Songs { get; set; }

        public IDbSet<Album> Albums { get; set; }

        public IDbSet<Artist> Artists { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}