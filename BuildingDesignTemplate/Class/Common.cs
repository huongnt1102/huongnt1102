using System.Linq;

namespace BuildingDesignTemplate.Class
{
    public static class Common
    {
        private static readonly Library.MasterDataContext Db = new Library.MasterDataContext();

        public static class ReportIndex
        {
            public const int BIEU_MAU_CONG_NO_NCC = 107;
            public const int THONG_KE_KINH_PHI_DU_KIEN = 108;
            public const int _1KH_THU = 118;
            public const int BC_THU = 119;
        }

        public static class GroupSubName
        {
            public const string BIEU_MAU_CONG_NO_NCC = "CNHD";
            public const string THONG_KE_KINH_PHI_DU_KIEN = "KPDK";
        }

        public static string GetContents(string contents, int? groupId, System.Data. DataRow rData, string groupSub)
        {
            var db = new Library.MasterDataContext();
            var fields = db.template_Fields.Where(_ => _.GroupId == groupId);
            if(groupSub!="") fields = db.template_Fields.Where(_ => _.GroupId == groupId & ( _.GroupSub.ToLower() == groupSub.ToLower()));

            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl();
            ctlRtf.RtfText = contents;

            foreach (var i in fields)
            {
                try
                {
                    string value = rData[i.Field.Replace("[", "").Replace("]", "")].ToString();
                    ctlRtf.Document.ReplaceAll(i.Field, value , DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                }
                catch{}
            }

            return ctlRtf.RtfText;
        }

        public static Library.tnToaNha GetToaNhaById(string buildingId)
        {
            return Db.tnToaNhas.FirstOrDefault(_ => _.MaTN.ToString() == buildingId);
        }

        public static Library.tnToaNha GetToaNhaById(byte? buildingId)
        {
            return Db.tnToaNhas.FirstOrDefault(_ => _.MaTN == buildingId);
        }

        private static string RtfDeleteFieldFinish(string rtfText, string tableName)
        {
            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };

            ctlRtf.RtfText = FindAndRemoveTable(ctlRtf.RtfText, tableName);


            ctlRtf.Document.ReplaceAll(tableName, "", DevExpress.XtraRichEdit.API.Native.SearchOptions.None);


            return ctlRtf.RtfText;
        }

        public static int FindIndexTable(DevExpress.XtraRichEdit.API.Native.Document document, string key)
        {
            var tableList = (from p in document.Tables
                    select new { cellName = document.GetText(p.Rows[0].Cells[0].Range).Replace(" ", "") }).AsEnumerable()
                .Select((t, index) => new { stt = index, t.cellName }).ToList();
            var tableIndex = tableList.FirstOrDefault(_ => _.cellName.Contains(key));
            return tableIndex != null ? tableIndex.stt : 0;
        }

        private static string FindAndRemoveTable(string rtfText, string key)
        {
            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };
            var document = ctlRtf.Document;
            var index = FindIndexTable(document, key);

            if (index <= 0) return ctlRtf.RtfText;
            var table = document.Tables[index];
            var pos = table.Range.Start.ToInt() - 2;
            document.Tables.Remove(table);
            if (pos <= 0) return ctlRtf.RtfText;
            var range = document.CreateRange(pos, 2);
            document.Replace(range, "");
            return ctlRtf.RtfText;
        }

        public static BuildingDesignTemplate.Class.MergeField.Field FindTable(DevExpress.XtraRichEdit.API.Native.Document document, string key)
        {
            var tableList = (from p in document.Tables
                    select new { cellName = document.GetText(p.Rows[0].Cells[0].Range).Replace(" ", "") }).AsEnumerable()
                .Select((t, index) => new BuildingDesignTemplate.Class.MergeField.Field { Index = index, Name = t.cellName }).ToList();
            var tableIndex = tableList.FirstOrDefault(_ => _.Name.Contains(key));
            return tableIndex;
        }


        public static string MergeTable(string contents, string tableName, System.Data.DataTable dataTable)
        {
            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = contents };

            var document = ctlRtf.Document;
            var field = FindTable(document, tableName);
            if (field != null)
            {
                var table = document.Tables[field.Index];
                var rField = table.Rows[2];
                var ltFieldName = (from cell in rField.Cells let cellName = document.GetText(cell.Range).Replace(System.Environment.NewLine, "").Replace(" ", "") select new BuildingDesignTemplate.Class.MergeField.Field { Index = cell.Index, Name = cellName }).ToList();

                var index = 2;

                var ii = 0;

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    var row = table.Rows.InsertBefore(index);
                    System.Data.DataRow rd = dataTable.Rows[ii];

                    foreach (var f in ltFieldName)
                    {
                        var cell = row[f.Index];
                        try
                        {
                            if (f.Name.Replace("[", "").Replace("]", "") == "") continue;
                            if (f.Name.Replace("[", "").Replace("]", "").Contains(rd.Table.Columns[f.Name.Replace("[", "").Replace("]", "")].ColumnName))
                                document.InsertSingleLineText(cell.Range.Start, rd[f.Name.Replace("[", "").Replace("]", "")].ToString());
                        }
                        catch { }
                    }

                    index += 1;
                    ii++;
                }

                table.Rows.RemoveAt(index);
                table.Rows.RemoveAt(0);
            }

            contents = ctlRtf.RtfText;

            return contents;
        }

    }
}
