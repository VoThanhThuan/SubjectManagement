using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using SubjectManagement.GUI.Constant;
using SubjectManagement.GUI.Controller;
using SubjectManagement.GUI.Dialog;
using Path = System.IO.Path;

namespace SubjectManagement.GUI.Dialog
{
    /// <summary>
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow(bool isMainApp = false)
        {
            InitializeComponent();
            IsInMainApp = isMainApp;
            LoadDefaultColor();
            LoadFaculty();
        }

        private bool IsInMainApp { get; set; } = false;

        public new MyDialogResult.Result DialogResult = MyDialogResult.Result.Close;

        private void RunOrtherApp()
        {
            var proc = new Process();
            var exist = Process.GetProcessesByName("StickerPOP");
            var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\tool\\StickerPOP.exe";
            if (!File.Exists(path) || exist.Length != 0) return;
            proc.StartInfo.FileName = path;
            proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(path);
            proc.Start();
            //Process.Start($"{path}\\tool\\StickerPOP.exe");
        }

        private void LoadFaculty()
        {
            if (IsInMainApp == false) return;
            var faculty = new FacultyController();
            faculty.GetFaculty(cbb_Faculty);
            faculty.GetFaculty(cbb_FacultyOfClass);
            faculty.GetClass(cbb_Class);
            faculty.GetClass(cbb_ClassLock);
        }

        private void LoadDefaultColor()
        {
            var setting = new SettingApp();

            var colorStart = (Color)ColorConverter.ConvertFromString(setting.ColorStart);
            var colorEnd = (Color)ColorConverter.ConvertFromString(setting.ColorEnd);

            ClrPcker_BackgroundLeft.Color = colorStart;
            ClrPcker_BackgroundRight.Color = colorEnd;
            //var linear = new LinearGradientBrush() { StartPoint = new Point(0, 0), EndPoint = new Point(1, 1) };
            //linear.GradientStops.Add(new GradientStop(colorStart, 0.0));
            //linear.GradientStops.Add(new GradientStop(colorEnd, 1.0));

            //TitleSimulator.Background = linear;
        }

        private void SetColorTitleSimulator()
        {
            //var color = (Color)ColorConverter.ConvertFromString("#FFDFD991");
            //var c = new GradientStopCollection();
            //c.Add(new GradientStop(ClrPcker_BackgroundLeft.Color, 0.0));
            //c.Add(new GradientStop(ClrPcker_BackgroundRight.Color, 1.0));
            var linear = new LinearGradientBrush() { StartPoint = new Point(0, 0), EndPoint = new Point(1, 1) };
            linear.GradientStops.Add(new GradientStop(ClrPcker_BackgroundLeft.Color, 0.0));
            linear.GradientStops.Add(new GradientStop(ClrPcker_BackgroundRight.Color, 1.0));

            TitleSimulator.Background = linear;

        }

        private SettingApp GetInfor()
        {
            var lcolor = ClrPcker_BackgroundLeft.Color;
            var rcolor = ClrPcker_BackgroundRight.Color;

            var infor = new SettingApp()
            {
                ColorStart = $"{lcolor}",
                ColorEnd = $"{rcolor}",
                TextColor = $"{btn_OpenColorDialog.Content}"
            };
            return infor;
        }

        private void Btn_Accept_OnClick(object sender, RoutedEventArgs e)
        {
            var write = new SettingController();
            write.WriteSetting(GetInfor());
            DialogResult = MyDialogResult.Result.Ok;
            this.Close();
        }

        private void Btn_OpenColorDialog_OnClick(object sender, RoutedEventArgs e)
        {
            var cd = new ColorDialog();
            cd.ShowDialog();
            if (cd.DialogResult != MyDialogResult.Result.Ok) return;
            btn_OpenColorDialog.Content = cd._Color;
            var cbrush = new BrushConverter();
            var color = (Brush)cbrush.ConvertFromString(cd._Color);
            btn_OpenColorDialog.Foreground = color;
        }

