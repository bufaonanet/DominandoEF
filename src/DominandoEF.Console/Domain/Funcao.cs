using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominandoEF.Domain
{
    public class Funcao
    {
        public int Id { get; set; }

        [Column(TypeName ="nvarchar(100)")]
        public string Descricao1 { get; set; }

        [Column(TypeName ="varchar(100)")]
        public string Descricao2 { get; set; }

        public DateTime Data1 { get; set; }
        public string Data2 { get; set; }
    }
}
