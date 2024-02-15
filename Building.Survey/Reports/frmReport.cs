using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
using System.Data.Linq.SqlClient;
using DevExpress.XtraGrid;
using DevExpress.Data.ODataLinq.Helpers;
using DevExpress.Utils;
using System.Threading.Tasks;
using System.Collections;
using DevExpress.Data;
using DevExpress.XtraLayout;
using DevExpress.XtraCharts;

namespace Building.Survey
{
    public partial class frmReport : DevExpress.XtraEditors.XtraForm
    {
        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;

            //gvHoaDon.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;

            itemToaNha.EditValue = Common.User.MaTN;

        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if ((int?)itemKhaoSat.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn phiếu khảo sát!!!");
                return;
            }

            using (var db = new MasterDataContext())
            {
                var question = db.svQuestions.Where(o => o.idSurvey == (int?)itemKhaoSat.EditValue).ToList();
                //var ans = question.Where(_ => _.svAnswerQuestions.Count() > 0);
                //if (db.svQuestions.Any(o => o.idSurvey == (int?)itemKhaoSat.EditValue && o.svAnswerQuestions.Count() == 0))
                //    return;
            }
                

            LoadType();
            LoadLayout(ltType);
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }


        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
                glkKhaoSat.DataSource = db.svSurveys.Where(o => o.towerID == (byte?)itemToaNha.EditValue);
        }

        List<itemData> ltType;

        private void itemKhaoSat_EditValueChanged(object sender, EventArgs e)
        {
            //loadtype();
        }

        void LoadType()
        {
            var id = (int?)itemKhaoSat.EditValue;

            if (id == null)
                return;

            using (var db = new MasterDataContext())
                ltType = db.svQuestions.Where(o => o.idSurvey == id)
                           .Select(o => new itemData { idQuestion = o.ID, answer_type = o.answer_type, question_name = o.question_name }).ToList();
        }
        LayoutControl lc;
        SeriesPoint[] s;
        void LoadLayout(List<itemData> ltData)
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    this.Controls.Remove(lc);
                    lc = new LayoutControl();
                    lc.Dock = System.Windows.Forms.DockStyle.Fill;
                    lc.AutoScroll = true;
                    lc.AutoSize = true;
                    lc.AutoSizeMode = AutoSizeMode.GrowOnly;
                    lc.AllowDrop = true;
                    this.Controls.Add(lc);

                    lc.BeginUpdate();
                    try
                    {
                        lc.Root.GroupBordersVisible = false;

                        //var label = (from i in ltData
                        //           join a in db.svAnswerQuestions on i.idQuestion equals a.idQuestion
                        //           select new LabelControlItem
                        //           {
                        //               question_name = i.question_name,
                        //               Text = a.AnswerText
                        //           }).ToList();

                        //var layout = (from item in label
                        //              select new LayoutControlItems
                        //              {
                        //                  question_name = item.question_name,
                        //                  Control = item,
                        //                  TextVisible = false,
                        //              }).ToArray();

                        //var objgroup = (from i in ltData
                        //             select new LayoutControlGroup
                        //             {
                        //                 GroupStyle = DevExpress.Utils.GroupStyle.Title,
                        //                 Text = i.question_name
                        //             }).ToList();

                        //var gr = from g in objgroup
                        //         join l in layout on g.Text equals l.question_name
                        //         select new LayoutControlGroup
                        //         {
                        //             GroupStyle = DevExpress.Utils.GroupStyle.Title,
                        //             Text = g.question_name,


                        //         }

                        foreach (var i in ltData)
                        {
                            LayoutControlGroup group = new LayoutControlGroup();
                            group.GroupStyle = DevExpress.Utils.GroupStyle.Title;
                            //group.Name = "GroupDetails";
                            group.Text = i.question_name;



                            var obj = db.svAnswerQuestions.Where(o => o.idQuestion == i.idQuestion).ToList();

                            if (i.answer_type != 1)
                                try
                                {
                                    s = (from item in obj.GroupBy(o => o.idAnswer)
                                         select new SeriesPoint(db.svAnswers.FirstOrDefault(o => o.ID == item.Key).answer_name, obj.Count())).Distinct().ToArray();
                                }
                                catch { }
                                

                            LayoutControlItem layout;
                            ChartControl chart;
                            Series series;
                            Size size;

                            switch (i.answer_type)
                            {
                                case 1:

                                    var label = (from item in obj
                                                 select new LabelControl
                                                 {
                                                     Text = item.AnswerText
                                                 }).ToList();

                                    var layouts = (from item in label
                                                   select new LayoutControlItem
                                                   {
                                                       Control = item,
                                                       TextVisible = false,
                                                   }).ToArray();

                                    group.AddRange(layouts);
                                    break;
                                case 2:

                                    layout = group.AddItem();
                                    layout.SizeConstraintsType = SizeConstraintsType.Custom;
                                    //layout.Height = 175;
                                    size = new Size(default, 250);
                                    layout.MaxSize = size;
                                    layout.MinSize = size;

                                    chart = new ChartControl();
                                    series = new Series("", ViewType.Bar);
                                    series.Label.TextPattern = "{A}: {V:n0}";
                                    if (s != null)
                                        series.Points.AddRange(s);
                                    chart.Series.Add(series);

                                    ((SideBySideBarSeriesView)series.View).ColorEach = true;

                                    layout.Control = chart;
                                    break;
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                default:
                                    layout = group.AddItem();
                                    layout.SizeConstraintsType = SizeConstraintsType.Custom;
                                    //layout.Height = 175;
                                    size = new Size(default, 250);
                                    layout.MaxSize = size;
                                    layout.MinSize = size;

                                    chart = new ChartControl();
                                    series = new Series("", ViewType.Pie);
                                    series.Points.AddRange(s);
                                    chart.Series.Add(series);

                                    //((SideBySideBarSeriesView)series.View).ColorEach = true;
                                    series.Label.TextPattern = "{A}: {VP:p0}";
                                    ((PieSeriesLabel)series.Label).Position = PieSeriesLabelPosition.TwoColumns;
                                    ((PieSeriesLabel)series.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

                                    //PieSeriesView myView = (PieSeriesView)series.View;

                                    //myView.Titles.Add(new SeriesTitle());
                                    //myView.Titles[0].Text = series.Name;

                                    //myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Value_1,
                                    //    DataFilterCondition.Equal, 9));
                                    //myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                                    //    DataFilterCondition.Equal, "Others"));
                                    //myView.ExplodeMode = PieExplodeMode.UseFilters;
                                    //myView.ExplodedDistancePercentage = 30;
                                    //myView.RuntimeExploding = true;
                                    //myView.HeightToWidthRatio = 0.75;
                                    layout.Control = chart;
                                    break;
                            }


                            lc.Root.Add(group);
                        }
                    }
                    finally
                    {
                        lc.EndUpdate();
                    }
                }
            }
            catch (System.Exception ex) { }
            
        }

        private class itemData
        {
            public int? idQuestion { get; set; }
            public int? answer_type { get; set; }
            public string question_name { get; set; }
        }

        private class LayoutControlItems : LayoutControlItem
        {
            public int? idQuestion { get; set; }
            public LabelControl label { get; set; }
        }

        private class LayoutControlGroupItems : LayoutControlGroup
        {
            public List<LayoutControlItems> layout {get; set;}
        }

    }
}