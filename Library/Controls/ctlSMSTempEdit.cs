﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors.Controls;
using Library;

namespace Library.Controls
{
    public partial class ctlSMSTempEdit : LookEdit
    {
        public int? FormID { get; set; }
        public ctlSMSTempEdit()
        {
            this.Properties.DisplayMember = "TempName";
            this.Properties.ValueMember = "TempID";
            this.Properties.ShowHeader = false;
        }

        public void LoadData()
        {
            this.Properties.Columns.Clear();
            this.Properties.Columns.Add(new LookUpColumnInfo("TempName"));

            using (var db = new MasterDataContext())
            {
                this.Properties.DataSource = null;
                this.Properties.DataSource = db.SMSTemplates.Where(p=>p.FormID==this.FormID).OrderBy(p => p.TempID).ToList();
            }
        }
    }
}
