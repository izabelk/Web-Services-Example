namespace Music.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Song
    {
        public int SongId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        public int? Year { get; set; }

        public string Genre { get; set; }

        public int? AlbumId { get; set; }

        public virtual Album Album { get; set; }
    }
}