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
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing Data", "Please Enter the playlist name");
            }
            else
            {
                string username = "HansenB"; //Username will come from security once implemented

                //MessageUserControl will be used to handle the code behind user friendly error handling
                //you will NOT be using try/catch
                //your try/catch is embedded within the MessageUserControl class
                MessageUserControl.TryRun(() =>
                {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    List<UserPlaylistTrack> playlistInfo = null;
                    playlistInfo = sysmgr.List_TracksForPlaylist(PlaylistName.Text, username);
                    if (playlistInfo == null)
                    {
                        throw new Exception("Playlist no longer exists");
                    }
                    else
                    {
                        PlayList.DataSource = playlistInfo;
                        PlayList.DataBind();
                    }
                }, "Playlist", "Manage your playlist");//string after successful coding block
            }
 
        }

        protected void MoveDown_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            //call BLL to move track
 
        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void TracksSelectionList_ItemCommand(object sender, 
            ListViewCommandEventArgs e)
        {
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing Data", "Enter a playlist name");
            }
            else
            {
                string username = "HansenB";
                MessageUserControl.TryRun(() =>
                {
                    int trackid = int.Parse(e.CommandArgument.ToString());
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    sysmgr.Add_TrackToPLaylist(PlaylistName.Text, username, trackid);
                    List<UserPlaylistTrack> playlistInfo = null;
                    PlayList.DataSource = playlistInfo;
                    PlayList.DataBind();
                }, "Add Track","Track has been added to your playlist");
            }
            
        }

    }
}