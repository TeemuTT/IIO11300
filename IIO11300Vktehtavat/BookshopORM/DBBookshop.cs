using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshopORM
{
    class DBBookshop
    {
        public static DataTable GetTestData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("author", typeof(string));
            dt.Columns.Add("country", typeof(string));
            dt.Columns.Add("year", typeof(int));
            dt.Rows.Add(11, "Pekka Lipposen seikkailut", "Outsider", "Suomi", 1950);
            dt.Rows.Add(12, "Lucky Luke", "René Coscinny", "Belgia", 194);
            return dt;
        }

        public static DataTable GetBooks(string connStr)
        {
            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    string sql = "select id, name, author, country, year from books";
                    var cmd = new SqlCommand(sql, conn);
                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable("Books");
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int UpdateBook(string connStr, int id, string name, string author, string country, int year)
        {
            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    string sql = string.Format("update books set name=@Nimi, author='{2}', country='{3}', year='{4}' where id='{0}'",
                        id, name, author, country, year);
                    var cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Nimi", name);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int InsertBook(string connStr, string name, string author, string country, int year)
        {
            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    string sql = string.Format("insert into books (name, author, country, year) values (@Nimi, @Kirjailija, @Maa, @Vuosi)");
                    var cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Nimi", name);
                    cmd.Parameters.AddWithValue("@Kirjailija", author);
                    cmd.Parameters.AddWithValue("@Maa", country);
                    cmd.Parameters.AddWithValue("@Vuosi", year);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int DeleteBook(string connStr, int id)
        {
            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    string sql = string.Format("delete from books where id = {0}", id);
                    var cmd = new SqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
