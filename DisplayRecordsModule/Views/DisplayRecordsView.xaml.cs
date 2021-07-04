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
using DisplayRecordsModule.ViewModels;

namespace DisplayRecordsModule.Views
{
    /// <summary>
    /// Interaction logic for DisplayRecordsView.xaml
    /// </summary>
    public partial class DisplayRecordsView : UserControl
    {
        public DisplayRecordsView(DisplayRecordsViewModel displayRecordsViewModel)
        {
            InitializeComponent();
            DataContext = displayRecordsViewModel;
        }
    }
}
