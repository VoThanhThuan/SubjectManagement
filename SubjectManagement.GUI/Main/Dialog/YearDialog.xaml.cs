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

namespace SubjectManagement.GUI.Main.Dialog
{
    /// <summary>
    /// Interaction logic for YearDialog.xaml
    /// </summary>
    public partial class YearDialog : Window
    {
        public YearDialog()
        {
            InitializeComponent();
            CreateYear();
        }

        private int _year = 2020;
        public string YearResult { get; set; } = "2020";
        private void CreateYear()
        {
            wp_year.Children.Clear();
            for (var i = _year; i < _year+12; i++)
            {
                var btn = new Button()
                {
                    Content = i,
                    BorderBrush = null,
                    Width = 70,
                    Height = 40,
                    Margin = new Thickness(5)
                };
                btn.Click += Btn_Click;
                wp_year.Children.Add(btn);
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            tbl_Year.Text = $"{((Button) sender).Content}";
        }

        private void Btn_Previous_OnClick(object sender, RoutedEventArgs e)
        {
            _year -= 10;
            if (_year < 2000) return;
                CreateYear();
        }

        private void Btn_Next_OnClick(object sender, RoutedEventArgs e)
        {
            _year += 10;
            CreateYear();
        }

        private void Btn_Accept_OnClick(object sender, RoutedEventArgs e)
        {
            YearResult = tbl_Year.Text;
        }
    }
}
