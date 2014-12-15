namespace Music.Services.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using Music.Models;

    public class AlbumModel
    {
        public static Expression<Func<Album, AlbumModel>> FromAlbum
        {
            get
            {
                return a => new AlbumModel
                {
                    AlbumId = a.AlbumId,
                    Title = a.Title,
                    Year = a.Year,
                    Producer = a.Producer,
                    ArtistId = a.ArtistId
                };
            }
        }

        public int AlbumId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        public int? Year { get; set; }

        public string Producer { get; set; }

        public int? ArtistId { get; set; }

    }
}