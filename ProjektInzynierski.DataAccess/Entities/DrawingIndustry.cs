using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektInzynierski.DataAccess.Entities
{
    public class DrawingIndustry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FolderPath { get; set; }
        public string LastUpdate { get; set; }
    }
}
