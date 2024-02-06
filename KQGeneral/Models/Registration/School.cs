using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KQGeneral.Models.Registration
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaximumCapacity { get; set; }
        public int Registration_Type_Schools { get; set; }
    }
}
