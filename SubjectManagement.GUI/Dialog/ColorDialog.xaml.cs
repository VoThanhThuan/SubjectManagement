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
using SubjectManagement.Common.Result;

namespace SubjectManagement.GUI.Dialog
{
    /// <summary>
    /// Interaction logic for ColorDialog.xaml
    /// </summary>
    public partial class ColorDialog : Window
    {
        public ColorDialog()
        {
            InitializeComponent();
        }

        public string _Color { get; set; }

        public new MyDialogResult.Result DialogResult = MyDialogResult.Result.Close;

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _Color = $"{ClrPcker_ColorText.Color}";
            DialogResult = MyDialogResult.Result.Ok;
            this.Close();

        }
    }
}
