using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;

namespace CoBRA.Application
{
    public class RegraAppService : IRegraAppService
    {
        private readonly IMapper _mapper;
        private readonly IRegraRepository _regraRepository;
        
        public RegraAppService(IMapper mapper, IRegraRepository regraRepository)
        {
            _mapper = mapper;
            _regraRepository = regraRepository;
        }

        public Regra ObterRegraPorId(int id)
        {
            return _regraRepository.ObterRegraPorId(id);
        }
    }
}
