using Harmonix.DTOs;

namespace Harmonix.Models
{
    public class Playlist
    {
        public string Name { get; set; }
        public List<Song> Songs { get; set; }

        public Playlist()
        {
            Name = "";
            Songs = new List<Song>();
        }

        public Playlist(string name, OpenAIResponse response)
        {
            Name = name;

            // Parse the OpenAI response to extract the playlist
            Songs = new List<Song>();
            string[] songList = response.Choices[0].Message.Content.Split("\n");
            foreach (string song in songList)
            {
                string[] songInfo = song.Split("-");
                Songs.Add(new Song(songInfo[0], songInfo[1]));
            }
        }
    }
}
