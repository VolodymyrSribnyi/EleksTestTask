using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DTOs;
using WebApi.Infrastructure;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/student
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _context.Students
                .AsNoTracking()
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    StudentFirstName = s.StudentFirstName,
                    StudentLastName = s.StudentLastName
                })
                .ToListAsync();

            return Ok(students);
        }

        // GET: api/student/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                throw new NotFoundException($"Student with id {id} not found");

            var dto = new StudentDto
            {
                Id = student.Id,
                StudentFirstName = student.StudentFirstName,
                StudentLastName = student.StudentLastName
            };
            return Ok(dto);
        }

        // POST: api/student
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentCreateUpdateDto dto)
        {
            var student = new Student
            {
                StudentFirstName = dto.StudentFirstName,
                StudentLastName = dto.StudentLastName
            };

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            var createdStudentDto = new StudentDto
            {
                Id = student.Id,
                StudentFirstName = student.StudentFirstName,
                StudentLastName = student.StudentLastName
            };

            // 4. Повертаємо 201 Created з повним DTO
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, createdStudentDto);
        }

        // PUT: api/student/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] StudentCreateUpdateDto dto)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                throw new NotFoundException($"Student with id {id} not found");

            student.StudentFirstName = dto.StudentFirstName;
            student.StudentLastName = dto.StudentLastName;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/student/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                throw new NotFoundException($"Student with id {id} not found");

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
