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
    public partial class MessageDialog : Window
    {
        public MessageDialog()
        {
            InitializeComponent();
        }

        public new MyDialogResult.Result DialogResult = MyDialogResult.Result.Close;

        private void Btn_OK_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = MyDialogResult.Result.Ok;
            this.Close();
        }

        private void Btn_Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = MyDialogResult.Result.Cancel;
            this.Close();
        }

        private void Btn_Close_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = MyDialogResult.Result.Close;
            this.Close();
        }

        private void MessageDialog_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }


    }

}
