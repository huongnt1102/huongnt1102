using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
using System.Data.Linq.SqlClient;

namespace Library.Controls.LichHen
{
    public partial class ctlEditLichHen : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();

        Library.LichHen objLH;

        public int? MaNC;

        public int? MaLH;

        public int? MaKH;

        public byte? MaTN;


        public ctlEditLichHen()
        {
            InitializeComponent();
                
        }

        void LoadDictionary()
        {
            lookUpRemine.Properties.DataSource = db.Times.ToList();
            lookUpRemine.ItemIndex = 0;

            lookUpRepeat.Properties.DataSource = db.Times.ToList();
            lookUpRepeat.ItemIndex = 0;

            glkKhachHang.LoadData(null);

            glkNhanVien_Tiep.LoadData(null);

            glkNhanVien_Tiep.EditValue = Common.User.MaNV;


            glkNhanVien_HoTro.LoadData(null);

            cmbNhanVien.Properties.DataSource = db.tnNhanViens.Where(p => p.MaNV != Common.User.MaNV
                                                                      & p.MaTN == Common.User.MaTN)
                                                              .Select(p => new
                                                              {
                                                                  p.MaNV,
                                                                  HoTen = p.HoTenNV
                                                              }).ToList();

            glkToaNha.Properties.DataSource = db.tnToaNhas.Select(o => new {o.MaTN, o.TenVT, o.TenTN }).ToList();

            lkNhiemVu.Properties.DataSource = db.NhiemVus.Select(p => new { p.MaNV, p.TieuDe });

            cmbChuDe.Properties.DataSource = db.LichHen_ChuDes.Select(p => new { p.MaCD, p.TenCD });

            lkThoiDiem.Properties.DataSource = db.LichHen_ThoiDiems;
        }

        public void LoadData(int? maLH)
        {
            this.MaLH = maLH;

            this.LoadDictionary();

            try
            {
                objLH = db.LichHens.SingleOrDefault(p => p.MaLH == this.MaLH);

                if (objLH != null)
                {
                    objLH.NgaySua = DateTime.Now;
                    objLH.MaNVS = Common.User.MaNV;
                }
                else
                {
                    objLH = new Library.LichHen();
                    objLH.MaTN = this.MaTN;
                    objLH.MaNV = Common.User.MaNV;
                    objLH.NgayNhap = DateTime.Now;
                    objLH.NgayBD = DateTime.Now;
                    objLH.NgayKT = DateTime.Now.AddHours(3);
                    objLH.IsRepeat = false;
                    objLH.IsNhac = false;
                    objLH.MaNC = this.MaNC;
                    objLH.MaKH = this.MaKH;

                    if (this.MaNC != null)
                    {
                        var objNC = db.ncNhuCaus.Single(p => p.MaNC == this.MaNC);
                        objLH.MaTN = objNC.MaTN;
                        objLH.MaKH = objNC.MaKH;
                        objLH.SoLH = Library.Utilities.LichHenCls.TaoSoCT(objNC.MaNC, this.MaTN);
                    }
                    else
                    {
                        objLH.SoLH = Library.Utilities.LichHenCls.TaoSoCT(0, this.MaTN);
                    }
                    
                    db.LichHens.InsertOnSubmit(objLH);
                }

                

                txtSoLH.Text = objLH.SoLH;
                txtTieuDe.EditValue = objLH.TieuDe;

                txtDienGiai.EditValue = objLH.DienGiai;

                glkToaNha.EditValue = objLH.DiaDiem;
                //checkToaNha.SetEditValue(objLH.DiaDiem);

                dateNgayBD.EditValue = objLH.NgayBD;
                dateNgayKT.EditValue = objLH.NgayKT;

                glkKhachHang.EditValue = objLH.MaKH;
                chkRemine.EditValue = objLH.IsNhac;
                spThoiGianGap.EditValue = objLH.ThoiGianGap;
                chkIsRepeat.EditValue = objLH.IsRepeat;
                lkThoiDiem.EditValue = objLH.MaTD;
                lookUpRemine.EditValue = objLH.TimeID;
                lookUpRepeat.EditValue = objLH.TimeID;
                lookUpRepeat.Enabled = objLH.IsRepeat.GetValueOrDefault();
                lookUpRemine.Enabled = objLH.IsNhac.GetValueOrDefault();

                glkNhanVien_HoTro.EditValue = objLH.MaNVHoTro;

                glkNhanVien_Tiep.EditValue = objLH.MaNVTiep;

                string nv = String.Join(";", objLH.LichHen_tnNhanViens.Select(o => objLH.MaNV.ToString()).ToArray());
                cmbNhanVien.SetEditValue(nv);

                string chudes = String.Join(",", objLH.LichHen_ChiTietChuDes.Select( p=> p.MaCD.ToString()).ToArray());
                cmbChuDe.SetEditValue(chudes);

				if(CheckLockDate())
				{
					dateNgayBD.Properties.ReadOnly = true;
					spThoiGianGap.Properties.ReadOnly = true;
				}

                this.chiTietCoHoiLoad();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                //db.Dispose();
            }
        }

