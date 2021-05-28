using System.Collections.Generic;

namespace DominandoEF.Domain
{
    public class Filme
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public ICollection<Ator> Atores { get; } = new List<Ator>();

    }

    public class Ator
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Filme> Filmes { get; } = new List<Filme>();
    }
}
