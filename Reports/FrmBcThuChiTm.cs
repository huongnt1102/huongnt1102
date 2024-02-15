using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Drawing;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;

namespace LandSoftBuilding.Fund.Reports
{
    public partial class FrmBcThuChiTm : XtraForm
    {
        public FrmBcThuChiTm()
        {
            InitializeComponent();
        }

        private void FrmBcThuChiTm_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            foreach (var v in objKbc.Source)
            {
                cbxKbc.Items.Add(v);
            }
            itemKyBaoCao.EditValue = objKbc.Source[7];
            SetDate(7);

            LoadData();
        }
        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao()
            {
                Index = index
            };
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            var db = new MasterDataContext();
            try
            {
                gc.DataSource = null;
                if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
                {
                    var tuNgay = (DateTime) itemTuNgay.EditValue;
                    var denNgay = (DateTime) itemDenNgay.EditValue;

                    DataTable data = new DataTable();
                    data.Columns.Add("NgayHachToan");
                    data.Columns.Add("MaKhachHang");
                    data.Columns.Add("SoPhieuThu");
                    data.Columns.Add("SoPhieuChi");
                    data.Columns.Add("Name");
                    data.Columns.Add("DienGiai");
                    data.Columns.Add("Thu");
                    data.Columns.Add("Chi");
                    data.Columns.Add("TonQuy");
                    data.Columns.Add("NhanVien");
                    data.Columns.Add("Key");
                    data.Columns.Add("OutputTyleName");

                    var objDauKy = db.SoQuy_ThuChis.Where(_ => SqlMethods.DateDiffDay(_.NgayPhieu, tuNgay) > 0 & _.MaTN == ((byte?)itemToaNha.EditValue??Common.User.MaTN) & _.IsKhauTru == false)
                        .Select(_ => new {SoTien = _.IsPhieuThu == true ? _.DaThu : -_.DaThu}).ToList();
                    decimal soTien = objDauKy.Sum(_ => _.SoTien).GetValueOrDefault();

                    var r1 = data.NewRow();
                    r1["DienGiai"] = "Số tồn đầu kỳ";
                    r1["TonQuy"] = string.Format("{0:#,0.##}", soTien);
                    r1["Key"] = 1;
                    data.Rows.Add(r1);

                    // Phiếu thu
                    var objTrongKy1 = (from _ in db.SoQuy_ThuChis
                        join kh in db.tnKhachHangs on _.MaKH equals kh.MaKH into khachHang from kh in khachHang.DefaultIfEmpty()
                        join nv in db.tnNhanViens on _.MaNV equals nv.MaNV into nhanVien from nv in nhanVien.DefaultIfEmpty()
                        where SqlMethods.DateDiffDay(tuNgay, _.NgayPhieu) >= 0 &
                              SqlMethods.DateDiffDay(_.NgayPhieu, denNgay) >= 0 &
                              _.MaTN == ((byte?)itemToaNha.EditValue??Common.User.MaTN) &
                              _.IsKhauTru == false & _.MaLoaiPhieu!=24 & _.IsPhieuThu == true
                              orderby _.NgayPhieu
                        select new //BcThuChiTm
                        {
                            NgayHachToan = _.NgayPhieu, SoPhieuThu = _.IsPhieuThu == true ? _.SoPhieu : "",
                            SoPhieuChi = _.IsPhieuThu == false ? _.SoPhieu : "", MaKhachHang = kh.KyHieu, Name = kh.IsCaNhan == true?kh.HoKH+" "+kh.TenKH : kh.CtyTen, DienGiai=_.DienGiai, NhanVien = nv.HoTenNV, Thu = _.IsPhieuThu == true? _.DaThu.GetValueOrDefault():0, Chi = _.IsPhieuThu==false?_.DaThu.GetValueOrDefault():0, OutputTyleName = ""
                        }).ToList();

                    // chi cho khách hàng
                    var objTrongKy2 = (from _ in db.SoQuy_ThuChis
                        join kh in db.tnKhachHangs on _.MaKH equals kh.MaKH into khachHang
                        from kh in khachHang.DefaultIfEmpty()
                        join nv in db.tnNhanViens on _.MaNV equals nv.MaNV into nhanVien
                        from nv in nhanVien.DefaultIfEmpty()
                        where SqlMethods.DateDiffDay(tuNgay, _.NgayPhieu) >= 0 &
                              SqlMethods.DateDiffDay(_.NgayPhieu, denNgay) >= 0 &
                              _.MaTN == ((byte?)itemToaNha.EditValue ?? Common.User.MaTN) &
                              _.IsKhauTru == false & _.MaLoaiPhieu != 24 & _.IsPhieuThu == false & _.TableName == "KH"
                        orderby _.NgayPhieu
                        select new //BcThuChiTm
                        {
                            NgayHachToan = _.NgayPhieu,
                            SoPhieuThu = _.IsPhieuThu == true ? _.SoPhieu : "",
                            SoPhieuChi = _.IsPhieuThu == false ? _.SoPhieu : "",
                            MaKhachHang = kh.KyHieu,
                            Name = kh.IsCaNhan == true ? kh.HoKH + " " + kh.TenKH : kh.CtyTen,
                            DienGiai = _.DienGiai,
                            NhanVien = nv.HoTenNV,
                            Thu = _.IsPhieuThu == true ? _.DaThu.GetValueOrDefault() : 0,
                            Chi = _.IsPhieuThu == false ? _.DaThu.GetValueOrDefault() : 0,
                            OutputTyleName = "Chi cho Khách hàng"
                        }).ToList();

                    // chi cho nhân viên
                    var objTrongKy3 = (from _ in db.SoQuy_ThuChis
                        join kh in db.tnNhanViens on _.MaKH equals kh.MaNV into khachHang
                        from kh in khachHang.DefaultIfEmpty()
                        join nv in db.tnNhanViens on _.MaNV equals nv.MaNV into nhanVien
                        from nv in nhanVien.DefaultIfEmpty()
                        where SqlMethods.DateDiffDay(tuNgay, _.NgayPhieu) >= 0 &
                              SqlMethods.DateDiffDay(_.NgayPhieu, denNgay) >= 0 &
                              _.MaTN == ((byte?)itemToaNha.EditValue ?? Common.User.MaTN) &
                              _.IsKhauTru == false & _.MaLoaiPhieu != 24 & _.IsPhieuThu == false & _.TableName == "NV"
                        orderby _.NgayPhieu
                        select new //BcThuChiTm
                        {
                            NgayHachToan = _.NgayPhieu,
                            SoPhieuThu = _.IsPhieuThu == true ? _.SoPhieu : "",
                            SoPhieuChi = _.IsPhieuThu == false ? _.SoPhieu : "",
                            MaKhachHang = kh.MaSoNV,
                            Name = kh.HoTenNV,
                            DienGiai = _.DienGiai,
                            NhanVien = nv.HoTenNV,
                            Thu = _.IsPhieuThu == true ? _.DaThu.GetValueOrDefault() : 0,
                            Chi = _.IsPhieuThu == false ? _.DaThu.GetValueOrDefault() : 0,
                            OutputTyleName = "Chi cho Nhân viên"
                        }).ToList();

                    // chi ngẫu nhiên, chi cho nhà cung cấp
                    var objTrongKy4 = (from _ in db.SoQuy_ThuChis
                        join pc in db.pcPhieuChis on _.IDPhieu equals pc.ID
                        join nv in db.tnNhanViens on _.MaNV equals nv.MaNV into nhanVien
                        from nv in nhanVien.DefaultIfEmpty()
                        where SqlMethods.DateDiffDay(tuNgay, _.NgayPhieu) >= 0 &
                              SqlMethods.DateDiffDay(_.NgayPhieu, denNgay) >= 0 &
                              _.MaTN == ((byte?)itemToaNha.EditValue ?? Common.User.MaTN) &
                              _.IsKhauTru == false & _.MaLoaiPhieu != 24 & _.IsPhieuThu == false & _.TableName == "NCC"
                        orderby _.NgayPhieu
                        select new //BcThuChiTm
                        {
                            NgayHachToan = _.NgayPhieu,
                            SoPhieuThu = _.IsPhieuThu == true ? _.SoPhieu : "",
                            SoPhieuChi = _.IsPhieuThu == false ? _.SoPhieu : "",
                            MaKhachHang = "",
                            Name = pc.NguoiNhan,
                            DienGiai = _.DienGiai,
                            NhanVien = nv.HoTenNV,
                            Thu = _.IsPhieuThu == true ? _.DaThu.GetValueOrDefault() : 0,
                            Chi = _.IsPhieuThu == false ? _.DaThu.GetValueOrDefault() : 0,
                            OutputTyleName = "Chi cho Nhà cung cấp"
                        }).ToList();

                    var objTrongKys = objTrongKy1.Concat(objTrongKy2).Concat(objTrongKy3).Concat(objTrongKy4);

                    var objTrongKy = (from _ in objTrongKys where _.Thu != 0 || _.Chi != 0 select _).ToList();

                    foreach (var item in objTrongKy)
                    {
                        soTien = soTien + item.Thu - item.Chi;
                        var r = data.NewRow();
                        r["NgayHachToan"] = string.Format("{0:dd/MM/yyyy}", item.NgayHachToan);
                        r["SoPhieuThu"] = item.SoPhieuThu;
                        r["SoPhieuChi"] = item.SoPhieuChi;
                        r["MaKhachHang"] = item.MaKhachHang;
                        r["Name"] = item.Name;
                        r["DienGiai"] = item.DienGiai;
                        r["Thu"] = string.Format("{0:#,0.##}", item.Thu);
                        r["Chi"] = string.Format("{0:#,0.##}", item.Chi);
                        r["TonQuy"] = string.Format("{0:#,0.##}", soTien);
                        r["NhanVien"] = item.NhanVien;
                        r["Key"] = 0;
                        r["OutputTyleName"] = item.OutputTyleName;
                        data.Rows.Add(r);
                    }

                    //var r2 = data.NewRow();
                    //r2["DienGiai"] = "TỔNG CỘNG";
                    //r2["Thu"] = string.Format("{0:#,0.##}", objTrongKy.Sum(_ => _.Thu));
                    //r2["Chi"] = string.Format("{0:#,0.##}", objTrongKy.Sum(_ => _.Chi));
                    //r2["TonQuy"] = string.Format("{0:#,0.##}", soTien);
                    //r2["Key"] = 1;
                    //data.Rows.Add(r2);

                    gc.DataSource = data;

                    colTonQuy.SummaryItem.DisplayFormat = string.Format("{0:n0}", soTien);
                }
            }
            catch{}
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }

        private void gv_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView gridview = sender as GridView;
            try
            {
                if (e.RowHandle >= 0)
                {
                    string category = gridview.GetRowCellDisplayText(e.RowHandle, gridview.Columns["Key"]);
                    if (category == "1")
                    {
                        e.Appearance.Options.UseTextOptions = true;
                        e.Appearance.Font = new Font(e.Appearance.Font.Name,
                            e.Appearance.Font.Size, FontStyle.Bold);
                    }
                }
            }
            catch { }
        }

        private void cbxKbc_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit) sender).SelectedIndex);
        }

        private void itemSendMail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            try
            {
                gv.SelectAll();
                //var strToaNha = (itemToaNha.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',')
                //    .Replace(" ", "");
                //var ltToaNha = strToaNha.Split(',');
                //if (strToaNha == "") return;

                var db = new MasterDataContext();

                var objManagerEmployee = db.mail_SetupSendMailToEmployeeDebits
                    .Where(_ => _.BuildingId == (byte) itemToaNha.EditValue & _.IsSendMail == true & _.GroupId == 2)
                    .Select(_ => new {_.EmployeeId}).Distinct().ToList();

                foreach (var employee in objManagerEmployee)
                {
                    var objEmployee = db.tnNhanViens.FirstOrDefault(_ => _.MaNV == employee.EmployeeId);
                    if (objEmployee == null) continue;
                    if (objEmployee.Email == "") continue;

                    #region Send mail

                    var objMail = new LandSoftBuilding.Marketing.Mail.MailClient();
                    var objFrom = db.mailConfigs.OrderByDescending(_ => _.ID).FirstOrDefault();
                    int status = 1;
                    if (objFrom != null)
                    {
                        try
                        {
                            // file excel
                            var result = new System.IO.MemoryStream();
                            var options = new DevExpress.XtraPrinting.XlsExportOptionsEx();
                            options.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                            gc.ExportToXlsx(result);
                            result.Seek(0, System.IO.SeekOrigin.Begin);

                            // send email
                            objMail.SmtpServer = objFrom.Server;
                            if (objFrom.Port != null) objMail.Port = objFrom.Port.Value;
                            if (objFrom.EnableSsl != null) objMail.EnableSsl = objFrom.EnableSsl.Value;
                            objMail.From = objFrom.Email;
                            objMail.Reply = objFrom.Reply;
                            objMail.Display = objFrom.Display;
                            objMail.Pass = it.EncDec.Decrypt(objFrom.Password);
                            objMail.To = objEmployee.Email;
                            objMail.Subject = "Sổ kế toán chi tiết Quỹ tiền mặt";
                            objMail.Content = "";
                            var fileAttach = new System.Net.Mail.Attachment(result, "SoKeToanChiTietQuyTienMat",
                                "application/vnd.ms-excel");
                            objMail.Attachs = new List<System.Net.Mail.Attachment>();
                            objMail.Attachs.Add(fileAttach);

                            objMail.Send();
                            status = 1;
                        }
                        catch
                        {
                            status = 2;
                        }
                    }

                    #endregion

                    #region Lịch sử gửi email

                    mailHistory objHistoryEmail = new mailHistory();
                    objHistoryEmail.MailID = objFrom.ID;
                    objHistoryEmail.ToMail = objEmployee.Email;
                    objHistoryEmail.CcMail = "";
                    objHistoryEmail.BccMail = "";
                    objHistoryEmail.Subject = "Sổ kế toán chi tiết Quỹ tiền mặt";
                    objHistoryEmail.Contents = "";
                    objHistoryEmail.Status = status;
                    objHistoryEmail.DateCreate = DateTime.UtcNow.AddHours(7);
                    objHistoryEmail.StaffCreate = Common.User.MaNV;
                    objHistoryEmail.CusID = null;
                    db.mailHistories.InsertOnSubmit(objHistoryEmail);
                    db.SubmitChanges();

                    #endregion
                }

                DialogBox.Success("Đã gửi thư xong.");
                return;
            }
            catch
            {
            }
        }
    }
}