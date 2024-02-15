using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using System.Collections.Generic;

namespace DichVu.YeuCau
{
    public partial class frmDoUuTien_ThoiGianXuLy : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmDoUuTien_ThoiGianXuLy()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }
        public class tnycDoUuTien_ThoiGianXuLyCls
        {
            public int? MaDoUuTienID { set; get; }
            public byte? MaTN { set; get; }
            public decimal? ThoiGianXuLy { set; get; }
            public decimal? ThoiGianCanhBaoToiHan { set; get; }
            public int? MauCanhBaoToiHan { set; get; }
            public int? MauCanhBaoQuaHan { set; get; }
        }
        void LoadData()
        {
            if (barDuAn.EditValue != null)
            {
                List<tnycDoUuTien_ThoiGianXuLyCls> lstThoiGian = new List<tnycDoUuTien_ThoiGianXuLyCls>();
                var objTotal = db.tnycDoUuTiens.ToList();
                foreach(var item in objTotal)
                {
                    var i = new tnycDoUuTien_ThoiGianXuLyCls();
                    i.MaDoUuTienID = item.MaDoUuTien;
                    lstThoiGian.Add(i);
                }
                var objData = db.tnycDoUuTien_ThoiGianXuLies.Where(p=>p.MaTN==(byte?)barDuAn.EditValue);
                if(objData.Count()>0)
                {
                    foreach(var item in lstThoiGian)
                    {
                        foreach(var item2 in objData)
                        {
                            if(item.MaDoUuTienID==item2.MaDoUuTienID)
                            {
                                item.MaTN = (byte?)barDuAn.EditValue;
                                item.ThoiGianXuLy = (decimal?)item2.ThoiGianXuLy;
                                item.ThoiGianCanhBaoToiHan = (decimal?)item2.ThoiGianCanhBaoToiHan;
                                item.MauCanhBaoToiHan = (int?)item2.MauCanhBaoToiHan;
                                item.MauCanhBaoQuaHan = (int?)item2.MauCanhBaoQuaHan;
                            }
                        }
                    }
                    
                }
                gcTrangThai.DataSource = lstThoiGian;
            }
           
        }

        private void frmTrangThai_Load(object sender, EventArgs e)
        {
           lkDuAn.DataSource = Common.TowerList;
            rpklkDoUuTien.DataSource = db.tnycDoUuTiens.ToList();
            LoadData();
        }
        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for(int i=0;i<grvTrangThai.RowCount;i++)
            {
                var obj = db.tnycDoUuTien_ThoiGianXuLies.FirstOrDefault(p=>p.MaTN== (byte?)barDuAn.EditValue && p.MaDoUuTienID==(int?)grvTrangThai.GetRowCellValue(i, "MaDoUuTienID"));
                if(obj!=null)
                {
                    obj.ThoiGianXuLy = (decimal?)grvTrangThai.GetRowCellValue(i, "ThoiGianXuLy");
                    obj.ThoiGianCanhBaoToiHan= (decimal?)grvTrangThai.GetRowCellValue(i, "ThoiGianCanhBaoToiHan");
                    obj.MauCanhBaoToiHan=(int?)grvTrangThai.GetRowCellValue(i, "MauCanhBaoToiHan");
                    obj.MauCanhBaoQuaHan= (int?)grvTrangThai.GetRowCellValue(i, "MauCanhBaoQuaHan");
                }
                else
                {
                    if((int?)grvTrangThai.GetRowCellValue(i, "MaDoUuTienID")!=null)
                    {
                        obj = new tnycDoUuTien_ThoiGianXuLy();
                        obj.MaTN = (byte?)barDuAn.EditValue;
                        obj.MaDoUuTienID = (int?)grvTrangThai.GetRowCellValue(i, "MaDoUuTienID");
                        obj.ThoiGianXuLy = (decimal?)grvTrangThai.GetRowCellValue(i, "ThoiGianXuLy");
                        obj.ThoiGianCanhBaoToiHan = (decimal?)grvTrangThai.GetRowCellValue(i, "ThoiGianCanhBaoToiHan");
                        obj.MauCanhBaoToiHan = (int?)grvTrangThai.GetRowCellValue(i, "MauCanhBaoToiHan");
                        obj.MauCanhBaoQuaHan = (int?)grvTrangThai.GetRowCellValue(i, "MauCanhBaoQuaHan");
                        db.tnycDoUuTien_ThoiGianXuLies.InsertOnSubmit(obj);
                    }
                }
            }
            db.SubmitChanges();

            DialogBox.Alert("Dữ liệu đã được lưu");

            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void barDuAn_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}