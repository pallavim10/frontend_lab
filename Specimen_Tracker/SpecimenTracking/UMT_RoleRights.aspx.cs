using SpecimenTracking.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SpecimenTracking
{
    public partial class UMT_RoleRights : System.Web.UI.Page
    {
        DAL_UMT dal_UMT = new DAL_UMT();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                GetLicenseSys();
            }
        }

        private void GetLicenseSys()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_ROLE_SP(ACTION: "GET_SYSTEMS");

                drpSystems.DataSource = ds;
                drpSystems.DataTextField = "SystemName";
                drpSystems.DataValueField = "SystemID";
                drpSystems.DataBind();
                drpSystems.Items.Insert(0, new ListItem("--Select System--", "0"));
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void lbtnGetRights_Click(object sender, EventArgs e)
        {
            try
            {
                if (drpSystems.SelectedValue != "0" && drpRoles.SelectedValue != "0")
                {
                    divFunction.Visible = true;
                    GET_RIGHTS();
                    GET_ADDED_RIGHTS();
                }
                else
                {
                    divFunction.Visible = false;
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public void GET_RIGHTS()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_ROLE_SP(
                                    ACTION: "GET_RIGHTS",
                                    SystemName: drpSystems.SelectedItem.Text,
                                    Parent: drpSystems.SelectedItem.Text,
                                    RoleID: drpRoles.SelectedValue
                                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdlevel1.DataSource = ds.Tables[0];
                    grdlevel1.DataBind();
                }

                else
                {
                    grdlevel1.DataSource = null;
                    grdlevel1.DataBind();

                }
            }

            catch (Exception ex)
            {
                
            }
        }

        private void GET_ADDED_RIGHTS()
        {
            try
            {
                DataSet ds = dal_UMT.UMT_ROLE_SP(
                                            ACTION: "GET_ADDED_RIGHTS",
                                            SystemName: drpSystems.SelectedItem.Text,
                                            Parent: drpSystems.SelectedItem.Text,
                                            RoleID: drpRoles.SelectedValue
                                            );
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lstLevel1.DataSource = ds.Tables[0];
                    lstLevel1.DataBind();
                }
                else
                {
                    lstLevel1.DataSource = null;
                    lstLevel1.DataBind();
                }
            }

            catch (Exception ex)
            {
               
            }
        }

        protected void drpSystems_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                divFunction.Visible = false;

                DataSet ds = dal_UMT.UMT_ROLE_SP(ACTION: "GET_UMT_SYS_ROLE", SystemID: drpSystems.SelectedValue);
                BIND_UMT_Roles(drpRoles, ds);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void BIND_UMT_Roles(DropDownList drpRoles, DataSet ds)
        {
            try
            {
                drpRoles.Items.Clear();
                drpRoles.DataSource = ds.Tables[0];
                drpRoles.DataValueField = "ID";
                drpRoles.DataTextField = "RoleName";
                drpRoles.DataBind();
                drpRoles.Items.Insert(0, new ListItem("--Select Roles--", "0"));
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void grdlevel1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string Function = dr["Name"].ToString();
                    string IsSelected = dr["IsSelected"].ToString();
                    CheckBox chkSeleect = (CheckBox)e.Row.FindControl("chkSeleect");

                    if (IsSelected == "True")
                    {
                        chkSeleect.Checked = true;
                    }
                    else
                    {
                        chkSeleect.Checked = false;
                    }


                    GridView grdlevel2 = (GridView)e.Row.FindControl("grdlevel2");
                    DataSet ds = dal_UMT.UMT_ROLE_SP(ACTION: "GET_RIGHTS", SystemName: drpSystems.SelectedItem.Text, Parent: Function, RoleID: drpRoles.SelectedValue);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdlevel2.DataSource = ds.Tables[0];
                        grdlevel2.DataBind();
                    }
                    else
                    {
                        HtmlControl anchor = (HtmlControl)e.Row.FindControl("anchor");
                        anchor.Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void grdlevel2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string Function = dr["Name"].ToString();
                    string IsSelected = dr["IsSelected"].ToString();
                    CheckBox chkSeleect = (CheckBox)e.Row.FindControl("chkSeleect");

                    if (IsSelected == "True")
                    {
                        chkSeleect.Checked = true;
                    }
                    else
                    {
                        chkSeleect.Checked = false;
                    }

                    GridView grdlevel3 = (GridView)e.Row.FindControl("grdlevel3");
                    DataSet ds = dal_UMT.UMT_ROLE_SP(ACTION: "GET_RIGHTS", SystemName: drpSystems.SelectedItem.Text, Parent: Function, RoleID: drpRoles.SelectedValue);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdlevel3.DataSource = ds.Tables[0];
                        grdlevel3.DataBind();
                    }
                    else
                    {
                        HtmlControl anchor = (HtmlControl)e.Row.FindControl("anchor");
                        anchor.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void grdlevel3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                string IsSelected = dr["IsSelected"].ToString();
                CheckBox chkSeleect = (CheckBox)e.Row.FindControl("chkSeleect");

                if (IsSelected == "True")
                {
                    chkSeleect.Checked = true;
                }
                else
                {
                    chkSeleect.Checked = false;
                }

                //GridView grdlevel3 = (GridView)e.Row.FindControl("grdlevel3");
                //DataSet ds = dal_UMT.UMT_ROLE_SP(ACTION: "GET_RIGHTS", SystemName: drpSystems.SelectedItem.Text, Parent: Function);
                //grdlevel3.DataSource = ds.Tables[0];
                //grdlevel3.DataBind();
            }
        }

        protected void lbtnAddFunctionName_Click(object sender, EventArgs e)
        {
            try
            {
                string RoleExists = CHECK_ROLE_RIGHTS_EXISTS();

                if (RoleExists != "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Selected functions are already assigned to Role " + RoleExists + ".');", true);
                }
                else
                {
                    INSER_ROLE_FUNC();
                    GET_ADDED_RIGHTS();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void INSER_ROLE_FUNC()
        {
            try
            {
                bool chkSeleect1 = false;
                bool chkSeleect2 = false;
                bool chkSeleect3 = false;
                for (int i = 0; i < grdlevel1.Rows.Count; i++)
                {
                    CheckBox chkSeleectLevel1 = (CheckBox)grdlevel1.Rows[i].FindControl("chkSeleect");
                    Label lblLevelID1 = (Label)grdlevel1.Rows[i].FindControl("lblID");

                    if (chkSeleectLevel1.Checked)
                    {
                        dal_UMT.UMT_ROLE_SP(
                                ACTION: "INSER_ROLE_FUNC",
                                    RoleID: drpRoles.SelectedValue,
                                    FuncID: lblLevelID1.Text
                                    );
                        chkSeleect1 = true;
                    }
                    else
                    {
                        dal_UMT.UMT_ROLE_SP(
                              ACTION: "DELETE_ROLE_FUNC",
                                  RoleID: drpRoles.SelectedValue,
                                  FuncID: lblLevelID1.Text
                                  );
                    }

                    GridView grdlevel2 = (GridView)grdlevel1.Rows[i].FindControl("grdlevel2");
                    for (int j = 0; j < grdlevel2.Rows.Count; j++)
                    {
                        CheckBox chkSeleectlevel2 = (CheckBox)grdlevel2.Rows[j].FindControl("chkSeleect");
                        Label lblLevelID2 = (Label)grdlevel2.Rows[j].FindControl("lblID");

                        if (chkSeleectlevel2.Checked)
                        {
                            dal_UMT.UMT_ROLE_SP(
                                    ACTION: "INSER_ROLE_FUNC",
                                        RoleID: drpRoles.SelectedValue,
                                        FuncID: lblLevelID2.Text
                                        );
                            chkSeleect2 = true;
                        }
                        else
                        {
                            dal_UMT.UMT_ROLE_SP(
                                  ACTION: "DELETE_ROLE_FUNC",
                                      RoleID: drpRoles.SelectedValue,
                                      FuncID: lblLevelID2.Text
                                      );
                        }

                        GridView grdlevel3 = (GridView)grdlevel2.Rows[j].FindControl("grdlevel3");
                        for (int k = 0; k < grdlevel3.Rows.Count; k++)
                        {
                            CheckBox chkSeleectLevel3 = (CheckBox)grdlevel3.Rows[k].FindControl("chkSeleect");
                            Label lblLevelID3 = (Label)grdlevel3.Rows[k].FindControl("lblID");

                            if (chkSeleectLevel3.Checked)
                            {
                                dal_UMT.UMT_ROLE_SP(
                                        ACTION: "INSER_ROLE_FUNC",
                                            RoleID: drpRoles.SelectedValue,
                                            FuncID: lblLevelID3.Text
                                            );
                                chkSeleect3 = true;
                            }
                            else
                            {
                                dal_UMT.UMT_ROLE_SP(
                                      ACTION: "DELETE_ROLE_FUNC",
                                          RoleID: drpRoles.SelectedValue,
                                          FuncID: lblLevelID3.Text
                                          );
                            }
                        }
                    }
                }

                if (chkSeleect1 == true || chkSeleect2 == true || chkSeleect3 == true)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User role rights added successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User role rights removed successfully.');", true);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private string CHECK_ROLE_RIGHTS_EXISTS()
        {
            string RoleName = "";
            try
            {
                string FuncIDS = "";

                for (int i = 0; i < grdlevel1.Rows.Count; i++)
                {
                    CheckBox chkSeleectLevel1 = (CheckBox)grdlevel1.Rows[i].FindControl("chkSeleect");
                    Label lblLevelID1 = (Label)grdlevel1.Rows[i].FindControl("lblID");

                    if (chkSeleectLevel1.Checked)
                    {
                        FuncIDS += "," + lblLevelID1.Text;
                    }

                    GridView grdlevel2 = (GridView)grdlevel1.Rows[i].FindControl("grdlevel2");
                    for (int j = 0; j < grdlevel2.Rows.Count; j++)
                    {
                        CheckBox chkSeleectlevel2 = (CheckBox)grdlevel2.Rows[j].FindControl("chkSeleect");
                        Label lblLevelID2 = (Label)grdlevel2.Rows[j].FindControl("lblID");

                        if (chkSeleectlevel2.Checked)
                        {
                            FuncIDS += "," + lblLevelID2.Text;
                        }

                        GridView grdlevel3 = (GridView)grdlevel2.Rows[j].FindControl("grdlevel3");
                        for (int k = 0; k < grdlevel3.Rows.Count; k++)
                        {
                            CheckBox chkSeleectLevel3 = (CheckBox)grdlevel3.Rows[k].FindControl("chkSeleect");
                            Label lblLevelID3 = (Label)grdlevel3.Rows[k].FindControl("lblID");

                            if (chkSeleectLevel3.Checked)
                            {
                                FuncIDS += "," + lblLevelID3.Text;
                            }
                        }
                    }
                }

                DataSet ds = dal_UMT.UMT_ROLE_SP(ACTION: "CHECK_ROLE_RIGHTS_EXISTS", FuncIDS: FuncIDS, RoleID: drpRoles.SelectedValue);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    RoleName = ds.Tables[0].Rows[0]["RoleName"].ToString();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return RoleName;
        }

        protected void lstLevel1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListView lstLevel2 = (ListView)e.Item.FindControl("lstLevel2");
            LinkButton lbtnName = (LinkButton)e.Item.FindControl("lbtnName");

            HtmlAnchor a1 = (HtmlAnchor)e.Item.FindControl("a1");
            HtmlGenericControl lisub = (HtmlGenericControl)e.Item.FindControl("lisub");
            HtmlControl i1 = (HtmlControl)e.Item.FindControl("i1");

            DataSet ds = dal_UMT.UMT_ROLE_SP(
                                            ACTION: "GET_ADDED_RIGHTS",
                                            SystemName: drpSystems.SelectedItem.Text,
                                            Parent: lbtnName.Text,
                                            RoleID: drpRoles.SelectedValue
                                            );

            lstLevel2.DataSource = ds.Tables[0];
            lstLevel2.DataBind();



            if (ds.Tables[0].Rows.Count > 0)
            {
                lisub.Attributes.Add("class", "treeview");
                i1.Attributes.Add("class", "fa fa-angle-left pull-right");
            }
        }

        protected void lstLevel2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListView lstLevel3 = (ListView)e.Item.FindControl("lstLevel3");
            LinkButton lbtnName = (LinkButton)e.Item.FindControl("lbtnName");

            HtmlAnchor a2 = (HtmlAnchor)e.Item.FindControl("a2");
            HtmlGenericControl li2 = (HtmlGenericControl)e.Item.FindControl("li2");
            HtmlControl i2 = (HtmlControl)e.Item.FindControl("i2");



            DataSet ds = dal_UMT.UMT_ROLE_SP(
                                            ACTION: "GET_ADDED_RIGHTS",
                                            SystemName: drpSystems.SelectedItem.Text,
                                            Parent: lbtnName.Text,
                                            RoleID: drpRoles.SelectedValue
                                            );

            lstLevel3.DataSource = ds.Tables[0];
            lstLevel3.DataBind();


            if (ds.Tables[0].Rows.Count > 0)
            {
                li2.Attributes.Add("class", "treeview");
                i2.Attributes.Add("class", "fa fa-angle-left pull-right");
            }
        }

        protected void lstLevel3_ItemDataBound(object sender, ListViewItemEventArgs e)
        {


            HtmlAnchor a3 = (HtmlAnchor)e.Item.FindControl("a3");
            HtmlGenericControl li3 = (HtmlGenericControl)e.Item.FindControl("li3");
            HtmlControl i3 = (HtmlControl)e.Item.FindControl("i3");

            ListView lstLevel4 = (ListView)e.Item.FindControl("lstLevel4");
            LinkButton lbtnName = (LinkButton)e.Item.FindControl("lbtnName");

            DataSet ds = dal_UMT.UMT_ROLE_SP(
                                            ACTION: "GET_ADDED_RIGHTS",
                                            SystemName: drpSystems.SelectedItem.Text,
                                            Parent: lbtnName.Text,
                                            RoleID: drpRoles.SelectedValue
                                            );

            lstLevel4.DataSource = ds.Tables[0];
            lstLevel4.DataBind();



            if (ds.Tables[0].Rows.Count > 0)
            {
                li3.Attributes.Add("class", "treeview");
                i3.Attributes.Add("class", "fa fa-angle-left pull-right");
            }
        }

        protected void lstLevel4_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

        }
    }
}