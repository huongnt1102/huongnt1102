using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthorizationClass.Generator
{
    public class Generator
    {
        public bool GeneratorRibbon(DevExpress.XtraBars.Ribbon.RibbonControl ribbon, string Description)
        {
            if (CheckRibbonAlreayOnDatabase(ribbon))
            {
                return false;
            }
            Common.dbAccess db = new Common.dbAccess();
            db.BeginTransaction();
            try
            {
                //1. Add form detail
                db.CreateNewSqlCommand();

                db.AddParmetter("@FormName", ribbon.Name);
                db.AddParmetter("@Description", Description);

                db.ExecuteNonQueryWithTransaction("spInsertFormList");  

                //2. add control on it 
                
                //foreach (DevExpress.XtraBars.BarButtonItem item in (ribbon as DevExpress.XtraBars.Ribbon.RibbonControl).Items)
                //{
                //    if (item.Tag != null)
                //    {
                //        db.CreateNewSqlCommand();
                //        db.AddParmetter("@ControlName", item.Name);
                //        db.AddParmetter("@ControlTag", item.Tag);
                //        db.AddParmetter("@FormName", ribbon.Name);

                //        db.ExecuteNonQueryWithTransaction("spInsertFormControl");  
                //    } 
                //}

                foreach (DevExpress.XtraBars.Ribbon.RibbonPage page in ribbon.Pages)
                {
                    if (page.Tag != null)
                    {
                        db.CreateNewSqlCommand();
                        db.AddParmetter("@ControlName", page.Name);
                        db.AddParmetter("@ControlTag", page.Tag);
                        db.AddParmetter("@FormName", ribbon.Name);

                        db.ExecuteNonQueryWithTransaction("spInsertFormControl");  
                    }
                    foreach (DevExpress.XtraBars.Ribbon.RibbonPageGroup pageGroup in page.Groups)
                    {
                        foreach (DevExpress.XtraBars.BarItemLink item in pageGroup.ItemLinks)
                        {
                            if (item.Item.Tag != null)
                            {
                                db.CreateNewSqlCommand();
                                db.AddParmetter("@ControlName", item.Item.Name);
                                db.AddParmetter("@ControlTag", item.Item.Tag);
                                db.AddParmetter("@FormName", ribbon.Name);

                                db.ExecuteNonQueryWithTransaction("spInsertFormControl");  
                            }
                        }
                        if (pageGroup.Tag != null)
                        {
                            db.CreateNewSqlCommand();
                            db.AddParmetter("@ControlName", pageGroup.Name);
                            db.AddParmetter("@ControlTag", pageGroup.Tag);
                            db.AddParmetter("@FormName", ribbon.Name);

                            db.ExecuteNonQueryWithTransaction("spInsertFormControl");  
                        }
                        
                    }
                }

                db.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                db.RollbackTransaction();
                return false;
            }
            
        }

        private bool CheckRibbonAlreayOnDatabase(DevExpress.XtraBars.Ribbon.RibbonControl ribbon)
        {
            bool available = false;
            Common.dbAccess db = new Common.dbAccess();
            db.CreateNewSqlCommand();
            db.AddParmetter("@FormName", ribbon.Name);
            System.Data.SqlClient.SqlDataReader reader = db.ExecuteSqlDataReader("spGetAuthFormExists");  
            if (reader.Read())
                available = true;
            else
                available = false;
            reader.Close();

            return available;
        }
        /// <summary>
        /// Tạo danh sách form và control trong database. Đối với control, chỉ thêm vào database
        /// những control được đánh dấu tag (như ghi chú chức năng)
        /// </summary>
        /// <param name="frm">Form</param>
        /// <param name="Description">Chú thích, ghi chú cho form</param>
        /// <returns>Bool</returns>
        public bool GeneratorForm(System.Windows.Forms.Form frm, string Description)
        {
            if (CheckFromAndControlAlreayOnDatabase(frm))
            {
                return false;
            }
            Common.dbAccess db = new Common.dbAccess();
            db.BeginTransaction();
            try
            {
                //1. Add form detail
                db.CreateNewSqlCommand();

                db.AddParmetter("@FormName", frm.Name);
                db.AddParmetter("@Description", Description);

                db.ExecuteNonQueryWithTransaction("spInsertFormList");  
                
                //2. add control on it 
                foreach (System.Windows.Forms.Control ctrl in frm.Controls)
                {
                    if (ctrl.Tag != null)
                    {
                        db.CreateNewSqlCommand();
                        db.AddParmetter("@ControlName", ctrl.Name);
                        db.AddParmetter("@ControlTag", ctrl.Tag);
                        db.AddParmetter("@FormName", frm.Name);

                        db.ExecuteNonQueryWithTransaction("spInsertFormControl");  
                    }
                }

                //3. Add form,control to Module with default value
                db.CommitTransaction();
                return true;
            }
            catch
            {
                db.RollbackTransaction();
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra table form và control đã có dữ liệu chưa
        /// </summary>
        /// <returns></returns>
        public bool CheckFromAndControlAlreayOnDatabase(System.Windows.Forms.Form frm)
        {
            bool available = false;
            Common.dbAccess db = new Common.dbAccess();
            db.CreateNewSqlCommand();
            db.AddParmetter("@FormName", frm.Name);
            System.Data.SqlClient.SqlDataReader reader = db.ExecuteSqlDataReader("spGetAuthFormExists");  
            if (reader.Read())
                available = true;
            else
                available = false;
            reader.Close();

            return available;
        }

        /// <summary>
        /// Xóa dữ liệu về Form và control trên database
        /// </summary>
        /// <returns>Bool</returns>
        public bool DeleteFormAndConrolFromDatabase()
        {
            DevExpress.Utils.WaitDialogForm wait = new DevExpress.Utils.WaitDialogForm("Đang xử lý, vui lòng chờ...", "Processing...");  
            try
            {
                Common.dbAccess db = new Common.dbAccess();
                db.CreateNewSqlCommand();
                db.ExecuteNonQuery("spDeleteFormAndControl");  
                wait.Close();
                return true;
            }
            catch
            {
                wait.Close();
                return false;
            }
        }

        /// <summary>
        /// Thêm module
        /// </summary>
        /// <param name="ModuleName"></param>
        /// <param name="Note"></param>
        /// <param name="control"></param>
        /// <param name="FormName"></param>
        /// <returns>bool</returns>
        public bool AddModule(string ModuleName, string Note, int control, string FormName)
        {
            try
            {
                Common.dbAccess db = new Common.dbAccess();
                db.CreateNewSqlCommand();
                db.AddParmetter("@ModuleName", ModuleName);
                db.AddParmetter("@Note", Note);
                db.AddParmetter("@ControlID", control);
                db.AddParmetter("@FormName", FormName);

                db.ExecuteNonQuery("spInsertNewModule");  
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
