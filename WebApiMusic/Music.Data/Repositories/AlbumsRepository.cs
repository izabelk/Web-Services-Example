namespace Music.Data.Repositories
{
    using Music.Models;

    public class AlbumsRepository : GenericRepository<Album>, IGenericRepository<Album>
    {
        public AlbumsRepository(IMusicDbContext context)
            : base(context)
        {
        }
    }
}