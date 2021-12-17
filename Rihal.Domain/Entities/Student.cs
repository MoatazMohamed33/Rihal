using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihal.Domain.Entities
{
    public class Student : Base<Guid>
    {
        public int CountryId { get; set; }
        public int ClassId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual Country Country { get; set; }
        public virtual Class Class { get; set; }
    }
}
