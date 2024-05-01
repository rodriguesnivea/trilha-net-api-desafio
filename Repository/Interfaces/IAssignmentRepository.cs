using System.Threading.Tasks;
using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Models.Enums;

namespace TrilhaApiDesafio.Repository.Interfaces
{
    public interface IAssignmentRepository
    {
        Task<Assignment> GetById(int id);
        Task<IEnumerable<Assignment>> GetAll();
        Task<IEnumerable<Assignment>> GetByTitle(string title);
        Task<IEnumerable<Assignment>> GetByDate(DateTime date);
        Task<IEnumerable<Assignment>> GetByStatus(AssignmentStatus status);
        Task<Assignment> Create(Assignment assignment);
        Task<Assignment> Update(int id, Assignment assignment);
        Task<bool> Delete(int id);
    }
}
