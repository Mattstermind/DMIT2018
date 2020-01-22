<%@ Page Title="ListView ODS CRUD" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListViewODSCRUD.aspx.cs" Inherits="WebApp.SamplePages.ListViewODSCRUD" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>ListView ODS CRUD</h1>
    <blockquote class="alert alert-info">
        This page will demostrate the ListView Control doing a complete CRUD using only ODS.
        The page will demonstrate the use of user control MessagerUserControl as it pertains 
        to the ODS.
    </blockquote>
    <br />
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" 
        HeaderText="Correct the following concerns of the insert data."
        ValidationGroup="SquadI"/>
    <asp:ValidationSummary ID="ValidationSummaryEdit" runat="server" 
        HeaderText="Correct the following concerns of the Edit data."
        ValidationGroup="SquadE"/>
    
    <br />
    <%-- For the delete to be functional on this ODS CRUD, you MUST include
        a parameter called DataKeyNames
        This parameter will be set to the pkeyfield Name on the Entity--%>
    <asp:ListView ID="AlbumList" runat="server" DataSourceID="AlbumListODS" 
        InsertItemPosition="LastItem" DataKeyNames="AlbumId">

        <AlternatingItemTemplate>
            <tr style="background-color: #FFFFFF; color: #284775;">
                <td>
                    <asp:Button runat="server" CommandName="Delete" Text="Remove" ID="DeleteButton" 
                        OnClientClick="return confirm(' Are you sure you want to remove this album?')"/>
                    <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                </td>
                <td>
                    <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" ID="AlbumIdLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                <td>
                    <asp:DropDownList ID="ArtistList" runat="server" 
                        DataSourceID="ArtistODS"
                        DataTextField="Name" 
                        DataValueField="ArtistId"
                        selectedvalue='<%# Eval("ArtistId") %>'
                        Width="300px"
                        AppendDataBoundItems="true">
                        <asp:ListItem Value="">none</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
                <td>     
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <asp:RequiredFieldValidator ID="RequiredTitleTextBoxE" runat="server" ErrorMessage="Title is required"
                Display="None" ControlToValidate="TitleTextBoxE" ValidationGroup="SquadE">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegExTitleBoxE" runat="server" ErrorMessage="Title is limited to 160 characters"
                ControlToValidate="TitleTextBoxE" ValidationGroup="SquadE" ValidationExpression="^.{1,160}$"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegExReleaseLabelTextBoxE" runat="server" ErrorMessage="Label is limited to 50 characters" ControlToValidate="ReleaseLabelTextBoxE" ValidationGroup="SquadE" ValidationExpression="^.{1,50}$"></asp:RegularExpressionValidator>

            <tr style="background-color: #999999;">
                <td>
                    <asp:Button runat="server" CommandName="Update" Text="Update" ID="UpdateButton" ValidationGroup="SquadE"/>
                    <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                </td>
                <td>
                    <asp:TextBox Text='<%# Bind("AlbumId") %>' runat="server" ID="AlbumIdTextBox" Enabled="false" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Title") %>' runat="server" ID="TitleTextBoxE" /></td>
                <td>
                    <asp:DropDownList ID="ArtistList" runat="server" 
                        DataSourceID="ArtistODS" DataTextField="Name" 
                        DataValueField="ArtistId"
                        SelectedValue='<%# Bind("ArtistId") %>'
                        Width="300px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox Text='<%# Bind("ReleaseYear") %>' runat="server" ID="ReleaseYearTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("ReleaseLabel") %>' runat="server" ID="ReleaseLabelTextBoxE" /></td>
                <td>   
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <asp:RequiredFieldValidator ID="RequiredTitleTextBoxI" runat="server" ErrorMessage="Title is required"
                Display="None" ControlToValidate="TitleTextBoxI" ValidationGroup="SquadI">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegExTitleBoxI" runat="server" ErrorMessage="Title is limited to 160 characters"
                ControlToValidate="TitleTextBoxI" ValidationGroup="SquadI" ValidationExpression="^.{1,160}$"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegExReleaseLabelTextBoxI" runat="server" ErrorMessage="Label is limited to 50 characters" ControlToValidate="ReleaseLabelTextBoxI" ValidationGroup="SquadI" ValidationExpression="^.{1,50}$"></asp:RegularExpressionValidator>
            <tr style="">
                <td>
                    <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" ValidationGroup="SquadI"/>
                    <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                </td>
                <td>
                    <asp:TextBox Text='<%# Bind("AlbumId") %>' runat="server" ID="AlbumIdTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Title") %>' runat="server" ID="TitleTextBoxI" /></td>
                <td>
                    <asp:DropDownList ID="ArtistList" runat="server" 
                        DataSourceID="ArtistODS" DataTextField="Name" 
                        DataValueField="ArtistId"
                        SelectedValue='<%# Bind("ArtistId") %>'
                        Width="300px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox Text='<%# Bind("ReleaseYear") %>' runat="server" ID="ReleaseYearTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("ReleaseLabel") %>' runat="server" ID="ReleaseLabelTextBoxI" /></td>
                <td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="background-color: #E0FFFF; color: #333333;">
                <td>
                    <asp:Button runat="server" CommandName="Delete" Text="Remove" ID="DeleteButton" 
                        OnClientClick="return confirm(' Are you sure you want to remove this album?')"/>
                    <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                </td>
                <td>
                    <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" ID="AlbumIdLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                <td>
                    <asp:DropDownList ID="ArtistList" runat="server" 
                        DataSourceID="ArtistODS" DataTextField="Name" 
                        DataValueField="ArtistId"
                        SelectedValue='<%# Eval("ArtistId") %>'
                        Width="300px"
                        AppendDataBoundItems="true">
                        <asp:ListItem Value="">none</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
                <td>                
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                            <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                <th runat="server"></th>
                                <th runat="server">AlbumId</th>
                                <th runat="server">Title</th>
                                <th runat="server">ArtistId</th>
                                <th runat="server">ReleaseYear</th>
                                <th runat="server">ReleaseLabel</th>
                            </tr>
                            <tr runat="server" id="itemPlaceholder"></tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF">
                        <asp:DataPager runat="server" ID="DataPager1">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                <asp:NumericPagerField></asp:NumericPagerField>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="background-color: #E2DED6; font-weight: bold; color: #333333;">
                <td>
                    <asp:Button runat="server" CommandName="Delete" Text="Remove" ID="DeleteButton" 
                        OnClientClick="return confirm(' Are you sure you want to remove this album?')"/>
                    <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                </td>
                <td>
                    <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" ID="AlbumIdLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                <td>
                    <asp:DropDownList ID="ArtistList" runat="server" 
                        DataSourceID="ArtistODS" DataTextField="Name" 
                        DataValueField="ArtistId"
                        SelectedValue='<%# Eval("ArtistId") %>'
                        Width="300px"
                        AppendDataBoundItems="true">
                        <asp:ListItem Value="">none</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
                <td>                
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>

    <asp:ObjectDataSource ID="AlbumListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Album_List" 
        TypeName="ChinookSystem.BLL.AlbumController" 
        DataObjectTypeName="ChinookSystem.Data.Entities.Album" 
        DeleteMethod="Album_Delete" 
        InsertMethod="Album_Add" 
        UpdateMethod="Album_Update"
        OnDeleted="DeleteCheckForException"
        OnUpdated="UpdateCheckForException"
        OnInserted="InsertCheckForException"
        OnSelected="SelectCheckForException">
        
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ArtistODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Artist_List" TypeName="ChinookSystem.BLL.ArtistController" OnSelected="SelectCheckForException">

    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource3" runat="server">

    </asp:ObjectDataSource>

</asp:Content>
