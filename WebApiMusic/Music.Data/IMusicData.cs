namespace Music.Data
{
    using Music.Data.Repositories;

    public interface IMusicData
    {
        SongsRepository Songs { get; }

        AlbumsRepository Albums { get; }

        ArtistsRepository Artists { get; }

        void SaveChanges();
    }
}