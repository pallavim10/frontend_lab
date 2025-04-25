using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace CTMS
{
    public partial class RM_Risk_Details : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    if (Session["User_ID"] == null)
                    {
                        Response.Redirect("~/SessionExpired.aspx", false);
                    }

                    Session["TYPE"] = Request.QueryString["TYPE"]; //HttpUtility.HtmlDecode(Request.QueryString["TYPE"]);
                    Session["Risk_ID"] = Request.QueryString["RiskId"];// HttpUtility.HtmlDecode(Request.QueryString["RiskId"]);
                    if (!this.IsPostBack)
                    {
                        DataSet ds = new DataSet();
                        ds = dal.getDDLValue(Project_ID: Session["PROJECTID"].ToString(), SERVICE: "ISSUERISK", VARIABLENAME: "Impact");

                        lstRiskImpact.Items.Clear();
                        lstRiskImpact.DataSource = ds;
                        lstRiskImpact.DataTextField = "TEXT";
                        lstRiskImpact.DataValueField = "VALUE";
                        lstRiskImpact.DataBind();
                    }
                    getData();
                }
            }

            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        private void getData()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds = dal.getRiskList(Action: "Update", Id: Session["Risk_ID"].ToString());
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtRiskID.Text = ds.Tables[0].Rows[0]["RiskActualId"].ToString();
                    BindCategory();
                    ddlcategory.SelectedValue = ds.Tables[0].Rows[0]["Categoryid"].ToString();
                    BindSubcategory();
                    ddlsubcategory.SelectedValue = ds.Tables[0].Rows[0]["SubCategoryid"].ToString();
                    BindFactors();
                    ddlfactor.SelectedValue = ds.Tables[0].Rows[0]["Topicid"].ToString();
                    txtRiskCons.Text = ds.Tables[0].Rows[0]["Risk_Description"].ToString();                    
                    txtSugMitig.Text = ds.Tables[0].Rows[0]["Possible_Mitigations"].ToString();
                    txtSugRiskCat.Text = ds.Tables[0].Rows[0]["SugestedRiskCategory"].ToString();
                    if (ds.Tables[0].Rows[0]["RiskNature"].ToString() == "Dynamic")
                    {
                        radDynamic.Checked = true;
                    }
                    else
                    {
                        radStatic.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["KeyValue"].ToString() == "No")
                    {
                        radno.Checked = true;
                    }
                    else
                    {
                        radyes.Checked = true;
                    }
                    txtTransCat.Text = ds.Tables[0].Rows[0]["TranscelerateCategory"].ToString();

                    dt = dal.getRisk_Impact(Action: "GET_Risk_Impact", RISK_ID: Session["Risk_ID"].ToString(), VariableName: "Risk Impact", ProjectId: Session["PROJECTID"].ToString());
                    lstRiskImpact.ClearSelection();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string temp = dt.Rows[i]["Risk_Impacts"].ToString();
                            if (temp != "")
                            {
                                ListItem itm = lstRiskImpact.Items.FindByValue(temp);
                                if (itm != null)
                                    itm.Selected = true;
                            }
                        }
                    }
                    else
                    {
                        lstRiskImpact.ClearSelection();
                    }
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void bntSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                string rimpacts = "";
                foreach (ListItem item in lstRiskImpact.Items)
                {
                    if (item.Selected == true)
                    {
                        if (rimpacts.ToString() == "")
                        {
                            rimpacts = item.Value;
                        }
                        else
                        {
                            rimpacts += "," + item.Value;
                        }
                    }
                }

                string Rnature="";
                if(radDynamic.Checked==true)
                {
                Rnature="Dynamic";
                }
                else
                {
                Rnature="Static";
                }

                string keyvalue = "";
                if (radno.Checked == true)
                {
                    keyvalue = "No";
                }
                else
                {
                    keyvalue = "Yes";
                }

                DataSet ds = dal.UpdateRisk(
                id: Session["Risk_ID"].ToString(),
                desc:txtRiskCons.Text,
                impact:rimpacts,
                posmit:txtSugMitig.Text,
                sugriskcat:txtSugRiskCat.Text,
                transcat:txtTransCat.Text,
                nature:Rnature,
                category:ddlcategory.SelectedValue,
                subCat:ddlsubcategory.SelectedValue,
                factor:ddlfactor.SelectedValue,
                keyvalue:keyvalue
                    );

                //DELETE RISK IMPACT   
                dt = dal.getRisk_Impact(Action: "DELETE_RISK_Impact",
                RISK_ID: Session["Risk_ID"].ToString(), VariableName: "Risk Impact", ProjectId: Session["PROJECTID"].ToString());
                //INSERT RISK TYPE   
                foreach (ListItem item in lstRiskImpact.Items)
                {
                    if (item.Selected == true)
                    {
                        dt = dal.getRisk_Impact(Action: "INSERT_RISK_Impact",
                        RISK_ID: Session["Risk_ID"].ToString(),
                        RiskImpact: item.Value.Trim(),
                        ENTEREDBY: Session["User_ID"].ToString(),
                    VariableName: "Risk Impact",
                    ProjectId: Session["PROJECTID"].ToString());
                    }
                }
                Response.Write("<script> alert('Record Updated successfully.')</script>");
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindSubcategory();
                BindFactors();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlsubcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindFactors();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }



        public void BindCategory()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dt = dal.getcategory(Action: "Category");
                if (dt.Rows.Count > 1)
                {
                    ddlcategory.DataSource = dt;
                    ddlcategory.DataTextField = "Description";
                    ddlcategory.DataValueField = "id";
                    ddlcategory.DataBind();

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void BindSubcategory()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dt = dal.getcategory(Action: "SubCategory", Categoryvalue: ddlcategory.SelectedValue);
                if (dt.Rows.Count > 1)
                {
                    ddlsubcategory.DataSource = dt;
                    ddlsubcategory.DataTextField = "Description";
                    ddlsubcategory.DataValueField = "id";
                    ddlsubcategory.DataBind();

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void BindFactors()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dt = dal.getcategory(Action: "Factor", SubCategoryvalue: ddlsubcategory.SelectedValue, ProjectId: Session["PROJECTID"].ToString());
                if (dt.Rows.Count > 1)
                {
                    ddlfactor.DataSource = dt;
                    ddlfactor.DataTextField = "Description";
                    ddlfactor.DataValueField = "id";
                    ddlfactor.DataBind();
                }
                else
                {
                    DataTable dtf = new DataTable();
                    ddlfactor.DataSource = dtf;
                    ddlfactor.DataTextField = "Description";
                    ddlfactor.DataValueField = "id";
                    ddlfactor.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}