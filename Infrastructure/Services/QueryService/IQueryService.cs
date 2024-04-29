using Domain.DTO_s.CourseDTO;
using Domain.DTO_s.MentorDTO;
using Domain.DTO_s.StudentDTO;
using Domain.Filters;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.QueryService
{
    public interface IQueryService
    {
        Task<PageResponse<List<GetCourseDto>>>GetCourseWithANameAsync(EmptyFilter filter); 
        Task<PageResponse<List<GetCourseDto>>>GetCourseWithBothGenderAync(EmptyFilter filter);
        Task<PageResponse<List<GetStudentDto>>> GetStudentWithHighPoints(EmptyFilter filter); 
        Task<PageResponse<List<GetCourseDto>>>GetCourseWhereManmoreWomen(EmptyFilter filter);
        Task<PageResponse<List<GetMentorDto>>>GetMentorsinSpecificDay(EmptyFilter filter); 
        Task<PageResponse<List<GetCourseDto>>>GetCoursesWithAvgAgeStudent(EmptyFilter filter);

    }
}
