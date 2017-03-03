<%@ Page Title="Development Data Discovery"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="DDRPOC.Home" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
<%--<link rel="stylesheet" type="text/css" href="<%=Request.ApplicationPath%>../Styles/Site.css" />--%>
<%--<link id="Link1"  href="~/Styles/Site.css" runat="server" rel="stylesheet" type="text/css" media="screen"/>--%>
<style type="text/css" >
        .style1
        {
            width: 106px;
        }
       .page
        {
        width: 1300px;
        background-color: #fff;
        margin: 15px auto 0px auto;
        border: 1px solid #496077;
        }
        .header
        {
            position: relative;
            margin: 0px;
            padding: 0px;
            background:#63CAF0;
            width: 100%;
        }
        .title
        {
            display: block;
            float: left;
            text-align: left;
            width: auto;
        }
        .loginDisplay
        {
            font-size: 1.1em;
            display: block;
            text-align: right;
            padding: 10px;
            color: black;
            background-color:#63caf0;
        }
        div.hideSkiplink
        {
            background-color:#3a4f63;
            width:100%;
        }
        .main
        {
			padding: 0px 12px;
            margin: 12px 8px 8px 8px;
            min-height: 530px;
            width: 1250px;
            height:550px;
        }
        .footer
        {
            color: #EEE;
            padding: 0px 0px 0px 0px;
            margin: 0px auto;
            margin-top: 10px;
            height: 40px;
            text-align: center;
            line-height: normal ;
            background-color: #007fff;
            background: #000000; 
        }
        /* TAB MENU   
        ----------------------------------------------------------*/
        div.hideSkiplink
        {
            background-color:#3a4f63;
            width:100%;
        }
        div.menu
        {
            padding: 4px 0px 4px 8px;
        }
        div.menu ul
        {
            list-style: none;
            margin: 0px;
            padding: 0px;
            width: auto;
        }
        div.menu ul li a, div.menu ul li a:visited
        {
            background-color: #465c71;
            border: 1px #4e667d solid;
            color: #dde4ec;
            display: block;
            line-height: 1.35em;
            padding: 4px 20px;
            text-decoration: none;
            white-space: nowrap;
        }
        div.menu ul li a:hover
        {
            background-color: #bfcbd6;
            color: #465c71;
            text-decoration: none;
        }
        div.menu ul li a:active
        {
            background-color: #465c71;
            color: #cfdbe6;
            text-decoration: none;
        }
       /* FORM ELEMENTS   
----------------------------------------------------------*/
            fieldset
            {
                margin: 1em 0px;
                padding: 1em;
                border: 1px solid #ccc;
            }
            fieldset p 
            {
                margin: 2px 12px 10px 10px;
            }
            fieldset.login label, fieldset.register label, fieldset.changePassword label
            {
                display: block;
            }
            fieldset label.inline 
            {
                display: inline;
            }
            legend 
            {
                font-size: 1.1em;
                font-weight: 600;
                padding: 2px 4px 8px 4px;
            }
            input.textEntry 
            {
                width: 200px;
                border: 1px solid #ccc;
            }
            input.passwordEntry 
            {
                width: 200px;
                border: 1px solid #ccc;
            }
            div.accountInfo
            {
                width: 42%;
            }
            /* MISC  
----------------------------------------------------------*/
        .clear
        {
            clear: both;
        }
        .title
        {
            display: block;
            float: left;
            text-align: left;
            width: auto;
           /* color:Black */
        }
        .loginDisplay
        {
            font-size: 1.1em;
            display: block;
            text-align: right;
            padding: 10px;
            color: black;
            background-color:#63caf0;
        }
        .loginDisplay a:link
        {
            color: white;
        }
        .loginDisplay a:visited
        {
            color: white;
        }
        .loginDisplay a:hover
        {
            color: white;
        }
        .failureNotification
        {
            font-size: 1.2em;
            color: Red;
        }
        .bold
        {
            font-weight: bold;
        }
        .submitButton
        {
            text-align: right;
            padding-right: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

      <table style="width: 100%">
      <tr align="center"><td>
      <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false">
        <LayoutTemplate>
            <div class="accountInfo">
                <fieldset class="login">
                    <legend align="left">Account Information</legend>
                    <p>
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName"  >Username:</asp:Label>
                        <asp:TextBox ID="UserName" runat="server" CssClass="textEntry" 
                            ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                             CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                        <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                             CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:CheckBox ID="RememberMe" runat="server"/>
                        <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Keep me logged in</asp:Label>
                        <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" 
                            CssClass="failureNotification" ValidationGroup="LoginUserValidationGroup" 
                            DisplayMode="List" />
                        <p>
                            <asp:Label ID="lblnopassworduser" runat="server" ForeColor="Red" Text="Label" CssClass="failureNotification"></asp:Label>
                        </p>
                    </p>
                </fieldset>
                <p class="submitButton">
                    <asp:Button ID="LoginButton" runat="server" Text="Log In" 
                        onclick="LoginButton_Click1" 
                        ValidationGroup="LoginUserValidationGroup" />
                </p>
            </div>
        </LayoutTemplate>
    </asp:Login>
      </td></tr></table>
</asp:Content>
