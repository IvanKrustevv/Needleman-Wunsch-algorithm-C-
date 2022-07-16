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
    /// Interaction logic for AddNewSeq.xaml
    /// </summary>
    public partial class AddNewSeq : Window
    {
        public AddNewSeq()
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
     
        private void textSeqName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtSeqName.Focus();
        }
        private void txtSeqName_TextChanged(object sender, RoutedEventArgs e)
        {
            textSeqName.Visibility = !string.IsNullOrEmpty(txtSeqName.Text) && txtSeqName.Text.Length > 0 ? Visibility.Collapsed : Visibility.Visible;
        }
        private void textSeqType_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtSeqType.Focus();
        }
        private void txtSeqType_Changed(object sender, RoutedEventArgs e)
        {
            textSeqType.Visibility = !string.IsNullOrEmpty(txtSeqType.Text) && txtSeqType.Text.Length > 0 ? Visibility.Collapsed : Visibility.Visible;
        }
        private void textString_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtString.Focus();
        }
        private void txtString_TextChanged(object sender, RoutedEventArgs e)
        {
            textString.Visibility = !string.IsNullOrEmpty(txtString.Text) && txtString.Text.Length > 0 ? Visibility.Collapsed : Visibility.Visible;
        }
        private void textNote_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtNote.Focus();
        }
        private void txtNote_TextChanged(object sender, RoutedEventArgs e)
        {
            textNote.Visibility = !string.IsNullOrEmpty(txtNote.Text) && txtNote.Text.Length > 0 ? Visibility.Collapsed : Visibility.Visible;
        }
        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPassword.Focus();
        }
        private void txtPassword_TextChanged(object sender, RoutedEventArgs e)
        {
            textPassword.Visibility = !string.IsNullOrEmpty(txtPassword.Password) && txtPassword.Password.Length > 0 ? Visibility.Collapsed : Visibility.Visible;
        }
        private void Add_Sequence_to_DB(object sender, RoutedEventArgs e)
        {           
            SqlConnection sqlcon = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
            
            string Name = txtSeqName.Text;
            string Description = txtNote.Text;
            string String = txtString.Text;
            string Sequence_Name_ID, Sequence_Type_ID;

            string query1 = "INSERT INTO [Sequence_Name] (Name,Description,String) VALUES ('" + Name + "', '" + Description + "', '" + String + "') ";
            sqlcon.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM [Sequence_Name] WHERE Name = '" + Name + "'", sqlcon);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);
            if (dtbl.Rows[0][0].ToString() == "0")
                {
                    SqlCommand create_command = new SqlCommand(query1, sqlcon);
                    create_command.ExecuteNonQuery();
                                    
                    string Type = txtSeqType.Text;
                    string query2 = "INSERT INTO [Sequence_Type] (Type,Description) VALUES ('" + Type + "', '" + Description + "') ";                  
                    SqlCommand create_command1 = new SqlCommand(query2, sqlcon);
                    create_command1.ExecuteNonQuery();
                                                                        
                    SqlConnection sqlcon3 = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Sequence_Name WHERE ID = (SELECT MAX(ID) FROM Sequence_Name)", sqlcon3))
                    {

                    
                    DataSet ds1 = new DataSet();
                        using (SqlDataAdapter da1 = new SqlDataAdapter(command))
                            da1.Fill(ds1);

                            //Get the result of the first row        
                            DataRow dr = ds1.Tables[0].Rows[0];

                            //Get the value of the column in the first row        
                            Sequence_Name_ID = dr["ID"].ToString();
                    }

                    SqlConnection sqlcon4 = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
                    using (SqlCommand command1 = new SqlCommand("SELECT * FROM Sequence_Type WHERE ID = (SELECT MAX(ID) FROM Sequence_Type)", sqlcon4))
                    {
                        
                        DataSet ds = new DataSet();
                        using (SqlDataAdapter da = new SqlDataAdapter(command1))
                            da.Fill(ds);

                        //Get the result of the first row        
                        DataRow dr = ds.Tables[0].Rows[0];

                        //Get the value of the column in the first row        
                        Sequence_Type_ID = dr["ID"].ToString();
                    }

                     string query3 = "INSERT INTO [Sequence] (TypeID,NameID,Description) VALUES ('" + Sequence_Type_ID + "', '" + Sequence_Name_ID + "', '" + Description + "')";
                     SqlCommand create_command4 = new SqlCommand(query3, sqlcon);
                     create_command4.ExecuteNonQuery();
                     sqlcon.Close();
                     MessageBox.Show("Sequence added!");
                     this.Hide();

            }
            else
            {
                MessageBox.Show("A Sequence with this name is already registered!");
                sqlcon.Close();
            }
                  
        }
        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
        }
    }
}
