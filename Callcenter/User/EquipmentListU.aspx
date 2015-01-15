<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EquipmentListU.aspx.cs" Inherits="Callcenter.User.EquipmentListU" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Meter estos dos ficheros para usar los datatables (tablas easy) -->
    <script src="../Scripts/DataTables-1.10.4/extensions/Responsive/js/dataTables.responsive.js"></script>
    <script src="../Scripts/DataTables-1.10.4/media/js/jquery.dataTables.js"></script>
    <link rel="stylesheet" href="//cdn.datatables.net/1.10.4/css/jquery.dataTables.css" type="text/css" />
    <!-- Crear tabla . 1- Crear encabezado y pie a mano -->
    <table id="example" class="display" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th width="10%" style="visibility:hidden">Id</th>
                <th>Descripcion</th>
                <th>Tipo de equipo</th>
                <th>Fecha de compra</th>
                <th></th><th></th>
            </tr>
        </thead>
        <tfoot>
            <tr>
                <th style="visibility:hidden">Id</th>
                <th>Descripcion</th>
                <th>Tipo de equipo</th>
                <th>Fecha de compra</th>
                <th></th><th></th>
            </tr>
        </tfoot>
    </table>

    <!-- Crear tabla . 2- Script de creacion  -->
    <script type="text/javascript">
            $.ajax({
                type: "POST",
                url: "EquipmentListU.aspx/GetEquipments",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                bRetrieve: true,
                success: function (json) {
                    $('#example').dataTable({
                        "aaData": json.d.aaData,
                        "aaSorting": [[1, "desc"]],
                        "aoColumnDefs": [
                            { "bVisible": false, "aTargets": [0] },
                                {
                                    "bSortable": false,
                                    "aTargets": [4],
                                    "mData": 0,
                                    "mRender": function (data, type, full) {
                                        return '<a href="EquipmentNewEditU.aspx?Id=' + data + '">Editar</a>';
                                    }
                                },
                                {
                                    "bSortable": false,
                                    "aTargets": [5],
                                    "mData": 0,
                                    "mRender": function (data, type, full) {
                                        return '<a href="EquipmentListU.aspx?Id=' + data + '">Eliminar</a>';
                                    }
                                }

                        ],
                        "language": {
                            "url": '../Scripts/DataTables-1.10.4/localization/Spanish.txt'
                        }
                    });
                },
                error: function (xhr, status, err) {
                    var error = jQuery.parseJSON(xhr.responseText).Message
                    alert(error);
                }
            });
    </script>
    <br />
    <br />
    <asp:Button ID="BtnNew" runat="server" OnClick="BtnNew_Click" Text="Añadir nuevo" />
    <br />
    <asp:Label ID="LblError" runat="server" BackColor="Red" Font-Bold="True"></asp:Label>
    <br />
</asp:Content>
