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
    /// Interaction logic for ChoseFromDB.xaml
    /// </summary>
    public partial class ChoseFromDB : Window
    {
       
        private readonly SeqAllgn _mainWindow;
        public ChoseFromDB(SeqAllgn mainwindow)
        {
            _mainWindow = mainwindow;
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
        private void Done(object sender, EventArgs e)
        {
            
            this.Hide();
        }
        private void DataGrid1_SelectionChanged(object sender, EventArgs e)
        {
          
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {

                _mainWindow.txtString.Text = row_selected["String"].ToString();
            }

        }

        public void Load()
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

    }
}
