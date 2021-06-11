using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameThatMusic.Model
{
    class Music
    {
        public string Name { get; private set; }
        public string Artist { get; private set; }
        public string Path { get; private set; }
        public string MusicStyle { get; private set; }
        public bool wasPlayed { get; set; } = false;
        public Music(string _name, string _artist, string _path, string _musicStyle)
        {
            Name = _name;
            Artist = _artist;
            Path = _path;
            MusicStyle = _musicStyle;
        }
    }
}
