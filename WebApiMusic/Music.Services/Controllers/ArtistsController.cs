namespace Music.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using Music.Data;
    using Music.Models;
    using Music.Services.Models;

    public class ArtistsController : ApiController
    {
        private IMusicData data;

        public ArtistsController()
            : this(new MusicData())
        {
        }

        public ArtistsController(IMusicData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var artists = this.data
                .Artists
                .All()
                .Select(ArtistModel.FromArtist);

            return Ok(artists);
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var artist = this.data
                .Artists
                .All()
                .Where(a => a.ArtistId == id)
                .Select(ArtistModel.FromArtist)
                .FirstOrDefault();

            if (artist == null)
            {
                return BadRequest(string.Format("Artist with id {0} does not exist.", id));
            }

            return Ok(artist);
        }

        [HttpPost]
        public IHttpActionResult Create(ArtistModel artist)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artistToBeAdded = new Artist
            {
               Name = artist.Name,
               Country = artist.Country,
               DateOfBirth = artist.DateOfBirth
            };

            this.data.Artists.Add(artistToBeAdded);
            this.data.SaveChanges();

            artist.ArtistId = artistToBeAdded.ArtistId;
            return Ok(artist);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, ArtistModel artist)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artistToBeUpdated = this.data.Artists.All().FirstOrDefault(a => a.ArtistId == id);

            if (artistToBeUpdated == null)
            {
                return BadRequest(string.Format("Artist with id {0} does not exist.", id));
            }

            artistToBeUpdated.Name = artist.Name;
            artistToBeUpdated.Country = artist.Country;
            artistToBeUpdated.DateOfBirth = artist.DateOfBirth;
            this.data.SaveChanges();

            artist.ArtistId = artistToBeUpdated.ArtistId;
            return Ok(artist);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var artistToBeDeleted = this.data.Artists.All().FirstOrDefault(a => a.ArtistId == id);

            if (artistToBeDeleted == null)
            {
                return BadRequest(string.Format("Artist with id {0} does not exist.", id));
            }

            this.data.Artists.Delete(artistToBeDeleted);
            this.data.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult AddAlbum(int artistId, int albumId)
        {
            var artist = this.data.Artists.All().FirstOrDefault(a => a.ArtistId == artistId);

            if (artist == null)
            {
                return BadRequest(string.Format("Artist with id {0} does not exist", artistId));
            }

            var album = this.data.Albums.All().FirstOrDefault(a => a.AlbumId == albumId);

            if (album == null)
            {
                return BadRequest(string.Format("Album with id {0} does not exist", albumId));
            }

            artist.Albums.Add(album);
            this.data.SaveChanges();

            return Ok();
        }
    }
}