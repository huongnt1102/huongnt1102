using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Data.Linq.SqlClient;

using Library;
//using Library.Email;
using Library.Utilities;
using DevExpress.XtraGrid.Views.Grid;

namespace DIPCRM.NhuCau
{
	public partial class ctlManager : DevExpress.XtraEditors.XtraForm
	{
		public int? MaNC { get; set; }
		public int DayTo { get; set; }
		public int DayFrom { get; set; }

		private MasterDataContext db;
		List<ncNhuCau> ListReminder;
		byte SDBID = 6;

        GridView grvNhuCau;

		public ctlManager()
		{
			InitializeComponent();
            grvNhuCau = ctlNhuCau1.grvNhuCau;

            ctlBaoGia1.frm = this;
            ctlNhuCau1.frm = this;
            //ctlMailHistory1.frm = this;
            ctlLichHen1.frm = this;
		}

		void itemCongTy_EditValueChanged(object sender, EventArgs e)
		{
			ctlNhuCau1.LoadData();
		}

		private void SetDate(int index)
		{
			KyBaoCao objKBC = new KyBaoCao();
			objKBC.Index = index;
			objKBC.SetToDate();

			itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
			itemTuNgay.EditValue = objKBC.DateFrom;
			itemDenNgay.EditValue = objKBC.DateTo;
			itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
		}


