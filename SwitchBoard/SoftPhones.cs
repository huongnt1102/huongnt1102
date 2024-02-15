using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library;
using DIP.SoftPhoneAPI;
using System.Xml.Linq;
using System.Windows.Forms;

namespace DIP.SwitchBoard
{
    public static class SwitchBoard
    {
        public static SoftPhones SoftPhone;

        public static List<CallRecord> GetHistory(DateTime fromDate, DateTime toDate, int type)
        {
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                System.Net.WebClient client = new System.Net.WebClient();

                var url = string.Format("{0}?user={1}&pass={2}&tbegin={3:yyyy-MM-dd}&tend={4:yyyy-MM-dd}&call={5}", HistoryCls.LinkReport, HistoryCls.UserName, HistoryCls.Password, fromDate, toDate, type);
                var strXML = client.DownloadString(url);

                var doc = XDocument.Parse(strXML);

                var typeName = type == 0 ? "Cuộc gọi đến" : "Cuộc gọi đi";

                return (from c in doc.Descendants("item")
                        select new CallRecord()
                        {
                            uniqueid = c.Attribute("uniqueid").Value,
                            calldate = c.Attribute("calldate").Value,
                            src = c.Attribute("src").Value,
                            dst = c.Attribute("dst").Value,
                            filename = GetAudioUrl(c.Attribute("filename").Value),
                            duration = int.Parse( c.Attribute("duration").Value),
                            billsec = int.Parse( c.Attribute("billsec").Value),
                            type = typeName,
                            status = HistoryCls.GetStatus(typeName, c.Attribute("disposition").Value),
                            line = HistoryCls.GetLine(c.Attribute("src").Value, c.Attribute("dst").Value),
                            staff = HistoryCls.GetStaff(c.Attribute("src").Value, c.Attribute("dst").Value)
                        }).ToList();
            }
            catch
            {
                return null;
            }
        }

        public static void ListenAgain(string phoneNumber, DateTime date)
        {
            try
            {
                var ltHistory = GetHistory(date, date, 0);
                var objRecord = (from h in ltHistory
                                 where (h.src == phoneNumber | h.dst == phoneNumber) & (date - HistoryCls.GetDate(h.calldate)).TotalSeconds >= 0
                                 orderby HistoryCls.GetDate(h.calldate) descending
                                 select h).FirstOrDefault();
                if (objRecord == null)
                {
                    ltHistory = GetHistory(date, date, 1);
                    objRecord = (from h in ltHistory
                                 where (h.src == phoneNumber | h.dst == phoneNumber) & (date - HistoryCls.GetDate(h.calldate)).TotalSeconds >= 0
                                 orderby HistoryCls.GetDate(h.calldate) descending
                                 select h).FirstOrDefault();
                }
                if (objRecord.filename != null && objRecord.filename != "")
                {
                    var _fileName = GetAudioUrl(objRecord.filename);
                    HistoryCls.ListenAgain(_fileName, false);
                }
            }
            catch { }
        }

