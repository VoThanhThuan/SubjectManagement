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
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Controller;

namespace SubjectManagement.GUI.Dialog
{
    /// <summary>
    /// Interaction logic for AddNewOrOldDialog.xaml
    /// </summary>
    public partial class AddNewOrOldDialog : Window
    {
        public AddNewOrOldDialog(Class _class, int idFaculty)
        {
            InitializeComponent();
            _IdFaculty = idFaculty;
            _Class = _class;
            LoadClass();
        }

        private Class _Class { get; init; }
        private int _IdFaculty { get; init; }
        public bool? IsCopy { get; set; } = null;
        public int IdClassNew { get; set; }

        public bool IsAddOneSubject { get; set; } = true;

        private void LoadClass()
        {
            var load = new FacultyController();
            load.GetDifferentClassOlder(cbb_Class, _IdFaculty, _Class);
        }

        private void Btn_Import_OnClick(object sender, RoutedEventArgs e)
        {
            if(cbb_Class.SelectedIndex < 0) return;
            IsCopy = true;
            this.Close();
        }

        private void Btn_New_OnClick(object sender, RoutedEventArgs e)
        {
            IsCopy = false;
            this.Close();
        }

        private void Cbb_Class_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IdClassNew = ((Class) cbb_Class.SelectedValue).ID;
        }

        private void Cbb_ModeAdd_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbb_ModeAdd.SelectedIndex < 0) return;
            IsAddOneSubject = cbb_ModeAdd.SelectedIndex == 0;
        }
    }
}
