using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibrarySearch
{
    public class DbLibrary
    {
        SqlConnection connection = new SqlConnection();
        DataSet dataSet = new DataSet();
        SqlDataAdapter adapter = new SqlDataAdapter();
        public DbLibrary()
        {
            connection.ConnectionString =
               ConfigurationManager.ConnectionStrings["library"].ConnectionString;

            InitGroupAdapter();
        }
        void InitGroupAdapter()
        {
            var selectCommand = connection.CreateCommand();
            selectCommand.CommandText = "SELECT * FROM Books";

            adapter.SelectCommand = selectCommand;
            adapter.Fill(dataSet);

            var commandBuilder = new SqlCommandBuilder(adapter);
            // adapter.InsertCommand = commandBuilder.GetInsertCommand();
            adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
            adapter.DeleteCommand = commandBuilder.GetDeleteCommand();
        }

        public DataTable SearchByName(string n)
        {

            dataSet.Tables.Clear();
            var query = connection.CreateCommand();
            query.CommandText = $"SELECT * FROM Books AS B WHERE B.Name LIKE '%{n}%'";
            adapter.SelectCommand = query;
            adapter.Fill(dataSet);

            return dataSet.Tables[0];
        }
        public DataTable SearchByAuthor(string n)
        {
            dataSet.Tables.Clear();
            var query = connection.CreateCommand();
query.CommandText  = $"SELECT B.Name, A.FirstName + ' ' + A.LastName" +
                $" FROM Books as B join Authors as A on B.Id_Author=A.ID WHERE A.FirstName LIKE '%{n}%' OR A.LastName LIKE '%{n}%'" ;
                
            adapter.SelectCommand = query ;
                adapter.Fill(dataSet);

                return dataSet.Tables[0];
            
        }
        public DataTable SearchByCat(string n)
        {
            dataSet.Tables.Clear();
            var query = connection.CreateCommand();
            query.CommandText = $"SELECT * FROM Books AS B join Categories AS C on B.Id_Category=C.ID WHERE C.Name LIKE '%{n}%'";
            adapter.SelectCommand = query;
            adapter.Fill(dataSet);

            return dataSet.Tables[0];
        }
        

    }
}
