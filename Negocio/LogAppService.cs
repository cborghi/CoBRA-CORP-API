using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;

namespace CoBRA.Application
{
    public class LogAppService : ILogAppService
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;

        public LogAppService(IMapper mapper, ILogRepository logRepository)
        {
            _mapper = mapper;
            _logRepository = logRepository;
        }

        public void GravarLog(LogViewModel log)
        {
            if(log.Erro != null)
            {
                log.Descricao += $". Detalhes adicionais de erro: {log.Erro.Message} - {log.Erro.StackTrace}";
            }

            var logMap = _mapper.Map<Log>(log);
            _logRepository.GravarLog(logMap);
        }

    }
}
