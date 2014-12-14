<%@ Page Title="S'inscrire" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="CentreSportif.Account.Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:CreateUserWizard ID="RegisterUser" runat="server" EnableViewState="false" OnCreatedUser="RegisterUser_CreatedUser">
        <LayoutTemplate>
            <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <WizardSteps>
            <asp:CreateUserWizardStep ID="RegisterUserWizardStep" runat="server">
                <ContentTemplate>
                    <h2>
                        Créer un nouveau compte
                    </h2>
                    <p>
                        Utilisez le formulaire ci-dessous pour créer un nouveau compte.
                    </p>
                    <p>
                        Les mots de passe doivent comporter au minimum <%= Membership.MinRequiredPasswordLength %> caractères.
                    </p>
                    <span class="failureNotification">
                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="RegisterUserValidationGroup"/>
                    <div class="accountInfo">
                        <fieldset class="register">
                            <legend>Informations de compte</legend>
                            <p>
                               <asp:Label ID="LabelNom" runat="server" AssociatedControlID="Nom">Nom</asp:Label>
                                <asp:TextBox runat="server" ID="Nom" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Nom"
                                    CssClass="field-validation-error" ErrorMessage="Le champ de nom est nécessaire." />
                            </p>
                            <p>
                                 <asp:Label ID="LabelPrenom" runat="server" AssociatedControlID="Prenom">Prénom</asp:Label>
                                <asp:TextBox runat="server" ID="Prenom" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="Prenom"
                                    CssClass="field-validation-error" ErrorMessage="Le champ de prénom est nécessaire." />
                            </p>
                            <p>
                               <asp:Label ID="LabelSexe" runat="server" AssociatedControlID="Sexe">Sexe</asp:Label>
                                <asp:DropDownList ID="Sexe" runat="server">
                                <asp:ListItem Value="0" Text="Masculin"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Féminin"></asp:ListItem>
                                </asp:DropDownList>
                            </p>
                            <p>
                                <asp:Label ID="labelAdresse" runat="server" AssociatedControlID="Adresse">Adresse</asp:Label>
                                <asp:TextBox runat="server" ID="Adresse" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="Adresse"
                                    CssClass="field-validation-error" ErrorMessage="Le champ Adresse est nécessaire." />
                            </p>
                             <p>
                                <asp:Label ID="LabelVille" runat="server" AssociatedControlID="Ville">Ville/Province</asp:Label>
                                <asp:TextBox runat="server" ID="Ville" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Ville"
                                    CssClass="field-validation-error" ErrorMessage="Le champ Ville/Province est nécessaire." />
                            </p>
                             <p>
                               <asp:Label ID="LabelCodePostal" runat="server" AssociatedControlID="CodePostal">Code postal</asp:Label>
                                <asp:TextBox runat="server" ID="CodePostal" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="CodePostal"
                                    CssClass="field-validation-error" ErrorMessage="Le champ Code Postal est nécessaire." />
                            </p>
                             <p>
                                <asp:Label ID="LabelDDN" runat="server" AssociatedControlID="Naissance">Date de naissance</asp:Label>
                                <asp:TextBox runat="server" ID="Naissance" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Naissance"
                                    CssClass="field-validation-error" ErrorMessage="Le champ Date de naissance est nécessaire." />
                            </p>
                            <p>
                                 <asp:Label ID="Label1" runat="server" AssociatedControlID="Email">Adresse e-mail</asp:Label>
                                <asp:TextBox runat="server" ID="Email" TextMode="Email" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="Email"
                                    CssClass="field-validation-error" ErrorMessage="Le champ d'adresse e-mail est nécessaire." />
                            </p>
                            <p>
                                <asp:Label ID="Label2" runat="server" AssociatedControlID="UserName">Nom d'utilisateur</asp:Label>
                                <asp:TextBox runat="server" ID="UserName" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="UserName"
                                    CssClass="field-validation-error" ErrorMessage="Le champ de nom d'utilisateur est nécessaire." />
                            </p>
                             <p>
                                <asp:Label ID="Label3" runat="server" AssociatedControlID="Password">Mot de passe</asp:Label>
                                <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Password"
                                    CssClass="field-validation-error" ErrorMessage="Le mot de passe est nécessaire." />
                            </p>
                             <p>
                               <asp:Label ID="Label4" runat="server" AssociatedControlID="ConfirmPassword">Confirmez le mot de passe</asp:Label>
                                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Le champ de mot de passe de confirmation est nécessaire. " />
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Le mot de passe et la confirmation de passe ne correspondent pas." />
                            </p>
                        </fieldset>
                        <p class="submitButton">
                            <asp:Button ID="CreateUserButton" runat="server" CommandName="MoveNext" Text="Créer un utilisateur" 
                                 ValidationGroup="RegisterUserValidationGroup"/>
                        </p>
                    </div>
                </ContentTemplate>
                <CustomNavigationTemplate>
                </CustomNavigationTemplate>
            </asp:CreateUserWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
</asp:Content>
