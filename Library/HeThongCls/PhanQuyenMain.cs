namespace Library.HeThongCls
{
    public static class PhanQuyenMain
    {
        private static Library.MasterDataContext _db = new Library.MasterDataContext();

        public static void UpdateRibbon(System.Windows.Forms.Form form, DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl)
        {
            try
            {
                var lRibbonPage = Library.HeThongCls.PhanQuyenCls.getAllRibbonPage(ribbonControl);

                foreach (var page in lRibbonPage)
                {
                    UpdateAllPage(page);
                }

                _db.SubmitChanges();
                Library.DialogBox.Success();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
            }
        }

        private static void UpdateAllPage(DevExpress.XtraBars.Ribbon.RibbonPage page)
        {
            //var pageId = _db.pq_PhanQuyenMain_Module_Update(page.Name, 0, page.Text);
            var pageId = _db.pqModule_ReturnValueUpdate(page.Name, page.Text, 0, false);
            foreach (var pageGroup in page.Groups)
            {
                if (pageGroup is DevExpress.XtraBars.Ribbon.RibbonPageGroup)
                {
                    var pageGroupItem = pageGroup as DevExpress.XtraBars.Ribbon.RibbonPageGroup;
                    UpdateAllPageGroup(pageGroupItem, pageId);
                }
            }
        }

        private static void UpdateAllPageGroup(DevExpress.XtraBars.Ribbon.RibbonPageGroup pageGroupItem, int? pageId)
        {
            //var pageGroupId = _db.pq_PhanQuyenMain_Module_Update(pageGroupItem.Name, pageId, pageGroupItem.Text);

            var pageGroupId = _db.pqModule_ReturnValueUpdate(pageGroupItem.Name, pageGroupItem.Text, pageId, false);

            foreach (DevExpress.XtraBars.BarItemLink currentLink in pageGroupItem.ItemLinks)
            {
                //var currentLinkId = _db.pq_PhanQuyenMain_Module_Update(currentLink.Item.Name, pageGroupId, currentLink.Item.Caption);
                var currentLinkId = _db.pqModule_ReturnValueUpdate(currentLink.Item.Name, currentLink.Item.Caption, pageGroupId, false);
                var itemLink = currentLink.Item;
                if (itemLink.GetType().FullName == "DevExpress.XtraBars.BarSubItem")
                {
                    if (itemLink is DevExpress.XtraBars.BarSubItem)
                    {
                        var subItem = itemLink as DevExpress.XtraBars.BarSubItem;
                        UpdateAllItemInPageGroup(subItem, currentLinkId);
                    }
                }
            }
        }

        private static void UpdateAllItemInPageGroup(DevExpress.XtraBars.BarSubItem subItem, int? currentLinkId)
        {
            foreach (var subItemLink in subItem.ItemLinks)
            {
                if (subItemLink is DevExpress.XtraBars.BarButtonItemLink)
                {
                    var itemsub = subItemLink as DevExpress.XtraBars.BarButtonItemLink;
                    //_db.pq_PhanQuyenMain_Module_Update(itemsub.Item.Name, currentLinkId, itemsub.Item.Caption);
                    _db.pqModule_ReturnValueUpdate(itemsub.Item.Name, itemsub.Item.Caption, currentLinkId, false);
                }

                if (subItemLink is DevExpress.XtraBars.BarSubItemLink)
                {
                    var itemSubLink = subItemLink as DevExpress.XtraBars.BarSubItemLink;
                    UpdateAllButtonInSub(itemSubLink, currentLinkId);
                }
            }
        }

        private static void UpdateAllButtonInSub(DevExpress.XtraBars.BarSubItemLink barSubItemLink, int? parentId)
        {
            //var barSubItemLinkId = _db.pq_PhanQuyenMain_Module_Update(barSubItemLink.Item.Name, parentId, barSubItemLink.Item.Caption);
            var barSubItemLinkId = _db.pqModule_ReturnValueUpdate(barSubItemLink.Item.Name, barSubItemLink.Item.Caption, parentId, false);
            foreach (var itemButton in barSubItemLink.Item.ItemLinks)
            {
                if (itemButton is DevExpress.XtraBars.BarButtonItemLink)
                {
                    var button = itemButton as DevExpress.XtraBars.BarButtonItemLink;
                    //_db.pq_PhanQuyenMain_Module_Update(button.Item.Name, barSubItemLinkId, button.Item.Caption);
                    _db.pqModule_ReturnValueUpdate(button.Item.Name, button.Item.Caption, parentId, false);
                }
            }
            
        }
    }
}
