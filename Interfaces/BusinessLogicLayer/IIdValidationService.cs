
namespace Interfaces.BusinessLogicLayer
{
    public interface IIdValidationService
    {
        Task<List<string>> GetAllIdsAsync();
    }
}