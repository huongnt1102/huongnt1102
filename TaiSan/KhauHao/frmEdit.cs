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

namespace TaiSan.KhauHao
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objNhanVien { get; set; }
        public int?  MaKH{ get; set; }
        MasterDataContext db;
        string SoPhieu = "";
        tsKhauHao objKH;
        public int[] ListTS { get; set; }

        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
            lookTaiSan.EditValueChanged += new EventHandler(lookTaiSan_EditValueChanged);
            btnSoChungTu.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(btnSoChungTu_ButtonClick);
            lookLoaiCT.EditValueChanged += new EventHandler(lookLoaiCT_EditValueChanged);
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
            SeachChungTu(); ;
        }

        void lookTaiSan_EditValueChanged(object sender, EventArgs e)
        {
            //var ts = (LookUpEdit)sender;
            //if (ts.EditValue != null)
            //{
            //    var objTS=db.tsTaiSans.Single(p=>p.ID==(int)ts.EditValue);
            //    gvChiTiet.SetFocusedRowCellValue("IsNoiBoC",objTS.IsNoiBo.GetValueOrDefault());
            //}
        }

        void AddNew()
        {
            db.tsKhauHao_TaoSoPhieu(ref SoPhieu);
            txtSoCT.Text = SoPhieu;
            txtDienGiai.Text = "";
            dateNgayCT.EditValue = DateTime.Now;
            txtDienGiai.Text = "";
            //txtTKCo.Text = "";
            //txtTKNo.Text = "";
            //spinSoTien.EditValue = 0;
            //chkCPIsHopLy.Checked = false;
            //txtDienGiaiKHDK.Text = "";
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
                ///Load 1
                if (MaKH == null)
                {
                    AddNew();
                    objKH = new tsKhauHao();
                    db.tsKhauHaos.InsertOnSubmit(objKH);
                }
                else
                {
                    objKH = db.tsKhauHaos.SingleOrDefault(p => p.ID == MaKH);
                    txtDienGiai.Text = objKH.DienGiai;
                    txtSoCT.Text = objKH.SoCT;
                    dateNgayCT.EditValue = objKH.NgayCT;
                }

                if (ListTS != null)
                {
                    tsTaiSan obj;
                    decimal TGSuDung;
                    for (int i = 0; i < ListTS.Length; i++)
                    {
                        tsKhauHaoChiTiet item = new tsKhauHaoChiTiet();
                        obj = db.tsTaiSans.Single(p => p.ID == ListTS[i]);
                        TGSuDung = DateTime.Now.Subtract(obj.NgayBDSD.Value).Days / 30;
                        objKH.tsKhauHaoChiTiets.Add(item);
                    }

                }

                gcChiTiet.DataSource = objKH.tsKhauHaoChiTiets;
                gcChungTu.DataSource = objKH.tskhChungTuThamChieus;
                gcKHDinhKhoan.DataSource = objKH.tsKhauHaoDinhKhoans;

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
                if (MaKH == null)
                {
                    objKH.MaNV = objNhanVien.MaNV;
                    objKH.NgayTao = DateTime.Now;
                }
                else
                {
                    objKH = db.tsKhauHaos.SingleOrDefault(p => p.ID == MaKH);
                    objKH.MaNVCN = objNhanVien.MaNV;
                    objKH.NgayCN = DateTime.Now;
                }
                objKH.SoCT = txtSoCT.Text.Trim();
                objKH.NgayCT = (DateTime?)dateNgayCT.EditValue;
                objKH.DienGiai = txtDienGiai.Text.Trim();
                gvChungTu.RefreshData();
                gvChiTiet.RefreshData();
                gvKHDinhKhoan.RefreshData();
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
