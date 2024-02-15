using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace LandSoftBuilding.Lease.DatCoc.ThiCong
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        #region Param/ Class
        public object Id { get; set; }
        public byte? TowerId { get; set; }

        
        #endregion


        public frmEdit()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    glkKhachHang.Properties.DataSource = (from kh in db.tnKhachHangs
                                                          where kh.MaTN == TowerId
                                                          select new
                                                          {
                                                              kh.MaKH,
                                                              TenKH = kh.IsCaNhan == true ? kh.TenKH : kh.CtyTen
                                                          });
                    glkHangMuc.Properties.DataSource = (from hm in db.DangKyThiCong_HangMucs
                                                          select new
                                                          {
                                                              MaHM = hm.ID,
                                                              TenHM = hm.TenHangMuc
                                                          });
                    glkPhongBan.Properties.DataSource = (from pb in db.tnPhongBans
                                                         select new
                                                         {
                                                             pb.MaPB,
                                                             pb.TenPB
                                                         });
                    // Phòng ban show app của tòa nhà
                    var departmentDefault = db.tnPhongBans.FirstOrDefault(_ => _.IsShowApp.GetValueOrDefault() & _.MaTN == TowerId);
                    if(departmentDefault!= null)
                    {
                        glkPhongBan.EditValue = departmentDefault.MaPB;
                    }
                    
                    dateTuNgay.DateTime = dateDenNgay.DateTime = System.DateTime.UtcNow.AddHours(7);

                    glkMatBang.Properties.DataSource = db.mbMatBangs.Where(_ => _.MaTN == TowerId);

                    var obj = db.dkThiCongs.FirstOrDefault(_ => Convert.ToString(_.Id) == Convert.ToString(Id));
                    if (obj != null)
                    {
                        glkKhachHang.EditValue = obj.MaKH;
                        var details = db.dkThiCong_ChiTiets.FirstOrDefault(_ => _.DangKy_ID == obj.Id);
                        if (details != null)
                        {
                            glkHangMuc.EditValue = details.HangMuc_ID;
                        }
                        chkGayTiengOn.EditValue = obj.GayTiengOn;
                        chkPhatSinhChatThai.EditValue = obj.PhatSinhChacThai;
                        chkSuDungHan.EditValue = obj.SuDungHan;
                        chkSuDungKhoan.EditValue = obj.SuDungKhoan;

                        glkMatBang.EditValue = obj.MaMB;

                        memoNote.Text = obj.GhiChu;

                        dateTuNgay.DateTime = obj.TuNgay.GetValueOrDefault();
                        dateDenNgay.DateTime = obj.DenNgay.GetValueOrDefault();

                        glkPhongBan.EditValue = obj.MaPB;
                    }
                }
            }
            catch (System.Exception ex)
            {
                //DialogBox.Error("Load thông tin phiếu bị lỗi: " + ex.Message);
            }  
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                #region Check dữ liệu
                if (glkKhachHang.EditValue == null)
                {
                    DialogBox.Alert("Vui lòng chọn khách hàng");
                    glkKhachHang.Focus();
                    this.layoutControlItem1.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
                    this.layoutControlItem1.AppearanceItemCaption.Options.UseForeColor = true;
                    return;
                }
                else
                {
                    this.layoutControlItem1.AppearanceItemCaption.Options.UseForeColor = false;
                }

                if (glkHangMuc.EditValue == null)
                {
                    DialogBox.Alert("Vui lòng chọn hạng mục thi công");
                    glkHangMuc.Focus();
                    this.layoutControlItem2.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
                    this.layoutControlItem2.AppearanceItemCaption.Options.UseForeColor = true;
                    return;
                }
                else
                {
                    this.layoutControlItem2.AppearanceItemCaption.Options.UseForeColor = false;
                }

                if (spinSoLuongNhanCong.Value <= 0)
                {
                    DialogBox.Alert("Số lượng nhân công không hợp lệ");
                    spinSoLuongNhanCong.Focus();
                    this.layoutControlItem3.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
                    this.layoutControlItem3.AppearanceItemCaption.Options.UseForeColor = true;
                    return;
                }
                else
                {
                    this.layoutControlItem3.AppearanceItemCaption.Options.UseForeColor = false;
                }

                if (glkMatBang.EditValue == null)
                {
                    DialogBox.Alert("Vui lòng chọn mặt bằng thi công");
                    glkMatBang.Focus();
                    this.layoutControlItem6.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
                    this.layoutControlItem6.AppearanceItemCaption.Options.UseForeColor = true;
                    return;
                }
                else
                {
                    this.layoutControlItem6.AppearanceItemCaption.Options.UseForeColor = false;
                }

                if(glkPhongBan.EditValue == null)
                {
                    DialogBox.Alert("Vui lòng chọn phòng ban thực hiện / tiếp nhận");
                    glkPhongBan.Focus();
                    this.layoutControlItem7.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
                    this.layoutControlItem7.AppearanceItemCaption.Options.UseForeColor = true;
                    return;
                }
                else
                {
                    this.layoutControlItem7.AppearanceItemCaption.Options.UseForeColor = false;
                }

                if (dateTuNgay.DateTime > dateDenNgay.DateTime)
                {
                    DialogBox.Alert("Thời gian thi công không hợp lệ, ngày thực hiện xong phải >= ngày bắt đầu thực hiện");
                    dateDenNgay.Focus();
                    this.layoutControlItem5.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
                    this.layoutControlItem5.AppearanceItemCaption.Options.UseForeColor = true;
                    return;
                }
                else
                {
                    this.layoutControlItem5.AppearanceItemCaption.Options.UseForeColor = false;
                }

                #endregion


                using (var db = new MasterDataContext())
                {
                    var obj = db.dkThiCongs.FirstOrDefault(_ => Convert.ToString(_.Id) == Convert.ToString(Id));
                    if (obj == null)
                    {
                        obj = new dkThiCong();
                        obj.NgayTao = System.DateTime.UtcNow.AddHours(7);
                        obj.Id = Guid.NewGuid();
                        obj.MaTT = 1; // Chờ duyệt

                        db.dkThiCongs.InsertOnSubmit(obj);

                        
                    }

                    obj.MaTN = TowerId;
                    obj.MaKH = (int?)glkKhachHang.EditValue;
                    obj.MaMB = (int?)glkMatBang.EditValue;
                    obj.MaPB = (int?)glkPhongBan.EditValue;

                    var objHangMuc = db.DangKyThiCong_HangMucs.FirstOrDefault(_ => _.ID == (int?)glkHangMuc.EditValue);
                    if(objHangMuc == null)
                    {
                        DialogBox.Alert("Vui lòng chọn hạng mục thi công");
                        glkHangMuc.Focus();
                        this.layoutControlItem2.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
                        this.layoutControlItem2.AppearanceItemCaption.Options.UseForeColor = true;
                        return;
                    }
                    obj.HangMucThiCong = objHangMuc.TenHangMuc;

                    obj.SuDungKhoan = chkSuDungKhoan.Checked;
                    obj.GayTiengOn = chkGayTiengOn.Checked;
                    obj.SuDungHan = chkSuDungHan.Checked;
                    obj.PhatSinhChacThai = chkPhatSinhChatThai.Checked;

                    obj.SoLuongCongNhan = spinSoLuongNhanCong.Value;

                    obj.TuNgay = dateTuNgay.DateTime;
                    obj.DenNgay = dateDenNgay.DateTime;

                    obj.GhiChu = memoNote.Text;
                    
                    // Chi tiết
                    var objChiTiet = db.dkThiCong_ChiTiets.FirstOrDefault(_ => Convert.ToString(_.DangKy_ID) == Convert.ToString(Id));
                    if (objChiTiet == null)
                    {
                        objChiTiet = new dkThiCong_ChiTiet();
                        obj.dkThiCong_ChiTiets.Add(objChiTiet);
                    }

                    objChiTiet.HangMuc_ID = (int?)glkHangMuc.EditValue;
                    objChiTiet.MaMB = (int?)glkMatBang.EditValue;

                    db.SubmitChanges();



                    #region Gửi notify đến phòng ban

                    try
                    {
                        var objItemNotify = (from dk in db.dkThiCongs
                                             join kh in db.tnKhachHangs on dk.MaKH equals kh.MaKH
                                             where Convert.ToString(dk.Id) == Convert.ToString(obj.Id)
                                             select new
                                             {
                                                 dk.Id,
                                                 TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.TenKH : kh.CtyTen,
                                                 dk.MaPB,
                                                 dk.MaTN
                                             }).ToList();

                        foreach (var item in objItemNotify)
                        {
                            var objNSBM = new
                            {
                                notification = new
                                {
                                    body = item.TenKH + " đăng ký thi công",
                                    title = "Đăng ký thi công",
                                    tag = Convert.ToString(item.Id)
                                },
                                data = new
                                {
                                    actionId = 22,
                                    body = item.TenKH + " đăng ký thi công",
                                    title = "Đăng ký thi công",
                                    item = new
                                    {
                                        content = item.TenKH + " đăng ký thi công",
                                        id = Convert.ToString(item.Id),
                                        title = "Đăng ký thi công"
                                    }
                                },
                                //to = $"/topics/group{item.MaPB}",
                                to = "/topics/group" + item.MaPB,
                                NewId = 1
                            };

                            //var post = Building.AppVime.VimeService.PostH(objNSBM, $"/Notification/SendNotifyBasic");
                            var post = Building.AppVime.VimeService.PostH(objNSBM, "/Notification/SendNotifyBasic");

                            if (post == "\"OK\"")
                            {
                                var tokens = new List<Class.dk_get_token_pb>();
                                var list_token = Library.Class.Connect.QueryConnect.QueryData<Class.dk_get_token_pb>("dk_get_token_pb",new
                                {
                                    mapb = item.MaPB
                                });

                                if (list_token.Count() > 0)
                                {
                                    tokens = list_token.ToList();

                                    foreach (var i in tokens)
                                    {
                                        var kq_notify = Library.Class.Connect.QueryConnect.QueryData<bool>("dbo.app_notification_create_dk", new
                                        {
                                            notifyid = Convert.ToString(item.Id),
                                            typeid = 22,
                                            residentid = (long)i.Id,
                                            diengiai = item.TenKH + " đăng ký thi công",
                                            tieude = "Đăng ký thi công",
                                            towerid = (byte)item.MaTN
                                        });
                                    }
                                }
                            }
                        }
                    }
                    catch (System.Exception ex) { }

                    

                    

                    #endregion


                    DialogBox.Success();

                    this.Close();
                }
                
            }
            catch(System.Exception ex)
            {
                DialogBox.Error("Lưu dữ liệu bị lỗi: " + ex.Message);
            }
        }

        

        private void itemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void glkKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue != null)
                {
                    using (var db = new MasterDataContext())
                    {
                        var obj = db.mbMatBangs.Where(_ => _.MaKH == (int?)item.EditValue);
                        if(obj.Count() >0)
                        {
                            glkMatBang.Properties.DataSource = obj;
                            glkMatBang.EditValue = obj.First();
                        }
                    }
                }
            }
            catch { }
            
        }
    }
}