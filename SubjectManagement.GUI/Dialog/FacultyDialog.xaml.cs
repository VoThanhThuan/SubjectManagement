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
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Controller;

namespace SubjectManagement.GUI.Dialog
{
    /// <summary>
    /// Interaction logic for FacultyDialog.xaml
    /// </summary>
    public partial class FacultyDialog : Window
    {
        public FacultyDialog()
        {
            InitializeComponent();
            LoadCbbFaculty();
        }
        public new MyDialogResult.Result DialogResult = MyDialogResult.Result.Close;

        public Class _Class { get; set; }
        public int _IdFaculty { get; set; }

        private void LoadCbbFaculty()
        {
            var faculty = new FacultyController();
            faculty.GetFaculty(cbb_Faculty);
        }

        private void cbb_Faculty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var f = (Faculty)cbb_Faculty.SelectedValue;
            var faculty = new FacultyController();
            faculty.GetClassInFaculty(cbb_Class, f.ID);

        }
        private void Cbb_Class_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbb_Class.SelectedIndex >= 0)
                btn_accept.IsEnabled = true;
        }
        private void Btn_accept_OnClick(object sender, RoutedEventArgs e)
        {
            _Class = ((Class) cbb_Class.SelectedValue);
            _IdFaculty = ((Faculty)cbb_Faculty.SelectedValue).ID;

            DialogResult = MyDialogResult.Result.Ok;
            this.Close();
        }


    }
}
