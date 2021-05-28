using System.Net;

namespace DominandoEF.Domain
{
    public class Conversor
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public bool Excluido { get; set; }
        public Versao Versao { get; set; }
        public IPAddress EnderecoIP { get; set; }
        public Status Status { get; set; }
    }

    public enum Status
    {
        Analise,
        Enviado,
        Devolvido
    }

    public enum Versao
    {
        ECore1,
        ECore2,
        ECore3,
        ECore5
    }
}
