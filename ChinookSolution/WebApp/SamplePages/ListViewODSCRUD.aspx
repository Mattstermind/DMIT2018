<%@ Page Title="ListView ODS CRUD" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListViewODSCRUD.aspx.cs" Inherits="WebApp.SamplePages.ListViewODSCRUD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>ListView ODS CRUD</h1>
    <blockquote class="alert alert-info">
        This page will demostrate the ListView Control doing a complete CRUD using only ODS.
        The page will demonstrate the use of user control MessagerUserControl as it pertains 
        to the ODS.
    </blockquote>

    <asp:ListView ID="AlbumList" runat="server">

    </asp:ListView>

    <asp:ObjectDataSource ID="AlbumListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Album_List" 
        TypeName="ChinookSystem.BLL.AlbumController" 
        DataObjectTypeName="ChinookSystem.Data.Entities.Album" 
        DeleteMethod="Album_Delete" 
        InsertMethod="Album_Add" 
        UpdateMethod="Album_Update">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ArtistODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Artist_List" TypeName="ChinookSystem.BLL.ArtistController">

    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource3" runat="server">

    </asp:ObjectDataSource>

</asp:Content>
