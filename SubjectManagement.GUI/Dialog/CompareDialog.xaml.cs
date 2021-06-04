using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Controller;
using SubjectManagement.GUI.Main;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SubjectManagement.GUI.Dialog
{
    /// <summary>
    /// Interaction logic for CompareDialog.xaml
    /// </summary>
    public partial class CompareDialog : Window
    {
        public CompareDialog(Class _class, int idFaculty)
        {
            InitializeComponent();
            _IdFaculty = idFaculty;
            _Class = _class;
            tbl_ClassCurrent.Text = _Class.CodeClass;
            LoadClass();
        }

        private int _IdFaculty { get; init; }

        private Class _Class { get; init; }
        public Class _ClassCompare { get; set; }


        private void LoadClass()
        {
            var c = new FacultyController();
            //c.GetClassDifferentClass(cbb_Class_2, _IdFaculty, _Class);
            c.GetDifferentClassOlder(cbb_Class_2, _IdFaculty, _Class);
        }

        private void Cbb_Class_2_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbb_Class_2.SelectedIndex < 0) return;
            btn_Review.IsEnabled = true;
            btn_Accept.IsEnabled = true;
        }

        private void Btn_Review_OnClick(object sender, RoutedEventArgs e)
        {
            var _class2 = (Class)cbb_Class_2.SelectedValue;
            if (_Class == _class2)
            {
                var mess = new MessageDialog()
                {
                    tbl_Title = { Text = "o.O hở ?" },
                    tbl_Message = { Text = $"Tại sao bạn lại muốn so sánh 2 thứ giống nhau ?" },
                    title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                    Topmost = true
                };
                mess.ShowDialog();
                return;
            }
            var review = new ReviewTwoTables(_Class, _class2);
            review.ShowDialog();
        }

        private void Btn_Accept_OnClick(object sender, RoutedEventArgs e)
        {
            _ClassCompare = (Class)cbb_Class_2.SelectedValue;
            this.DialogResult = true;
        }
    }
}
