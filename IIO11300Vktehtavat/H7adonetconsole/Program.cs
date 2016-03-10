using System;
using System.Data.SqlClient;

namespace H7adonetconsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string connStr = H7adonetconsole.Properties.Settings.Default.Tietokanta;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT asioid, firstname, lastname, date FROM presences where asioid = 'H8705'", conn);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            Console.WriteLine("Läsnäolosi {0} {1} {2} {3}", rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetDateTime(3).ToShortDateString());
                        }
                    }
                    Console.WriteLine("Tietokantayhteys suljettu.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
