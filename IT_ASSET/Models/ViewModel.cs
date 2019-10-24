using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IT_ASSET.Models
{
    public class ViewModel
    {
        string connectionString = "Data Source= COMP05\\SQLEXPRESS;Initial Catalog=IT_ASSET_MANAGEMENT;Integrated Security=True";

        public void AddIncidents(tbl_incident_case tbl_Incident_Case)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("AddIncident", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@INC_DATE", tbl_Incident_Case.INC_DATE);
                cmd.Parameters.AddWithValue("@INC_REQUESTER", tbl_Incident_Case.INC_REQUESTER);
                cmd.Parameters.AddWithValue("@INC_TOPIC", tbl_Incident_Case.INC_TOPIC);
                cmd.Parameters.AddWithValue("@INC_STATUS", tbl_Incident_Case.INC_STATUS);
                cmd.Parameters.AddWithValue("@INC_DESCRIPTION", tbl_Incident_Case.INC_DESCRIPTION);

                cmd.Parameters.Add("@Year", SqlDbType.NVarChar).Value = "2019";
                cmd.Parameters.Add("@PrefixText", SqlDbType.NVarChar).Value = "INC19";
                cmd.Parameters.Add("@iLength", SqlDbType.Int).Value = "5";


                cmd.Parameters.Add("@INC_CODE", SqlDbType.NVarChar, 20);
                cmd.Parameters["@INC_CODE"].Direction = ParameterDirection.Output;
                con.Open();
                cmd.ExecuteNonQuery();
                 
                string deCode = Convert.ToString(cmd.Parameters["@INC_CODE"].Value);
                
                SqlDataReader rdr = cmd.ExecuteReader();
                tbl_Incident_Case.INC_CODE = rdr["@INC_CODE"].ToString();
                Console.WriteLine("TESt");
                Console.ReadLine();
                while (rdr.Read())
                {
                    tbl_Incident_Case.INC_CODE = rdr["INC_CODE"].ToString();
                }
                con.Close();

              
              
                
            }
             
        }
    }
}