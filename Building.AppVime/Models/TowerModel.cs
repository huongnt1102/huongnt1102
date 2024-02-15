using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Building.AppVime
{
    public class TowerModel
    {
        public short Id { get; set; }

        public string Name { get; set; }

        public byte TowerId { get; set; }

        public short FunctionId { get; set; }

        public byte IndexNumber { get; set; }
    }
}
