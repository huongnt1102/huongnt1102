using DevExpress.CodeParser;
using DevExpress.XtraReports.Serialization;
using System.Linq;

namespace Building.Help.Panel
{
    public partial class CtlGroup : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// Tên nhóm
        /// Truyền tên nhóm từ frm manager qua
        /// </summary>
        public string GroupName { get; set; }

        private Library.MasterDataContext masterDataContext = new Library.MasterDataContext();

        /// <summary>
        /// pqModule tầng 1, parent 0
        /// </summary>
        private Library.pqModule pqModule1;

        public CtlGroup()
        {
            InitializeComponent();
        }

        private void CtlGroup_Load(object sender, System.EventArgs e)
        {
            pqModule1 = GetPqModule(GroupName, 0);
            
            //System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtlGroup));
            //DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElementFooterMenu = new DevExpress.XtraBars.Navigation.AccordionControlElement(DevExpress.XtraBars.Navigation.ElementStyle.Group);
            //accordionControlElementFooterMenu.Text = "Menu";
            //accordionControlElementFooterMenu.Expanded = true;
            //accordionControlElementFooterMenu.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("accordionControlElementFooterMenu.ImageOptions.SvgImage")));
            //accordionControl1.Elements.Add(accordionControlElementFooterMenu);
            //DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElementFooterBack = new DevExpress.XtraBars.Navigation.AccordionControlElement(DevExpress.XtraBars.Navigation.ElementStyle.Group);
            //accordionControlElementFooterMenu.Text = "Back";
            //accordionControl1.Elements.Add(accordionControlElementFooterBack);
            if (pqModule1!=null)
            {
                accordionControlElementFooterMenu.Elements.Clear();
                var listGroup = GetPqModules(pqModule1.ModuleID);
                foreach(var group in listGroup)
                {
                    DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElementGroup = new DevExpress.XtraBars.Navigation.AccordionControlElement(DevExpress.XtraBars.Navigation.ElementStyle.Group);
                    accordionControlElementGroup.Text = group.ModuleName;
                    accordionControlElementGroup.Expanded = true;
                    // Get image
                    try
                    {
                        var imageName = group.ModuleName.Trim().ToLower().Replace("/","") + ".png";
                        var image = imageCollection1.Images[imageName];
                        var indexImage = imageCollection1.Images.IndexOf(image);
                        accordionControlElementGroup.ImageOptions.ImageIndex = indexImage;
                    }
                    catch { }
                    accordionControlElementFooterMenu.Elements.Add(accordionControlElementGroup);
                    var listItem = GetPqModules(group.ModuleID);
                    foreach(var item in listItem)
                    {
                        DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElementItem = new DevExpress.XtraBars.Navigation.AccordionControlElement(DevExpress.XtraBars.Navigation.ElementStyle.Item);
                        accordionControlElementItem.Text = item.ModuleName;
                        accordionControlElementGroup.Elements.Add(accordionControlElementItem);
                        accordionControlElementItem.Name = item.ModuleID.ToString();
                        accordionControlElementItem.Click += AccordionControlElementItem_Click;
                    }
                }
            }
        }

        private void AccordionControlElementItem_Click(object sender, System.EventArgs e)
        {
            DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElementItem = sender as DevExpress.XtraBars.Navigation.AccordionControlElement;
            ctlTaiLieu1.FormID = 40;
            ctlTaiLieu1.LinkID = int.Parse(accordionControlElementItem.Name);
            ctlTaiLieu1.MaNV = Library.Common.User.MaNV;
            ctlTaiLieu1.objNV = Library.Common.User;
            ctlTaiLieu1.TaiLieu_Load();
        }

        private Library.pqModule GetPqModule(string moduleName, int moduleParent)
        {
            return masterDataContext.pqModules.FirstOrDefault(_ => _.ModuleName.Trim().ToLower() == moduleName.Trim().ToLower() & _.ModuleParentID == moduleParent);
        }
        private System.Collections.Generic.List<Library.pqModule> GetPqModules(int moduleIdParent)
        {
            return masterDataContext.pqModules.Where(_ => _.ModuleParentID == moduleIdParent).ToList();
        }

        private void accordionControlElementFooterBack_Click(object sender, System.EventArgs e)
        {
            this.Controls.Clear();
            Building.Help.Panel.CtlTotal total = new Building.Help.Panel.CtlTotal();
            total.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Controls.Add(total);
            total.Show();
        }
    }
}
