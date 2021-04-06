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

namespace SubjectManagement.GUI.Member
{
    /// <summary>
    /// Interaction logic for ViewAlternativeSubject.xaml
    /// </summary>
    public partial class ViewAlternativeSubject : Window
    {
        public ViewAlternativeSubject(Class _class, Subject subject)
        {
            InitializeComponent();
            _Class = _class;
            loadListAlter(subject.ID);
            tbl_Class.Text = $"{tbl_Class.Text} {subject.Name}";
        }

        private Class _Class { get; init; }

        public void loadListAlter(Guid idSubject)
        {
            var loadAlter = new AlternativeController(_Class);
            dg_ListSubject.ItemsSource = loadAlter.GetAlternative(idSubject);
        }
    }
}
