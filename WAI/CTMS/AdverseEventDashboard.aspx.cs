using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using PPT;

namespace CTMS
{
    public partial class AdverseEventDashboard : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                graphSiteWisemCRC();
                graphAdverseSevere();
                graphLifeThreatening();
                graphRelatedThreatening();
                graphRelatedSevere();
                graphAdverseSevereNSCLC();
                grpahAdverseThreateningNSCLC();
                graphAdvRelatedSevereNSCLC();
                graphInfusionMild();
                graphInfusionSevere();
                graphInfusionSevereNSCLC();
                graphInfusionThreatening();
                graphInfusionThreateningNSCLC();
                graphChemotherapySevere();
                graphChemotherapySevereNSCLC();
                graphChemotherapyThreatening();
                graphChemoThreateningNSCLC();
                graphSeriousSevere();
                graphSeriousSevereNSCLC();
                graphSeriousThreatening();
                graphSerThreateningNSCLC();
                graphSeriousDeath();
                graphSeriousDeathNSCLC();
                graphSeriousRelatedSevere();
                graphSerRelSevereNSCLC();
                graphSeriousRelatedThreatening();
                graphSerRelThreateningNSCLC();
                graphSerRelatedDeath();
                graphSerRelDeathNSCLC();
                dashboarddetail();
                if (!IsPostBack)
                {
                    graphfrequentmCRC("10");
                    graphfrequentNSCLC("10");
                    ddlfrequentnsclc.SelectedValue = "1";
                    ddlfrequentmcrc.SelectedValue = "1";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        public void graphfrequentNSCLC(string percent)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string freqnsclc = "";

                ds = dal.getMedicalMonitoringSummary(Action: "MostFrequentAEs_NSCLC", Project_ID: Session["PROJECTID"].ToString(), INVID: null, PERCENT: percent);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    freqnsclc += "{'" + dt.Columns[1].ToString() + "' : '" + dt.Rows[i][1].ToString() + "', '" + dt.Columns[3].ToString() + "' : " + dt.Rows[i][3].ToString() + "},";
                }

                hffrequentNSCLC.Value = "[" + freqnsclc.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void graphfrequentmCRC(string persent)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string freqmcrc = "";

