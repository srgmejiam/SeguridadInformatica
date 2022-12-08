<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="SeguridadInformatica.Principal" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <link href="assets/CSS/Principal.css" rel="stylesheet" />

            <div class="card-body">

                <div class="container-fluid" style="margin-top: 20px">


                    <div class="row">

                        <asp:Panel runat="server" ID="panelFormulario_1" class="col-md-3 CardMenuPrincipal">
                            <asp:LinkButton runat="server" ID="lnkFormulario_1" Enabled="true" OnClick="lnkFormulario_1_Click" Style="text-decoration: none;">
                            <div class="card" style="background-color:rgb(234 98 0 / 0.60);color:white; height:130px; border-radius: 0px;">
                                <div class="card-content">
                                    <br />
                                    <div class="card-body">
                                        <div class="d-flex">
                                            <div class="align-self-center">
                                                <i class="fas fa-area-chart icono"></i>
                                            </div>
                                             <div class="text-right" >
                                                <h4>Formulario 1</h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            </asp:LinkButton>
                        </asp:Panel>

                        <asp:Panel runat="server" ID="panelFormulario_2" class="col-md-3 CardMenuPrincipal">
                            <asp:LinkButton runat="server" ID="lnkFormulario_2" Enabled="true" OnClick="lnkFormulario_2_Click" Style="text-decoration: none">
                            <div class="card" style="background-color:#73dcc6;color:white; height:130px; border-radius: 0px;">
                                <div class="card-content">
                                    <br />
                                    <div class="card-body">
                                        <div class="d-flex">
                                            <div class="align-self-center">
                                                <i class="fas fa-person-walking icono"></i>
                                            </div>
                                             <div class="text-right" >
                                                <h4>Formulario 2</h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            </asp:LinkButton>
                        </asp:Panel>

                        <asp:Panel runat="server" ID="panelFormulario_3" class="col-md-3 CardMenuPrincipal">
                            <asp:LinkButton runat="server" ID="lnkFormulario_3" OnClick="lnkFormulario_3_Click" Enabled="true" Style="text-decoration: none;">
                            <div class="card" style="background-color:rgb(197 60 147 / 0.86);color:white; height:130px; border-radius: 0px;">
                                <div class="card-content">
                                    <br />
                                    <div class="card-body">
                                        <div class="d-flex">
                                            <div class="align-self-center">
                                                <i class="fas fa-list-check icono"></i>
                                            </div>
                                             <div class="text-right" >
                                                <h4>Formulario 3</h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            </asp:LinkButton>
                        </asp:Panel>

                        <asp:Panel runat="server" ID="panelCatalogo_1" class="col-md-3 CardMenuPrincipal">
                            <asp:LinkButton runat="server" ID="lnkCatalogo_1" OnClick="lnkCatalogo_1_Click" Enabled="true" Style="text-decoration: none;">
                            <div class="card" style="background-color:rgb(47 60 147 / 0.86);color:white; height:130px; border-radius: 0px;">
                                <div class="card-content">
                                    <br />
                                    <div class="card-body">
                                        <div class="d-flex">
                                            <div class="align-self-center">
                                                <i class="fas fa-allergies icono"></i>
                                            </div>
                                             <div class="text-right" >
                                                <h4>Catálogo 1</h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            </asp:LinkButton>
                        </asp:Panel>

                         <asp:Panel runat="server" ID="panelCatalogo_2" class="col-md-3 CardMenuPrincipal">
                            <asp:LinkButton runat="server" ID="lnkCatalogo_2" OnClick="lnkCatalogo_2_Click" Enabled="true" Style="text-decoration: none;">
                            <div class="card" style="background-color:rgb(247 140 17 / 0.60);color:white; height:130px; border-radius: 0px;">
                                <div class="card-content">
                                    <br />
                                    <div class="card-body">
                                        <div class="d-flex">
                                            <div class="align-self-center">
                                                <i class="fas fa-allergies icono"></i>
                                            </div>
                                             <div class="text-right" >
                                                <h4>Catálogo 2</h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            </asp:LinkButton>
                        </asp:Panel>

                         <asp:Panel runat="server" ID="panelCatalogo_3" class="col-md-3 CardMenuPrincipal">
                            <asp:LinkButton runat="server" ID="lnkCatalogo_3" OnClick="lnkCatalogo_3_Click" Enabled="true" Style="text-decoration: none;">
                            <div class="card" style="background-color:rgb(87 60 147 / 0.86);color:white; height:130px; border-radius: 0px;">
                                <div class="card-content">
                                    <br />
                                    <div class="card-body">
                                        <div class="d-flex">
                                            <div class="align-self-center">
                                                <i class="fas fa-allergies icono"></i>
                                            </div>
                                             <div class="text-right" >
                                                <h4>Catálogo 3</h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            </asp:LinkButton>
                        </asp:Panel>
         
                        <asp:Panel runat="server" ID="panelUsuarios" class="col-md-3 CardMenuPrincipal">
                            <asp:LinkButton runat="server" ID="lnkUsuarios" Enabled="true" OnClick="lnkUsuarios_Click" Style="text-decoration: none;">
                            <div class="card" style="background-color:rgb(85 103 157 / 0.95); color:white; height:130px; border-radius: 0px;">
                                <div class="card-content">
                                    <br />
                                    <div class="card-body">
                                        <div class="d-flex">
                                            <div class="align-self-center">
                                                <i class="fas fa-users icono"></i>
                                            </div>
                                             <div class="text-right" >
                                                <h4>Usuarios</h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            </asp:LinkButton>
                        </asp:Panel>

                    </div>

                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
