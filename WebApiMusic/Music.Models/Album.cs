namespace Music.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Album
    {
        private ICollection<Song> songs;

        public Album()
        {
            this.songs = new HashSet<Song>();
        }

        public int AlbumId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        public int? Year { get; set; }

        public string Producer { get; set; }

        public virtual ICollection<Song> Songs
        {
            get
            {
                return this.songs;
            }
            set
            {
                this.songs = value;
            }
        }

        public int? ArtistId { get; set; }

        public virtual Artist Artist { get; set; }
    }
}