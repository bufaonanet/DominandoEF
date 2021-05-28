using System;
using System.Collections.Generic;

namespace DominandoEF.Tips.Domain
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadstro { get; set; }
        public List<Colaborador> Colaboradores { get; set; }
    }
}
