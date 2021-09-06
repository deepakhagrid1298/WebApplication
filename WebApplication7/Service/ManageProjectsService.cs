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
    public class ManageProjectsService
    {
        public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

        private SqlDataAdapter sqlDataAdapter;
        private DataSet dataSet;

        public IList<ManageProjectsModel> Getlist()
        {
            IList<ManageProjectsModel> getList = new List<ManageProjectsModel>();
            dataSet = new DataSet();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ManageRole", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "ProjectEmpList");
                sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataSet);
                if (dataSet.Tables.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        ManageProjectsModel obj = new ManageProjectsModel();
                        obj.Id = Convert.ToInt32(dataSet.Tables[0].Rows[i]["Id"]);
                        obj.Name = Convert.ToString(dataSet.Tables[0].Rows[i]["Name"]);
                        obj.StartDate = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["StartDate"]);
                        obj.EndDate = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["EndDate"]);
                        obj.Budget = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["Budget"]);
                        obj.EmpId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["EmpId"]);
                        obj.FirstName = Convert.ToString(dataSet.Tables[0].Rows[i]["FirstName"]);
                        obj.LastName = Convert.ToString(dataSet.Tables[0].Rows[i]["LastName"]);
                        obj.RoleId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["RoleId"]);
                        getList.Add(obj);

                    }

                }
            }

            return getList;
        }

        public void addEngineer(ManageProjectsModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ManageRole", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "ManageRole");
                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                cmd.Parameters.AddWithValue("@LastName", model.LastName);
                //cmd.Parameters.AddWithValue("@DOB", model.DOB);
                //cmd.Parameters.AddWithValue("@Contact", model.Contact);
                cmd.Parameters.AddWithValue("@RoleId", model.RoleId);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.ExecuteNonQuery();
            }

        }
    }
}