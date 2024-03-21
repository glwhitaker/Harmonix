namespace Harmonix.Models
{
    public class Song
    {
        public string Title { get; set; }
        public string Artist { get; set; }

        public Song()
        {
            Title = "";
            Artist = "";
        }

        public Song(string title, string artist)
        {
            Title = title;
            Artist = artist;
        }
    }
}
