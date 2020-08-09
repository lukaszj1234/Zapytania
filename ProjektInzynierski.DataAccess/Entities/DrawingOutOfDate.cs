using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektInzynierski.DataAccess.Entities
{
    public class DrawingOutOfDate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int DrawingId { get; set; }
    }
}
