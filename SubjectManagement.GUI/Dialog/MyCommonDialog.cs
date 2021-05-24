using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using SubjectManagement.Common.Result;

namespace SubjectManagement.GUI.Dialog
{
    public static class MyCommonDialog
    {
        public static Color ColorMessage = Colors.Red;

        public static MyDialogResult.Result MessageDialog(string message)
        {
            return Message("Thông Báo", message, ColorMessage);
        }
        public static MyDialogResult.Result MessageDialog(string message, Color color)
        {
            return Message("Thông Báo", message, color);
        }
        public static MyDialogResult.Result MessageDialog(string title, string message)
        {
            return Message(title, message, ColorMessage);
        }
        public static MyDialogResult.Result MessageDialog(string title, string message, Color color)
        {
            return Message(title, message, color);
        }

        private static MyDialogResult.Result Message(string title, string message, Color color)
        {
            var mess = new MessageDialog()
            {
                tbl_Title = { Text = title },
                tbl_Message = { Text = message },
                Topmost = true,
                title_color = { Background = new SolidColorBrush(color) }
            };
            mess.ShowDialog();
            return mess.DialogResult;
        }
    }

    public static class MCD
    {
        public static MyDialogResult.Result MessageDialog(string message)
        {
            return MyCommonDialog.MessageDialog("Thông Báo", message, MyCommonDialog.ColorMessage);
        }
        public static MyDialogResult.Result MessageDialog(string message, Color color)
        {
            return MyCommonDialog.MessageDialog("Thông Báo", message, color);
        }
        public static MyDialogResult.Result MessageDialog(string title, string message)
        {
            return MyCommonDialog.MessageDialog(title, message, MyCommonDialog.ColorMessage);
        }
        public static MyDialogResult.Result MessageDialog(string title, string message, Color color)
        {
            return MyCommonDialog.MessageDialog(title, message, color);
        }
    }
}
