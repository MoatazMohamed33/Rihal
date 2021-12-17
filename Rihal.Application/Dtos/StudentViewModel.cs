using System;

namespace Rihal.Application.Dtos
{
    public class StudentViewModel
    {
        public Guid Id { get; set; }
        public int CountryId { get; set; }
        public int ClassId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string CountryName { get; set; }
        public string ClassName { get; set; }
    }
}