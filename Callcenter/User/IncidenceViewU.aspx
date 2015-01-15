<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IncidenceViewU.aspx.cs" Inherits="Callcenter.User.IncidenceViewU" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:GridView ID="IncidenceData" runat="server" Width="90%"></asp:GridView>
    <asp:GridView ID="GridMessages" runat="server">
    </asp:GridView>
    <br />


    <!--Listado de los mensajes-->

    <br />
    <br />
        <br />
        <asp:Label ID="LblMessage" runat="server" Font-Bold="True" Text="Nuevo Mensaje"></asp:Label>
        <br />
        <br />
        <asp:Label ID="LblTitle" runat="server" Text="Asunto"></asp:Label>
        <br />
        <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="LblText" runat="server" Text="Mensaje"></asp:Label>
        <br />
        <textarea ID="areaText" name="areaText" runat="server"></textarea><br />
        <br />
        <asp:Button ID="BtnSend" runat="server" OnClick="BtnSend_Click" Text="Enviar" />
    <br />
    <br />
    <br />
    <asp:Button ID="BtnEdit" runat="server" Text="Editar incidencia" OnClick="BtnEdit_Click" />
    <asp:Button ID="BtnReabrir" runat="server" Text="Reabrir Incidencia" Visible="False" OnClick="BtnReabrir_Click" />
    <asp:Button ID="BtnBack" runat="server" Text="Volveral listado" OnClick="BtnBack_Click" />
    <br />
    <br />
    <asp:TextBox ID="txtId" runat="server" Visible="False"></asp:TextBox>
    <asp:Label ID="LblError" runat="server" BackColor="Red" Font-Bold="True"></asp:Label>
    <br />
</asp:Content>
