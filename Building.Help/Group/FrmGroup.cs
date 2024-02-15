using System.Linq;

namespace Building.Help.Group
{
    public partial class FrmGroup : DevExpress.XtraEditors.XtraForm
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

        public FrmGroup()
        {
            InitializeComponent();
        }

        private void FrmGroup_Load(object sender, System.EventArgs e)
        {
            pqModule1 = GetPqModule(GroupName,0);
        }

        /// <summary>
        /// Get pq module theo parent và name
        /// </summary>
        /// <param name="moduleName">tên module</param>
        /// <param name="moduleParent">parent</param>
        /// <returns></returns>
        private Library.pqModule GetPqModule(string moduleName, int moduleParent)
        {
            return masterDataContext.pqModules.FirstOrDefault(_ => _.ModuleName.Trim().ToLower() == moduleName.Trim().ToLower() & _.ModuleParentID == moduleParent);
        }
    }
}