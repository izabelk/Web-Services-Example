namespace Music.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using Music.Data;
    using Music.Models;
    using Music.Services.Models;

    [CustomHeaderFilter]
    public class AlbumsController : ApiController
    {
         private IMusicData data;

        public AlbumsController()
            : this(new MusicData())
        {
        }

        public AlbumsController(IMusicData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var albums = this.data
                .Albums
                .All()
                .Select(AlbumModel.FromAlbum);

            return Ok(albums);
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var album = this.data
                .Albums
                .All()
                .Where(a => a.AlbumId == id)
                .Select(AlbumModel.FromAlbum)
                .FirstOrDefault();

            if (album == null)
            {
                return BadRequest(string.Format("Album with id {0} does not exist.", id));
            }

            return Ok(album);
        }

        [HttpPost]
        public IHttpActionResult Create(AlbumModel album)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var albumToBeAdded = new Album
            {
                Title = album.Title,
                Year = album.Year,
                Producer = album.Producer,
                ArtistId = album.ArtistId
            };

            this.data.Albums.Add(albumToBeAdded);
            this.data.SaveChanges();

            album.AlbumId = albumToBeAdded.AlbumId;
            return Ok(album);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, AlbumModel album)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var albumToBeUpdated = this.data.Albums.All().FirstOrDefault(a => a.AlbumId == id);

            if (albumToBeUpdated == null)
            {
                return BadRequest(string.Format("Album with id {0} does not exist.", id));
            }

            albumToBeUpdated.Title = album.Title;
            albumToBeUpdated.Year = album.Year;
            albumToBeUpdated.Producer = album.Producer;
            this.data.SaveChanges();

            album.AlbumId = albumToBeUpdated.AlbumId;
            return Ok(album);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var albumToBeDeleted = this.data.Albums.All().FirstOrDefault(a => a.AlbumId == id);

            if (albumToBeDeleted == null)
            {
                return BadRequest(string.Format("Album with id {0} does not exist.", id));
            }

            this.data.Albums.Delete(albumToBeDeleted);
            this.data.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult AddSong(int albumId, int songId)
        {
            var album = this.data.Albums.All().FirstOrDefault(a => a.AlbumId == albumId);

            if (album == null)
            {
                return BadRequest(string.Format("Album with id {0} does not exist", albumId));
            }

            var song = this.data.Songs.All().FirstOrDefault(s => s.SongId == songId);

            if (song == null)
            {
                return BadRequest(string.Format("Song with id {0} does not exist", songId));
            }

            album.Songs.Add(song);
            this.data.SaveChanges();

            return Ok();
        }
    }
}