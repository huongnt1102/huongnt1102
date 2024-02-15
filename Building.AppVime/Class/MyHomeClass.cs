using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Building.AppVime.Class
{
    public class user_edit_from_orther_db
    {
        public string UserName { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? Lock { get; set; }
        public bool? IsCustomer { get; set; }
        public bool? IsEmp { get; set; }
        public int? building_matn { get; set; }
        public string building_code { get; set; }
        public int? apartment_mamb { get; set; }
        public string apartment_code { get; set; }
        public string pass { get; set; }
    }

    public class tbl_building_edit_from_orther_db_insert
    {
        public byte? Building_Matn { get; set; }

        public string Building_Code { get; set; }

        public string Building_Name { get; set; }

        public string Logo { get; set; }

        public string Building_ConnectString { get; set; }

        public string Hotline { get; set; }

        public string Building_Address { get; set; }

    }

    public class tbl_building_edit_from_orther_db_return
    {
        public int ID { get; set; }

        public int? Building_MaTN { get; set; }

        public string Building_Code { get; set; }

        public string Building_Name { get; set; }

        public string Building_ConnectString { get; set; }

        public string Hotline { get; set; }

        public string Building_Address { get; set; }

        public string Logo { get; set; }

    }

    public class tbl_building_get_id
    {
        public int? Building_MaTN { get; set; }

        public string Building_Code { get; set; }
    }
}
