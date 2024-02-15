using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Library;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace TaiSan.DieuChuyen
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objNhanVien { get; set; }
        public int?  MaDC{ get; set; }
        MasterDataContext db;
        string SoPhieu = "";
        tsDieuChuyen objDC;
        public int[] ListTS { get; set; }
        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
            lookTaiSan.EditValueChanged += new EventHandler(lookTaiSan_EditValueChanged);
            lookLoaiCT.EditValueChanged += new EventHandler(lookLoaiCT_EditValueChanged);
            btnSoChungTu.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(btnSoChungTu_ButtonClick);
        }

        void lookLoaiCT_EditValueChanged(object sender, EventArgs e)
        {
            var LookLCT = (LookUpEdit)sender;
            int MaLCT = (int)LookLCT.EditValue;
            gvChungTu.SetFocusedRowCellValue("LoaiCT", MaLCT);
            if (gvChungTu.GetFocusedRowCellValue("MaCT") != null)
            {
                gvChungTu.SetFocusedRowCellValue("SoCT", null);
                gvChungTu.SetFocusedRowCellValue("MaCT", null);
            }
        }

        void btnSoChungTu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvChungTu.GetFocusedRowCellValue("LoaiCT") == null)
                return;
            SeachChungTu();
        }

        void lookTaiSan_EditValueChanged(object sender, EventArgs e)
        {
            var ts = (LookUpEdit)sender;
            if (ts.EditValue != null)
            {
                var objTS=db.tsTaiSans.Single(p=>p.ID==(int)ts.EditValue);
                gvDieuChuyenCT.SetFocusedRowCellValue("IsNoiBoC",objTS.IsNoiBo.GetValueOrDefault());
            }
        }


        void SeachChungTu()
        {
            frmSelectChungTu frm = new frmSelectChungTu();
            frm.MaLCT = Convert.ToInt32(gvChungTu.GetFocusedRowCellValue("LoaiCT"));
            frm.objnhanvien = objNhanVien;
            frm.ShowDialog();
            gvChungTu.SetFocusedRowCellValue("SoCT", frm.SoCT);
            gvChungTu.SetFocusedRowCellValue("MaCT", frm.MaCT);
        }

        void AddNew()
        {
            db.tsDieuChuyen_TaoSoPhieu(ref SoPhieu);
            txtSoDC.Text = SoPhieu;
            txtLyDo.Text = "";
            dateNgayDC.EditValue = DateTime.Now;
            txtLyDo.Text = "";
            txtNguoiBG.Text = "";
            txtNguoiDC.Text = "";
            txtNguoiTN.Text = "";
            Enable(true);
        }

        void Enable(bool bl)
        {
            xtraTabControl2.Enabled = bl;
            groupBox1.Enabled = bl;
            itemDelete.Enabled = bl;
            itemNew.Enabled = !bl;
            itemSave.Enabled = bl;
            itemEdit.Enabled = bl;
            itemStandBy.Enabled = bl;
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                lookTaiSan.DataSource = db.tsTaiSans.Select(p => new { p.MaTS, p.TenTS, p.ID });
                lookLoaiCT.DataSource = db.tsLoaiChungTus;
                lookDVSDC.DataSource = db.tsDonViSuDungs.Select(p => new { p.MaDV, p.ID, p.TenDV });
                lookDVSDN.DataSource = db.tsDonViSuDungs.Select(p => new { p.MaDV, p.ID, p.TenDV });
                lookMatBangN.DataSource = db.mbMatBangs.Select(p =>new { p.MaMB,p.MaSoMB});
                lookMatBangC.DataSource = db.mbMatBangs.Select(p => new { p.MaMB, p.MaSoMB });

                ///Load 1
                if (MaDC == null)
                {
                    AddNew();
                    objDC = new tsDieuChuyen();
                    db.tsDieuChuyens.InsertOnSubmit(objDC);
                }
                else
                {
                    objDC = db.tsDieuChuyens.SingleOrDefault(p => p.ID == MaDC);
                    txtLyDo.Text = objDC.LyDo;
                    txtNguoiBG.Text = objDC.NguoiBanGiao;
                    txtNguoiDC.Text = objDC.NguoiDieuChuyen;
                    txtNguoiTN.Text = objDC.NguoiTiepNhan;
                    txtSoDC.Text = objDC.SoDC;
                    dateNgayDC.EditValue = objDC.NgayDC;
                }

                if (ListTS != null)
                {
                    tsTaiSan obj;
                    for (int i = 0; i < ListTS.Length; i++)
                    {
                        tsDieuChinhChiTiet item = new tsDieuChinhChiTiet();
                        obj = db.tsTaiSans.Single(p => p.ID == ListTS[i]);
                        item.MaDVSDC = obj.MaDVSD;
                        item.IsNoiBoC = obj.IsNoiBo;
                        item.MaMBC = obj.MaMB;
                        item.MaTS = ListTS[i];
                        objDC.tsDieuChinhChiTiets.Add(item);
                    }

                }

                gcDieuChuyenCT.DataSource = objDC.tsDieuChinhChiTiets;
                gcChungTu.DataSource = objDC.tsdcChungTuThamChieus;

            }
            catch { }
            finally
            {
                wait.Close();
            }
        }

        void SaveData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                if (MaDC == null)
                {
                    objDC.MaNV = objNhanVien.MaNV;
                    objDC.NgayTao = DateTime.Now;
                }
                else
                {
                    objDC = db.tsDieuChuyens.SingleOrDefault(p => p.ID == MaDC);
                    objDC.MaNVCN = objNhanVien.MaNV;
                    objDC.NgayCN = DateTime.Now;
                }
                objDC.SoDC = txtSoDC.Text.Trim();
                objDC.NgayDC = (DateTime?)dateNgayDC.EditValue;
                objDC.NguoiTiepNhan = txtNguoiTN.Text.Trim();
                objDC.NguoiDieuChuyen = txtNguoiDC.Text.Trim();
                objDC.NguoiBanGiao = txtNguoiBG.Text.Trim();
                objDC.LyDo = txtLyDo.Text.Trim();
                gvDieuChuyenCT.RefreshData();

                List<tsTaiSan> ListTS = new List<tsTaiSan>();
                for (int i = 0; i < gvDieuChuyenCT.RowCount - 1; i++)
                {
                    var obj = db.tsTaiSans.SingleOrDefault(p => p.ID == (int)gvDieuChuyenCT.GetRowCellValue(i, "MaTS"));
                    ListTS.Add(obj);
                }
                for (int i = 0; i < ListTS.Count; i++)
                {
                    ListTS[i].IsNoiBo = (bool?)gvDieuChuyenCT.GetRowCellValue(i, "IsNoiBoN");
                    ListTS[i].MaDVSD = (int?)gvDieuChuyenCT.GetRowCellValue(i, "MaDVSDN");
                    ListTS[i].MaMB = (int?)gvDieuChuyenCT.GetRowCellValue(i, "MaMBN");
                }

                db.SubmitChanges();

            }
            catch { }
            finally
            {
                wait.Close();
            }
            Enable(false);
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddNew();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveData();    
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemStandBy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Enable(false);
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Enable(true);
        }

        private void lookTaiSan_EditValueChanged_1(object sender, EventArgs e)
        {
            var lookTS = (LookUpEdit)sender;
            if (lookTS.EditValue == null)
                return;
            var objTS = db.tsTaiSans.SingleOrDefault(p => p.ID == (int)lookTS.EditValue);
            gvDieuChuyenCT.SetFocusedRowCellValue("MaDVSDC", objTS.MaDVSD);
            gvDieuChuyenCT.SetFocusedRowCellValue("MaMBC", objTS.MaMB);
        }
    }
}
