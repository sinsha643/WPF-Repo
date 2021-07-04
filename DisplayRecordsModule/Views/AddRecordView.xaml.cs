using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DisplayRecordsModule.Interfaces.Views;
using DisplayRecordsModule.ViewModels;

namespace DisplayRecordsModule.Views
{
    /// <summary>
    /// Interaction logic for AddRecordView.xaml
    /// </summary>
    public partial class AddRecordView : IAddRecordView
    {
        public AddRecordView(IAddViewModel addRecordViewModel)
        {
            InitializeComponent();
            DataContext = addRecordViewModel;
        }
    }
}
