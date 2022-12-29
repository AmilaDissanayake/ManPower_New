﻿using ManPowerCore.Common;
using ManPowerCore.Domain;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManPowerCore.Infrastructure
{
    public interface StaffLeaveDAO
    {
        int saveStaffLeave(StaffLeave staffLeave, DBConnection dBConnection);

        List<StaffLeave> getStaffLeaves(DBConnection dbConnection);

        StaffLeave getStaffLeaveById(int id, DBConnection dbConnection);
    }
    public class StaffLeaveDAOSqlImpl : StaffLeaveDAO
    {
        public int saveStaffLeave(StaffLeave staffLeave, DBConnection dBConnection)
        {
            if (dBConnection.dr != null)
                dBConnection.dr.Close();

            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            dBConnection.cmd.CommandText = "INSERT INTO Staff_Leave (Day_Type_id,Leave_Type_id,Employee_ID,Leave_Date,Created_Date,Is_Half_Day,Leave_Status_Id,Reason_For_Leave,Resuming_Date,No_Of_Leave)" +
               " VALUES(@DayType,@LeaveTypeId,@EmpId,@LeaveDate,@CreatedDate,@IsHalfDay,@LeaveStatusId,@Reason,@ResumingDate,@NoLeaves);";

            dBConnection.cmd.Parameters.AddWithValue("@LeaveTypeId", staffLeave.LeaveTypeId);
            dBConnection.cmd.Parameters.AddWithValue("@NoLeaves", staffLeave.NoOfLeaves);
            dBConnection.cmd.Parameters.AddWithValue("@ResumingDate", staffLeave.ResumingDate);
            dBConnection.cmd.Parameters.AddWithValue("@Reason", staffLeave.ReasonForLeave);
            dBConnection.cmd.Parameters.AddWithValue("@LeaveDate", staffLeave.LeaveDate);
            dBConnection.cmd.Parameters.AddWithValue("@DayType", staffLeave.DayTypeId);
            dBConnection.cmd.Parameters.AddWithValue("@EmpId", staffLeave.EmployeeId);

            dBConnection.cmd.Parameters.AddWithValue("@CreatedDate", staffLeave.CreatedDate);
            dBConnection.cmd.Parameters.AddWithValue("@IsHalfDay", staffLeave.IsHalfDay);
            dBConnection.cmd.Parameters.AddWithValue("@LeaveStatusId", staffLeave.LeaveStatusId);



            return dBConnection.cmd.ExecuteNonQuery();

        }
        public List<StaffLeave> getStaffLeaves(DBConnection dbConnection)
        {
            if (dbConnection.dr != null)
                dbConnection.dr.Close();

            dbConnection.cmd.CommandText = "SELECT * FROM Staff_Leave";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            return dataAccessObject.ReadCollection<StaffLeave>(dbConnection.dr);
        }

        public StaffLeave getStaffLeaveById(int id, DBConnection dbConnection)
        {
            if (dbConnection.dr != null)
                dbConnection.dr.Close();

            dbConnection.cmd.CommandText = "SELECT * FROM Staff_Leave WHERE Id=" + id + "";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            return dataAccessObject.GetSingleOject<StaffLeave>(dbConnection.dr);
        }
    }
}
