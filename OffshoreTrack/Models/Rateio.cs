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

        public string? valor1 { get; set; }

        public string? valor2 { get; set; }

        public int? id_setor1 { get; set; }
        public Setor? setor1 { get; set; }

        public int? id_setor2 { get; set; }
        public Setor? setor2 { get; set; }

        //Relacionamentos
    }
}

