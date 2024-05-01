using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Models.Enums;
using TrilhaApiDesafio.Repository.Interfaces;
using TrilhaApiDesafio.Services.Interfaces;

namespace TrilhaApiDesafio.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _repository;
        private readonly ILogger<AssignmentService> _logger;

        public AssignmentService(IAssignmentRepository repository, ILogger<AssignmentService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Assignment> Create(Assignment assignment)
        {
            var createdAssignment = await _repository.Create(assignment);
            _logger.LogInformation("Assignment created successfully: {Id}", createdAssignment.Id);
            return createdAssignment;
        }

        public async Task<bool> Delete(int id)
        {
            var deleted = await _repository.Delete(id);
            if (deleted)
            {
                _logger.LogInformation("Assignment deleted successfully: {Id}", id);
            }
            else
            {
                _logger.LogWarning("Attempt to delete non-existent assignment: {Id}", id);
            }
            return deleted;
        }

        public async Task<IEnumerable<Assignment>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<Assignment>> GetByDate(DateTime date)
        {
            return await _repository.GetByDate(date);
        }

        public async Task<Assignment> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<Assignment>> GetByStatus(AssignmentStatus status)
        {
            return await _repository.GetByStatus(status);
        }

        public async Task<IEnumerable<Assignment>> GetByTitle(string title)
        {
            return await _repository.GetByTitle(title);
        }

        public async Task<Assignment> Update(int id, Assignment assignment)
        {
            return await _repository.Update(id, assignment);
        }
    }
}
