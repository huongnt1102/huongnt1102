using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Linq.SqlClient;
using Library;
using System.Linq;
using LinqToExcel;

namespace LandSoftBuilding.Fund.Input
{
    public partial class frmImportGachNo : DevExpress.XtraEditors.XtraForm
    {
        public frmImportGachNo()
        {
            InitializeComponent();
        }

        public byte MaTN { get; set; }
        public bool isSave { get; set; }

        List<int> GetMaLDVs(string _MaLDVs)
        {
            var ltMaLDV = new List<int>();

            try
            {
                var arrMaLDV = _MaLDVs.Split(',');
                if (arrMaLDV[0].Trim() != "")
                {
                    foreach (var i in arrMaLDV)
                    {
                        ltMaLDV.Add(int.Parse(i));
                    }
                }
            }
            catch { }

            return ltMaLDV;
        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);
        }

        private void itemChoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var file = new OpenFileDialog();
            try
            {
                file.Filter = "(Excel file)|*.xls;*.xlsx";
                file.ShowDialog();
                if (file.FileName == "") return;

                var excel = new ExcelQueryFactory(file.FileName);
                var sheets = excel.GetWorksheetNames();

                cmbSheet.Items.Clear();
                foreach (string s in sheets)
                    cmbSheet.Items.Add(s.Trim('$'));

                itemSheet.EditValue = null;
                this.Tag = file.FileName;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                file.Dispose();
            }
        }

        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var excel = new ExcelQueryFactory(this.Tag.ToString());

                System.Collections.Generic.List<PhieuThuGachNoItem> list = Library.Import.ExcelAuto.ConvertDataTable<PhieuThuGachNoItem>(Library.Import.ExcelAuto.GetDataExcel(excel, gvImport, itemSheet));

                gcImport.DataSource = list;

                excel = null;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;
            gvImport.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gcImport.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                #region Nap tu dien 
                var ltKhachHang = (from kh in db.tnKhachHangs
                                   where kh.MaTN == this.MaTN
                                   select new { kh.MaKH, MaSoKH = kh.KyHieu.ToLower() })
                                   .ToList();
                var ltMatBang = (from kh in db.mbMatBangs
                                   where kh.MaTN == this.MaTN
                                   select new { kh.MaMB, MaSoMB = kh.MaSoMB.ToLower() })
                                   .ToList();

                var ltPhanLoai = (from pl in db.ptPhanLoais
                                  select new { pl.ID, TenPL = pl.TenPL.ToLower() })
                                  .ToList();

                var ltSoTKNH = (from tk in db.nhTaiKhoans
                                where tk.MaTN == this.MaTN
                                select new { tk.ID, SoTKNH = tk.SoTK.ToLower() })
                                .ToList();
                #endregion

                var ltSource = (List<PhieuThuGachNoItem>)gcImport.DataSource;
                var ltError = new List<PhieuThuGachNoItem>();

                foreach(var i in ltSource)
                {
                    db = new MasterDataContext();
                    try
                    {
                        #region Rang buoc nhập nhiệu
                        if (i.NgayCT.Year == 1)
                        {
                            i.Error = "Vui lòng nhập ngày chứng từ";
                            ltError.Add(i);
                            continue;
                        }

                        if (i.SoTien == 0)
                        {
                            i.Error = "Vui lòng nhập số tiền";
                            ltError.Add(i);
                            continue;
                        }

                        var objKH = ltKhachHang.FirstOrDefault(p => p.MaSoKH == i.MaSoKH.ToLower());
                        if (objKH == null)
                        {
                            i.Error = "Khách hàng không tồn tại trong hệ thống";
                            ltError.Add(i);
                            continue;
                        }
                        var objMB = ltMatBang.FirstOrDefault(p => p.MaSoMB == i.MaSoMB.ToLower());
                        if (objMB == null)
                        {
                            i.Error = "Mặt bằng không tồn tại trong hệ thống";
                            ltError.Add(i);
                            continue;
                        }
                        var objPL = ltPhanLoai.FirstOrDefault(p => p.TenPL == "hóa đơn dịch vụ");
                        if (objPL == null)
                        {
                            i.Error = "Phân loại không chính xác";
                            ltError.Add(i);
                            continue;
                        }

                        int? _MaTKNH = null;
                        if (!string.IsNullOrEmpty(i.SoTKNH))
                        {
                            var objTKNH = ltSoTKNH.FirstOrDefault(p => p.SoTKNH == i.SoTKNH.ToLower());
                            if (objTKNH == null)
                            {
                                i.Error = "Tài khoản ngân hàng không chính xác";
                                ltError.Add(i);
                                continue;
                            }
                            else
                            {
                                _MaTKNH = objTKNH.ID;
                            }
                        }
                        #endregion

                        #region Kiểm tra khóa hóa đơn
                        // Cần trả về là có được phép sửa hay return
                        // truyền vào form service, từ ngày đến ngày, tòa nhà

                        var result = DichVu.KhoaSo.Class.ClosingEntry.Closing(MaTN, i.NgayCT, DichVu.KhoaSo.Class.Enum.PAY);

                        if (result.Count() > 0)
                        {
                            i.Error = "Ngày thu đã khóa";
                            ltError.Add(i);
                            continue;
                        }

                        #endregion

                        #region Them phieu thu

                        string sopt = "";
                        int? matknh = 0; int mahtht = 0; string hinhthucthanhtoanname = "";
                        if(string.IsNullOrEmpty(i.SoCT))
                        {
                            sopt = db.CreateSoChungTu(10, MaTN);
                        }
                        else
                        {
                            sopt = i.SoCT;
                        }

                        if(_MaTKNH != null)
                        {
                            matknh = _MaTKNH;
                            mahtht = 2;
                            hinhthucthanhtoanname = "Chuyển khoản";
                        }
                        else
                        {
                            matknh = null;
                            mahtht = 1;
                            hinhthucthanhtoanname = "Tiền mặt";
                        }

                        var model_pt = new { matn = MaTN, ngaythu = i.NgayCT, sopt = sopt, makh = objKH.MaKH, mamb = objMB.MaMB, nguoinop = i.NguoiNop, diachi = i.DiaChi, lydo = i.DienGiai, sotien = i.SoTien, mapl = objPL.ID, manv = Common.User.MaNV, ngaynhap = db.GetSystemDate(), manvn = Common.User.MaNV, matknh = matknh, mahtht = mahtht, hinhthucthanhtoanname = hinhthucthanhtoanname, maldv_name = i.MaLDVs, thang = i.Thang, nam = i.Nam, bienso = i.BienSo };
                        var param_pt = new Dapper.DynamicParameters();
                        param_pt.AddDynamicParams(model_pt);
                        int kq = Library.Class.Connect.QueryConnect.Query<int>("sq_phieuthu_insert", param_pt).First();

                        #endregion

                        if(kq == 0)
                        {
                            i.Error = "Không tìm thấy hóa đơn cần gạch nợ";
                            ltError.Add(i);
                        }
                    }
                    catch (Exception ex)
                    {
                        i.Error = ex.Message;
                        ltError.Add(i);
                    }
                }

                this.isSave = true;

                if (ltError.Count > 0)
                {
                    DialogBox.Alert(string.Format("Có {0:n0} dòng xảy ra lỗi", ltError.Count));
                     gcImport.DataSource = ltError;
                }
                else
                {
                    DialogBox.Success();
                    gcImport.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                wait.Close();
                db.Dispose();
            }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcImport);
        }

        public class PhieuThuGachNoItem
        {
            public DateTime NgayCT { get; set; }
            public string SoCT { get; set; }
            public string MaSoKH { get; set; }
            public string MaSoMB { get; set; }
            public string NguoiNop { get; set; }
            public string DiaChi { get; set; }
            public string DienGiai { get; set; }
            public decimal SoTien { get; set; }
            public string SoTKNH { get; set; }
            public string ChungTuGoc { get; set; }
            public string BienSo { get; set; }
            public string MaLDVs { get; set; }
            public int Thang { get; set; }
            public int Nam { get; set; }
            public string Error { get; set; }
        }
    }

}