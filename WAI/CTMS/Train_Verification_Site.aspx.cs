using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;

namespace CTMS
{
    public partial class Train_Verification_Site : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    btnsubmit.Visible = false;
                    bind_Questions();
                    bind_Labels();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Labels()
        {
            try
            {
                DataSet ds = dal.Training_SP(Action: "select_team", ID: Request.QueryString["Emp_ID"].ToString());
                lblTrainee.Text = ds.Tables[0].Rows[0]["Name"].ToString();

                DataSet ds1 = dal.Training_SP(Action: "select_TrainPlan_Site", ID: Request.QueryString["Plan_ID"].ToString());
                lblPlan.Text = ds1.Tables[0].Rows[0]["TrainingPlan"].ToString();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Questions()
        {
            try
            {
                DataSet ds = dal.Train_Verification_SP(Action: "get_Plan_Ques_Site", Plan_ID: Request.QueryString["Plan_ID"].ToString(), SubSection_IDs: Request.QueryString["SubSec"].ToString());
                gvSections.DataSource = ds.Tables[0];
                gvSections.DataBind();

                if (gvSections.Rows.Count > 0)
                {
                    btnsubmit.Visible = true;
                }
                else
                {
                    btnsubmit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvSections_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddl = (DropDownList)e.Row.FindControl("ddl");
                    TextBox txt = (TextBox)e.Row.FindControl("txt");
                    Repeater repeat_chk = (Repeater)e.Row.FindControl("repeat_chk");
                    Repeater repeat_rbtn = (Repeater)e.Row.FindControl("repeat_rbtn");

                    DataRowView dr = e.Row.DataItem as DataRowView;
                    string CONTROLTYPE = dr["CONTROLTYPE"].ToString();
                    string ID = dr["ID"].ToString();
                    string CLASS = dr["CLASS"].ToString();


                    if (CONTROLTYPE == "DROPDOWN")
                    {
                        ddl.Visible = true;
                        DataSet ds = dal.Train_Verification_SP(Action: "get_ddl_Site", QueNo: ID);
                        ddl.DataSource = ds.Tables[0];
                        ddl.DataValueField = "Item";
                        ddl.DataTextField = "Item";
                        ddl.DataBind();
                        ddl.Items.Insert(0, new ListItem("--Select--", "0"));
                        ddl.CssClass = CLASS;
                    }
                    else if (CONTROLTYPE == "CHECKBOX")
                    {
                        repeat_chk.Visible = true;
                        //CheckBox chk = (CheckBox)(repeat_chk.FindControl("chk"));
                        //chk.CssClass = CLASS;
                        DataSet ds = dal.Train_Verification_SP(Action: "get_ddl_Site", QueNo: ID);
                        repeat_chk.DataSource = ds.Tables[0];
                        repeat_chk.DataBind();
                    }
                    else if (CONTROLTYPE == "RADIOBUTTON")
                    {
                        repeat_rbtn.Visible = true;
                        //RadioButton rbtn = (RadioButton)(repeat_rbtn.FindControl("rbtn"));
                        //rbtn.CssClass = CLASS;
                        DataSet ds = dal.Train_Verification_SP(Action: "get_ddl_Site", QueNo: ID);
                        repeat_rbtn.DataSource = ds.Tables[0];
                        repeat_rbtn.DataBind();
                    }
                    else if (CONTROLTYPE == "TEXTBOX")
                    {
                        txt.Visible = true;
                        txt.CssClass = CLASS;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void check_Answers()
        {
            try
            {
                string data = "";
                int correct = 0;
                int incorrect = 0;

                for (int i = 0; i < gvSections.Rows.Count; i++)
                {
                    string ANS = ((Label)gvSections.Rows[i].FindControl("lblANS")).Text;
                    string CONTROLTYPE = ((Label)gvSections.Rows[i].FindControl("lblCONTROLTYPE")).Text;

                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        data = ((TextBox)gvSections.Rows[i].FindControl("txt")).Text.Trim();
                        if (ANS == data)
                        {
                            correct += 1;
                        }
                        else
                        {
                            incorrect += 1;
                        }
                    }
                    else if (CONTROLTYPE == "DROPDOWN")
                    {
                        data = ((DropDownList)gvSections.Rows[i].FindControl("ddl")).SelectedValue.Trim();
                        if (ANS == data)
                        {
                            correct += 1;
                        }
                        else
                        {
                            incorrect += 1;
                        }
                    }
                    else if (CONTROLTYPE == "CHECKBOX")
                    {
                        Repeater repeat_chk = gvSections.Rows[i].FindControl("repeat_chk") as Repeater;
                        for (int a = 0; a < repeat_chk.Items.Count; a++)
                        {
                            if (((CheckBox)repeat_chk.Items[a].FindControl("chk")).Checked == true)
                            {
                                if (data == "")
                                {
                                    data = ((CheckBox)repeat_chk.Items[a].FindControl("chk")).Text;
                                }
                                else
                                {
                                    data += "," + ((CheckBox)repeat_chk.Items[a].FindControl("chk")).Text;
                                }
                            }
                            if (ANS == data)
                            {
                                correct += 1;
                            }
                            else
                            {
                                incorrect += 1;
                            }

                        }
                    }
                    else if (CONTROLTYPE == "RADIOBUTTON")
                    {
                        Repeater repeat_rbtn = gvSections.Rows[i].FindControl("repeat_rbtn") as Repeater;
                        for (int a = 0; a < repeat_rbtn.Items.Count; a++)
                        {
                            if (((CheckBox)repeat_rbtn.Items[a].FindControl("rbtn")).Checked == true)
                            {
                                data = ((CheckBox)repeat_rbtn.Items[a].FindControl("rbtn")).Text.ToString();
                            }
                        }
                        if (ANS == data)
                        {
                            correct += 1;
                        }
                        else
                        {
                            incorrect += 1;
                        }
                    }
                }
                if (incorrect > correct)
                {
                    Response.Write("<script language='javascript'> alert('You have Scored " + correct + " Out of " + gvSections.Rows.Count + ". \\n ***Better Luck Next Time***'); window.location.href = 'Training_Site.aspx';</script>");
                }
                else
                {
                    string plan = Request.QueryString["Plan_ID"].ToString();
                    string emp = Request.QueryString["Emp_ID"].ToString();
                    string SubSec = Request.QueryString["SubSec"].ToString();
                    string[] values = SubSec.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();
                        dal.Train_Verification_SP(Action: "insert_Read_Site", Plan_ID: plan, SubSec_ID: values[i], Emp_ID: emp, Project_ID: Session["PROJECTID"].ToString());
                    }
                    Response.Write("<script language='javascript'> alert('***Congratulations*** \\n You have Scored " + correct + " Out of " + gvSections.Rows.Count + ".'); window.location.href = 'Training_Site.aspx';</script>");
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                check_Answers();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}