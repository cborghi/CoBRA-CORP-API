namespace CoBRA.Domain.Interfaces
{
    public interface IBaseRepositoryIntranet
    {
        string BuscarArquivoConsulta(string nomeArquivo);
        string Conexao { get; }
    }
}