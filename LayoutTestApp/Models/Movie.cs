using System;

namespace LayoutTestApp.Models
{
    public class Movie
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public Uri Uri { get; set; }
        public string Description { get; set; }
        public string Rating { get; set; }
        public Uri PosterLink { get; set; }

    }
}
