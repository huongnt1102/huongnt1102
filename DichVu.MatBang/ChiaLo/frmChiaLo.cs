using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace MatBang.ChiaLo
{
    public partial class frmChiaLo : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        public mbMatBang objmatbang;
        decimal DienTichConLai = 0;
        MasterDataContext db;

        public frmChiaLo()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmChiaLo_Load(object sender, EventArgs e)
        {
            objmatbang = db.mbMatBangs.Single(p => p.MaMB == objmatbang.MaMB);

            txtMaMB.Text = objmatbang.MaSoMB;
            txtDienTich.Text = string.Format("{0:#,0.#} m2", objmatbang.DienTich);
            DienTichConLai = objmatbang.DienTich ?? 0;
            lookTrangThai.DataSource = db.mbTrangThais;
            lookKhachHang.DataSource = db.tnKhachHangs
                .Select(p => new
                {
                    p.MaKH,
                    KhachHang = p.IsCaNhan.HasValue ? (p.IsCaNhan.Value ? p.HoKH + " " + p.TenKH : p.CtyTen) : ""
                });

            gcChiaLo.DataSource = db.mbMatBang_ChiaLos.Where(p=>p.MaMB == objmatbang.MaMB);
        }
        
        decimal dt = 0;
        private void spinSoLo_EditValueChanged(object sender, EventArgs e)
        {
            dt = 0;
            grvChiaLo.SelectAll();
            grvChiaLo.DeleteSelectedRows();

            for (int i = 0; i < spinSoLo.Value; i++)
            {
                grvChiaLo.AddNewRow();
                grvChiaLo.SetFocusedRowCellValue(colMaMB, objmatbang.MaMB);
                grvChiaLo.SetFocusedRowCellValue(colTenLo,string.Format("{0}/{1}",objmatbang.MaSoMB,i));
                if (i == (spinSoLo.Value - 1))
                {
                    TinhDienTichDaSuDung();
                    DienTichConLai = (objmatbang.DienTich ?? 0) - dt;
                    grvChiaLo.SetFocusedRowCellValue(colDienTich, DienTichConLai);
                }
                else
                {
                    grvChiaLo.SetFocusedRowCellValue(colDienTich, (objmatbang.DienTich ?? 0) / spinSoLo.Value);
                }
                grvChiaLo.UpdateCurrentRow();
            }
            TinhDienTichDaSuDung();
            spinDienTichConLai.Value = (objmatbang.DienTich ?? 0) - dt;
        }

        private void TinhDienTichDaSuDung()
        {
            dt = 0;
            try
            {
                for (int i = 0; i < grvChiaLo.RowCount; i++)
                {
                    dt = dt + (decimal)grvChiaLo.GetRowCellValue(i, colDienTich);
                }
            }
            catch { }
        }

        private void grvChiaLo_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colDienTich)
            {
                TinhDienTichDaSuDung();
                spinDienTichConLai.Value = (objmatbang.DienTich ?? 0) - dt;
            }
        }

        private void btnChapNhan_Click(object sender, EventArgs e)
        {
            TinhDienTichDaSuDung();
            if ((objmatbang.DienTich ?? 0) - dt < 0)
            {
                DialogBox.Alert("Tổng diện tích các lô lớn hơn diện tích mặt bằng");
                return;
            }


            try
            {
                db.SubmitChanges();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch
            {
                DialogBox.Alert("Không lưu được, có dịch vụ đang sử dụng lô hiện tại. Việc tạo lại lô sẽ vi phạm ràng buộc dữ liệu");
                this.Close();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}