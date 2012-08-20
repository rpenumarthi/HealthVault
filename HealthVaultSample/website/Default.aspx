<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/HelloWorld.master" CodeFile="Default.aspx.cs" Inherits="HelloWorldPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
    Hello, world<br />

    Name: <asp:Label ID="c_UserName" runat="Server"/><br />
    Birth Year: <asp:Label ID="c_BirthYear" runat="Server"/><br />
    
    <asp:Table ID="c_HeightTable"  runat="Server"/>
    
    <asp:Button ID="c_AddHeightEntry" Text="Add Height Entry" runat="server"/>
    
</asp:Content>
