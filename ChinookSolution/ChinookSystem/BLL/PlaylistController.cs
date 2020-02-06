using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Addition Namespaces
using ChinookSystem.Data.Entities;
using ChinookSystem.DAL;
using System.ComponentModel;
using DMIT2018Common.UserControls;
using ChinookSystem.Data.POCOs;
using ChinookSystem.Data.DTOs;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class PlaylistController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<MyPlayList> Playlist_GetBySize(int numberoftracks)
        {
            using (var context = new ChinookContext())
            {
                var test = from x in context.Playlists
                           where x.PlaylistTracks.Count() > 15
                           select new MyPlayList
                           {
                               PlaylistTitle = x.Name,
                               TrackCount = x.PlaylistTracks.Count(),
                               PlayTime = x.PlaylistTracks.Sum(plt => plt.Track.Milliseconds),
                               TracksList = (from y in x.PlaylistTracks
                                             select new PlayListSong
                                             {
                                                 Name = y.Track.Name,
                                                 Genre = y.Track.Genre.Name
                                             }).ToList()
                           };
                return test.ToList();
            }
        }
    }
}