                ds = dal.getMedicalMonitoringSummary(Action: "MostFrequentAEs_mCRC", Project_ID: Session["PROJECTID"].ToString(), INVID: null, PERCENT: persent);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    freqmcrc += "{'" + dt.Columns[1].ToString() + "' : '" + dt.Rows[i][1].ToString() + "', '" + dt.Columns[3].ToString() + "' : " + dt.Rows[i][3].ToString() + "},";
                }

                hffrequentmCRC.Value = "[" + freqmcrc.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void dashboarddetail()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                ds = dal.getMMwidget(Action: "TotalAE", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lbltotalevent.Text = dt.Rows[0]["TotalAE"].ToString();
                }
                else
                {
                    lbltotalevent.Text = "0";
                }
                ds = dal.getMMwidget(Action: "TotalSAE", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lbltotalse.Text = dt.Rows[0]["TotalSAE"].ToString(); ;
                }
                else
                {
                    lbltotalse.Text = "0";
                }

                ds = dal.getMMwidget(Action: "TotalDeaths", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lbldeath.Text = dt.Rows[0]["TotalDeaths"].ToString();
                }
                else
                {
                    lbldeath.Text = "0";

                }

                ds = dal.getMMwidget(Action: "TotalINFURN", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lblinfusion.Text = dt.Rows[0]["TotalINFURN"].ToString();
                }
                else
                {
                    lblinfusion.Text = "0";
                }

                ds = dal.mdicaldashgraph(Action: "AESevGrade3n4", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lblseverity3n4.Text = dt.Rows[0]["Count"].ToString();
                }
                else
                {
                    lblseverity3n4.Text = "0";
                }

                ds = dal.getMMwidget(Action: "AESpecialInterest", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lblsplint.Text = dt.Rows[0]["AESpecInt"].ToString();
                }
                else
                {
                    lblsplint.Text = "0";
                }

                ds = dal.getMMwidget(Action: "AERel", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lblrelatedaes.Text = dt.Rows[0]["Count"].ToString();
                }
                else
                {
                    lblrelatedaes.Text = "0";
                }

                ds = dal.mdicaldashgraph(Action: "AEOngoing", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lblongoingevents.Text = dt.Rows[0]["Count"].ToString();
                }
                else
                {
                    lblongoingevents.Text = "0";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void graphSerRelDeathNSCLC()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "SAERelatedDeath_NSCLC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfSerRelDeathNSCLC.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void graphSerRelatedDeath()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "SAERelatedDeath_mCRC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfSerRelatedDeath.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void graphSerRelThreateningNSCLC()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "SAERelatedLifeThreatening_NSCLC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfSerRelThreateningNSCLC.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void graphSeriousRelatedThreatening()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "SAERelatedLifeThreatening_mCRC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfSeriousRelatedThreatening.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void graphSerRelSevereNSCLC()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "SAERelatedSevere_NSCLC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfSerRelSevereNSCLC.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void graphSeriousRelatedSevere()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "SAERelatedSevere_mCRC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfSeriousRelatedSevere.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void graphSeriousDeathNSCLC()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "SAEDeath_NSCLC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfSeriousDeathNSCLC.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void graphSeriousDeath()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "SAEDeath_mCRC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value':'" + dt.Rows[i][3].ToString() + "'},";
                }
                hfSeriousDeath.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void graphSerThreateningNSCLC()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "SAELifeThreatening_NSCLC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfSerThreateningNSCLC.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void graphSeriousThreatening()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "SAELifeThreatening_mCRC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfSeriousThreatening.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void graphSeriousSevereNSCLC()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "SAESevere_NSCLC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfSeriousSevereNSCLC.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void graphSeriousSevere()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "SAESevere_mCRC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfSeriousSevere.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void graphChemoThreateningNSCLC()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "AEInfReacChemoLifeThreatening_NSCLC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfChemoThreateningNSCLC.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void graphChemotherapyThreatening()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "AEInfReacChemoLifeThreatening_mCRC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfChemotherapyThreatening.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void graphChemotherapySevereNSCLC()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "AEInfReacChemoSevere_NSCLC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfChemotherapySevereNSCLC.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void graphChemotherapySevere()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "AEInfReacChemoSevere_mCRC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfChemotherapySevere.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void graphInfusionThreateningNSCLC()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "AEInfReacLifeThreatening_NSCLC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfInfusionThreateningNSCLC.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void graphInfusionThreatening()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "AEInfReacLifeThreatening_mCRC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfInfusionThreatening.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void graphInfusionSevereNSCLC()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "AEInfReacSevere_NSCLC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfInfusionSevereNSCLC.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void graphInfusionSevere()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "AEInfReacSevere_mCRC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfInfusionSevere.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void graphInfusionMild()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "AEInfReacMild_mCRC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfInfusionMild.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void graphAdvRelatedSevereNSCLC()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "AERelatedSevere_NSCLC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfAdvRelatedSevereNSCLC.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void grpahAdverseThreateningNSCLC()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "LifeThreateningAEs_NSCLC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfAdverseThreateningNSCLC.Value = "[" + barinfo.TrimEnd(',') + "]";

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void graphAdverseSevereNSCLC()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "SevereAEs_NSCLC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }

                hfAdverseSevereNSCLC.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }



        public void graphSiteWisemCRC()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "Study_Status_Sitewise_mCRC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'Site' : " + dt.Rows[i][0].ToString() + ",'Registered' : " + dt.Rows[i][1].ToString() + ",'Screened' : " + dt.Rows[i][2].ToString() + ",'Screen Failure' : " + dt.Rows[i][3].ToString() + ",'Randomized' : " + dt.Rows[i][4].ToString() + ",'Termminated' : " + dt.Rows[i][5].ToString() + "},";
                }
                hfSiteWisemCRC.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void graphAdverseSevere()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "SevereAEs_mCRC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfAdverseSevere.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void graphLifeThreatening()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "LifeThreateningAEs_mCRC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfLifeThreatening.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void graphRelatedThreatening()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "AERelatedLifeThreatening_mCRC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "'},";
                }
                hfRelatedThreatening.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void graphRelatedSevere()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string barinfo = "";

                ds = dal.getMedicalMonitoringSummary(Action: "AERelatedSevere_mCRC", Project_ID: Session["PROJECTID"].ToString(), INVID: null);
                dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barinfo += "{ 'category': '" + dt.Rows[i][1].ToString() + "', 'value': '" + dt.Rows[i][3].ToString() + "},";
                }
                hfRelatedSevere.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void ddlfrequentmcrc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlfrequentmcrc.SelectedItem.Value != "0")
                {
                    graphfrequentmCRC(ddlfrequentmcrc.SelectedItem.Text);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }

        }

        protected void ddlfrequentnsclc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlfrequentnsclc.SelectedItem.Value != "0")
                {
                    graphfrequentNSCLC(ddlfrequentnsclc.SelectedItem.Text);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}