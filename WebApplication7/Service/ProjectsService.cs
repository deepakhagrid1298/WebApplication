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
    public class ProjectsService
    {
        public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

        private SqlDataAdapter sqlDataAdapter;
        private DataSet dataSet;
        public IList<ProjectsModel> getAllProjects()
        {
            IList<ProjectsModel> getProjectList = new List<ProjectsModel>();
            dataSet = new DataSet();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Project_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "GetAllProjects");
                sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataSet);
                if (dataSet.Tables.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        ProjectsModel obj = new ProjectsModel();
                        obj.Id = Convert.ToInt32(dataSet.Tables[0].Rows[i]["Id"]);
                        obj.Name = Convert.ToString(dataSet.Tables[0].Rows[i]["Name"]);
                        obj.StartDate = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["StartDate"]);
                        obj.EndDate = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["EndDate"]);
                        obj.Budget = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["Budget"]);
                        getProjectList.Add(obj);


                    }
                }
                return getProjectList;
            }
        }



        public void addProject(ProjectsModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Project_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "AddProject");
                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@StartDate", model.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", model.EndDate);
                cmd.Parameters.AddWithValue("@Budget", model.Budget);
                cmd.ExecuteNonQuery();
            }
        }
        public ProjectsModel updateProjectById(int Id)
        {
            var model = new ProjectsModel();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Project_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "GetProjectById");
                cmd.Parameters.AddWithValue("@Id", Id);
                sqlDataAdapter = new SqlDataAdapter(cmd);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    model.Id = Convert.ToInt32(dataSet.Tables[0].Rows[0]["Id"]);
                    model.Name = Convert.ToString(dataSet.Tables[0].Rows[0]["Name"]);
                    model.StartDate = Convert.ToDateTime(dataSet.Tables[0].Rows[0]["StartDate"]);
                    model.EndDate = Convert.ToDateTime(dataSet.Tables[0].Rows[0]["EndDate"]);
                    model.Budget = Convert.ToDecimal(dataSet.Tables[0].Rows[0]["Budget"]);

                }
            }
            return model;
        }
        public void updateProject(ProjectsModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Project_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "UpdateProject");
                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@StartDate", model.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", model.EndDate);
                cmd.Parameters.AddWithValue("@Budget", model.Budget);
                cmd.ExecuteNonQuery();
            }
        }
    }
}