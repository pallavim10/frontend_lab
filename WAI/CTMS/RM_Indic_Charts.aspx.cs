using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.IO;
using System.Data.OleDb;
using System.Globalization;

namespace CTMS
{
    public partial class RM_Indic_Charts : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string barinfo = "";
                    decimal percen = 0;

                    DataSet dsTrig = dal.Risk_Indicator_SP(Action: "GET_RM_INDIC_TRIGGER", ID: Request.QueryString["TILEID"].ToString());
                    lblGraphHeader.Text = dsTrig.Tables[0].Rows[0]["TileName"].ToString();

                    if (dsTrig.Tables[0].Rows[0]["Method"].ToString() == "Advance Metrics")
                    {
                        DataSet ds = dal.DM_LISTINGS_SP(Action: "GETADVANCEMETRICS", ID: dsTrig.Tables[0].Rows[0]["LISTID"].ToString());

                        string INVID = "", COUNTRYID = "";
                        if (Session["DASHBOARD_SITE"].ToString() == "All")
                        {
                            INVID = "0";
                        }
                        else
                        {
                            INVID = Session["DASHBOARD_SITE"].ToString();
                        }

                        if (Session["DASHBOARD_COUNTRYID"].ToString() == "All")
                        {
                            COUNTRYID = "0";
                        }
                        else
                        {
                            COUNTRYID = Session["DASHBOARD_COUNTRYID"].ToString();
                        }

                        DataSet dsNum = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_GRAPH", LISTING_ID: ds.Tables[0].Rows[0]["Num_List_ID"].ToString(), INVID: INVID, COUNTRYID: COUNTRYID);
                        DataSet dsDenom = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_GRAPH", LISTING_ID: ds.Tables[0].Rows[0]["Denom_List_ID"].ToString(), INVID: INVID, COUNTRYID: COUNTRYID);

                        if (Session["DASHBOARD_SITE"].ToString() == "All")
                        {
                            DataSet dtINVID = dal.DM_LISTINGS_SP(Action: "GETINVIDS", USERID: Session["USER_ID"].ToString(), COUNTRYID: Session["DASHBOARD_COUNTRYID"].ToString());

                            for (int k = 0; k < dtINVID.Tables[0].Rows.Count; k++)
                            {
                                for (int i = 0; i < dsNum.Tables[0].Rows.Count; i++)
                                {
                                    if (dtINVID.Tables[0].Rows[k]["INVID"].ToString() == dsNum.Tables[0].Rows[i]["INVID"].ToString())
                                    {
                                        for (int j = 0; j < dsDenom.Tables[0].Rows.Count; j++)
                                        {
                                            if (dsNum.Tables[0].Rows[i]["INVID"].ToString() == dsDenom.Tables[0].Rows[j]["INVID"].ToString())
                                            {
                                                percen = ((Convert.ToDecimal(dsNum.Tables[0].Rows[i]["Count"]) / Convert.ToDecimal(dsDenom.Tables[0].Rows[j]["Count"])) * 100);
                                            }
                                        }
                                    }
                                }

                                barinfo += "{'INVID': '" + dtINVID.Tables[0].Rows[k]["INVID"].ToString() + "', 'Count': " + String.Format("{0:0.00}", percen) + " },";
                                percen = 0;
                            }
                        }
                        else
                        {
                            if (dsNum.Tables[0].Rows.Count > 0)
                            {
                                if (dsDenom.Tables[0].Rows.Count > 0)
                                {
                                    percen = ((Convert.ToDecimal(dsNum.Tables[0].Rows[0]["Count"]) / Convert.ToDecimal(dsDenom.Tables[0].Rows[0]["Count"])) * 100);
                                }
                                else
                                {
                                    percen = 0;
                                }
                            }
                            else
                            {
                                percen = 0;
                            }

                            barinfo += "{'INVID': '" + INVID + "', 'Count': " + String.Format("{0:0.00}", percen) + " },";
                            percen = 0;
                        }
                    }
                    else
                    {
                        DataSet dsSite = dal.Dashboard_SP(
                        Action: "Get_Alerts_Sites",
                        User_ID: Session["User_ID"].ToString(),
                        INVID: "All",
                        COUNTRYID: "All"
                        );


                        foreach (DataRow dr in dsSite.Tables[0].Rows)
                        {
                            DataSet ds = new DataSet();

                            if (dsTrig.Tables[0].Rows[0]["LISTID"].ToString() != "")
                            {
                                ds = dal.DM_LISTINGS_SP(Action: "GETLISTDATA_TILE", LISTING_ID: dsTrig.Tables[0].Rows[0]["LISTID"].ToString(), INVID: dr["INVID"].ToString());
                            }
                            else
                            {
                                ds = dal.Dashboard_SP(Action: dsTrig.Tables[0].Rows[0]["Method"].ToString(), Project_ID: Session["PROJECTID"].ToString(),
                                INVID: dr["INVID"].ToString(), User_ID: Session["User_ID"].ToString());
                            }

                            DataTable dt = new DataTable();
                            string Val = "";

                            if (dsTrig.Tables[0].Rows[0]["Method"].ToString() != "Advance Metrics")
                            {
                                if (ds.Tables.Count != 0)
                                {
                                    dt = ds.Tables[0];
                                    if (dt.Rows.Count > 0)
                                    {
                                        Val = dt.Rows[0][0].ToString();
                                    }
                                    else
                                    {
                                        Val = "0";
                                    }
                                }
                                else
                                {
                                    Val = "0";
                                }
                            }
                            else
                            {
                                Val = String.Format("{0:0.00}", 0);
                            }

                            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0][0].ToString() != "")
                                {
                                    barinfo += "{'INVID': '" + dr["INVID"].ToString() + "', 'Count': " + Val + " },";
                                }
                                else
                                {
                                    barinfo += "{'INVID': '" + dr["INVID"].ToString() + "', 'Count': " + Val + " },";
                                }
                            }
                            else
                            {
                                barinfo += "{'INVID': '" + dr["INVID"].ToString() + "', 'Count': " + Val + " },";
                            }
                        }
                    }

                    hfData.Value = "[" + barinfo.TrimEnd(',') + "]";
                }
            }
            catch (Exception ex)
            {
                lblGraphHeader.Text = ex.Message.ToString();
            }
        }
    }
}