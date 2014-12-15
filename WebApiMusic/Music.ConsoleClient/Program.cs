namespace Music.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;

    using Newtonsoft.Json;

    using Music.Models;
    using Music.Services.Models;


    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:2125/")
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            GetAllSongs(client);
            GetSongById(client, 1);
            CreateSong(client, "Baby boy", 1998, "Pop");
            UpdateSong(client, 3, "Baby boy 2", 1999, "Modern pop");
            DeleteSong(client, 3);

            GetAllAlbums(client);
            GetAlbumById(client, 2);
            CreateAlbum(client, "Special album", 2006, "Tom Jonson");
            UpdateAlbum(client, 2, "My album", 2003, "Peter Ivanov");
            DeleteAlbum(client, 4);

            GetAllArtists(client);
            GetArtistById(client, 1);
            CreateArtist(client, "Justin Beeber", "USA", DateTime.Now);
            UpdateArtist(client, 3, "Justin Beeber 2", "USA", DateTime.Now);
            DeleteArtist(client, 3);
        }

        private static void GetAllSongs(HttpClient client)
        {
            HttpResponseMessage response = client.GetAsync("api/Songs/All").Result;

            Console.WriteLine("Songs:");

            if (response.IsSuccessStatusCode)
            {
                var songs = response.Content.ReadAsAsync<IEnumerable<SongModel>>().Result;

                foreach (var song in songs)
                {
                    Console.WriteLine("Id: {0}, Title: {1}, Year: {2}, Genre: {3}", song.SongId, song.Title, song.Year, song.Genre);
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine();
        }

        private static void GetSongById(HttpClient client, int id)
        {
            HttpResponseMessage response = client.GetAsync("api/Songs/ById/" + id).Result;

            Console.WriteLine("Song with id {0}:", id);

            if (response.IsSuccessStatusCode)
            {
                var song = response.Content.ReadAsAsync<SongModel>().Result;

                Console.WriteLine("Id: {0}, Title: {1}, Year: {2}, Genre: {3}", song.SongId, song.Title, song.Year, song.Genre);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine();
        }

        private static void CreateSong(HttpClient client, string title, int year, string genre)
        {
            Console.WriteLine("Adding song...");

            var song = new SongModel
            {
                Title = title,
                Genre = genre,
                Year = year,
            };

            HttpResponseMessage response = client.PostAsJsonAsync("api/Songs/Create", song).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Song added!");
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            Console.WriteLine();
        }

        private static void UpdateSong(HttpClient client, int id, string newTitle, int newYear, string newGenre)
        {
            Console.WriteLine("Updating song...");

            var song = new SongModel
            {
                Title = newTitle,
                Year = newYear,
                Genre = newGenre
            };

            HttpResponseMessage response = client.PutAsJsonAsync("api/Songs/Update/" + id, song).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Song updated!");
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            Console.WriteLine();
        }

        private static void DeleteSong(HttpClient client, int id)
        {
            Console.WriteLine("Deleting song...");

            HttpResponseMessage response = client.DeleteAsync("api/Songs/Delete/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Song deleted!");
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            Console.WriteLine();
        }

        private static void GetAllAlbums(HttpClient client)
        {
            HttpResponseMessage response = client.GetAsync("api/Albums/All").Result;

            Console.WriteLine("Albums:");

            if (response.IsSuccessStatusCode)
            {
                var albums = response.Content.ReadAsAsync<IEnumerable<AlbumModel>>().Result;

                foreach (var album in albums)
                {
                    Console.WriteLine("Id: {0}, Title: {1}, Year: {2}, Producer: {3}", album.AlbumId, album.Title, album.Year, album.Producer);
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine();
        }

        private static void GetAlbumById(HttpClient client, int id)
        {
            HttpResponseMessage response = client.GetAsync("api/Albums/ById/" + id).Result;

            Console.WriteLine("Album with id {0}:", id);

            if (response.IsSuccessStatusCode)
            {
                var album = response.Content.ReadAsAsync<AlbumModel>().Result;

                Console.WriteLine("Id: {0}, Title: {1}, Year: {2}, Genre: {3}", album.AlbumId, album.Title, album.Year, album.Producer);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            Console.WriteLine("-------------------------------------------------------");
        }

        private static void CreateAlbum(HttpClient client, string title, int year, string producer)
        {
            Console.WriteLine("Adding album...");

            var song = new AlbumModel
            {
                Title = title,
                Year = year,
                Producer = producer
            };

            HttpResponseMessage response = client.PostAsJsonAsync("api/Albums/Create", song).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Album added!");
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            Console.WriteLine();
        }

        private static void UpdateAlbum(HttpClient client, int id, string newTitle, int newYear, string newProducer)
        {
            Console.WriteLine("Updating album...");

            var album = new AlbumModel
            {
                Title = newTitle,
                Year = newYear,
                Producer = newProducer
            };

            HttpResponseMessage response = client.PutAsJsonAsync("api/Albums/Update/" + id, album).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Album updated!");
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            Console.WriteLine();
        }

        private static void DeleteAlbum(HttpClient client, int id)
        {
            Console.WriteLine("Deleting album...");

            HttpResponseMessage response = client.DeleteAsync("api/Albums/Delete/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Album deleted!");
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            Console.WriteLine();
        }

        private static void GetAllArtists(HttpClient client)
        {
            HttpResponseMessage response = client.GetAsync("api/Artists/All").Result;

            Console.WriteLine("Artists:");

            if (response.IsSuccessStatusCode)
            {
                var artists = response.Content.ReadAsAsync<IEnumerable<ArtistModel>>().Result;

                foreach (var artist in artists)
                {
                    Console.WriteLine("Id: {0}, Name: {1}, Country: {2}, Date of birth: {3}", artist.ArtistId, artist.Name, artist.Country, artist.DateOfBirth);
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine();
        }

        private static void GetArtistById(HttpClient client, int id)
        {
            HttpResponseMessage response = client.GetAsync("api/Artists/ById/" + id).Result;

            Console.WriteLine("Artist with id {0}:", id);

            if (response.IsSuccessStatusCode)
            {
                var artist = response.Content.ReadAsAsync<ArtistModel>().Result;

                Console.WriteLine("Id: {0}, Name: {1}, Country: {2}, Date of birth: {3}", artist.ArtistId, artist.Name, artist.Country, artist.DateOfBirth);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            Console.WriteLine("-------------------------------------------------------");
        }

        private static void CreateArtist(HttpClient client, string name, string country, DateTime dateOfBirth)
        {
            Console.WriteLine("Adding artist...");

            var artist = new ArtistModel
            {
                Name = name,
                Country = country,
                DateOfBirth = dateOfBirth
            };

            HttpResponseMessage response = client.PostAsJsonAsync("api/Artists/Create", artist).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Artist added!");
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            Console.WriteLine();
        }

        private static void UpdateArtist(HttpClient client, int id, string newName, string newCountry, DateTime newDateOfBirth)
        {
            Console.WriteLine("Updating artist...");

            var artist = new ArtistModel
            {
                Name = newName,
                Country = newCountry,
                DateOfBirth = newDateOfBirth
            };

            HttpResponseMessage response = client.PutAsJsonAsync("api/Artists/Update/" + id, artist).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Artist updated!");
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            Console.WriteLine();
        }

        private static void DeleteArtist(HttpClient client, int id)
        {
            Console.WriteLine("Deleting artist...");

            HttpResponseMessage response = client.DeleteAsync("api/Artists/Delete/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Artist deleted!");
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            Console.WriteLine();
        }
    }
}