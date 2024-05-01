using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Models.Enums;
using TrilhaApiDesafio.Repository.Interfaces;

namespace TrilhaApiDesafio.Repository
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly DbSet<Assignment> _assignments;
        private readonly OrganizerContext _context;

        public AssignmentRepository(OrganizerContext context)
        {
            _context = context;
            _assignments = _context.Set<Assignment>(); 
        }

        public async Task<Assignment> Create(Assignment assignment)
        {
            await _assignments.AddAsync(assignment);
            await _context.SaveChangesAsync();
            return assignment;
        }

        public async Task<bool> Delete(int id)
        {
            var assignment = await _assignments.FindAsync(id);
            if (assignment == null)
                return false;

            _assignments.Remove(assignment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Assignment>> GetAll()
        {
            return await _assignments.ToListAsync();
        }

        public async Task<IEnumerable<Assignment>> GetByDate(DateTime date)
        {
            return await _assignments.Where(a => a.Date == date).ToListAsync();
        }

        public async Task<Assignment> GetById(int id)
        {
            return await _assignments.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Assignment>> GetByStatus(AssignmentStatus status)
        {
            return await _assignments.Where(a => a.Status == status).ToArrayAsync();
        }

        public async Task<IEnumerable<Assignment>> GetByTitle(string title)
        {
            return await _assignments.Where(a => a.Title == title).ToArrayAsync();
        }

        public async Task<Assignment> Update(int id, Assignment updatedAssignment)
        {
            var existingAssignment = await _assignments.FindAsync(id);
            if (existingAssignment == null)
                return null;

            updatedAssignment.Id = id;

            _context.Entry(existingAssignment).CurrentValues.SetValues(updatedAssignment);

            await _context.SaveChangesAsync();
            return existingAssignment;
        }
    }
}
