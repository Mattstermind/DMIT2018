using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region Addition Namespaces
using ChinookSystem.Data.Entities;
using ChinookSystem.DAL;
using System.ComponentModel;
#endregion
namespace ChinookSystem.BLL
{   [DataObject]
    public class TrackController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Track> Get_TracksForPlaylistSelection(int id, string fetchby)
        {
            using (var context = new ChinookContext())
            {
                var results = from x in context.Tracks
                              where x.AlbumId == id 
                              select x;
                return results.ToList();
            }
        }

    }
}