        private void Btn_AddFaculty_OnClick(object sender, RoutedEventArgs e)
        {
            var add = new FacultyController();
            add.AddFaculty(tbx_NameFaculty.Text);
            LoadFaculty();
        }

        private void Btn_RemoveFaculty_OnClick(object sender, RoutedEventArgs e)
        {
            if (cbb_Faculty.SelectedIndex < 0) return;
            var remove = new FacultyController();
            remove.RemoveFaculty(((Faculty)cbb_Faculty.SelectedValue).ID);
            LoadFaculty();
        }

        private void Btn_AddClass_OnClick(object sender, RoutedEventArgs e)
        {
            if (cbb_FacultyOfClass.SelectedIndex < 0) return;
            var add = new FacultyController();
            var clss = new Class
            {
                CodeClass = tbx_CodeClass.Text,
                Name = tbx_NameClass.Text,
                Year = DateTime.Parse($"01/01/{tbl_Year.Text}")
            };
            add.AddClass(clss, ((Faculty)cbb_FacultyOfClass.SelectedValue).ID);
            LoadFaculty();
        }

        private void Btn_RemoveClass_OnClick(object sender, RoutedEventArgs e)
        {
            if (cbb_Class.SelectedIndex < 0) return;
            var remove = new FacultyController();
            remove.RemoveClass(((Class)cbb_Class.SelectedValue).ID);
            LoadFaculty();
        }

        private void Btn_CreateConnect_OnClick(object sender, RoutedEventArgs e)
        {
            var setting = new SettingController();
            var connectString = "";
            connectString = cbb_ModeCreate.SelectedIndex == 0 ? setting.CreateConnectString(true,tbx_ServerName.Text, tbx_DatabaseName.Text) : setting.CreateConnectString(false, tbx_ServerName.Text, tbx_DatabaseName.Text,tbx_Username.Text, tbx_Password.Password);
            setting.WriteConnectString(connectString);
        }

        private void Btn_TestConnect_OnClick(object sender, RoutedEventArgs e)
        {
            var setting = new SettingController();
            setting.ReadConnectString();
            setting.TestConnect();
        }

        private void Btn_ChooseYear_OnClick(object sender, RoutedEventArgs e)
        {
            var yd = new YearDialog() { Owner = this };
            yd.ShowDialog();
            if (yd.DialogResult == MyDialogResult.Result.Ok)
                tbl_Year.Text = yd.tbl_Year.Text;
        }

        private void Btn_StickerPOP_OnClick(object sender, RoutedEventArgs e)
        {
            RunOrtherApp();
        }

        private void Cbb_ClassLock_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbb_ClassLock.SelectedIndex < 0) return;
            var clss = (Class)cbb_ClassLock.SelectedValue;
            if (clss.CanEdit == false)
            {
                btn_ClassLock.Background = new SolidColorBrush(Color.FromRgb(21, 146, 255));
                btn_ClassLock.Content = "Mở khóa";
            }
            else
            {
                btn_ClassLock.Background = new SolidColorBrush(Colors.Red);
                btn_ClassLock.Content = "khóa lớp";
            }
        }

        private void Btn_ClassLock_OnClick(object sender, RoutedEventArgs e)
        {
            if (cbb_ClassLock.SelectedIndex < 0) return;

            var lockClass = new FacultyController();
            var clss = (Class)cbb_ClassLock.SelectedValue;
            lockClass.UnlockClass(clss.ID, !clss.CanEdit);
            lockClass.GetClass(cbb_ClassLock);
        }

        private void ModeCreate_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbb_ModeCreate.SelectedIndex == 0)
            {
                tbx_Username.IsEnabled = false;
                tbx_Password.IsEnabled = false;
            }
            else
            {
                tbx_Username.IsEnabled = true;
                tbx_Password.IsEnabled = true;
            }
        }
    }
}
