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

namespace SequenceAllignProject.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void Redirect_to_site(object sender, MouseButtonEventArgs e)
        {
            string url = "https://medium.com/analytics-vidhya/sequence-alignment-and-the-needleman-wunsch-algorithm-710c7b1a23a4";
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        }

        private void Learn_more(object sender, MouseButtonEventArgs e)
        {
            string url = "https://towardsdatascience.com/how-to-process-bio-sequences-for-use-in-data-science-7740c199c412";
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        }
       
        private void View_seq(object sender, MouseButtonEventArgs e)
        {              
                SeeRegisteredSequences seeregseq = new SeeRegisteredSequences();              
                seeregseq.Show(); 
        }
        private void Check_for_update(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("You are already on the newest version of the APP!");
        }
        private void Seq_All(object sender, MouseButtonEventArgs e)
        {
            SeqAllgn seeregseq = new SeqAllgn();
            seeregseq.Show(); 
        }
        
        private void View_made_seq(object sender, MouseButtonEventArgs e)
        {
            ViewMadeAllign seeregseq = new ViewMadeAllign();
            seeregseq.Show();
        }
    }
}
