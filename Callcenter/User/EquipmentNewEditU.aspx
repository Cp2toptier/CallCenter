<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EquipmentNewEditU.aspx.cs" Inherits="Callcenter.User.EquipmentNewEditU" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <p>
    <br />
    <asp:Label ID="LblPurchaseDate" runat="server" Text="Fecha de compra"></asp:Label>
    <br />
    <asp:TextBox ID="TxtPurchaseDate" runat="server" Width="300px"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="LblDescription" runat="server" Text="Descripcion"></asp:Label>
    <br />
    <asp:TextBox ID="TxtDescription" runat="server" Width="300px"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="LblEquipmentType" runat="server" Text="Tipo de Equipo"></asp:Label>
    <br />
    <asp:DropDownList ID="DropEquipmentType" runat="server" Height="18px" Width="311px">
    </asp:DropDownList>
    </p>
    <br />
    <br />

    <asp:Button ID="BtnNew" runat="server" OnClick="BtnNew_Click" Text="Nuevo" />
    <asp:Button ID="BtnEdit" runat="server" OnClick="BtnEdit_Click" Text="Editar" Visible="False" />
    <asp:Button ID="BtnBack" runat="server" OnClick="BtnBack_Click" Text="Volver al listado"/>
    <br />
    <br />
    <asp:Label ID="LblError" runat="server" BackColor="Red" Font-Bold="True"></asp:Label>
    <asp:Label ID="LblExito" runat="server" BackColor="Green" Font-Bold="True"></asp:Label>
</asp:Content>
