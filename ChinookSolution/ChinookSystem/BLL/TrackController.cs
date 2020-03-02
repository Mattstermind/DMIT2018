using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region Addition Namespaces
using ChinookSystem.Data.Entities;
using ChinookSystem.DAL;
using ChinookSystem.Data.DTOs;
using ChinookSystem.Data.POCOs;
using System.ComponentModel;
#endregion
namespace ChinookSystem.BLL
{   [DataObject]
    public class TrackController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<TrackList> Track_GetTrackByTrackAndId(string trcksby, string searchargs)
        {
            using (var context = new ChinookContext())
            {
                List<TrackList> results = null;
                int searchint = 0;
                if (int.TryParse(searchargs, out searchint))
                {
                    //the first two are searches for media type and genre
                    results = (from x in context.Tracks
                              where (x.GenreId == searchint && trcksby.Equals("Genre"))
                               || (x.MediaTypeId == searchint && trcksby.Equals("MediaType"))
                              select new TrackList
                              {
                                  TrackID = x.TrackId,
                                  Name = x.Name,
                                  Title = x.Album.Title,
                                  ArtistName = x.Album.Artist.Name,
                                  MediaName = x.MediaType.Name,
                                  GenreName = x.Genre.Name,
                                  Composer = x.Composer,
                                  Milliseconds = x.Milliseconds,
                                  Bytes = x.Bytes,
                                  UnitPrice = x.UnitPrice
                              }).ToList();
                }
                else
                {
                    results = (from x in context.Tracks
                               where (x.Album.Title.Contains(searchargs) && trcksby.Equals("Album"))
                                || (x.Album.Artist.Name.Contains(searchargs) && trcksby.Equals("Artist"))
                               select new TrackList
                               {
                                   TrackID = x.TrackId,
                                   Name = x.Name,
                                   Title = x.Album.Title,
                                   ArtistName = x.Album.Artist.Name,
                                   MediaName = x.MediaType.Name,
                                   GenreName = x.Genre.Name,
                                   Composer = x.Composer,
                                   Milliseconds = x.Milliseconds,
                                   Bytes = x.Bytes,
                                   UnitPrice = x.UnitPrice
                               }).ToList();
                }
                return results;
            }
        }



        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Track> Track_List()
        {
            using (var context = new ChinookContext())
            {
                return context.Tracks.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Track Track_Find(int trackid)
        {
            using (var context = new ChinookContext())
            {
                return context.Tracks.Find(trackid);
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Track> Track_GetByAlbumId(int albumid)
        {
            using (var context = new ChinookContext())
            {
                var results = from aRowOn in context.Tracks
                              where aRowOn.AlbumId.HasValue
                              && aRowOn.AlbumId == albumid
                              select aRowOn;
                return results.ToList();
            }
        }

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
