using SpecimenTracking.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpecimenTracking
{
    public partial class GENERATE_SHIPMENTMENIFEST : System.Web.UI.Page
    {
        DAL_MF Dal_MF = new DAL_MF();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GET_SPECIMENTYPE();
                    GET_VISIT();
                    GET_ALIQUOT();
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void GET_SPECIMENTYPE()
        {
            try
            {
                DataSet ds = Dal_MF.SHIPMENT_MENIFEST_SP(ACTION: "GET_SPECIMENTYPE");
                string ISACTIVE = ds.Tables[0].Rows[0]["ISACTIVE"].ToString();
                if (ISACTIVE == "True")
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                    {

                        DivSpeciType.Visible = true;
                        drpspecimentype.DataSource = ds.Tables[0];
                        drpspecimentype.DataValueField = "ID";
                        drpspecimentype.DataTextField = "OPTION_VALUE";
                        drpspecimentype.DataBind();

                        drpspecimentype.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                    else
                    {
                        DivSpeciType.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        private void GET_VISIT()
        {
            try
            {
                DataSet ds = Dal_MF.SHIPMENT_MENIFEST_SP(ACTION: "GET_VISIT");

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {

                    drpvisit.DataSource = ds.Tables[0];
                    drpvisit.DataValueField = "VISITNUM";
                    drpvisit.DataTextField = "VISITNAME";
                    drpvisit.DataBind();

                    drpvisit.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void GET_ALIQUOT()
        {
            try
            {
                DataSet ds = Dal_MF.SHIPMENT_MENIFEST_SP(ACTION: "GET_ALIQUOT");

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    drpAliquottype.DataSource = ds.Tables[0];
                    drpAliquottype.DataValueField = "ID";
                    drpAliquottype.DataTextField = "ALIQUOTTYPE";
                    drpAliquottype.DataBind();

                    drpAliquottype.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            CLEAR();
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            CLEAR();
        }

        private void CLEAR()
        {
            drpspecimentype.ClearSelection();
            drpAliquottype.ClearSelection();
            drpvisit.ClearSelection();
        }
    }
}