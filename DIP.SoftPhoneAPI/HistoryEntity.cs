using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace DIP.SoftPhoneAPI
{
    class HistoryEntity
    {
        public int ID { get; set; }
        public int Type { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public int? CusID { get; set; }
        public string CusName { get; set; }
        public int? RefID { get; set; }
        public int? RefType { get; set; }       
        public string RefName { get; set; }
        public int? StatusID { get; set; }
        public string Note { get; set; }
        public string Duration { get; set; }
        public bool SaveDB { get; set; }

        private string _Path;

        public HistoryEntity()
        {
            _Path = System.Windows.Forms.Application.StartupPath + "\\CallHistory.xml";
        }

        XDocument Load()
        {
            var doc = XDocument.Load(_Path);
            return doc;
        }

        public List<HistoryEntity> GetData()
        {
            try
            {
                var doc = this.Load();
                return (from d in doc.Descendants("item")
                        orderby d.Attribute("Date").Value descending
                        select new HistoryEntity()
                        {
                            ID = int.Parse(d.Attribute("ID").Value),
                            Number = d.Attribute("Number").Value,
                            Type = int.Parse(d.Attribute("Type").Value),
                            Date = HistoryCls.GetDate(d.Attribute("Date").Value),
                            CusID = d.Attribute("CusID") != null ? int.Parse(d.Attribute("CusID").Value) : (int?)null,
                            RefID = d.Attribute("RefID") != null ? int.Parse(d.Attribute("RefID").Value) : (int?)null,
                            RefType = d.Attribute("RefType") != null ? int.Parse(d.Attribute("RefType").Value) : (int?)null,
                            StatusID = d.Attribute("StatusID") != null ? int.Parse(d.Attribute("StatusID").Value) : (int?)null,
                            CusName = d.Attribute("CusName") != null ? d.Attribute("CusName").Value : "",
                            RefName = d.Attribute("RefName") != null ? d.Attribute("RefName").Value : "",
                            Note = d.Value,
                            Duration = d.Attribute("Duration").Value,
                            SaveDB = bool.Parse(d.Attribute("SaveDB").Value)
                        }).ToList();
            }
            catch
            {
                return null;
            }
        }

        public HistoryEntity Get(int _ID)
        {
            try
            {
                var doc = this.Load();
                return (from d in doc.Descendants("item")
                        where d.Attribute("ID").Value == _ID.ToString()
                        select new HistoryEntity()
                        {
                            ID = int.Parse(d.Attribute("ID").Value),
                            Number = d.Attribute("Number").Value,
                            Type = int.Parse(d.Attribute("Type").Value),
                            Date = HistoryCls.GetDate(d.Attribute("Date").Value),
                            CusID = d.Attribute("CusID") != null ? int.Parse(d.Attribute("CusID").Value) : (int?)null,
                            RefID = d.Attribute("RefID") != null ? int.Parse(d.Attribute("RefID").Value) : (int?)null,
                            RefType = d.Attribute("RefType") != null ? int.Parse(d.Attribute("RefType").Value) : (int?)null,
                            StatusID = d.Attribute("StatusID") != null ? int.Parse(d.Attribute("StatusID").Value) : (int?)null,
                            CusName = d.Attribute("CusName") != null ? d.Attribute("CusName").Value : "",
                            RefName = d.Attribute("RefName") != null ? d.Attribute("RefName").Value : "",
                            Note = d.Value,
                            Duration = d.Attribute("Duration").Value,
                            SaveDB = bool.Parse(d.Attribute("SaveDB").Value)
                        }).First();
            }
            catch
            {
                return new HistoryEntity();
            }
        }

        public void Add()
        {
            try
            {
                if (this.Date.Year == 1) return;

                var doc = this.Load();

                try
                {
                    this.ID = (from d in doc.Descendants("item") select int.Parse(d.Attribute("ID").Value)).Max() + 1;
                }
                catch
                {
                    this.ID = 1;
                }

                XElement item = new XElement("item");
                item.SetAttributeValue("ID", this.ID);
                item.SetAttributeValue("Number", this.Number);
                item.SetAttributeValue("Type", this.Type);
                item.SetAttributeValue("Date", this.Date.ToString("yyyy-MM-dd HH:mm:ss"));
                item.SetAttributeValue("CusID", this.CusID);
                item.SetAttributeValue("RefID", this.RefID);
                item.SetAttributeValue("RefType", this.RefType);
                item.SetAttributeValue("StatusID", this.StatusID);
                item.SetAttributeValue("CusName", this.CusName);
                item.SetAttributeValue("RefName", this.RefName);
                item.SetAttributeValue("Duration", this.Duration);
                item.SetAttributeValue("SaveDB", this.SaveDB);
                item.Value = this.Note;

                doc.Element("root").Add(item);
                doc.Save(_Path);
            }
            catch { }
        }

        public void Update()
        {
            try
            {
                var doc = this.Load();
                var item = (from d in doc.Descendants("item")
                            where d.Attribute("ID").Value == this.ID.ToString()
                            select d).First();
                item.SetAttributeValue("Type", this.Type);
                item.SetAttributeValue("CusID", this.CusID);
                item.SetAttributeValue("RefID", this.RefID);
                item.SetAttributeValue("RefType", this.RefType);
                item.SetAttributeValue("StatusID", this.StatusID);
                item.SetAttributeValue("CusName", this.CusName);
                item.SetAttributeValue("RefName", this.RefName);
                item.SetAttributeValue("Duration", this.Duration);
                item.SetAttributeValue("SaveDB", this.SaveDB);
                item.Value = this.Note;
                doc.Save(_Path);
            }
            catch { }
        }

        public void Delete(List<int> arrID)
        {
            try
            {
                var doc = this.Load();

                foreach (var id in arrID)
                {
                    var item = (from d in doc.Descendants("item")
                                where d.Attribute("ID").Value == id.ToString()
                                select d).First();
                    item.Remove();
                }

                doc.Save(_Path);
            }
            catch { }
        }

        public int ResetMissed()
        {
            try
            {
                var doc = this.Load();
                var root = (from d in doc.Descendants("root") select d).First();
                root.SetAttributeValue("Missed", 0);
                doc.Save(_Path);
            }
            catch
            {

            }

            return 0;
        }

        public int SetMissed()
        {
            try
            {
                var doc = this.Load();
                var root = (from d in doc.Descendants("root") select d).First();
                var Missed = 0;
                if (root.Attribute("Missed") != null && root.Attribute("Missed").Value != "")
                    Missed = int.Parse(root.Attribute("Missed").Value);
                Missed++;
                root.SetAttributeValue("Missed", Missed);
                doc.Save(_Path);

                return Missed;
            }
            catch
            {
                return 0;
            }
        }

        public int GetMissed()
        {
            try
            {
                var doc = this.Load();
                var root = (from d in doc.Descendants("root") select d).First();
                return int.Parse(root.Attribute("Missed").Value);
            }
            catch
            {
                return 0;
            }
        }
    }
}
