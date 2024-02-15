using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Library;

namespace DichVu.ThongKe
{
    public partial class frmDienNuoc : Form
    {
        Thread th1;
        Thread th2;
        Thread th3;
        Thread th4;
        public Library.tnNhanVien objnhanvien;
        public frmDienNuoc()
        {
            InitializeComponent();
        }

        private void frmDienNuoc_Load(object sender, EventArgs e)
        {
            th1 = new Thread(LoadCtl1) { IsBackground = true };
            th1.Start();

            th2 = new Thread(LoadCtl2) { IsBackground = true };
            th2.Start();

            th3 = new Thread(LoadCtl3) { IsBackground = true };
            th3.Start();

            th4 = new Thread(LoadCtl4) { IsBackground = true };
            th4.Start();
        }

        private delegate void dlgAddItemN();
        private void LoadCtl1()
        {
            Library.ThongKeCtl.ctlTKTienDien ctl1 = new Library.ThongKeCtl.ctlTKTienDien(objnhanvien) { Dock = DockStyle.Fill };

            if (pnlTKTienDien.InvokeRequired)
            {
                Invoke(new dlgAddItemN(LoadCtl1));
            }
            else
            {
                pnlTKTienDien.Controls.Add(ctl1);
            }
        }
        private void LoadCtl2()
        {
            Library.ThongKeCtl.ctlLuongDienTieuThu ctl2 = new Library.ThongKeCtl.ctlLuongDienTieuThu(objnhanvien) { Dock = DockStyle.Fill };

            if (pnlLuongDienTieuThu.InvokeRequired)
            {
                Invoke(new dlgAddItemN(LoadCtl2));
            }
            else
            {
                pnlLuongDienTieuThu.Controls.Add(ctl2);
            }
        }
        private void LoadCtl3()
        {
            Library.ThongKeCtl.ctlTKTienNuoc ctl3 = new Library.ThongKeCtl.ctlTKTienNuoc(objnhanvien) { Dock = DockStyle.Fill };

            if (pnlTKTienNuoc.InvokeRequired)
            {
                Invoke(new dlgAddItemN(LoadCtl3));
            }
            else
            {
                pnlTKTienNuoc.Controls.Add(ctl3);
            }
        }

        private void LoadCtl4()
        {
            Library.ThongKeCtl.ctlTKLuongNuocTieuThu ctl4 = new Library.ThongKeCtl.ctlTKLuongNuocTieuThu(objnhanvien) { Dock = DockStyle.Fill };

            if (pnlLuongNuocTieuThu.InvokeRequired)
            {
                Invoke(new dlgAddItemN(LoadCtl4));
            }
            else
            {
                pnlLuongNuocTieuThu.Controls.Add(ctl4);
            }
        }

        private void frmDienNuoc_SizeChanged(object sender, EventArgs e)
        {
            splitContainerControl1.SplitterPosition = splitContainerControl1.Width / 2;
            splitContainerControl2.SplitterPosition = splitContainerControl2.Height / 2;
            splitContainerControl3.SplitterPosition = splitContainerControl3.Height / 2;
        }

        private void frmDienNuoc_FormClosed(object sender, FormClosedEventArgs e)
        {
            th1.Abort();
            th2.Abort();
            th3.Abort();
            th3.Abort();
        }
    }
}
