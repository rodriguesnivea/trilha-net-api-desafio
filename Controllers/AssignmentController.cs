using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Models.Enums;
using TrilhaApiDesafio.Services.Interfaces;

namespace TrilhaApiDesafio.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;
        private readonly ILogger<AssignmentController> _logger;

        public AssignmentController(IAssignmentService assignmentService, ILogger<AssignmentController> logger)
        {
            _assignmentService = assignmentService;
            _logger = logger;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _assignmentService.GetById(id);
            if (result == null)
            {
                _logger.LogWarning("Assignment with ID {Id} not found", id);
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _assignmentService.GetAll();
            _logger.LogInformation("All assignments retrieved successfully");
            return Ok(result);
        }

        [HttpGet("GetByTitle")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            var result = await _assignmentService.GetByTitle(title);
            _logger.LogInformation("Assignments retrieved successfully by title: {Title}", title);
            return Ok(result);
        }

        [HttpGet("GetByDate")]
        public async Task<IActionResult> GetByDate(DateTime date)
        {
            var result = await _assignmentService.GetByDate(date);
            _logger.LogInformation("Assignments retrieved successfully by date: {Date}", date);
            return Ok(result);
        }

        [HttpGet("GetByStatus")]
        public async Task<IActionResult> GetByStatus(AssignmentStatus status)
        {
            var result = await _assignmentService.GetByStatus(status);
            _logger.LogInformation("Assignments retrieved successfully by status: {Status}", status);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Assignment assignment)
        {
            if (assignment.Date == DateTime.MinValue)
            {
                _logger.LogError("Failed to create assignment. Task date cannot be empty.");
                return BadRequest(new { Error = "Task date cannot be empty" });
            }

            var result = await _assignmentService.Create(assignment);
            _logger.LogInformation("Assignment created successfully. ID: {Id}", result.Id);
            return CreatedAtAction(nameof(GetById), new { Id = assignment.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Assignment assignment)
        {
            if (assignment.Date == DateTime.MinValue)
            {
                _logger.LogError("Failed to update assignment with ID {Id}. Task date cannot be empty.", id);
                return BadRequest(new { Error = "Task date cannot be empty" });
            }

            var result = await _assignmentService.Update(id, assignment);

            if (result == null)
            {
                _logger.LogWarning("Assignment with ID {Id} not found for update", id);
                return NotFound();
            }

            _logger.LogInformation("Assignment with ID {Id} updated successfully", id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _assignmentService.Delete(id);

            if (!success)
            {
                _logger.LogWarning("Failed to delete assignment with ID {Id}. Assignment not found", id);
                return NotFound();
            }

            _logger.LogInformation("Assignment with ID {Id} deleted successfully", id);
            return NoContent();
        }
    }
}
