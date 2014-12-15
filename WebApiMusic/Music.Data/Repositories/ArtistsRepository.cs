namespace Music.Data.Repositories
{
    using Music.Models;

    public class ArtistsRepository : GenericRepository<Artist>, IGenericRepository<Artist>
    {
        public ArtistsRepository(IMusicDbContext context)
            : base(context)
        {
        }
    }
}