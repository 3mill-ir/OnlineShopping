<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Views_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title><%= "ورود به سیستم" + " | " + Constants.StaticTitle%></title>
    <meta name="Description" content='<%= Constants.StaticDescription %>'/>
    <meta name="Keywords" content='<%= Constants.StaticKeywords %>'/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="ContentMainDiv" style="padding : 15px;">
        <asp:Login ID="Login1" runat="server" onloggedin="Login1_LoggedIn" style="margin-right : auto; margin-left : auto; margin-top : 20px; margin-bottom : 20px;"
            BorderColor="#27739E" BorderStyle="Solid" BorderWidth="1px" Height="100px" 
            LoginButtonText="ورود به سیستم" Orientation="Horizontal" 
            PasswordLabelText="گذرواژه :" TitleText="ورود به سیستم" 
            UserNameLabelText="نام کاربری :" Width="650px" 
            RememberMeText="مرا بخاطر بسپار" 
            FailureText="نام کاربری یا گذرواژه وارد شده اشتباه است ، لطفاً مجدداً سعی کنید." 
            PasswordRequiredErrorMessage="گذرواژه خود را وارد فرمائید." 
            UserNameRequiredErrorMessage="نام کاربری خود را وارد فرمائید.">
            <TextBoxStyle CssClass="TextBoxes" Font-Names="Tahoma" Font-Size="8pt" 
                Width="100px" />
            <LoginButtonStyle CssClass="Buttons" Width="120px" />
            <TitleTextStyle BackColor="#27739E" Font-Bold="True" ForeColor="White" 
                Height="30px" />
        </asp:Login>
    </div>
</asp:Content>