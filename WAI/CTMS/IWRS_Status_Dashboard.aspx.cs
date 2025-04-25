using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Web.UI.HtmlControls;

namespace CTMS
{
    public partial class IWRS_Status_Dashboard : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_IWRS dal_IWRS = new DAL_IWRS();
        CommonFunction.CommonFunction commFun = new CommonFunction.CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                if (!IsPostBack)
                {
                    DataSet ds = dal_IWRS.IWRS_SET_OPTION_SP(ACTION: "GET_QUE_ANS", QUECODE: "SUBJECTTEXT");
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Session["SUBJECTTEXT"] = ds.Tables[0].Rows[0]["ANS"].ToString();
                        }
                    }
                    else
                    {
                        Session["SUBJECTTEXT"] = "";
                    }

                    if (Session["IWRS_CurrentDate"] == null)
                    {
                        Session["IWRS_CurrentDate"] = commFun.GetCurrentDateTimeByTimezone().ToString("dd-MMM-yyyy");
                    }


                    Get_Sites();
                    GET_MainChart();
                    GET_OVERALL_GRAPH();
                    GET_TILES();
                    GET_GRAPHS();
                    GET_GRAPH_BAR();
                    GET_GRAPH_PIE();
                    Get_Dashboard();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Get_Sites()
        {
            try
            {
                DataSet dsINV = dal.GET_INVID_SP();
                lstSites.DataSource = dsINV.Tables[0];
                lstSites.DataValueField = "INVNAME";
                lstSites.DataBind();

                lstSites.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lstSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GET_MainChart();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_MainChart()
        {
            try
            {
                string barDATA = "", series = "";

                String[] numArr = new string[30] { "first", "second", "third", "fourth", "fifth", "sixth", "seventh", "eighth", "ninenth", "ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen", "Twenty",
                "Twenty-one", "Twenty-two", "Twenty-three", "Twenty-four", "Twenty-five", "Twenty-six", "Twenty-seven", "Twenty-eight", "Twenty-nine", "Thirty"};

                DataSet dsGraphs = dal_IWRS.IWRS_SET_DB_SP(ACTION: "GET_DASHBOARD_STATUS_GRAPH");

                if (dsGraphs.Tables.Count > 0 && dsGraphs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drGraph in dsGraphs.Tables[0].Rows)
                    {
                        series += drGraph["Header"].ToString() + ",";
                    }

                    hfSeries.Value = series.TrimEnd(',');

                    foreach (ListItem item in lstSites.Items)
                    {
                        if (item.Selected == true)
                        {
                            string innerDATA = "";

                            int num = 0;
                            foreach (DataRow drGraph in dsGraphs.Tables[0].Rows)
                            {
                                DataSet ds = dal_IWRS.IWRS_SET_DB_SP(ACTION: "GET_DASHBOARD_STATUS_ITEMS_SITE", ID: drGraph["ID"].ToString(), SITEID: item.Value.ToString(), ENTEREDBY: Session["User_ID"].ToString());
                                if (ds.Tables.Count > 0)
                                {
                                    innerDATA += "'" + numArr[num] + "': " + ds.Tables[0].Rows[0]["Count"].ToString() + ",";
                                }

                                num++;
                            }

                            barDATA += "{'category': '" + item.Value.ToString() + "', " + innerDATA.TrimEnd(',') + " },";
                        }
                    }
                    hfMainCHart.Value = "[" + barDATA.TrimEnd(',') + "]";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_TILES()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dal_IWRS.IWRS_SET_DB_SP(ACTION: "GET_DASHBOARD_STATUS", ENTEREDBY: Session["User_ID"].ToString());
                if (ds.Tables.Count > 0)
                {
                    lstStatusDashboard.DataSource = ds;
                    lstStatusDashboard.DataBind();
                }

                ds = dal_IWRS.IWRS_SET_DB_SP(ACTION: "GET_STATUS_IWRS_TILES", ENTEREDBY: Session["User_ID"].ToString());
                if (ds.Tables.Count > 0)
                {
                    lstTile.DataSource = ds;
                    lstTile.DataBind();
                }

                ds = dal_IWRS.IWRS_SET_DB_SP(ACTION: "GET_COLS_IWRS_TILES", ENTEREDBY: Session["User_ID"].ToString());
                if (ds.Tables.Count > 0)
                {
                    lstTileCols.DataSource = ds;
                    lstTileCols.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_GRAPHS()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_DB_SP(ACTION: "GET_STATUS_IWRS_GRAPHS", ENTEREDBY: Session["User_ID"].ToString());
                if (ds.Tables.Count > 0)
                {
                    lstGraph.DataSource = ds;
                    lstGraph.DataBind();
                }

                ds = dal_IWRS.IWRS_SET_DB_SP(ACTION: "GET_COLS_IWRS_GRAPHS", ENTEREDBY: Session["User_ID"].ToString());
                if (ds.Tables.Count > 0)
                {
                    lstGraphCols.DataSource = ds;
                    lstGraphCols.DataBind();
                }

                //ds = dal_IWRS.NIWRS_DASHOARD_SP(ACTION: "GET_DASHBOARD_STATUS_GRAPH", ENTEREDBY: Session["User_ID"].ToString());
                //if (ds.Tables.Count > 0)
                //{
                //    lstStatusGraph.DataSource = ds;
                //    lstStatusGraph.DataBind();
                //}

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_OVERALL_GRAPH()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_DB_SP(ACTION: "GET_OVERALLSTATUS_GRAPH", ENTEREDBY: Session["User_ID"].ToString());

                string barinfo = "";

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        barinfo += "{'INVID': '" + ds.Tables[0].Rows[i]["STATUSNAME"].ToString() + "', 'Count': " + ds.Tables[0].Rows[i]["Count"].ToString() + " },";
                    }
                }

                ds = dal_IWRS.IWRS_SET_DB_SP(ACTION: "GET_OVERALLCOLS_GRAPH", ENTEREDBY: Session["User_ID"].ToString());

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        barinfo += "{'INVID': '" + ds.Tables[0].Rows[i]["FIELDNAME"].ToString() + "', 'Count': " + ds.Tables[0].Rows[i]["Count"].ToString() + " },";
                    }
                }

                hfDataOverAllStatus.Value = "[" + barinfo.TrimEnd(',') + "]";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_GRAPH_BAR()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_DB_SP(ACTION: "GET_GRAPH_BAR", ENTEREDBY: Session["User_ID"].ToString());
                if (ds.Tables.Count > 0)
                {
                    lstBars.DataSource = ds;
                    lstBars.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GET_GRAPH_PIE()
        {
            try
            {
                DataSet ds = dal_IWRS.IWRS_SET_DB_SP(ACTION: "GET_GRAPH_PIE", ENTEREDBY: Session["User_ID"].ToString());
                if (ds.Tables.Count > 0)
                {
                    lstPies.DataSource = ds;
                    lstPies.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public int col = 0;

        protected void lstStatusDashboard_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                System.Web.UI.HtmlControls.HtmlGenericControl divBox = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divBox");
                int itemIndex = e.Item.DataItemIndex;

                Label lblCount = (Label)e.Item.FindControl("lblCount");
                DataRowView drv = (DataRowView)e.Item.DataItem;
                string ID = drv["ID"].ToString();
                DataSet ds = dal_IWRS.IWRS_SET_DB_SP(ACTION: "GET_DASHBOARD_STATUS_ITEMS", ID: ID, ENTEREDBY: Session["User_ID"].ToString());
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblCount.Text = ds.Tables[0].Rows[0]["COUNT"].ToString();
                }

                string[] color = { "small-box bg-red", "small-box bg-yellow", "small-box bg-aqua", "small-box bg-blue", "small-box bg-light-blue", "small-box bg-green", "small-box bg-navy", "small-box bg-teal", "small-box bg-olive", "small-box bg-lime", "small-box bg-orange", "small-box bg-fuchsia", "small-box bg-purple", "small-box bg-maroon" };
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    if (col == 13)
                    {
                        col = 0;
                    }
                    divBox.Attributes.Add("class", color[col]);
                    col++;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lstTile_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                System.Web.UI.HtmlControls.HtmlGenericControl divBox = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divBox");
                int itemIndex = e.Item.DataItemIndex;

                string[] color = { "small-box bg-red", "small-box bg-yellow", "small-box bg-aqua", "small-box bg-blue", "small-box bg-light-blue", "small-box bg-green", "small-box bg-navy", "small-box bg-teal", "small-box bg-olive", "small-box bg-lime", "small-box bg-orange", "small-box bg-fuchsia", "small-box bg-purple", "small-box bg-maroon" };
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    if (col == 13)
                    {
                        col = 0;
                    }
                    divBox.Attributes.Add("class", color[col]);
                    col++;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lstTileCols_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                System.Web.UI.HtmlControls.HtmlGenericControl divBox = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divBox");
                int itemIndex = e.Item.DataItemIndex;

                string[] color = { "small-box bg-red", "small-box bg-yellow", "small-box bg-aqua", "small-box bg-blue", "small-box bg-light-blue", "small-box bg-green", "small-box bg-navy", "small-box bg-teal", "small-box bg-olive", "small-box bg-lime", "small-box bg-orange", "small-box bg-fuchsia", "small-box bg-purple", "small-box bg-maroon" };
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    if (col == 13)
                    {
                        col = 0;
                    }
                    divBox.Attributes.Add("class", color[col]);
                    col++;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lstGraph_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                int itemIndex = e.Item.DataItemIndex;
                string barinfo = "";

                HiddenField hfData = (HiddenField)e.Item.FindControl("hfData");

                DataRowView drv = (DataRowView)e.Item.DataItem;
                string STATUSCODE = drv["STATUSCODE"].ToString();


                DataSet ds = dal_IWRS.IWRS_SET_DB_SP(ACTION: "GET_STATUS_IWRS_GRAPHS_DATA", STATUSCODE: STATUSCODE, ENTEREDBY: Session["User_ID"].ToString());
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        barinfo += "{'INVID': '" + ds.Tables[0].Rows[i]["INVID"].ToString() + "', 'Count': " + ds.Tables[0].Rows[i]["Count"].ToString() + " },";
                    }
                }
                hfData.Value = "[" + barinfo.TrimEnd(',') + "]";

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lstStatusGraph_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                int itemIndex = e.Item.DataItemIndex;
                string barinfo = "";

                HiddenField hfData = (HiddenField)e.Item.FindControl("hfData");

                Label lblCount = (Label)e.Item.FindControl("lblCount");
                DataRowView drv = (DataRowView)e.Item.DataItem;
                string ID = drv["ID"].ToString();

                DataSet dsINV = dal.GET_INVID_SP();

                foreach (DataRow drINV in dsINV.Tables[0].Rows)
                {
                    string INVID = drINV["INVNAME"].ToString();

                    DataSet ds = dal_IWRS.NIWRS_DASHOARD_SP(ACTION: "GET_DASHBOARD_STATUS_ITEMS_SITE", ID: ID, SITEID: INVID, ENTEREDBY: Session["User_ID"].ToString());
                    if (ds.Tables.Count > 0)
                    {
                        barinfo += "{'INVID': '" + drINV["INVNAME"].ToString() + "', 'Count': " + ds.Tables[0].Rows[0]["Count"].ToString() + " },";
                    }
                }

                hfData.Value = "[" + barinfo.TrimEnd(',') + "]";

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lstGraphCols_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                int itemIndex = e.Item.DataItemIndex;
                string barinfo = "";

                HiddenField hfData = (HiddenField)e.Item.FindControl("hfData");

                DataRowView drv = (DataRowView)e.Item.DataItem;
                string COL_NAME = drv["COL_NAME"].ToString();


                DataSet ds = dal_IWRS.IWRS_SET_DB_SP(ACTION: "GET_COLS_IWRS_GRAPHS_DATA", COL_NAME: COL_NAME, ENTEREDBY: Session["User_ID"].ToString());
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        barinfo += "{'INVID': '" + ds.Tables[0].Rows[i]["INVID"].ToString() + "', 'Count': " + ds.Tables[0].Rows[i]["Count"].ToString() + " },";
                    }
                }

                hfData.Value = "[" + barinfo.TrimEnd(',') + "]";

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lstBars_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                int itemIndex = e.Item.DataItemIndex;
                string barinfo = "";

                HiddenField hfBarData = (HiddenField)e.Item.FindControl("hfBarData");

                DataRowView drv = (DataRowView)e.Item.DataItem;
                string VARIABLENAME = drv["VARIABLENAME"].ToString();
                string TABLENAME = drv["TABLENAME"].ToString();


                DataSet ds = dal_IWRS.IWRS_SET_DB_SP(ACTION: "GET_GRAPH_DETAILS", TABLENAME: TABLENAME, VARIABLENAME: VARIABLENAME, ENTEREDBY: Session["User_ID"].ToString());

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        barinfo += "{'COLUMN': '" + ds.Tables[0].Rows[i]["Col"].ToString() + "', 'VALUE': " + ds.Tables[0].Rows[i]["Count"].ToString() + " },";
                    }
                }

                hfBarData.Value = "[" + barinfo.TrimEnd(',') + "]";

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        protected void lstPies_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                int itemIndex = e.Item.DataItemIndex;
                string barinfo = "";

                HiddenField hfPieData = (HiddenField)e.Item.FindControl("hfPieData");

                DataRowView drv = (DataRowView)e.Item.DataItem;
                string VARIABLENAME = drv["VARIABLENAME"].ToString();
                string TABLENAME = drv["TABLENAME"].ToString();


                DataSet ds = dal_IWRS.IWRS_SET_DB_SP(ACTION: "GET_GRAPH_DETAILS", TABLENAME: TABLENAME, VARIABLENAME: VARIABLENAME, ENTEREDBY: Session["User_ID"].ToString());

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        barinfo += "{'COLUMN': '" + ds.Tables[0].Rows[i]["Col"].ToString() + "', 'VALUE': " + ds.Tables[0].Rows[i]["Count"].ToString() + " },";
                    }
                }

                hfPieData.Value = "[" + barinfo.TrimEnd(',') + "]";

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Get_Dashboard()
        {
            try
            {
                DataSet ds1 = dal_IWRS.IWRS_SET_DB_SP(ACTION: "GET_Dashboard", User_ID: Session["User_ID"].ToString(), Project_ID: Session["PROJECTID"].ToString(), Section: "IWRS");
                repeatDashboard.DataSource = ds1.Tables[0];
                repeatDashboard.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void repeatDashboard_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    string uName = row["Usercontrol_Name"].ToString();
                    string Method = row["Method"].ToString();
                    string X = row["X"].ToString();
                    string Y = row["Y"].ToString();
                    string Width = row["Width"].ToString();
                    string Height = row["Height"].ToString();

                    HtmlGenericControl divMain = e.Item.FindControl("divMain") as HtmlGenericControl;
                    PlaceHolder placeHolder = e.Item.FindControl("placeHolder") as PlaceHolder;

                    if (X != null && X.ToString() != "")
                    {
                        divMain.Attributes.Remove("data-gs-x");
                        divMain.Attributes.Add("data-gs-x", X);
                    }
                    if (Y != null && Y.ToString() != "")
                    {
                        divMain.Attributes.Remove("data-gs-y");
                        divMain.Attributes.Add("data-gs-y", Y);
                    }
                    if (Width != null && Width.ToString() != "")
                    {
                        divMain.Attributes.Remove("data-gs-width");
                        divMain.Attributes.Add("data-gs-width", Width);
                    }
                    if (Height != null && Height.ToString() != "")
                    {
                        divMain.Attributes.Remove("data-gs-height");
                        divMain.Attributes.Add("data-gs-height", Height);
                    }

                    string usercontrolName = "~/Dashboard Charts/" + uName;
                    UserControl uc = (UserControl)Page.LoadControl(usercontrolName);
                    placeHolder.Controls.Add(uc);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}