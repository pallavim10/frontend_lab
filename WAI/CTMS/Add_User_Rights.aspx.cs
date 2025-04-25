using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using CTMS;

namespace PPT
{
    public partial class Add_User_Rights : System.Web.UI.Page
    {
        DAL constr = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                fill_drpdwn();
                GETPARENTFUNCTIONS();
                fill_drpdwn_User_Group_ID();

            }
        }

        private void fill_drpdwn()
        {

            DAL dal;
            dal = new DAL();
            DataSet ds = dal.GetSetPROJECTDETAILS(
            Action: "Get_Specific_Project",
            Project_ID: Convert.ToInt32(Session["PROJECTID"]),
            ENTEREDBY: Session["User_ID"].ToString()
            );
            Drp_Project.DataSource = ds;
            Drp_Project.DataTextField = "PROJNAME";
            Drp_Project.DataValueField = "Project_ID";
            Drp_Project.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Drp_Project.Items.Insert(0, new ListItem("--Select Project--", "0"));
                Drp_Project.SelectedValue = ds.Tables[0].Rows[0]["Project_ID"].ToString();
            }
            else if (ds.Tables[0].Rows.Count > 1)
            {
                Drp_Project.Items.Insert(0, new ListItem("--Select Project--", "0"));
            }
            Drp_User_Group.Items.Insert(0, new ListItem("--Select User Group--", "0"));
            Drp_User.Items.Insert(0, new ListItem("--Select User--", "0"));

        }

        public void GETPARENTFUNCTIONS()
        {
            try
            {
                DataSet ds = constr.ManageUserGroups(ACTION: "GETFUNCTIONSNAME");
                ddlFunctions.DataSource = ds.Tables[0];
                ddlFunctions.DataTextField = "FunctionName";
                ddlFunctions.DataValueField = "FunctionID";
                ddlFunctions.DataBind();
                ddlFunctions.Items.Insert(0, new ListItem("--Select Function--", "0"));
            }

            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void fill_drpdwn_User_Group_ID()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con = new SqlConnection(constr.getconstr());
                SqlCommand cmd = new SqlCommand("Get_User_Group_ID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Project_ID", Drp_Project.SelectedValue);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                Drp_User_Group.DataSource = ds.Tables[0];
                Drp_User_Group.DataTextField = "UserGroup_Name";
                Drp_User_Group.DataValueField = "UserGroup_ID";
                Drp_User_Group.DataBind();
                con.Close();
                Drp_User_Group.Items.Insert(0, new ListItem("--Select User Group--", "0"));
            }

            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void fill_drpdwn_User_ID()
        {
            try
            {

                SqlConnection con = new SqlConnection();
                con = new SqlConnection(constr.getconstr());
                SqlCommand cmd = new SqlCommand("Get_User_ID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Project_ID", Drp_Project.SelectedValue);
                cmd.Parameters.AddWithValue("@UserGroup_ID", Drp_User_Group.SelectedValue);

                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                Drp_User.DataSource = ds.Tables[0];
                Drp_User.DataTextField = "User_Name";
                Drp_User.DataValueField = "User_ID";
                Drp_User.DataBind();
                con.Close();
                Drp_User.Items.Insert(0, new ListItem("--Select User--", "0"));
            }

            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void Drp_User_Group_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill_drpdwn_User_ID();
        }

        protected void Btn_Add_Fun_Click(object sender, EventArgs e)
        {
            try
            {
                Int16 i;

                SqlConnection con = new SqlConnection();
                con = new SqlConnection(constr.getconstr());
                SqlCommand cmd = new SqlCommand();
                SqlCommand cmd1 = new SqlCommand();
                CheckBox ChAction;
                string Fn_ID = "", Parent = "", Fn_Name = "";
                for (i = 0; i < GridView1.Rows.Count; i++)
                {
                    ChAction = (CheckBox)GridView1.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        Fn_ID = ((TextBox)GridView1.Rows[i].FindControl("txt_Fun_ID")).Text;

                        Parent = ((TextBox)GridView1.Rows[i].FindControl("txt_Parent")).Text;

                        Fn_Name = ((TextBox)GridView1.Rows[i].FindControl("txt_Fun_Name")).Text;

                        DataSet ds = constr.ManageUserGroups(ACTION: "InsertUserFunction", PROJECTID: Drp_Project.SelectedValue, User_ID: Drp_User.SelectedValue, FUNCTIONID: Fn_ID, Parent: Parent, ENTEREDBY: Session["User_ID"].ToString());
                    }
                    else
                    {
                        Fn_ID = ((TextBox)GridView1.Rows[i].FindControl("txt_Fun_ID")).Text;

                        Parent = ((TextBox)GridView1.Rows[i].FindControl("txt_Parent")).Text;

                        Fn_Name = ((TextBox)GridView1.Rows[i].FindControl("txt_Fun_Name")).Text;

                        DataSet ds = constr.ManageUserGroups(ACTION: "DELETEUSERFUNCTION", User_ID: Drp_User.SelectedValue, PROJECTID: Drp_Project.SelectedValue, FUNCTIONID: Fn_ID, ENTEREDBY: Session["User_ID"].ToString());
                    }
                    // put  value  in column ENTEREDBY  using GridView1,GridView2,GridView3 and GridView4 on page Adduser Rights.aspx where stored procedure ManageUserGroups where action is  'DELETEUSERFUNCTION' in Project WAI.
                    GridView GridView2 = (GridView)GridView1.Rows[i].FindControl("GridView2");
                    for (int j = 0; j < GridView2.Rows.Count; j++)
                    {
                        cmd = new SqlCommand("Add_Up_Del_User_Group_Fun", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        ChAction = (CheckBox)GridView2.Rows[j].FindControl("Chk_Sel_Fun");

                        if (ChAction.Checked)
                        {
                            Fn_ID = ((TextBox)GridView2.Rows[j].FindControl("txt_Fun_ID")).Text;

                            Parent = ((TextBox)GridView2.Rows[j].FindControl("txt_Parent")).Text;

                            Fn_Name = ((TextBox)GridView2.Rows[j].FindControl("txt_Fun_Name")).Text;

                            DataSet ds = constr.ManageUserGroups(ACTION: "InsertUserFunction", PROJECTID: Drp_Project.SelectedValue, User_ID: Drp_User.SelectedValue, FUNCTIONID: Fn_ID, Parent: Parent, ENTEREDBY: Session["User_ID"].ToString());
                        }
                        else
                        {
                            Fn_ID = ((TextBox)GridView2.Rows[j].FindControl("txt_Fun_ID")).Text;

                            Parent = ((TextBox)GridView2.Rows[j].FindControl("txt_Parent")).Text;

                            Fn_Name = ((TextBox)GridView2.Rows[j].FindControl("txt_Fun_Name")).Text;

                            DataSet ds = constr.ManageUserGroups(ACTION: "DELETEUSERFUNCTION", User_ID: Drp_User.SelectedValue, PROJECTID: Drp_Project.SelectedValue, FUNCTIONID: Fn_ID, ENTEREDBY: Session["User_ID"].ToString());
                        }

                        GridView GridView3 = (GridView)GridView2.Rows[j].FindControl("GridView3");
                        for (int k = 0; k < GridView3.Rows.Count; k++)
                        {
                            cmd = new SqlCommand("Add_Up_Del_User_Group_Fun", con);
                            cmd.CommandType = CommandType.StoredProcedure;

                            ChAction = (CheckBox)GridView3.Rows[k].FindControl("Chk_Sel_Fun");

                            if (ChAction.Checked)
                            {
                                Fn_ID = ((TextBox)GridView3.Rows[k].FindControl("txt_Fun_ID")).Text;

                                Parent = ((TextBox)GridView3.Rows[k].FindControl("txt_Parent")).Text;

                                Fn_Name = ((TextBox)GridView3.Rows[k].FindControl("txt_Fun_Name")).Text;

                                DataSet ds = constr.ManageUserGroups(ACTION: "InsertUserFunction", PROJECTID: Drp_Project.SelectedValue, User_ID: Drp_User.SelectedValue, FUNCTIONID: Fn_ID, Parent: Parent, ENTEREDBY: Session["User_ID"].ToString());
                            }
                            else
                            {
                                Fn_ID = ((TextBox)GridView3.Rows[k].FindControl("txt_Fun_ID")).Text;

                                Parent = ((TextBox)GridView3.Rows[k].FindControl("txt_Parent")).Text;

                                Fn_Name = ((TextBox)GridView3.Rows[k].FindControl("txt_Fun_Name")).Text;

                                DataSet ds = constr.ManageUserGroups(ACTION: "DELETEUSERFUNCTION", User_ID: Drp_User.SelectedValue, PROJECTID: Drp_Project.SelectedValue, FUNCTIONID: Fn_ID, ENTEREDBY: Session["User_ID"].ToString());
                            }

                            GridView GridView4 = (GridView)GridView3.Rows[k].FindControl("GridView4");
                            for (int l = 0; l < GridView4.Rows.Count; l++)
                            {
                                cmd = new SqlCommand("Add_Up_Del_User_Group_Fun", con);
                                cmd.CommandType = CommandType.StoredProcedure;

                                ChAction = (CheckBox)GridView4.Rows[l].FindControl("Chk_Sel_Fun");

                                if (ChAction.Checked)
                                {
                                    Fn_ID = ((TextBox)GridView4.Rows[l].FindControl("txt_Fun_ID")).Text;

                                    Parent = ((TextBox)GridView4.Rows[l].FindControl("txt_Parent")).Text;

                                    Fn_Name = ((TextBox)GridView4.Rows[l].FindControl("txt_Fun_Name")).Text;

                                    DataSet ds = constr.ManageUserGroups(ACTION: "InsertUserFunction", PROJECTID: Drp_Project.SelectedValue, User_ID: Drp_User.SelectedValue, FUNCTIONID: Fn_ID, Parent: Parent, ENTEREDBY: Session["User_ID"].ToString());
                                }
                                else
                                {
                                    Fn_ID = ((TextBox)GridView4.Rows[l].FindControl("txt_Fun_ID")).Text;

                                    Parent = ((TextBox)GridView4.Rows[l].FindControl("txt_Parent")).Text;

                                    Fn_Name = ((TextBox)GridView4.Rows[l].FindControl("txt_Fun_Name")).Text;

                                    DataSet ds = constr.ManageUserGroups(ACTION: "DELETEUSERFUNCTION", User_ID: Drp_User.SelectedValue, PROJECTID: Drp_Project.SelectedValue, FUNCTIONID: Fn_ID, ENTEREDBY: Session["User_ID"].ToString());
                                }

                            }

                        }

                    }

                }
                //Response.Write("<script> alert('Record Updated successfully.');window.location='Add_User_Rights.aspx'; </script>");
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Record Updated successfully'); window.location='Add_User_Rights.aspx';", true);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void btn_Get_Doc_List_Click(object sender, EventArgs e)
        {
            try
            {
                //SqlConnection con = new SqlConnection();
                //con = new SqlConnection(constr.getconstr());
                //SqlCommand cmd = new SqlCommand("Get_User_Functions", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@Project_Name", Drp_Project.SelectedValue);
                //cmd.Parameters.AddWithValue("@User_Name", Drp_User.SelectedValue);
                //SqlDataAdapter ad = new SqlDataAdapter(cmd);
                //DataSet ds = new DataSet();
                //ad.Fill(ds);

                DataSet ds = constr.ManageUserGroups(ACTION: "GETFUNCTIONSOFUSER", FUNCTIONNAME: ddlFunctions.SelectedItem.Text, PROJECTID: Drp_Project.SelectedValue, User_ID: Drp_User.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    divselectchk.Visible = true;
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    divselectchk.Visible = false;
                }

            }

            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void Drp_Project_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill_drpdwn_User_Group_ID();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string FunctionName = dr["FunctionName"].ToString();
                    string FUNCTIONTICK = dr["FUNCTIONTICK"].ToString();
                    CheckBox Chk_Sel_Fun = (CheckBox)e.Row.FindControl("Chk_Sel_Fun");

                    if (FUNCTIONTICK == "1")
                    {
                        Chk_Sel_Fun.Checked = true;
                    }
                    else
                    {
                        Chk_Sel_Fun.Checked = false;
                    }

                    GridView GridView2 = (GridView)e.Row.FindControl("GridView2");
                    DataSet ds = constr.ManageUserGroups(ACTION: "GETFUNCTIONSOFUSER", Parent: FunctionName, PROJECTID: Drp_Project.SelectedValue, UserGroupID: Drp_User_Group.SelectedValue, User_ID: Drp_User.SelectedValue);
                    GridView2.DataSource = ds.Tables[0];
                    GridView2.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string FunctionName = dr["FunctionName"].ToString();
                    string FUNCTIONTICK = dr["FUNCTIONTICK"].ToString();
                    CheckBox Chk_Sel_Fun = (CheckBox)e.Row.FindControl("Chk_Sel_Fun");

                    if (FUNCTIONTICK == "1")
                    {
                        Chk_Sel_Fun.Checked = true;
                    }
                    else
                    {
                        Chk_Sel_Fun.Checked = false;
                    }

                    GridView GridView3 = (GridView)e.Row.FindControl("GridView3");
                    DataSet ds = constr.ManageUserGroups(ACTION: "GETFUNCTIONSOFUSER", Parent: FunctionName, PROJECTID: Drp_Project.SelectedValue, UserGroupID: Drp_User_Group.SelectedValue, User_ID: Drp_User.SelectedValue);
                    GridView3.DataSource = ds.Tables[0];
                    GridView3.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string FunctionName = dr["FunctionName"].ToString();
                    string FUNCTIONTICK = dr["FUNCTIONTICK"].ToString();
                    CheckBox Chk_Sel_Fun = (CheckBox)e.Row.FindControl("Chk_Sel_Fun");

                    if (FUNCTIONTICK == "1")
                    {
                        Chk_Sel_Fun.Checked = true;
                    }
                    else
                    {
                        Chk_Sel_Fun.Checked = false;
                    }

                    GridView GridView4 = (GridView)e.Row.FindControl("GridView4");
                    DataSet ds = constr.ManageUserGroups(ACTION: "GETFUNCTIONSOFUSER", Parent: FunctionName, PROJECTID: Drp_Project.SelectedValue, UserGroupID: Drp_User_Group.SelectedValue, User_ID: Drp_User.SelectedValue);
                    GridView4.DataSource = ds.Tables[0];
                    GridView4.DataBind();


                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string FunctionName = dr["FunctionName"].ToString();
                    string FUNCTIONTICK = dr["FUNCTIONTICK"].ToString();
                    CheckBox Chk_Sel_Fun = (CheckBox)e.Row.FindControl("Chk_Sel_Fun");

                    if (FUNCTIONTICK == "1")
                    {
                        Chk_Sel_Fun.Checked = true;
                    }
                    else
                    {
                        Chk_Sel_Fun.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void Chk_Select_All_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Int16 i;
                CheckBox ChAction;
                if (Chk_Select_All.Checked)
                {
                    for (i = 0; i < GridView1.Rows.Count; i++)
                    {
                        ChAction = (CheckBox)GridView1.Rows[i].FindControl("Chk_Sel_Fun");
                        ChAction.Checked = true;

                        GridView GridView2 = (GridView)GridView1.Rows[i].FindControl("GridView2");
                        for (int j = 0; j < GridView2.Rows.Count; j++)
                        {
                            ChAction = (CheckBox)GridView2.Rows[j].FindControl("Chk_Sel_Fun");
                            ChAction.Checked = true;

                            GridView GridView3 = (GridView)GridView2.Rows[j].FindControl("GridView3");
                            for (int k = 0; k < GridView3.Rows.Count; k++)
                            {
                                ChAction = (CheckBox)GridView3.Rows[k].FindControl("Chk_Sel_Fun");
                                ChAction.Checked = true;

                                GridView GridView4 = (GridView)GridView3.Rows[k].FindControl("GridView4");
                                for (int l = 0; l < GridView4.Rows.Count; l++)
                                {
                                    ChAction = (CheckBox)GridView4.Rows[l].FindControl("Chk_Sel_Fun");
                                    ChAction.Checked = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (i = 0; i < GridView1.Rows.Count; i++)
                    {
                        ChAction = (CheckBox)GridView1.Rows[i].FindControl("Chk_Sel_Fun");
                        ChAction.Checked = false;

                        GridView GridView2 = (GridView)GridView1.Rows[i].FindControl("GridView2");
                        for (int j = 0; j < GridView2.Rows.Count; j++)
                        {
                            ChAction = (CheckBox)GridView2.Rows[j].FindControl("Chk_Sel_Fun");
                            ChAction.Checked = false;

                            GridView GridView3 = (GridView)GridView2.Rows[j].FindControl("GridView3");
                            for (int k = 0; k < GridView3.Rows.Count; k++)
                            {
                                ChAction = (CheckBox)GridView3.Rows[k].FindControl("Chk_Sel_Fun");
                                ChAction.Checked = false;

                                GridView GridView4 = (GridView)GridView3.Rows[k].FindControl("GridView4");
                                for (int l = 0; l < GridView4.Rows.Count; l++)
                                {
                                    ChAction = (CheckBox)GridView4.Rows[l].FindControl("Chk_Sel_Fun");
                                    ChAction.Checked = false;
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        //lbGetFunctionExport_Click
        protected void lbGetFunctionExport_Click(object sender, EventArgs e)
        {
            try
            {
                string headerName = "";
                if(ddlFunctions.SelectedItem.Text == "--Select Function--")
                {
                    headerName = "User Functions";
                }
                else
                {
                    if(Drp_User.SelectedItem.Text== "--Select User--")
                    {
                        headerName = "Assign User Function name  " + ddlFunctions.SelectedItem.Text;
                    }
                    else
                    {
                        headerName = "Assign User " + Drp_User.SelectedItem.Text + "  Right " + ddlFunctions.SelectedItem.Text;
                    }
                }
                Get_Assign_User_Rights(header: headerName, Action: "Get_Assign_UserRights",FUNCTION_NAME:ddlFunctions.SelectedItem.Text, UserID: Drp_User.SelectedValue);

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
        private void Get_Assign_User_Rights(string header = null, string Action = null, string PRODUCT_ID = null, string SUBCLASS_ID = null,string FUNCTION_NAME=null,string UserID=null)
        {
            try
            {
                DataSet ds = new DataSet();
                DAL dal = new DAL();
                ds = dal.Export_Data_SP(Action: Action, PRODUCTID: PRODUCT_ID, SUBCLASSID: SUBCLASS_ID,FUNCTIONNAME: FUNCTION_NAME, ENTEREDBY:UserID);

                ds.Tables[0].TableName = header;
                Multiple_Export_Excel.ToExcel(ds, header + ".xls", Page.Response);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}