using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additonal Namespaces
using ChinookSystem.BLL;
using ChinookSystem.Data.POCOs;
using ChinookSysyem.Data.POCOs;

#endregion

namespace WebApp.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null;
        }

        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void ArtistFetch_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(ArtistName.Text))
            {
                MessageUserControl.ShowInfo("Select Error", "You must enter an Artist");
            }
            else
            {
                TracksBy.Text = "ArtistName";
                SearchArg.Text = ArtistName.Text;
                //TracksSelectionList.DataBind(); //Forced ODS to re-execute
            }

        }
        protected void GenreFetch_Click(object sender, EventArgs e)
        {
                TracksBy.Text = "Genre";
                SearchArg.Text = GenreDDL.SelectedValue; ;
                //TracksSelectionList.DataBind(); //Forced ODS to re-execute
        }

        protected void AlbumFetch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AlbumTitle.Text))
            {
                MessageUserControl.ShowInfo("Select Error", "You must enter an Album Title");
            }
            else
            {
                TracksBy.Text = "Album";
                SearchArg.Text = AlbumTitle.Text;
                //TracksSelectionList.DataBind(); //Forced ODS to re-execute
            }
        }
        protected void MediaTypeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MediaTypeDDL.SelectedIndex == 0)
            {
                MessageUserControl.ShowInfo("Select Error", "You must select a Media Type");
            }
            else
            {
                TracksBy.Text = "MediaType";
                SearchArg.Text = MediaTypeDDL.SelectedValue;
                //TracksSelectionList.DataBind(); //Forced ODS to re-execute
            }
        }
        protected void PlayListFetch_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void MoveDown_Click(object sender, EventArgs e)
        {
            //code to go here
            if (PlayList.Rows.Count == 0)
            {
                MessageUserControl.ShowInfo("Moving Tracks", "You must a selected Playlist or have no tracks to move.");
            }
            else
            {
                if (string.IsNullOrEmpty(PlaylistName.Text))
                {
                    MessageUserControl.ShowInfo("Missing Data", "You need a playlist name.");
                }
                else
                {
                    int trackid = 0;
                    int tracknumber = 0;
                    int rowselected = 0;
                    CheckBox playlistselection = null;
                    for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
                    {
                        playlistselection = PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                        if (playlistselection.Checked)
                        {
                            rowselected++;
                            trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text);
                            tracknumber = int.Parse((PlayList.Rows[rowindex].FindControl("TrackNumber") as Label).Text);
                        }
                    }

                    if (rowselected != 1)
                    {
                        MessageUserControl.ShowInfo("Warning", "Select a track to move");
                    }
                    else
                    {
                        if (tracknumber == PlayList.Rows.Count)
                        {
                            MessageUserControl.ShowInfo("Warning", "Track cannot be moved down");
                        }
                        else
                        {
                            MoveTrack(trackid, tracknumber, "down");
                        }
                    }
                }
            }
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            //code to go here
            //code to go here
            if (PlayList.Rows.Count == 0)
            {
                MessageUserControl.ShowInfo("Moving Tracks", "You must a selected Playlist or have no tracks to move.");
            }
            else
            {
                if (string.IsNullOrEmpty(PlaylistName.Text))
                {
                    MessageUserControl.ShowInfo("Missing Data", "You need a playlist name.");
                }
                else
                {
                    int trackid = 0;
                    int tracknumber = 0;
                    int rowselected = 0;
                    CheckBox playlistselection = null;
                    for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
                    {
                        playlistselection = PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                        if (playlistselection.Checked)
                        {
                            rowselected++;
                            trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text);
                            tracknumber = int.Parse((PlayList.Rows[rowindex].FindControl("TrackNumber") as Label).Text);
                        }
                    }

                    if (rowselected != 1)
                    {
                        MessageUserControl.ShowInfo("Warning", "Select a track to move");
                    }
                    else
                    {
                        if (tracknumber == 1)
                        {
                            MessageUserControl.ShowInfo("Warning", "Track cannot be moved up");
                        }
                        else
                        {
                            MoveTrack(trackid, tracknumber, "up");
                        }
                    }
                }
            }

        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            string username = "HansenB";
            //call BLL to move track
            MessageUserControl.TryRun(() =>
            {
                PlaylistTracksController sysymgr = new PlaylistTracksController();
                sysymgr.MoveTrack(username, PlaylistName.Text, trackid, tracknumber, direction);
                List<UserPlaylistTrack> info = sysymgr.List_TracksForPlaylist(PlaylistName.Text, username);
                PlayList.DataSource = info;
                PlayList.DataBind();

            }, "Moving Track", "Track has been moved");
        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void TracksSelectionList_ItemCommand(object sender, 
            ListViewCommandEventArgs e)
        {
            //code to go here
            
        }

    }
}