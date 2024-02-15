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

namespace TaiSan.GhiGiam
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objNhanVien { get; set; }
        public int?  MaGG{ get; set; }
        MasterDataContext db;
        string SoPhieu = "";
        tsGhiGiam objGG;
        public int[] ListTS { get; set; }
        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
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


        void AddNew()
        {
            db.tsGhiGiam_TaoSoPhieu(ref SoPhieu);
            txtSoGG.Text = SoPhieu;
            dateNgayGG.EditValue = DateTime.Now;
            lookLyDo.EditValue = null;
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
                lookLyDo.Properties.DataSource = db.tsGhiGiamLyDos;
                ///Load 1
                if (MaGG == null)
                {
                    AddNew();
                    objGG = new tsGhiGiam();
                    db.tsGhiGiams.InsertOnSubmit(objGG);
                }
                else
                {
                    objGG = db.tsGhiGiams.SingleOrDefault(p => p.ID == MaGG);
                    txtSoGG.Text = objGG.SoCT;
                    dateNgayGG.EditValue = (DateTime?)objGG.NgayCT;
                    lookLyDo.EditValue = objGG.MaLyDo;
                }

                if (ListTS != null)
                {
                    tsTaiSan obj;
                    for (int i = 0; i < ListTS.Length; i++)
                    {
                        tsGhiGiamChiTiet item = new tsGhiGiamChiTiet();
                        obj = db.tsTaiSans.Single(p => p.ID == ListTS[i]);
                        item.TKHaoMon = obj.TKKhauHao;
                        item.GiaTriConLai = obj.GiaTriConLai;
                        item.HaoMonLuyKe = obj.HaoMonLuyKe;
                        item.GiaTriKhauHao = obj.GiaTriTinhKH;
                        item.MaTS = ListTS[i];
                        objGG.tsGhiGiamChiTiets.Add(item);
                    }

                }

                gcGGChiTiet.DataSource = objGG.tsGhiGiamChiTiets;
                gcChungTu.DataSource = objGG.tsggChungTuThamChieus;

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
                if (MaGG == null)
                {
                    objGG.MaNV = objNhanVien.MaNV;
                    objGG.NgayTao = DateTime.Now;
                }
                else
                {
                    objGG = db.tsGhiGiams.SingleOrDefault(p => p.ID == MaGG);
                    objGG.MaNVCN = objNhanVien.MaNV;
                    objGG.NgayCN = DateTime.Now;
                }
                objGG.SoCT = txtSoGG.Text.Trim();
                objGG.NgayCT = (DateTime?)dateNgayGG.EditValue;
                objGG.MaLyDo = (short?)lookLyDo.EditValue;

                gvGGChiTiet.RefreshData();
                List<tsTaiSan> ListTS = new List<tsTaiSan>();
                for (int i = 0; i < gvGGChiTiet.RowCount - 1; i++)
                {
                    var obj = db.tsTaiSans.SingleOrDefault(p => p.ID == (int)gvGGChiTiet.GetRowCellValue(i, "MaTS"));
                    ListTS.Add(obj);
                }
                for (int i = 0; i < ListTS.Count; i++)
                {
                    ListTS[i].IsGhiGiam = true;
                }
                gvChungTu.RefreshData();
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

        private void lookTaiSan_EditValueChanged(object sender, EventArgs e)
        {
            var lookTS = (LookUpEdit)sender;
            var objTS = db.tsTaiSans.SingleOrDefault(p => p.ID == (int)lookTS.EditValue);
            gvGGChiTiet.SetFocusedRowCellValue("GiaTriKhauHao", objTS.GiaTriTinhKH);
            gvGGChiTiet.SetFocusedRowCellValue("GiaTriConLai", objTS.GiaTriConLai);
            gvGGChiTiet.SetFocusedRowCellValue("HaoMonLuyKe", objTS.HaoMonLuyKe);
        }
    }
}
