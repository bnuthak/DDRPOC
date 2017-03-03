<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="Report_Output.aspx.cs" Inherits="DDRPOC.MM.Report_Output" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
<%--<link rel="stylesheet" type="text/css" href="<%=Request.ApplicationPath%>../Styles/Site.css" />--%>
<%--<link id="Link1"  href="~/Styles/Site.css" runat="server" rel="stylesheet" type="text/css" media="screen"/>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table style="width:100%;" runat="server" id="rpttable">
          <%-- <tr>
                <td  align="left">
                    <asp:Label ID="lblrptname" runat="server" Font-Bold="True" 
                     Font-Size="Large"></asp:Label>
                    </td>
            </tr>--%>
             <%--
            <tr>
                <td>
                    <asp:Label ID="lblauditreportmessage" runat="server" ForeColor="Black" Text="No Results." ></asp:Label></td>
            </tr>--%>
            <tr id="MasterGrid">
                <td align="left">
                <%--<asp:Panel ID="grdpnl" runat="server" height="50%" Width="88%" ScrollBars="auto" CssClass="main" horizontalalign="Left"> --%>
                    <asp:GridView ID="grdmmmultiplereport" runat="server" BackColor="#DEBA84" 
                         BorderColor="#DEBA84" BorderStyle="Solid" BorderWidth="1.5px" CellPadding="3" 
                        CellSpacing="2" onrowdatabound="grdmmmultiplereport_RowDataBound">
                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FFF1D4" />
                        <SortedAscendingHeaderStyle BackColor="#B95C30" />
                        <SortedDescendingCellStyle BackColor="#F1E5CE" />
                        <SortedDescendingHeaderStyle BackColor="#93451F" />
                    </asp:GridView>
                   <%-- </asp:Panel>--%>
                </td>
            </tr>
        </table>
    </div>
  </asp:Content>

