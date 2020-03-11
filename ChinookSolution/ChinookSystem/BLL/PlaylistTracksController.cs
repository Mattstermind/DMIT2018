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
                var exists = (from x in context.Playlists
                             where x.UserName.Equals(username)
                             && x.Name.Equals(playlistname)
                             select x).FirstOrDefault();
                if (exists == null)
                {
                    throw new Exception("Playlist has been removed from the file");
                }
                else
                {
                    PlaylistTrack moveTrack = (from x in exists.PlaylistTracks
                                               where x.TrackId == trackid
                                               select x).FirstOrDefault();

                    if (moveTrack == null)
                    {
                        throw new Exception("Playlist song has been removed from the file.");
                    }
                    else
                    {
                        PlaylistTrack otherTrack = null;
                        if (direction.Equals("up"))
                        {
                            if (moveTrack.TrackNumber == 1)
                            {
                                throw new Exception("Playlist song already at the top");
                            }
                            else
                            {
                                otherTrack = (from x in exists.PlaylistTracks
                                              where x.TrackNumber == moveTrack.TrackNumber - 1
                                              select x).FirstOrDefault();
                                if (otherTrack == null)
                                {
                                    throw new Exception("Other playlist song is missing");
                                }
                                else
                                {
                                    moveTrack.TrackNumber -= 1;
                                    otherTrack.TrackNumber += 1;
                                }
                            }
                        }
                        else
                        {
                            if (moveTrack.TrackNumber == exists.PlaylistTracks.Count)
                            {
                                throw new Exception("Playlist song is already at the bottom");
                            }
                            else
                            {
                                otherTrack = (from x in exists.PlaylistTracks
                                              where x.TrackNumber == moveTrack.TrackNumber + 1
                                              select x).FirstOrDefault();
                                if (otherTrack == null)
                                {
                                    throw new Exception("Other playlist song is missing");
                                }
                                else
                                {
                                    moveTrack.TrackNumber += 1;
                                    otherTrack.TrackNumber -= 1;
                                }
                            }
                        }
                        //We are only updating the field not the ENTITY
                        context.Entry(moveTrack).Property(y => y.TrackNumber).IsModified = true;
                        context.Entry(otherTrack).Property(y => y.TrackNumber).IsModified = true;
                        //Commit Transaction
                        context.SaveChanges();
                    }

                }

            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookContext())
            {
                //code to go here
                var exists = (from x in context.Playlists
                              where x.UserName.Equals(username)
                              && x.Name.Equals(playlistname)
                              select x).FirstOrDefault();
                if (exists == null)
                {
                    throw new Exception("Playlist has been removed from the system");
                }
                else
                {
                    var trackKept = exists.PlaylistTracks
                                    .Where(tr => !trackstodelete.Any(tod => tod == tr.TrackId))
                                    .OrderBy(tr => tr.TrackNumber)
                                    .Select(tr => tr);
                    PlaylistTrack item = null;
                    foreach (var dtrackid in trackstodelete)
                    {
                        item = (exists.PlaylistTracks
                               .Where(tr => tr.TrackId == dtrackid)
                               .Select(tr => tr)).FirstOrDefault();
                        if (item != null)
                        {
                            exists.PlaylistTracks.Remove(item);
                        }
                    }
                    int number = 1;
                    foreach (var tKept in trackKept)
                    {
                        tKept.TrackNumber = number;
                        context.Entry(tKept).Property(y => y.TrackNumber).IsModified = true;
                        number++;
                    }
                    context.SaveChanges();
                }
            }
        }//eom
    }
}
