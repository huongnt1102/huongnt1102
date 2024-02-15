﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace DichVu.KhachHang.CSKH
{
    public partial class frmNgheNghiep : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmNgheNghiep()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmNgheNghiep_Load);
            this.btnLuu.Click += new EventHandler(btnLuu_Click);
            this.btnHuy.Click += new EventHandler(btnHuy_Click);
        }

        void frmNgheNghiep_Load(object sender, EventArgs e)
        {
            gcNguonKH.DataSource = db.NgheNghieps.OrderBy(p => p.STT);
        }

        void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                grvNguonKH.RefreshData();

                db.SubmitChanges();

                DialogBox.Success();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch(Exception ex){
                DialogBox.Error(ex.Message);
            }
        }

        void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvNguonKH_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.Question("Bạn có chắc không?") == DialogResult.Yes)
                    grvNguonKH.DeleteSelectedRows();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                 if (DialogBox.Question("Bạn có chắc không?") == DialogResult.Yes)
                    grvNguonKH.DeleteSelectedRows();
            }
            catch
            {
                DialogBox.Error("Tên nghề nghiệp đã sử dụng. Không thể xóa!");
            }
        }
    }
}