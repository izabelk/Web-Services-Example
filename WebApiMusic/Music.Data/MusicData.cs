namespace Music.Data
{
    using System;
    using System.Collections.Generic;

    using Music.Data.Repositories;
    using Music.Models;

    public class MusicData : IMusicData
    {
        private IMusicDbContext context;
        private IDictionary<Type, object> repositories;

        public MusicData()
            : this(new MusicDbContext())
        {
        }

        public MusicData(IMusicDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public SongsRepository Songs
        {
            get
            {
                return (SongsRepository)this.GetRepository<Song>();
            }
        }

        public AlbumsRepository Albums
        {
            get
            {
                return (AlbumsRepository)this.GetRepository<Album>();
            }
        }

        public ArtistsRepository Artists
        {
            get
            {
                return (ArtistsRepository)this.GetRepository<Artist>();
            }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);

            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(GenericRepository<T>);

                if (typeOfModel.IsAssignableFrom(typeof(Song)))
                {
                    type = typeof(SongsRepository);
                }
                else if (typeOfModel.IsAssignableFrom(typeof(Album)))
                {
                    type = typeof(AlbumsRepository);
                }
                else if (typeOfModel.IsAssignableFrom(typeof(Artist)))
                {
                    type = typeof(ArtistsRepository);
                }

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeOfModel];
        }
    }
}