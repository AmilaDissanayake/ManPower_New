﻿using ManPowerCore.Common;
using ManPowerCore.Controller;
using ManPowerCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManPowerWeb
{
    public partial class PaymentVoucherCheckBy : System.Web.UI.Page
    {
        static List<PaymentVoucher> paymentVouchersList = new List<PaymentVoucher>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindDatasource();
            }
        }

        private void bindDatasource()
        {
            PaymentVoucherController paymentVoucherController = ControllerFactory.CreatePaymentVoucherController();

            paymentVouchersList = paymentVoucherController.GetAllPaymentVoucherWithSupplier(true);

            gvPaymentVoucher.DataSource = paymentVouchersList;
            gvPaymentVoucher.DataBind();
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pagesize = gvPaymentVoucher.PageSize;
            int pageindex = gvPaymentVoucher.PageIndex;
            rowIndex = (pagesize * pageindex) + rowIndex;

            PaymentVoucher paymentVoucher = new PaymentVoucher();
            paymentVoucher = paymentVouchersList[rowIndex];


            ViewState["Id"] = paymentVouchersList[rowIndex].Id;
            txtSupplier.Text = paymentVoucher.Supplier.Name;
            txtVNumber.Text = paymentVoucher.VoucherNumber;
            txtVDate.Text = paymentVoucher.VoucherDate.ToString("yyyy-MM-dd");
            txtPName.Text = paymentVoucher.PayeeName;
            txtPAddres.Text = paymentVoucher.PayeeAddress;
            txtChequeNumber.Text = paymentVoucher.ChequeNumber;
            txtTotalAmount.Text = paymentVoucher.TotalAmount.ToString();
            txtBankAcc.Text = paymentVoucher.BankAccount.ToString();
            if (paymentVoucher.BankBranch != null)
                txtBankBranch.Text = paymentVoucher.BankBranch.ToString();
            if (paymentVoucher.BankName != null)
                txtBankName.Text = paymentVoucher.BankName.ToString();

        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            PaymentVoucher paymentVoucher = new PaymentVoucher();
            paymentVoucher.Id = Convert.ToInt32(ViewState["Id"]);
            paymentVoucher.Status = 7;
            paymentVoucher.PayAuthorizedUser = Session["UserId"].ToString();
            paymentVoucher.PayAuthorizedDate = DateTime.Now;

            PaymentVoucherController paymentVoucherController = ControllerFactory.CreatePaymentVoucherController();
            int response = paymentVoucherController.UpdateStatus(paymentVoucher.Status, paymentVoucher.PayAuthorizedUser, paymentVoucher.PayAuthorizedDate, paymentVoucher.Id);


            if (response != 0)
            {

                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", string.Format("swal('Success!', 'Successfully Rejected!', 'success');window.setTimeout(function(){{window.location='PaymentVoucherCheckBy.aspx'}} ,2500);"), true);

            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", string.Format("swal('Error!', 'Something Went Wrong!', 'error');window.setTimeout(function(){{window.location='PaymentVoucherCheckBy.aspx'}} ,2500);"), true);
            }

        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            PaymentVoucher paymentVoucher = new PaymentVoucher();
            paymentVoucher.Id = Convert.ToInt32(ViewState["Id"]);
            paymentVoucher.Status = 8;
            paymentVoucher.PayAuthorizedUser = Session["UserId"].ToString();
            paymentVoucher.PayAuthorizedDate = DateTime.Now;

            PaymentVoucherController paymentVoucherController = ControllerFactory.CreatePaymentVoucherController();
            int response = paymentVoucherController.UpdateStatus(paymentVoucher.Status, paymentVoucher.PayAuthorizedUser, paymentVoucher.PayAuthorizedDate, paymentVoucher.Id);


            if (response != 0)
            {

                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", string.Format("swal('Success!', 'Successfully Approved!', 'success');window.setTimeout(function(){{window.location='PaymentVoucherCheckBy.aspx'}} ,2500);"), true);

            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", string.Format("swal('Error!', 'Something Went Wrong!', 'error');window.setTimeout(function(){{window.location='PaymentVoucherCheckBy.aspx'}} ,2500);"), true);
            }

        }
    }
}