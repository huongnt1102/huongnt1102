using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.MatBang
{
    public partial class frmBCTHDienTichChoThue : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public frmBCTHDienTichChoThue()
        {
            InitializeComponent();
            this.a1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.a2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.a3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.a4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.a5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.a6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.a7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.a8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.a9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.a10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.a11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.a12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.b1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.b2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.b3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.b4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.b5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.b6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.b7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.b8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.b9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.b10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.b11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.b12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

            this.h1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.h2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.h3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.h4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.h5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.h6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.h7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.h8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.h9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.h10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.h11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.h12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.h13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.h14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.h15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.h16.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.h17.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.h18.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.h19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.h20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.h21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.h22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.h23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.h24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

            this.c1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c16.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c17.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c18.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

            this.c1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c16.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c17.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c18.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.c24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

            this.k1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.k2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.k3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.k4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.k5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.k6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.k7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.k8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.k9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.k10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.k11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.k12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.k13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.k14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.k15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.k16.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.k17.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.k18.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.k19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.k20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.k21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.k22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.k23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.k24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

            this.z1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.z2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.z3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.z4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.z5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.z6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.z7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.z8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.z9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.z10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.z11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.z12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.z13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.z14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.z15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.z16.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.z17.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.z18.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.z19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.z20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.z21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.z22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.z23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.z24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

            this.d1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.d2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.d3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.d4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.d5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.d6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.d7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.d8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.d9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.d10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.d11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.d12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.d13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.d14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.d15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.d16.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.d17.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.d18.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.d19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.d20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.d21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.d22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.d23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.d24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

            this.f1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.f2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.f3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.f4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.f5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.f6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.f7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.f8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.f9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.f10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.f11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.f12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.f13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.f14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.f15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.f16.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.f17.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.f18.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.f19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.f20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.f21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.f22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.f23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.f24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

            this.v1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.v2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.v3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.v4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.v5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.v6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.v7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.v8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.v9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.v10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.v11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.v12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.v13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.v14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.v15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.v16.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.v17.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.v18.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.v19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.v20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.v21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.v22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.v23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.v24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

            this.y1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.y2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.y3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.y4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.y5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.y6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.y7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.y8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.y9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.y10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.y11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.y12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.y13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.y14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.y15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.y16.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.y17.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.y18.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.y19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.y20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.y21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.y22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.y23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.y24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

            this.t1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.t2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.t3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.t4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.t5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.t6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.t7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.t8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.t9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.t10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.t11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.t12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.t13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.t14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.t15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.t16.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.t17.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.t18.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.t19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.t20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.t21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.t22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.t23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.t24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

        }

        private void frmBCTHDienTichChoThue_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbbKyBC.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[34];
            SetDate(35);
           
        }
        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcMatBang);
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void cbbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }
        void LoadData()
        {
            var wait = new WaitDialogForm("Ðang xử lý. Vui lòng chờ...", "LandSoft Building");
            var tungay = (DateTime?)itemTuNgay.EditValue;
            var denngay = (DateTime?)itemDenNgay.EditValue;
            var den1 = new DateTime(tungay.Value.Year, 2, 1).AddDays(-1);
            var den2 = new DateTime(tungay.Value.Year, 3, 1).AddDays(-1);
            var den3 = new DateTime(tungay.Value.Year, 4, 1).AddDays(-1);
            var den4 = new DateTime(tungay.Value.Year, 5, 1).AddDays(-1);
            var den5 = new DateTime(tungay.Value.Year, 6, 1).AddDays(-1);
            var den6 = new DateTime(tungay.Value.Year, 7, 1).AddDays(-1);
            var den7 = new DateTime(tungay.Value.Year, 8, 1).AddDays(-1);
            var den8 = new DateTime(tungay.Value.Year, 9, 1).AddDays(-1);
            var den9 = new DateTime(tungay.Value.Year, 10, 1).AddDays(-1);
            var den10 = new DateTime(tungay.Value.Year, 11, 1).AddDays(-1);
            var den11 = new DateTime(tungay.Value.Year, 12, 1).AddDays(-1);
            var den12 = new DateTime(tungay.Value.Year, 12, DateTime.DaysInMonth(tungay.Value.Year, 12));

            var D1 = new DateTime(denngay.Value.Year, 2, 1).AddDays(-1);
            var D2 = new DateTime(denngay.Value.Year, 3, 1).AddDays(-1);
            var D3 = new DateTime(denngay.Value.Year, 4, 1).AddDays(-1);
            var D4 = new DateTime(denngay.Value.Year, 5, 1).AddDays(-1);
            var D5 = new DateTime(denngay.Value.Year, 6, 1).AddDays(-1);
            var D6 = new DateTime(denngay.Value.Year, 7, 1).AddDays(-1);
            var D7 = new DateTime(denngay.Value.Year, 8, 1).AddDays(-1);
            var D8 = new DateTime(denngay.Value.Year, 9, 1).AddDays(-1);
            var D9 = new DateTime(denngay.Value.Year, 10, 1).AddDays(-1);
            var D10 = new DateTime(denngay.Value.Year, 11, 1).AddDays(-1);
            var D11 = new DateTime(denngay.Value.Year, 12, 1).AddDays(-1);
            var D12 = new DateTime(denngay.Value.Year, 12, DateTime.DaysInMonth(denngay.Value.Year, 12));

            gridBand75.Caption = "Dự án     " + db.tnToaNhas.SingleOrDefault(p=>p.MaTN == (byte?)itemToaNha.EditValue).TenTN;
            gridBand81.Caption = "Từ " + tungay.ToString() + " đến " + denngay.ToString();
            ban1.Caption = tungay.Value.Year.ToString();
            ban2.Caption = denngay.Value.Year.ToString();

            try
            {
                var thang1 = from mb in db.mbMatBangs
                             join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                             join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                             where
                              mb.MaTN == (byte?)itemToaNha.EditValue
                             select new
                             {
                                 Tang = tl.TenTL,
                                 TenKH =  db.tnKhachHangs.SingleOrDefault(p=>p.MaKH == 
                                          db.ctHopDongs.FirstOrDefault(o=>o.ID == 
                                          db.ctChiTiets.OrderByDescending(u => u.ID).FirstOrDefault(u=>u.MaMB == mb.MaMB && SqlMethods.DateDiffDay(u.DenNgay, denngay) >= 0).MaHDCT).MaKH).TenKH != null
                                          ? db.tnKhachHangs.SingleOrDefault(p => p.MaKH ==
                                          db.ctHopDongs.FirstOrDefault(o => o.ID ==
                                          db.ctChiTiets.OrderByDescending(u => u.ID).FirstOrDefault(u => u.MaMB == mb.MaMB && SqlMethods.DateDiffDay(u.DenNgay, denngay) >= 0).MaHDCT).MaKH).TenKH
                                          : mb.SoNha.ToLower() == "hành lang" ? "Hành lang" : mb.SoNha.ToLower() == "kho" ? "Kho" : "Trống",
                                
                                 TongDienTich = db.mbMatBangs.Where(p=>p.MaTL == tl.MaTL).Sum(x=>x.DienTich) + " - " + kn.TenKN,
                                 DienTich = mb.DienTich != 0 ? mb.DienTich : null,
                                 Thang1 = db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                            && SqlMethods.DateDiffDay(p.TuNgay, den1) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, den1) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich != null ? db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                               && SqlMethods.DateDiffDay(p.TuNgay, den1) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, den1) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich : null,
                                 Thang2 = db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                            && SqlMethods.DateDiffDay(p.TuNgay, den2) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, den2) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich != null ? db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                             && SqlMethods.DateDiffDay(p.TuNgay, den2) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, den2) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich : null,
                                 Thang3 = db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                            && SqlMethods.DateDiffDay(p.TuNgay, den3) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, den3) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich != null ? db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                             && SqlMethods.DateDiffDay(p.TuNgay, den3) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, den3) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich : null,
                                 Thang4 = db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                             && SqlMethods.DateDiffDay(p.TuNgay, den4) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, den4) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich != null ? db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                           && SqlMethods.DateDiffDay(p.TuNgay, den4) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, den4) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich : null,
                                 Thang5 = db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                              && SqlMethods.DateDiffDay(p.TuNgay, den5) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, den5) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich != null ? db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                            && SqlMethods.DateDiffDay(p.TuNgay, den5) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, den5) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich : null,
                                 Thang6 = db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                               && SqlMethods.DateDiffDay(p.TuNgay, den6) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, den6) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich != null ? db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                              && SqlMethods.DateDiffDay(p.TuNgay, den6) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, den6) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich : null,
                                 Thang7 = db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                              && SqlMethods.DateDiffDay(p.TuNgay, den7) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, den7) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich != null ? db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                              && SqlMethods.DateDiffDay(p.TuNgay, den7) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, den7) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich : null,
                                 Thang8 = db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                              && SqlMethods.DateDiffDay(p.TuNgay, den8) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, den8) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich != null ? db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                              && SqlMethods.DateDiffDay(p.TuNgay, den8) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, den8) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich : null,
                                 Thang9 = db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                             && SqlMethods.DateDiffDay(p.TuNgay, den9) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, den9) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich != null ? db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                             && SqlMethods.DateDiffDay(p.TuNgay, den9) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, den9) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich : null,
                                 Thang10 = db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                            && SqlMethods.DateDiffDay(p.TuNgay, den10) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, den10) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich != null ? db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                             && SqlMethods.DateDiffDay(p.TuNgay, den10) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, den10) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich : null,
                                 Thang11 = db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                             && SqlMethods.DateDiffDay(p.TuNgay, den11) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, den11) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich != null ? db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                           && SqlMethods.DateDiffDay(p.TuNgay, den11) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, den11) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich : null,
                                 Thang12 = db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                             && SqlMethods.DateDiffDay(p.TuNgay, den12) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, den12) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich != null ? db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                             && SqlMethods.DateDiffDay(p.TuNgay, den12) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, den12) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich : null,
                                 ViTri = mb.MaSoMB,
                                 t1 = db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                             && SqlMethods.DateDiffDay(p.TuNgay, D1) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, D1) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich != null ? db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                            && SqlMethods.DateDiffDay(p.TuNgay, D1) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, D1) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich : null,
                                 t2 = db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                             && SqlMethods.DateDiffDay(p.TuNgay, D2) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, D2) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich != null ? db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                            && SqlMethods.DateDiffDay(p.TuNgay, D2) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, D2) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich : null,
                                 t3 = db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                             && SqlMethods.DateDiffDay(p.TuNgay, D3) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, D3) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich != null ? db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                               && SqlMethods.DateDiffDay(p.TuNgay, D3) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, D3) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich : null,
                                 t4 = db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                              && SqlMethods.DateDiffDay(p.TuNgay, D4) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, D4) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich != null ? db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                              && SqlMethods.DateDiffDay(p.TuNgay, D4) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, D4) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich : null,
                                 t5 = db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                              && SqlMethods.DateDiffDay(p.TuNgay, D5) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, D5) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich != null ? db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                              && SqlMethods.DateDiffDay(p.TuNgay, D5) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, D5) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich : null,
                                 t6 = db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                            && SqlMethods.DateDiffDay(p.TuNgay, D6) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, D6) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich != null ? db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                               && SqlMethods.DateDiffDay(p.TuNgay, D6) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, D6) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich : null,
                                 t7 = db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                              && SqlMethods.DateDiffDay(p.TuNgay, D7) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, D7) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich != null ? db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                               && SqlMethods.DateDiffDay(p.TuNgay, D7) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, D7) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich : null,
                                 t8 = db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                          && SqlMethods.DateDiffDay(p.TuNgay, D8) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, D8) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich != null ? db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                              && SqlMethods.DateDiffDay(p.TuNgay, D8) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, D8) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich : null,
                                 t9 = db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                              && SqlMethods.DateDiffDay(p.TuNgay, D9) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, D9) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich != null ? db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                               && SqlMethods.DateDiffDay(p.TuNgay, D9) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, D9) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich : null,
                                 t10 = db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, D10) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, D10) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich != null ? db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                                  && SqlMethods.DateDiffDay(p.TuNgay, D10) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, D10) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich : null,
                                 t11 = db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                               && SqlMethods.DateDiffDay(p.TuNgay, D11) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, D11) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich != null ? db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, D11) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, D11) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich : null,
                                 t12 = db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, D12) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, D12) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich != null ? db.ctChiTiets.SingleOrDefault(p => p.MaMB == mb.MaMB
                                               && SqlMethods.DateDiffDay(p.TuNgay, D12) >= 0
                                            && SqlMethods.DateDiffDay(p.TuNgay, D12) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                           ).DienTich : null,
                             };
           
           
            var Tong = (decimal?)thang1.Sum(p => p.DienTich) != null ? (decimal?)thang1.Sum(p => p.DienTich) : 0;
            var HanhLang = (decimal?)thang1.Where(p => p.TenKH.ToLower() == "hành lang").Sum(p => p.DienTich) != null ? (decimal?)thang1.Where(p => p.TenKH.ToLower() == "hành lang").Sum(p => p.DienTich) : 0;
            var B =  Tong - HanhLang;
            var Kho = (decimal?)thang1.Where(p => p.TenKH.ToLower() == "kho").Sum(p => p.DienTich) != null ? (decimal?)thang1.Where(p => p.TenKH.ToLower() == "kho").Sum(p => p.DienTich) : 0;
            var T1 = (decimal?)thang1.Sum(p => p.Thang1) != null ? (decimal?)thang1.Sum(p => p.Thang1) : 0;
            var T2 = (decimal?)thang1.Sum(p => p.Thang2) != null ? (decimal?)thang1.Sum(p => p.Thang2) : 0;
            var T3 = (decimal?)thang1.Sum(p => p.Thang3) != null ? (decimal?)thang1.Sum(p => p.Thang3) : 0;
            var T4 = (decimal?)thang1.Sum(p => p.Thang4) != null ? (decimal?)thang1.Sum(p => p.Thang4) : 0;
            var T5 = (decimal?)thang1.Sum(p => p.Thang5) != null ? (decimal?)thang1.Sum(p => p.Thang5) : 0;
            var T6 = (decimal?)thang1.Sum(p => p.Thang6) != null ? (decimal?)thang1.Sum(p => p.Thang6) : 0;
            var T7 = (decimal?)thang1.Sum(p => p.Thang7) != null ? (decimal?)thang1.Sum(p => p.Thang7) : 0;
            var T8 = (decimal?)thang1.Sum(p => p.Thang8) != null ? (decimal?)thang1.Sum(p => p.Thang8) : 0;
            var T9 = (decimal?)thang1.Sum(p => p.Thang9) != null ? (decimal?)thang1.Sum(p => p.Thang9) : 0;
            var T10 = (decimal?)thang1.Sum(p => p.Thang10) != null ? (decimal?)thang1.Sum(p => p.Thang10) : 0;
            var T11 = (decimal?)thang1.Sum(p => p.Thang11) != null ? (decimal?)thang1.Sum(p => p.Thang11) : 0;
            var T12 = (decimal?)thang1.Sum(p => p.Thang12) != null ? (decimal?)thang1.Sum(p => p.Thang12) : 0;
            var T13 = (decimal?)thang1.Sum(p => p.t1) != null ? (decimal?)thang1.Sum(p => p.t1) : 0;
            var T14 = (decimal?)thang1.Sum(p => p.t2) != null ? (decimal?)thang1.Sum(p => p.t2) : 0;
            var T15 = (decimal?)thang1.Sum(p => p.t3) != null ? (decimal?)thang1.Sum(p => p.t3) : 0;
            var T16 = (decimal?)thang1.Sum(p => p.t4) != null ? (decimal?)thang1.Sum(p => p.t4) : 0;
            var T17 = (decimal?)thang1.Sum(p => p.t5) != null ? (decimal?)thang1.Sum(p => p.t5) : 0;
            var T18 = (decimal?)thang1.Sum(p => p.t6) != null ? (decimal?)thang1.Sum(p => p.t6) : 0;
            var T19 = (decimal?)thang1.Sum(p => p.t7) != null ? (decimal?)thang1.Sum(p => p.t7) : 0;
            var T20 = (decimal?)thang1.Sum(p => p.t8) != null ? (decimal?)thang1.Sum(p => p.t8) : 0;
            var T21 = (decimal?)thang1.Sum(p => p.t9) != null ? (decimal?)thang1.Sum(p => p.t9) : 0;
            var T22 = (decimal?)thang1.Sum(p => p.t10) != null ? (decimal?)thang1.Sum(p => p.t10) : 0;
            var T23 = (decimal?)thang1.Sum(p => p.t11) != null ? (decimal?)thang1.Sum(p => p.t11) : 0;
            var T24 = (decimal?)thang1.Sum(p => p.t12) != null ? (decimal?)thang1.Sum(p => p.t12) : 0;

            a1.Caption = String.Format( "{0:n2}", Tong);
            a2.Caption = String.Format("{0:n2}", Tong);
            a3.Caption = String.Format("{0:n2}", Tong);
            a4.Caption = String.Format("{0:n2}", Tong);
            a5.Caption = String.Format("{0:n2}", Tong);
            a6.Caption = String.Format("{0:n2}", Tong);
            a7.Caption = String.Format("{0:n2}", Tong);
            a8.Caption = String.Format("{0:n2}", Tong);
            a9.Caption = String.Format("{0:n2}", Tong);
            a10.Caption = String.Format("{0:n2}", Tong);
            a11.Caption = String.Format("{0:n2}", Tong);
            a12.Caption = String.Format("{0:n2}", Tong);
            b1.Caption = String.Format("{0:n2}", Tong);
            b2.Caption = String.Format("{0:n2}", Tong);
            b3.Caption = String.Format("{0:n2}", Tong);
            b4.Caption = String.Format("{0:n2}", Tong);
            b5.Caption = String.Format("{0:n2}", Tong);
            b6.Caption = String.Format("{0:n2}", Tong);
            b7.Caption = String.Format("{0:n2}", Tong);
            b8.Caption = String.Format("{0:n2}", Tong);
            b9.Caption = String.Format("{0:n2}", Tong);
            b10.Caption = String.Format("{0:n2}", Tong);
            b11.Caption = String.Format("{0:n2}", Tong);
            b12.Caption = String.Format("{0:n2}", Tong);

            h1.Caption = String.Format("{0:n2}", HanhLang);
            h2.Caption = String.Format("{0:n2}", HanhLang);
            h3.Caption = String.Format("{0:n2}", HanhLang);
            h4.Caption = String.Format("{0:n2}", HanhLang);
            h5.Caption = String.Format("{0:n2}", HanhLang);
            h6.Caption = String.Format("{0:n2}", HanhLang);
            h7.Caption = String.Format("{0:n2}", HanhLang);
            h8.Caption = String.Format("{0:n2}", HanhLang);
            h9.Caption = String.Format("{0:n2}", HanhLang);
            h10.Caption = String.Format("{0:n2}", HanhLang);
            h11.Caption = String.Format("{0:n2}", HanhLang);
            h12.Caption = String.Format("{0:n2}", HanhLang);
            h13.Caption = String.Format("{0:n2}", HanhLang);
            h14.Caption = String.Format("{0:n2}", HanhLang);
            h15.Caption = String.Format("{0:n2}", HanhLang);
            h16.Caption = String.Format("{0:n2}", HanhLang);
            h17.Caption = String.Format("{0:n2}", HanhLang);
            h18.Caption = String.Format("{0:n2}", HanhLang);
            h19.Caption = String.Format("{0:n2}", HanhLang);
            h20.Caption = String.Format("{0:n2}", HanhLang);
            h21.Caption = String.Format("{0:n2}", HanhLang);
            h22.Caption = String.Format("{0:n2}", HanhLang);
            h23.Caption = String.Format("{0:n2}", HanhLang);
            h24.Caption = String.Format("{0:n2}", HanhLang);

            c1.Caption = String.Format("{0:n2}", B);
            c2.Caption = String.Format("{0:n2}", B);
            c3.Caption = String.Format("{0:n2}", B);
            c4.Caption = String.Format("{0:n2}", B);
            c5.Caption = String.Format("{0:n2}", B);
            c6.Caption = String.Format("{0:n2}", B);
            c7.Caption = String.Format("{0:n2}", B);
            c8.Caption = String.Format("{0:n2}", B);
            c9.Caption = String.Format("{0:n2}", B);
            c10.Caption = String.Format("{0:n2}", B);
            c11.Caption = String.Format("{0:n2}", B);
            c12.Caption = String.Format("{0:n2}", B);
            c13.Caption = String.Format("{0:n2}", B);
            c14.Caption = String.Format("{0:n2}", B);
            c15.Caption = String.Format("{0:n2}", B);
            c16.Caption = String.Format("{0:n2}", B);
            c17.Caption = String.Format("{0:n2}", B);
            c18.Caption = String.Format("{0:n2}", B);
            c19.Caption = String.Format("{0:n2}", B);
            c20.Caption = String.Format("{0:n2}", B);
            c21.Caption = String.Format("{0:n2}", B);
            c22.Caption = String.Format("{0:n2}", B);
            c23.Caption = String.Format("{0:n2}", B);
            c24.Caption = String.Format("{0:n2}", B);

            k1.Caption = String.Format("{0:n2}", B - Kho);
            k2.Caption = String.Format("{0:n2}", B - Kho);
            k3.Caption = String.Format("{0:n2}", B - Kho);
            k4.Caption = String.Format("{0:n2}", B - Kho);
            k5.Caption = String.Format("{0:n2}", B - Kho);
            k6.Caption = String.Format("{0:n2}", B - Kho);
            k7.Caption = String.Format("{0:n2}", B - Kho);
            k8.Caption = String.Format("{0:n2}", B - Kho);
            k9.Caption = String.Format("{0:n2}", B - Kho);
            k10.Caption = String.Format("{0:n2}", B - Kho);
            k11.Caption = String.Format("{0:n2}", B - Kho);
            k12.Caption = String.Format("{0:n2}", B - Kho);
            k13.Caption = String.Format("{0:n2}", B - Kho);
            k14.Caption = String.Format("{0:n2}", B - Kho);
            k15.Caption = String.Format("{0:n2}", B - Kho);
            k16.Caption = String.Format("{0:n2}", B - Kho);
            k17.Caption = String.Format("{0:n2}", B - Kho);
            k18.Caption = String.Format("{0:n2}", B - Kho);
            k19.Caption = String.Format("{0:n2}", B - Kho);
            k20.Caption = String.Format("{0:n2}", B - Kho);
            k21.Caption = String.Format("{0:n2}", B - Kho);
            k22.Caption = String.Format("{0:n2}", B - Kho);
            k23.Caption = String.Format("{0:n2}", B - Kho);
            k24.Caption = String.Format("{0:n2}", B - Kho);

            z1.Caption = String.Format("{0:n2}", T1);
            z2.Caption = String.Format("{0:n2}", T2);
            z3.Caption = String.Format("{0:n2}", T3);
            z4.Caption = String.Format("{0:n2}", T4);
            z5.Caption = String.Format("{0:n2}", T5);
            z6.Caption = String.Format("{0:n2}", T6);
            z7.Caption = String.Format("{0:n2}", T7);
            z8.Caption = String.Format("{0:n2}", T8);
            z9.Caption = String.Format("{0:n2}", T9);
            z10.Caption = String.Format("{0:n2}", T10);
            z11.Caption = String.Format("{0:n2}", T11);
            z12.Caption = String.Format("{0:n2}", T12);
            z13.Caption = String.Format("{0:n2}", T13);
            z14.Caption = String.Format("{0:n2}", T14);
            z15.Caption = String.Format("{0:n2}", T15);
            z16.Caption = String.Format("{0:n2}", T16);
            z17.Caption = String.Format("{0:n2}", T17);
            z18.Caption = String.Format("{0:n2}", T18);
            z19.Caption = String.Format("{0:n2}", T19);
            z20.Caption = String.Format("{0:n2}", T20);
            z21.Caption = String.Format("{0:n2}", T21);
            z22.Caption = String.Format("{0:n2}", T22);
            z23.Caption = String.Format("{0:n2}", T23);
            z24.Caption = String.Format("{0:n2}", T24);

            f1.Caption = String.Format("{0:n2}", Kho);
            f2.Caption = String.Format("{0:n2}", Kho);
            f3.Caption = String.Format("{0:n2}", Kho);
            f4.Caption = String.Format("{0:n2}", Kho);
            f5.Caption = String.Format("{0:n2}", Kho);
            f6.Caption = String.Format("{0:n2}", Kho);
            f7.Caption = String.Format("{0:n2}", Kho);
            f8.Caption = String.Format("{0:n2}", Kho);
            f9.Caption = String.Format("{0:n2}", Kho);
            f10.Caption = String.Format("{0:n2}", Kho);
            f11.Caption = String.Format("{0:n2}", Kho);
            f12.Caption = String.Format("{0:n2}", Kho);
            f13.Caption = String.Format("{0:n2}", Kho);
            f14.Caption = String.Format("{0:n2}", Kho);
            f15.Caption = String.Format("{0:n2}", Kho);
            f16.Caption = String.Format("{0:n2}", Kho);
            f17.Caption = String.Format("{0:n2}", Kho);
            f18.Caption = String.Format("{0:n2}", Kho);
            f19.Caption = String.Format("{0:n2}", Kho);
            f20.Caption = String.Format("{0:n2}", Kho);
            f21.Caption = String.Format("{0:n2}", Kho);
            f22.Caption = String.Format("{0:n2}", Kho);
            f23.Caption = String.Format("{0:n2}", Kho);
            f24.Caption = String.Format("{0:n2}", Kho);

            d1.Caption = String.Format("{0:n2}", T1 - Kho < 0 ? 0 : T1 - Kho);
            d2.Caption = String.Format("{0:n2}", T2 - Kho < 0 ? 0 : T2 - Kho);
            d3.Caption = String.Format("{0:n2}", T3 - Kho < 0 ? 0 : T3 - Kho);
            d4.Caption = String.Format("{0:n2}", T4 - Kho < 0 ? 0 : T4 - Kho);
            d5.Caption = String.Format("{0:n2}", T5 - Kho < 0 ? 0 : T5 - Kho);
            d6.Caption = String.Format("{0:n2}", T6 - Kho < 0 ? 0 : T6 - Kho);
            d7.Caption = String.Format("{0:n2}", T7 - Kho < 0 ? 0 : T7 - Kho);
            d8.Caption = String.Format("{0:n2}", T8 - Kho < 0 ? 0 : T8 - Kho);
            d9.Caption = String.Format("{0:n2}", T9 - Kho < 0 ? 0 : T9 - Kho);
            d10.Caption = String.Format("{0:n2}", T10 - Kho < 0 ? 0 : T10 - Kho);
            d11.Caption = String.Format("{0:n2}", T11 - Kho < 0 ? 0 : T11 - Kho);
            d12.Caption = String.Format("{0:n2}", T12 - Kho < 0 ? 0 : T12 - Kho);
            d13.Caption = String.Format("{0:n2}", T13 - Kho < 0 ? 0 : T13 - Kho);
            d14.Caption = String.Format("{0:n2}", T14 - Kho < 0 ? 0 : T14 - Kho);
            d15.Caption = String.Format("{0:n2}", T15 - Kho < 0 ? 0 : T15 - Kho);
            d16.Caption = String.Format("{0:n2}", T16 - Kho < 0 ? 0 : T16 - Kho);
            d17.Caption = String.Format("{0:n2}", T17 - Kho < 0 ? 0 : T17 - Kho);
            d18.Caption = String.Format("{0:n2}", T18 - Kho < 0 ? 0 : T18 - Kho);
            d19.Caption = String.Format("{0:n2}", T19 - Kho < 0 ? 0 : T19 - Kho);
            d20.Caption = String.Format("{0:n2}", T20 - Kho < 0 ? 0 : T20 - Kho);
            d21.Caption = String.Format("{0:n2}", T21 - Kho < 0 ? 0 : T21 - Kho);
            d22.Caption = String.Format("{0:n2}", T22 - Kho < 0 ? 0 : T22 - Kho);
            d23.Caption = String.Format("{0:n2}", T23 - Kho < 0 ? 0 : T23 - Kho);
            d24.Caption = String.Format("{0:n2}", T24 - Kho < 0 ? 0 : T24 - Kho);

            v1.Caption = String.Format("{0:n2}", Tong - (HanhLang + T1) < 0 ? 0 : Tong - (HanhLang + T1));
            v2.Caption = String.Format("{0:n2}", Tong - (HanhLang + T2) < 0 ? 0 : Tong - (HanhLang + T2));
            v3.Caption = String.Format("{0:n2}", Tong - (HanhLang + T3) < 0 ? 0 : Tong - (HanhLang + T3));
            v4.Caption = String.Format("{0:n2}", Tong - (HanhLang + T4) < 0 ? 0 : Tong - (HanhLang + T4));
            v5.Caption = String.Format("{0:n2}", Tong - (HanhLang + T5) < 0 ? 0 : Tong - (HanhLang + T5));
            v6.Caption = String.Format("{0:n2}", Tong - (HanhLang + T6) < 0 ? 0 : Tong - (HanhLang + T6));
            v7.Caption = String.Format("{0:n2}", Tong - (HanhLang + T7) < 0 ? 0 : Tong - (HanhLang + T7));
            v8.Caption = String.Format("{0:n2}", Tong - (HanhLang + T8) < 0 ? 0 : Tong - (HanhLang + T8));
            v9.Caption = String.Format("{0:n2}", Tong - (HanhLang + T9) < 0 ? 0 : Tong - (HanhLang + T9));
            v10.Caption = String.Format("{0:n2}", Tong - (HanhLang + T10) < 0 ? 0 : Tong - (HanhLang + T10));
            v11.Caption = String.Format("{0:n2}", Tong - (HanhLang + T11) < 0 ? 0 : Tong - (HanhLang + T11));
            v12.Caption = String.Format("{0:n2}", Tong - (HanhLang + T12) < 0 ? 0 : Tong - (HanhLang + T12));
            v13.Caption = String.Format("{0:n2}", Tong - (HanhLang + T12) < 0 ? 0 : Tong - (HanhLang + T13));
            v14.Caption = String.Format("{0:n2}", Tong - (HanhLang + T14) < 0 ? 0 : Tong - (HanhLang + T14));
            v15.Caption = String.Format("{0:n2}", Tong - (HanhLang + T15) < 0 ? 0 : Tong - (HanhLang + T15));
            v16.Caption = String.Format("{0:n2}", Tong - (HanhLang + T16) < 0 ? 0 : Tong - (HanhLang + T16));
            v17.Caption = String.Format("{0:n2}", Tong - (HanhLang + T17) < 0 ? 0 : Tong - (HanhLang + T17));
            v18.Caption = String.Format("{0:n2}", Tong - (HanhLang + T18) < 0 ? 0 : Tong - (HanhLang + T18));
            v19.Caption = String.Format("{0:n2}", Tong - (HanhLang + T19) < 0 ? 0 : Tong - (HanhLang + T19));
            v20.Caption = String.Format("{0:n2}", Tong - (HanhLang + T20) < 0 ? 0 : Tong - (HanhLang + T20));
            v21.Caption = String.Format("{0:n2}", Tong - (HanhLang + T21) < 0 ? 0 : Tong - (HanhLang + T21));
            v22.Caption = String.Format("{0:n2}", Tong - (HanhLang + T22) < 0 ? 0 : Tong - (HanhLang + T22));
            v23.Caption = String.Format("{0:n2}", Tong - (HanhLang + T23) < 0 ? 0 : Tong - (HanhLang + T23));
            v24.Caption = String.Format("{0:n2}", Tong - (HanhLang + T24) < 0 ? 0 : Tong - (HanhLang + T24));

            y1.Caption = String.Format("{0:p2}", (T1 - Kho) != 0 && (B - Kho) != 0 ? (T1 - Kho) / (B - Kho) : 0);
            y2.Caption = String.Format("{0:p2}", (T2 - Kho) != 0 && (B - Kho) != 0 ? (T2 - Kho) / (B - Kho) : 0);
            y3.Caption = String.Format("{0:p2}", (T3 - Kho) != 0 && (B - Kho) != 0 ? (T3 - Kho) / (B - Kho) : 0);
            y4.Caption = String.Format("{0:p2}", (T4 - Kho) != 0 && (B - Kho) != 0 ? (T4 - Kho) / (B - Kho) : 0);
            y5.Caption = String.Format("{0:p2}", (T5 - Kho) != 0 && (B - Kho) != 0 ? (T5 - Kho) / (B - Kho) : 0);
            y6.Caption = String.Format("{0:p2}", (T6 - Kho) != 0 && (B - Kho) != 0 ? (T6 - Kho) / (B - Kho) : 0);
            y7.Caption = String.Format("{0:p2}", (T7 - Kho) != 0 && (B - Kho) != 0 ? (T7 - Kho) / (B - Kho) : 0);
            y8.Caption = String.Format("{0:p2}", (T8 - Kho) != 0 && (B - Kho) != 0 ? (T8 - Kho) / (B - Kho) : 0);
            y9.Caption = String.Format("{0:p2}", (T9 - Kho) != 0 && (B - Kho) != 0 ? (T9 - Kho) / (B - Kho) : 0);
            y10.Caption = String.Format("{0:p2}", (T10 - Kho) != 0 && (B - Kho) != 0 ? (T10 - Kho) / (B - Kho) : 0);
            y11.Caption = String.Format("{0:p2}", (T11 - Kho) != 0 && (B - Kho) != 0 ? (T11 - Kho) / (B - Kho) : 0);
            y12.Caption = String.Format("{0:p2}", (T12 - Kho) != 0 && (B - Kho) != 0 ? (T12 - Kho) / (B - Kho) : 0);
            y13.Caption = String.Format("{0:p2}", (T13 - Kho) != 0 && (B - Kho) != 0 ? (T13 - Kho) / (B - Kho) : 0);
            y14.Caption = String.Format("{0:p2}", (T14 - Kho) != 0 && (B - Kho) != 0 ? (T14 - Kho) / (B - Kho) : 0);
            y15.Caption = String.Format("{0:p2}", (T15 - Kho) != 0 && (B - Kho) != 0 ? (T15 - Kho) / (B - Kho) : 0);
            y16.Caption = String.Format("{0:p2}", (T16 - Kho) != 0 && (B - Kho) != 0 ? (T16 - Kho) / (B - Kho) : 0);
            y17.Caption = String.Format("{0:p2}", (T17 - Kho) != 0 && (B - Kho) != 0 ? (T17 - Kho) / (B - Kho) : 0);
            y18.Caption = String.Format("{0:p2}", (T18 - Kho) != 0 && (B - Kho) != 0 ? (T18 - Kho) / (B - Kho) : 0);
            y19.Caption = String.Format("{0:p2}", (T19 - Kho) != 0 && (B - Kho) != 0 ? (T19 - Kho) / (B - Kho) : 0);
            y20.Caption = String.Format("{0:p2}", (T20 - Kho) != 0 && (B - Kho) != 0 ? (T20 - Kho) / (B - Kho) : 0);
            y21.Caption = String.Format("{0:p2}", (T21 - Kho) != 0 && (B - Kho) != 0 ? (T21 - Kho) / (B - Kho) : 0);
            y22.Caption = String.Format("{0:p2}", (T22 - Kho) != 0 && (B - Kho) != 0 ? (T22 - Kho) / (B - Kho) : 0);
            y23.Caption = String.Format("{0:p2}", (T23 - Kho) != 0 && (B - Kho) != 0 ? (T23 - Kho) / (B - Kho) : 0);
            y24.Caption = String.Format("{0:p2}", (T24 - Kho) != 0 && (B - Kho) != 0 ? (T24 - Kho) / (B - Kho) : 0);

            
            t1.Caption = String.Format("{0:p2}", T1 != 0 && B != 0 ? T1 / B : 0);
            t2.Caption = String.Format("{0:p2}", T2 != 0 && B != 0 ? T1 / B : 0);
            t3.Caption = String.Format("{0:p2}", T3 != 0 && B != 0 ? T1 / B : 0);
            t4.Caption = String.Format("{0:p2}", T4 != 0 && B != 0 ? T1 / B : 0);
            t5.Caption = String.Format("{0:p2}", T5 != 0 && B != 0 ? T1 / B : 0);
            t6.Caption = String.Format("{0:p2}", T6 != 0 && B != 0 ? T1 / B : 0);
            t7.Caption = String.Format("{0:p2}", T7 != 0 && B != 0 ? T1 / B : 0);
            t8.Caption = String.Format("{0:p2}", T8 != 0 && B != 0 ? T1 / B : 0);
            t9.Caption = String.Format("{0:p2}", T9 != 0 && B != 0 ? T1 / B : 0);
            t10.Caption = String.Format("{0:p2}", T10 != 0 && B != 0 ? T1 / B : 0);
            t11.Caption = String.Format("{0:p2}", T11 != 0 && B != 0 ? T1 / B : 0);
            t12.Caption = String.Format("{0:p2}", T12 != 0 && B != 0 ? T1 / B : 0);
            t13.Caption = String.Format("{0:p2}", T13 != 0 && B != 0 ? T1 / B : 0);
            t14.Caption = String.Format("{0:p2}", T14 != 0 && B != 0 ? T1 / B : 0);
            t15.Caption = String.Format("{0:p2}", T15 != 0 && B != 0 ? T1 / B : 0);
            t16.Caption = String.Format("{0:p2}", T16 != 0 && B != 0 ? T1 / B : 0);
            t17.Caption = String.Format("{0:p2}", T17 != 0 && B != 0 ? T1 / B : 0);
            t18.Caption = String.Format("{0:p2}", T18 != 0 && B != 0 ? T1 / B : 0);
            t19.Caption = String.Format("{0:p2}", T19 != 0 && B != 0 ? T1 / B : 0);
            t20.Caption = String.Format("{0:p2}", T20 != 0 && B != 0 ? T1 / B : 0);
            t21.Caption = String.Format("{0:p2}", T21 != 0 && B != 0 ? T1 / B : 0);
            t22.Caption = String.Format("{0:p2}", T22 != 0 && B != 0 ? T1 / B : 0);
            t23.Caption = String.Format("{0:p2}", T23 != 0 && B != 0 ? T1 / B : 0);
            t24.Caption = String.Format("{0:p2}", T24 != 0 && B != 0 ? T1 / B : 0);

            gcMatBang.DataSource = thang1;
            bandedGridView1.FocusedRowHandle = -1;
            bandedGridView1.ExpandAllGroups();
            wait.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error("" + ex);
            }
        }

        //public void RowsColor()
        //{
        //    DataGridViewCellStyle style = new DataGridViewCellStyle();
        //    style.BackColor = Color.Gray;
           
        //    for (int i = 0; i < bandedGridView1.RowCount - 1; i++)
        //    {
        //        var check = (decimal?)bandedGridView1.GetRowCellValue(i, "Thang1");
        //        if (check == null)
        //        {
        //            bandedGridView1.RowStyle. = style;
        //        }
        //    }
        //}

    }
}
