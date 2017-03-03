<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="QM_qinfo_sap.aspx.cs" Inherits="DDRPOC.QM.QM_qinfo_sap" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
<%--<link rel="stylesheet" type="text/css" href="<%=Request.ApplicationPath%>../Styles/Site.css" />--%>
<%--<link id="Link1"  href="~/Styles/Site.css" runat="server" rel="stylesheet" type="text/css" media="screen"/>--%>
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table style="width:100%;" >
            <tr>
                <td  align="left">
                    <asp:Label ID="lblrpthverification" runat="server" Font-Bold="True" 
                Font-Size="Large"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td>
                
                    <asp:Label ID="lblauditreportmessage" runat="server" ForeColor="Red" Text="There is no any report available on this records." ></asp:Label></td>
            </tr>
            <tr>
           <%-- HeaderStyle BackColor="#003399"--%>
                <td align="left">
                <asp:Panel ID="MainContentPanel" runat="server" height="50%" Width="88%" ScrollBars="auto" CssClass="main"> 
                    <asp:GridView ID="qmveryficationreport" runat="server" BackColor="White" 
                        allowpaging="True" pagesize="20" overflow="auto"
                        BorderColor="#DEBA84" BorderStyle="Solid" BorderWidth="2px" CellPadding="4" 
                        onpageindexchanging="qmveryficationreport_PageIndexChanging">
                      <%--  <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <HeaderStyle BackColor="#3F4A59" Font-Bold="True" ForeColor="#CCCCFF" HorizontalAlign="Left" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                        <RowStyle BackColor="White" ForeColor="#003399" />
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                        <SortedDescendingHeaderStyle BackColor="#002876" />--%>
                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                        <%--<PagerStyle ForeColor="#8C4510" HorizontalAlign="left" />--%>
                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FFF1D4" />
                        <SortedAscendingHeaderStyle BackColor="#B95C30" />
                        <SortedDescendingCellStyle BackColor="#F1E5CE" />
                        <SortedDescendingHeaderStyle BackColor="#93451F" />
                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" Mode="Numeric" Position="Bottom" />  
                        <%--<PagerStyle BackColor="Yellow" ForeColor="Red"#8C4510 />--%>  
                        <PagerStyle ForeColor="White" HorizontalAlign="left" Font-Size="Medium" BackColor="#A55129" />
                    </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
  </asp:Content>
