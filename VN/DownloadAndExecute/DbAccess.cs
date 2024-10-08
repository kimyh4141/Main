
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace DownloadAndExecute
{
    static class DbAccess
    {

        private static string ReadConnStr()
        {
            //ConnectionString 설정
            return "Data Source=" + ConfigData.ConnectionIP + "; Initial Catalog=Y2sVn1Mes3; User ID=wisem2; password =wisem2608";
        }

        /// <summary>
        /// DB 의 연결 주소를 가져옵니다.
        /// </summary>
        public static string ConnectionString
        {
            get { return ReadConnStr(); }
        }

        /// <summary>
        /// DB와 연결 되었는지 확인합니다.
        /// </summary>
        /// <returns>연결시 True</returns>
        public static bool IsConn()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                    return true;
            }
            catch (Exception)
            {
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return false;
        }

        /// <summary>
        /// 쿼리를 이용하여 데이터 테이블 을 가져 옵니다.
        /// </summary>
        /// <param name="query">Sql Query</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDt(string query)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);

            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(comm);
                da.Fill(dt);
            }
            catch
            {
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return dt;
        }

        /// <summary>
        /// 쿼리를 이용하여 데이터셋 을 가져 옵니다.
        /// </summary>
        /// <param name="query">Sql Query</param>
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string query)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);

            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(comm);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return ds;
        }

        /// <summary>
        /// 쿼리를 이용하여 가져오는 데이터의 첫번째 내용을 가져옵니다.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static string GetString(string query)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            string resultStr = null;
            object resultObj = null;
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(query, conn);
                resultObj = comm.ExecuteScalar();
            }
            catch
            { }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }


            if (resultObj != null)
            {
                return resultStr = resultObj.ToString();
            }
            else
            {
                return resultStr;
            }

        }

        /// <summary>
        /// 쿼리 명령어를 DB 에 전송합니다.
        /// </summary>
        /// <param name="query"></param>
        /// <returns>영향을 받은 행의 수</returns>
        public static int ExecuteQuery(string query)
        {
            int insertNum = -1;
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(query, conn);
                insertNum = comm.ExecuteNonQuery();
            }
            catch
            { }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return insertNum;
        }

        /// <summary>
        /// ConnectionString 을 이용하여 만든 SqlConnection 을 제공합니다.
        /// </summary>
        /// <returns></returns>
        public static SqlConnection CreateSqlConnection()
        {
            return new SqlConnection(ConnectionString);
        }





    }
}
