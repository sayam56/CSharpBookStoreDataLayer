/* ************************************************************
 * For students to work on assignments and project.
 * Permission required material. Contact: xyuan@uwindsor.ca 
 * ************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;

namespace BookStoreDATA
{
    public class DALUserInfo
    {
        public int LogIn(string userName, string password)
        {
            var conn = new SqlConnection(Properties.Settings.Default.aiConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select UserID from UserData where "
                    + " UserName = @UserName and Password = @Password ";
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Password", password);
                conn.Open();
                int userId;

                if (cmd.ExecuteScalar() == null)
                {
                    userId = -1;
                }
                else
                    userId = (int)cmd.ExecuteScalar();
                if (userId > 0) return userId;
                else return -1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return -1;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        public string GetFullName(int userID)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.aiConnectionString);
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select FullName from UserData where " +
                    " UserID=@userID";
                cmd.Parameters.AddWithValue("@UserID", userID);
                conn.Open();
                string fullName;
                if (cmd.ExecuteScalar() == null)
                {
                    return "";
                }
                else
                {
                    fullName = (string)cmd.ExecuteScalar();
                }
                return fullName;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return "";
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}
