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
                // ignore numbers like "1. " as well as any quotation marks
                string title = songInfo[0].Trim().TrimStart('1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '.', ' ', '"').TrimEnd('"');
                string artist = songInfo[1].Trim();
                Songs.Add(new Song(title, artist));
            }
        }
    }
}
