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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace SequenceAllignProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public string user_id;
        public MainWindow()
        {
            InitializeComponent();
        }

        public string ConString = "Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True";

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private void txtEmail_TextChanged(object sender, RoutedEventArgs e)
        {
            textEmail.Visibility = !string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPassword.Focus();
        }

        private void txtPassword_TextChanged(object sender, RoutedEventArgs e)
        {
            textPassword.Visibility = !string.IsNullOrEmpty(txtPassword.Password) && txtPassword.Password.Length > 0 ? Visibility.Collapsed : Visibility.Visible;
        }
        private void Button_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sqlcon = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
                SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM [User_tb] WHERE Email = '" + txtEmail.Text + "' AND Password = '" + txtPassword.Password + "'", sqlcon);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);

                if (dtbl.Rows[0][0].ToString() == "1")
                {
                    Window1 objmainwindow = new Window1();
                    this.Hide();
                    objmainwindow.Show();
                   

                }
                else
                {
                    MessageBox.Show("Incorrect Email or Password !");
                }
            }
            catch
            {
               MessageBox.Show("Please fill in email and password");
            }

        }
        private void Sigh_in_click(object sender, EventArgs e)
        {
            SighInWindow sighinwindow = new SighInWindow();
            this.Hide();
            sighinwindow.Show();
        }
        
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    

    }

}
