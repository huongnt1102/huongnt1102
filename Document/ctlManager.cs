using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using ftp = FTP;

namespace Document
{
    public partial class ctlManager : DevExpress.XtraEditors.XtraUserControl
    {
        public tnNhanVien objnhanvien;
        public ctlManager()
        {
            InitializeComponent();
            TranslateLanguage.TranslateUserControl(this, barManager1);
        }

        public ctlManager(short cateID)
        {
            InitializeComponent();

            CateID = cateID;
        }

        MasterDataContext db = new MasterDataContext();
        byte SDBID = 6;
        short CateID = 0;

        public void TaiLieu_Load()
        {
            var wait = DialogBox.WaitingForm();

            try
            {
                DateTime tuNgay = itemTuNgay.EditValue != null ? (DateTime)itemTuNgay.EditValue : DateTime.Now.AddDays(-90);
                DateTime denNgay = itemDenNgay.EditValue != null ? (DateTime)itemDenNgay.EditValue : DateTime.Now;
                CateID = itemLoaiTL.EditValue == null ? (short)0 : (short)itemLoaiTL.EditValue;

                if (objnhanvien.IsSuperAdmin.Value)
                    gcTaiLieu.DataSource = db.docTaiLieu_Select(tuNgay, denNgay, objnhanvien.MaNV, CateID);
                else
                {
                    var GetNhomOfNV = db.pqNhomNhanViens.Where(p => p.IsTruongNhom.Value & p.MaNV == objnhanvien.MaNV).Select(p => p.GroupID).ToList();
                    if (GetNhomOfNV.Count > 0)
                    {
                        var GetListNV = db.pqNhomNhanViens.Where(p => GetNhomOfNV.Contains(p.GroupID)).Select(p => p.MaNV).ToList();

                        gcTaiLieu.DataSource = db.docTaiLieu_Select(tuNgay, denNgay, objnhanvien.MaNV, CateID).Where(p => GetListNV.Contains(p.MaNV.Value));
                    }
                    else
                    {
                        gcTaiLieu.DataSource = db.docTaiLieu_Select(tuNgay, denNgay, objnhanvien.MaNV, CateID).Where(p => p.MaNV == objnhanvien.MaNV);
                    }
                }
            }
            catch { }

            wait.Close();
        }

        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            var loaiTL = db.docLoaiTaiLieus.Select(p => new { p.TenLTL, p.MaLTL, p.STT}).OrderBy(p => p.STT);
            lookLoaiTL.DataSource = loaiTL;
            lookUpLoaiTL.DataSource = loaiTL;
            if (CateID != 0)
                itemLoaiTL.EditValue = CateID;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cmbKyBC.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[3];
            SetDate(3);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            TaiLieu_Load();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            TaiLieu_Load();
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TaiLieu_Load();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmEdit();
            frm.objNV = objnhanvien;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK) TaiLieu_Load();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maTL = (int?)grvTaiLieu.GetFocusedRowCellValue("MaTL");
            if (maTL == null)
            {
                DialogBox.Error("Vui lòng chọn tài liệu");
                return;
            }
            var frm = new frmEdit();
            frm.objNV = objnhanvien;
            frm.MaTL = maTL;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK) TaiLieu_Load();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var indexs = grvTaiLieu.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Error("Vui lòng chọn tài liệu");
                    return;
                }
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa không?") == DialogResult.No) return;
                List<string> files = new List<string>();
                using (var db = new MasterDataContext())
                {
                    foreach (var i in indexs)
                    {
                        var objDoc = db.docTaiLieus.Single(p => p.MaTL == (int)grvTaiLieu.GetRowCellValue(i, "MaTL"));
                        if (objDoc.DuongDan != null)
                            files.Add(objDoc.DuongDan);
                        db.docTaiLieus.DeleteOnSubmit(objDoc);
                    }
                    db.SubmitChanges();
                }

                var cmd = new ftp.FtpClient();
                foreach (var url in files)
                {
                    cmd.Url = url;
                    try
                    {
                        cmd.DeleteFile();
                    }
                    catch { }
                }

                TaiLieu_Load();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void txtXem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvTaiLieu.GetFocusedRowCellValue("DuongDan") == null) return;
            var frm = new ftp.frmDownloadFile();
            frm.FileName = grvTaiLieu.GetFocusedRowCellValue("DuongDan").ToString();
            frm.ShowDialog();
        }

        private void txtTaiVe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvTaiLieu.GetFocusedRowCellValue("DuongDan") == null) return;
            var frm = new ftp.frmDownloadFile();
            frm.FileName = grvTaiLieu.GetFocusedRowCellValue("DuongDan").ToString();
            if (frm.SaveAs())
                frm.ShowDialog();
        }

        private void itemLoaiTL_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
