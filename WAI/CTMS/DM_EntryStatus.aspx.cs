using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;
using System.Drawing;


namespace CTMS
{
    public partial class DM_EntryStatus : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FillINV(); 
                    
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                throw;
            }
        }


        public void GetData(string Action = null, string INVID = null, string SUBJID = null)
        {
            try
            {
                ds = dal.EntryStatus(Action: Action, INVID: INVID, SUBJID: SUBJID);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        grdEntryStatus.DataSource = ds.Tables[0];
                        grdEntryStatus.DataBind();
                    }
                
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
                // throw;
            }
        }

      

        protected void grdEntryStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    int i;
                    for (i = 0; i < e.Row.Cells.Count; i++)
                    {
                        BoundField fieldName = (BoundField)((DataControlFieldCell)e.Row.Cells[i]).ContainingField;
                        e.Row.Cells[i].Attributes.Add("Style", "text-align: center ! important; ");

                        if (fieldName.HeaderText != "INVID" && fieldName.HeaderText != "SUBJID")
                        {
                            CheckBox myCheckBox = new CheckBox();
                            if (e.Row.Cells[i].Text == "0")
                            {
                                myCheckBox.Checked = false;
                            }
                            else if (e.Row.Cells[i].Text != "0")
                            {
                                myCheckBox.Checked = true;
                            }
                            myCheckBox.Enabled = false;
                            e.Row.Cells[i].Controls.Add(myCheckBox);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }

        }


        public void FillINV()
        {
            try
            {
                DAL dal;
                dal = new DAL();
                DataSet ds = dal.GET_INVID_SP();
                drpInvID.DataSource = ds.Tables[0];
                drpInvID.DataValueField = "INVNAME";
                drpInvID.DataBind();
                drpInvID.Items.Insert(0, new ListItem("--Select Inv ID--", "0"));
                FillSubject();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void FillSubject()
        {
            try
            {
                DataSet ds = dal.DM_ADD_UPDATE(ACTION: "GET_SUBJECT", PROJECTID: Session["PROJECTID"].ToString(), INVID: drpInvID.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpSubID.DataSource = ds.Tables[0];
                    drpSubID.DataValueField = "SUBJID";
                    drpSubID.DataTextField = "SUBJID";
                    drpSubID.DataBind();
                    drpSubID.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    drpSubID.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpInvID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSubject();
                grdEntryStatus.DataSource = null;
                grdEntryStatus.DataBind();
                if (drpInvID.SelectedValue != "0")
                {
                    GetData(drpIncompStatus.SelectedValue, drpInvID.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        protected void drpSubID_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData(drpIncompStatus.SelectedValue, drpInvID.SelectedValue, drpSubID.SelectedValue);
        }

        protected void drpIncompStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grdEntryStatus.DataSource = null;
                grdEntryStatus.DataBind();
                FillINV();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }
    }
}