<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="DDT-Mef.aspx.cs" Inherits="GestioneCantieri2._0.DDT_Mef" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>DDT Mef</title>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#body_spinnerImg").hide();
        });

        function ShowHideLoader() {
            $("#body_spinnerImg").show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1 class="text-center mt-3">DDT Mef</h1>
    <div class="row mt-3">
        <div class="col-12 text-center">
            <div class="offset-md-1 col-10">
                <div class="row">
                    <div class="col-2">
                        <asp:Label ID="lblAcquirente" runat="server" Text="Acquirente"></asp:Label>
                        <asp:TextBox ID="txtAcquirente" CssClass="form-control" runat="server" Text="Mau"></asp:TextBox><br />
                        <asp:Label ID="lblFornitore" runat="server" Text="Fornitore"></asp:Label>
                        <asp:DropDownList ID="ddlFornitore" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div class="col-2 dbfButtonWrapper">
                        <asp:Button ID="btn_GeneraDdtDaDbf" class="btn btn-info btn-lg" OnClick="btn_GeneraDdtDaDbf_Click" OnClientClick="javascript:ShowHideLoader()" Text="Importa DBF" runat="server" />
                        <img id="spinnerImg" visible="false" src="Images/spinner.gif" alt="spinner" runat="server" />
                    </div>
                    <div class="col-8 recapContainer">
                        <div class="row">
                            <!-- Media prezzo unitario -->
                            <div class="col-4">
                                <asp:Label ID="lblMedia" runat="server" CssClass="w-100" Text="Media Prezzo Unitario"></asp:Label>
                                <asp:TextBox ID="txtMedia" Enabled="false" CssClass="form-control w-100" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row mt-4">
                            <!-- Imponibile,Iva,Totale DDT -->
                            <div class="col-4">
                                <asp:Label ID="lblImponibileDDT" runat="server" CssClass="w-100" Text="Imponibile DDT"></asp:Label>
                                <asp:TextBox ID="txtImponibileDDT" Enabled="false" CssClass="form-control w-100" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-4">
                                <asp:Label ID="lblIvaDDT" runat="server" CssClass="w-100" Text="Iva DDT"></asp:Label>
                                <asp:TextBox ID="txtIvaDDT" Enabled="false" CssClass="form-control w-100" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-4">
                                <asp:Label ID="lblTotDDT" runat="server" CssClass="w-100" Text="Totale DDT"></asp:Label>
                                <asp:TextBox ID="txtTotDDT" Enabled="false" CssClass="form-control w-100" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlFiltriDDT" DefaultButton="btnSearch" CssClass="w-100 mt-3" runat="server">
        <div class="row">
            <!-- Ricerca Per Anno -->
            <div class="col-2">
                <asp:Label ID="lblCercaAnno" runat="server" CssClass="h5" Text="Cerca per anno"></asp:Label>
                <br />
                <asp:Label ID="lblAnnoInizio" runat="server" Text="Anno Iniziale"></asp:Label>
                <asp:TextBox ID="txtAnnoInizio" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-2">
                <asp:Label ID="lblCercaData" runat="server" CssClass="h5" Text="Cerca per data"></asp:Label>
                <br />
                <asp:Label ID="lblDataInizio" runat="server" Text="Data Inizio"></asp:Label>
                <asp:TextBox ID="txtDataInizio" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-2">
                <asp:Label ID="lblCercaQta" runat="server" CssClass="h5" Text="Cerca per Qta o N_DDT"></asp:Label>
                <br />
                <asp:Label ID="lblQta" runat="server" Text="Quantità"></asp:Label>
                <asp:TextBox ID="txtQta" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-4">
                <asp:Label ID="Label1" runat="server" CssClass="h5" Text="CodArt & DescriCodArt"></asp:Label>
                <div class="row">
                    <asp:Label ID="lblCercaCodArt" CssClass="w-100" runat="server" Text="Cerca per codice articolo"></asp:Label>
                    <div class="col-4">
                        <asp:TextBox ID="txtCodArt1" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-4">
                        <asp:TextBox ID="txtCodArt2" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-4">
                        <asp:TextBox ID="txtCodArt3" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-2 text-center">
                <br />
                <br />
                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="btn btn-primary btn-lg" Text="Cerca" />
            </div>
        </div>
        <div class="row mt-3">
            <div class="col-2">
                <asp:Label ID="lblAnnoFine" runat="server" Text="Anno Finale"></asp:Label>
                <asp:TextBox ID="txtAnnoFine" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-2">
                <asp:Label ID="lblDataFine" runat="server" Text="Data Fine"></asp:Label>
                <asp:TextBox ID="txtDataFine" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-2">
                <asp:Label ID="lblN_DDT" runat="server" Text="N_DDT"></asp:Label>
                <asp:TextBox ID="txtN_DDT" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-4">
                <div class="row">
                    <asp:Label ID="lblCercaDescriCodArt" CssClass="w-100" runat="server" Text="Cerca per Descrizione Cod. Art."></asp:Label>
                    <div class="col-4">
                        <asp:TextBox ID="txtDescriCodArt1" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-4">
                        <asp:TextBox ID="txtDescriCodArt2" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-4">
                        <asp:TextBox ID="txtDescriCodArt3" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-2 text-center">
                <br />
                <asp:Button ID="btnSvuotaTxt" runat="server" OnClick="btnSvuotaTxt_Click" Text="Svuota Caselle di Testo" CssClass="btn btn-default btn-lg" />
            </div>
        </div>
    </asp:Panel>
    <div class="row">
        <div class="col-12 mt-5">
            <asp:GridView ID="grdListaDDTMef" runat="server" OnRowDataBound="grdListaDDTMef_RowDataBound" ItemType="GestioneCantieri.Data.DDTMef"
                AutoGenerateColumns="False" OnRowCommand="grdListaDDTMef_RowCommand" BorderWidth="0"
                PageSize="20" OnPageIndexChanging="grdListaDDTMef_PageIndexChanging" CssClass="table table-dark table-striped text-center">
                <Columns>
                    <asp:BoundField DataField="Anno" HeaderText="Anno" />
                    <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}" ApplyFormatInEditMode="True" />
                    <asp:BoundField DataField="NDDt" HeaderText="N_DDT" />
                    <asp:BoundField DataField="CodArt" HeaderText="Codice Articolo" />
                    <asp:BoundField DataField="DescriCodArt" HeaderText="Descrizione Codice Articolo" />
                    <asp:BoundField DataField="Qta" HeaderText="Quantità" />
                    <asp:BoundField DataField="Importo" HeaderText="Importo" DataFormatString="{0:0.00}" />
                    <asp:BoundField DataField="Acquirente" HeaderText="Acquirente" />
                    <asp:BoundField DataField="PrezzoUnitario" HeaderText="Prezzo Unitario" DataFormatString="{0:0.00}" />
                    <asp:BoundField DataField="AnnoNDDT" HeaderText="Anno N_DDT" />
                </Columns>
                <PagerSettings Mode="Numeric" Position="Bottom" PageButtonCount="20" />
                <PagerStyle ForeColor="#333" BorderWidth="0" BorderColor="Transparent" BorderStyle="None" CssClass="text-center pagination-container" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
