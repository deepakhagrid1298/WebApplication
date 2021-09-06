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
    public class RoleService
    {

        public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private SqlDataAdapter sqlDataAdapter;
        private DataSet dataSet;

        public IList<RoleModel> GetAllRoles()
        {
            IList<RoleModel> getRoles = new List<RoleModel>();
            dataSet = new DataSet();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Role_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "getAllRoles");
                sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataSet);
                if (dataSet.Tables.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        RoleModel obj = new RoleModel();
                        obj.Id = Convert.ToInt32(dataSet.Tables[0].Rows[i]["Id"]);
                        obj.Name = Convert.ToString(dataSet.Tables[0].Rows[i]["Name"]);
                        getRoles.Add(obj);

                    }

                }
            }


            return getRoles;

        }

        public void UpdateRole(RoleModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Role_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "UpdateRole");
                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@Id", model.Name);
                cmd.ExecuteNonQuery();
            }
        }
    }
}