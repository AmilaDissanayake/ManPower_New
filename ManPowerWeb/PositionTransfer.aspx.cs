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
    public partial class PositionTransfer : System.Web.UI.Page
    {
        List<TransfersRetirementResignationMain> transfersRetirementResignationMainList = new List<TransfersRetirementResignationMain>();
        List<Employee> employeesList = new List<Employee>();
        List<Employee> employeesListDropDown = new List<Employee>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindDataSource();
            }
        }

        private void bindDataSource()
        {
            TransfersRetirementResignationMainController transfersRetirementResignationMainController = ControllerFactory.CreateTransfersRetirementResignationMainController();
            transfersRetirementResignationMainList = transfersRetirementResignationMainController.GetAllTransfersRetirementResignation(false);

            EmployeeController employeeController = ControllerFactory.CreateEmployeeController();
            employeesList = employeeController.GetAllEmployees();

            foreach (Employee employee in employeesList)
            {
                foreach (var item in transfersRetirementResignationMainList.Where(x => x.StatusId == 2 && x.RequestTypeId == 1))
                {
                    if (item.EmployeeId == employee.EmployeeId)
                    {
                        employeesListDropDown.Add(employee);
                    }
                }
            }

            ddlRetire.DataSource = employeesListDropDown;
            ddlRetire.DataTextField = "NameWithInitials";
            ddlRetire.DataValueField = "EmployeeId";
            ddlRetire.DataBind();

            ddlRetire.Items.Insert(0, new ListItem("Select Person Retired", ""));
        }

        protected void ddlRetire_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmployeeController employeeController = ControllerFactory.CreateEmployeeController();
            Employee employeeObject = new Employee();
            int employeeId = Convert.ToInt32(ddlRetire.SelectedValue);
            employeeObject = employeeController.GetEmployeeById(employeeId);



            SystemUserController systemUserController = ControllerFactory.CreateSystemUserController();
            List<SystemUser> systemUsers = new List<SystemUser>();
            systemUsers = systemUserController.GetAllSystemUser(true, false, false);

            //get retirment Employee Systemuser Id

            var retireEmployeeSystemUserDetails = systemUsers.Where(x => x.EmpNumber == employeeId).Single();
            int retireEmployeeSystemUserId = retireEmployeeSystemUserDetails.SystemUserId;

            ViewState["retireEmployeedepUnitPosId"] = retireEmployeeSystemUserDetails._DepartmentUnitPositions.DepartmetUnitPossitionsId;

            if (employeeObject.UnitType == 3)
            {
                systemUsers = systemUsers.Where(x => x._DepartmentUnitPositions.DepartmentUnitId == employeeObject.DSDivisionId && x.SystemUserId != retireEmployeeSystemUserId).ToList();

            }
            else
            {
                systemUsers = systemUsers.Where(x => x._DepartmentUnitPositions.DepartmentUnitId == employeeObject.DistrictId && x.SystemUserId != retireEmployeeSystemUserId).ToList();

            }


            ddlTransfer.DataSource = systemUsers;
            ddlTransfer.DataValueField = "SystemUserId";
            ddlTransfer.DataTextField = "Name";
            ddlTransfer.DataBind();

            ddlTransfer.Items.Insert(0, new ListItem("Select Employee ", ""));




        }

        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            DepartmentUnitPositionsController departmentUnitPositionsController = ControllerFactory.CreateDepartmentUnitPositionsController();
            int systemUserId = Convert.ToInt32(ddlTransfer.SelectedValue);
            int depUnitPosId = Convert.ToInt32(ViewState["retireEmployeedepUnitPosId"]);

            int response = departmentUnitPositionsController.UpdateSytemUserIdByDepartment_Unit_PositionId(systemUserId, depUnitPosId);

            if (response != 0)
            {

                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", string.Format("swal('Success!', 'Successfully Transferd!', 'success');window.setTimeout(function(){{window.location='PositionTransfer.aspx'}} ,2500);"), true);

            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", string.Format("swal('Success!', 'Successfully Transferd!', 'success');window.setTimeout(function(){{window.location='PositionTransfer.aspx'}} ,2500);"), true);
            }
        }
    }
}