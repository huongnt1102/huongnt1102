using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.API.Native;
using Library;
using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildingDesignTemplate.Class
{
    public class ThongBaoPhi
    {
        /// <summary>
        /// MERGE THÔNG BÁO PHÍ
        /// </summary>
        /// <param name="towerId">TÒA NHÀ</param>
        /// <param name="month">THÁNG</param>
        /// <param name="year">NĂM</param>
        /// <param name="customerId">MÃ KHÁCH HÀNG</param>
        /// <param name="serviceIds">DỊCH VỤ</param>
        /// <param name="bankAccountId">TÀI KHOẢN NGÂN HÀNG</param>
        /// <param name="rtfText"></param>
        /// <param name="oldDept">NỢ CŨ</param>
        /// <param name="prePay">THU TRƯỚC</param>
        /// <returns></returns>
        public static string Merge
            (
                byte towerId,
                int month,
                int year,
                int customerId,
                List<int> serviceIds,
                int bankAccountId,
                string rtfText,
                decimal oldDept,
                decimal prePay,
                Library.CongNoCls.DataCongNo dept = null,
                bool IsUseApartment = false,
                int ApartmentId = 0,
                string SoThongBaoPhi = ""
            )
        {
            if (customerId == 0) return rtfText;

            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };

            try
            {
                var document = ctlRtf.Document;

                ctlRtf.Document.RtfText = Public(ctlRtf, year, month, customerId, bankAccountId, dept, ApartmentId);

                var tableList = (from p in document.Tables
                                 select new
                                 {
                                     cellName = document.GetText(p.Rows[0].Cells[0].Range).Replace(" ", "")
                                 })
                                 .AsEnumerable()
                                 .Select((t, indexs) => new Field
                                 {
                                     Index = indexs,
                                     Name = t.cellName
                                 })
                                 .ToList();

                // Thông báo phí dịch vụ khách mua theo tháng
                ctlRtf = Orientation("[DebitRiverPark]", towerId, month, year, customerId, ctlRtf, oldDept, document, tableList, prePay);

                // Thông báo thu phí diện khách thuê theo tháng
                ctlRtf = Orientation("[DeptMeLing]", towerId, month, year, customerId, ctlRtf, oldDept, document, tableList, prePay);

                // Thông báo thu phí nước khách thuê theo tháng
                ctlRtf = Orientation("[DeptWater]", towerId, month, year, customerId, ctlRtf, oldDept, document, tableList, prePay);

                ctlRtf = Orientation("[ERPN]", towerId, month, year, customerId, ctlRtf, oldDept, document, tableList, prePay, ApartmentId);
                ctlRtf = Orientation("[ETPN]", towerId, month, year, customerId, ctlRtf, oldDept, document, tableList, prePay, ApartmentId);

                ctlRtf = Orientation("[DVHD]", towerId, month, year, customerId, ctlRtf, oldDept, document, tableList, prePay, ApartmentId);

                ctlRtf = Orientation("[DNHD]", towerId, month, year, customerId, ctlRtf, oldDept, document, tableList, prePay, ApartmentId);

                ctlRtf = Orientation("[SPS]", towerId, month, year, customerId, ctlRtf, oldDept, document, tableList, prePay);

                ctlRtf = Orientation("[OldDebts]", towerId, month, year, customerId, ctlRtf, oldDept, document, tableList, prePay);

                ctlRtf = Orientation("[OldDebtsHD]", towerId, month, year, customerId, ctlRtf, oldDept, document, tableList, prePay);
                ctlRtf = Orientation("[OldDebtsDNHD]", towerId, month, year, customerId, ctlRtf, oldDept, document, tableList, prePay);

                ctlRtf = Orientation("[StopSv]", towerId, month, year, customerId, ctlRtf, oldDept, document, tableList, prePay);
                ctlRtf = Orientation("[NuocTTTM]", towerId, month, year, customerId, ctlRtf, oldDept, document, tableList, prePay);
                ctlRtf = Orientation("[DienTTTM]", towerId, month, year, customerId, ctlRtf, oldDept, document, tableList, prePay);

                ctlRtf = Orientation("[TBP_TTTM]", towerId, month, year, customerId, ctlRtf, oldDept, document, tableList, prePay, ApartmentId);


                return ctlRtf.Document.RtfText;
            }
            catch (System.Exception ex)
            {
                //string mes = Translate.TranslateGoogle.TranslateText(ex.Message, "en-us", "vi-vn");
                DevExpress.XtraEditors.XtraMessageBoxArgs args = new DevExpress.XtraEditors.XtraMessageBoxArgs();
                //args.AutoCloseOptions.Delay = 1000;
                args.Caption = ex.GetType().FullName;
                args.Text = ex.Message;
                args.Buttons = new System.Windows.Forms.DialogResult[] { System.Windows.Forms.DialogResult.OK, System.Windows.Forms.DialogResult.Cancel };
                DevExpress.XtraEditors.XtraMessageBox.Show(args).ToString();
            }
            return rtfText;
        }

        /// <summary>
        /// ĐỊNH HƯỚNG
        /// </summary>
        /// <param name="name"></param>
        /// <param name="towerId"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="customerId"></param>
        /// <param name="ctlRtf"></param>
        /// <param name="oldDept">NỢ CŨ</param>
        /// <param name="document"></param>
        /// <param name="fields"></param>
        /// <param name="prePay">THU TRƯỚC</param>
        /// <returns></returns>
        public static DevExpress.XtraRichEdit.RichEditControl Orientation
            (
                string name,
                byte towerId,
                int month,
                int year,
                int customerId,
                DevExpress.XtraRichEdit.RichEditControl ctlRtf,
                decimal oldDept,
                Document document,
                List<Field> fields,
                decimal prePay,
                int ApartmentId = 0
            )
        {
            Field field = GetField(fields, name);
            if (field != null)
            {
                switch (name)
                {
                    case "[ERPN]":
                        ctlRtf.Document.RtfText = GetDebitNote1(customerId, month, year, ctlRtf, field, document, ApartmentId);
                        ctlRtf.Document.ReplaceAll("[ERPN]", "", SearchOptions.None);
                        break;
                    case "[ETPN]":
                        ctlRtf.Document.RtfText = GetDebitNoteETP(customerId, month, year, ctlRtf, field, document, ApartmentId);
                        ctlRtf.Document.ReplaceAll("[ETPN]", "", SearchOptions.None);
                        break;

                    case "[DVHD]":
                        ctlRtf.Document.RtfText = GetDebitNoteDVHD(customerId, month, year, ctlRtf, field, document, ApartmentId);
                        ctlRtf.Document.ReplaceAll("[DVHD]", "", SearchOptions.None);
                        break;

                    case "[DNHD]":
                        ctlRtf.Document.RtfText = GetDebitNoteDNHD(customerId, month, year, ctlRtf, field, document, ApartmentId);
                        ctlRtf.Document.ReplaceAll("[DNHD]", "", SearchOptions.None);
                        break;

                    case "[TBP_TTTM]":
                        ctlRtf.Document.RtfText = GetDebitNoteTTTM(customerId, month, year, ctlRtf, field, document, ApartmentId);
                        ctlRtf.Document.ReplaceAll("[TBP_TTTM]", "", SearchOptions.None);
                        break;

                    case "[DebitRiverPark]":
                        ctlRtf.Document.RtfText = DeptServiceRP(towerId, month, year, customerId, ctlRtf, oldDept, field, document);
                        ctlRtf.Document.ReplaceAll("[DebitRiverPark]", "", SearchOptions.None);
                        break;

                    case "[DeptMeLing]":
                        ctlRtf.Document.RtfText = DeptMeLing(towerId, month, year, customerId, ctlRtf, oldDept, field, document);
                        ctlRtf.Document.ReplaceAll("[DeptMeLing]", "", SearchOptions.None);
                        break;

                    case "[SPS]":
                        ctlRtf.Document.RtfText = StopProvidingService(customerId, month, year, ctlRtf, field, document);
                        ctlRtf.Document.ReplaceAll("[SPS]", "", SearchOptions.None);
                        break;

                    case "[OldDebts]":
                        ctlRtf.Document.RtfText = OldDebts(customerId, month, year, ctlRtf, field, document);
                        ctlRtf.Document.ReplaceAll("[OldDebts]", "", SearchOptions.None);
                        break;

                    case "[OldDebtsHD]":
                        ctlRtf.Document.RtfText = OldDebtsHD(customerId, month, year, ctlRtf, field, document);
                        ctlRtf.Document.ReplaceAll("[OldDebtsHD]", "", SearchOptions.None);
                        break;

                    case "[OldDebtsDNHD]":
                        ctlRtf.Document.RtfText = OldDebtsDNHD(customerId, month, year, ctlRtf, field, document);
                        ctlRtf.Document.ReplaceAll("[OldDebtsDNHD]", "", SearchOptions.None);
                        break;

                    case "[StopSv]":
                        ctlRtf.Document.RtfText = StopProvidingService(customerId, month, year, ctlRtf, field, document);
                        ctlRtf.Document.ReplaceAll("[StopSv]", "", SearchOptions.None);
                        break;
                    case "[NuocTTTM]":
                        ctlRtf.Document.RtfText = ThongBaoNuoc(customerId, month, year, ctlRtf, field, document);
                        ctlRtf.Document.ReplaceAll("[NuocTTTM]", "", SearchOptions.None);
                        break;
                    case "[DienTTTM]":
                        ctlRtf.Document.RtfText = ThongBaoDien(customerId, month, year, ctlRtf, field, document);
                        ctlRtf.Document.ReplaceAll("[DienTTTM]", "", SearchOptions.None);
                        break;
                }
            }
            return ctlRtf;
        }

        public static Field GetField(List<Field> fields, string name)
        {
            return fields.FirstOrDefault(_ => _.Name.Contains(name));
        }

        public static string Public
            (
                DevExpress.XtraRichEdit.RichEditControl ctlRtf,
                int year,
                int month,
                int customerId,
                int bankAccountId,
                Library.CongNoCls.DataCongNo dept = null,
                int ApartmentId = 0
            )
        {
            // Tháng hiện tại
            var currentMonth = new DateTime(year, month, 1);
            // Cuối tháng trước
            var endLastMonth = currentMonth.AddDays(-1);
            // Đầu tháng trước nữa
            var earlyLastMonth = currentMonth.AddMonths(-2);
            // Cuối tháng này
            var endEarlyLastMonth = currentMonth.AddMonths(1).AddDays(-1);
            // Tháng sau
            var nextMonth = currentMonth.AddMonths(1);

            ctlRtf.Document.ReplaceAll("[Month]", currentMonth.Month.ToString("00"), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[Year]", year.ToString(), SearchOptions.None);

            ctlRtf.Document.ReplaceAll("[Thang1]", endLastMonth.Month.ToString("00"), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NgayCuoiThang1]", endLastMonth.Day.ToString("00"), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NgayCuoiThang]", endEarlyLastMonth.Day.ToString("00"), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[Nam1]", endLastMonth.Year.ToString(), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[LastMonth]", endLastMonth.Month.ToString("00"), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[LastYear]", endLastMonth.Year.ToString(), SearchOptions.None);

            ctlRtf.Document.ReplaceAll("[DayPrint]", System.DateTime.Now.Day.ToString(), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[MonthPrint]", System.DateTime.Now.Month.ToString(), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[YearPrint]", System.DateTime.Now.Year.ToString(), SearchOptions.None);

            ctlRtf.Document.ReplaceAll("[thang_sau]", nextMonth.Month.ToString("00"), SearchOptions.None);

            // Get Info Bank Account
            var db = new MasterDataContext();
            var bankId = db.nhTaiKhoans
                .Where(tk => tk.ID == bankAccountId)
                .Select(tk => tk.SoTK)
                .FirstOrDefault();
            var idBank = db.nhTaiKhoans
                .Where(tk => tk.SoTK == bankId)
                .Select(tk => tk.MaNH)
                .FirstOrDefault();
            var bankName = db.nhNganHangs
                .Where(nh => nh.ID == idBank)
                .Select(nh => nh.TenNH)
                .FirstOrDefault();
            ctlRtf.Document.ReplaceAll("[TBP.TD]", bankId, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[Ngân hàng]", bankName, SearchOptions.None);

            // Get Dept Customer Infor
            var model = new { ma_kh = customerId, ma_mb = ApartmentId };
            var param = new Dapper.DynamicParameters();
            param.AddDynamicParams(model);
            var customerInfor = Library.Class.Connect.QueryConnect.Query<DeptCustomer>("dv_hoadon_thong_bao_phi_kh", param);

            if (customerInfor.Count() > 0)
            {
                var item = customerInfor.First();

                ctlRtf.Document.ReplaceAll("[CustomerName]", item.TenKH != null ? item.TenKH : "", SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Số nhà]", item.SoNha != null ? item.SoNha : "", SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Mã mặt bằng]", item.MaSoMB != null ? item.MaSoMB : "", SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[CustomerNumber]", item.KyHieu != null ? item.KyHieu : "", SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[DepartmentName]", item.MaSoMB != null ? item.MaSoMB : "", SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[ContractNumber]", item.SoHDCT != null ? item.SoHDCT : "", SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[ContractNumber]", item.SoHDCT != null ? item.SoHDCT : "", SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[DepartmentNumber]", item.SoNha != null ? item.SoNha : "", SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[DepartmentCode]", item.MaSo != null ? item.MaSo : "", SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[BlockName]", item.BlockName != null ? item.BlockName : "", SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[TowerName]", item.TenTN != null ? item.TenTN : "", SearchOptions.None);
            }

            if(ApartmentId == 0)
            {
                //if (dept != null)
                //{
                //    ctlRtf.Document.RtfText = MergeDeptValue(ctlRtf, dept);
                //}

                var deptValue = Library.Class.Connect.QueryConnect.QueryData<Model.DeptCustomerByApartment>("cnGetDebitNote",
                    new
                    {
                        CustomerId = customerId,
                        Month = month,
                        Year = year,
                        TuNgay = currentMonth,
                        DenNgay = endEarlyLastMonth
                    });
                if (deptValue.Count() > 0)
                {
                    var item = deptValue.First();
                    Library.CongNoCls.DataCongNo data = new Library.CongNoCls.DataCongNo
                    {
                        NoDauKy = item.DauKy,
                        PhatSinh = item.PhatSinh,
                        DaThu = item.DaThu,
                        KhauTru = 0,
                        ThuTruoc = item.ThuTruoc,
                        ConNo = item.ConNo,
                        NoCuoi = item.ConNoCuoi,
                        //EMC
                        NoDauKyEMC = item.DauKyEMC,
                        PhatSinhEMC = item.PhatSinhEMC,
                        DaThuEMC = item.DaThuEMC,
                        KhauTruEMC = item.KhauTruEMC,
                        ThuTruocEMC = item.ThuTruocEMC,
                        ConNoEMC = item.ConNoEMC,
                        NoCuoiEMC = item.ConNoCuoiEMC
                    };
                    ctlRtf.Document.RtfText = MergeDeptValue(ctlRtf, data);
                }
            }
            else
            {
                var deptValue = Library.Class.Connect.QueryConnect.QueryData<Model.DeptCustomerByApartment>("cnGetDebitNoteByApartment",
                    new
                    {
                        CustomerId = customerId,
                        Month = month,
                        Year = year,
                        TuNgay = currentMonth,
                        DenNgay = endEarlyLastMonth,
                        ApartmentId = ApartmentId
                    });
                if(deptValue.Count() > 0)
                {
                    var item = deptValue.First();
                    Library.CongNoCls.DataCongNo data = new Library.CongNoCls.DataCongNo
                    {
                        NoDauKy = item.DauKy,
                        PhatSinh = item.PhatSinh,
                        DaThu = item.DaThu,
                        KhauTru = 0,
                        ThuTruoc = item.ThuTruoc,
                        ConNo = item.ConNo,
                        NoCuoi = item.ConNoCuoi,
                        //EMC
                        NoDauKyEMC = item.DauKyEMC,
                        PhatSinhEMC = item.PhatSinhEMC,
                        DaThuEMC = item.DaThuEMC,
                        KhauTruEMC = item.KhauTruEMC,
                        ThuTruocEMC = item.ThuTruocEMC,
                        ConNoEMC = item.ConNoEMC,
                        NoCuoiEMC = item.ConNoCuoiEMC
                    };
                    ctlRtf.Document.RtfText = MergeDeptValue(ctlRtf, data);
                }
            }


            return ctlRtf.Document.RtfText;
        }

        public static string MergeDeptValue(DevExpress.XtraRichEdit.RichEditControl ctlRtf, Library.CongNoCls.DataCongNo dept)
        {
            ctlRtf.Document.ReplaceAll("[MonthlyFee]", string.Format("{0:#,0}", dept.PhatSinh), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[MonthlyFeeEMC]", string.Format("{0:#,0}", dept.PhatSinhEMC), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[Pay]", string.Format("{0:#,0}", dept.ConNo < 0 ? 0 : dept.ConNo), SearchOptions.None);
            var conNoEMC = (dept.NoDauKyEMC > 0 ? dept.NoDauKyEMC : 0) + (dept.PhatSinhEMC > 0 ? dept.PhatSinhEMC : 0) - (dept.DaThuEMC + dept.KhauTruEMC + dept.ThuTruocEMC);
            ctlRtf.Document.ReplaceAll("[PayEMC]", string.Format("{0:#,0}", conNoEMC < 0 ? 0 : conNoEMC), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[MonthlyPaid]", string.Format("{0:#,0.##; #,0.##;}", dept.DaThu + dept.KhauTru), SearchOptions.None);

            // Dư nợ kỳ trước
            decimal? OpeningBalance = dept.NoDauKy - dept.DaThu - dept.KhauTru - dept.ThuTruoc;
            string sign = "";
            if (OpeningBalance.GetValueOrDefault() > 0)
            {
                sign = "+ ";
            }
            if (OpeningBalance.GetValueOrDefault() < 0)
            {
                sign = "- ";
            }
            ctlRtf.Document.ReplaceAll("[Sign]", sign, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[Ob]", string.Format("{0:#,0.##; #,0.##;}", OpeningBalance), SearchOptions.None);

            ctlRtf.Document.ReplaceAll("[OldDept]", string.Format("{0:#,0.##; #,0.##;}", dept.NoDauKy), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[OldDeptEMC]", string.Format("{0:#,0.##; #,0.##;}", dept.NoDauKyEMC), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[ObDept]", string.Format("{0:#,0.##; #,0.##;}", dept.ThuTruoc), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[Paid]", string.Format("{0:#,0.##; #,0.##;}", dept.DaThu + dept.KhauTru + dept.ThuTruoc), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[PaidEMC]", string.Format("{0:#,0.##; #,0.##;}", dept.DaThuEMC + dept.KhauTruEMC + dept.ThuTruocEMC), SearchOptions.None);

            long amount = Convert.ToInt64(Math.Round((decimal)(dept.ConNo < 0 ? 0 : dept.ConNo), 0));
            var amountElectricText = new TienTeCls().DocTienBangChu(amount);
            var _tientienganh = new TienTeCls().DocTienBangChuEN(amount, "VND");
            ctlRtf.Document.ReplaceAll("[PayText]", amountElectricText, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[PayEngText]", _tientienganh, SearchOptions.None);
            //EMC
            long amountEmc = Convert.ToInt64(Math.Round((decimal)(conNoEMC < 0 ? 0 : conNoEMC), 0));
            var amountElectricTextEMC = new TienTeCls().DocTienBangChu(amountEmc);
            var _tientienganhEMC = new TienTeCls().DocTienBangChuEN(amountEmc, "VND");
            ctlRtf.Document.ReplaceAll("[PayTextEMC]", amountElectricTextEMC, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[PayEngTextEMC]", _tientienganhEMC, SearchOptions.None);

            long amountPs = Convert.ToInt64(Math.Round((decimal)((dept.NoDauKy + dept.PhatSinh) < 0 ? 0 : (dept.NoDauKy + dept.PhatSinh)), 0));
            ctlRtf.Document.ReplaceAll("[PayPs]", string.Format("{0:#,0}", amountPs), SearchOptions.None);

            var amountElectricTextPs = new TienTeCls().DocTienBangChu(amountPs);
            var _tientienganhPs = new TienTeCls().DocTienBangChuEN(amount, "");
            ctlRtf.Document.ReplaceAll("[PayTextPs]", amountElectricTextPs, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[PayEngTextPs]", _tientienganhPs, SearchOptions.None);

            ctlRtf.Document.ReplaceAll("[MonthDeptFee]", string.Format("{0:#,0.##; #,0.##;}", dept.PhatSinh - dept.DaThu - dept.KhauTru), SearchOptions.None);
            long payFee = Convert.ToInt64(Math.Round((decimal)(dept.ConNo < 0 ? 0 : dept.ConNo), 0));
            ctlRtf.Document.ReplaceAll("[PayFee]", string.Format("{0:#,0}", payFee), SearchOptions.None);

            var payFeeText = new TienTeCls().DocTienBangChu(payFee);
            var payFeeEndText = new TienTeCls().DocTienBangChuEN(payFee, "");
            ctlRtf.Document.ReplaceAll("[PayFeeText]", amountElectricTextPs, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[PayFeeEngText]", _tientienganhPs, SearchOptions.None);

            return ctlRtf.Document.RtfText;
        }

        public static string GetQuater(int month)
        {
            string quater = "";
            if (month >= 1 & month <= 3)
            {
                quater = "I";
            }
            if (month >= 4 & month <= 6)
            {
                quater = "II";
            }
            if (month >= 7 & month <= 9)
            {
                quater = "III";
            }
            if (month >= 10 & month <= 12)
            {
                quater = "IV";
            }

            return quater;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="towerId"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="customerId"></param>
        /// <param name="ctlRtf"></param>
        /// <param name="oldDept">NỢ CŨ</param>
        /// <param name="field"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public static string DeptServiceRP
            (
                byte towerId,
                int month,
                int year,
                int customerId,
                DevExpress.XtraRichEdit.RichEditControl ctlRtf,
                decimal oldDept,
                Field field,
                Document document
            )
        {
            #region Chi tiết nước
            var objDataW = Library.Class.Connect.QueryConnect.QueryData<DeptServiceRPWater>("DeptServiceRPWater",
                new
                {
                    CustomerId = customerId,
                    Date = new DateTime(year, month, 1)
                });

            TableHandle<DeptServiceRPWater>(field, document, objDataW.ToList(), 29);

            //Calculate water bill before and after tax.
            double sumWaterA = objDataW.Sum(obj => (int)obj.WaterA);
            var waterVat = sumWaterA * 0.05;
            var totalAfterVat = sumWaterA + waterVat;
            //Render
            ctlRtf.Document.ReplaceAll("[SumWaterA]", string.Format("{0:#,0}", sumWaterA), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[WaterVAT]", string.Format("{0:#,0}", waterVat), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TotalAfterVAT]", string.Format("{0:#,0}", totalAfterVat), SearchOptions.None);
            #endregion

            #region 

            var objData = Library.Class.Connect.QueryConnect.QueryData<DebitRiverPark>("DeptServiceRP",
                new
                {
                    CustomerId = customerId,
                    Date = new DateTime(year, month, 1)
                });

            if(objData.Count() > 0)
            {
                var itemFirst = objData.FirstOrDefault();

                ctlRtf.Document.ReplaceAll("[Service]", string.Format("{0:#,0}", itemFirst.Service), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[OldS]", string.Format("{0:#,0}", itemFirst.OldS), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[CurrS]", string.Format("{0:#,0}", itemFirst.CurrS), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Area]", string.Format("{0:#,0}", itemFirst.Area), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[UnitPrice]", string.Format("{0:#,0}", itemFirst.UnitPrice), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Car]", string.Format("{0:#,0}", itemFirst.Car), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[OldC]", string.Format("{0:#,0}", itemFirst.OldC), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[CurrC]", string.Format("{0:#,0}", itemFirst.CurrC), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[CarUP]", string.Format("{0:#,0}", itemFirst.CarUP), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[CardQty]", string.Format("{0:#,0}", itemFirst.CardQty), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[MotorUP]", string.Format("{0:#,0}", itemFirst.MotorUP), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[MotorQty]", string.Format("{0:#,0}", itemFirst.MotorQty), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[EBikeUP]", string.Format("{0:#,0}", itemFirst.EBikeUP), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[EBikeQty]", string.Format("{0:#,0}", itemFirst.EBikeQty), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[BikeUP]", string.Format("{0:#,0}", itemFirst.BikeUP), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[BikeQty]", string.Format("{0:#,0}", itemFirst.BikeQty), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[OldE]", string.Format("{0:#,0}", itemFirst.OldE), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Electric]", string.Format("{0:#,0}", itemFirst.Electric), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[CurrW]", string.Format("{0:#,0}", itemFirst.CurrW), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[OldW]", string.Format("{0:#,0}", itemFirst.OldW), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Water]", string.Format("{0:#,0}", itemFirst.Water), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[WaterUP]", string.Format("{0:#,0}", itemFirst.WaterUP), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[OldWM]", string.Format("{0:#,0}", itemFirst.OldWM), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[NewWM]", string.Format("{0:#,0}", itemFirst.NewWM), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[QtyWM]", string.Format("{0:#,0}", itemFirst.QtyWM), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[WaterExVAT]", string.Format("{0:#,0}", itemFirst.WaterExVAT), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Amount]", string.Format("{0:#,0}", itemFirst.Amount), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[TN.ChiSoCu]", string.Format("{0:#,0}", itemFirst.OldWM), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[TN.ChiSoMoi]", string.Format("{0:#,0}", itemFirst.NewWM), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[TN.TieuThu]", string.Format("{0:#,0}", itemFirst.QtyWM), SearchOptions.None);
            }

            #endregion

            return ctlRtf.Document.RtfText;
        }

        public static Table TableHandle<T>
    (
        Field field,
        Document document,
        List<T> result,
        int index
    )
        {
            var table = document.Tables[field.Index];
            //var index = 18;
            var index_old = index;
            var rField = table.Rows[index];

            var ltFieldName = (from cell in rField.Cells
                               let cellName = document.GetText(cell.Range).Replace(Environment.NewLine, "")
                               select new
                               {
                                   Index = cell.Index,
                                   Name = cellName
                               }).ToList(); //.Replace(" ", "")

            int item_check = 1;
            bool checkBold = false;

            foreach (var r in result)
            {
                var row = table.Rows.InsertBefore(index + 1);
                int indexCell = 0;
                TableCell cellFrom = null;
                foreach (var f in ltFieldName)
                {
                    TableCell cellTo = null;
                    try
                    {
                        if (r.GetType().GetProperty("NoInt") != null)
                        {
                            if (r.GetType().GetProperty("NoInt").GetValue(r, null).ToString() == "0")
                            {
                                checkBold = true;
                            }
                            else
                            {
                                checkBold = false;
                            }
                        }
                        //var cell = row[f.Index];
                        var cell = row[indexCell];
                        //var celltest = document.Tables.GetTableCell(document.CaretPosition);
                        var tx = r.GetType().GetProperty(f.Name.Trim().Replace("[", "").Replace("]", "")) != null
                            ? r.GetType().GetProperty(f.Name.Trim().Replace("[", "").Replace("]", "")).GetValue(r, null)
                            : null;
                        var text = f.Name;
                        if (tx != null)
                        {
                            cellFrom = cell;

                            text = tx.ToString();

                            if (f.Name.Trim() == "[Froms]"
                            || f.Name.Trim() == "[Tos]"
                            || f.Name.Trim() == "[DueDate]")
                            {
                                //text = string.Format("{0:d.M.yyyy}", tx);
                                text = string.Format("{0:d.M}", tx);
                            }
                            else if (f.Name.Trim() == "[PTax]" || f.Name.Trim() == "[Tax]")
                            {
                                text = string.Format("{0:#,0.##; -#,0.##;}%", tx);
                            }
                            else
                            {
                                text = string.Format("{0:#,#.##; -#,#.##;}", tx);
                            }

                            if(r.GetType().GetProperty("NoInt").GetValue(r, null).ToString() == "2")
                            {
                                CharacterProperties cp = document.BeginUpdateCharacters(cell.Range);
                                cp.Bold = checkBold;
                                //cp.FontName = "Truyện tranh Sans MS";
                                //cp.AllCaps = true;
                                cp.Italic = true;
                                document.EndUpdateCharacters(cp);
                            }
                            
                            //= string.Format("<div style='font-family:Times New Roman; font-size:9pt'><b><i>/ {0}</i></b></div>", text);


                            document.InsertSingleLineText(cell.Range.Start, text);

                            indexCell = cell.Index + 1;
                        }
                        else
                        {
                            document.InsertSingleLineText(cell.Range.Start, "");
                            cellTo = cell;
                            //table.MergeCells(row.Cells., table.LastRow.LastCell);
                        }
                        //if (f.Name == "[tong_b]" & item_check == 1) { document.InsertSingleLineText(cell.Range.Start, f.Name); item_check++; }
                        //else if (f.Name != "[tong_b]") document.InsertSingleLineText(cell.Range.Start, f.Name);

                        //if (cellFrom != null && cellTo != null)
                        //{
                        //    table.MergeCells(cellFrom, cellTo);
                        //}
                    }
                    catch { }
                }

                index += 1;
            }

            table.Rows.RemoveAt(index + 1);
            table.Rows.RemoveAt(index_old);

            return table;
        }
        public static Table TableHandle1<T>
    (
        Field field,
        Document document,
        List<T> result,
        int index,
        int numberRemove = 1
    )
        {
            var table = document.Tables[field.Index];
            //var index = 18;
            var index_old = index;
            var rField = table.Rows[index];

            var ltFieldName = (from cell in rField.Cells
                               let cellName = document.GetText(cell.Range).Replace(Environment.NewLine, "")
                               select new
                               {
                                   Index = cell.Index,
                                   Name = cellName
                               }).ToList(); //.Replace(" ", "")

            int item_check = 1;
            bool checkBold = false;

            foreach (var r in result)
            {
                var row = table.Rows.InsertBefore(index + 1);
                int indexCell = 0;
                TableCell cellFrom = null;
                foreach (var f in ltFieldName)
                {
                    TableCell cellTo = null;
                    try
                    {
                        if (r.GetType().GetProperty("NoInt") != null)
                        {
                            if (r.GetType().GetProperty("NoInt").GetValue(r, null).ToString() == "0")
                            {
                                checkBold = true;
                            }
                            else
                            {
                                checkBold = false;
                            }
                        }
                        //var cell = row[f.Index];
                        var cell = row[indexCell];
                        //var celltest = document.Tables.GetTableCell(document.CaretPosition);
                        var tx = r.GetType().GetProperty(f.Name.Trim().Replace("[", "").Replace("]", "")) != null
                            ? r.GetType().GetProperty(f.Name.Trim().Replace("[", "").Replace("]", "")).GetValue(r, null)
                            : null;
                        var text = f.Name;
                        if (tx != null)
                        {
                            cellFrom = cell;

                            text = tx.ToString();

                            if (f.Name.Trim() == "[Froms]"
                            || f.Name.Trim() == "[Tos]"
                            || f.Name.Trim() == "[DueDate]")
                            {
                                //text = string.Format("{0:d.M.yyyy}", tx);
                                text = string.Format("{0:d.M}", tx);
                            }
                            else if (f.Name.Trim() == "[PTax]" || f.Name.Trim() == "[Tax]")
                            {
                                if (text == "0,00")
                                {
                                    text = string.Format("{0:#,#.##; -#,#.##;}", tx);
                                }
                                else
                                    text = string.Format("{0:#,0.##; -#,0.##;}%", tx);
                            }
                            else
                            {
                                text = string.Format("{0:#,#.##; -#,#.##;}", tx);
                            }

                            if (r.GetType().GetProperty("NoInt").GetValue(r, null).ToString() == "2")
                            {
                                CharacterProperties cp = document.BeginUpdateCharacters(cell.Range);
                                cp.Bold = checkBold;
                                //cp.FontName = "Truyện tranh Sans MS";
                                //cp.AllCaps = true;
                                cp.Italic = true;
                                document.EndUpdateCharacters(cp);
                            }

                            if (r.GetType().GetProperty("NoInt").GetValue(r, null).ToString() == "3")
                            {
                                CharacterProperties cp = document.BeginUpdateCharacters(cell.Range);
                                cp.Bold = checkBold;
                                //cp.FontName = "Truyện tranh Sans MS";
                                //cp.AllCaps = true;
                                cp.Italic = false;
                                document.EndUpdateCharacters(cp);
                            }

                            //= string.Format("<div style='font-family:Times New Roman; font-size:9pt'><b><i>/ {0}</i></b></div>", text);



                            document.InsertSingleLineText(cell.Range.Start, text);

                            
                        }
                        else
                        {
                            document.InsertSingleLineText(cell.Range.Start, f.Name);
                            cellTo = cell;
                            //table.MergeCells(row.Cells., table.LastRow.LastCell);
                        }
                        indexCell = cell.Index + 1;
                        //if (f.Name == "[tong_b]" & item_check == 1) { document.InsertSingleLineText(cell.Range.Start, f.Name); item_check++; }
                        //else if (f.Name != "[tong_b]") document.InsertSingleLineText(cell.Range.Start, f.Name);

                        //if (cellFrom != null && cellTo != null)
                        //{
                        //    table.MergeCells(cellFrom, cellTo);
                        //}
                    }
                    catch { }
                }

                index += 1;
            }


            for (int i = 1; i <= numberRemove; i++)
            {
                table.Rows.RemoveAt(index + i);
            }
            table.Rows.RemoveAt(index_old);

            return table;
        }

        public static Table TableHandleMergeCell<T>
    (
        Field field,
        Document document,
        List<T> result,
        int index,
        int numberRemove = 1
    )
        {
            var table = document.Tables[field.Index];
            //var index = 18;
            var index_old = index;
            var rField = table.Rows[index];

            var ltFieldName = (from cell in rField.Cells
                               let cellName = document.GetText(cell.Range).Replace(Environment.NewLine, "")
                               select new
                               {
                                   Index = cell.Index,
                                   Name = cellName
                               }).ToList(); //.Replace(" ", "")

            int item_check = 1;
            bool checkBold = false;

            foreach (var r in result)
            {
                var row = table.Rows.InsertBefore(index + 1);
                int indexCell = 0;
                TableCell cellFrom = null;
                foreach (var f in ltFieldName)
                {
                    TableCell cellTo = null;
                    try
                    {
                        if (r.GetType().GetProperty("NoInt") != null)
                        {
                            if (r.GetType().GetProperty("NoInt").GetValue(r, null).ToString() == "0")
                            {
                                checkBold = true;
                            }
                            else
                            {
                                checkBold = false;
                            }
                        }
                        //var cell = row[f.Index];
                        var cell = row[indexCell];
                        //var celltest = document.Tables.GetTableCell(document.CaretPosition);
                        var tx = r.GetType().GetProperty(f.Name.Trim().Replace("[", "").Replace("]", "")) != null
                            ? r.GetType().GetProperty(f.Name.Trim().Replace("[", "").Replace("]", "")).GetValue(r, null)
                            : null;
                        var text = f.Name;
                        if (tx != null)
                        {
                            cellFrom = cell;

                            text = tx.ToString();

                            if (f.Name.Trim() == "[Froms]"
                            || f.Name.Trim() == "[Tos]"
                            || f.Name.Trim() == "[DueDate]")
                            {
                                //text = string.Format("{0:d.M.yyyy}", tx);
                                text = string.Format("{0:d.M}", tx);
                            }
                            else if (f.Name.Trim() == "[PTax]" || f.Name.Trim() == "[Tax]")
                            {
                                text = string.Format("{0:#,0.##; -#,0.##;}%", tx);
                            }
                            else
                            {
                                text = string.Format("{0:#,#.##; -#,#.##;}", tx);
                            }

                            if (r.GetType().GetProperty("NoInt").GetValue(r, null).ToString() == "2")
                            {
                                CharacterProperties cp = document.BeginUpdateCharacters(cell.Range);
                                cp.Bold = checkBold;
                                //cp.FontName = "Truyện tranh Sans MS";
                                //cp.AllCaps = true;
                                cp.Italic = true;
                                document.EndUpdateCharacters(cp);
                            }

                            if (r.GetType().GetProperty("NoInt").GetValue(r, null).ToString() == "3")
                            {
                                CharacterProperties cp = document.BeginUpdateCharacters(cell.Range);
                                cp.Bold = checkBold;
                                //cp.FontName = "Truyện tranh Sans MS";
                                //cp.AllCaps = true;
                                cp.Italic = false;
                                document.EndUpdateCharacters(cp);
                            }

                            //= string.Format("<div style='font-family:Times New Roman; font-size:9pt'><b><i>/ {0}</i></b></div>", text);



                            document.InsertSingleLineText(cell.Range.Start, text);


                        }
                        else
                        {
                            document.InsertSingleLineText(cell.Range.Start, f.Name);
                            cellTo = cell;
                            //table.MergeCells(row.Cells., table.LastRow.LastCell);
                        }
                        indexCell = cell.Index + 1;
                    }
                    catch { }
                }

                index += 1;
            }


            for (int i = 1; i <= numberRemove; i++)
            {
                table.Rows.RemoveAt(index + i);
            }
            table.Rows.RemoveAt(index_old);

            return table;
        }

        public static Table TableHandleWithMergeCell<T>
    (
        Field field,
        Document document,
        List<T> result,
        int index,
        int numberRemove = 1
    )
        {
            var table = document.Tables[field.Index];
            //var index = 18;
            var index_old = index;
            var rField = table.Rows[index];

            var ltFieldName = (from cell in rField.Cells
                               let cellName = document.GetText(cell.Range).Replace(Environment.NewLine, "")
                               select new
                               {
                                   Index = cell.Index,
                                   Name = cellName
                               }).ToList(); //.Replace(" ", "")

            int item_check = 1;
            bool checkBold = false;

            foreach (var r in result)
            {
                var row = table.Rows.InsertBefore(index + 1);
                int indexCell = 0;
                TableCell cellFrom = null;
                foreach (var f in ltFieldName)
                {
                    TableCell cellTo = null;
                    try
                    {
                        if (r.GetType().GetProperty("NoInt") != null)
                        {
                            if (r.GetType().GetProperty("NoInt").GetValue(r, null).ToString() == "0")
                            {
                                checkBold = true;
                            }
                            else
                            {
                                checkBold = false;
                            }
                        }
                        //var cell = row[f.Index];
                        var cell = row[indexCell];
                        //var celltest = document.Tables.GetTableCell(document.CaretPosition);
                        var tx = r.GetType().GetProperty(f.Name.Trim().Replace("[", "").Replace("]", "")) != null
                            ? r.GetType().GetProperty(f.Name.Trim().Replace("[", "").Replace("]", "")).GetValue(r, null)
                            : null;
                        var text = f.Name;
                        if (tx != null)
                        {
                            cellFrom = cell;

                            text = tx.ToString();

                            if (f.Name.Trim() == "[Froms]"
                            || f.Name.Trim() == "[Tos]"
                            || f.Name.Trim() == "[DueDate]")
                            {
                                //text = string.Format("{0:d.M.yyyy}", tx);
                                text = string.Format("{0:d.M}", tx);
                            }
                            else if (f.Name.Trim() == "[PTax]" || f.Name.Trim() == "[Tax]"
                                & r.GetType().GetProperty("No").GetValue(r, null).ToString() != "99"
                                & r.GetType().GetProperty("No").GetValue(r, null).ToString() != "98")
                            {
                                text = string.Format("{0:#,0.##; -#,0.##;}%", tx);
                            }
                            else
                            {
                                text = string.Format("{0:#,#.##; -#,#.##;}", tx);
                            }

                            if (r.GetType().GetProperty("NoInt").GetValue(r, null).ToString() == "2")
                            {
                                CharacterProperties cp = document.BeginUpdateCharacters(cell.Range);
                                cp.Bold = checkBold;
                                //cp.FontName = "Truyện tranh Sans MS";
                                //cp.AllCaps = true;
                                cp.Italic = true;
                                document.EndUpdateCharacters(cp);
                            }

                            if (r.GetType().GetProperty("NoInt").GetValue(r, null).ToString() == "3")
                            {
                                CharacterProperties cp = document.BeginUpdateCharacters(cell.Range);
                                cp.Bold = checkBold;
                                //cp.FontName = "Truyện tranh Sans MS";
                                //cp.AllCaps = true;
                                cp.Italic = false;
                                document.EndUpdateCharacters(cp);
                            }

                            if (r.GetType().GetProperty("NoInt").GetValue(r, null).ToString() == "0")
                            {
                                CharacterProperties cp = document.BeginUpdateCharacters(cell.Range);
                                cp.Bold = true;
                                //cp.FontName = "Truyện tranh Sans MS";
                                //cp.AllCaps = true;
                                cp.Italic = false;
                                document.EndUpdateCharacters(cp);
                            }

                            //= string.Format("<div style='font-family:Times New Roman; font-size:9pt'><b><i>/ {0}</i></b></div>", text);



                            document.InsertSingleLineText(cell.Range.Start, text);

                            indexCell = cell.Index + 1;
                        }
                        else
                        {
                            document.InsertSingleLineText(cell.Range.Start, "");
                            cellTo = cell;
                            //table.MergeCells(row.Cells., table.LastRow.LastCell);
                        }
                        //if (f.Name == "[tong_b]" & item_check == 1) { document.InsertSingleLineText(cell.Range.Start, f.Name); item_check++; }
                        //else if (f.Name != "[tong_b]") document.InsertSingleLineText(cell.Range.Start, f.Name);

                        if (cellFrom != null && cellTo != null)
                        {
                            table.MergeCells(cellFrom, cellTo);
                        }

                    }
                    catch { }
                }

                index += 1;
            }


            for (int i = 1; i <= numberRemove; i++)
            {
                table.Rows.RemoveAt(index + i);
            }
            table.Rows.RemoveAt(index_old);

            return table;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="towerId"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="customerId"></param>
        /// <param name="ctlRtf"></param>
        /// <param name="oldDept">NỢ CŨ</param>
        /// <param name="field"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public static string DeptMeLing
            (
                byte towerId,
                int month,
                int year,
                int customerId,
                DevExpress.XtraRichEdit.RichEditControl ctlRtf,
                decimal oldDept,
                Field field,
                Document document
            )
        {

            #region 

            var objData = Library.Class.Connect.QueryConnect.QueryData<DeptMeLing>("DeptMeLing",
                new
                {
                    CustomerId = customerId,
                    Date = new DateTime(year, month, 1)
                });

            var objDataCurrent = objData.Where(_ => _.Type == 1);
            oldDept = objData.Where(_ => _.Type == 2).Sum(_=>_.PAT).GetValueOrDefault();

            TableHandle<DeptMeLing>(field, document, objDataCurrent.ToList(), 10);

            long pat = Convert.ToInt64(Math.Round((decimal)objDataCurrent.Sum(_ => _.PAT).GetValueOrDefault(), 0));

            ctlRtf.Document.ReplaceAll("[SumPAT]", string.Format("{0:#,0}", pat), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[Unpaid]", string.Format("{0:#,0}", oldDept), SearchOptions.None);

            long amount = Convert.ToInt64(Math.Round((decimal)pat + oldDept, 0));

            ctlRtf.Document.ReplaceAll("[Amount]", string.Format("{0:#,0}", amount), SearchOptions.None);

            var amountElectricText = new TienTeCls().DocTienBangChu(amount);
            ctlRtf.Document.ReplaceAll("[AmountText]", amountElectricText, SearchOptions.None);

            #endregion

            return ctlRtf.Document.RtfText;
        }

        public static string GetDebitNote1(int CustomerId, int month, int year, DevExpress.XtraRichEdit.RichEditControl ctlRtf, Field field, Document document, int? ApartmentId)
        {

            #region

            var objData = Library.Class.Connect.QueryConnect.QueryData<DebitNote>("cnGetDebitNote1",
                new
                {
                    CustomerId = CustomerId,
                    Month = month,
                    Year = year,
                    ApartmentId = ApartmentId
                });

            var objDataTotal = objData.Where(_ => _.No == "99").ToList();

            TableHandle1<DebitNote>(field, document, objDataTotal, 14);
            TableHandle1<DebitNote>(field, document, objData.Where(_ => _.No == "4").ToList(), 12);
            //TableHandle1<DebitNote>(field, document, objData.Where(_ => _.No == "6").ToList(), 10);
            //TableHandle1<DebitNote>(field, document, objData.Where(_ => _.No == "5").ToList(), 8);
            //TableHandle1<DebitNote>(field, document, objData.Where(_ => _.No == "3").ToList(), 6);
            
            var objNuoc =  objData.Where(_ => _.No == "2").ToList();
            TableHandle1<DebitNote>(field, document,objNuoc, 4);

            TableHandle1<DebitNote>(field, document, objData.Where(_ => _.No == "1").ToList(), 2);

            // Đổ dữ liệu tay

            if (objNuoc.Count() == 0)
            {
                TableHandle1<DebitNote>(field, document, objNuoc, 4, 11);
            }
            else
            {
                var objDm1 = objData.Where(_ => _.No == "3" & _.Frst == 1).FirstOrDefault();
                ctlRtf.Document.ReplaceAll("[Qun1]", string.Format("{0:#,0}", objDm1!= null ? objDm1.Qun : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Prc1]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Prc : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Amount1]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Amount : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tam1]", string.Format("{0:#,0}", objDm1 != null ? objDm1.TAm : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tamt1]", string.Format("{0:#,0}", objDm1 != null ? objDm1.TAmt : 0), SearchOptions.None);

                objDm1 = objData.Where(_ => _.No == "3" & _.Frst == 2).FirstOrDefault();
                ctlRtf.Document.ReplaceAll("[Qun2]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Qun : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Prc2]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Prc : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Amount2]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Amount : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tam2]", string.Format("{0:#,0}", objDm1 != null ? objDm1.TAm : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tamt2]", string.Format("{0:#,0}", objDm1 != null ? objDm1.TAmt : 0), SearchOptions.None);

                objDm1 = objData.Where(_ => _.No == "3" & _.Frst == 3).FirstOrDefault();
                ctlRtf.Document.ReplaceAll("[Qun3]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Qun : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Prc3]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Prc : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Amount3]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Amount : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tam3]", string.Format("{0:#,0}", objDm1 != null ? objDm1.TAm : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tamt3]", string.Format("{0:#,0}", objDm1 != null ? objDm1.TAmt : 0), SearchOptions.None);

                objDm1 = objData.Where(_ => _.No == "3" & _.Frst == 4).FirstOrDefault();
                ctlRtf.Document.ReplaceAll("[Qun4]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Qun : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Prc4]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Prc : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Amount4]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Amount : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tam4]", string.Format("{0:#,0}", objDm1 != null ? objDm1.TAm : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tamt4]", string.Format("{0:#,0}", objDm1 != null ? objDm1.TAmt : 0), SearchOptions.None);

                objDm1 = objData.Where(_ => _.No == "5").FirstOrDefault();
                ctlRtf.Document.ReplaceAll("[Qun5]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Qun : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Prc5]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Prc : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Amount5]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Amount : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tam5]", string.Format("{0:#,0}", objDm1 != null ? objDm1.TAm : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tamt5]", string.Format("{0:#,0}", objDm1 != null ? objDm1.TAmt : 0), SearchOptions.None);

                objDm1 = objData.Where(_ => _.No == "6").FirstOrDefault();
                ctlRtf.Document.ReplaceAll("[Qun6]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Qun : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Prc6]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Prc : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Amount6]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Amount : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tam6]", string.Format("{0:#,0}", objDm1 != null ? objDm1.TAm : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tamt6]", string.Format("{0:#,0}", objDm1 != null ? objDm1.TAmt : 0), SearchOptions.None);

                objDm1 = objData.Where(_ => _.No == "4" || _.No == "99").FirstOrDefault();
                ctlRtf.Document.ReplaceAll("[Qun7]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Qun : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Prc7]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Prc : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Amount7]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Amount : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tam7]", string.Format("{0:#,0}", objDm1 != null ? objDm1.TAm : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tamt7]", string.Format("{0:#,0}", objDm1 != null ? objDm1.TAmt : 0), SearchOptions.None);
            }

            if (objDataTotal.Count() > 0)
            {
                var first = objData.First();
                ctlRtf.Document.ReplaceAll("[MonthlyFee]", string.Format("{0:#,0}", first.TAmt), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Pay]", string.Format("{0:#,0}", first.TAmt), SearchOptions.None);

                ctlRtf.Document.ReplaceAll("[OldDept]", string.Format("{0:#,0}", first.TAmt), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Paid]", string.Format("{0:#,0}", first.TAmt), SearchOptions.None);

                long amount = Convert.ToInt64(Math.Round((decimal)(first.TAmt), 0));
                var amountElectricText = new TienTeCls().DocTienBangChu(amount);
                var _tientienganh = new TienTeCls().DocTienBangChuEN(amount, "VND");
                ctlRtf.Document.ReplaceAll("[PayText]", amountElectricText, SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[PayEngText]", _tientienganh, SearchOptions.None);
            }

            #endregion

            return ctlRtf.Document.RtfText;
        }
        public static string GetDebitNoteETP(int CustomerId, int month, int year, DevExpress.XtraRichEdit.RichEditControl ctlRtf, Field field, Document document, int? ApartmentId)
        {

            #region

            var objData = Library.Class.Connect.QueryConnect.QueryData<DebitNote>("cnGetDebitNoteETP",
                new
                {
                    CustomerId = CustomerId,
                    Month = month,
                    Year = year,
                    ApartmentId = ApartmentId
                });

            var objDataTotal = objData.Where(_ => _.No == "99").ToList();

            TableHandle1<DebitNote>(field, document, objDataTotal, 20);
            //TableHandle1<DebitNote>(field, document, objData.Where(_ => _.No == "4").ToList(), 5);
            //TableHandle1<DebitNote>(field, document, objData.Where(_ => _.No == "6").ToList(), 10);
            //TableHandle1<DebitNote>(field, document, objData.Where(_ => _.No == "5").ToList(), 8);
            //TableHandle1<DebitNote>(field, document, objData.Where(_ => _.No == "3").ToList(), 6);
            var objPhiQuanLy = objData.Where(_ => _.No == "1").ToList();
            TableHandle1<DebitNote>(field, document, objPhiQuanLy, 2);

            var objNuoc = objData.Where(_ => _.No == "2").ToList();
            TableHandle1<DebitNote>(field, document, objNuoc, 3);

            TableHandle1<DebitNote>(field, document, objData.Where(_ => _.No == "4").ToList(), 7);

            var objDien = objData.Where(_ => _.No == "10").ToList();
            TableHandle1<DebitNote>(field, document, objDien, 8);

            //TableHandle1<DebitNote>(field, document, objData.Where(_ => _.No == "1").ToList(), 2);

            // Đổ dữ liệu tay

            if (objNuoc.Count() == 0)
            {
                //TableHandle1<DebitNote>(field, document, objNuoc, 4, 11);
            }
            else
            {
                var objDm1 = objData.Where(_ => _.No == "3").FirstOrDefault();
                ctlRtf.Document.ReplaceAll("[Fr1]", objNuoc.FirstOrDefault() != null ? objNuoc.FirstOrDefault().Fr : null, SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[To1]", objNuoc.FirstOrDefault() != null ? objNuoc.FirstOrDefault().To : null, SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Frst1]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Frst : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Lst1]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Lst : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Qun1]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Qun : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Prc1]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Prc : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Amount1]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Amount : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tam1]", string.Format("{0:#,0}", objDm1 != null ? objDm1.TAm : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tamt1]", string.Format("{0:#,0}", objDm1 != null ? objDm1.TAmt : 0), SearchOptions.None);
                objDm1 = objData.Where(_ => _.No == "5").FirstOrDefault();
                ctlRtf.Document.ReplaceAll("[Fr2]", objNuoc.FirstOrDefault() != null ? objNuoc.FirstOrDefault().Fr : null, SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[To2]", objNuoc.FirstOrDefault() != null ? objNuoc.FirstOrDefault().To : null, SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Frst2]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Frst : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Lst2]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Lst : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Qun2]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Qun : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Prc2]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Prc : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Amount2]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Amount : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tam2]", string.Format("{0:#,0}", objDm1 != null ? objDm1.TAm : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tamt2]", string.Format("{0:#,0}", objDm1 != null ? objDm1.TAmt : 0), SearchOptions.None);

                objDm1 = objData.Where(_ => _.No == "6").FirstOrDefault();
                ctlRtf.Document.ReplaceAll("[Fr3]", objNuoc.FirstOrDefault() != null ? objNuoc.FirstOrDefault().Fr : null, SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[To3]", objNuoc.FirstOrDefault() != null ? objNuoc.FirstOrDefault().To : null, SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Frst3]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Frst : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Lst3]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Lst : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Qun3]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Qun : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Prc3]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Prc : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Amount3]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Amount : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tam3]", string.Format("{0:#,0}", objDm1 != null ? objDm1.TAm : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tamt3]", string.Format("{0:#,0}", objDm1 != null ? objDm1.TAmt : 0), SearchOptions.None);

                objDm1 = objData.Where(_ => _.No == "4" || _.No == "99").FirstOrDefault();
                ctlRtf.Document.ReplaceAll("[Fr4]", objDm1 != null ? objDm1.Fr : null, SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[To4]", objDm1 != null ? objDm1.To : null, SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Frst4]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Frst : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Lst4]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Lst : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Qun4]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Qun : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Prc4]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Prc : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Amount4]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Amount : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tam4]", string.Format("{0:#,0}", objDm1 != null ? objDm1.TAm : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tamt4]", string.Format("{0:#,0}", objDm1 != null ? objDm1.TAmt : 0), SearchOptions.None);
                var j = 5;
                for (int i = 11; i < 18; i++)
                {
                    objDm1 = objData.Where(_ => _.No == i.ToString()).FirstOrDefault();
                    ctlRtf.Document.ReplaceAll("[Fr" + j + "]", objDm1 != null ? objDm1.Fr : null, SearchOptions.None);
                    ctlRtf.Document.ReplaceAll("[To" + j + "]", objDm1 != null ? objDm1.To : null, SearchOptions.None);
                    ctlRtf.Document.ReplaceAll("[Frst" + j + "]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Frst : 0), SearchOptions.None);
                    ctlRtf.Document.ReplaceAll("[Lst" + j + "]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Lst : 0), SearchOptions.None);
                    ctlRtf.Document.ReplaceAll("[Qun" + j + "]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Qun : 0), SearchOptions.None);
                    ctlRtf.Document.ReplaceAll("[Prc" + j + "]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Prc : 0), SearchOptions.None);
                    ctlRtf.Document.ReplaceAll("[Amount" + j + "]", string.Format("{0:#,0}", objDm1 != null ? objDm1.Amount : 0), SearchOptions.None);
                    ctlRtf.Document.ReplaceAll("[Tam" + j + "]", string.Format("{0:#,0}", objDm1 != null ? objDm1.TAm : 0), SearchOptions.None);
                    ctlRtf.Document.ReplaceAll("[Tamt" + j + "]", string.Format("{0:#,0}", objDm1 != null ? objDm1.TAmt : 0), SearchOptions.None);
                    j++;
                }
               


            }

            if (objDataTotal.Count() > 0)
            {
                var first = objData.First();
                ctlRtf.Document.ReplaceAll("[MonthlyFee]", string.Format("{0:#,0}", first.TAmt), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Pay]", string.Format("{0:#,0}", first.TAmt), SearchOptions.None);

                ctlRtf.Document.ReplaceAll("[OldDept]", string.Format("{0:#,0}", first.TAmt), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Paid]", string.Format("{0:#,0}", first.TAmt), SearchOptions.None);

                long amount = Convert.ToInt64(Math.Round((decimal)(first.TAmt), 0));
                var amountElectricText = new TienTeCls().DocTienBangChu(amount);
                var _tientienganh = new TienTeCls().DocTienBangChuEN(amount, "VND");
                ctlRtf.Document.ReplaceAll("[PayText]", amountElectricText, SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[PayEngText]", _tientienganh, SearchOptions.None);
            }

            #endregion

            return ctlRtf.Document.RtfText;
        }
        public static string GetDebitNoteDVHD(int CustomerId, int month, int year, DevExpress.XtraRichEdit.RichEditControl ctlRtf, Field field, Document document, int? ApartmentId)
        {

            #region

            var objData = Library.Class.Connect.QueryConnect.QueryData<DebitNoteWithTyGia>("cnGetDebitNoteDVHD",
                new
                {
                    CustomerId = CustomerId,
                    Month = month,
                    Year = year,
                    ApartmentId = ApartmentId
                });

            var objNuoc = objData.Where(_=>_.No != "99").ToList();
            TableHandle1(field, document, objNuoc, 2);

            var total = objData.Where(_ => _.No == "99").FirstOrDefault();

            if(total != null)
            {
                ctlRtf.Document.ReplaceAll("[Amount]", string.Format("{0:#,0}", total != null ? total.Amount : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tam]", string.Format("{0:#,0}", total != null ? total.TAm : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tamt]", string.Format("{0:#,0}", total != null ? total.TAmt : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[TyGiaNganHang]", string.Format("{0:#,0}", total != null ? total.TyGiaNganHang : 0), SearchOptions.None);
            }
            // Tháng hiện tại
            var currentMonth = new DateTime(year, month, 1);
            // Cuối tháng trước
            var endLastMonth = currentMonth.AddDays(-1);
            // Đầu tháng trước nữa
            var earlyLastMonth = currentMonth.AddMonths(-2);
            // Cuối tháng này
            var endEarlyLastMonth = currentMonth.AddMonths(1).AddDays(-1);

            var deptValue = Library.Class.Connect.QueryConnect.QueryData<Model.DeptCustomerEp>("cnGetDebitNoteHD",
                    new
                    {
                        CustomerId = CustomerId,
                        Month = month,
                        Year = year,
                        TuNgay = currentMonth,
                        DenNgay = endEarlyLastMonth
                    });

            if(deptValue.Count() > 0)
            {
                var first = deptValue.First();
                ctlRtf.Document.ReplaceAll("[NoCu]", string.Format("{0:#,0}", first.DauKy), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[TienLaiChuaThu]", string.Format("{0:#,0}", first.SoTienLai), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[PhatSinh]", string.Format("{0:#,0}", first.PhatSinh), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[ConNo]", string.Format("{0:#,0}", first.ConNo), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[TienCocChuaThu]", string.Format("{0:#,0}", first.TienCocChuaThu), SearchOptions.None);

                long amount = Convert.ToInt64(Math.Round((decimal)(first.ConNo), 0));
                var amountElectricText = new TienTeCls().DocTienBangChu(amount);
                var _tientienganh = new TienTeCls().DocTienBangChuEN(amount, "VND");
                ctlRtf.Document.ReplaceAll("[ConNoText]", amountElectricText, SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[ConNoEngText]", _tientienganh, SearchOptions.None);
            }

            //if (objNuoc.Count() > 0)
            //{
            //    var first = objData.First();
            //    ctlRtf.Document.ReplaceAll("[MonthlyFee]", string.Format("{0:#,0}", first.TAmt), SearchOptions.None);
            //    ctlRtf.Document.ReplaceAll("[Pay]", string.Format("{0:#,0}", first.TAmt), SearchOptions.None);

            //    ctlRtf.Document.ReplaceAll("[OldDept]", string.Format("{0:#,0}", first.TAmt), SearchOptions.None);
            //    ctlRtf.Document.ReplaceAll("[Paid]", string.Format("{0:#,0}", first.TAmt), SearchOptions.None);

            //    long amount = Convert.ToInt64(Math.Round((decimal)(first.TAmt), 0));
            //    var amountElectricText = new TienTeCls().DocTienBangChu(amount);
            //    var _tientienganh = new TienTeCls().DocTienBangChuEN(amount, "VND");
            //    ctlRtf.Document.ReplaceAll("[PayText]", amountElectricText, SearchOptions.None);
            //    ctlRtf.Document.ReplaceAll("[PayEngText]", _tientienganh, SearchOptions.None);
            //}

            #endregion

            return ctlRtf.Document.RtfText;
        }

        public static string GetDebitNoteDNHD(int CustomerId, int month, int year, DevExpress.XtraRichEdit.RichEditControl ctlRtf, Field field, Document document, int? ApartmentId)
        {


            #region
              
            var objData = Library.Class.Connect.QueryConnect.QueryData<DebitNoteWithTyGia>("cnGetDebitNoteDNHD",
                new
                {
                    CustomerId = CustomerId,
                    Month = month,
                    Year = year,
                    ApartmentId = ApartmentId
                });

            var objNuoc = objData.Where(_ => _.No != "99").ToList();
            TableHandle1(field, document, objNuoc, 2);

            var total = objData.Where(_ => _.No == "99").FirstOrDefault();

            if (total != null)
            {
                ctlRtf.Document.ReplaceAll("[Amount]", string.Format("{0:#,0}", total != null ? total.Amount : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tam]", string.Format("{0:#,0}", total != null ? total.TAm : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[Tamt]", string.Format("{0:#,0}", total != null ? total.TAmt : 0), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[TyGiaNganHang]", string.Format("{0:#,0}", total != null ? total.TyGiaNganHang : 0), SearchOptions.None);
            }
            // Tháng hiện tại
            var currentMonth = new DateTime(year, month, 1);
            // Cuối tháng trước
            var endLastMonth = currentMonth.AddDays(-1);
            // Đầu tháng trước nữa
            var earlyLastMonth = currentMonth.AddMonths(-2);
            // Cuối tháng này
            var endEarlyLastMonth = currentMonth.AddMonths(1).AddDays(-1);

            var deptValue = Library.Class.Connect.QueryConnect.QueryData<Model.DeptCustomerEp>("cnGetDebitNoteDienNuocHD",
                    new
                    {
                        CustomerId = CustomerId,
                        Month = month,
                        Year = year,
                        TuNgay = currentMonth,
                        DenNgay = endEarlyLastMonth
                    });

            if (deptValue.Count() > 0)
            {
                var first = deptValue.First();
                ctlRtf.Document.ReplaceAll("[NoCu]", string.Format("{0:#,0}", first.DauKy), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[TienLaiChuaThu]", string.Format("{0:#,0}", first.SoTienLai), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[PhatSinh]", string.Format("{0:#,0}", first.PhatSinh), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[ConNo]", string.Format("{0:#,0}", first.ConNo), SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[TienCocChuaThu]", string.Format("{0:#,0}", first.TienCocChuaThu), SearchOptions.None);

                long amount = Convert.ToInt64(Math.Round((decimal)(first.ConNo), 0));
                var amountElectricText = new TienTeCls().DocTienBangChu(amount);
                var _tientienganh = new TienTeCls().DocTienBangChuEN(amount, "VND");
                ctlRtf.Document.ReplaceAll("[ConNoText]", amountElectricText, SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[ConNoEngText]", _tientienganh, SearchOptions.None);
            }

            //if (objNuoc.Count() > 0)
            //{
            //    var first = objData.First();
            //    ctlRtf.Document.ReplaceAll("[MonthlyFee]", string.Format("{0:#,0}", first.TAmt), SearchOptions.None);
            //    ctlRtf.Document.ReplaceAll("[Pay]", string.Format("{0:#,0}", first.TAmt), SearchOptions.None);

            //    ctlRtf.Document.ReplaceAll("[OldDept]", string.Format("{0:#,0}", first.TAmt), SearchOptions.None);
            //    ctlRtf.Document.ReplaceAll("[Paid]", string.Format("{0:#,0}", first.TAmt), SearchOptions.None);

            //    long amount = Convert.ToInt64(Math.Round((decimal)(first.TAmt), 0));
            //    var amountElectricText = new TienTeCls().DocTienBangChu(amount);
            //    var _tientienganh = new TienTeCls().DocTienBangChuEN(amount, "VND");
            //    ctlRtf.Document.ReplaceAll("[PayText]", amountElectricText, SearchOptions.None);
            //    ctlRtf.Document.ReplaceAll("[PayEngText]", _tientienganh, SearchOptions.None);
            //}

            #endregion

            return ctlRtf.Document.RtfText;
        }

        public static string StopProvidingService(int CustomerId, int month, int year, DevExpress.XtraRichEdit.RichEditControl ctlRtf, Field field, Document document)
        {


            var objData = Library.Class.Connect.QueryConnect.QueryData<StopProvidingServiceModel>("deptStopProvidingService",
                new
                {
                    CustomerId = CustomerId
                });

            TableHandleWithMergeCell(field, document, objData.ToList(),1);

            return ctlRtf.Document.RtfText;
        }
        public static string ThongBaoNuoc(int CustomerId, int month, int year, DevExpress.XtraRichEdit.RichEditControl ctlRtf, Field field, Document document)
        {
            MasterDataContext _db = new MasterDataContext();
            DateTime dtResult = new DateTime(year, month, 1);
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            var objTien = new TienTeCls();
            var hoaDon = _db.dvHoaDons.Where(x => x.MaKH == CustomerId & SqlMethods.DateDiffDay(x.NgayTT, dtResult) >= 0 & x.IsDuyet == true & x.ConNo != 0 & x.TableName == "dvNuoc").FirstOrDefault() != null ? _db.dvHoaDons.Where(x => x.MaKH == CustomerId & SqlMethods.DateDiffDay(x.NgayTT, dtResult) >= 0 & x.IsDuyet == true & x.ConNo != 0 & x.TableName == "dvNuoc").OrderByDescending(x => x.NgayTT).FirstOrDefault().LinkID : -1;
            #region Thông tin dữ liệu
            var obj = (from n in _db.dvNuocs
                       join b in _db.mbMatBangs on n.MaMB equals b.MaMB
                       join t in _db.mbTangLaus on b.MaTL equals t.MaTL
                       join kn in _db.mbKhoiNhas on t.MaKN equals kn.MaKN
                       join k in _db.tnKhachHangs on n.MaKH equals k.MaKH
                       join lmb in _db.mbLoaiMatBangs on b.MaLMB equals lmb.MaLMB
                       join dh in _db.dvNuocDongHos on n.MaDH equals dh.ID into tblDongHo
                       from dh in tblDongHo.DefaultIfEmpty()
                       join nvn in _db.tnNhanViens on n.MaNVN equals nvn.MaNV
                       from nvs in _db.tnNhanViens.Where(nvs => nvs.MaNV == n.MaNVS).DefaultIfEmpty()
                       where n.ID == hoaDon
                       select new
                       {
                           n.ID,
                           kn.TenKN,
                           n.MaMB,
                           k.MaPhu,
                           IsConfirm = 0,
                           b.MaSoMB,
                           KhachHang = k.IsCaNhan == true ? k.HoKH + " " + k.TenKH : k.CtyTen,
                           dh.SoDH,
                           n.ChiSoCu,
                           n.ChiSoMoi,
                           n.HeSo,
                           SoTieuThu = n.SoTieuThu.GetValueOrDefault() + n.SoTieuThuDHCu.GetValueOrDefault(),
                           n.ThanhTien,
                           n.TyLeVAT,
                           n.TienVAT,
                           DonGia = Math.Round(n.ThanhTien.Value / n.SoTieuThu.Value, 2, MidpointRounding.AwayFromZero),
                           n.TyLeBVMT,
                           n.TienBVMT,
                           n.TienTT,
                           n.SoNguoiUD,
                           SoM3UDNguoi = n.SoM3UD1Nguoi,
                           n.SoM3UD,
                           n.DienGiai,
                           t.TenTL,
                           n.TuNgay,
                           n.DenNgay,
                           n.NgayNhap,
                           NguoiNhap = nvn.HoTenNV,
                           n.NgaySua,
                           SoTien_BangChu = objTien.DocTienBangChu(n.TienTT.Value, "đồng chẵn"),
                           NguoiSua = nvs.HoTenNV,
                           ChenhLech = n.SoTieuThu -
                                (_db.dvNuocs.Where(_ => _.MaMB == n.MaMB & SqlMethods.DateDiffDay(_.DenNgay, n.DenNgay) > 0)
                                     .OrderByDescending(_ => _.DenNgay).First() != null
                                    ? (int)_db.dvNuocs
                                        .Where(_ => _.MaMB == n.MaMB & SqlMethods.DateDiffDay(_.DenNgay, n.DenNgay) > 0)
                                        .OrderByDescending(_ => _.DenNgay).First().SoTieuThu
                                    : 0),
                           SoTieuThuDHCu =
                        (_db.dvNuocs.Where(_ => _.MaMB == n.MaMB & SqlMethods.DateDiffDay(_.DenNgay, n.DenNgay) > 0)
                             .OrderByDescending(_ => _.DenNgay).First() != null
                            ? (int)_db.dvNuocs
                                .Where(_ => _.MaMB == n.MaMB & SqlMethods.DateDiffDay(_.DenNgay, n.DenNgay) > 0)
                                .OrderByDescending(_ => _.DenNgay).First().SoTieuThu
                            : 0),
                           //n.SoTieuThuDHCu,
                           n.NgayTT,
                           n.LinkUrl,
                           lmb.TenLMB
                       }).FirstOrDefault();
            if (obj == null)
            {
                return RtfThongBaoDienNuoc(ctlRtf.Document.RtfText, obj.KhachHang, obj.TuNgay, "407A", "", 0, 0, 0, 0, 0, 0, 0, 0,
               0, 0, 0, "", 0, 0, 0, "", "");
            }
            #endregion

            return RtfThongBaoDienNuoc(ctlRtf.Document.RtfText, obj.KhachHang, obj.TuNgay, "407A", obj.SoDH, obj.ChiSoCu, obj.ChiSoMoi, obj.SoTieuThu, obj.DonGia, obj.ThanhTien, obj.ThanhTien, obj.TienVAT, obj.TienTT,
                0, obj.TienTT, obj.TyLeVAT, obj.SoTien_BangChu, obj.DonGia, obj.TyLeBVMT, obj.SoTieuThu);
        }
        public static string ThongBaoDien(int CustomerId, int month, int year, DevExpress.XtraRichEdit.RichEditControl ctlRtf, Field field, Document document)
        {
            MasterDataContext db = new MasterDataContext();
            DateTime dtResult = new DateTime(year, month, 1);
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            var objTien = new TienTeCls();
            var hoaDon = db.dvHoaDons.Where(x => x.MaKH == CustomerId & SqlMethods.DateDiffDay(x.NgayTT, dtResult) >= 0 & x.IsDuyet == true & x.ConNo != 0 & x.TableName == "dvDien3Pha").FirstOrDefault() != null ? db.dvHoaDons.Where(x => x.MaKH == CustomerId & SqlMethods.DateDiffDay(x.NgayTT, dtResult) >= 0 & x.IsDuyet == true & x.ConNo != 0 & x.TableName == "dvDien3Pha").OrderByDescending(x => x.NgayTT).FirstOrDefault().LinkID : -1 ;

            #region Thông tin dữ liệu
            var obj = (from n in db.dvDien3Phas
                       join b in db.mbMatBangs on n.MaMB equals b.MaMB
                       join t in db.mbTangLaus on b.MaTL equals t.MaTL
                       join kn in db.mbKhoiNhas on t.MaKN equals kn.MaKN
                       join k in db.tnKhachHangs on n.MaKH equals k.MaKH
                       join nvn in db.tnNhanViens on n.MaNVN equals nvn.MaNV
                       join nvs in db.tnNhanViens on n.MaNVS equals nvs.MaNV into tblNguoiSua
                       from nvs in tblNguoiSua.DefaultIfEmpty()
                       join dh in db.dvDien3PhaDongHos on n.MaDH equals dh.ID into dongho
                       from dh in dongho.DefaultIfEmpty()
                       where n.ID == hoaDon
                       select new
                       {
                           n.ID,
                           kn.TenKN,
                           n.MaMB,
                           IsConfirm = 0,
                           b.MaSoMB,
                           KhachHang = k.IsCaNhan == true ? k.HoKH + " " + k.TenKH : k.CtyTen,
                           n.SoTieuThu,
                           ChenhLech = db.funcChenhLechDien3Pha(n.MaMB, n.SoTieuThu, dtResult),
                           n.ThanhTien,
                           n.TyLeVAT,
                           n.TienVAT,
                           n.TienTT,
                           n.DienGiai,
                           t.TenTL,
                           n.TuNgay,
                           n.DenNgay,
                           n.NgayTT,
                           n.NgayNhap,
                           NguoiNhap = nvn.HoTenNV,
                           n.NgaySua,
                           NguoiSua = nvs.HoTenNV,
                           SoDongHo = dh.SoDH,
                           n.HeSo,
                           SoTienBangChu = objTien.DocTienBangChu(n.TienTT.GetValueOrDefault(), "đồng chẵn"),
                           TuNgayString = string.Format("Từ {0:dd}/{0:MM}/{0:yyyy} đến {1:dd}/{1:MM}/{1:yyyy}", n.TuNgay.Value.AddMonths(-1), n.TuNgay.Value.AddDays(-1)),
                           DenNgayString = string.Format("Từ {0:dd}/{0:MM}/{0:yyyy} đến {1:dd}/{1:MM}/{1:yyyy}", n.TuNgay.Value, n.DenNgay.Value)
                       }).FirstOrDefault();
            var objCT = (from nc in db.dvDien3PhaChiTiets
                         join dm in db.dvDien3PhaDinhMucs on nc.MaDM equals dm.ID
                         where nc.MaDien == hoaDon
                         orderby dm.STT
                         select new
                         {
                             dm.TenDM,
                             nc.ChiSoCu,
                             nc.ChiSoMoi,
                             nc.SoLuong,
                             nc.DonGia,
                             nc.ThanhTien,
                             nc.DienGiai
                         }).ToList();
            if (obj == null)
            {
                return RtfThongBaoDienNuoc(ctlRtf.Document.RtfText, obj.KhachHang, obj.TuNgay, "407A", "", 0, 0, 0, 0, 0, 0,0, 0,
               0,0, 0,"", 0, 0, 0,"", "");
            }

            #endregion
            return RtfThongBaoDienNuoc(ctlRtf.Document.RtfText, obj.KhachHang, obj.TuNgay, "407A", obj.SoDongHo, objCT.Sum(x=> x.ChiSoCu), objCT.Sum(x=> x.ChiSoMoi), objCT.Sum(x => x.SoLuong), (obj.ThanhTien / objCT.Sum(x=> x.SoLuong)), obj.ThanhTien, obj.ThanhTien, obj.TienVAT, obj.TienTT,
                0, obj.TienTT, obj.TyLeVAT, obj.SoTienBangChu, (obj.ThanhTien / objCT.Sum(x => x.SoLuong)), 0, (decimal)objCT.Sum(x=> x.SoLuong), obj.TuNgayString, obj.DenNgayString);
        }
        private static string RtfThongBaoDienNuoc(string rtfText, string khachHang, DateTime? ngayThongBao, string gianHangCT, string maCongToCT, decimal? CSDK, decimal? CSCC, decimal? chiSoDung, decimal? donGiaMoi, decimal? thanhTienMaCongTo, decimal? thanhTienTruocVAT, decimal? thanhTienVAT,
           decimal? thanhTienSauVAT, decimal? noKyTruocChuaThanhToan, decimal? tongThanhToan, decimal? VAT, string soTienBangChu, decimal? donGiaCu = 0, decimal? tyLe = 0, decimal chiSoDungThucTe = 0, string ngayCuTu = null, string ngayMoiTu = null)
        {
            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };
            //ctlRtf.Document.ReplaceAll("[TieuDePhieu]", maTknh == null ? "PHIẾU CHI" : "PHIẾU CHI TIỀN CHUYỂN KHOẢN", SearchOptions.None);
            //ctlRtf.Document.ReplaceAll("[TenTN]", tenTn, SearchOptions.None); //"Ban Quản lý Tòa nhà: " + 
            ctlRtf.Document.ReplaceAll("[KhachHang]", khachHang, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NgayHomNay]", string.Format("ngày {0:dd} tháng {0:MM} năm {0:yyyy}", DateTime.Now), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NgayThongBao]", string.Format("tháng {0:MM} năm {0:yyyy}", ngayThongBao), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NgayCu]", string.Format("Đơn giá cũ ({0})", ngayCuTu), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NgayMoi]", string.Format("Đơn giá mới ({0})", ngayMoiTu), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[GHCT]", gianHangCT, SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[MaCTCT]", maCongToCT != null ? maCongToCT : "", SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[CSĐK]", string.Format("{0:#,0.#}", CSDK), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[CSCC]", string.Format("{0:#,0.#}", CSCC), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TyLe]", string.Format("{0:p0}", tyLe), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[CSD]", string.Format("{0:#,0.#}", chiSoDung), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[CSDTT]", string.Format("{0:#,0.#}", chiSoDungThucTe), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[DGCU]", string.Format("{0:n0}", donGiaCu), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[DGMoi]", string.Format("{0:n0}", donGiaMoi), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[VAT]", string.Format("{0:p0}", VAT), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[MaCTTT]", string.Format("{0:n0}", thanhTienMaCongTo), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TTTVAT]", string.Format("{0:n0}", thanhTienTruocVAT), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TTVAT]", string.Format("{0:n0}", thanhTienVAT), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TTSauVAT]", string.Format("{0:n0}", thanhTienSauVAT), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[NoKT]", noKyTruocChuaThanhToan > 0 ? string.Format("{0:n0}", noKyTruocChuaThanhToan) : "-", SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[TTT]", string.Format("{0:n0}", tongThanhToan), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[SoTien_BangChu]", soTienBangChu, SearchOptions.None);

            return ctlRtf.RtfText;
        }
        public static string OldDebts(int CustomerId, int month, int year, DevExpress.XtraRichEdit.RichEditControl ctlRtf, Field field, Document document)
        {
            var currentMonth = new DateTime(year, month, 1);
            var endEarlyLastMonth = currentMonth.AddDays(-1);

            var objData = Library.Class.Connect.QueryConnect.QueryData<deptOld>("deptOld",
                new
                {
                    CustomerId = CustomerId,
                    DateTo = endEarlyLastMonth
                });

            TableHandleMergeCell(field, document, objData.ToList(), 6);

            long amount = Convert.ToInt64(Math.Round((decimal)objData.Sum(_ => _.Amount).GetValueOrDefault(), 0));
            ctlRtf.Document.ReplaceAll("[PayTotal]", string.Format("{0:#,0}", amount), SearchOptions.None);

            var amountElectricText = new TienTeCls().DocTienBangChu(amount);
            ctlRtf.Document.ReplaceAll("[PayTotalWithVnText]", amountElectricText, SearchOptions.None);

            var PayTotalWithEngText = new TienTeCls().DocTienBangChuEN(amount, "VND");
            ctlRtf.Document.ReplaceAll("[PayTotalWithEngText]", PayTotalWithEngText, SearchOptions.None);

            return ctlRtf.Document.RtfText;
        }

        public static string OldDebtsHD(int CustomerId, int month, int year, DevExpress.XtraRichEdit.RichEditControl ctlRtf, Field field, Document document)
        {
            var currentMonth = new DateTime(year, month, 1);
            var endEarlyLastMonth = currentMonth.AddDays(-1);

            var objData = Library.Class.Connect.QueryConnect.QueryData<deptOld>("deptOldHD",
                new
                {
                    CustomerId = CustomerId,
                    DateTo = endEarlyLastMonth
                });

            TableHandleMergeCell(field, document, objData.ToList(), 6);

            long amount = Convert.ToInt64(Math.Round((decimal)objData.Sum(_ => _.Amount).GetValueOrDefault(), 0));
            ctlRtf.Document.ReplaceAll("[PayTotal]", string.Format("{0:#,0}", amount), SearchOptions.None);

            var amountElectricText = new TienTeCls().DocTienBangChu(amount);
            ctlRtf.Document.ReplaceAll("[PayTotalWithVnText]", amountElectricText, SearchOptions.None);

            var PayTotalWithEngText = new TienTeCls().DocTienBangChuEN(amount, "VND");
            ctlRtf.Document.ReplaceAll("[PayTotalWithEngText]", PayTotalWithEngText, SearchOptions.None);

            return ctlRtf.Document.RtfText;
        }

        public static string OldDebtsDNHD(int CustomerId, int month, int year, DevExpress.XtraRichEdit.RichEditControl ctlRtf, Field field, Document document)
        {
            var currentMonth = new DateTime(year, month, 1);
            var endEarlyLastMonth = currentMonth.AddDays(-1);

            var objData = Library.Class.Connect.QueryConnect.QueryData<deptOld>("deptOldDNHD",
                new
                {
                    CustomerId = CustomerId,
                    DateTo = endEarlyLastMonth
                });

            TableHandleMergeCell(field, document, objData.ToList(), 6);

            long amount = Convert.ToInt64(Math.Round((decimal)objData.Sum(_ => _.Amount).GetValueOrDefault(), 0));
            ctlRtf.Document.ReplaceAll("[PayTotal]", string.Format("{0:#,0}", amount), SearchOptions.None);

            var amountElectricText = new TienTeCls().DocTienBangChu(amount);
            ctlRtf.Document.ReplaceAll("[PayTotalWithVnText]", amountElectricText, SearchOptions.None);

            var PayTotalWithEngText = new TienTeCls().DocTienBangChuEN(amount, "VND");
            ctlRtf.Document.ReplaceAll("[PayTotalWithEngText]", PayTotalWithEngText, SearchOptions.None);

            return ctlRtf.Document.RtfText;
        }

        public static string GetDebitNoteTTTM(int CustomerId, int month, int year, DevExpress.XtraRichEdit.RichEditControl ctlRtf, Field field, Document document, int ApartmentId = 0)
        {

            #region

            var objData = Library.Class.Connect.QueryConnect.QueryData<DebitNoteHaveHeSo>("cnGetDebitNoteTTTM",
                new
                {
                    CustomerId = CustomerId,
                    Month = month,
                    Year = year,
                    ApartmentId = ApartmentId
                });

            TableHandleWithMergeCell(field, document, objData.ToList(), 12);
            
            #endregion

            return ctlRtf.Document.RtfText;
        }

    }

    #region Class
    public class DebitNote
    {
        public string No { get; set; }
        public string Des { get; set; }
        public string Fr { get; set; }
        public string To { get; set; }
        public decimal? Frst { get; set; }
        public decimal? Lst { get; set; }
        public decimal? Qun { get; set; }
        public string Unt { get; set; }
        public decimal? Prc { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Tax { get; set; }
        public decimal? TAm { get; set; }
        public decimal? TAmt { get; set; }
        public string Due { get; set; }
        public string Detail { get; set; }
        public string NoInt { get; set; }
    }

    public class DebitNoteHaveHeSo : DebitNote
    {
        public int? HeSo { get; set; }
    }

    public class DebitNoteWithTyGia : DebitNote
    {
        public decimal? TyGiaNganHang { get; set; }
        public decimal? Ti { get; set; }
    }

    public class Field
    {
        public int Index { get; set; }
        public string Name { get; set; }
    }

    public class DeptCustomer
    {
        public byte? MaTN { get; set; }

        public int MaMB { get; set; }

        public decimal? DienTich { get; set; }

        public string KyHieu { get; set; }

        public string TenKH { get; set; }

        public string MaSoMB { get; set; }

        public string SoHDCT { get; set; }

        public string TenTL { get; set; }

        public string MaPhu { get; set; }

        public string DCLL { get; set; }

        public string CtyMaSoThue { get; set; }

        public string CustomerAddress { get; set; }
        public string SoNha { get; set; }
        public string BlockName { get; set; }
        public string MaSo { get; set; }
        public string TenTN { get; set; }
    }

    public class DebitRiverPark
    {
        public decimal? Service { get; set; }

        public decimal OldS { get; set; }

        public decimal CurrS { get; set; }

        public decimal Area { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal? Car { get; set; }

        public decimal OldC { get; set; }

        public decimal CurrC { get; set; }

        public decimal CarUP { get; set; }

        public decimal CardQty { get; set; }

        public decimal MotorUP { get; set; }

        public decimal MotorQty { get; set; }

        public decimal EBikeUP { get; set; }

        public decimal EBikeQty { get; set; }

        public decimal BikeUP { get; set; }

        public decimal BikeQty { get; set; }

        public decimal OldE { get; set; }

        public decimal Electric { get; set; }

        public decimal CurrW { get; set; }

        public decimal OldW { get; set; }

        public decimal? Water { get; set; }

        public decimal WaterUP { get; set; }

        public decimal OldWM { get; set; }

        public decimal NewWM { get; set; }

        public decimal QtyWM { get; set; }
        public decimal WaterExVAT { get; set; }

        public decimal? Amount { get; set; }

    }

    public class DeptServiceRPWater
    {
        public int? WaterQty { get; set; }

        public decimal? WaterUP { get; set; }

        public decimal? WaterA { get; set; }

        public string Name { get; set; }

    }

    public class DeptMeLing
    {
        public decimal? Qty { get; set; }

        public decimal? Area { get; set; }

        public decimal? UP { get; set; }

        public decimal? ExVAT { get; set; }

        public decimal? PAT { get; set; }

        public long? STT { get; set; }
        public int? Type { get; set; }

        public string Description { get; set; }

    }

    public class StopProvidingServiceModel
    {
        public string No { get; set; }
        public string Description { get; set; }
        public string PayMonth { get; set; }
        public decimal Debit { get; set; }
        public string Detail { get; set; }
        public string NoInt { get; set; }
        public int ServiceId { get; set; }
    }

    public class deptOld
    {
        public int? ServiceId { get; set; }

        public decimal? Amount { get; set; }

        public string No { get; set; }

        public string NoInt { get; set; }

        public string Description { get; set; }

        public string Period { get; set; }

        public string Amtount { get; set; }

        public string Detail { get; set; }

    }

    #endregion
}
