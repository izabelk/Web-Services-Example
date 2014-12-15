namespace Music.Services.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using Music.Models;

    public class SongModel
    {
        public static Expression<Func<Song, SongModel>> FromSong
        {
            get
            {
                return s => new SongModel
                {
                    SongId = s.SongId,
                    Title = s.Title,
                    Year = s.Year,
                    Genre = s.Genre,
                    AlbumId = s.AlbumId
                };
            }
        }

        public int SongId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        public int? Year { get; set; }

        public string Genre { get; set; }

        public int? AlbumId { get; set; }
    }
}