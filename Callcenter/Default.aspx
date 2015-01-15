<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Callcenter._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    <h3>Para usar la plataforma:</h3>
    <ol class="round">
        <li class="one">
            <h5>Inicio de sesión</h5>
            Para empezar a usar el Call Center lo primero que debes hacer es iniciar la sesión con tu usuario y contraseña.
            Si aún no tienes un usuario, puedes registrarte grauitamente.
        </li>
        <li class="two">
            <h5>Introduce los datos</h5>
            Para trabajar las incidencias, primero debes entrar en las paginas de datos e introducir tus equipos, para crear
            las incidencias sobre ellos y facilitar la identificación a la hora de resolverlas.
        </li>
        <li class="three">
            <h5>Incidencias</h5>
            Cuando tienes todo regitrado, y quieres reportar una incidencia, simplemente ve a Incidencias, elige tu equipo y
            pulsa en añadir. Elige la incidencia recién creada y envía un mensaje explicando qué ocurre. Espera a que
            el técnico contacte contigo, mediante mensaje, o por otros medios que le quieras dejar 
            para empezar buscar una solución.
        </li>
        <li class="four">
            <h5>Solucionar la incidencia</h5>
            En caso de ser una incidencia complicada, el técnico puede necesitar conectarse de forma remota a su equipo.
            Si le da permiso para hacerlo necesitará TeamViewer. Esta herramienta creará una conexión temporal segura que
            permitirá al técnico tomar el control de su ordenador para investigar su incidencia. <br />Si no lo tiene instalado, 
            puede descargarlo <a href="http://www.teamviewer.com/es/download/windows.aspx">
                <span style="color: blue ">desde aqui</span></a>:<br />
            Si tiene alguna duda respecto a la descarga y uso, consulte al técnico.
        </li>
    </ol>
</asp:Content>
