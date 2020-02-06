using ChinookSystem.Data.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.Data.DTOs
{
    public class MyPlayList
    {
        public string PlaylistTitle { get; set; }
        public int TrackCount { get; set; }
        public int PlayTime { get; set; }
        public IEnumerable<PlayListSong> TracksList { get; set; }
    }
}
