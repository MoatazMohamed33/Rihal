using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihal.Domain.Entities
{
    public class Class :Base<int>
    {
        public string Name { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