		bool CheckLockDate()
		{
			return db.LichHens.Any(o => o.MaLH == this.MaLH & SqlMethods.DateDiffMinute(o.NgayBD, DateTime.Now) > 0);
		}

        public bool Save(bool IsSendMail)
        {
            if (objLH.MaNC != null & objLH.MaLH == 0)
            {
                if (db.LichHens.Any(o => o.MaNC == this.MaNC & o.MaLH != objLH.MaLH & o.XuLy_MaTT == null))
                {
                    DialogBox.Error("Đang có lịch hẹn vẫn chưa xử lý. Không thể tạo thêm lịch hẹn");
                    return false;
                }
            }

            if (txtTieuDe.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập chủ đề. Xin cảm ơn.");
                txtTieuDe.Focus();
                return false;
            }

            if (dateNgayBD.EditValue == null | dateNgayKT.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập <Ngày Bắt Đầu> - <Ngày Kết Thúc>");
                return false;
            }

            if (SqlMethods.DateDiffSecond(dateNgayBD.DateTime, dateNgayKT.DateTime) < 0)
            {
                DialogBox.Error("Khoảng thời gian không hợp lệ. Vui lòng kiểm tra lại");
                dateNgayKT.Focus();
                return false;
            }

            if (glkNhanVien_Tiep == null || glkNhanVien_Tiep.EditValue == "")
            {
                DialogBox.Error("Vui lòng chọn nhân viên tiếp");
                return false;
            }

            if (spThoiGianGap.Value == 0)
            {
                DialogBox.Error("Vui lòng nhập thời gian gặp");
                return false;
            }

            var idChuDes = Convert.ToString(cmbChuDe.EditValue).Split(',')
                                  .Where(p => p.Trim().Length > 0)
                                  .Select(p => new LichHen_ChiTietChuDe()
                                  {
                                      MaCD = int.Parse(p)
                                  }).ToList();

            if (idChuDes.Count == 0)
            {
                DialogBox.Error("Vui lòng chọn chủ đề");
                return false;
            }

            if (glkToaNha.EditValue == null || glkToaNha.EditValue == "")
            {
                DialogBox.Error("Vui lòng chọn địa điểm");
                return false;
            }

           

            try
            {
                objLH.MaKH = (int?)glkKhachHang.EditValue;
                objLH.TieuDe = txtTieuDe.Text;
                objLH.DienGiai = txtDienGiai.Text;
                //objLH.DiaDiem = (byte?)glkToaNha.EditValue;
                objLH.NgayBD = dateNgayBD.DateTime;
                objLH.NgayKT = dateNgayKT.DateTime;
                objLH.MaNVu = (int?)lkNhiemVu.EditValue;
                objLH.IsNhac = chkRemine.Checked;
                objLH.TimeID = (byte?)lookUpRemine.EditValue;
                objLH.TimeID2 = (byte?)lookUpRepeat.EditValue;
                objLH.ThoiGianGap = (int)spThoiGianGap.Value;
                objLH.MaTD = (int?)lkThoiDiem.EditValue;
                objLH.IsNhac = chkRemine.Checked;
                objLH.IsRepeat = chkIsRepeat.Checked;
                objLH.MaNVTiep = (int?)glkNhanVien_Tiep.EditValue;
                objLH.MaNVHoTro = (int?)glkNhanVien_HoTro.EditValue;

                if (chkRemine.Checked == true)
                {
                    if (lookUpRemine.EditValue == null)
                    {
                        DialogBox.Error("Vui lòng chọn thời gia nhắc");
                        return false;
                    }

                    var obj = db.Times.Single(p => p.TimeID == (byte?)lookUpRemine.EditValue);
                    objLH.NgayNhac = dateNgayBD.DateTime.AddMinutes(-obj.Minutes.GetValueOrDefault());
                }
                else
                {
                    objLH.NgayNhac = null;
                }

                var nv = Convert.ToString(cmbNhanVien.EditValue).Split(';').Where(p => p.Trim().Length > 0).Select(p => int.Parse(p)).ToList();
                nv.Add(objLH.MaNV.Value);

                var removes = objLH.LichHen_tnNhanViens.Where(o => nv.Contains(o.MaNV)).ToList();
                db.LichHen_tnNhanViens.DeleteAllOnSubmit(removes);

                // Luu nhan vien co mat trong table Lichhen_tnnhanvien
                foreach (var i in nv)
                {
                    if (!objLH.LichHen_tnNhanViens.Any(p => p.MaNV == i))
                    {
                        var objNV = new LichHen_tnNhanVien();
                        objNV.MaNV = int.Parse(i.ToString());
                        objNV.DaNhac = false;
                        objNV.IsMain = true;
                        objNV.IsNhac = true;
                        objNV.NgayNhac = objLH.NgayNhac;
                        objLH.LichHen_tnNhanViens.Add(objNV);
                    }
                }

                var ltRemove = db.LichHen_MatBangs.Where(p => p.MaLH == MaLH);

                db.LichHen_MatBangs.DeleteAllOnSubmit(ltRemove);

                objLH.LichHen_ChiTietChuDes.Clear();
                objLH.LichHen_ChiTietChuDes.AddRange(idChuDes);

                db.SubmitChanges();

                if (this.MaNC > 0)
                    Library.Utilities.NhuCauCls.TinhTiemNang(this.MaNC);

                if (IsSendMail)
                {
                    //List<Library.Mail.MailClient.EmailCls> Emails = new List<Library.Mail.MailClient.EmailCls>();
                    //var objKH = db.tnKhachHangs.Single(o => o.MaKH == (int?)glkKhachHang.EditValue);
                    //Emails.Add(new Library.Mail.MailClient.EmailCls() { Email = objKH.Email, MaKH = objKH.MaKH });

                    //using (var frm = new Library.Email.frmSend("Building.WorkSchedule.LichHen", 248, (byte)MaTN, Emails))
                    //{
                    //    frm.LinkID = objLH.MaLH;
                    //    frm.ShowDialog();
                    //}
                }
                return true;
            }
            catch(Exception ex)
            {
                DialogBox.Error(ex.Message);
                return false;
            }
        }

