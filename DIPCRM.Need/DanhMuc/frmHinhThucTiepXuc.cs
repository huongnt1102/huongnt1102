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


namespace DIPCRM.Need
{
    public partial class frmHinhThucTiepXuc : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmHinhThucTiepXuc()
        {
            InitializeComponent();

            //

            this.Load += new EventHandler(frmHinhThucTiepXuc_Load);
            this.btnLuu.Click += new EventHandler(btnLuu_Click);
            this.btnHuy.Click += new EventHandler(btnHuy_Click);
            grView.KeyUp += new KeyEventHandler(grView_KeyUp);
        }

        void frmHinhThucTiepXuc_Load(object sender, EventArgs e)
        {
            grControl.DataSource = db.ncHinhThucTiepXucs.OrderBy(p=>p.STT);
        }

        void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                db.SubmitChanges();

                //

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

        private void grView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.Question("Bạn có chắc không") == DialogResult.Yes)
                    grView.DeleteSelectedRows();
            }
        }
    }
}