<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="DDRPOC.Error" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table style="width:100%; height:100%">
         <tr>
            <td align="center">

            </td>
            </tr>
            <tr>
                <td align="center">
                <h2>An Error has occurred</h2>
                <p/>
                <h2> An unexpected error occured on the website. Please contact to the system administrator. </h2>
                <ul>
                <li>
                     <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Home.aspx">Return to the homepage</asp:HyperLink>
                </li>
                </ul>
              </td>
           </tr>
                <tr>
                <td align="center">
              </td>
           </tr>
</table>
  </div>         
</asp:Content>