using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;


namespace CustomerManager
{
    /// <summary>
    /// Interaction logic for EditDialog.xaml
    /// </summary>
    public partial class EditDialog : ThemedWindow
    {
        public EditDialog()
        {
            InitializeComponent();
        }

        private void Name_Validate(object sender, DevExpress.Xpf.Editors.ValidationEventArgs e)
        {
              if (e.Value == null) return;
            string val = e.Value.ToString();
            if (val == null || val == string.Empty) return;
            if (!Regex.IsMatch(val, "^[\u4e00-\u9fa5]{0,}$"))
            {
                e.IsValid = false;
                e.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Information;
                e.ErrorContent = "用户名必需由汉字组成.";
            }
        }

        private void Sid_Validate(object sender, DevExpress.Xpf.Editors.ValidationEventArgs e)
        {
            if (e.Value == null) return;
            string val = e.Value.ToString();
            if (val == null || val == string.Empty) return;
            if (!Regex.IsMatch(val, @"^[1-9]\d{5}(18|19|([23]\d))\d{2}((0[1-9])|(10|11|12))(([0-2][1-9])|10|20|30|31)\d{3}[0-9Xx]$"))
            {
                e.IsValid = false;
                e.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Information;
                e.ErrorContent = "身份证必需是18的有效格式";
            }
        }
    }
}
