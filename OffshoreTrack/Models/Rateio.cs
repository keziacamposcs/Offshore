using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffshoreTrack.Models
{
    public class Rateio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_rateio { get; set; }

        public string? rateio { get; set; }

        public decimal? porcentagem1 { get; set; }
        public decimal? porcentagem2 { get; set; }

        public int? id_setor1 { get; set; }
        public Setor? setor1 { get; set; }

        public int? id_setor2 { get; set; }
        public Setor? setor2 { get; set; }

        //Relacionamentos
        public List<OrdemCompra>? ordemCompras { get; set; }

    }
}

