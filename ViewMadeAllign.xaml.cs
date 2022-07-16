using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace SequenceAllignProject
{
    /// <summary>
    /// Interaction logic for ViewMadeAllign.xaml
    /// </summary>
    public partial class ViewMadeAllign : Window
    {
        public ViewMadeAllign()
        {
            InitializeComponent();
        }
        private void Cancel(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();

            }
        }
        public void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                //txtString_seq.Text = row_selected["String"].ToString();
                //txtString.Text = row_selected["Description"].ToString();
                //txtString_Type.Text = row_selected["Type"].ToString();
                AlignmentID.Text = row_selected["ID"].ToString();
                //txtSeq1ID.Text = row_selected["Query_sting_ID"].ToString();
                //txtSeq2ID.Text = row_selected["Subject_string_ID"].ToString();
                txtString_seq2.Text = row_selected["Result"].ToString();
                
            }
            SqlConnection sqlcon8 = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
            using (SqlCommand command = new SqlCommand("SELECT * FROM Alignments_tb S WHERE S.ID = '" + AlignmentID.Text + "'", sqlcon8))
            {
                DataSet ds = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter(command))
                    da.Fill(ds);

                //Get the result of the first row        
                DataRow dr = ds.Tables[0].Rows[0];

                //Get the value of the column in the first row        
                txtSeq1ID.Text = dr["Query_sting_ID"].ToString();
                txtSeq2ID.Text = dr["Subject_string_ID"].ToString();
            }

            SqlConnection sqlcon = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
            using (SqlCommand command = new SqlCommand("SELECT * FROM Sequence S WHERE S.ID = '" + txtSeq1ID.Text + "'", sqlcon))
            {
                DataSet ds = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter(command))
                da.Fill(ds);

                //Get the result of the first row        
                DataRow dr = ds.Tables[0].Rows[0];

                //Get the value of the column in the first row        
                txtSeq1TBID.Text = dr["NameID"].ToString();
            }

            SqlConnection sqlcon1 = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
            using (SqlCommand command1 = new SqlCommand("SELECT * FROM Sequence_Name S_name WHERE S_name.ID = '" + txtSeq1TBID.Text + "'", sqlcon))
            {
                DataSet ds = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter(command1))
                    da.Fill(ds);

                //Get the result of the first row        
                DataRow dr = ds.Tables[0].Rows[0];

                //Get the value of the column in the first row        
                txtSeq1Name.Text = dr["Name"].ToString();
                txtString.Text = dr["String"].ToString();
                
            }

            SqlConnection sqlcon2 = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
            using (SqlCommand command2 = new SqlCommand("SELECT * FROM Sequence S WHERE S.ID = '" + txtSeq1ID.Text + "'", sqlcon2))
            {
                DataSet ds = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter(command2))
                    da.Fill(ds);

                //Get the result of the first row        
                DataRow dr = ds.Tables[0].Rows[0];

                //Get the value of the column in the first row        
                txtSeqType1TBID.Text = dr["TypeID"].ToString();
            }

            SqlConnection sqlcon3 = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
            using (SqlCommand command1 = new SqlCommand("SELECT * FROM Sequence_Type S_type WHERE S_type.ID = '" + txtSeqType1TBID.Text + "'", sqlcon))
            {
                DataSet ds = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter(command1))
                    da.Fill(ds);

                //Get the result of the first row        
                DataRow dr = ds.Tables[0].Rows[0];

                //Get the value of the column in the first row        
                txtSeq1Type.Text = dr["Type"].ToString();
            }
            ////////////////////////////

            SqlConnection sqlcon4 = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
            using (SqlCommand command = new SqlCommand("SELECT * FROM Sequence S WHERE S.ID = '" + txtSeq2ID.Text + "'", sqlcon4))
            {
                DataSet ds = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter(command))
                    da.Fill(ds);

                //Get the result of the first row        
                DataRow dr = ds.Tables[0].Rows[0];

                //Get the value of the column in the first row        
                txtSeq12BID.Text = dr["NameID"].ToString();
            }

            SqlConnection sqlcon5 = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
            using (SqlCommand command1 = new SqlCommand("SELECT * FROM Sequence_Name S_name WHERE S_name.ID = '" + txtSeq12BID.Text + "'", sqlcon))
            {
                DataSet ds = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter(command1))
                    da.Fill(ds);

                //Get the result of the first row        
                DataRow dr = ds.Tables[0].Rows[0];

                //Get the value of the column in the first row        
                txtSeq2Name.Text = dr["Name"].ToString();
                txtString_seq.Text = dr["String"].ToString();

            }

            SqlConnection sqlcon6 = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
            using (SqlCommand command2 = new SqlCommand("SELECT * FROM Sequence S WHERE S.ID = '" + txtSeq2ID.Text + "'", sqlcon2))
            {
                DataSet ds = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter(command2))
                    da.Fill(ds);

                //Get the result of the first row        
                DataRow dr = ds.Tables[0].Rows[0];

                //Get the value of the column in the first row        
                txtSeqType2TBID.Text = dr["TypeID"].ToString();
            }

            SqlConnection sqlcon7 = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
            using (SqlCommand command1 = new SqlCommand("SELECT * FROM Sequence_Type S_type WHERE S_type.ID = '" + txtSeqType2TBID.Text + "'", sqlcon))
            {
                DataSet ds = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter(command1))
                    da.Fill(ds);

                //Get the result of the first row        
                DataRow dr = ds.Tables[0].Rows[0];

                //Get the value of the column in the first row        
                txtSeq2Type.Text = dr["Type"].ToString();
            }

        }
        private void Load_table(object sender, EventArgs e)
        {

            
            SqlConnection sqlcon = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");

            string query = "SELECT Alignments_tb.ID, User_tb.Username, User_tb.FirstName, User_tb.Email, Alignments_tb.Description, Alignments_tb.Date, Alignments_tb.Result FROM [User_tb], [Alignments_tb] WHERE Alignments_tb.UserID = User_tb.ID";

            sqlcon.Open();
                SqlCommand create_command = new SqlCommand(query, sqlcon);
                create_command.ExecuteNonQuery();

                DataTable dtbl = new DataTable();

                SqlDataAdapter sda = new SqlDataAdapter(create_command);
                DataTable dt = new DataTable("Sequence");
                sda.Fill(dt);
                DataGrid1.ItemsSource = dt.DefaultView;
                sda.Update(dt);
                sqlcon.Close();
         }

        private void Refresh_table(object sender, EventArgs e)
        {


            SqlConnection sqlcon = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");

            string query = "SELECT Alignments_tb.ID, User_tb.Username, User_tb.FirstName, User_tb.Email, Alignments_tb.Description, Alignments_tb.Date, Alignments_tb.Result FROM [User_tb], [Alignments_tb] WHERE Alignments_tb.UserID = User_tb.ID";

            sqlcon.Open();
            SqlCommand create_command = new SqlCommand(query, sqlcon);
            create_command.ExecuteNonQuery();

            DataTable dtbl = new DataTable();

            SqlDataAdapter sda = new SqlDataAdapter(create_command);
            DataTable dt = new DataTable("Sequence");
            sda.Fill(dt);
            DataGrid1.ItemsSource = dt.DefaultView;
            sda.Update(dt);
            sqlcon.Close();
        }

        private void Exit(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlcon1 = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
            sqlcon1.Open();
            SqlCommand create_command = new SqlCommand("DELETE [Alignments_tb] where ID=@ID", sqlcon1);
            create_command.Parameters.AddWithValue("@ID", AlignmentID.Text);
            create_command.ExecuteNonQuery();
            sqlcon1.Close();


            SqlConnection sqlcon = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
            string query = "SELECT Alignments_tb.ID, User_tb.Username, User_tb.FirstName, User_tb.Email, Alignments_tb.Description, Alignments_tb.Date, Alignments_tb.Result FROM [User_tb], [Alignments_tb] WHERE Alignments_tb.UserID = User_tb.ID";
            sqlcon.Open();
            SqlCommand create_command1 = new SqlCommand(query, sqlcon);
            create_command1.ExecuteNonQuery();

            DataTable dtbl = new DataTable();

            SqlDataAdapter sda = new SqlDataAdapter(create_command1);
            DataTable dt = new DataTable("Sequence");
            sda.Fill(dt);
            DataGrid1.ItemsSource = dt.DefaultView;
            sda.Update(dt);
            sqlcon.Close();

        }
    }

}

