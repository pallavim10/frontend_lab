using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;
using CTMS.CommonFunction;

namespace CTMS
{
    public partial class MM_LISTING_COMMENTS : System.Web.UI.Page
    {
        DAL dal = new DAL();
        DAL_MM dal_Mm = new DAL_MM();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_COMMENTS();

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

        private void GET_COMMENTS()
        {
            try
            {
                DataSet ds = new DataSet();
                if (Request.QueryString["SUB_TEST"] != null)
                {
                    if (Request.QueryString["TYPE"] != null)
                    {
                        ds = dal_Mm.MM_LAB_ADD_SP(ACTION: "GET_PR_LAB_CAT_COMMENTS", SUB_TEST: Request.QueryString["SUB_TEST"].ToString(), USERID: Session["User_ID"].ToString());

                        if (ds.Tables.Count > 1)
                        {
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                if (ds.Tables[1].Rows[0]["PEER_REVIEW"].ToString() == "True" && ds.Tables[1].Rows[0]["PEER_USER"].ToString() == Session["USER_ID"].ToString())
                                {
                                    divClosePeer.Visible = true;
                                }
                                else
                                {
                                    divClosePeer.Visible = false;
                                }
                            }
                            else
                            {
                                divClosePeer.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        ds = dal_Mm.MM_LAB_ADD_SP(ACTION: "GET_LAB_CAT_COMMENTS", SUB_TEST: Request.QueryString["SUB_TEST"].ToString(), USERID: Session["User_ID"].ToString());
                    }
                }
                else if (Request.QueryString["SUB_TEST_DAT"] != null)
                {
                    if (Request.QueryString["TYPE"] != null)
                    {
                        ds = dal_Mm.MM_LAB_ADD_SP(ACTION: "GET_PR_LAB_GRADE_COMMENTS", SUB_TEST_DAT: Request.QueryString["SUB_TEST_DAT"].ToString(), USERID: Session["User_ID"].ToString());

                        if (ds.Tables.Count > 1)
                        {
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                if (ds.Tables[1].Rows[0]["PEER_REVIEW"].ToString() == "True" && ds.Tables[1].Rows[0]["PEER_USER"].ToString() == Session["USER_ID"].ToString())
                                {
                                    divClosePeer.Visible = true;
                                }
                                else
                                {
                                    divClosePeer.Visible = false;
                                }
                            }
                            else
                            {
                                divClosePeer.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        ds = dal_Mm.MM_LAB_ADD_SP(ACTION: "GET_LAB_GRADE_COMMENTS", SUB_TEST_DAT: Request.QueryString["SUB_TEST_DAT"].ToString(), USERID: Session["User_ID"].ToString());
                    }
                }
                else
                {
                    if (Request.QueryString["TYPE"] != null)
                    {
                        ds = dal_Mm.MM_COMMENT_SP(ACTION: "GET_PR_COMMENTS", PVID: Request.QueryString["PVID"].ToString(), RECID: Request.QueryString["RECID"].ToString());

                        if (ds.Tables.Count > 1)
                        {
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                if (ds.Tables[1].Rows[0]["PEER_REVIEW"].ToString() == "True" && ds.Tables[1].Rows[0]["PEER_USER"].ToString() == Session["USER_ID"].ToString())
                                {
                                    divClosePeer.Visible = true;
                                }
                                else
                                {
                                    divClosePeer.Visible = false;
                                }
                            }
                            else
                            {
                                divClosePeer.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        ds = dal_Mm.MM_COMMENT_SP(ACTION: "GET_COMMENTS", PVID: Request.QueryString["PVID"].ToString(), RECID: Request.QueryString["RECID"].ToString());
                    }
                }

                grd.DataSource = ds.Tables[0];
                grd.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }



        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["SUB_TEST"] != null)
                {
                    if (Request.QueryString["TYPE"] != null)
                    {
                        string Type = "";
                        if (chkClosePeer.Checked == true)
                        {
                            Type = "Close";
                        }
                        else
                        {
                            Type = "Open";
                        }

                        dal_Mm.MM_LAB_ADD_SP(
                        ACTION: "INSERT_PR_LAB_CAT_COMMENTS",
                        SITE: Request.QueryString["INVID"].ToString(),
                        SUB_TEST: Request.QueryString["SUB_TEST"].ToString(),
                        SUBJID: Request.QueryString["SUBJID"].ToString(),
                        LAB_TEST: Request.QueryString["TEST"].ToString(),
                        USERID: Session["USER_ID"].ToString(),
                        COMMENTS: txtComment.Text,
                        QUERYTYPE: Type
                            );
                    }
                    else
                    {
                        dal_Mm.MM_LAB_ADD_SP(
                        ACTION: "INSERT_LAB_CAT_COMMENTS",
                        SITE: Request.QueryString["INVID"].ToString(),
                        SUB_TEST: Request.QueryString["SUB_TEST"].ToString(),
                        SUBJID: Request.QueryString["SUBJID"].ToString(),
                        LAB_TEST: Request.QueryString["TEST"].ToString(),
                        USERID: Session["USER_ID"].ToString(),
                        COMMENTS: txtComment.Text
                            );
                    }
                }
                else if (Request.QueryString["SUB_TEST_DAT"] != null)
                {
                    if (Request.QueryString["TYPE"] != null)
                    {
                        string Type = "";
                        if (chkClosePeer.Checked == true)
                        {
                            Type = "Close";
                        }
                        else
                        {
                            Type = "Open";
                        }

                        dal_Mm.MM_LAB_ADD_SP(
                        ACTION: "INSERT_PR_LAB_GRADE_COMMENTS",
                        SITE: Request.QueryString["INVID"].ToString(),
                        SUB_TEST_DAT: Request.QueryString["SUB_TEST_DAT"].ToString(),
                        SUBJID: Request.QueryString["SUBJID"].ToString(),
                        LAB_TEST: Request.QueryString["TEST"].ToString(),
                        VISITDAT: Request.QueryString["VISITDAT"].ToString(),
                        USERID: Session["USER_ID"].ToString(),
                        COMMENTS: txtComment.Text,
                        QUERYTYPE: Type
                            );
                    }
                    else
                    {
                        dal_Mm.MM_LAB_ADD_SP(
                        ACTION: "INSERT_LAB_GRADE_COMMENTS",
                        SITE: Request.QueryString["INVID"].ToString(),
                        SUB_TEST_DAT: Request.QueryString["SUB_TEST_DAT"].ToString(),
                        SUBJID: Request.QueryString["SUBJID"].ToString(),
                        LAB_TEST: Request.QueryString["TEST"].ToString(),
                        VISITDAT: Request.QueryString["VISITDAT"].ToString(),
                        USERID: Session["USER_ID"].ToString(),
                        COMMENTS: txtComment.Text
                            );
                    }
                }
                else
                {
                    if (Request.QueryString["TYPE"] != null)
                    {
                        string Type = "";
                        if (chkClosePeer.Checked == true)
                        {
                            Type = "Close";
                        }
                        else
                        {
                            Type = "Open";
                        }

                        dal_Mm.MM_COMMENT_SP(
                        ACTION: "INSERT_PR_COMMENTS",
                        PVID: Request.QueryString["PVID"].ToString(),
                        RECID: Request.QueryString["RECID"].ToString(),
                        LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                        SUBJID: Request.QueryString["SUBJID"].ToString(),
                        COMMENTS: txtComment.Text,
                        QUERYTYPE: Type
                            );
                    }
                    else
                    {
                        dal_Mm.MM_COMMENT_SP(
                        ACTION: "INSERT_COMMENTS",
                        PVID: Request.QueryString["PVID"].ToString(),
                        RECID: Request.QueryString["RECID"].ToString(),
                        LISTING_ID: Request.QueryString["LISTING_ID"].ToString(),
                        SUBJID: Request.QueryString["SUBJID"].ToString(),
                        COMMENTS: txtComment.Text
                            );
                    }
                }

                txtComment.Text = "";

                GET_COMMENTS();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.ToString();
            }
        }

    }
}