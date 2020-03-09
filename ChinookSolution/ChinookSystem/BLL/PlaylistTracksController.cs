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
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        public List<UserPlaylistTrack> List_TracksForPlaylist(
            string playlistname, string username)
        {
            using (var context = new ChinookContext())
            {
               
                //code to go here

                return null;
            }
        }//eom
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid)
        {
            using (var context = new ChinookContext())
            {
                //code to go here
                
             
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


            }
        }//eom
    }
}
