<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="About.aspx.cs" Inherits="DDRPOC.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <table style="width:100%;" >
            <tr>
            <td></td>
                <td  align="left">
                   
                    </td>
                    <td></td>
            </tr>
            <tr>
            <td ></td>
                <td align="left">
                
                  <h3>  Please select the Site/Country code:</h3></td>
            </tr>
            <td></td>
            <tr>
            <td></td>
                <td align="left">
                <%--<asp:Panel ID="MainContentPanel" runat="server" height="50%" Width="88%" ScrollBars="auto" CssClass="main" horizontalalign="center"> --%>
                    <asp:GridView ID="grdmmauditingreport" runat="server" BackColor="White" 
                        allowpaging="True" pagesize="20" overflow="auto"
                        BorderColor="blue" BorderStyle="Solid" BorderWidth="1.5px"  
                        CellPadding="4" onselectedindexchanged="grdmmauditingreport_SelectedIndexChanged" 
                        AutoPostBack = "true" >
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                        <RowStyle BackColor="White" ForeColor="#003399" BorderStyle="Solid" BorderWidth ="1.5px" BorderColor="blue" />
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                        <SortedDescendingHeaderStyle BackColor="#002876" />
                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" Mode="Numeric" Position="Bottom" />  
                        <%--<PagerStyle BackColor="Yellow" ForeColor="Red"#8C4510 />--%>  
                        <PagerStyle ForeColor="White" HorizontalAlign="left" Font-Size="Medium" BackColor="blue" />
                    </asp:GridView>
                    <%--</asp:Panel>--%>
                </td>
                <td></td>
            </tr>
        </table>
    </div>

</asp:Content>
