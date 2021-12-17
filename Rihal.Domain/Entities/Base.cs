using System;

namespace Rihal.Domain.Entities
{
    public class Base<T>
    {
        public T Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}