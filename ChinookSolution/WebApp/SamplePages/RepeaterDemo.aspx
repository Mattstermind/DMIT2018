<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RepeaterDemo.aspx.cs" Inherits="WebApp.SamplePages.RepeaterDemo" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Repeater for Nested Query</h1>
    <blockquote>This page will demonstrate the usage of the Repeater control. This control is a great web control to display the structure of a DTO/POCO Query
        The control can be nested within itself to be used to display the POCO component of the DTO structure. <br /> <br />
        To ease the working with the properties in your control on this control use the ItemType attribute and assign the class name of your data definition. The control uses a series of templates to fashion your display.
    </blockquote>
    <div class="row text-center">
        <div class="col-md-12 text-center">
            Enter the size of playlist to view:&nbsp;&nbsp;
            <asp:TextBox ID="numberOfTracks" runat="server"></asp:TextBox>&nbsp;&nbsp;
            <asp:LinkButton ID="submitNumber" runat="server">Search</asp:LinkButton>
        </div>
         <div class="col-md-12 text-center">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Track Size is required" ControlToValidate="numberOfTracks" Display="None" SetFocusOnError="true" ForeColor="Firebrick"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator2" runat="server"  ErrorMessage="Track Size must be a positive whole number" ControlToValidate="numberOfTracks" Display="None" SetFocusOnError="true" ForeColor="Firebrick" Type="Integer" ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" />
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        </div>
    </div>
     <div class="row text-center">
        <div class="col-md-12 text-center">
            <asp:Repeater ID="ClientPlayListDTO" runat="server" DataSourceID="CurrentPlaylistODS"
                ItemType="ChinookSystem.Data.DTOs.MyPlayList">
                <HeaderTemplate>
                    <h2>Client PlayList for Requested Size</h2>
                </HeaderTemplate>
                <ItemTemplate>
                    <h3>
                        <%# Item.PlaylistTitle %> Tracks: <%# Item.TrackCount %> Playtime: <%# Item.PlayTime %> 
                    </h3>
                   <%-- <asp:GridView ID="SongList" runat="server" DataSource="<%#Item.TracksList%>"
                        ItemType="ChinookSystem.Data.POCOs.PlayListSong">


                    </asp:GridView>--%>
                    <asp:Repeater ID="SongList" runat="server" DataSource="<%#Item.TracksList%>"
                        ItemType="ChinookSystem.Data.POCOs.PlayListSong">
                        <ItemTemplate>
                            <%# Item.Name %> &nbsp;&nbsp; <%# Item.Genre %>
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <asp:ObjectDataSource 
        ID="CurrentPlaylistODS"
        runat="server"
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Playlist_GetBySize"
        TypeName="ChinookSystem.BLL.PlaylistController" 
        OnSelected="SelectCheckForException">
        <SelectParameters>
            <asp:ControlParameter 
                ControlID="numberOfTracks"
                PropertyName="Text"
                DefaultValue="1" 
                Name="numberoftracks" 
                Type="Int32">
            </asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
