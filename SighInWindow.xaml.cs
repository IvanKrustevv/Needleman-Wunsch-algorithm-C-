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
    /// Interaction logic for SighInWindow.xaml
    /// </summary>
    public partial class SighInWindow : Window
    {

        public string ConString = "Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True";
        SqlConnection sqlcon = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
        SqlCommand cmd;
        public SighInWindow()
        {
            InitializeComponent();
        }
        private void Sigh_in_click(object sender, EventArgs e)
        {
            MainWindow sighinwindow = new MainWindow();
            this.Hide();
            sighinwindow.Show();
        }


        private void textFirstName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtFirstName.Focus();
        }
        private void txtFirstName_Changed(object sender, RoutedEventArgs e)
        {
            textFirstName.Visibility = !string.IsNullOrEmpty(txtFirstName.Text) && txtFirstName.Text.Length > 0 ? Visibility.Collapsed : Visibility.Visible;
        }



        private void textLastNmae_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtLastName.Focus();
        }
        private void txtLastName_TextChanged(object sender, RoutedEventArgs e)
        {
            textLastName.Visibility = !string.IsNullOrEmpty(txtLastName.Text) && txtLastName.Text.Length > 0 ? Visibility.Collapsed : Visibility.Visible;
        }


        private void textUsername_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtUsername.Focus();
        }
        private void txtUsername_TextChanged(object sender, RoutedEventArgs e)
        {
            textUsername.Visibility = !string.IsNullOrEmpty(txtUsername.Text) && txtUsername.Text.Length > 0 ? Visibility.Collapsed : Visibility.Visible;
        }


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


        private void textConfirmPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtConfirmPassword.Focus();
        }
        private void txtConfirmPassword_TextChanged(object sender, RoutedEventArgs e)
        {
            textConfirmPassword.Visibility = !string.IsNullOrEmpty(txtConfirmPassword.Password) && txtConfirmPassword.Password.Length > 0 ? Visibility.Collapsed : Visibility.Visible;
        }


        private void Create_account_click(object sender, EventArgs e)
        {
            sqlcon.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM [User_tb] WHERE Email = '" + txtEmail.Text + "'", sqlcon);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);

            if (dtbl.Rows[0][0].ToString() == "0")
            {
                cmd = new SqlCommand("Insert into [User_tb] values('" + txtFirstName.Text + "','" + txtLastName.Text + "','" + txtEmail.Text + "','" + txtUsername.Text + "','" + txtPassword.Password + "')", sqlcon);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Account created!");
                sqlcon.Close();

            }

            else
            {
                MessageBox.Show("A user with this email is already registered!");
                sqlcon.Close();
            }
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
