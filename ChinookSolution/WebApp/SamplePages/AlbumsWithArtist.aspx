<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AlbumsWithArtist.aspx.cs" Inherits="WebApp.SamplePages.AlbumsWithArtist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Albums with Artists</h1>
    <blockquote class="alert alert-info">
        This page is proof of Linq query development using LinqPad for query development.
        Testing of the query and POCO class has been completed within LinqPad. Code has been
        transfered to the application AFTER successful testing in LinqPad.
    </blockquote>
    <br />
    <br />
    <asp:GridView ID="GridViewAlbumsWithArtistList"
        runat="server" AutoGenerateColumns="False" 
        DataSourceID="AlbumsWithArtistListODS" 
        AllowPaging="True"
        PageSize="15"
        GridLines="Horizontal"
        BorderStyles="None" CssClass="table table-striped">
        <Columns>
            <asp:BoundField DataField="AlbumTitle" HeaderText="AlbumTitle" SortExpression="AlbumTitle"></asp:BoundField>
            <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year"></asp:BoundField>
            <asp:BoundField DataField="ArtistName" HeaderText="ArtistName" SortExpression="ArtistName"></asp:BoundField>
        </Columns>
    </asp:GridView>


    <asp:ObjectDataSource ID="AlbumsWithArtistListODS" 
        runat="server" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Albums_ListAlbumsWithArtist" 
        TypeName="ChinookSystem.BLL.AlbumController">
    </asp:ObjectDataSource>

</asp:Content>
