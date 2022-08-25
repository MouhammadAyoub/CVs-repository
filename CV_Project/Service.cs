using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CV_Project.Data;

namespace Cv_Project
{
    public class Service
    {
        readonly ApplicationDbContext _context;
        readonly ILogger _Logger;

        public Service(ApplicationDbContext context, ILoggerFactory factory)
        {
            _context = context;
            _Logger = factory.CreateLogger<Service>();
        }

        public int CalculateGrade(string myGender,string mySkills)
        {
            int myGrade = 0;

            if (myGender.Equals("male"))
            {
                myGrade += 5;

                if (mySkills != null)
                {
                    myGrade += 10 * mySkills.Split().Length - 10;
                }

            }
            else
            {
                myGrade += 10;

                if (mySkills != null)
                {
                    myGrade += 10 * mySkills.Split().Length - 10;
                }
            }

            return myGrade;
        }

        public async Task<int> AddCvAsync(CV cv)
        {
            _context.CVs.Add(cv);
            await _context.SaveChangesAsync();
            return cv.CvId;
        }

        public async Task<List<CV>> GetCvs()
        {
            return await _context.CVs
            .Where(r => !r.IsDeleted)
            .Select(x => new CV
            {
                CvId = x.CvId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DateOfBirth = x.DateOfBirth,
                Nationality = x.Nationality,
                Gender = x.Gender,
                Skills = x.Skills,
                Email = x.Email,
                Photo = x.Photo
            })
            .ToListAsync();
        }

        public async Task<CV> GetCv(int Id)
        {
            return await _context.CVs
           .Where(r => !r.IsDeleted)
           .Where(r => r.CvId == Id)
           .Select(x => new CV
           {
               CvId = x.CvId,
               FirstName = x.FirstName,
               LastName = x.LastName,
               DateOfBirth = x.DateOfBirth,
               Nationality = x.Nationality,
               Gender = x.Gender,
               Skills = x.Skills,
               Email = x.Email,
               Photo = x.Photo
           }).SingleOrDefaultAsync();
        }

        public async Task DeleteCv(int CvId)
        {
            Console.WriteLine("Cvid = " + CvId);
            var cv = await _context.CVs.FindAsync(CvId);
            Console.WriteLine("name :" + cv.FirstName);
            if (cv is null)
            {
                throw new Exception("Unable to find CV");
            }
            cv.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<int> EditCv(CV cv)
        {
            var OldCv = await _context.CVs.FindAsync(cv.CvId);
            if (OldCv == null)
            {
                throw new Exception("Unable to find the CV");
            }
            if (OldCv.IsDeleted)
            {
                throw new Exception("Unable to update a deleted CV");
            }
            OldCv.FirstName = cv.FirstName;
            OldCv.LastName = cv.LastName;
            OldCv.DateOfBirth = cv.DateOfBirth;
            OldCv.Nationality = cv.Nationality;
            OldCv.Gender = cv.Gender;
            OldCv.Skills = cv.Skills;
            OldCv.Email = cv.Email;
            OldCv.Photo = cv.Photo;
            await _context.SaveChangesAsync();
            return OldCv.CvId;
        }
        public async Task<List<CV>> Search(string key)
        {
            return await _context.CVs
            .Where(r => !r.IsDeleted)
            .Where(r => r.FirstName.StartsWith(key) || r.LastName.StartsWith(key) || r.Email.StartsWith(key) || r.Gender.StartsWith(key) )
            .Select(x => new CV
            {
                CvId = x.CvId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DateOfBirth = x.DateOfBirth,
                Nationality = x.Nationality,
                Gender = x.Gender,
                Skills = x.Skills,
                Email = x.Email,
                Photo = x.Photo
            })
            .ToListAsync();
        }
    }
}
