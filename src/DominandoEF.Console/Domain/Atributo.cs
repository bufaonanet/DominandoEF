using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DominandoEF.Domain
{
    [Table("TabelaAtriburos")]
    [Index(nameof(Descricao), nameof(Id), IsUnique = true)]
    [Comment("Meu comentário da tabela")]
    public class Atributo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("MinhaDescricao", TypeName = "varchar(255)")]
        [Comment("Meu comentário do campo")]
        public string Descricao { get; set; }

        [MaxLength(255)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string Observacao { get; set; }
    }

    public class Aeroporto
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        [NotMapped]
        public string PropTeste { get; set; }

        [InverseProperty("AeroportoDePartida")]
        public List<Voo> VoosDePartida { get; set; }

        [InverseProperty("AeroportoDeChegada")]
        public List<Voo> VoosDeChegada { get; set; }
    }

    [NotMapped]
    public class Voo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Aeroporto AeroportoDePartida { get; set; }
        public Aeroporto AeroportoDeChegada { get; set; }
    }

    [Keyless]
    public class RelatorioFinanceiro
    {
        public string Descricao { get; set; }
        public decimal Total { get; set; }
        public DateTime Data { get; set; }
    }


}
