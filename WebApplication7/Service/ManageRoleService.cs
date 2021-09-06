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
    public class ManageRoleService
    {
        public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

        private SqlDataAdapter sqlDataAdapter;
        private DataSet dataSet;

        public IList<ManageRoleModel> GetRoles()
        {
            IList<ManageRoleModel> getEmpList = new List<ManageRoleModel>();
            dataSet = new DataSet();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ManageRole", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "RoleDetails");
                sqlDataAdapter = new SqlDataAdapter(cmd);

                sqlDataAdapter.Fill(dataSet);
                if (dataSet.Tables.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        ManageRoleModel obj = new ManageRoleModel();
                        obj.Id = Convert.ToInt32(dataSet.Tables[0].Rows[i]["Id"]);
                        obj.FirstName = Convert.ToString(dataSet.Tables[0].Rows[i]["FirstName"]);
                        obj.LastName = Convert.ToString(dataSet.Tables[0].Rows[i]["LastName"]);
                        obj.DOB = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["DOB"]);
                        obj.Contact = (long)Convert.ToInt64(dataSet.Tables[0].Rows[i]["Contact"]);
                        obj.RoleId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["RoleId"]);
                        obj.Name = Convert.ToString(dataSet.Tables[0].Rows[i]["Name"]);
                        getEmpList.Add(obj);

                    }

                }
            }
            return getEmpList;
        }

        public ManageRoleModel GetEmployeeById(int Id)
        {
            var model = new ManageRoleModel();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ManageRole", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "GetRoleById");
                cmd.Parameters.AddWithValue("@Id", Id);
                sqlDataAdapter = new SqlDataAdapter(cmd);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);

                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    model.Id = Convert.ToInt32(dataSet.Tables[0].Rows[0]["Id"]);
                    model.FirstName = Convert.ToString(dataSet.Tables[0].Rows[0]["FirstName"]);
                    model.LastName = Convert.ToString(dataSet.Tables[0].Rows[0]["LastName"]);
                    model.DOB = Convert.ToDateTime(dataSet.Tables[0].Rows[0]["DOB"]);
                    model.Contact = (long)Convert.ToInt64(dataSet.Tables[0].Rows[0]["Contact"]);
                    model.RoleId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["RoleId"]);
                    model.Name = Convert.ToString(dataSet.Tables[0].Rows[0]["Name"]);
                }
            }
            return model;
        }


        public void ManageRole(ManageRoleModel model)
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
                cmd.Parameters.AddWithValue("@DOB", model.DOB);
                cmd.Parameters.AddWithValue("@Contact", model.Contact);
                cmd.Parameters.AddWithValue("@RoleId", model.RoleId);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.ExecuteNonQuery();
            }
        }
    }


}