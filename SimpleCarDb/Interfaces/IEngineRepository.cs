using SimpleCarDb.Models;

namespace SimpleCarDb.Interfaces
{
    public interface IEngineRepository
    {
        Task<IEnumerable<EngineDetail>> GetAll();
        Task<EngineDetail?> Get(int id);
        Task<EngineDetail> Create(EngineDetail theEngine);
        Task<EngineDetail?> Update(int id, EngineDetail theEngine);
        Task<bool> Delete(int id);
    }
}
