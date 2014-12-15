namespace Music.Data.Repositories
{
    using Music.Models;

    public class SongsRepository : GenericRepository<Song>, IGenericRepository<Song>
    {
        public SongsRepository(IMusicDbContext context)
            : base(context)
        {
        }
    }
}