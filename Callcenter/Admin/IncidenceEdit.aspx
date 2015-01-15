<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IncidenceEdit.aspx.cs" Inherits="Callcenter.Admin.IncidenceEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #editForm {
            height: 137px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <br />
    </p>
        <asp:Label ID="LblNew" runat="server" Text="Crear una nueva incidencia" Font-Bold="True" Font-Size="Large" Font-Underline="True"></asp:Label>
        <asp:Label ID="lblEdit" runat="server" Font-Bold="True" Font-Size="14pt" Font-Underline="True" Text="Editar incidencia" Visible="False"></asp:Label>
        <br />
        <br />
        <asp:GridView ID="gridIncidence" runat="server" Visible="False">
        </asp:GridView>
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="BtnEdit" runat="server" Text="Editar"/>
        <asp:Button ID="btnClose" runat="server" Text="Cerrar incidencia"/>
        <asp:Button ID="BtnBackView" runat="server" Text="Volver a incidencia"/>
        <br />
        <br />
        <asp:Label ID="LblError" runat="server" BackColor="Red" Font-Bold="True"></asp:Label>
        <br />
        <asp:TextBox ID="txtId" runat="server" Visible="False"></asp:TextBox>
        <br />
    </asp:Content>
