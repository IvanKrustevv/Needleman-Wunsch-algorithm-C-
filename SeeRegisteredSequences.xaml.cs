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
    /// Interaction logic for SeeRegisteredSequences.xaml
    /// </summary>
    public partial class SeeRegisteredSequences : Window
    {
       
        public SeeRegisteredSequences()
        {
            InitializeComponent();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Cancel(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Load_table(object sender, EventArgs e)
        {
            
                SqlConnection sqlcon = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
            
                string query = "SELECT Sequence.ID, Sequence_Name.Name,Sequence_Type.Type, Sequence_Type.Description,Sequence_Name.String " +
                "FROM [Sequence],[Sequence_Name],[Sequence_Type]" +
                "WHERE Sequence.TypeID = Sequence_Type.ID AND Sequence.NameID=Sequence_Name.ID";

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
            sqlcon.Open();
            string query = "SELECT Sequence.ID, Sequence_Name.Name,Sequence_Type.Type, Sequence_Type.Description,Sequence_Name.String " +
                "FROM [Sequence],[Sequence_Name],[Sequence_Type] " +
                "WHERE Sequence.TypeID = Sequence_Type.ID AND Sequence.NameID=Sequence_Name.ID";

                
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
        private void Insert(object sender, EventArgs e)
        {
            
            AddNewSeq addnewseq = new AddNewSeq();
            addnewseq.Show();
        }
        private void Check(object sender, EventArgs e)
        {          
            SqlConnection sqlcon3 = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");           
            using (SqlCommand command = new SqlCommand("SELECT * FROM Sequence_Name WHERE ID = (SELECT MAX(ID) FROM Sequence_Name)", sqlcon3))
            {
                
                DataSet ds = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter(command))
                    da.Fill(ds);

                //Get the result of the first row        
                DataRow dr = ds.Tables[0].Rows[0];

                //Get the value of the column in the first row        
                txtString_Type.Text = dr["ID"].ToString();
            }
        }
            private void Allign_seq(object sender, EventArgs e)
        {
            SeqAllgn seqallgn = new SeqAllgn();
            this.Hide();
            seqallgn.Show();
            seqallgn.txtString.Text = txtString_seq.Text;
        }   
        private void Remove_table(object sender, EventArgs e)
        {

            SqlConnection sqlcon1 = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
            sqlcon1.Open();
            SqlCommand create_command = new SqlCommand("DELETE [Sequence_Name] where String=@String", sqlcon1);
            create_command.Parameters.AddWithValue("@String", txtString_seq.Text);
            create_command.ExecuteNonQuery();
            sqlcon1.Close();

            SqlConnection sqlcon2 = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
            sqlcon2.Open();
            SqlCommand create_command1 = new SqlCommand("DELETE [Sequence_Type] where Type=@Type", sqlcon2);
            create_command1.Parameters.AddWithValue("@Type", txtString_Type.Text);
            create_command1.ExecuteNonQuery();
            sqlcon2.Close();

            SqlConnection sqlcon3 = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
            sqlcon3.Open();
            SqlCommand create_command2 = new SqlCommand("DELETE [Sequence] where Description=@Descripion", sqlcon3);
            create_command2.Parameters.AddWithValue("@Descripion", txtString.Text);
            create_command2.ExecuteNonQuery();
            sqlcon3.Close();

            txtString.Text = "";
            txtString_seq.Text = "";

            SqlConnection sqlcon = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
            sqlcon.Open();
            string query = "SELECT Sequence.ID, Sequence_Name.Name,Sequence_Type.Type, Sequence_Type.Description,Sequence_Name.String " +
                "FROM [Sequence],[Sequence_Name],[Sequence_Type] " +
                "WHERE Sequence.TypeID = Sequence_Type.ID AND Sequence.NameID=Sequence_Name.ID";


            SqlCommand create_command4 = new SqlCommand(query, sqlcon);
            create_command4.ExecuteNonQuery();

            DataTable dtbl = new DataTable();

            SqlDataAdapter sda = new SqlDataAdapter(create_command4);
            DataTable dt = new DataTable("Sequence");
            sda.Fill(dt);

            DataGrid1.ItemsSource = dt.DefaultView;
            sda.Update(dt);
            sqlcon.Close();


        }
        private void Copy_String(object sender, EventArgs e)
        {          
            string replacementHtmlText = txtString_seq.Text;
            Clipboard.SetText(replacementHtmlText, TextDataFormat.Html);
        }
        private void Exit(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if(row_selected != null)
            {
                txtString_seq.Text = row_selected["String"].ToString();
                txtString.Text = row_selected["Description"].ToString();
                txtString_Type.Text = row_selected["Type"].ToString();
            }
           
            
        }

        
    }

}