		private void NhuCau_Click()
		{
			try
			{
				int? MaNC = (int?)grvNhuCau.GetFocusedRowCellValue("MaNC");
				if (MaNC == null)
				{
					switch (tabMain.SelectedTabPage.Name)
					{
						case "tpThongTinChiTiet":
							vgcChiTiet.DataSource = null;
							txtDienGiai.EditValue = null;
							break;
						case "tpChiTietPhongGhe":
							gcProduct.DataSource = null;
							break;
						case "tpNhatKyXuLy":
							gcNhatKy.DataSource = null;
							break;
						case "tpLichLamViec":
							break;
						case "tpHopDong":
							break;
						case "tpBaoGia":
							ctlBaoGia1.MaKH = null;
							ctlBaoGia1.MaNC = null;
							ctlBaoGia1.BaoGia_Load();
							break;
						case "tpEmail":
							break;
						case "tpSMS":
							break;
						case "tpNguoiLienHe":
							break;
						case "tpDoiTac":
							break;
						case "tpDoiThu":
							break;
						case "tpTaiLieu":
							break;
						case "tpGhiChu":
							break;
					}
					return;
				}

				db = new MasterDataContext();

				switch (tabMain.SelectedTabPage.Name)
				{
					case "tpThongTinChiTiet":
						#region Chi tiet
						var ltChiTiet = (from nc in db.ncNhuCaus
										 join kh in db.tnKhachHangs on nc.MaKH equals kh.MaKH
                                         //join lh in db.NguoiLienHes on kh.MaNLH equals lh.ID into lhe
                                         //from lh in lhe.DefaultIfEmpty()
										 join ql in db.tnNhanViens on nc.MaNVQL equals ql.MaNV
										 join nhap in db.tnNhanViens on nc.MaNVN equals nhap.MaNV
										 join sua in db.tnNhanViens on nc.MaNVS equals sua.MaNV into nvSua
										 from sua in nvSua.DefaultIfEmpty()
										 where nc.MaNC == MaNC
										 select new
										 {
											 nc.MaNC,
											 nc.SoNC,
											 nc.TiemNang,
											 nc.DienGiai,
											 HoTenNVQL = ql.HoTenNV,
											 nc.NgayNhap,
											 HoTenNVN = nhap.HoTenNV,
											 nc.NgaySua,
											 HoTenNVS = sua.HoTenNV,
											 HoTenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH + " " + kh.TenKH : kh.CtyTen,
											 kh.NgaySinh,
											 kh.DiaChi,
											 //kh.Tuoi,
											 //TenNN = kh.NgheNghiep.TenNN,
											 //TenNghe = kh.TenNNKD,
											 kh.DiDong,
											 kh.Email,
                                             //NguoiDaiDien = lh.HoTen,
											 //kh.ncNguonKH.TenNguon,
											 SLKH = nc.ncSanPhams.Sum(p => p.SoLuong).GetValueOrDefault()
										 }).ToList();
						vgcChiTiet.DataSource = ltChiTiet;
						txtDienGiai.EditValue = ltChiTiet[0].DienGiai;
						#endregion
						break;
					case "tpChiTietPhongGhe":
						#region San pham
						gcProduct.DataSource = (from ct in db.ncSanPhams

												join nc in db.NhuCauThues on ct.idNhuCauThue equals nc.ID into nhucau
												from nc in nhucau.DefaultIfEmpty()

												join tn in db.tnToaNhas on ct.MaTN equals tn.MaTN into toanha
												from tn in toanha.DefaultIfEmpty()

												join dvt in db.DonViTinhs on ct.MaDVT equals dvt.ID into donvitinh
												from dvt in donvitinh.DefaultIfEmpty()

												where ct.MaNC == MaNC
												select new
												{
													ct.ID,
													nc.TenNhuCau,
                                                    ct.NganSach,
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
						#endregion
						break;
					case "tpNhatKyXuLy":
						#region Nhat ky xu ly
						gcNhatKy.DataSource = (from nk in db.ncNhatKies
											   join tt in db.ncTrangThais on nk.MaTT equals tt.MaTT
											   join tx in db.ncHinhThucTiepXucs on nk.MaHTTX equals tx.ID into hinhthuc
											   from tx in hinhthuc.DefaultIfEmpty()
											   join ql in db.tnNhanViens on nk.MaNVQL equals ql.MaNV into ql_nk
											   from ql in ql_nk.DefaultIfEmpty()
											   join nhap in db.tnNhanViens on nk.MaNVN equals nhap.MaNV
											   where nk.MaNC == MaNC
											   orderby nk.NgayXL descending
											   select new
											   {
												   nk.ID,
												   nk.NgayXL,
												   tt.TenTT,
												   tx.TenHTTX,
												   nk.DienGiai,
												   HoTenNVQL = ql.HoTenNV,
												   nk.MaNVN,
												   nk.NgayNhap,
												   HoTenNVN = nhap.HoTenNV,
                                                   //nk.CallID
											   }).ToList();
						#endregion
						break;
					case "tpLichLamViec":
						#region Lich lam viec
						ctlLichHen1.LoadData(MaNC, 0);
						#endregion
						break;
                    //case "tpHopDong":
                        //ctlHopDong1.MaKH = ctlNhuCau1.getMaKH();
                        //ctlHopDong1.MaNC = ctlNhuCau1.getID();
                        //ctlHopDong1.LoadData();
						break;
					case "tpBaoGia":
						ctlBaoGia1.MaNC = MaNC;
						ctlBaoGia1.BaoGia_Load();
						break;
					case "tpEmail":
						break;
					case "tpSMS":
						break;
					case "tpNguoiLienHe":
						break;
					case "tpDoiTac":
						break;
					case "tpDoiThu":
						break;
					case "tpTaiLieu":
						break;
					case "tpGhiChu":
						break;
				}
			}
			catch (Exception ex)
			{
				DialogBox.Error(ex.Message);
			}
		}

		private void ctlManager_Load(object sender, EventArgs e)
		{
			try
			{
                //Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
				db = new MasterDataContext();
				cmbTrangThai.DataSource = db.ncTrangThais.OrderBy(p => p.STT).Select(p => new { p.MaTT, p.TenTT });
				lkToaNha.DataSource = Common.TowerList;
				itemToaNha.EditValue = Common.User.MaTN;

				KyBaoCao objKBC = new KyBaoCao();
				foreach (string str in objKBC.Source)
				{
					cmbKyBaoCao.Items.Add(str);
				}
				itemKyBaoCao.EditValue = objKBC.Source[3];

				SetDate(3);
                ctlNhuCau1.MaTN = Common.User.MaTN;
                ctlNhuCau1.LoadData();

                this.SetEventButton();

			}
			catch { }


		}

        void SetEventButton()
        {
            itemPreview.ItemClick += ctlNhuCau1.itemPreview_ItemClick;
            itemThem.ItemClick += ctlNhuCau1.itmThem_ItemClick;
            itemSua.ItemClick += ctlNhuCau1.itemSua_ItemClick;
            itemXoa.ItemClick += ctlNhuCau1.itemXoa_ItemClick;
            itemDuyet.ItemClick += ctlNhuCau1.itemXuLy_ItemClick;
            itemNhanVienHoTro.ItemClick += ctlNhuCau1.itemNhanVienHoTro_ItemClick;
            itemGiaoDich.ItemClick += ctlNhuCau1.itemHopDong_ItemClick;
            itemSendMail.ItemClick += ctlNhuCau1.itemGuiMail_ItemClick;
            itemSendSMS.ItemClick += ctlNhuCau1.itemGuiSMS_ItemClick;
            itemExport.ItemClick += ctlNhuCau1.itemExport_ItemClick;
            itemTinhTiemNang.ItemClick += ctlNhuCau1.itemTinhTiemNang_ItemClick;

            ctlNhuCau1.grvNhuCau.FocusedRowChanged += grvNhuCau_FocusedRowChanged;
        }

		private void ItemPrintBM_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
		}

		private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
		{
            ctlNhuCau1.LoadData();
		}

		private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
		{
            ctlNhuCau1.LoadData();
		}

		private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
		{
			SetDate((sender as ComboBoxEdit).SelectedIndex);
		}

		private void itemTrangThai_EditValueChanged(object sender, EventArgs e)
		{
			if (itemTrangThai.EditValue != null)
                ctlNhuCau1.LoadData();
		}

		private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
            ctlNhuCau1.MaTN = (byte)itemToaNha.EditValue;
            ctlNhuCau1.LoadData();
        }

