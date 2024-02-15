using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using System.Linq;

namespace Library
{
    public class DialogBox
    {
        static string NgonNgu = global::Library.Properties.Settings.Default.NgonNgu;

        public static void Alert(string text)
        {
            switch (NgonNgu)
            {
                case "EN":
                    using (MasterDataContext db = new MasterDataContext())
                    {
                        try
                        {
                            text = db.Translates.FirstOrDefault(p => p.TiengViet == text.Trim()).TiengAnh;
                        }
                        catch { }
                    }
                    break;

                default:
                    break;
            }
            
            XtraMessageBox.Show(text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Success()
        {
            string text = "";
            switch (NgonNgu)
            {
                case "EN":
                    using (MasterDataContext db = new MasterDataContext())
                    {
                        try
                        {
                            text = db.Translates.FirstOrDefault(p => p.TiengViet == text.Trim()).TiengAnh;
                        }
                        catch { }
                    }
                    break;
                default:
                    text = "Dữ liệu đã được cập nhật.";
                    break;
            }
            XtraMessageBox.Show(text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Success(string text)
        {
            switch (NgonNgu)
            {
                case "EN":
                    using (MasterDataContext db = new MasterDataContext())
                    {
                        try
                        {
                            text = db.Translates.FirstOrDefault(p => p.TiengViet == text.Trim()).TiengAnh;
                        }
                        catch { }
                    }
                    break;

                default:
                    break;
            }
            XtraMessageBox.Show(text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Error(string text)
        {
            switch (NgonNgu)
            {
                case "EN":
                    using (MasterDataContext db = new MasterDataContext())
                    {
                        try
                        {
                            text = db.Translates.FirstOrDefault(p => p.TiengViet == text.Trim()).TiengAnh;
                        }
                        catch { }
                    }
                    break;

                default:
                    
                    
                    break;
            }
            XtraMessageBox.Show(text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Warning(string text)
        {
            switch (NgonNgu)
            {
                case "EN":
                    using (MasterDataContext db = new MasterDataContext())
                    {
                        try
                        {
                            text = db.Translates.FirstOrDefault(p => p.TiengViet == text.Trim()).TiengAnh;
                        }
                        catch { }
                    }
                    break;

                default:
                    break;
            }
            XtraMessageBox.Show(text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult Question(string text)
        {
            switch (NgonNgu)
            {
                case "EN":
                    using (MasterDataContext db = new MasterDataContext())
                    {
                        try
                        {
                            text = db.Translates.FirstOrDefault(p => p.TiengViet == text.Trim()).TiengAnh;
                        }
                        catch { }
                    }
                    break;

                default:
                    break;
            }
            return XtraMessageBox.Show(text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static DialogResult QuestionDelete()
        {
            string text = "Bạn có muốn xóa không?";
            switch (NgonNgu)
            {
                case "EN":
                    using (MasterDataContext db = new MasterDataContext())
                    {
                        try
                        {
                            text = db.Translates.FirstOrDefault(p => p.TiengViet == text.Trim()).TiengAnh;
                        }
                        catch { }
                    }
                    break;

                default:
                    text = "Bạn có muốn xóa không?";
                    break;
            }
            return XtraMessageBox.Show(text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static WaitDialogForm WaitingForm()
        {
            string text;
            switch (NgonNgu)
            {
                case "EN":
                    text = "Processing. Please wait...";
                    break;

                default:
                    text = "Ðang xử lý. Vui lòng chờ...";
                    break;
            }
            return new WaitDialogForm(text, "EuroWindow Building");  
        }

        public static WaitDialogForm WaitingForm(string caption)
        {
            switch (NgonNgu)
            {
                case "EN":
                    using (MasterDataContext db = new MasterDataContext())
                    {
                        try
                        {
                            caption = db.Translates.FirstOrDefault(p => p.TiengViet == caption.Trim()).TiengAnh;
                        }
                        catch { }
                    }
                    break;

                default:
                    break;
            }
            return new WaitDialogForm(caption, "EuroWindow Building");  
        }

        public static DialogResult QuestionThuHoi()
        {
            return XtraMessageBox.Show("Bạn có muốn thu hồi tài sản này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
