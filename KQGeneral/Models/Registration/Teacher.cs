using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KQGeneral.Models.Registration
{
    public class Teacher
    {
        public int Id { get; set; }
        /// <summary>
        /// The School/Mitham Id that contains the Gan that this teacher manages
        /// </summary>
        public int MithamId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int MaximumCapacity { get; set; }
        public int NumberOfRegisteredStudents { get; set; }
        public bool IsActive { get; set; }
        public int TeacherId { get; set; }
        public int Registration_Type { get; set; }
    }
}
