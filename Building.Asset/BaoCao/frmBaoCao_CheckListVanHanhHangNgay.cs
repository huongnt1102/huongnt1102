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
using System.Data.Linq.SqlClient;
using DevExpress.XtraGrid.Views.Grid;
using System.Diagnostics;
using System.Threading;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Building.Asset.BaoCao
{
    public partial class frmBaoCao_CheckListVanHanhHangNgay : XtraForm
    {
        #region parameter private
        private MasterDataContext _db;
        private DataTable _data;
        private DateTime _tuNgay, _denNgay;
        private tbl_PhieuVanHanh_Status_Level _firstStatusLevel;
        private List<BackgroundWorker> _ltThread = new List<BackgroundWorker>();
        private bool _isStopThread = false;
        private List<ToaNhaItem> _sql;
        private string[] _s;
        private List<DataItem> _sql1, _sql2, _sql3;
        private List<PhieuVanHanhItem> _objPhieuVanHanh;
        private string[] _ltToaNha;
        #endregion

        #region class
        public class PhieuVanHanhItem
        {
            public int? NhomTaiSanID { get; set; }
            public short? MaTN { get; set; }
            public string TenNhomTaiSan { get; set; }
            public int? StatusLevelID { get; set; }
            public int? Level { get; set; }
            public string Color { get; set; }
            public int? ID { get; set; }
            public int? LoaiTaiSanID { get; set; }
            public int? TenTaiSanID { get; set; }
            public int? LoaiHeThong { get; set; }
            public bool? IsTenTaiSan { get; set; }
        }

        public class DataItem
        {
            public int? NhomTaiSanID { get; set; }
            public short? MaTN { get; set; }
            public string TenNhomTaiSan { get; set; }
            public int? StatusLevelID { get; set; }
            public int? Level { get; set; }
            public string Color { get; set; }
            public int? ID { get; set; }
        }

        public class ToaNhaItem
        {
            public int? NhomTaiSanID { get; set; }
            public short? MaTN { get; set; }
            public string TenNhomTaiSan { get; set; }
            public int? StatusLevelID { get; set; }
            public int? Level { get; set; }
            public string Color { get; set; }
            public int? ID { get; set; }
            public string TenTN { get; set; }
        }
        #endregion
        
        public frmBaoCao_CheckListVanHanhHangNgay()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            _db = new MasterDataContext();

            cmbToaNha.DataSource = Common.TowerList;
            //repcmbHeThong.DataSource = _db.tbl_NhomTaiSans.ToList();
            repcmbHeThong.DataSource = _db.tbl_DanhMuc_NhomTaiSans.ToList();

            var objKbc = new KyBaoCao();
            foreach (string str in objKbc.Source)
            {
                cbxKBC.Items.Add(str);
            }
            beiKBC.EditValue = objKbc.Source[3];
            SetDate(3);

            // khởi tạo thread
            for (int i = 0; i < 3; i++)
            {
                var thread = new BackgroundWorker();
                thread.DoWork += backroundWorker_DoWork;
                thread.RunWorkerCompleted += backgroundWorker_RunWorkerComplete;
                _ltThread.Add(thread);
            }
            LoadData();
        }

        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao
            {
                Index = index
            };
            objKbc.SetToDate();
            beiTuNgay.EditValue = objKbc.DateFrom;
            beiDenNgay.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            var strToaNha = (itemTN.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
            _ltToaNha = strToaNha.Split(',');
            var strHeThong = (itemHeThong.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',')
                .Replace(" ", "");
            var ltHeThong = strHeThong.Split(',');

            if (strToaNha != "" && strHeThong != "")
            {
                itemNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                gc.DataSource = null;
                // tạo data
                _data = new DataTable();

                gv.Columns.Clear();
                _data.Columns.Add("TenNhomTaiSan");
                // đổ gridview
                var heThong = new GridColumn();
                heThong.Name = "colHeThong";
                heThong.Caption = @"Hệ thống";
                heThong.FieldName = "TenNhomTaiSan";
                heThong.Visible = true;
                heThong.VisibleIndex = 0;
                gv.Columns.Add(heThong);
                try
                {
                    if (beiTuNgay.EditValue != null && beiDenNgay.EditValue != null)
                    {
                        _tuNgay = (DateTime) beiTuNgay.EditValue;
                        _denNgay = (DateTime) beiDenNgay.EditValue;
                        _db = new MasterDataContext();

                        _firstStatusLevel = _db.tbl_PhieuVanHanh_Status_Levels.First(_ => _.Levels == 0);

                        var listHeThong = (from p in _db.tbl_NhomTaiSans
                            where ltHeThong.Contains(p.IDDanhMucNhomTaiSan.ToString())
                            select new
                            {
                                p.ID,
                                p.TenNhomTaiSan
                            }).ToList();
                        _s = listHeThong.Select(p => p.ID.ToString()).ToArray();

                        _objPhieuVanHanh = (from p in _db.tbl_PhieuVanHanhs
                            join st in _db.tbl_PhieuVanHanh_Status_Levels on p.StatusLevelID equals st.ID into
                                statusLevel
                            from st in statusLevel.DefaultIfEmpty()
                            where SqlMethods.DateDiffDay(_tuNgay, p.TuNgay) >= 0
                                  & SqlMethods.DateDiffDay(p.TuNgay, _denNgay) >= 0
                                  & (p.IsPhieuBaoTri == null || p.IsPhieuBaoTri==false)
                            select new PhieuVanHanhItem
                            {
                                MaTN = p.MaTN,
                                StatusLevelID = p.StatusLevelID ?? _firstStatusLevel.ID,
                                Level = p.StatusLevelID != null ? st.Levels : _firstStatusLevel.Levels,
                                Color = p.StatusLevelID != null ? st.Color : _firstStatusLevel.Color,
                                ID = p.ID,
                                NhomTaiSanID = p.NhomTaiSanID,
                                LoaiTaiSanID = p.LoaiTaiSanID,
                                TenTaiSanID = p.TenTaiSanID,
                                LoaiHeThong = p.LoaiHeThong,
                                IsTenTaiSan = p.IsTenTaiSan
                            }).ToList();

                        int i = 1;

                        foreach (var thread in _ltThread)
                        {
                            thread.RunWorkerAsync(i);
                            i++;
                        }

                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        private void backroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int ht = (int)e.Argument;
            GetDataTask(ht);
        }
        private void backgroundWorker_RunWorkerComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!_isStopThread) (sender as BackgroundWorker).RunWorkerAsync(4);
            else if (!_ltThread.Any(_ => _.IsBusy)) GetData();
        }

        private void GetDataTask(int i)
        {
            using (var db = new MasterDataContext())
            {
                switch (i)
                {
                    case 1:
                        // loại tài sản
                        var s1 = _s;
                        _sql2 = (from p in _objPhieuVanHanh
                                 join lts in db.tbl_LoaiTaiSans on p.LoaiTaiSanID equals lts.ID
                                 where p.LoaiHeThong == 2
                                       & s1.Contains(lts.NhomTaiSanID.ToString())
                                 select new DataItem
                                 {
                                     NhomTaiSanID = (int)lts.NhomTaiSanID,
                                     MaTN = p.MaTN,
                                     TenNhomTaiSan = lts.tbl_NhomTaiSan.TenNhomTaiSan,
                                     StatusLevelID = p.StatusLevelID ?? _firstStatusLevel.ID,
                                     Level = p.Level,
                                     Color = p.Color,
                                     ID = p.ID
                                 }).ToList();
                        break;
                    case 2:
                        // nhóm tài sản
                        var s2 = _s;
                        _sql1 = (from p in _objPhieuVanHanh
                                 join ht in db.tbl_NhomTaiSans on p.NhomTaiSanID equals ht.ID
                                 where
                                     s2.Contains(p.NhomTaiSanID.ToString())
                                     && p.IsTenTaiSan.GetValueOrDefault() == false
                                     & p.LoaiHeThong == 1
                                 select new DataItem
                                 {
                                     NhomTaiSanID = (int)p.NhomTaiSanID,
                                     MaTN = p.MaTN,
                                     TenNhomTaiSan = ht.TenNhomTaiSan,
                                     StatusLevelID = p.StatusLevelID ?? _firstStatusLevel.ID,
                                     Level = p.Level,
                                     Color = p.Color,
                                     ID = p.ID
                                 }).ToList();
                        break;
                    case 3:
                        // tên tài sản
                        var s3 = _s;
                        _sql3 = (from p in _objPhieuVanHanh
                                 join ht in db.tbl_TenTaiSans on p.TenTaiSanID equals ht.ID
                                 where
                                     s3.Contains(ht.tbl_LoaiTaiSan.tbl_NhomTaiSan.ID.ToString())
                                     && p.IsTenTaiSan.GetValueOrDefault()
                                     & p.LoaiHeThong == 3
                                 select new DataItem
                                 {
                                     NhomTaiSanID = ht.tbl_LoaiTaiSan.tbl_NhomTaiSan.ID,
                                     MaTN = p.MaTN,
                                     TenNhomTaiSan = ht.tbl_LoaiTaiSan.tbl_NhomTaiSan.TenNhomTaiSan,
                                     StatusLevelID = p.StatusLevelID ?? _firstStatusLevel.ID,
                                     Level = p.Level,
                                     Color = p.Color,
                                     ID = p.ID
                                 }).ToList();
                        break;

                    default:
                        _isStopThread = true;
                        break;
                }
            }
        }

        private void GetData()
        {
            if (_sql1 == null) _sql1 = new List<DataItem>();
            if (_sql2 == null) _sql2 = new List<DataItem>();
            if (_sql3 == null) _sql3 = new List<DataItem>();

            var sql4 = _sql1.Concat(_sql2).Concat(_sql3);

            _sql = (from p in sql4
                join tn in _db.tnToaNhas on p.MaTN equals tn.MaTN
                where _ltToaNha.Contains(p.MaTN.ToString())
                //| strToaNha == ""
                select new ToaNhaItem
                {
                    NhomTaiSanID = p.NhomTaiSanID,
                    MaTN = p.MaTN,
                    TenNhomTaiSan = p.TenNhomTaiSan,
                    StatusLevelID = p.StatusLevelID,
                    Level = p.Level,
                    Color = p.Color,
                    TenTN = tn.TenTN,
                    ID = p.ID
                }).ToList();

            Final();
        }

        private void Final()
        {
            var ii = 1;
            // cột tòa nhà tự động cho data
            var fielNameToaNha = _sql.Select(_ => new { _.TenTN, _.MaTN }).Distinct().ToList();
            foreach (var tn in fielNameToaNha)
            {
                if (_data.Columns.Contains(tn.MaTN.ToString())) { }
                else
                {
                    _data.Columns.Add(tn.MaTN.ToString());

                    var colToaNha = new GridColumn();
                    colToaNha.Name = tn.TenTN;
                    colToaNha.Caption = tn.TenTN;
                    colToaNha.FieldName = tn.MaTN.ToString();
                    colToaNha.AppearanceHeader.TextOptions.HAlignment = (HorzAlignment)HorizontalAlignment.Center;
                    colToaNha.AppearanceCell.TextOptions.HAlignment = (HorzAlignment)HorizontalAlignment.Center;
                    colToaNha.VisibleIndex = ii;
                    gv.Columns.Add(colToaNha);
                    ii++;
                }
                if (_data.Columns.Contains(tn.MaTN + "Color")) { }
                else
                {
                    _data.Columns.Add(tn.MaTN + "Color");

                    var colColor = new GridColumn();
                    colColor.Name = tn.TenTN + "Color";
                    colColor.Caption = tn.TenTN + @"Color";
                    colColor.FieldName = tn.MaTN + "Color";
                    colColor.AppearanceHeader.TextOptions.HAlignment = (HorzAlignment)HorizontalAlignment.Center;
                    colColor.AppearanceCell.TextOptions.HAlignment = (HorzAlignment)HorizontalAlignment.Center;
                    colColor.Visible = false;
                    gv.Columns.Add(colColor);
                    ii++;
                }
                if (_data.Columns.Contains(tn.MaTN + "ID")) { }
                else
                {
                    _data.Columns.Add(tn.MaTN + "ID");

                    var colId = new GridColumn();
                    colId.Name = tn.TenTN + "ID";
                    colId.Caption = tn.TenTN + @"ID";
                    colId.FieldName = tn.MaTN + "ID";
                    colId.AppearanceHeader.TextOptions.HAlignment = (HorzAlignment)HorizontalAlignment.Center;
                    colId.AppearanceCell.TextOptions.HAlignment = (HorzAlignment)HorizontalAlignment.Center;
                    colId.Visible = false;
                    gv.Columns.Add(colId);
                    ii++;
                }
                //_data.Columns.Add(tn.MaTN + "Color");
                //_data.Columns.Add(tn.MaTN + "ID");
            }


            // đổ dữ liệu

            //foreach (var thread in _ltThread)
            //    thread.RunWorkerAsync();

            var tenHeThongs = _sql.OrderBy(_ => _.TenNhomTaiSan).Select(_ => _.TenNhomTaiSan).Distinct();
            foreach (var ht in tenHeThongs)
            {
                GetTask(ht);
                gc.DataSource = _data;
            }

            //gc.DataSource = _data;
            gv.OptionsBehavior.Editable = false;
            itemNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }

        private void GetTask(object obj)
        {
            string ht = obj.ToString();
            var r = _data.NewRow();
            r["TenNhomTaiSan"] = _sql.First(_ => _.TenNhomTaiSan == ht).TenNhomTaiSan;
            var objHeThong = _sql.Where(_ => _.TenNhomTaiSan == ht).ToList();
            var distinctHeThong = objHeThong.Distinct();

            foreach (var dl in distinctHeThong)
            {
                var max = objHeThong.Where(_ => _.TenNhomTaiSan == ht & _.MaTN == dl.MaTN)
                    .Max(_ => _.Level); // lấy ra số max
                var objMax =
                    objHeThong.First(_ => _.TenNhomTaiSan == ht & _.MaTN == dl.MaTN & _.Level == max); // lấy ra màu của max

                // count số lượng
                r[dl.MaTN.ToString()] =
                    _sql.Count(_ =>
                        _.NhomTaiSanID == dl.NhomTaiSanID & _.StatusLevelID == objMax.StatusLevelID &
                        _.MaTN == dl.MaTN) + "/" +
                    _sql.Count(_ => _.TenNhomTaiSan == ht & _.MaTN == dl.MaTN);
                // màu
                r[dl.MaTN + "Color"] = objMax.Color;
                // list id
                var listIds = _sql.Where(_ => _.MaTN == dl.MaTN & _.TenNhomTaiSan == ht);
                var listId = "";
                foreach (var i in listIds)
                {
                    listId = listId + i.ID + ",";
                }

                r[dl.MaTN + "ID"] = listId;
            }

            _data.Rows.Add(r);
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "TenNhomTaiSan") return;
            try
            {
                var nameColorid = gv.GetRowCellValue(e.RowHandle, e.Column.FieldName + "Color").ToString();
                e.Appearance.BackColor = Color.FromArgb(int.Parse(nameColorid));
            }
            catch
            {
                // ignored
            }
        }

        private void gv_DoubleClick(object sender, EventArgs e)
        {
            var ea = e as DXMouseEventArgs;
            var view = sender as GridView;
            if (ea == null) return;
            var info = view.CalcHitInfo(ea.Location);
            if (info.InRow || info.InRowCell)
            {
                if (info.Column.FieldName == "TenNhomTaiSan") return;
                var listId = gv.GetRowCellValue(info.RowHandle, info.Column.FieldName + "ID").ToString();
                using (var frm = new frmBaoCao_CheckListVanHanhHangNgay_ViewItem { Id = listId })
                {
                    frm.ShowDialog();
                }
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }
    }
}