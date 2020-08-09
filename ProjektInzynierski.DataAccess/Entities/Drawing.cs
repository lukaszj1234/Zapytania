using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektInzynierski.DataAccess.Entities
{
    public class Drawing
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastUpdateName { get; set; }
        public string Path { get; set; }
        public string AddDate { get; set; }
        public string LastUpdateDate { get; set; }
        public int IndustryId { get; set; }
        //public ICollection<DrawingOutOfDate> OutOfDateDrawings { get; set; }
    }
}
