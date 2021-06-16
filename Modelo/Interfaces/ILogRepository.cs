using CoBRA.Domain.Entities;

namespace CoBRA.Domain.Interfaces
{
    public interface ILogRepository
    {
        void GravarLog(Log log);
    }
}
