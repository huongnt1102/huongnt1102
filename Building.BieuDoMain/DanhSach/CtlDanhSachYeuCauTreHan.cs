

using DevExpress.XtraCharts;
using Library;
using System.Linq;

namespace Building.BieuDoMain
{
    public partial class CtlDanhSachYeuCauTreHan : DevExpress.XtraEditors.XtraUserControl
    {
        private Library.MasterDataContext _db = new Library.MasterDataContext();
        public CtlDanhSachYeuCauTreHan()
        {
            InitializeComponent();
        }

        private void CtlDanhGiaSao_Load(object sender, System.EventArgs e)
        {
            if (Common.User == null) return;
            ckCbxToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            var objKbc = new KyBaoCao();
            foreach (var item in objKbc.Source) cbxKbc.Items.Add(item);
            itemKyBaoCao.EditValue = objKbc.Source[3];
            SetDate(3);
            LoadData();
        }

        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao { Index = index };
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        #region Danh sách yêu cầu trễ hạn

        public class YeuCauTreHan
        {
            public int? ID { get; set; }
            public System.DateTime? NgayYC { get; set; }
            public string MaYC { get; set; }
            public string TieuDe { get; set; }
            public string NoiDung { get; set; }
            public string TrangThai { get; set; }
            public string MatBang { get; set; }
        }

        private System.Collections.Generic.List<YeuCauTreHan> GetYeuCauTreHans(string[] ltToaNha)
        {
            return (from yc in _db.tnycYeuCaus
                    join tt in _db.tnycTrangThais on yc.MaTT equals tt.MaTT
                    join mb in _db.mbMatBangs on yc.MaMB equals mb.MaMB
                    where ltToaNha.Contains(yc.MaTN.ToString()) & yc.NgayYC != null & yc.NgayYC.Value <= System.DateTime.UtcNow.AddHours(7) & yc.MaTT != 3 & yc.MaTT != 5 & yc.MaMB != null
                    select new YeuCauTreHan
                    {
                        ID = yc.ID,
                        MaYC = yc.MaYC,
                        NgayYC = yc.NgayYC,
                        TieuDe = yc.TieuDe,
                        NoiDung = yc.NoiDung,
                        TrangThai = tt.TenTT,
                        MatBang = mb.MaSoMB
                    }).ToList();
        }

        #endregion

        private async void LoadData()
        {
            try
            {
                Library.PhanQuyenBieuDo.SaveControl(GetType().Namespace + "." + Name, "Danh sách yêu cầu trễ hạn", GetType().Namespace + ".dll");

                var strToaNha = (itemToaNha.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                string[] ltToaNha = strToaNha.Split(',');
                if (strToaNha == "") return;
                var tuNgay = (System.DateTime?) itemTuNgay.EditValue;
                var denNgay = (System.DateTime?) itemDenNgay.EditValue;

                _db = new Library.MasterDataContext();

                System.Collections.Generic.List<YeuCauTreHan> yeuCauTreHans = null;
                await System.Threading.Tasks.Task.Run(() => { yeuCauTreHans = GetYeuCauTreHans(ltToaNha); });

                gc.DataSource = yeuCauTreHans;
            }
            catch (System.Exception e)
            {
                //
            }
        }

        private void ItemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void cbxKbc_EditValueChanged(object sender, System.EventArgs e)
        {
            SetDate(((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedIndex);
        }

        private void itemView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new DichVu.YeuCau.frmManager() { MaYC = (int?)gv.GetFocusedRowCellValue("ID") }) frm.ShowDialog();
        }
    }
}
