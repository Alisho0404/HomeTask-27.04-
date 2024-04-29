using Domain.DTO_s.CourseDTO;
using Domain.DTO_s.MentorDTO;
using Domain.DTO_s.StudentDTO;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Services.QueryService;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/queries")]
    public class QueryController:ControllerBase
    { 
        private readonly IQueryService _queryService;
        public QueryController(IQueryService queryService)
        {
            _queryService = queryService;
        }
        [HttpGet("Course with average age")] 
        public async Task<PageResponse<List<GetCourseDto>>> GetCoursesWithAvgAgeStudentAsync(EmptyFilter filter)
        {
            return await _queryService.GetCourseWithANameAsync(filter);
        }

        [HttpGet("Course with comparing man and women")]
        public async Task<PageResponse<List<GetCourseDto>>> GetCourseWhereManmoreWomen(EmptyFilter filter)
        {
            return await _queryService.GetCourseWhereManmoreWomen(filter);
        }

        [HttpGet("Course with student starts with A")]
        public async Task<PageResponse<List<GetCourseDto>>> GetCourseWithANameAsync(EmptyFilter filter)
        {
            return await _queryService.GetCourseWithANameAsync(filter);
        }

        [HttpGet("Course with both gender")]
        public async Task<PageResponse<List<GetCourseDto>>> GetCourseWithBothGenderAync(EmptyFilter filter)
        {
            return await _queryService.GetCourseWithBothGenderAync(filter);
        }

        [HttpGet("Mentors in specific day")]
        public async Task<PageResponse<List<GetMentorDto>>> GetMentorsinSpecificDayAsync(EmptyFilter filter)
        {
            return await _queryService.GetMentorsinSpecificDay(filter);
        }

        [HttpGet("Student with high points")]
        public async Task<PageResponse<List<GetStudentDto>>> GetStudentWithHighPoints(EmptyFilter filter)
        {
            return await _queryService.GetStudentWithHighPoints(filter);
        }
    }
}
