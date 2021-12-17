using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rihal.Application.Dtos;
using Rihal.Application.Interfaces;
using Rihal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihal.Infrastructure.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public StudentService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ValidationResult>> Create(CreateStudentDto model)
        {
            var validateResponse = new List<ValidationResult>();
            if (Validate(model).Count > 0)
            {
                validateResponse = Validate(model);
                return validateResponse;
            }
            var entity = _mapper.Map<Student>(model);
            await _context.Students.AddAsync(entity);
            await _context.SaveChangesAsync();
            return validateResponse;
        }



        public async Task<List<ValidationResult>> Delete(Guid id)
        {
            var validateResponse = new List<ValidationResult>();
            if (id == Guid.Empty)
            {
                validateResponse.Add(new ValidationResult("Id Required", new string[] { "id" }));
            }

            var student = _context.Students.FirstOrDefault(c => c.Id == id && c.IsDeleted == false);
            if (student == null)
            {
                validateResponse.Add(new ValidationResult("Not Valid Id", new string[] { "id" }));
            }

            if (validateResponse.Count > 0)
            {
                return validateResponse;
            }

            student.IsDeleted = true;
            await _context.SaveChangesAsync();
            return validateResponse;
        }

        public async Task<List<ValidationResult>> Edit(EditStudentDto model)
        {
            var validateResponse = new List<ValidationResult>();
            var student = _context.Students.FirstOrDefault(c => c.Id == model.Id && c.IsDeleted == false);
            if (student == null)
            {
                validateResponse.Add(new ValidationResult("Not Valid Id", new string[] { "id" }));
            }
            if (Validate(model).Count > 0)
            {
                validateResponse = Validate(model);
                return validateResponse;
            }
            student.Name = model.Name;
            student.BirthDate = model.BirthDate;
            student.ClassId = model.ClassId;
            student.CountryId = model.CountryId;
            await _context.SaveChangesAsync();
            return validateResponse;
        }

        public async Task<EditStudentDto> Get(Guid id)
        {
            var res = await _context.Students.FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted==false);
            var result = _mapper.Map<EditStudentDto>(res);
            return result;
        }

        public async Task<List<StudentViewModel>> GetAll()
        {
            var res = await _context.Students.Where(c=>c.IsDeleted == false).Include(c => c.Country).Include(c => c.Class).ToListAsync();
            var result = _mapper.Map<List<StudentViewModel>>(res);
            return result;
        }

        public async Task<List<AveregePerCountryViewModel>> GetAveregePerCountry()
        {
            var averegePerCountry = await _context.Students.Where(c => c.IsDeleted == false)
                                    .GroupBy(t => t.Country.Name)
                                    .Select(group => new AveregePerCountryViewModel
                                    {
                                        CountryName = group.Key,
                                        Averege = group.Sum(x => (DateTime.Now.Year - x.BirthDate.Year)) / group.Count()
                                    }).ToListAsync();
            return averegePerCountry;
        }

        public async Task<List<ClassViewModel>> GetClasses()
        {
            var classes = await _context.Classes.Where(c => c.IsDeleted == false).Select(x=>new ClassViewModel {ClassName=x.Name,Id=x.Id }).ToListAsync();
            return classes;
        }

        public async Task<List<CountryViewModel>> GetCountries()
        {
            var countries = await _context.Countries.Where(c => c.IsDeleted == false).Select(x => new CountryViewModel { CountryName = x.Name, Id = x.Id }).ToListAsync();
            return countries;
        }

        public async Task<List<StudentPerClassViewModel>> GetPerClass()
        {
            var studentsPerClass = await _context.Students.Where(c => c.IsDeleted == false)
                                   .GroupBy(t => t.Class.Name)
                                   .Select(group => new StudentPerClassViewModel
                                   {
                                       ClassName = group.Key,
                                       Count = group.Count()
                                   }).ToListAsync();
            return studentsPerClass;
        }

        public async Task<List<StudentPerCountryViewModel>> GetPerCountry()
        {
            var studentsPerCountry = await _context.Students.Where(c => c.IsDeleted == false)
                                   .GroupBy(t => t.Country.Name)
                                   .Select(group => new StudentPerCountryViewModel
                                   {
                                       CountryName = group.Key,
                                       Count = group.Count()
                                   }).ToListAsync();
            return studentsPerCountry;
        }

        private List<ValidationResult> Validate(CreateStudentDto model)
        {
            var errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(model.Name))
                errors.Add(new ValidationResult("NameRequired", new string[] { "Name" }));
            if (model.BirthDate >= DateTime.Now)
                errors.Add(new ValidationResult("Enter Valid BirthDate", new string[] { "BirthDate" }));
            if (model.ClassId==0)
                errors.Add(new ValidationResult("ClassIdRequired", new string[] { "ClassId" }));
            if (model.CountryId==0)
                errors.Add(new ValidationResult("CountryIdRequired", new string[] { "CountryId" }));
            return errors;
        }

        private List<ValidationResult> Validate(EditStudentDto model)
        {
            var errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(model.Name))
                errors.Add(new ValidationResult("NameRequired", new string[] { "Name" }));
            if (model.BirthDate >= DateTime.Now)
                errors.Add(new ValidationResult("Enter Valid BirthDate", new string[] { "BirthDate" }));
            if (model.Id == Guid.Empty)
                errors.Add(new ValidationResult("IdRequired", new string[] { "id" }));
            if (model.ClassId == 0)
                errors.Add(new ValidationResult("ClassIdRequired", new string[] { "ClassId" }));
            if (model.CountryId == 0)
                errors.Add(new ValidationResult("CountryIdRequired", new string[] { "CountryId" }));

            return errors;
        }
    }
}
