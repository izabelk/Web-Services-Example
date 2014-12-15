namespace Music.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using Music.Data;
    using Music.Models;
    using Music.Services.Models;
    using System.Net.Http.Headers;
    using System.Net.Mime;
    using System.Net.Http;

    public class SongsController : ApiController
    {
        private IMusicData data;

        public SongsController()
            : this(new MusicData())
        {
        }

        public SongsController(IMusicData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var songs = this.data
                .Songs
                .All()
                .Select(SongModel.FromSong);

           

            return Ok(songs);
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var song = this.data
                .Songs
                .All()
                .Where(s => s.SongId == id)
                .Select(SongModel.FromSong)
                .FirstOrDefault();

            if (song == null)
            {
                return BadRequest(string.Format("Song with id {0} does not exist.", id));
            }

            return Ok(song);
        }

        [HttpPost]
        public IHttpActionResult Create(SongModel song)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var songToBeAdded = new Song
            {
                Title = song.Title,
                Year = song.Year,
                Genre = song.Genre,
                AlbumId = song.AlbumId
            };

            this.data.Songs.Add(songToBeAdded);
            this.data.SaveChanges();

            song.SongId = songToBeAdded.SongId;
            return Ok(song);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, SongModel song)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var songToBeUpdated = this.data.Songs.All().FirstOrDefault(s => s.SongId == id);

            if (songToBeUpdated == null)
            {
                return BadRequest(string.Format("Song with id {0} does not exist.", id));
            }

            songToBeUpdated.Title = song.Title;
            songToBeUpdated.Year = song.Year;
            songToBeUpdated.Genre = song.Genre;
            this.data.SaveChanges();

            song.SongId = songToBeUpdated.SongId;
            return Ok(song);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var songToBeDeleted = this.data.Songs.All().FirstOrDefault(s => s.SongId == id);

            if (songToBeDeleted == null)
            {
                return BadRequest(string.Format("Song with id {0} does not exist.", id));
            }

            this.data.Songs.Delete(songToBeDeleted);
            this.data.SaveChanges();

            return Ok();
        }
    }
}