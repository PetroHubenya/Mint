
namespace Interfaces.BusinessLogicLayer
{
    public interface IIdValidationService
    {
        Task<List<string>> GetAllIdsAsync();
        Task<List<string>> GetAllIdsCacheAsync();
        Task<bool> VerifyIdAsync(string id);
    }
}