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

namespace CRUD_Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MJFMMK0\SQLEXPRESS;Initial Catalog=crud_db;Integrated Security=True");

        private void Form1_Load(object sender, EventArgs e)
        {
            BinData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cnn = new SqlCommand("insert into emptab values(@id,@name,@age,@salary)", con);
            cnn.Parameters.AddWithValue("@Id", int.Parse(textBox1.Text));
            cnn.Parameters.AddWithValue("@Name", textBox2.Text);
            cnn.Parameters.AddWithValue("@Age", int.Parse(textBox3.Text));
            cnn.Parameters.AddWithValue("@Salary", int.Parse(textBox4.Text));
            cnn.ExecuteNonQuery();
            con.Close();
            BinData();

            MessageBox.Show("Record Added Successfully", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void BinData()
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MJFMMK0\SQLEXPRESS;Initial Catalog=crud_db;Integrated Security=True");

            SqlCommand cnn = new SqlCommand("select * from emptab", con);
            SqlDataAdapter da = new SqlDataAdapter(cnn);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MJFMMK0\SQLEXPRESS;Initial Catalog=crud_db;Integrated Security=True");

            con.Open();
            SqlCommand cnn = new SqlCommand("update emptab set name=@name,age=@age,salary=@salary where id=@id", con);
            cnn.Parameters.AddWithValue("@Id", int.Parse(textBox1.Text));
            cnn.Parameters.AddWithValue("@Name", textBox2.Text);
            cnn.Parameters.AddWithValue("@Age", int.Parse(textBox3.Text));
            cnn.Parameters.AddWithValue("@Salary", int.Parse(textBox4.Text));
            cnn.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Record Updated Successfully", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MJFMMK0\SQLEXPRESS;Initial Catalog=crud_db;Integrated Security=True");

            con.Open();
            SqlCommand cnn = new SqlCommand("delete from emptab where id=@id", con);
            cnn.Parameters.AddWithValue("@Id", int.Parse(textBox1.Text));
            
            cnn.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Record Deleted Successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            SqlCommand cnn = new SqlCommand("select * from emptab", con);
            SqlDataAdapter da = new SqlDataAdapter(cnn);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MJFMMK0\SQLEXPRESS;Initial Catalog=crud_db;Integrated Security=True");

            con.Open();
            SqlCommand cnn = new SqlCommand("select * from emptab where id=@id", con);
            cnn.Parameters.AddWithValue("@Id", int.Parse(textBox1.Text));
            SqlDataAdapter da = new SqlDataAdapter(cnn);
            DataTable table = new DataTable();
            da.Fill(table);
            con.Close();
            dataGridView1.DataSource = table;

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap imagebmp = new Bitmap(dataGridView1.Width, dataGridView1.Height);
            dataGridView1.DrawToBitmap(imagebmp, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
            e.Graphics.DrawImage(imagebmp, 5, 20);
        }
    }
}
