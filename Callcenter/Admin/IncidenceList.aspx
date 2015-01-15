<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IncidenceList.aspx.cs" Inherits="Callcenter.Admin.IncidenceList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Meter estos dos ficheros para usar los datatables (tablas easy) -->
    <script src="../Scripts/DataTables-1.10.4/extensions/Responsive/js/dataTables.responsive.js"></script>
    <script src="../Scripts/DataTables-1.10.4/media/js/jquery.dataTables.js"></script>
    <link rel="stylesheet" href="//cdn.datatables.net/1.10.4/css/jquery.dataTables.css" type="text/css" />

    <table id="example" class="display" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th width="10%" style="visibility:hidden">Id</th>
                <th>Usuario</th>
                <th>Equipo</th>
                <th>Estado</th>
                <th>Fecha</th>
                <th>Fecha de cierre</th>
                <th></th>
            </tr>
        </thead>
        <tfoot>
            <tr>
                <th style="visibility:hidden">Id</th>
                <th>Usuario</th>
                <th>Equipo</th>
                <th>Estado</th>
                <th>Fecha</th>
                <th>Fecha de cierre</th>
                <th></th>
            </tr>
        </tfoot>
    </table>

    <script type="text/javascript">
        $.ajax({
            type: "POST",
            url: "IncidenceList.aspx/GetIncidences",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            bRetrieve: true,
            success: function (json) {
                $('#example').dataTable({
                    "aaData": json.d.aaData,
                    "aaSorting": [[3, "asc"], [4, "asc"]],
                    "aoColumnDefs": [
                        { "bVisible": false, "aTargets": [0] },
                            {
                                "bSortable": false,
                                "aTargets": [6],
                                "mData": 0,
                                "mRender": function (data, type, full) {
                                    return '<a href="IncidenceView.aspx?Id=' + data + '">Ver incidencia</a>';
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
    <asp:Label ID="LblError" runat="server" BackColor="Red" Font-Bold="True"></asp:Label>
    <br />
</asp:Content>
