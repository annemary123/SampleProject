using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace mvcevaluation.Models
{
    public class StudentDBHandle
    {

        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["StudentConn"].ToString();
            con = new SqlConnection(constring);
        }


        public List<Student> GetStudent()
        {
            connection();
            List<Student> studentlist = new List<Student>();

            SqlCommand cmd = new SqlCommand("getdetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                studentlist.Add(
                    new Student
                    {
                        Id=Convert.ToInt32(dr["id"]),
                        Lastname = Convert.ToString(dr["lastname"]),
                        Firstname = Convert.ToString(dr["firstname"]),
                        Date=Convert.ToDateTime(dr["dob"]),
                        Phone=Convert.ToString(dr["phone"]),
                        Address1=Convert.ToString(dr["address1"]),
                        State=Convert.ToString(dr["state"]),
                        Country=Convert.ToString(dr["country"]),
                        Pin=Convert.ToString(dr["pin"])
                        
                    });
            }
            return studentlist;
        }


        public void InsertStudents(Student smodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("insertstudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@firstname", smodel.Firstname);
            cmd.Parameters.AddWithValue("@lastname", smodel.Lastname);
            cmd.Parameters.AddWithValue("@dob", smodel.Date);
            cmd.Parameters.AddWithValue("@phone", smodel.Phone);
            SqlParameter output = new SqlParameter("@id", SqlDbType.Int);
            output.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(output);
            cmd.Parameters.AddWithValue("@address1", smodel.Address1);
            cmd.Parameters.AddWithValue("@state", smodel.State);
            cmd.Parameters.AddWithValue("@country", smodel.Country);
            cmd.Parameters.AddWithValue("@pin", smodel.Pin);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }


        public void UpdateStudent(Student smodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("updatestudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", smodel.Id);
            cmd.Parameters.AddWithValue("@firstname", smodel.Firstname);
            cmd.Parameters.AddWithValue("@lastname", smodel.Lastname);
            cmd.Parameters.AddWithValue("@dob", smodel.Date);
            cmd.Parameters.AddWithValue("@phone", smodel.Phone);
            cmd.Parameters.AddWithValue("@address1", smodel.Address1);
            cmd.Parameters.AddWithValue("@state", smodel.State);
            cmd.Parameters.AddWithValue("@country", smodel.Country);
            cmd.Parameters.AddWithValue("@pin", smodel.Pin);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
        }

        public bool DeleteStudent(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("deletestudent", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }    
}