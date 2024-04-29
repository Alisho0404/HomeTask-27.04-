using AutoMapper;
using Domain.DTO_s.CourseDTO;
using Domain.DTO_s.MentorDTO;
using Domain.DTO_s.StudentDTO;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Domain.Enteties;

namespace Infrastructure.Services.QueryService
{
    public class QueryService : IQueryService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public QueryService(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper=mapper;
        }
        public async Task<PageResponse<List<GetCourseDto>>> GetCoursesWithAvgAgeStudent(EmptyFilter filter)
        {
            try
            {
                var average = 25;
                var course = await (from c in _context.Courses 
                                    join g in _context.Groups on c.Id equals g.CourseId
                                    join sg in _context.StudentGroups on g.Id equals sg.GroupId 
                                    join s in _context.Students on sg.StudentId equals s.Id
                                    let avg=_context.StudentGroups.Average(x=>x.StudentId)
                                    where  avg>average
                                    select new GetCourseDto
                                    {
                                        CourseName=c.CourseName,
                                        Description=c.Description
                                    }).ToListAsync();

                var response = course
                   .Skip((filter.PageNumber - 1) * filter.PageSize)
                   .Take(filter.PageSize).ToList();
                var totalRecord = course.Count();
                return new PageResponse<List<GetCourseDto>>(response, filter.PageNumber, filter.PageSize, totalRecord);

            }
            catch (Exception e)
            {

                return new PageResponse<List<GetCourseDto>>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<PageResponse<List<GetCourseDto>>> GetCourseWhereManmoreWomen(EmptyFilter filter)
        {
            try
            {
                var courses=await(from c in _context.Courses
                                  join g in _context.Groups on c.Id equals g.CourseId 
                                  join sg in _context.StudentGroups on g.Id equals sg.GroupId 
                                  join s in _context.Students on sg.StudentId equals s.Id 
                                  let menCount=_context.Students.Count(x=>x.Gender==Domain.Enum_s.Gender.Male)
                                  let womenCount=_context.Students.Count(x=>x.Gender==Domain.Enum_s.Gender.Female)
                                  where menCount>womenCount
                                  select new GetCourseDto
                                  {
                                      Id=c.Id,
                                      CourseName=c.CourseName,
                                      Description=c.Description
                                      
                                  }).ToListAsync();
                var response = courses
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize).ToList();
                var totalRecord = courses.Count();
                return new PageResponse<List<GetCourseDto>>(response, filter.PageNumber, filter.PageSize, totalRecord);

            }
            catch (Exception e)
            {

                return new PageResponse<List<GetCourseDto>>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<PageResponse<List<GetCourseDto>>> GetCourseWithANameAsync(EmptyFilter filter)
        {
            try
            {
                var courses = await (from c in _context.Courses
                                     join g in _context.Groups on c.Id equals g.CourseId
                                     join sg in _context.StudentGroups on g.Id equals sg.GroupId
                                     join s in _context.Students on sg.StudentId equals s.Id
                                     where s.FirstName.StartsWith("A")
                                     select new GetCourseDto
                                     { 
                                         Id=c.Id,
                                         CourseName=c.CourseName,
                                         Description=c.Description,
                                         Status=c.Status

                                     }).ToListAsync();
                var response = courses
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize).ToList();
                var totalRecord = courses.Count(); 
                return new PageResponse<List<GetCourseDto>>(response,filter.PageNumber,filter.PageSize,totalRecord);
            }
            catch (Exception e)
            {

                return new PageResponse<List<GetCourseDto>>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<PageResponse<List<GetCourseDto>>> GetCourseWithBothGenderAync(EmptyFilter filter)
        {
            try
            {
                var courses = await(from c in _context.Courses
                                    join g in _context.Groups on c.Id equals g.CourseId
                                    join sg in _context.StudentGroups on g.Id equals sg.GroupId
                                    join s in _context.Students on sg.StudentId equals s.Id
                                    where s.Gender==Domain.Enum_s.Gender.Male
                                    where s.Gender==Domain.Enum_s.Gender.Female
                                    select new GetCourseDto
                                    {
                                        Id = c.Id,
                                        CourseName = c.CourseName,
                                        Description = c.Description,
                                        Status = c.Status

                                    }).ToListAsync();
                var response = courses
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize).ToList();
                var totalRecord = courses.Count();
                return new PageResponse<List<GetCourseDto>>(response, filter.PageNumber, filter.PageSize, totalRecord);
            }
            catch (Exception e)
            {

                return new PageResponse<List<GetCourseDto>>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<PageResponse<List<GetMentorDto>>> GetMentorsinSpecificDay(EmptyFilter filter)
        {
            string dayOfWeek = "DayOfWeek";
            try
            {
                var mentors = await (from m in _context.Mentors
                                     join mg in _context.MentorsGroups on m.Id equals mg.MentorId
                                     join g in _context.Groups on mg.GroupId equals g.Id
                                     join t in _context.TimeTables on g.Id equals t.GroupId
                                     where t.DayOfWeek.ToString() == dayOfWeek
                                     select new GetMentorDto {
                                         FirstName = m.FirstName,
                                         LastName = m.LastName,
                                         Phone = m.Phone,
                                         Email = m.Email,
                                         Address = m.Address
                                     }).ToListAsync();

                var response = mentors
                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                  .Take(filter.PageSize).ToList();
                var totalRecord = mentors.Count();
                return new PageResponse<List<GetMentorDto>>(response, filter.PageNumber, filter.PageSize, totalRecord);

            }
            catch (Exception e)
            {

                return new PageResponse<List<GetMentorDto>>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<PageResponse<List<GetStudentDto>>> GetStudentWithHighPoints(EmptyFilter filter)
        {
            try
            {
                var students = await (from s in _context.Students
                                      join pb in _context.ProgressBooks on s.Id equals pb.StudentId
                                      where pb.Grade > 90
                                      select new GetStudentDto
                                      {
                                          Id = s.Id,
                                          FirstName = s.FirstName,
                                          LastName = s.LastName,
                                          Phone = s.Phone,
                                          Email = s.Email,
                                          Address = s.Address,
                                          DoB = s.DoB
                                      }).ToListAsync();

                var response = students
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize).ToList();
                var totalRecord = students.Count(); 
                return new PageResponse<List<GetStudentDto>>(response,filter.PageNumber,filter.PageSize,totalRecord);

            }
            catch (Exception e)
            {

                return new PageResponse<List<GetStudentDto>>(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
