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

namespace TaiSan.KiemKe
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objNhanVien { get; set; }
        public int?  MaKK{ get; set; }
        MasterDataContext db;
        string SoPhieu = "";
        tsKiemKe objKK;
        public int[] ListTS { get; set; }
       // ntchNghiemThuCanHo objNT;
        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
            lookTaiSan.EditValueChanged += new EventHandler(lookTaiSan_EditValueChanged);
            lookLoaiCT.EditValueChanged += new EventHandler(lookLoaiCT_EditValueChanged);
            btnSoChungTu.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(btnSoChungTu_ButtonClick);
        }

        void btnSoChungTu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvChungTu.GetFocusedRowCellValue("LoaiCT") == null)
                return;
            SeachChungTu();
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

        void lookTaiSan_EditValueChanged(object sender, EventArgs e)
        {
            var ts = (LookUpEdit)sender;
            if (ts.EditValue != null)
            {
                var objTS=db.tsTaiSans.Single(p=>p.ID==(int)ts.EditValue);
                gvChiTiet.SetFocusedRowCellValue("IsNoiBoC",objTS.IsNoiBo.GetValueOrDefault());
            }
        }

        void AddNew()
        {
            db.tsKiemKe_TaoSoPhieu(ref SoPhieu);
            txtSoKK.Text = SoPhieu;
            txtDienGiai.Text = "";
            dateNgayCT.EditValue = DateTime.Now;
            txtDienGiai.Text = "";
            Enable(true);
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
                lookChatLuong.DataSource = db.tsChatLuongs;
                lookKienNghi.DataSource = db.tsKiemKeKienNghis;
                ///Load 1
                if (MaKK == null)
                {
                    AddNew();
                    objKK = new tsKiemKe();
                    db.tsKiemKes.InsertOnSubmit(objKK);
                }
                else
                {
                    objKK = db.tsKiemKes.SingleOrDefault(p => p.ID == MaKK);
                    txtDienGiai.Text = objKK.DienGiai;
                    txtSoKK.Text = objKK.SoCT;
                    dateNgayCT.EditValue = (DateTime?)objKK.NgayCT;
                    dateNgayKK.EditValue = (DateTime?)objKK.NgayKiemKe;
                }

                if (ListTS != null)
                {
                    tsTaiSan obj;
                    //decimal TGSuDung;
                    for (int i = 0; i < ListTS.Length; i++)
                    {
                        tsKiemKeChiTiet item = new tsKiemKeChiTiet();
                        //obj = db.tsTaiSans.Single(p => p.ID == ListTS[i]);
                        //TGSuDung = DateTime.Now.Subtract(obj.NgayBDSD.Value).Days / 30;
                        item.MaTS = ListTS[i];
                        objKK.tsKiemKeChiTiets.Add(item);
                    }

                }

                gcChiTiet.DataSource = objKK.tsKiemKeChiTiets;
                gcChungTu.DataSource = objKK.tskkChungTuThamChieus;

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
                if (MaKK == null)
                {
                    objKK.MaNV = objNhanVien.MaNV;
                    objKK.NgayTao = DateTime.Now;
                }
                else
                {
                    objKK = db.tsKiemKes.SingleOrDefault(p => p.ID == MaKK);
                    objKK.MaNVCN = objNhanVien.MaNV;
                    objKK.NgayCN = DateTime.Now;
                }
                objKK.SoCT = txtSoKK.Text.Trim();
                objKK.NgayCT = (DateTime?)dateNgayCT.EditValue;
                objKK.NgayKiemKe = (DateTime?)dateNgayKK.EditValue;
                objKK.DienGiai = txtDienGiai.Text.Trim();
                gvChungTu.RefreshData();
                gvChiTiet.RefreshData();
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
    }
}