        public static string GetAudioUrl(string fileName)
        {
            var _result = fileName;
            if (_result != "")
            {
                _result = _result.Replace("audio:", "");
                _result = _result.Replace("/var/spool/asterisk/monitor/", "");
                if (_result.LastIndexOf(".wav") < 0)
                {
                    _result = _result + ".wav";
                }

                if (HistoryCls.LinkAudio.IndexOf('?') > 0)
                {
                    _result = string.Format("{0}&user={1}&pass={2}&audio={3}", HistoryCls.LinkAudio, HistoryCls.UserName, HistoryCls.Password, _result);
                }
                else
                {
                    _result = string.Format("{0}?user={1}&pass={2}&audio={3}", HistoryCls.LinkAudio, HistoryCls.UserName, HistoryCls.Password, _result);
                }

                return _result;
            }

            return _result;
        }
    }

    public class SoftPhones
    {
        DIP.SoftPhoneAPI.SoftPhoneEditor SoftPhoneEditor;

        #region ======================================== Constructor ========================
        public SoftPhones()
        {
            var db = new MasterDataContext();
            try
            {
                var objPBX = db.pbxSettings.First();
                var password = it.EncDec.Decrypt(objPBX.Password);

                DIP.SoftPhoneAPI.HistoryCls.LinkReport = objPBX.LinkReport;
                DIP.SoftPhoneAPI.HistoryCls.UserName = objPBX.UserName;
                DIP.SoftPhoneAPI.HistoryCls.Password = password;
                DIP.SoftPhoneAPI.HistoryCls.LinkAudio = objPBX.LinkAudio;
                DIP.SoftPhoneAPI.HistoryCls.Lines = (from e in db.pbxExtensions
                                                     join n in db.tnNhanViens on e.StaffID equals n.MaNV
                                                     select new DIP.SoftPhoneAPI.CallLine()
                                                     {
                                                         ID = e.ExtenName,
                                                         Name = n.HoTenNV,
                                                     }).ToList();
                DIP.SoftPhoneAPI.HistoryCls.Trunks = new List<string>();
                DIP.SoftPhoneAPI.HistoryCls.Trunks.Add("0473068168");
                //DIP.SoftPhoneAPI.HistoryCls.Trunks.Add("0873056780");
                //DIP.SoftPhoneAPI.HistoryCls.Trunks.Add("18006780");

                var objExten = db.pbxExtensions.First(p => p.StaffID == Common.User.MaNV);
                var passEX = it.EncDec.Decrypt(objExten.Password);

                SoftPhoneEditor = new DIP.SoftPhoneAPI.SoftPhoneEditor(objPBX.Server, objExten.Port.Value, objExten.Display, objExten.ExtenName, passEX);
                SoftPhoneEditor.BeginCall += new DIP.SoftPhoneAPI.BeginCallEventHandler(SoftPhoneEditor_BeginCall);
                SoftPhoneEditor.CustomerAddNewButtonClick += new DIP.SoftPhoneAPI.CustomerAddNewButtonClickEventHandler(SoftPhoneEditor_CustomerAddNewButtonClick);
                SoftPhoneEditor.ContactAddNewButtonClick += new DIP.SoftPhoneAPI.ContactAddNewButtonClickEventHandler(SoftPhoneEditor_ContactAddNewButtonClick);
                SoftPhoneEditor.ReferenceButtonClick += new DIP.SoftPhoneAPI.ReferenceButtonClickEventHandler(SoftPhoneEditor_ReferenceButtonClick);
                SoftPhoneEditor.SaveButtonClick += new DIP.SoftPhoneAPI.SaveButtonClickEventHandler(SoftPhoneEditor_SaveButtonClick);
                SoftPhoneEditor.TransactionPageView += new DIP.SoftPhoneAPI.TransactionPageViewEventHandler(SoftPhoneEditor_TransactionPageView);
                SoftPhoneEditor.HistoryPageView += new DIP.SoftPhoneAPI.HistoryPageViewEventHandler(SoftPhoneEditor_HistoryPageView);
                SoftPhoneEditor.ContactPageView += new DIP.SoftPhoneAPI.ContactPageViewEventHandler(SoftPhoneEditor_ContactPageView);
                SoftPhoneEditor.ReferenceValueChanged += new SoftPhoneAPI.ReferenceValueChangedEventHandler(SoftPhoneEditor_ReferenceValueChanged);
                SoftPhoneEditor.CustomerEditButtonClick += new SoftPhoneAPI.CustomerEditButtonClickEventHandler(SoftPhoneEditor_CustomerEditButtonClick);
                SoftPhoneEditor.CustomerSearchButtonClick += new SoftPhoneAPI.CustomerSearchButtonClickEventHandler(SoftPhoneEditor_CustomerSearchButtonClick);
                SoftPhoneEditor.YeuCauPageView += new YeuCauPageViewEventHandler(SoftPhoneEditor_YeuCauPageView);
                SoftPhoneEditor.CongNoPageView += new CongNoPageViewEventHandler(SoftPhoneEditor_CongNoPageView);
                SoftPhoneEditor.Show();
                SoftPhoneEditor.Hide();
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        void SoftPhoneEditor_CongNoPageView(object sender, CongNoPageViewEventArgs e)
        {
            var db = new MasterDataContext();
            try
            {
                e.Data = (from hd in db.dvHoaDons
                          join l in db.dvLoaiDichVus on hd.MaLDV equals l.ID
                          join mb in db.mbMatBangs on hd.MaMB equals mb.MaMB into tblMatBang
                          from mb in tblMatBang.DefaultIfEmpty()
                          join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB into loaimb
                          from lmb in loaimb.DefaultIfEmpty()
                          join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tblTangLau
                          from tl in tblTangLau.DefaultIfEmpty()
                          join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN into tblKhoiNha
                          from kn in tblKhoiNha.DefaultIfEmpty()
                          where hd.MaKH == e.CustomerID &  hd.IsDuyet == true
                          & (hd.ConNo != 0 || (hd.MaLDV == 49 && hd.ConNo < 0))
                          orderby hd.NgayTT descending
                          select new CongNo
                          {
                              NgayTT = hd.NgayTT,
                              TenLDV = l.TenHienThi,
                              DienGiai = hd.DienGiai,
                              PhaiThu = hd.PhaiThu,
                              DaThu = (from ct in db.ptChiTietPhieuThus
                                       join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                       //join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                                       where hd.ID == ct.LinkID //& SqlMethods.DateDiffMonth(pt.NgayThu, _TuNgay) == 0
                                       select ct.SoTien).Sum().GetValueOrDefault(),
                              ConNo = hd.PhaiThu - (from ct in db.ptChiTietPhieuThus
                                                    join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                                    //join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                                                    where hd.ID == ct.LinkID //& SqlMethods.DateDiffMonth(pt.NgayThu, _TuNgay) == 0
                                                    select ct.SoTien).Sum().GetValueOrDefault(),
                          }).ToList();
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        void SoftPhoneEditor_YeuCauPageView(object sender, YeuCauPageViewEventArgs e)
        {
            var db = new MasterDataContext();
            try
            {
                e.Data = (from p in db.tnycYeuCaus
                          join nv in db.tnNhanViens on p.MaNV equals nv.MaNV into nvs
                          from nv in nvs.DefaultIfEmpty()
                          join ntn in db.tnNhanViens on p.MaNTN equals ntn.MaNV into ntns
                          from ntn in ntns.DefaultIfEmpty()
                          where p.MaKH == e.CustomerID
                          orderby p.NgayYC descending
                          select new SoftPhoneAPI.YeuCau()
                          {
                              MaYC = p.MaYC,
                              NgayYC = p.NgayYC,
                              NoiDung = p.NoiDung,
                              TenTT = p.tnycTrangThai.TenTT,
                              TieuDe = p.TieuDe,
                          }).ToList();
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
        #endregion

        #region ==================== Method ============================================
        string Add9Number(string number)
        {
            if (number.Length < 7)
            {
                return number;
            }
            else
            {
                return number.IndexOf('9') == 0 ? number : ("9" + number);
            }
        }

        string Remove9Number(string number)
        {
            if (number.Length < 7)
            {
                return number;
            }
            else
            {
                return number.IndexOf('9') == 0 ? number.Substring(1) : number;
            }
        }

        public bool Call(string number)
        {
            //number = this.Add9Number(number);
            return SoftPhoneEditor.Call(number);
        }

        public bool Call(string number, int CustomerID)
        {
            //number = this.Add9Number(number);
            return SoftPhoneEditor.Call(number, CustomerID);
        }

        public bool Call(string number, int CustomerID, int LinkType, int LinkID, string LinkName)
        {
            //number = this.Add9Number(number);
            return SoftPhoneEditor.Call(number, CustomerID, LinkType, LinkID, LinkName);
        }

        public void ListenAgain(string callID, DateTime date)
        {
            DIP.SoftPhoneAPI.HistoryCls.ListenAgain(callID, date);
        }

        public void Dispose()
        {
            try
            {
                this.SoftPhoneEditor.Dispose();
                this.SoftPhoneEditor = null;
            }
            catch { }
        }

        #endregion

        #region ============================== Events =================================
        /// <summary>
        /// Luu nhat ky xu ly vao database
        /// </summary>
        /// <param name="sender">DIP.SoftPhoneAPI.SoftPhoneEditor</param>
        /// <param name="e">Get: CallID, Customer, LinkType, LinkID, Note</param>
        void SoftPhoneEditor_SaveButtonClick(object sender, DIP.SoftPhoneAPI.SaveButtonClickEventArgs e)
        {
            var db = new MasterDataContext();
            try
            {
                //if (e.LinkType == 74)
                //{
                //    var objNK = new ncNhatKy();
                //    var objNC = db.ncNhuCaus.Single(p => p.MaNC == e.LinkID);
                //    objNK.MaNC = e.LinkID;
                //    objNK.MaNVQL = objNC.MaNVQL;
                //    objNK.NgayXL = db.GetDate();
                //    objNK.NgayNhap = db.GetDate();
                //    objNK.MaNVN = Common.MaNV;
                //    objNK.DienGiai = e.Note;
                //    objNK.MaHTTX = 1;
                //    objNK.CallID = e.CallID;
                //    if (e.StatusID == null)
                //    {
                //        objNK.MaTT = objNC.MaTT;
                //    }
                //    else
                //    {
                //        objNK.MaTT = (short)e.StatusID;
                //        try
                //        {
                //            var dbo = new MasterDataContext();
                //            objNC = dbo.ncNhuCaus.Single(p => p.MaNC == e.LinkID);
                //            objNC.MaTT = (short?)e.StatusID;
                //            dbo.SubmitChanges();
                //        }
                //        catch
                //        {

                //        }
                //    }
                //    db.ncNhatKies.InsertOnSubmit(objNK);
                //    db.SubmitChanges();

                //    e.Result = true;
                //}
                //else
                //{
                //    var objNK = new cNhatKy();
                //    var objHD = db.cContracts.Single(p => p.ContractID == e.LinkID);
                //    objNK.MaHD = e.LinkID;
                //    objNK.MaNVQL = objHD.SaleID;
                //    objNK.NgayXL = db.GetDate();
                //    objNK.NgayNhap = db.GetDate();
                //    objNK.MaNVN = Common.MaNV;
                //    objNK.DienGiai = e.Note;
                //    objNK.CallID = e.CallID;
                //    if (e.StatusID == null)
                //    {
                //        objNK.MaTT = objHD.StatusID;
                //    }
                //    else
                //    {
                //        objNK.MaTT = e.StatusID;
                //        try
                //        {
                //            var dbo = new MasterDataContext();
                //            objHD = dbo.cContracts.Single(p => p.ContractID == e.LinkID);
                //            objHD.StatusID = e.StatusID;
                //            dbo.SubmitChanges();
                //        }
                //        catch
                //        {

                //        }
                //    }
                //    db.cNhatKies.InsertOnSubmit(objNK);
                //    db.SubmitChanges();

                //    e.Result = true;
                //}
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        /// <summary>
        /// Lload nhat ky xu ly
        /// </summary>
        /// <param name="sender">DIP.SoftPhoneAPI.SoftPhoneEditor</param>
        /// <param name="e">Get: CustomerID | Set: Data</param>
        void SoftPhoneEditor_HistoryPageView(object sender, DIP.SoftPhoneAPI.HistoryPageViewEventArgs e)
        {
            var db = new MasterDataContext();
            try
            {
                //e.Data = (from c in db.ncNhatKies
                //          join n in db.ncNhuCaus on c.MaNC equals n.MaNC
                //          join t in db.ncTrangThais on c.MaTT equals t.MaTT into trangThai
                //          from t in trangThai.DefaultIfEmpty()
                //          join h in db.ncHinhThucTiepXucs on c.MaHTTX equals h.ID into hinhThuc
                //          from h in hinhThuc.DefaultIfEmpty()
                //          join v in db.NhanViens on c.MaNVN equals v.MaNV into tblNhanVien
                //          from v in tblNhanVien.DefaultIfEmpty()
                //          where n.MaKH == e.CustomerID
                //          orderby c.NgayXL descending
                //          select new DIP.SoftPhoneAPI.History()
                //          {
                //              LinkName = "Cơ hội số " + n.SoNC,
                //              Date = c.NgayXL,
                //              Content = c.DienGiai,
                //              Formality = h.TenHTTX,
                //              Status = t.TenTT,
                //              Staff = v.HoTen,
                //              CallID = c.CallID
                //          })
                //          .Union(from c in db.cNhatKies
                //                 join n in db.cContracts on c.MaHD equals n.ContractID
                //                 join t in db.cStatus on c.MaTT equals t.StatusID into trangThai
                //                 from t in trangThai.DefaultIfEmpty()
                //                 join v in db.NhanViens on c.MaNVN equals v.MaNV into tblNhanVien
                //                 from v in tblNhanVien.DefaultIfEmpty()
                //                 where n.CusID == e.CustomerID
                //                 orderby c.NgayXL descending
                //                 select new DIP.SoftPhoneAPI.History()
                //                 {
                //                     LinkName = "Hợp đồng số " + n.ContractNo,
                //                     Date = c.NgayXL,
                //                     Content = c.DienGiai,
                //                     Status = t.StatusName,
                //                     Staff = v.HoTen,
                //                     CallID = c.CallID
                //                 })
                //          .ToList();
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        /// <summary>
        /// Load du lieu lich su giao dich
        /// </summary>
        /// <param name="sender">DIP.SoftPhoneAPI.SoftPhoneEditor</param>
        /// <param name="e">Get: CustomerID | Set: Data</param>
        void SoftPhoneEditor_TransactionPageView(object sender, DIP.SoftPhoneAPI.TransactionPageViewEventArgs e)
        {
             var db = new MasterDataContext();
             try
             {
                 //e.Data = (from nc in db.ncNhuCaus
                 //          join nv in db.NhanViens on nc.MaNVQL equals nv.MaNV into tblNhanVien
                 //          from nv in tblNhanVien.DefaultIfEmpty()
                 //          where nc.MaKH == e.CustomerID
                 //          orderby nc.NgayCN descending
                 //          select new DIP.SoftPhoneAPI.Transaction()
                 //          {
                 //              Type = "Cơ hội",
                 //              Code = nc.SoNC,
                 //              Date = nc.NgayCN,
                 //              Note = nc.DienGiai,
                 //              Money = nc.ThanhTien,
                 //              Staff = nv.HoTen
                 //          })
                 //         .Union(from hd in db.cContracts
                 //                join nv in db.NhanViens on hd.SaleID equals nv.MaNV into tblNhanVien
                 //                from nv in tblNhanVien.DefaultIfEmpty()
                 //                where hd.CusID == e.CustomerID
                 //                orderby hd.SigningDate descending
                 //                select new DIP.SoftPhoneAPI.Transaction()
                 //                {
                 //                    Type = "Hợp đồng",
                 //                    Code = hd.ContractNo,
                 //                    Date = hd.SigningDate,
                 //                    Note = hd.Description,
                 //                    Money = (from ct in db.cProducts where ct.ContractID == hd.ContractID select ct.ThanhTien).Sum(),
                 //                    Staff = nv.HoTen
                 //                }).ToList();

             }
             catch { }
             finally
             {
                 db.Dispose();
             }
        }

        /// <summary>
        /// Load danh sach lien he
        /// </summary>
        /// <param name="sender">DIP.SoftPhoneAPI.SoftPhoneEditor</param>
        /// <param name="e">Get: CustomerID | Set: Data</param>
        void SoftPhoneEditor_ContactPageView(object sender, DIP.SoftPhoneAPI.ContactPageViewEventArgs e)
        {
            var db = new MasterDataContext();
            try
            {
                //e.Data = (from lh in db.NguoiLienHes
                //          join nv in db.NhanViens on lh.MaNVN equals nv.MaNV
                //          join xh in db.QuyDanhs on lh.MaQD equals xh.MaQD into tblXungHo
                //          from xh in tblXungHo.DefaultIfEmpty()
                //          where lh.MaKH == e.CustomerID
                //          orderby lh.NgayNhap descending
                //          select new DIP.SoftPhoneAPI.Contact()
                //          {
                //              Vocative = xh.TenQD,
                //              FullName = lh.HoTen,
                //              Position = lh.TenCV,
                //              Department = lh.TenPB,
                //              PhoneNumber = lh.DiDong,
                //              OtherPhone = lh.DiDongKhac,
                //              Email = lh.Email,
                //              Address = lh.DiaChi,
                //              Note = lh.GhiChu,
                //              Birthday = lh.NgaySinh != null ? lh.NgaySinh.Value.ToString("dd/MM") : "",
                //              Age = lh.Tuoi.GetValueOrDefault()                              
                //          }).ToList();
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        /// <summary>
        /// Show form tham chieu, chon tham chieu
        /// </summary>
        /// <param name="sender">DIP.SoftPhoneAPI.SoftPhoneEditor</param>
        /// <param name="e">Get: Customer | Set: LinkType, LinID, LinkName</param>
        void SoftPhoneEditor_ReferenceButtonClick(object sender, DIP.SoftPhoneAPI.ReferenceButtonClickEventArgs e)
        {
            //var frm = new frmChungTu();
            //frm.MaKH = e.CusID;
            //frm.ShowDialog();
            //if (frm.DialogResult == DialogResult.OK)
            //{
            //    e.LinkID = frm.LinID;
            //    e.LinkType = frm.LinkType;
            //    e.LinkName = frm.LinkName;
            //    e.StatusID = frm.MaTT;
            //}
        }
        
        /// <summary>
        /// Them moi khach hang
        /// </summary>
        /// <param name="sender">DIP.SoftPhoneAPI.SoftPhoneEditor</param>
        /// <param name="e">Get: Number | Set: Customers</param>
        void SoftPhoneEditor_CustomerAddNewButtonClick(object sender, DIP.SoftPhoneAPI.CustomerAddNewButtonClickEventArgs e)
        {
            var db = new MasterDataContext();
            try
            {
                //using (var frm = new DIPCRM.Customer.frmEdit())
                //{
                //    frm.PhoneNumber = this.Remove9Number(e.Number);
                //    frm.ShowDialog();
                //    if (frm.IsSave)
                //    {
                //        e.Customer = (from p in db.KhachHangs
                //                      join nlh in db.NguoiLienHes on p.MaNLH equals nlh.ID into tblNguoiLienHe
                //                      from nlh in tblNguoiLienHe.DefaultIfEmpty()
                //                      where db.FormatPhone(p.DiDong) == this.Remove9Number(e.Number) | db.FormatPhone(p.DienThoaiCT) == this.Remove9Number(e.Number)
                //                      orderby p.NgayCN descending
                //                      select new DIP.SoftPhoneAPI.Customer()
                //                      {
                //                          ID = p.MaKH,
                //                          Code = p.TenVietTat,
                //                          Name = p.TenCongTy,
                //                          FullName = nlh.HoTen,
                //                          Email = p.Email,
                //                          Address = p.DiaChiCT,
                //                          Note = p.GhiChu
                //                      }).FirstOrDefault();
                //    }
                //}
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        /// <summary>
        /// Sửa khách hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SoftPhoneEditor_CustomerEditButtonClick(object sender, CustomerEditButtonClickEventArgs e)
        {
            var db = new MasterDataContext();
            try
            {
                //using (var frm = new DIPCRM.Customer.frmEdit())
                //{
                //    frm.ID = e.ID;
                //    frm.ShowDialog();
                //    if (frm.IsSave)
                //    {
                //        e.Customer = (from p in db.KhachHangs
                //                      join nlh in db.NguoiLienHes on p.MaNLH equals nlh.ID into tblNguoiLienHe
                //                      from nlh in tblNguoiLienHe.DefaultIfEmpty()
                //                      join n in db.NhomKHs on p.MaNKH equals n.MaNKH into nhom
                //                      from n in nhom.DefaultIfEmpty()
                //                      where p.MaKH == e.ID
                //                      select new DIP.SoftPhoneAPI.Customer()
                //                      {
                //                          ID = p.MaKH,
                //                          Code = p.TenVietTat,
                //                          Name = p.TenCongTy,
                //                          FullName = nlh.HoTen,
                //                          Email = p.Email,
                //                          Address = p.DiaChiCT,
                //                          Note = p.GhiChu,
                //                          Type = n.TenNKH
                //                      }).FirstOrDefault();
                //    }
                //}
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        /// <summary>
        /// Tìm kiếm khách hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SoftPhoneEditor_CustomerSearchButtonClick(object sender, CustomerSearchButtonClickEventArgs e)
        {
            var db = new MasterDataContext();
            try
            {
                //var frm = new DIPCRM.Customer.frmFind();
                //frm.ShowDialog();
                //if (frm.DialogResult == DialogResult.OK)
                //{
                //    e.Customer = (from p in db.KhachHangs
                //                  join nlh in db.NguoiLienHes on p.MaNLH equals nlh.ID into tblNguoiLienHe
                //                  from nlh in tblNguoiLienHe.DefaultIfEmpty()
                //                  join n in db.NhomKHs on p.MaNKH equals n.MaNKH into nhom
                //                  from n in nhom.DefaultIfEmpty()
                //                  where p.MaKH == frm.MaKH
                //                  select new DIP.SoftPhoneAPI.Customer()
                //                  {
                //                      ID = p.MaKH,
                //                      Code = p.TenVietTat,
                //                      Name = p.TenCongTy,
                //                      FullName = nlh.HoTen,
                //                      Email = p.Email,
                //                      PhoneNumber = p.DiDong == "" ? p.DienThoaiCT : p.DiDong,
                //                      Address = p.DiaChiCT,
                //                      Note = p.GhiChu,
                //                      Type = n.TenNKH
                //                  }).FirstOrDefault();
                //}
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        /// <summary>
        /// Them nguoi lien he
        /// </summary>
        /// <param name="sender">DIP.SoftPhoneAPI.SoftPhoneEditor</param>
        /// <param name="e">Get: Number, customerID</param>
        void SoftPhoneEditor_ContactAddNewButtonClick(object sender, DIP.SoftPhoneAPI.ContactAddNewButtonClickEventArgs e)
        {
            //using (var frm = new KyThuat.KhachHang.frmEdit())
            //{
            //    frm.PhoneNumber = this.Remove9Number(e.Number);
            //    frm.MaKH = e.CustomerID;
            //    frm.ShowDialog();
            //    e.Result = frm.IsSave;
            //}
        }

        /// <summary>
        /// Bat dau ket noi
        /// Nhan dien khach hang, nguoi lien he, bang so dien thoai
        /// </summary>
        /// <param name="sender">DIP.SoftPhoneAPI.SoftPhoneEditor</param>
        /// <param name="e">Number, Customers</param>
        void SoftPhoneEditor_BeginCall(object sender, DIP.SoftPhoneAPI.BeginCallEventArgs e)
        {
            var db = new MasterDataContext();
            try
            {
                if (this.Remove9Number(e.Number).Length < 7)
                {
                    e.Customers = (from ex in db.pbxExtensions
                                   join nv in db.tnNhanViens on ex.StaffID equals nv.MaNV
                                   where ex.ExtenName == this.Remove9Number(e.Number)
                                   select new DIP.SoftPhoneAPI.Customer()
                                   {
                                       ID = nv.MaNV,
                                       Code = nv.MaSoNV,
                                       Name = nv.HoTenNV,
                                       FullName = nv.HoTenNV,
                                       Email = nv.Email,
                                       Address = nv.DiaChi,
                                       Note = nv.DienThoai
                                   }).ToList();
                    return;
                }

                string phone = this.Remove9Number(e.Number);//.Substring(1);
                e.Customers = (from kh in db.tnKhachHangs
                               where db.FormatPhone(kh.DienThoaiKH) == phone
                               select new DIP.SoftPhoneAPI.Customer()
                               {
                                   ID = kh.MaKH,
                                   Code = kh.KyHieu,
                                   MaSoMB = kh.mbMatBangs.FirstOrDefault().MaSoMB,
                                   Name = kh.HoKH+" "+kh.TenKH,
                                   FullName = kh.HoKH + " " + kh.TenKH,
                                   Email = kh.EmailKH,
                                   Address = kh.DCTT,
                                   Note = ""
                               }).ToList();
                             
            }
            catch { }

            finally
            {
                db.Dispose();
            }

        }


        void SoftPhoneEditor_ReferenceValueChanged(object sender, SoftPhoneAPI.ReferenceValueChangedEventArgs e)
        {
            var db = new MasterDataContext();
            try
            {
                //e.StatusList = (from p in db.cTrangThaiNKs
                //                where p.TenTT.Trim() != ""
                //                orderby p.STT
                //                select new DIP.SoftPhoneAPI.HistoryStatus()
                //                {
                //                    ID = (int)p.ID,
                //                    Name = p.TenTT
                //                }).ToList();
                //if (e.LinkType == 88)
                //{
                //    e.StatusList = (from p in db.cStatus
                //                    select new DIP.SoftPhoneAPI.HistoryStatus()
                //                    {
                //                        ID = p.StatusID,
                //                        Name = p.StatusName
                //                    }).ToList();
                //}
                //else
                //{
                //    e.StatusList = (from p in db.ncTrangThais
                //                    where p.TenTT.Trim()!=""
                //                    orderby p.STT
                //                    select new DIP.SoftPhoneAPI.HistoryStatus()
                //                    {
                //                        ID = (int)p.MaTT,
                //                        Name = p.TenTT
                //                    }).ToList();
                //}
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
        #endregion
    }
}
