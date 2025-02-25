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
    public partial class LeaveAllocation : System.Web.UI.Page
    {
        List<LeaveType> leaveTypeslist = new List<LeaveType>();
        List<Employee> employeesList = new List<Employee>();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;

            if (!IsPostBack)
            {
                bindDataSource();
            }
        }

        private void bindDataSource()
        {
            LeaveTypeController leaveTypeController = ControllerFactory.CreateLeaveTypeController();
            leaveTypeslist = leaveTypeController.GetAllLeaveTypes();
            ddlLeaveType.DataSource = leaveTypeslist;
            ddlLeaveType.DataValueField = "LeaveTypeId";
            ddlLeaveType.DataTextField = "Name";
            ddlLeaveType.DataBind();

            EmployeeController employeeController = ControllerFactory.CreateEmployeeController();
            employeesList = employeeController.GetAllEmployees();
            List<Employee> employeeGetNameList = new List<Employee>();

            foreach (var item in employeesList)
            {
                item.NameWithInitials = item.EmpInitials + " " + item.LastName;
            }

            ddlStaff.DataSource = employeesList;
            ddlStaff.DataValueField = "EmployeeId";
            ddlStaff.DataTextField = "NameWithInitials";
            ddlStaff.DataBind();

        }

        protected void btnSaveLeaveAllocation_Click(object sender, EventArgs e)
        {
            StaffLeaveAllocationController staffLeaveAllocationController = ControllerFactory.CreateStaffLeaveAllocationController();
            StaffLeaveAllocation staffLeaveAllocation = new StaffLeaveAllocation();


            staffLeaveAllocation.EmployeesID = Convert.ToInt32(ddlStaff.SelectedValue);
            staffLeaveAllocation.Entitlement = txtEntitlement.Text;
            //staffLeaveAllocation.LeaveYear = 10;
            staffLeaveAllocation.LeaveTypeId = Convert.ToInt32(ddlLeaveType.SelectedValue);
            // staffLeaveAllocation.NoOfDays = 10;
            staffLeaveAllocation.MonthLimit = float.Parse(txtPerMontLimit.Text);
            staffLeaveAllocation.MonthLimitAppliedTo = DateTime.Parse(txtAppliedTo.Text);

            int response = staffLeaveAllocationController.saveStaffLeaveAllocation(staffLeaveAllocation);

            if (response != 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Succesfully Allocated !', 'success');window.setTimeout(function(){window.location='LeaveAllocation.aspx'},2500);", true);
                //Response.Redirect(Request.RawUrl);

            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Failed!', 'Something Went Wrong!', 'error');window.setTimeout(function(){window.location='LeaveAllocation.aspx'},2500);", true);

            }


        }
    }
}