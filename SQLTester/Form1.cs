using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace SQLTester
{
    public partial class frmSQLTester : Form
    {
        public frmSQLTester()
        {
            InitializeComponent();
        }
        SqlConnection booksConnection;

        private void frmSQLTester_Load(object sender, EventArgs e)
        {
            string path = Path.GetFullPath("SQLBooksDB.mdf");
            // connect to books database
            booksConnection = new
                SqlConnection("Data Source=.\\SQLEXPRESS; AttachDBFilename=C:\\Users\\thawkins022713\\source\\repos\\SQLTester\\SQLTester\\SQLBooksDB.mdf;" +
                "Integrated Security=True; Connect Timeout=30; User Instance=True");
            booksConnection.Open();
        }

        private void frmSQLTester_FormClosing(object sender, FormClosingEventArgs e)
        {
            booksConnection.Close();
            booksConnection.Dispose();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            SqlCommand resultsCommand = null;
            SqlDataAdapter resultsAdapter = new
                SqlDataAdapter();
            DataTable resultsTable = new DataTable();
            try
            {
                // establish command object and data table
                resultsCommand = new SqlCommand(txtSQLTester.Text, booksConnection);
                resultsAdapter.SelectCommand = resultsCommand;
                resultsAdapter.Fill(resultsTable);
                // bind grid view to data table
                grdSQLTester.DataSource = resultsTable;
                lblRecords.Text = resultsTable.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in Processing SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            resultsCommand.Dispose();
            resultsAdapter.Dispose();
            resultsTable.Dispose();
        }
    }
}
