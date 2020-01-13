<%@ Page Title="ODS Review" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ODSReview.aspx.cs" Inherits="WebApp.SamplePages.ODSReview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Query: Albums by Artist</h1>

    <asp:Label ID="Label1" runat="server" Text="Select an artist"></asp:Label>&nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="ArtistList" runat="server" 
        DataSourceID="ArtistListODS" 
        DataTextField="Name" 
        DataValueField="ArtistId">

    </asp:DropDownList>

    <asp:Button ID="Fetch" runat="server" Text="Fetch" />
    <br /><br />



    <asp:GridView ID="AlbumList" runat="server" DataSourceID="AlbumListODS" AllowPaging="True" PageSize="15" 
        GridLines="Horizontal" BorderStyle="Dotted" CssClass="table table-striped" AutoGenerateColumns="False"  Caption="Albums for Artist" >
        <Columns>         
            <asp:TemplateField HeaderText="ID">
                <ItemTemplate>
                    <asp:Label ID="AlbumId" runat="server" 
                        Text='<%# Eval("AlbumId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Title">
                <ItemTemplate>
                    <asp:Label ID="AlbumTitle" runat="server" 
                        Text='<%# Eval("Title") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Artist">
                <ItemTemplate>
                    <asp:DropDownList ID="ArtistListView" runat="server" DataSourceID="ArtistListODS" 
                        DataTextField="Name" DataValueField="ArtistId" Enable="false" AppendDataBoundItems="false"
                        SelectedValue='<%# Eval("ArtistId") %>'>
                        <asp:ListItem Value="">None</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Year">
                <ItemTemplate>
                    <asp:Label ID="AlbumYear" runat="server" 
                        Text='<%# Eval("ReleaseYear") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Label">
                <ItemTemplate>
                    <asp:Label ID="AlbumLabel" runat="server" 
                        Text='<%# Eval("ReleaseLabel") == null ? "--------" : Eval("ReleaseLabel") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>

    <asp:ObjectDataSource ID="AlbumListODS" runat="server"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="Album_FindByArtist"
        TypeName="ChinookSystem.BLL.AlbumController">
        <SelectParameters>
            <asp:ControlParameter ControlID="ArtistList" PropertyName="SelectedValue" DefaultValue="0" Name="artistid" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="ArtistListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="Artist_List" 
        TypeName="ChinookSystem.BLL.ArtistController"></asp:ObjectDataSource>

</asp:Content>
