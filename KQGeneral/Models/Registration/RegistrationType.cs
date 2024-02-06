using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KQGeneral.Models.Registration
{
    public class RegistrationType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ViewName
        {
            get
            {
                return $"{Name} - {Year}/{Year + 1}";

            }
        }
        public bool IsActiv { get; set; }
        public int Year { get; set; }
    }
}
