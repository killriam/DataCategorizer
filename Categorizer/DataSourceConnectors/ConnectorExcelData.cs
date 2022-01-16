using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Categorizer.DataSourceConnectors
{
    public class ConnectorExcelData : IDataSourceConnector
    {

        
        public int elementIndex = 0;
        public String filePath { get; }
        DataTable dtexcel;

        public ConnectorExcelData(String filepath)
        {
            if (File.Exists(filepath))
            {
                this.filePath = filepath;
            }
            dtexcel = new DataTable();
        }



        public object getCurrentElement()
        {
            return this.dtexcel.Rows[elementIndex];
        }

        public void MovetoNextElement()
        {
            if (elementIndex < dtexcel.Rows.Count)
            {
                elementIndex++;
            }
        }

        public void setCategoryOfCurrentElement(string newcategory)
        {
            throw new NotImplementedException();
        }

        public void readDataFromFile()
        {

            bool hasHeaders = false;
            string HDR = hasHeaders ? "Yes" : "No";
            string strConn;
            if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xlsx")
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
            else
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            //Looping Total Sheet of Xl File
            /*foreach (DataRow schemaRow in schemaTable.Rows)
            {
            }*/
            //Looping a first Sheet of Xl File
            DataRow schemaRow = schemaTable.Rows[0];
            string sheet = schemaRow["TABLE_NAME"].ToString();
            if (!sheet.EndsWith("_"))
            {
                string query = "SELECT  * FROM [" + sheet + "]";
                OleDbDataAdapter daexcel = new OleDbDataAdapter(query, conn);
                dtexcel.Locale = CultureInfo.CurrentCulture;
                daexcel.Fill(dtexcel);
            }

            conn.Close();
            /*return dtexcel;
            var ds = new DataSet();

            List<DataRow> list = dt.AsEnumerable().ToList();

            adapter.Fill(ds, "anyNameHere");

            DataTable data = ds.Tables["anyNameHere"];*/
        }

        public int getCurrentElementIndex()
        {
            return elementIndex;
        }
    }
}
