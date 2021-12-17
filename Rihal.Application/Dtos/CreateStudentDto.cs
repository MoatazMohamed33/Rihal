using System;

namespace Rihal.Application.Dtos
{
    public class CreateStudentDto
    {
        public int CountryId { get; set; }
        public int ClassId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}