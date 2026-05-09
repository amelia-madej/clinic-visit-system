using System.Threading.Tasks;

namespace Application.Services
{
    public interface IMedicationImportService
    {
        Task<int> ImportAsync();
    }
}
