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
    /// Interaction logic for SeqAllgn.xaml
    /// </summary>
    public partial class SeqAllgn : Window
    {
        public SeqAllgn()
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
        public void SetTextBoxValue(string value)
        {
            txtString.Text = value;
        }

        private void Chose_from_db(object sender, EventArgs e)
        {
            var choseFromDB = new ChoseFromDB(this);
            choseFromDB.Show();
            choseFromDB.Load();
        }
        
        private void Add_sequence_from_database(object sender, EventArgs e)
        {
            var choseFromDB = new Add_sequence_from_database(this);
            choseFromDB.Show();
            choseFromDB.Load();
        }

        private void Allign_seq(object sender, EventArgs e)
        {
            
            string refSeq = txtString.Text;
            string alignSeq = txtString_seq.Text;

            int refSeqCnt = refSeq.Length + 1;
            int alineSeqCnt = alignSeq.Length + 1;

            int[,] scoringMatrix = new int[alineSeqCnt, refSeqCnt];

            //Initialization Step - filled with 0 for the first row and the first column of matrix
            for (int i = 0; i < alineSeqCnt; i++)
            {
                scoringMatrix[i, 0] = 0;
            }

            for (int j = 0; j < refSeqCnt; j++)
            {
                scoringMatrix[0, j] = 0;
            }

            //Matrix Fill Step
            for (int i = 1; i < alineSeqCnt; i++)
            {
                for (int j = 1; j < refSeqCnt; j++)
                {
                    int scroeDiag = 0;
                    if (refSeq.Substring(j - 1, 1) == alignSeq.Substring(i - 1, 1))
                        scroeDiag = scoringMatrix[i - 1, j - 1] + 2;
                    else
                        scroeDiag = scoringMatrix[i - 1, j - 1] + -1;

                    int scroeLeft = scoringMatrix[i, j - 1] - 2;
                    int scroeUp = scoringMatrix[i - 1, j] - 2;

                    int maxScore = Math.Max(Math.Max(scroeDiag, scroeLeft), scroeUp);

                    scoringMatrix[i, j] = maxScore;
                }
            }

            //Traceback Step
            char[] alineSeqArray = alignSeq.ToCharArray();
            char[] refSeqArray = refSeq.ToCharArray();

            string AlignmentA = string.Empty;
            string AlignmentB = string.Empty;
            int m = alineSeqCnt - 1;
            int n = refSeqCnt - 1;
            while (m > 0 && n > 0)
            {
                int scroeDiag = 0;

                //Remembering that the scoring scheme is +2 for a match, -1 for a mismatch and -2 for a gap
                if (alineSeqArray[m - 1] == refSeqArray[n - 1])
                    scroeDiag = 2;
                else
                    scroeDiag = -1;

                if (m > 0 && n > 0 && scoringMatrix[m, n] == scoringMatrix[m - 1, n - 1] + scroeDiag)
                {
                    AlignmentA = refSeqArray[n - 1] + AlignmentA;
                    AlignmentB = alineSeqArray[m - 1] + AlignmentB;
                    m = m - 1;
                    n = n - 1;
                }
                else if (n > 0 && scoringMatrix[m, n] == scoringMatrix[m, n - 1] - 2)
                {
                    AlignmentA = refSeqArray[n - 1] + AlignmentA;
                    AlignmentB = "-" + AlignmentB;
                    n = n - 1;
                }
                else //if (m > 0 && scoringMatrix[m, n] == scoringMatrix[m - 1, n] + -2)
                {
                    AlignmentA = "-" + AlignmentA;
                    AlignmentB = alineSeqArray[m - 1] + AlignmentB;
                    m = m - 1;
                }
                
                txtString_Type.Text = (AlignmentA.ToString() + Environment.NewLine + AlignmentB.ToString());
            }
            
        }
        private void Add_to_db(object sender, EventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
            SqlConnection sqlcon5 = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");

            string Sequence_1_string = txtString.Text;
            string Sequence_2_string = txtString_seq.Text;
            string Description = txtDexcription.Text;
            string Result = txtString_Type.Text;
            string date = DateTime.UtcNow.ToString("yyyy-mm-dd");
            string SeqNmae1ID,  SeqNmae2ID, SequenceID1, SequenceID2;

            sqlcon.Open();
            
            SqlConnection sqlcon1 = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
            using (SqlCommand command1 = new SqlCommand("SELECT * FROM Sequence_Name WHERE String = '" + txtString.Text + "'", sqlcon1))
            {

                DataSet ds1 = new DataSet();
                using (SqlDataAdapter da1 = new SqlDataAdapter(command1))
                da1.Fill(ds1);

                if (ds1.Tables[0].Rows.Count != 0)
                {
                    //Get the result of the first row        
                    DataRow dr = ds1.Tables[0].Rows[0];

                    //Get the value of the column in the first row        
                    SeqNmae1ID = dr["ID"].ToString();
                    
                }
                else
                {
                    MessageBox.Show("Something went wrong");
                    SeqNmae1ID = "0";
                }
            }

            SqlConnection sqlcon3 = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
            using (SqlCommand command3 = new SqlCommand("SELECT * FROM Sequence WHERE NameID = '" + SeqNmae1ID + "'", sqlcon3))
            {

                DataSet ds1 = new DataSet();
                using (SqlDataAdapter da1 = new SqlDataAdapter(command3))
                    da1.Fill(ds1);

                //Get the result of the first row        
                DataRow dr = ds1.Tables[0].Rows[0];

                //Get the value of the column in the first row        
                SequenceID1 = dr["ID"].ToString();
            }
            SqlConnection sqlcon2 = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
            using (SqlCommand command1 = new SqlCommand("SELECT * FROM Sequence_Name WHERE String = '" + txtString_seq.Text + "'", sqlcon1))
            {

                DataSet ds1 = new DataSet();
                using (SqlDataAdapter da1 = new SqlDataAdapter(command1))
                    da1.Fill(ds1);

                if (ds1.Tables[0].Rows.Count != 0)
                {
                    //Get the result of the first row        
                    DataRow dr = ds1.Tables[0].Rows[0];

                    //Get the value of the column in the first row        
                    SeqNmae2ID = dr["ID"].ToString();

                }
                else
                {
                    MessageBox.Show("Something went wrong");
                    SeqNmae2ID = "0";
                }
            }
         
            SqlConnection sqlcon4 = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
            using (SqlCommand command4 = new SqlCommand("SELECT * FROM Sequence WHERE NameID = '" + SeqNmae2ID + "'", sqlcon4))
            {

                DataSet ds1 = new DataSet();
                using (SqlDataAdapter da1 = new SqlDataAdapter(command4))
                    da1.Fill(ds1);

                //Get the result of the first row        
                DataRow dr = ds1.Tables[0].Rows[0];

                //Get the value of the column in the first row        
                SequenceID2 = dr["ID"].ToString();
            }


            string query3 = "INSERT INTO [Alignments_tb] (UserID,Query_sting_ID,Subject_string_ID,Result,Description,Date) VALUES ('" +1+ "', '" + SequenceID1 + "', '" + SequenceID2 + "', '" + Result + "', '" + Description + "', '" + DateTime.Now + "')";
            SqlCommand create_command4 = new SqlCommand(query3, sqlcon);
            create_command4.ExecuteNonQuery();
            sqlcon.Close();
            MessageBox.Show("Alignment added!");
            this.Hide();

            
        }

        
        private void Exit(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Check_DB(object sender, EventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
            SqlCommand cmd;
            sqlcon.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM [Sequence_Name] WHERE String = '" + txtString.Text + "'", sqlcon);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);

            if (dtbl.Rows[0][0].ToString() == "1")
            {
                MessageBox.Show("This sequence is already in our database!");
                sqlcon.Close();
            }

            else
            {
                MessageBox.Show("This is a new sequence!");
                sqlcon.Close();
            }
        }
        private void Check_DB2(object sender, EventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection("Data Source=DESKTOP-VRL5B55\\SQLEXPRESS;Initial Catalog=SequenceAlign;Integrated Security=True");
            SqlCommand cmd;
            sqlcon.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM [Sequence_Name] WHERE String = '" + txtString_seq.Text + "'", sqlcon);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);

            if (dtbl.Rows[0][0].ToString() == "1")
            {
                MessageBox.Show("This sequence is already in our database!");
                sqlcon.Close();
            }

            else
            {
                MessageBox.Show("This is a new sequence!");
                sqlcon.Close();
            }
        }


        private void add_sequence1(object sender, RoutedEventArgs e)
        {
            AddNewSeq addnewseq = new AddNewSeq();
            addnewseq.Show();
            addnewseq.txtString.Text = txtString.Text;
        }
        private void add_sequence2(object sender, RoutedEventArgs e)
        {
            AddNewSeq addnewseq = new AddNewSeq();
            addnewseq.Show();
            addnewseq.txtString.Text = txtString_seq.Text;
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            txtString.Text = "";
            txtString_seq.Text = "";
            txtString_Type.Text = "";
            txtDexcription.Text = "";
        }
    }
}
