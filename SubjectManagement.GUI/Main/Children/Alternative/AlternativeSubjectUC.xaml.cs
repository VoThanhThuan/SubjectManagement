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
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Controller;

namespace SubjectManagement.GUI.Main.Children.Alternative
{
    /// <summary>
    /// Interaction logic for AlternativeSubjectUC.xaml
    /// </summary>
    public partial class AlternativeSubjectUC : UserControl
    {
        public AlternativeSubjectUC(Class _class)
        {
            InitializeComponent();
            _Class = _class;
            LoadSubjectClass();
        }

        private Class _Class { get; init; }

        private void LoadSubjectClass()
        {
            var clss = new FacultyController();
            clss.GetClass(cbb_Class, _Class.ID);

            var subAlter = new SubjectController(_Class);
            dg_ListAllSubject.ItemsSource = subAlter.GetSubjectClass();
            //var alter = new AlternativeController(_Class);
            //alter.LoadSubjectClass(dg_ListAllSubject);
        }

        private void loadListAlter()
        {
            var loadAlter = new AlternativeController(_Class);
            var subject = (Subject)dg_ListAllSubject.SelectedValue;

            dg_ListSubjectAlternative.ItemsSource = loadAlter.GetAlternative(subject.ID);
        }

        private void Btn_Add_OnClick(object sender, RoutedEventArgs e)
        {
            if((dg_ListAllSubject.SelectedIndex < 0) || (dg_SubjectOfClassOther.SelectedIndex < 0)) return;
            var subject = (Subject)dg_ListAllSubject.SelectedValue;
            var subjectAlter = (Subject)dg_ListAllSubject.SelectedValue;

            var add = new AlternativeController(_Class);
            add.AddAlternative(subject.ID, subjectAlter.ID);
            loadListAlter();
        }

        private void Cbb_Class_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var clss = (Class) cbb_Class.SelectedValue;
            var subAlter = new SubjectController(clss);
            dg_SubjectOfClassOther.ItemsSource = subAlter.GetSubjectClass();
        }

        private void Btn_Remove_OnClick(object sender, RoutedEventArgs e)
        {
            if(dg_ListSubjectAlternative.SelectedIndex < 0) return;
            var remove = new AlternativeController(_Class);
            var subject = (Subject)dg_ListSubjectAlternative.SelectedValue;
            remove.RemoveAlternative(subject.ID);
            loadListAlter();
        }


        private void Dg_ListAllSubject_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadListAlter();
        }

        private void Btn_AddInCell_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button) (sender);
            
            if ((dg_ListAllSubject.SelectedIndex < 0)) return;
            var subject = (Subject)dg_ListAllSubject.SelectedValue;
            //var subjectAlter = (Subject)dg_ListAllSubject.SelectedValue;

            var add = new AlternativeController(_Class);
            add.AddAlternative(subject.ID, Guid.Parse($"{btn.Tag}"));
            loadListAlter();
        }

        private void Btn_RemoveInCell_OnClick(object sender, RoutedEventArgs e)
        {
            var remove = new AlternativeController(_Class);
            var subject = (Button) sender;
            remove.RemoveAlternative(Guid.Parse($"{subject.Tag}"));
            loadListAlter();
        }
    }
}
