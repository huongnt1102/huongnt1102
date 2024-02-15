using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
using System.Data.Linq.SqlClient;

namespace Building.Asset.Import
{
    public partial class frmPhanCong_Import : XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        tbl_PhanCong PC;
       // tbl_PhanCong_DanhSachThap DS_T;
        tbl_PhanCong_NhanVienChiTiet DS_NV;
        public byte? MaTn { get; set; }
        public bool IsSave { get; set; }
        
        public frmPhanCong_Import()
        {
            InitializeComponent();
        }

        private int? SearchDSThap(string chuoi)
        {
            try
            {
                return db.tbl_PhanCong_PhanLoaiThaps.FirstOrDefault(p => SqlMethods.Like(p.Name, "%" + chuoi + "%")).ID;
            }
            catch
            {
               // DialogBox.Alert("Công việc: " + chuoi + " không tồn tại!");
                return null;
            }
        }

        private int? SearchNV(string chuoi)
        {
            try
            {
                return db.tnNhanViens.FirstOrDefault(p => SqlMethods.Like(p.HoTenNV, "%" + chuoi + "%")).MaNV;
            }
            catch
            {
                //DialogBox.Alert("Nhóm: " + chuoi + " không tồn tại!");
                return null;
            }
        }

        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            if (itemSheet.EditValue == null)
                gc.DataSource = null;
            else
                try
                {
                    var excel = new LinqToExcel.ExcelQueryFactory(Tag.ToString());
                    gc.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(_ => new PhanCong
                    {
                        MaTN = MaTn,//SearchToaNha(_["TenTN"].ToString().Trim()),
                        Ngay =Convert.ToDateTime(_["Ngay"].ToString().Trim()),
                        MaSoPC = _["MaSoPC"].ToString().Trim(),
                        NoiDungCongViec = _["NoiDungCongViec"].ToString().Trim(),
                        MaNV = SearchNV(_["MaNV"].ToString().Trim()),
                        ThapID = SearchDSThap(_["ThapID"].ToString().Trim()),
                        
                    }).ToList();

                    excel = null;
                }
                catch (Exception ex)
                {
                    DialogBox.Error(ex.Message);
                }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            gv.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gc.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }
            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var objDS = (List<PhanCong>)gc.DataSource;
                var ltError = new List<PhanCong>();
                foreach (var n in objDS)
                {
                    try
                    {
                        if(n.MaNV == null)
                        {
                            n.Error = "Nhân viên không tồn tại!";
                            ltError.Add(n);
                        }
                        if (n.ThapID == null)
                        {
                            n.Error = "Danh sách tháp không tồn tại!";
                            ltError.Add(n);
                        }
                      
                        else
                        {
                            var kt = db.tbl_PhanCongs.FirstOrDefault(_ => _.MaSoPC == n.MaSoPC.ToLower());
                            if (kt != null)
                            {
                                #region Sửa
                                kt.MaSoPC = n.MaSoPC;
                                kt.MaTN = MaTn;
                                kt.Ngay = n.Ngay;
                                kt.NoiDungCongViec = n.NoiDungCongViec;
                                db.SubmitChanges();
                                if (kt.ID > 0)
                                {
                                    var DS_T = new tbl_PhanCong_DanhSachThap();
                                    DS_T.PhanCongID = kt.ID;
                                    DS_T.ThapID = n.ThapID;
                                    db.tbl_PhanCong_DanhSachThaps.InsertOnSubmit(DS_T);
                                    db.SubmitChanges();

                                    if (DS_T.ID > 0)
                                    {
                                        #region Nhân viên chi tiết
                                        //kIÊM TRA SỰ TỒN TẠI NHÂN VIÊN TRONG CÙNG 1 NGÀY
                                        var obj_NVN = from nv in db.tbl_PhanCong_NhanVienChiTiets
                                                      where nv.MaNV == n.MaNV & System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(nv.Ngay, n.Ngay) == 0
                                                      select new
                                                      {
                                                          MaNV = nv.MaNV,
                                                          Ngay = nv.Ngay,
                                                      };
                                        if (obj_NVN.Count() > 0)
                                        {
                                            n.Error =string.Format("Nhân viên này đã được phân công trong ngày {0: dd/MM/yyyy} ", n.Ngay);
                                            ltError.Add(n);
                                        }
                                        
                                        else
                                        {
                                            // KIỂM TRA TỒN TẠI 2 NHÂN VIÊN CÙNG THÁP
                                            var obj_dst = from nv in db.tbl_PhanCong_NhanVienChiTiets
                                                          join dst in db.tbl_PhanCong_DanhSachThaps on nv.DanhSachThapID equals dst.ID into dstr
                                                          from dst in dstr.DefaultIfEmpty()
                                                          where dst.ThapID == n.ThapID & dst.PhanCongID == kt.ID & nv.MaNV == n.MaNV
                                                          select new
                                                          {
                                                              ThapID = dst.ThapID,
                                                          };
                                            var kt_nv = db.tbl_PhanCong_NhanVienChiTiets.FirstOrDefault(_ => _.MaNV == n.MaNV & _.IDPhanCong == kt.ID & obj_dst.Count() > 0);
                                            if (kt_nv == null)
                                            {
                                                var DS_NV = new tbl_PhanCong_NhanVienChiTiet();
                                                DS_NV.MaTN = MaTn;
                                                DS_NV.MaNV = n.MaNV;
                                                DS_NV.Ngay = n.Ngay;
                                                DS_NV.IDPhanCong = kt.ID;
                                                DS_NV.DanhSachThapID = DS_T.ID;
                                                DS_NV.NguoiTao = Library.Common.User.MaNV;
                                                DS_NV.NgayTao = DateTime.Now;
                                                db.tbl_PhanCong_NhanVienChiTiets.InsertOnSubmit(DS_NV);
                                                db.SubmitChanges();
                                            }
                                        }
                                        #endregion
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                #region Thêm mới
                                var PC = new tbl_PhanCong();
                                PC.MaTN = MaTn;
                                PC.Ngay = n.Ngay;
                                PC.MaSoPC = n.MaSoPC;
                                PC.NoiDungCongViec = n.NoiDungCongViec;
                                PC.NguoiTao = Library.Common.User.MaNV;
                                PC.NgayTao = DateTime.Now;
                                db.tbl_PhanCongs.InsertOnSubmit(PC);
                                db.SubmitChanges();

                                if (PC.ID > 0)
                                {
                                    var DS_T = new tbl_PhanCong_DanhSachThap();
                                    DS_T.PhanCongID = PC.ID;
                                    DS_T.ThapID = n.ThapID;
                                    db.tbl_PhanCong_DanhSachThaps.InsertOnSubmit(DS_T);
                                    db.SubmitChanges();

                                    if (DS_T.ID > 0)
                                    {
                                        #region Kiểm tra tồn tại nhân viên trong ngày
                                        var obj_NVN = from nv in db.tbl_PhanCong_NhanVienChiTiets
                                                      where nv.MaNV == n.MaNV & System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(nv.Ngay, n.Ngay) == 0
                                                      select new
                                                      {
                                                          MaNV = nv.MaNV,
                                                          Ngay = nv.Ngay,
                                                      };
                                        if (obj_NVN.Count() > 0)
                                        {
                                            n.Error = string.Format("Nhân viên này đã được phân công trong ngày {0: dd/MM/yyyy} ", n.Ngay);
                                            ltError.Add(n);
                                        }
                                        else
                                        {
                                            var DS_NV = new tbl_PhanCong_NhanVienChiTiet();
                                            DS_NV.MaTN = MaTn;
                                            DS_NV.MaNV = n.MaNV;
                                            DS_NV.Ngay = n.Ngay;
                                            DS_NV.IDPhanCong = PC.ID;
                                            DS_NV.DanhSachThapID = DS_T.ID;
                                            DS_NV.NguoiTao = Library.Common.User.MaNV;
                                            DS_NV.NgayTao = DateTime.Now;
                                            db.tbl_PhanCong_NhanVienChiTiets.InsertOnSubmit(DS_NV);
                                            db.SubmitChanges();
                                        }
                                        #endregion
                                    }
                                }
                                #endregion
                            }
                        }
                        db.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        n.Error = ex.Message;
                        ltError.Add(n);
                    }
                }
                IsSave = true;
                DialogBox.Success();

                if (ltError.Count > 0)
                {
                    gc.DataSource = ltError;
                }
                else
                {
                    gc.DataSource = null;
                }
            }
            catch
            {
                wait.Close();
                DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
                Close();
            }
            finally
            {
                wait.Dispose();
                db.Dispose();
            }
        }

        private void itemChonFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var file = new OpenFileDialog();
            try
            {
                file.Filter = @"(Excel file)|*.xls;*.xlsx";
                file.ShowDialog();
                if (file.FileName == "") return;

                var excel = new LinqToExcel.ExcelQueryFactory(file.FileName);
                var sheets = excel.GetWorksheetNames();
                cmbSheet.Items.Clear();
                foreach (var s in sheets)
                    cmbSheet.Items.Add(s.Trim('$'));

                itemSheet.EditValue = null;
                Tag = file.FileName;
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

        private void frmCaTruc_Import_Load(object sender, EventArgs e)
        {
            PC = new tbl_PhanCong();
            DS_NV = new tbl_PhanCong_NhanVienChiTiet();
            DS_T = new tbl_PhanCong_DanhSachThap();
        }
    }

    public class PhanCong
    {
        public byte? MaTN { get; set; }
        public DateTime? Ngay { get; set; }
        public string MaSoPC { get; set; }
        public string NoiDungCongViec { get; set; }
        public int? MaNV { get; set; }
        public int? ThapID{ get; set; }
        public string Error { get; set; }
       
    }
}