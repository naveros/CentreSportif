﻿<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="CentreSportif.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Utilisez le formulaire ci-dessous pour créer un nouveau compte.</h2>
    </hgroup>

    <asp:CreateUserWizard runat="server" ID="RegisterUser" ViewStateMode="Disabled">
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="wizardStepPlaceholder" />
            <asp:PlaceHolder runat="server" ID="navigationPlaceholder" />
        </LayoutTemplate>
        <WizardSteps>
            <asp:CreateUserWizardStep runat="server" ID="RegisterUserWizardStep">
                <ContentTemplate>
                    <p class="message-info">
                        Les mots de passe doivent avoir un minimum de <%: Membership.MinRequiredPasswordLength %> caractères.
                    </p>

                    <p class="validation-summary-errors">
                        <asp:Literal runat="server" ID="ErrorMessage" />
                    </p>

                    <fieldset>
                        <legend>Formulaire d'inscription</legend>
                        <ol>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="Nom">Nom</asp:Label>
                                <asp:TextBox runat="server" ID="Nom" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Nom"
                                    CssClass="field-validation-error" ErrorMessage="Le champ de nom est nécessaire." />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="Prenom">Prénom</asp:Label>
                                <asp:TextBox runat="server" ID="Prenom" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Prenom"
                                    CssClass="field-validation-error" ErrorMessage="Le champ de prénom est nécessaire." />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="Sexe">Sexe</asp:Label>
                                <asp:TextBox runat="server" ID="Sexe" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Sexe"
                                    CssClass="field-validation-error" ErrorMessage="Le champ d'adresse sexe est nécessaire." />
                            </li>
                            <li>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Sexe" CssClass="field-validation-error" ErrorMessage="Le champ de sexe est nécessaire." />
                                <asp:Label runat="server" AssociatedControlID="Naissance">Date de naissance</asp:Label>
                                <asp:TextBox runat="server" ID="Naissance" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                                    CssClass="field-validation-error" ErrorMessage="Le champ de naissance est nécessaire." />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="UserName">Nom d'utilisateur</asp:Label>
                                <asp:TextBox runat="server" ID="UserName" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                                    CssClass="field-validation-error" ErrorMessage="Le champ de nom d'utilisateur est nécessaire." />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="Email">Adresse e-mail</asp:Label>
                                <asp:TextBox runat="server" ID="Email" TextMode="Email" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                    CssClass="field-validation-error" ErrorMessage="Le champ d'adresse e-mail est nécessaire." />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="Password">Mot de passe</asp:Label>
                                <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                                    CssClass="field-validation-error" ErrorMessage="Le mot de passe est nécessaire." />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="ConfirmPassword">Confirmez le mot de passe</asp:Label>
                                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Le champ de mot de passe de confirmation est nécessaire. " />
                                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Le mot de passe et la confirmation de passe ne correspondent pas." />
                            </li>
                        </ol>
                        <asp:Button runat="server" Text="S'enregistrer" OnClick="registerUser" />
                    </fieldset>
                </ContentTemplate>
                <CustomNavigationTemplate />
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep runat="server"></asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
</asp:Content>