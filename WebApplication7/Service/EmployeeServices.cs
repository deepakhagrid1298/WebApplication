using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Web;
using WebApplication7.Models;

namespace WebApplication7.Service
{
    public class EmployeeServices
    {
        
        public string connect= ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private SqlDataAdapter _adapter;
        private DataSet _ds;
        public IList<EmployeeModel> GetEmployeeList()
        {
            IList<EmployeeModel> getEmpList = new List<EmployeeModel>();
            _ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EmployeeViewOrInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "GetEmpList");
                _adapter = new SqlDataAdapter(cmd);
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0)
                {
                    for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                    {
                        EmployeeModel obj = new EmployeeModel();
                        obj.Id = Convert.ToInt32(_ds.Tables[0].Rows[i]["Id"]);
                        obj.FirstName = Convert.ToString(_ds.Tables[0].Rows[i]["FirstName"]);
                        obj.LastName = Convert.ToString(_ds.Tables[0].Rows[i]["LastName"]);
                        obj.DOB = Convert.ToDateTime(_ds.Tables[0].Rows[i]["DOB"]);
                        obj.Contact = Convert.ToString(_ds.Tables[0].Rows[i]["Contact"]);
                        obj.RoleId = Convert.ToInt32(_ds.Tables[0].Rows[i]["RoleId"]);
                        getEmpList.Add(obj);
                    }
                }


            }


            return getEmpList;
        }


        public void InsertEmployee(EmployeeModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("EmployeeViewOrInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "AddEmployee");
                cmd.Parameters.AddWithValue("@EmpId", model.Id);
                cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                cmd.Parameters.AddWithValue("@LastName", model.LastName);
                cmd.Parameters.AddWithValue("@DOB", model.DOB);
                cmd.Parameters.AddWithValue("@Contact", model.Contact);
                cmd.Parameters.AddWithValue("@RoleId", model.RoleId);
                cmd.ExecuteNonQuery();

            }
        }


        public EmployeeModel GetEditById(int Id)
        {
            var model = new EmployeeModel();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EmployeeViewOrInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "GetEmployeeById");
                cmd.Parameters.AddWithValue("@EmpId", Id);
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    model.Id = Convert.ToInt32(_ds.Tables[0].Rows[0]["Id"]);
                    model.FirstName = Convert.ToString(_ds.Tables[0].Rows[0]["FirstName"]);
                    model.LastName = Convert.ToString(_ds.Tables[0].Rows[0]["LastName"]);
                    model.DOB = Convert.ToDateTime(_ds.Tables[0].Rows[0]["DOB"]);
                    model.Contact = Convert.ToString(_ds.Tables[0].Rows[0]["Contact"]);
                    model.RoleId = Convert.ToInt32(_ds.Tables[0].Rows[0]["RoleId"]);

                }
            }
            return model;
        }


        public void UpdateEmp(EmployeeModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EmployeeViewOrInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "UpdateEmployee");
                cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                cmd.Parameters.AddWithValue("@LastName", model.LastName);
                cmd.Parameters.AddWithValue("@DOB", model.DOB);
                cmd.Parameters.AddWithValue("@Contact", model.Contact);
                cmd.Parameters.AddWithValue("@RoleId", model.RoleId);
                cmd.Parameters.AddWithValue("@EmpId", model.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteEmp(int Id)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EmployeeViewOrInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "DeleteEmployee");
                cmd.Parameters.AddWithValue("@EmpId", Id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}