using GestioneCantieri2._0.DAO;
using GestioneCantieri2._0.Models;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestioneCantieri2._0
{
    public partial class DDT_Mef : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        #region Eventi Click
        protected void btn_GeneraDdtDaDbf_Click(object sender, EventArgs e)
        {
            string pathFile = "";

            pathFile = @"C:\MEF\ORDINI\";

            int idFornitore = FornitoriDAO.GetIdFornitore("Mef");

            //spinnerImg.Visible = true;

            // Genero una lista a partire dai dati contenuti nel nuovo file DBF
            List<TblDDTMefModel> ddtList = TblDDTMefModelDAO.GetDdtFromDBF(pathFile, txtAcquirente.Text, idFornitore);

            // Popolo la tabella temporanea
            InsertIntoDdtTemp(ddtList);

            //Prendo la lista dei DDT non presenti sulla tabella TblDDTMef
            List<TblDDTMefModel> ddtMancanti = TblDDTMefModelDAO.GetNewDDT();

            foreach (TblDDTMefModel ddt in ddtMancanti)
            {
                // Inserisco i nuovi DDT
                TblDDTMefModelDAO.InsertNewDdt(ddt);
            }

            //Aggiorno i prezzi del mese corrente
            UpdatePrezzi();

            BindGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtAnnoInizio.Text != "" || txtAnnoFine.Text != "")
            {
                txtDataInizio.Text = "";
                txtDataFine.Text = "";
            }
            else if (txtDataInizio.Text != "" || txtDataFine.Text != "")
            {
                txtAnnoInizio.Text = "";
                txtAnnoFine.Text = "";
            }

            BindGridWithSearch();
        }

        protected void btnSvuotaTxt_Click(object sender, EventArgs e)
        {
            //txtAnnoInizio.Text = "";
            //txtAnnoFine.Text = "";
            //txtDataInizio.Text = "";
            //txtDataFine.Text = "";
            //txtQta.Text = "";
            //txtN_DDT.Text = "";
            //txtCodArt1.Text = "";
            //txtCodArt2.Text = "";
            //txtCodArt3.Text = "";
            //txtDescriCodArt1.Text = "";
            //txtDescriCodArt2.Text = "";
            //txtDescriCodArt3.Text = "";
            foreach(Control c in pnlFiltriDDT.Controls)
            {
                if(c is TextBox)
                {
                    ((TextBox)c).Text = "";
                }
            }

            BindGrid();
            GetBasicValuesForRecap();
        }
        #endregion

        #region Eventi GridView
        protected void grdListaDDTMef_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grdListaDDTMef_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdListaDDTMef_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        #endregion

        #region Helpers
        protected void BindGrid()
        {
            List<TblDDTMefModel> listaDDT = new List<TblDDTMefModel>();
            listaDDT = TblDDTMefModelDAO.getDDTList();
            grdListaDDTMef.DataSource = listaDDT;
            grdListaDDTMef.DataBind();
            GetBasicValuesForRecap();
        }
        private void GetBasicValuesForRecap()
        {
            txtMedia.Text = TblDDTMefModelDAO.calcolaMediaPrezzoUnitario().ToString("0.00") + " €";
            txtTotDDT.Text = TblDDTMefModelDAO.GetTotalDDT().ToString("N2") + " €";
            txtImponibileDDT.Text = TblDDTMefModelDAO.GetImponibileDDT().ToString("N2") + " €";
            txtIvaDDT.Text = TblDDTMefModelDAO.GetIvaDDT().ToString("N2") + " €";
        }
        protected void BindGridWithSearch()
        {
            List<TblDDTMefModel> listaDDT = new List<TblDDTMefModel>();
            TblDDTMefObject ddt = FillDdtObject();

            // Rigenero la griglia
            listaDDT = TblDDTMefModelDAO.searchFilter(ddt);
            grdListaDDTMef.DataSource = listaDDT;
            grdListaDDTMef.DataBind();

            //Rigenero il valore della media dei prezzi unitari
            ddt = FillDdtObject();
            txtMedia.Text = TblDDTMefModelDAO.calcolaMediaPrezzoUnitarioWithSearch(ddt).ToString("0.00") + " €";
            ddt = FillDdtObject();
            txtTotDDT.Text = TblDDTMefModelDAO.GetTotalDDT(ddt).ToString("N2") + " €";
            ddt = FillDdtObject();
            txtImponibileDDT.Text = TblDDTMefModelDAO.GetImponibileDDT(ddt).ToString("N2") + " €";
            ddt = FillDdtObject();
            txtIvaDDT.Text = TblDDTMefModelDAO.GetIvaDDT(ddt).ToString("N2") + " €";
        }
        protected TblDDTMefObject FillDdtObject()
        {
            TblDDTMefObject ddt = new TblDDTMefObject();
            ddt.AnnoInizio = txtAnnoInizio.Text;
            ddt.AnnoFine = txtAnnoFine.Text;
            ddt.DataInizio = txtDataInizio.Text;
            ddt.DataFine = txtDataFine.Text;
            ddt.Qta = txtQta.Text;
            ddt.NDdt = txtN_DDT.Text;
            ddt.CodArt1 = txtCodArt1.Text;
            ddt.CodArt2 = txtCodArt2.Text;
            ddt.CodArt3 = txtCodArt3.Text;
            ddt.DescriCodArt1 = txtDescriCodArt1.Text;
            ddt.DescriCodArt2 = txtDescriCodArt2.Text;
            ddt.DescriCodArt3 = txtDescriCodArt3.Text;
            return ddt;
        }
        protected void FillDdlClienti()
        {
            List<TblForitoriModel> listClienti = FornitoriDAO.GetFornitori();

            ddlFornitore.Items.Clear();
            ddlFornitore.Items.Add(new ListItem("", "-1"));

            foreach (TblForitoriModel f in listClienti)
                ddlFornitore.Items.Add(new ListItem(f.RagSocForni, f.IdFornitori.ToString()));
        }
        protected void InsertIntoDdtTemp(List<TblDDTMefModel> ddtList)
        {
            // Svuoto la tabella TblDDTMefModelTemp
            TblDDTMefModelDAO.DeleteFromDdtTemp();

            // Per ogni elemento della lista
            foreach (TblDDTMefModel ddt in ddtList)
            {
                // Popolo la tabella temporanea con i nuovi dati
                TblDDTMefModelDAO.InsertIntoDdtTemp(ddt);
            }
        }
        private void UpdatePrezzi()
        {
            TblDDTMefModelDAO.UpdateDdt();
        }
        #endregion
    }
}