﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using Library;

namespace DIPCRM.NhuCau
{
    public partial class frmAddOrganization : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        //ncOrganization objNCO;

        public int MaNC = 0, ORGID = 0;

        public frmAddOrganization()
        {
            InitializeComponent();

            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            //if (ORGID != 0)
            //{
            //    objNCO = db.ncOrganizations.Single(o => o.ORGID == ORGID);
            //    txtDienGiai.Text = objNCO.DienGiai;
            //    txtVaiTro.Text = objNCO.VaiTro;
            //    //txtTenDoiTac.Text = objNCO.Organization.Name;
            //    //btnOrganization.Text = objNCO.Organization.Symbol;
            //    btnOrganization.Tag = objNCO.ORGID;
            //}
            //else
            //    this.Text = "Thêm <Đối tác>";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (ORGID == 0)
            //{
            //    objNCO = new ncOrganization();

            //    objNCO.NgayTao = DateTime.Now;
            //    objNCO.NguoiTao = Common.User.MaNV;
            //}
            //else
            //{
            //    objNCO.NgayCN = DateTime.Now;
            //    objNCO.NguoiCN = Common.User.MaNV;
            //}

            //objNCO.DienGiai = txtDienGiai.Text;
            //objNCO.MaNC = MaNC;
            //objNCO.ORGID = Convert.ToInt32(btnOrganization.Tag);
            //objNCO.VaiTro = txtVaiTro.Text;
            //objNCO.IsDoiTac = true;

            //if (ORGID == 0) db.ncOrganizations.InsertOnSubmit(objNCO);

            //try
            //{
            //    db.SubmitChanges();
            //    DialogResult = System.Windows.Forms.DialogResult.OK;
            //    this.Close();
            //}
            //catch { }
        }

        private void btnProduct_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //var f = new Organizations.frmSelect();
            //f.CateID = 2;
            //f.ShowDialog();
            //if (f.ID != 0)
            //{
            //    btnOrganization.Tag = f.ID;
            //    btnOrganization.Text = f.Symbol;
            //    txtTenDoiTac.Text = f.OrgName;
            //}
        }
    }
}