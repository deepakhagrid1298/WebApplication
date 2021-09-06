using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication7.Models;

namespace WebApplication7.Service
{
    public class ManageProjectsEmpServices
    {
        public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

        private SqlDataAdapter sqlDataAdapter;
        private DataSet dataSet;

        public List<int> GetAllEmployeeByProjectId(int ProjectId)
        {
            List<int> EmployeeIds = new List<int>();
            var model = new ManageProjectsEmpModel();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ManageProject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "getAllEmployeeByProjectId");
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
                sqlDataAdapter = new SqlDataAdapter(cmd);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                if (dataSet.Tables.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        EmployeeIds.Add(Convert.ToInt32(dataSet.Tables[0].Rows[i]["EmployeeId"]));

                    }

                }
            }

            return EmployeeIds;
        }



        public IList<ManageProjectsEmpModel> GetAllProjectEmp()
        {
            IList<ManageProjectsEmpModel> getAllProjectEmp = new List<ManageProjectsEmpModel>();
            dataSet = new DataSet();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ManageProject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "getAllProjectEmployee");

                sqlDataAdapter = new SqlDataAdapter(cmd);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                if (dataSet.Tables.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        ManageProjectsEmpModel obj = new ManageProjectsEmpModel();
                        obj.ProjectId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["ProjectId"]);
                        obj.EMployeeId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["EMployeeId"]);

                        getAllProjectEmp.Add(obj);

                    }

                }
            }
            return getAllProjectEmp;
        }

    }
}