using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Data.Entities;
using ChinookSystem.Data.DTOs;
using ChinookSystem.Data.POCOs;
using ChinookSystem.DAL;
using System.ComponentModel;
using ChinookSysyem.Data.POCOs;
using DMIT2018Common.UserControls;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        private List<string>errors = new List<string>();
        public List<UserPlaylistTrack> List_TracksForPlaylist(
            string playlistname, string username)
        {
            using (var context = new ChinookContext())
            {

                List<UserPlaylistTrack> results = (from x in context.PlaylistTracks
                                                   where x.Playlist.Name.Contains(playlistname) && x.Playlist.UserName.Equals(username)
                                                   orderby x.TrackNumber
                                                   select new UserPlaylistTrack
                                                   {
                                                       TrackID = x.TrackId,
                                                       TrackNumber = x.TrackNumber,
                                                       TrackName = x.Track.Name,
                                                       Milliseconds = x.Track.Milliseconds,
                                                       UnitPrice = x.Track.UnitPrice
                                                   }).ToList();

                return results;
            }
        }//eom
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid)
        {
            using (var context = new ChinookContext())
            {
                int tracknumber = 0;
                PlaylistTrack newTrack = null;
                //code to go here
                Playlist exists = (from x in context.Playlists
                              where x.Name.Equals(playlistname)
                              && x.UserName.Equals(username)
                              select x).FirstOrDefault();
                if (exists == null)
                {
                    //new playlist
                    exists = new Playlist();
                    exists.Name = playlistname;
                    exists.UserName = username;
                    context.Playlists.Add(exists);
                    tracknumber = 1;
                }
                else
                {
                    //existing playlist
                    newTrack = (from x in context.PlaylistTracks
                               where x.Playlist.Name.Equals(playlistname)
                               && x.Playlist.UserName.Equals(username)
                               && x.TrackId == trackid
                               select x).FirstOrDefault();
                    if (newTrack == null)
                    {
                        tracknumber = (from x in context.PlaylistTracks
                                    where x.Playlist.Name.Equals(playlistname)
                                    && x.Playlist.UserName.Equals(username)
                                    select x.TrackNumber).Max();
                        tracknumber++;
                    }
                    else
                    {
                        //Best for single errors
                        //throw new Exception("Song already exists on playlist. Choose something else.");
                        //Multiple Errors
                        errors.Add("Song already exists on playlist. Choose something else.");
                    }
                }
                if (errors.Count > 0)
                {
                    throw new BusinessRuleException("Adding Track", errors);
                }
                newTrack = new PlaylistTrack();
                //when you do an .Add(xxx) to a entity, the record is only staged AND NOT on the database
                //ANY expected pkey value DOES NOT yet exist

                //newTrack.PlaylistId = exists.PlaylistId;
                newTrack.TrackId = trackid;
                newTrack.TrackNumber = tracknumber;
                exists.PlaylistTracks.Add(newTrack);
                context.SaveChanges();
             
            }
        }//eom
        public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
        {
            using (var context = new ChinookContext())
            {
                //code to go here 

            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookContext())
            {
               //code to go here


            }
        }//eom
    }
}
