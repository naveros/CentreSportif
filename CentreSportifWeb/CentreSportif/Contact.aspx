<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Contact.aspx.cs" Inherits="CentreSportif.Contact" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent"> 
      <h1>Contactez-nous!</h1>
    <section class="contact">
        <header>
            
        </header>
         <p>
            <span class="label">Support:</span>
            <span>courriel:Support@cspTeam.com </br> telephone: (514)212-8564</span></p>
        <p>
            <span class="label">Marketing:</span>
            <span>Marketing@cspTeam.com </br> telephone: (514)212-8565</span></p>
        <p>
            <strong>Général:</strong>
            <span>General@spTeam.com </br> telephone: (514)212-8566</span>
        </p>
    </section>

    <section class="contact">
        <header>
            <h3>Address:</h3>
        </header>
        <p>
            698 René-Lévesque Blvd</br>
            Montreal, QC H3B 4W8
        </p>
    </section>
</asp:Content>
