using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Partial_Creation.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Partial_Creation.Bussiness_Layer
{
    public class UserBusinessLayer
    {
        string connectionString =
                    ConfigurationManager.ConnectionStrings["EmployeeContext"].ConnectionString;
        public IEnumerable<Users> users
        {
            get
            {
                //string connectionString =
                //    ConfigurationManager.ConnectionStrings["EmployeeContext"].ConnectionString;

                List<Users> UserList = new List<Users>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetAllUsers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Users user = new Users();
                        user.slNo = Convert.ToInt32(rdr["slNo"]);
                        user.UserName = rdr["UserName"].ToString();
                        user.EmailId = rdr["EmailId"].ToString();
                        user.Password = rdr["Password"].ToString();
                        user.ConfirmPassword = rdr["ConfirmPassword"].ToString();

                        UserList.Add(user);
                    }
                }

                return UserList;
            }
        }

        public void AddUser(Users users)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["EmployeeContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("UserName","@UserName");
                //cmd.Parameters.AddWithValue("EmailId", "@EmailId");
                //cmd.Parameters.AddWithValue("Password", "@Password");
                //cmd.Parameters.AddWithValue("ConfirmPassword", "@ConfirmPassword");
                SqlParameter paramUserName = new SqlParameter();
                paramUserName.ParameterName = "@UserName";
                paramUserName.Value = users.UserName;
                cmd.Parameters.Add(paramUserName);

                SqlParameter paramEmailId = new SqlParameter();
                paramEmailId.ParameterName = "@EmailId";
                paramEmailId.Value = users.EmailId;
                cmd.Parameters.Add(paramEmailId);

                SqlParameter paramPassword = new SqlParameter();
                paramPassword.ParameterName = "@Password";
                paramPassword.Value = users.Password;
                cmd.Parameters.Add(paramPassword);

                SqlParameter paramConfirmPassword = new SqlParameter();
                paramConfirmPassword.ParameterName = "@ConfirmPassword";
                paramConfirmPassword.Value = users.ConfirmPassword;
                cmd.Parameters.Add(paramConfirmPassword);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public  static DataTable getUserWithEmailId(string EmailId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["EmployeeContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "select * from tbl_Users where EmailId='" + EmailId + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Close();
                return dt;
            }
        }
    }
}