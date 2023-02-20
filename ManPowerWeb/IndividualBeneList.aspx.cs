﻿using ManPowerCore.Common;
using ManPowerCore.Controller;
using ManPowerCore.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManPowerWeb
{
    public partial class IndividualBeneList : System.Web.UI.Page
    {
        List<InduvidualBeneficiary> beneficiaries = new List<InduvidualBeneficiary>();
        static List<InduvidualBeneficiary> beneficiariesFinalList = new List<InduvidualBeneficiary>();
        List<InduvidualBeneficiary> filtered = new List<InduvidualBeneficiary>();
        string[] gen = { "Male", "Female" };
        string[] scl = { "School", "Non School" };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataSource();
            }
        }

        private void BindDataSource()
        {

            ddlGen.DataSource = gen;
            ddlGen.DataBind();
            ddlGen.Items.Insert(0, new ListItem(""));

            ddlScl.DataSource = scl;
            ddlScl.DataBind();
            ddlScl.Items.Insert(0, new ListItem(""));

            InduvidualBeneficiaryController bc = ControllerFactory.CreateInduvidualBeneficiaryController();
            beneficiaries = bc.GetAllInduvidualBeneficiary(true);

            GridView1.DataSource = beneficiaries;
            GridView1.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            InduvidualBeneficiaryController bc = ControllerFactory.CreateInduvidualBeneficiaryController();
            beneficiaries = bc.GetAllInduvidualBeneficiary(true);

            if (dob.Text != "" && ddlGen.SelectedValue == "" && ddlScl.SelectedValue == "")
            {
                beneficiariesFinalList = beneficiaries.Where(u => u.DateOfBirth.Date == Convert.ToDateTime(dob.Text).Date).ToList();
                GridView1.DataSource = beneficiariesFinalList;
                GridView1.DataBind();
            }

            //----------------------------------------

            else if (dob.Text == "" && ddlGen.SelectedValue != "" && ddlScl.SelectedValue == "")
            {
                beneficiariesFinalList = beneficiaries.Where(u => u.BeneficiaryGender == ddlGen.SelectedValue).ToList();
                GridView1.DataSource = beneficiariesFinalList;
                GridView1.DataBind();
            }

            //-----------------------------------------

            else if (dob.Text == "" && ddlGen.SelectedValue == "" && ddlScl.SelectedValue != "")
            {
                if (ddlScl.SelectedValue.ToLower() == "school")
                {
                    beneficiariesFinalList = beneficiaries.Where(u => u.IsSchoolStudent == 1).ToList();
                    GridView1.DataSource = beneficiariesFinalList;
                }
                else if (ddlScl.SelectedValue.ToLower() == "non school")
                {
                    beneficiariesFinalList = beneficiaries.Where(u => u.IsSchoolStudent == 0).ToList();
                    GridView1.DataSource = beneficiariesFinalList;
                }

                GridView1.DataBind();
            }

            //------------------------------------------

            else if (dob.Text != "" && ddlGen.SelectedValue != "" && ddlScl.SelectedValue == "")
            {
                beneficiariesFinalList = beneficiaries.Where(u => u.DateOfBirth.Date == Convert.ToDateTime(dob.Text).Date && u.BeneficiaryGender == ddlGen.SelectedValue).ToList();
                GridView1.DataSource = beneficiariesFinalList;
                GridView1.DataBind();
            }

            //-------------------------------------------

            else if (dob.Text != "" && ddlGen.SelectedValue == "" && ddlScl.SelectedValue != "")
            {
                if (ddlScl.SelectedValue.ToLower() == "school")
                {
                    beneficiariesFinalList = beneficiaries.Where(u => u.IsSchoolStudent == 1 && u.DateOfBirth.Date == Convert.ToDateTime(dob.Text).Date).ToList();
                    GridView1.DataSource = beneficiariesFinalList;
                }
                else if (ddlScl.SelectedValue.ToLower() == "non school")
                {
                    beneficiariesFinalList = beneficiaries.Where(u => u.IsSchoolStudent == 0 && u.DateOfBirth.Date == Convert.ToDateTime(dob.Text).Date).ToList();
                    GridView1.DataSource = beneficiariesFinalList;
                }
                GridView1.DataBind();
            }

            //---------------------------------------------

            else if (dob.Text == "" && ddlGen.SelectedValue != "" && ddlScl.SelectedValue != "")
            {
                if (ddlScl.SelectedValue.ToLower() == "school")
                {
                    beneficiariesFinalList = beneficiaries.Where(u => u.IsSchoolStudent == 1 && u.BeneficiaryGender == ddlGen.SelectedValue).ToList();
                    GridView1.DataSource = beneficiariesFinalList;
                }
                else if (ddlScl.SelectedValue.ToLower() == "non school")
                {
                    beneficiariesFinalList = beneficiaries.Where(u => u.IsSchoolStudent == 0 && u.BeneficiaryGender == ddlGen.SelectedValue).ToList();
                    GridView1.DataSource = beneficiariesFinalList;
                }
                GridView1.DataBind();
            }

            //---------------------------------------------

            else if (dob.Text != "" && ddlGen.SelectedValue != "" && ddlScl.SelectedValue != "")
            {
                if (ddlScl.SelectedValue.ToLower() == "school")
                {
                    beneficiariesFinalList = beneficiaries.Where(u => u.IsSchoolStudent == 1 && u.DateOfBirth.Date == Convert.ToDateTime(dob.Text).Date && u.BeneficiaryGender == ddlGen.SelectedValue).ToList();
                    GridView1.DataSource = beneficiariesFinalList;
                }
                else if (ddlScl.SelectedValue.ToLower() == "non school")
                {
                    beneficiariesFinalList = beneficiaries.Where(u => u.IsSchoolStudent == 0 && u.DateOfBirth.Date == Convert.ToDateTime(dob.Text).Date && u.BeneficiaryGender == ddlGen.SelectedValue).ToList();
                    GridView1.DataSource = beneficiariesFinalList;
                }
                GridView1.DataBind();
            }

            if (beneficiariesFinalList.Count > 0)
            {
                btnRun.Visible = true;
            }
        }


        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (beneficiaries.Count > 0 || beneficiariesFinalList.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "Individual Beneficiary List" + DateTime.Now + ".xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                GridView1.GridLines = GridLines.Both;
                //tblTaSummary.HeaderStyle.Font.Bold = true;
                GridView1.RenderControl(htmltextwrtter);
                Response.Write(strwritter.ToString());
                Response.End();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindDataSource();
        }
    }
}