		private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			NhuCau_Click();
		}

		private void grvNhuCau_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			NhuCau_Click();
		}

		#region Lich hen
		private void itemLichHen_Add_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			try
			{
				//var maNC = (int?)grvNhuCau.GetFocusedRowCellValue("MaNC");
				//if (maNC == null)
				//{
				//    DialogBox.Error("Vui lòng chọn nhu cầu, xin cảm ơn.");
				//    return;
				//}
				//var frm = new CongViec.LichHen.AddNew_frm();
				//frm.MaNC = maNC;
				//frm.ShowDialog();
				//if (frm.DialogResult == DialogResult.OK)
				//    NhuCau_Click();
			}
			catch (Exception ex)
			{
				DialogBox.Error(ex.Message);
			}
		}

		private void itemLichHen_Edit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			try
			{
				//var maLH = (int?)grvLich.GetFocusedRowCellValue("MaLH");
				//if (maLH == null)
				//{
				//    DialogBox.Error("Vui lòng chọn lịch hẹn, xin cảm ơn.");
				//    return;
				//}

				//var frm = new CongViec.LichHen.AddNew_frm();
				//frm.MaLH = maLH;
				//frm.ShowDialog();
				//if (frm.DialogResult == DialogResult.OK)
				//    NhuCau_Click();
			}
			catch (Exception ex)
			{
				DialogBox.Error(ex.Message);
			}
		}

		private void itemLichHen_Delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//try
			//{
			//    var indexs = grvLich.GetSelectedRows();
			//    if (indexs.Length <= 0)
			//    {
			//        DialogBox.Error("Vui lòng chọn lịch hẹn");
			//        return;
			//    }
			//    if (DialogBox.Question("Bạn có chắc không?") == DialogResult.No) return;
			//    foreach (var i in indexs)
			//    {
			//        var objLH = db.LichHens.Single(p => p.MaLH == (int)grvLich.GetRowCellValue(i, "MaLH"));
			//        db.LichHens.DeleteOnSubmit(objLH);
			//    }
			//    db.SubmitChanges();

			//    NhuCau_Click();
			//}
			//catch (Exception ex)
			//{
			//    DialogBox.Error(ex.Message);
			//}
		}
		#endregion

		private void grvNhatKy_DoubleClick(object sender, EventArgs e)
		{
			var id = (int?)grvNhatKy.GetFocusedRowCellValue("ID");
			if (id == null)
			{
				DialogBox.Error("Vui lòng chọn nhật ký cần sửa");
				return;
			}

			if ((int)grvNhatKy.GetFocusedRowCellValue("MaNVN") != Common.User.MaNV)
			{
				DialogBox.Error("Bạn không thể sửa nhật ký này");
				return;
			}

			try
			{
                var frm = new Library.Controls.NhuCau.frmDuyet();
				frm.objNK = db.ncNhatKies.Single(p => p.ID == id);
				if (frm.ShowDialog() == DialogResult.OK)
				{
					db.SubmitChanges();
					NhuCau_Click();
				}
			}
			catch (Exception ex)
			{
				DialogBox.Error(ex.Message);
			}
		}

		private void itemAddOrganization_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (grvNhuCau.FocusedRowHandle < 0)
			{
				DialogBox.Error("Vui lòng chọn <Nhu cầu>, xin cảm ơn.");
				return;
			}

			if (tabMain.SelectedTabPageIndex == 6)
			{
				var f = new frmAddOrganization();
				f.MaNC = Convert.ToInt32(grvNhuCau.GetFocusedRowCellValue("MaNC"));
				f.ShowDialog();
				if (f.DialogResult == DialogResult.OK)
					NhuCau_Click();
			}
			else
			{
				var f = new frmAddDoiThu();
				f.MaNC = Convert.ToInt32(grvNhuCau.GetFocusedRowCellValue("MaNC"));
				f.ShowDialog();
				if (f.DialogResult == DialogResult.OK)
					NhuCau_Click();
			}
		}

		private void itemEditOrganization_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (tabMain.SelectedTabPageIndex == 6)
			{
				if (gvDoiTac.FocusedRowHandle < 0)
				{
					DialogBox.Error("Vui lòng chọn <Đối tác>, xin cảm ơn.");
					return;
				}

				var f = new frmAddOrganization();
				f.MaNC = Convert.ToInt32(gvDoiTac.GetFocusedRowCellValue("MaNC"));
				f.ORGID = Convert.ToInt32(gvDoiTac.GetFocusedRowCellValue("ORGID"));
				f.ShowDialog();
				if (f.DialogResult == DialogResult.OK)
					NhuCau_Click();
			}
			else
			{
				if (gvDoiThu.FocusedRowHandle < 0)
				{
					DialogBox.Error("Vui lòng chọn <Đối thủ>, xin cảm ơn.");
					return;
				}

				var f = new frmAddDoiThu();
				f.MaNC = Convert.ToInt32(gvDoiThu.GetFocusedRowCellValue("MaNC"));
				f.ORGID = Convert.ToInt32(gvDoiThu.GetFocusedRowCellValue("ORGID"));
				f.ShowDialog();
				if (f.DialogResult == DialogResult.OK)
					NhuCau_Click();
			}
		}

		private void itemDeleteOrganization_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (tabMain.SelectedTabPageIndex == 6)
			{
				if (gvDoiTac.FocusedRowHandle < 0)
				{
					DialogBox.Error("Vui lòng chọn <Đối tác>, xin cảm ơn.");
					return;
				}

				if (DialogBox.Question("Bạn có chắc chắn muốn xóa <Đối tác> này không?") == DialogResult.Yes)
				{
					try
					{
                        //ncOrganization objNCO = db.ncOrganizations.Single(o => o.ORGID == Convert.ToInt32(gvDoiTac.GetFocusedRowCellValue("ORGID")) && o.MaNC == Convert.ToInt32(gvDoiTac.GetFocusedRowCellValue("MaNC")));
                        //db.ncOrganizations.DeleteOnSubmit(objNCO);
						db.SubmitChanges();

						gvDoiTac.DeleteSelectedRows();
					}
					catch { }
				}
			}
			else
			{
				if (gvDoiThu.FocusedRowHandle < 0)
				{
					DialogBox.Error("Vui lòng chọn <Đối thủ>, xin cảm ơn.");
					return;
				}

				if (DialogBox.Question("Bạn có chắc chắn muốn xóa <Đối thủ> này không?") == DialogResult.Yes)
				{
					try
					{
                        //ncOrganization objNCO = db.ncOrganizations.Single(o => o.ORGID == Convert.ToInt32(gvDoiThu.GetFocusedRowCellValue("ORGID")) && o.MaNC == Convert.ToInt32(gvDoiThu.GetFocusedRowCellValue("MaNC")));
                        //db.ncOrganizations.DeleteOnSubmit(objNCO);
						db.SubmitChanges();

						gvDoiThu.DeleteSelectedRows();
					}
					catch { }
				}
			}
		}


		private void itemFilterType_EditValueChanged(object sender, EventArgs e)
		{
            ctlNhuCau1.LoadData();
		}

		private void btnCallPhone_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{
				var phone = (sender as ButtonEdit).Text ?? "";
				if (phone.Trim() == "") return;

				DIP.SwitchBoard.SwitchBoard.SoftPhone.Call(phone);
			}
			catch { }
		}

		private void hplNgheLaiCuocGoi_Click(object sender, EventArgs e)
		{
			try
			{
				var callID = grvNhatKy.GetFocusedRowCellValue("CallID") as string;
				if (callID == null) return;

				var date = (DateTime)grvNhatKy.GetFocusedRowCellValue("NgayXL");

				DIP.SwitchBoard.SwitchBoard.SoftPhone.ListenAgain(callID, date);
			}
			catch { }
		}

		private void itemTinhTiemNang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
            var _MaNC = (int)ctlNhuCau1.getID();
            if (_MaNC != null)
                Library.Utilities.NhuCauCls.TinhTiemNang(MaNC); 
        }

        private void itemGiaoDich_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var maNC = ctlNhuCau1.getID();
            //var maKH = ctlNhuCau1.getMaKH();
            //var checkCT = db.tnKhachHangs.FirstOrDefault(o=>o.MaKH == maKH).IsChinhThuc;
            //if (maNC == null)
            //{
            //    DialogBox.Error("Vui lòng chọn cơ hội");
            //    return;
            //}
            //if(!(bool)checkCT)
            //{
            //    DialogBox.Alert("Vui lòng chọn khách hàng chính thức!");
            //    return;
            //}
            //try
            //{
            //    using (LandSoftBuilding.Lease.frmEdit frm = new LandSoftBuilding.Lease.frmEdit())
            //    {
            //        //frm.MaNC = maNC;
            //        frm.MaTN = (byte?)itemToaNha.EditValue;
            //        //frm.IsVPAo = false;
            //        //frm.MaKH_NC = maKH;

            //        frm.ShowDialog();
            //        if (frm.DialogResult == DialogResult.OK)
            //        {
            //            ctlNhuCau1.LoadData();
            //        }
            //    }
            //}
            //catch
            //{
            //}

        }
	}
}