        private void glkKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            //glkCoHoi.LoadData((int?)glkKhachHang.EditValue);
        }

        private void lkNhiemVu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 1:
                    NhiemVu.Select_frm frm = new Library.Controls.NhiemVu.Select_frm();
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        lkNhiemVu.EditValue = frm.MaNVu;
                    }
                    break;
                case 2:
                    lkNhiemVu.EditValue = null;
                    break;
            }
        }

        private void chkIsRepeat_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit _New = (CheckEdit)sender;
            if (_New.Checked)
                lookUpRepeat.Enabled = true;
            else
                lookUpRepeat.Enabled = false;
        }

        private void chkRemine_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit _New = (CheckEdit)sender;
            if (_New.Checked)
                lookUpRemine.Enabled = true;
            else
                lookUpRemine.Enabled = false;
        }

        private void glkCoHoi_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        void chiTietCoHoiLoad()
        {
            if (objLH.MaNC == null)
            {
                grChiTietThue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                return;
            }

            using (var db = new MasterDataContext())
            {
                gcNhuCau.DataSource = (from ct in db.ncSanPhams

                                       join nc in db.NhuCauThues on ct.idNhuCauThue equals nc.ID into nhucau
                                       from nc in nhucau.DefaultIfEmpty()

                                       join tn in db.tnToaNhas on ct.MaTN equals tn.MaTN into toanha
                                       from tn in toanha.DefaultIfEmpty()

                                       join dvt in db.DonViTinhs on ct.MaDVT equals dvt.ID into donvitinh
                                       from dvt in donvitinh.DefaultIfEmpty()

                                       where ct.MaNC == objLH.MaNC
                                       select new
                                       {
                                           nc.TenNhuCau,
                                           tn.TenTN,
                                           ct.NgayDatHang,
                                           ct.SoLuong,
                                           ct.DonGia,
                                           ct.ThanhTien,
                                           ct.TienCK,
                                           ct.TyLeCK,
                                           ct.ThueGTGT,
                                           ct.TienGTGT,
                                           ct.SoTien,
                                           ct.DienGiai,
                                           ct.ThoiGianDuKienSD,
                                           dvt.TenDVT,
                                       }).ToList();
            }
        }

        private void spThoiGianGap_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
				if (CheckLockDate())
					return;

                dateNgayKT.EditValue = dateNgayBD.DateTime.AddMinutes((int)spThoiGianGap.Value);
            }
            catch
            {
            }
        }

        private void ctlEditLichHen_Load(object sender, EventArgs e)
        {

        }

        private void cmbChuDe_EditValueChanged(object sender, EventArgs e)
        {
            txtTieuDe.Text = cmbChuDe.Text;
        }
    }
}
