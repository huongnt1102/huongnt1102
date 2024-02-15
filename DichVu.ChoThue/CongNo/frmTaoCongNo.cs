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

namespace DichVu.ChoThue.CongNo
{
    public partial class frmTaoCongNo : DevExpress.XtraEditors.XtraForm
    {
        public int MaHD;
        thueHopDong objHD;
        MasterDataContext db;
        public frmTaoCongNo()
        {
            InitializeComponent();
        }

        private void frmTaoCongNo_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            objHD = db.thueHopDongs.Single(p => p.MaHD == MaHD);
            gcCongNo.DataSource = objHD.thueCongNos;
            #region Add vào công nợ
            //var congno = db.thueCongNos.FirstOrDefault(p => p.MaHD == objHD.MaHD);
            ////if (congno == null)
            ////{
            //Library.CongNoCls.HopDongThue objhopdongthue = new Library.CongNoCls.HopDongThue();
            //ChuKyCls objchuky = new ChuKyCls();
            //List<ChuKyCls> lstchuky = new List<ChuKyCls>();
            //objchuky = objhopdongthue.LayChuKyTheoThoiDiemHienTai(objHD);
            //lstchuky = objhopdongthue.DanhSachChuKyThanhToan(objHD);

            //decimal solan = 0;
            //var thangdu = 0;
            //try
            //{
            //    solan = (decimal)(objHD.ThoiHan / objHD.ChuKyThanhToan);
            //    thangdu = (int)(objHD.ThoiHan % objHD.ChuKyThanhToan);
            //}
            //catch
            //{
            //    if (DialogBox.Question("Hợp đồng này không có thời hạn sử dụng và chu kỳ thanh toán.\r\nBạn có muốn phát sinh công nợ thanh toán 1 lần duy nhất không?") == System.Windows.Forms.DialogResult.No)
            //    {
            //        return;
            //    }
            //}
            //if (thangdu > 0) solan++;

            //if (objHD.thueCongNos.Count() <= 0 | objHD.thueCongNos.Count() != solan)//chua co
            //{
            //    if (objHD.thueCongNos.Count() != solan)//chua co
            //    {
            //        objHD.thueCongNos = null;
            //    }
            //    if (thangdu > 0)
            //    {
            //        #region co thang du
            //        for (int i = 0; i < lstchuky.Count() - 1; i++)
            //        {
            //            var lst = lstchuky[i];
            //            thueCongNo objcongno = new thueCongNo()
            //            {
            //                MaHD = objHD.MaHD,
            //                DaThanhToan = 0,
            //                ConNo = (objHD.ThanhTien ?? 0) * (objHD.ChuKyThanhToan ?? 1),

            //                ChuKyMin = lst.Min,
            //                ChuKyMax = lst.Max
            //            };
            //            objHD.thueCongNos.Add(objcongno);
            //        }
            //        var lsttd = lstchuky[lstchuky.Count() - 1];
            //        thueCongNo obj = new thueCongNo()
            //        {
            //            MaHD = objHD.MaHD,
            //            DaThanhToan = 0,
            //            ConNo = (objHD.ThanhTien ?? 0) * thangdu,

            //            ChuKyMin = lsttd.Min,
            //            ChuKyMax = lsttd.Max
            //        };
            //        objHD.thueCongNos.Add(obj);
            //        #endregion

            //    }
            //    else
            //    {
            //        foreach (ChuKyCls chuky in lstchuky)
            //        {
            //            thueCongNo objcongno = new thueCongNo()
            //            {
            //                MaHD = objHD.MaHD,
            //                DaThanhToan = 0,
            //                ConNo = (objHD.ThanhTien ?? 0) * (objHD.ChuKyThanhToan ?? 1),
            //                ChuKyMin = chuky.Min,
            //                ChuKyMax = chuky.Max
            //            };
            //            objHD.thueCongNos.Add(objcongno);
            //        }
            //    }
            //}
            //else  //da co
            //{
            //    for (int i = 0; i < objHD.thueCongNos.Count(); i++)
            //    {
            //        var temp = objHD.thueCongNos[i];
            //        temp.ChuKyMin = lstchuky[i].Min;
            //        temp.ChuKyMax = lstchuky[i].Max;
            //    }

            //        if (thangdu > 0)
            //        {
            //            for (int i = 0; i < objHD.thueCongNos.Count() - 1; i++)
            //            {
            //                var item = objHD.thueCongNos[i];
            //                item.DaThanhToan = 0;
            //                item.ConNo = objHD.ThanhTien * (objHD.ChuKyThanhToan ?? 1);
            //            }
            //            var itemtd = objHD.thueCongNos[objHD.thueCongNos.Count() - 1];
            //            itemtd.DaThanhToan = 0;
            //            itemtd.ConNo = objHD.ThanhTien * thangdu;
            //        }
            //        else
            //        {
            //            foreach (var item in objHD.thueCongNos)
            //            {
            //                item.DaThanhToan = 0;
            //                item.ConNo = objHD.ThanhTien * (objHD.ChuKyThanhToan ?? 1);
            //            }
            //        }
            //}
            //gcCongNo.DataSource = objHD.thueCongNos;
            #endregion
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                db.SubmitChanges();
             //   db.thueCongNo_resetDaThuSingle(objHD.MaMB, objHD.MaHD);
                DialogBox.Alert("Đã phát sinh xong công nợ.");
                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void grvCongNo_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvCongNo.SetFocusedRowCellValue("ChiKyMin", db.GetSystemDate());
            grvCongNo.SetFocusedRowCellValue("ConNo", 0);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            grvCongNo.DeleteSelectedRows();
        }

        private void grvCongNo_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Caption == "Phải đóng" | e.Column.Caption == "Đã thanh toán" | e.Column.Caption == "Nợ trước")
            {
                try
                {

                    decimal? total = (decimal?)grvCongNo.GetFocusedRowCellValue("ConNo") + (decimal?)grvCongNo.GetFocusedRowCellValue("NoTruoc") - (decimal?)grvCongNo.GetFocusedRowCellValue("DaThanhToan");
                    grvCongNo.SetFocusedRowCellValue("TongNo", total);
                 
                }
                catch { }
            }
        }
    }
}