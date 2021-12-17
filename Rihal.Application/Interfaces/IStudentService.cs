using Rihal.Application.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihal.Application.Interfaces
{
    public interface IStudentService
    {
        Task<List<ValidationResult>> Create(CreateStudentDto model);
        Task<EditStudentDto> Get(Guid id);
        Task<List<StudentViewModel>> GetAll();
        Task<List<ValidationResult>> Edit(EditStudentDto model);
        Task<List<ValidationResult>> Delete(Guid id);
        Task<List<StudentPerClassViewModel>> GetPerClass();
        Task<List<StudentPerCountryViewModel>> GetPerCountry();
        Task<List<AveregePerCountryViewModel>> GetAveregePerCountry();
        Task<List<CountryViewModel>> GetCountries();
        Task<List<ClassViewModel>> GetClasses();
    }
}
