﻿using Dapper;
using GestioneCantieri2._0.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;

namespace GestioneCantieri2._0.DAO
{
    public class TblDDTMefModelDAO : DAOBase
    {
        /*** Lista completa dei DDT ***/
        public static List<TblDDTMefModel> getDDTList()
        {
            string sql = "";
            SqlConnection cn = GetConnection();

            try
            {
                sql = "SELECT TOP 500 IdDDTMef, Anno, Data, N_DDT, CodArt, " +
                      "DescriCodArt, Qta, Importo, Acquirente, PrezzoUnitario, AnnoN_DDT " +
                      "FROM TblDDTMef " +
                      "ORDER BY Anno, Data, N_DDT, CodArt";

                return cn.Query<TblDDTMefModel>(sql).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dell'elenco dei DDT", ex);
            }
            finally { CloseResouces(cn, null); }
        }

        public static DataTable GetDDT(string anno, string n_ddt)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            anno = "%" + anno + "%";
            n_ddt = "%" + n_ddt + "%";

            try
            {
                sql = "SELECT Data, N_DDT " +
                      "FROM TblDDTMef " +
                      "WHERE Anno LIKE @pAnno AND N_DDT LIKE @pN_DDT " +
                      "GROUP BY N_DDT, Data " +
                      "ORDER BY Data, N_DDT ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pAnno", anno));
                cmd.Parameters.Add(new SqlParameter("pN_DDT", n_ddt));
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei DDT Mef", ex);
            }
        }
        public static List<TblDDTMefModel> GetDDTByNumDDT(string nDDT)
        {
            TblDDTMefModel ddt = new TblDDTMefModel();
            string sql = "";
            SqlConnection cn = GetConnection();

            try
            {
                sql = "SELECT IdDDTMef, Anno, Data, N_DDT, CodArt, " +
                      "DescriCodArt, Qta, Importo, Acquirente, PrezzoUnitario, AnnoN_DDT " +
                      "FROM TblDDTMef " +
                      "WHERE N_DDT = @N_DDT";

                return cn.Query<TblDDTMefModel>(sql, new { N_DDT = nDDT }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero delle informazioni di un singolo DDT", ex);
            }
        }
        public static DataTable GetDDTForPDF(string dataInizio, string dataFine, string acquirente, string n_ddt)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            acquirente = "%" + acquirente + "%";
            n_ddt = "%" + n_ddt + "%";

            try
            {
                /* Senza Filtro */
                sql = "SELECT IdDDTMef, Anno, Data, N_DDT, CodArt, " +
                      "DescriCodArt, Qta, Importo, Acquirente, PrezzoUnitario, AnnoN_DDT " +
                      "FROM TblDDTMef " +
                      "WHERE (Data BETWEEN Convert(date,@pDataInizio) AND Convert(date,@pDataFine)) AND Acquirente LIKE @pAcquirente AND N_DDT LIKE @pN_DDT " +
                      "ORDER BY Data, N_DDT, CodArt";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pDataInizio", dataInizio));
                cmd.Parameters.Add(new SqlParameter("pDataFine", dataFine));
                cmd.Parameters.Add(new SqlParameter("pAcquirente", acquirente));
                cmd.Parameters.Add(new SqlParameter("pN_DDT", n_ddt));
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei DDT Mef per la stampa in PDF", ex);
            }
        }

        /*** Mostro la lista dei DDT in base ai campi compilati ***/
        public static List<TblDDTMefModel> searchFilter(TblDDTMefObject ddt)
        {
            List<TblDDTMefModel> retList = new List<TblDDTMefModel>();
            string sql = "";
            SqlDataReader dr = null;
            SqlConnection cn = GetConnection();
            DateTime emptyData = new DateTime();

            ddt.CodArt1 = "%" + ddt.CodArt1 + "%";
            ddt.CodArt2 = "%" + ddt.CodArt2 + "%";
            ddt.CodArt3 = "%" + ddt.CodArt3 + "%";
            ddt.DescriCodArt1 = "%" + ddt.DescriCodArt1 + "%";
            ddt.DescriCodArt2 = "%" + ddt.DescriCodArt2 + "%";
            ddt.DescriCodArt3 = "%" + ddt.DescriCodArt3 + "%";

            string queryFilters = "Qta LIKE @pQta AND N_DDT LIKE @pN_DDT " +
                                  "AND CodArt LIKE @pCodArt1 AND CodArt LIKE @pCodArt2 AND CodArt LIKE @pCodArt3 " +
                                  "AND DescriCodArt LIKE @pDescriCodArt1 AND DescriCodArt LIKE @pDescriCodArt2 AND DescriCodArt LIKE @pDescriCodArt3 ";

            try
            {
                /* Senza Filtro */
                sql = "SELECT IdDDTMef, Anno, Data, N_DDT, CodArt, " +
                      "DescriCodArt, Qta, Importo, Acquirente, PrezzoUnitario, AnnoN_DDT " +
                      "FROM TblDDTMef ";

                //Controllo i casi in cui entrambi gli anni o le date siano
                //state valorizzate, oppure quanto tutti quanti sono vuoti
                //altrimenti faccio una where generica per tutti gli altri casi
                if (ddt.AnnoInizio != "" && ddt.AnnoFine != "")
                {
                    sql += "WHERE (ANNO BETWEEN @pAnnoInizio AND @pAnnoFine) AND " + queryFilters;
                }
                else if (ddt.DataInizio != "" && ddt.DataFine != "")
                {
                    sql += "WHERE (Data BETWEEN CONVERT(Date,@pDataInizio) AND CONVERT(Date,@pDataFine)) AND " + queryFilters;
                }
                else if (ddt.AnnoInizio == "" && ddt.AnnoFine == "" && ddt.DataInizio == "" && ddt.DataFine == "")
                {
                    ddt.AnnoInizio = "%" + ddt.AnnoInizio + "%";
                    ddt.AnnoFine = "%" + ddt.AnnoFine + "%";
                    ddt.DataInizio = "2010-01-01";
                    ddt.DataFine = DateTime.Now.ToString();

                    sql += "WHERE " + queryFilters;
                }
                else
                {
                    sql += "WHERE ((ANNO = @pAnnoInizio OR Anno = @pAnnoFine) OR (Data = CONVERT(Date, @pDataInizio) OR Data = CONVERT(Date, @pDataFine))) AND " + queryFilters;
                }

                sql += "ORDER BY Anno, Data, N_DDT, CodArt ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pAnnoInizio", ddt.AnnoInizio));
                cmd.Parameters.Add(new SqlParameter("pAnnoFine", ddt.AnnoFine));
                cmd.Parameters.Add(new SqlParameter("pCodArt1", ddt.CodArt1));
                cmd.Parameters.Add(new SqlParameter("pCodArt2", ddt.CodArt2));
                cmd.Parameters.Add(new SqlParameter("pCodArt3", ddt.CodArt3));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt1", ddt.DescriCodArt1));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt2", ddt.DescriCodArt2));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt3", ddt.DescriCodArt3));

                if (ddt.Qta == "")
                    cmd.Parameters.Add(new SqlParameter("pQta", "%%"));
                else
                    cmd.Parameters.Add(new SqlParameter("pQta", ddt.Qta));

                if (ddt.NDdt == "")
                    cmd.Parameters.Add(new SqlParameter("pN_DDT", "%%"));
                else
                    cmd.Parameters.Add(new SqlParameter("pN_DDT", ddt.NDdt));

                if (ddt.DataInizio != "" && ddt.DataFine != "")
                {
                    cmd.Parameters.Add(new SqlParameter("pDataInizio", Convert.ToDateTime(ddt.DataInizio)));
                    cmd.Parameters.Add(new SqlParameter("pDataFine", Convert.ToDateTime(ddt.DataFine)));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("pDataInizio", ddt.DataInizio));
                    cmd.Parameters.Add(new SqlParameter("pDataFine", ddt.DataFine));
                }

                dr = cmd.ExecuteReader(); //Esegue il comando e lo inserisce nel DataReader

                while (dr.Read()) //Restituisce FALSE quando non ci sono più record da leggere
                {
                    TblDDTMefModel tmpTblDDTMefModel = new TblDDTMefModel();
                    tmpTblDDTMefModel.IdDDTMef = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    tmpTblDDTMefModel.Anno = (dr.IsDBNull(1) ? -1 : dr.GetInt32(1));
                    tmpTblDDTMefModel.Data = (dr.IsDBNull(2) ? emptyData : dr.GetDateTime(2));
                    tmpTblDDTMefModel.NDDT = (dr.IsDBNull(3) ? -1 : dr.GetInt32(3));
                    tmpTblDDTMefModel.CodArt = (dr.IsDBNull(4) ? null : dr.GetString(4));
                    tmpTblDDTMefModel.DescriCodArt = (dr.IsDBNull(5) ? null : dr.GetString(5));
                    tmpTblDDTMefModel.Qta = (dr.IsDBNull(6) ? -1 : dr.GetInt32(6));
                    tmpTblDDTMefModel.Importo = (dr.IsDBNull(7) ? -1m : dr.GetDecimal(7));
                    tmpTblDDTMefModel.Acquirente = (dr.IsDBNull(8) ? null : dr.GetString(8));
                    tmpTblDDTMefModel.PrezzoUnitario = (dr.IsDBNull(9) ? -1m : dr.GetDecimal(9));
                    tmpTblDDTMefModel.AnnoNDDT = (dr.IsDBNull(10) ? -1 : dr.GetInt32(10));
                    retList.Add(tmpTblDDTMefModel);
                }
                return retList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la ricerca dei DDT Mef", ex);
            }
        }

        /*** Media del prezzo unitario ***/
        public static decimal calcolaMediaPrezzoUnitario()
        {
            decimal media = 0m;
            string sql = "";
            SqlDataReader dr = null;
            SqlConnection cn = GetConnection();

            try
            {
                sql = "SELECT (SUM(PrezzoUnitario)) / (COUNT(PrezzoUnitario)) " +
                      "FROM TblDDTMef ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                dr = cmd.ExecuteReader(); //Esegue il comando e lo inserisce nel DataReader

                if (dr.Read())
                    media = (dr.IsDBNull(0) ? -1m : dr.GetDecimal(0));

                return media;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il calcolo della media dei prezzi unitari totali", ex);
            }
        }

        /*** Media dei prezzi con filtro ***/
        public static decimal calcolaMediaPrezzoUnitarioWithSearch(TblDDTMefObject ddt)
        {
            decimal media = 0m;
            string sql = "";
            SqlDataReader dr = null;
            SqlConnection cn = GetConnection();

            ddt.CodArt1 = "%" + ddt.CodArt1 + "%";
            ddt.CodArt2 = "%" + ddt.CodArt2 + "%";
            ddt.CodArt3 = "%" + ddt.CodArt3 + "%";
            ddt.DescriCodArt1 = "%" + ddt.DescriCodArt1 + "%";
            ddt.DescriCodArt2 = "%" + ddt.DescriCodArt2 + "%";
            ddt.DescriCodArt3 = "%" + ddt.DescriCodArt3 + "%";

            string queryFilters = "Qta LIKE @pQta AND N_DDT LIKE @pN_DDT " +
                                  "AND CodArt LIKE @pCodArt1 AND CodArt LIKE @pCodArt2 AND CodArt LIKE @pCodArt3 " +
                                  "AND DescriCodArt LIKE @pDescriCodArt1 AND DescriCodArt LIKE @pDescriCodArt2 AND DescriCodArt LIKE @pDescriCodArt3 ";

            try
            {
                sql = "SELECT (SUM(PrezzoUnitario)) / (COUNT(PrezzoUnitario)) " +
                        "FROM TblDDTMef ";

                if (ddt.AnnoInizio != "" && ddt.AnnoFine != "")
                {
                    sql += "WHERE (ANNO BETWEEN @pAnnoInizio AND @pAnnoFine) AND " + queryFilters;
                }
                else if (ddt.DataInizio != "" && ddt.DataFine != "")
                {
                    sql += "WHERE (Data BETWEEN CONVERT(Date,@pDataInizio) AND CONVERT(Date,@pDataFine)) AND " + queryFilters;
                }
                else if (ddt.AnnoInizio == "" && ddt.AnnoFine == "" && ddt.DataInizio == "" && ddt.DataFine == "")
                {
                    ddt.AnnoInizio = "%" + ddt.AnnoInizio + "%";
                    ddt.AnnoFine = "%" + ddt.AnnoFine + "%";
                    ddt.DataInizio = "2010-01-01";
                    ddt.DataFine = DateTime.Now.ToString();

                    sql += "WHERE " + queryFilters;
                }
                else
                {
                    sql += "WHERE ((ANNO = @pAnnoInizio OR Anno = @pAnnoFine) OR (Data = CONVERT(Date,@pDataInizio) OR Data = CONVERT(Date,@pDataFine))) AND " + queryFilters;
                }


                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pAnnoInizio", ddt.AnnoInizio));
                cmd.Parameters.Add(new SqlParameter("pAnnoFine", ddt.AnnoFine));
                cmd.Parameters.Add(new SqlParameter("pCodArt1", ddt.CodArt1));
                cmd.Parameters.Add(new SqlParameter("pCodArt2", ddt.CodArt2));
                cmd.Parameters.Add(new SqlParameter("pCodArt3", ddt.CodArt3));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt1", ddt.DescriCodArt1));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt2", ddt.DescriCodArt2));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt3", ddt.DescriCodArt3));

                if (ddt.Qta == "")
                    cmd.Parameters.Add(new SqlParameter("pQta", "%%"));
                else
                    cmd.Parameters.Add(new SqlParameter("pQta", ddt.Qta));

                if (ddt.NDdt == "")
                    cmd.Parameters.Add(new SqlParameter("pN_DDT", "%%"));
                else
                    cmd.Parameters.Add(new SqlParameter("pN_DDT", ddt.NDdt));

                if (ddt.DataInizio != "" && ddt.DataFine != "")
                {
                    cmd.Parameters.Add(new SqlParameter("pDataInizio", Convert.ToDateTime(ddt.DataInizio)));
                    cmd.Parameters.Add(new SqlParameter("pDataFine", Convert.ToDateTime(ddt.DataFine)));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("pDataInizio", ddt.DataInizio));
                    cmd.Parameters.Add(new SqlParameter("pDataFine", ddt.DataFine));
                }

                dr = cmd.ExecuteReader(); //Esegue il comando e lo inserisce nel DataReader

                if (dr.Read())
                    media = (dr.IsDBNull(0) ? -1m : dr.GetDecimal(0));

                return media;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il calcolo della media con filtro per Descrizione Codice Articolo", ex);
            }
        }

        public static List<TblDDTMefModel> GetDdtFromDBF(string pathFile, string acquirente, int idFornitore)
        {
            string excelConnectionString = "Provider = vfpoledb; Data Source = " + pathFile + "; Collating Sequence = machine";
            string commandText = "SELECT FTANNO, FTDT, FTNR, FTAFO, FTDEX1, FTQTA, FTPU FROM " + pathFile + "\\D_DDT.DBF";
            OleDbConnection ExcelConection = null;
            List<TblDDTMefModel> list = new List<TblDDTMefModel>();

            try
            {
                OleDbConnectionStringBuilder OleStringBuilder = new OleDbConnectionStringBuilder(excelConnectionString);
                OleStringBuilder.DataSource = pathFile;
                ExcelConection = new OleDbConnection();
                ExcelConection.ConnectionString = OleStringBuilder.ConnectionString;

                using (OleDbDataAdapter adaptor = new OleDbDataAdapter(commandText, ExcelConection))
                {
                    DataSet ds = new DataSet();
                    adaptor.Fill(ds);
                    ExcelConection.Open();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if (Convert.ToInt32(row.ItemArray[5]) != 0)
                        {
                            DateTime date = Convert.ToDateTime(row.ItemArray[1].ToString().Substring(0, 4) + "-" + row.ItemArray[1].ToString().Substring(4, 2) + "-" + row.ItemArray[1].ToString().Substring(6, 2));

                            decimal prezzoUnitario = Convert.ToDecimal(row.ItemArray[6]) / Convert.ToInt32(row.ItemArray[5].ToString() == "0" ? 1 : row.ItemArray[5]);
                            int annoN_ddt = Convert.ToInt32(row.ItemArray[0].ToString() + row.ItemArray[2].ToString());

                            TblDDTMefModel ddt = new TblDDTMefModel();
                            ddt.Anno = Convert.ToInt32(row.ItemArray[0]);
                            ddt.Data = date;
                            ddt.NDDT = Convert.ToInt32(row.ItemArray[2]);
                            ddt.CodArt = row.ItemArray[3].ToString().Trim();
                            ddt.DescriCodArt = row.ItemArray[4].ToString().Trim();
                            ddt.Qta = Convert.ToInt32(row.ItemArray[5]);
                            ddt.Importo = Convert.ToDecimal(row.ItemArray[6]);
                            ddt.Acquirente = acquirente;
                            ddt.PrezzoUnitario = prezzoUnitario;
                            ddt.AnnoNDDT = annoN_ddt;
                            ddt.IdFornitore = idFornitore;

                            list.Add(ddt);
                        }
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'importazione del DBF per il DDT MEF", ex);
            }
            finally
            {
            }
        }

        public static bool CheckIfDdtExistBetweenData(string nDdt, string dataInizio, string dataFine)
        {
            string sql = "";
            SqlConnection cn = GetConnection();

            try
            {
                sql = "SELECT N_DDT FROM TblDDTMef WHERE N_DDT = @nDdt AND Data BETWEEN CONVERT(date, @dataInizio) AND CONVERT(date, @dataFine) ";

                return cn.Query<bool>(sql, new { nDdt, dataInizio, dataFine }).ToList().Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il controllo della presenza di un N_DDT del TblDDTMefModel", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static bool InsertNewDdt(TblDDTMefModel ddt)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblTblDDTMefModel (Anno,Data,N_DDT,CodArt,DescriCodArt,Qta,Importo,Acquirente,PrezzoUnitario,AnnoN_DDT) " +
                      "VALUES (@Anno,@Data,@N_DDT,@CodArt,@DescriCodArt,@Qta,@Importo,@Acquirente,@PrezzoUnitario,@AnnoN_DDT)";

                int rows = cn.Execute(sql, ddt);

                if (rows > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un nuovo record per il TblDDTMefModel ", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }

        public static bool UpdateDdt()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE A " +
                      "SET A.Importo = B.Importo, A.PrezzoUnitario = B.PrezzoUnitario " +
                      "FROM TblDDTMef AS A " +
                      "INNER JOIN TblTblDDTMefModelTemp AS B ON A.Anno = B.Anno AND A.N_DDT = B.N_DDT AND A.CodArt = B.CodArt " +
                      "WHERE A.Qta = B.Qta AND A.Importo != B.Importo ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'aggiornamento di un record del TblDDTMefModel", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }

        public static List<TblDDTMefModel> GetNewDDT()
        {
            string sql = "";
            SqlConnection cn = GetConnection();

            try
            {
                sql = "SELECT A.IdDDTMef, A.Anno, A.Data, A.N_DDT, A.CodArt, A.DescriCodArt, A.Qta, A.Importo, A.Acquirente, A.PrezzoUnitario, A.AnnoN_DDT " +
                      "FROM TblDDTMefTemp AS A " +
                      "LEFT JOIN TblTblDDTMefModel AS B ON A.AnnoN_DDT = B.AnnoN_DDT " +
                      "WHERE B.IdDDTMef IS NULL ";

                return cn.Query<TblDDTMefModel>(sql).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei nuovi DDT da aggiungere all'anagrafica", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }

        // Metodi per la tabella DDT Temporanea
        public static bool InsertIntoDdtTemp(TblDDTMefModel ddt)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblTblDDTMefModelTemp (Anno,Data,N_DDT,CodArt,DescriCodArt,Qta,Importo,Acquirente,PrezzoUnitario,AnnoN_DDT) " +
                      "VALUES (@Anno,@Data,@N_DDT,@CodArt,@DescriCodArt,@Qta,@Importo,@Acquirente,@PrezzoUnitario,@AnnoN_DDT) ";

                int rows = cn.Execute(sql, ddt);

                if (rows > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un nuovo record nella tabella TblTblDDTMefModelTemp ", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static bool DeleteFromDdtTemp()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "DELETE FROM TblDDTMefTemp ";

                int rows = cn.Execute(sql);

                if (rows > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione della tabella TblTblDDTMefModelTemp ", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }

        // Calcolo dei totali DDT
        public static decimal GetTotalDDT()
        {
            decimal totale = 0m;
            string sql = "";
            SqlDataReader dr = null;
            SqlConnection cn = GetConnection();

            try
            {
                sql = "SELECT SUM(Importo) FROM TblDDTMef ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                dr = cmd.ExecuteReader(); //Esegue il comando e lo inserisce nel DataReader

                if (dr.Read())
                {
                    totale = (dr.IsDBNull(0) ? -1m : dr.GetDecimal(0));
                }

                return totale;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il calcolo del totale DDT senza filtri", ex);
            }
        }
        public static decimal GetTotalDDT(TblDDTMefObject ddt)
        {
            decimal totale = 0m;
            string sql = "";
            SqlDataReader dr = null;
            SqlConnection cn = GetConnection();

            ddt.CodArt1 = "%" + ddt.CodArt1 + "%";
            ddt.CodArt2 = "%" + ddt.CodArt2 + "%";
            ddt.CodArt3 = "%" + ddt.CodArt3 + "%";
            ddt.DescriCodArt1 = "%" + ddt.DescriCodArt1 + "%";
            ddt.DescriCodArt2 = "%" + ddt.DescriCodArt2 + "%";
            ddt.DescriCodArt3 = "%" + ddt.DescriCodArt3 + "%";

            string queryFilters = "Qta LIKE @pQta AND N_DDT LIKE @pN_DDT " +
                                  "AND CodArt LIKE @pCodArt1 AND CodArt LIKE @pCodArt2 AND CodArt LIKE @pCodArt3 " +
                                  "AND DescriCodArt LIKE @pDescriCodArt1 AND DescriCodArt LIKE @pDescriCodArt2 AND DescriCodArt LIKE @pDescriCodArt3 ";

            try
            {
                sql = "SELECT SUM(Importo) FROM TblDDTMef ";

                if (ddt.AnnoInizio != "" && ddt.AnnoFine != "")
                {
                    sql += "WHERE (ANNO BETWEEN @pAnnoInizio AND @pAnnoFine) AND " + queryFilters;
                }
                else if (ddt.DataInizio != "" && ddt.DataFine != "")
                {
                    sql += "WHERE (Data BETWEEN CONVERT(Date,@pDataInizio) AND CONVERT(Date,@pDataFine)) AND " + queryFilters;
                }
                else if (ddt.AnnoInizio == "" && ddt.AnnoFine == "" && ddt.DataInizio == "" && ddt.DataFine == "")
                {
                    ddt.AnnoInizio = "%" + ddt.AnnoInizio + "%";
                    ddt.AnnoFine = "%" + ddt.AnnoFine + "%";
                    ddt.DataInizio = "2010-01-01";
                    ddt.DataFine = DateTime.Now.ToString();

                    sql += "WHERE " + queryFilters;
                }
                else
                {
                    sql += "WHERE ((ANNO = @pAnnoInizio OR Anno = @pAnnoFine) OR (Data = CONVERT(Date,@pDataInizio) OR Data = CONVERT(Date,@pDataFine))) AND " + queryFilters;
                }


                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pAnnoInizio", ddt.AnnoInizio));
                cmd.Parameters.Add(new SqlParameter("pAnnoFine", ddt.AnnoFine));
                cmd.Parameters.Add(new SqlParameter("pCodArt1", ddt.CodArt1));
                cmd.Parameters.Add(new SqlParameter("pCodArt2", ddt.CodArt2));
                cmd.Parameters.Add(new SqlParameter("pCodArt3", ddt.CodArt3));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt1", ddt.DescriCodArt1));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt2", ddt.DescriCodArt2));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt3", ddt.DescriCodArt3));

                if (ddt.Qta.ToString() == "")
                    cmd.Parameters.Add(new SqlParameter("pQta", "%%"));
                else
                    cmd.Parameters.Add(new SqlParameter("pQta", ddt.Qta));

                if (ddt.NDdt.ToString() == "")
                    cmd.Parameters.Add(new SqlParameter("pN_DDT", "%%"));
                else
                    cmd.Parameters.Add(new SqlParameter("pN_DDT", ddt.NDdt));

                if (ddt.DataInizio != "" && ddt.DataFine != "")
                {
                    cmd.Parameters.Add(new SqlParameter("pDataInizio", Convert.ToDateTime(ddt.DataInizio)));
                    cmd.Parameters.Add(new SqlParameter("pDataFine", Convert.ToDateTime(ddt.DataFine)));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("pDataInizio", ddt.DataInizio));
                    cmd.Parameters.Add(new SqlParameter("pDataFine", ddt.DataFine));
                }

                dr = cmd.ExecuteReader(); //Esegue il comando e lo inserisce nel DataReader

                if (dr.Read())
                {
                    totale = (dr.IsDBNull(0) ? -1m : dr.GetDecimal(0));
                }

                return totale;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il calcolo del totale DDT con filtri", ex);
            }
        }

        // Calcolo dell'imponibile DDT
        public static decimal GetImponibileDDT()
        {
            decimal totale = 0m;
            string sql = "";
            SqlDataReader dr = null;
            SqlConnection cn = GetConnection();

            try
            {
                sql = "SELECT SUM((Importo * 100) / CONVERT(decimal(10,2), 122)) FROM TblDDTMef ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                dr = cmd.ExecuteReader(); //Esegue il comando e lo inserisce nel DataReader

                if (dr.Read())
                {
                    totale = (dr.IsDBNull(0) ? -1m : dr.GetDecimal(0));
                }

                return totale;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il calcolo del totale DDT senza filtri", ex);
            }
        }
        public static decimal GetImponibileDDT(TblDDTMefObject ddt)
        {
            decimal totale = 0m;
            string sql = "";
            SqlDataReader dr = null;
            SqlConnection cn = GetConnection();

            ddt.CodArt1 = "%" + ddt.CodArt1 + "%";
            ddt.CodArt2 = "%" + ddt.CodArt2 + "%";
            ddt.CodArt3 = "%" + ddt.CodArt3 + "%";
            ddt.DescriCodArt1 = "%" + ddt.DescriCodArt1 + "%";
            ddt.DescriCodArt2 = "%" + ddt.DescriCodArt2 + "%";
            ddt.DescriCodArt3 = "%" + ddt.DescriCodArt3 + "%";

            string queryFilters = "Qta LIKE @pQta AND N_DDT LIKE @pN_DDT " +
                                  "AND CodArt LIKE @pCodArt1 AND CodArt LIKE @pCodArt2 AND CodArt LIKE @pCodArt3 " +
                                  "AND DescriCodArt LIKE @pDescriCodArt1 AND DescriCodArt LIKE @pDescriCodArt2 AND DescriCodArt LIKE @pDescriCodArt3 ";

            try
            {
                sql = "SELECT SUM((Importo * 100) / CONVERT(decimal(10,2), 122)) FROM TblDDTMef ";

                if (ddt.AnnoInizio != "" && ddt.AnnoFine != "")
                {
                    sql += "WHERE (ANNO BETWEEN @pAnnoInizio AND @pAnnoFine) AND " + queryFilters;
                }
                else if (ddt.DataInizio != "" && ddt.DataFine != "")
                {
                    sql += "WHERE (Data BETWEEN CONVERT(Date,@pDataInizio) AND CONVERT(Date,@pDataFine)) AND " + queryFilters;
                }
                else if (ddt.AnnoInizio == "" && ddt.AnnoFine == "" && ddt.DataInizio == "" && ddt.DataFine == "")
                {
                    ddt.AnnoInizio = "%" + ddt.AnnoInizio + "%";
                    ddt.AnnoFine = "%" + ddt.AnnoFine + "%";
                    ddt.DataInizio = "2010-01-01";
                    ddt.DataFine = DateTime.Now.ToString();

                    sql += "WHERE " + queryFilters;
                }
                else
                {
                    sql += "WHERE ((ANNO = @pAnnoInizio OR Anno = @pAnnoFine) OR (Data = CONVERT(Date,@pDataInizio) OR Data = CONVERT(Date,@pDataFine))) AND " + queryFilters;
                }


                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pAnnoInizio", ddt.AnnoInizio));
                cmd.Parameters.Add(new SqlParameter("pAnnoFine", ddt.AnnoFine));
                cmd.Parameters.Add(new SqlParameter("pCodArt1", ddt.CodArt1));
                cmd.Parameters.Add(new SqlParameter("pCodArt2", ddt.CodArt2));
                cmd.Parameters.Add(new SqlParameter("pCodArt3", ddt.CodArt3));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt1", ddt.DescriCodArt1));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt2", ddt.DescriCodArt2));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt3", ddt.DescriCodArt3));

                if (ddt.Qta.ToString() == "")
                    cmd.Parameters.Add(new SqlParameter("pQta", "%%"));
                else
                    cmd.Parameters.Add(new SqlParameter("pQta", ddt.Qta));

                if (ddt.NDdt.ToString() == "")
                    cmd.Parameters.Add(new SqlParameter("pN_DDT", "%%"));
                else
                    cmd.Parameters.Add(new SqlParameter("pN_DDT", ddt.NDdt));

                if (ddt.DataInizio != "" && ddt.DataFine != "")
                {
                    cmd.Parameters.Add(new SqlParameter("pDataInizio", Convert.ToDateTime(ddt.DataInizio)));
                    cmd.Parameters.Add(new SqlParameter("pDataFine", Convert.ToDateTime(ddt.DataFine)));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("pDataInizio", ddt.DataInizio));
                    cmd.Parameters.Add(new SqlParameter("pDataFine", ddt.DataFine));
                }

                dr = cmd.ExecuteReader(); //Esegue il comando e lo inserisce nel DataReader

                if (dr.Read())
                {
                    totale = (dr.IsDBNull(0) ? -1m : dr.GetDecimal(0));
                }

                return totale;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il calcolo del totale DDT con filtri", ex);
            }
        }

        // Calcolo dell'iva DDT
        public static decimal GetIvaDDT()
        {
            decimal totale = 0m;
            string sql = "";
            SqlDataReader dr = null;
            SqlConnection cn = GetConnection();

            try
            {
                sql = "SELECT SUM(Importo - (100 * Importo / CONVERT(decimal(10,2),122))) FROM TblDDTMef ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                dr = cmd.ExecuteReader(); //Esegue il comando e lo inserisce nel DataReader

                if (dr.Read())
                {
                    totale = (dr.IsDBNull(0) ? -1m : dr.GetDecimal(0));
                }

                return totale;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il calcolo del totale DDT senza filtri", ex);
            }
        }
        public static decimal GetIvaDDT(TblDDTMefObject ddt)
        {
            decimal totale = 0m;
            string sql = "";
            SqlDataReader dr = null;
            SqlConnection cn = GetConnection();

            ddt.CodArt1 = "%" + ddt.CodArt1 + "%";
            ddt.CodArt2 = "%" + ddt.CodArt2 + "%";
            ddt.CodArt3 = "%" + ddt.CodArt3 + "%";
            ddt.DescriCodArt1 = "%" + ddt.DescriCodArt1 + "%";
            ddt.DescriCodArt2 = "%" + ddt.DescriCodArt2 + "%";
            ddt.DescriCodArt3 = "%" + ddt.DescriCodArt3 + "%";

            string queryFilters = "Qta LIKE @pQta AND N_DDT LIKE @pN_DDT " +
                                  "AND CodArt LIKE @pCodArt1 AND CodArt LIKE @pCodArt2 AND CodArt LIKE @pCodArt3 " +
                                  "AND DescriCodArt LIKE @pDescriCodArt1 AND DescriCodArt LIKE @pDescriCodArt2 AND DescriCodArt LIKE @pDescriCodArt3 ";

            try
            {
                sql = "SELECT SUM(Importo - (100 * Importo / CONVERT(decimal(10,2),122))) FROM TblDDTMef ";

                if (ddt.AnnoInizio != "" && ddt.AnnoFine != "")
                {
                    sql += "WHERE (ANNO BETWEEN @pAnnoInizio AND @pAnnoFine) AND " + queryFilters;
                }
                else if (ddt.DataInizio != "" && ddt.DataFine != "")
                {
                    sql += "WHERE (Data BETWEEN CONVERT(Date,@pDataInizio) AND CONVERT(Date,@pDataFine)) AND " + queryFilters;
                }
                else if (ddt.AnnoInizio == "" && ddt.AnnoFine == "" && ddt.DataInizio == "" && ddt.DataFine == "")
                {
                    ddt.AnnoInizio = "%" + ddt.AnnoInizio + "%";
                    ddt.AnnoFine = "%" + ddt.AnnoFine + "%";
                    ddt.DataInizio = "2010-01-01";
                    ddt.DataFine = DateTime.Now.ToString();

                    sql += "WHERE " + queryFilters;
                }
                else
                {
                    sql += "WHERE ((ANNO = @pAnnoInizio OR Anno = @pAnnoFine) OR (Data = CONVERT(Date,@pDataInizio) OR Data = CONVERT(Date,@pDataFine))) AND " + queryFilters;
                }


                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pAnnoInizio", ddt.AnnoInizio));
                cmd.Parameters.Add(new SqlParameter("pAnnoFine", ddt.AnnoFine));
                cmd.Parameters.Add(new SqlParameter("pCodArt1", ddt.CodArt1));
                cmd.Parameters.Add(new SqlParameter("pCodArt2", ddt.CodArt2));
                cmd.Parameters.Add(new SqlParameter("pCodArt3", ddt.CodArt3));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt1", ddt.DescriCodArt1));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt2", ddt.DescriCodArt2));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt3", ddt.DescriCodArt3));

                if (ddt.Qta.ToString() == "")
                    cmd.Parameters.Add(new SqlParameter("pQta", "%%"));
                else
                    cmd.Parameters.Add(new SqlParameter("pQta", ddt.Qta));

                if (ddt.NDdt.ToString() == "")
                    cmd.Parameters.Add(new SqlParameter("pN_DDT", "%%"));
                else
                    cmd.Parameters.Add(new SqlParameter("pN_DDT", ddt.NDdt));

                if (ddt.DataInizio != "" && ddt.DataFine != "")
                {
                    cmd.Parameters.Add(new SqlParameter("pDataInizio", Convert.ToDateTime(ddt.DataInizio)));
                    cmd.Parameters.Add(new SqlParameter("pDataFine", Convert.ToDateTime(ddt.DataFine)));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("pDataInizio", ddt.DataInizio));
                    cmd.Parameters.Add(new SqlParameter("pDataFine", ddt.DataFine));
                }

                dr = cmd.ExecuteReader(); //Esegue il comando e lo inserisce nel DataReader

                if (dr.Read())
                {
                    totale = (dr.IsDBNull(0) ? -1m : dr.GetDecimal(0));
                }

                return totale;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il calcolo del totale DDT con filtri", ex);
            }
        }
    }
}