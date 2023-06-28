using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffshoreTrack.Models
{
    public class Criticidade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_criticidade { get; set; }
        public bool? Deletado { get; set; }

        public string? criticidade { get; set; }

        //Relacionamentos
        public List<Manutencao>? manutencaos { get; set; }
        public List<Material>? materials { get; set; }

    }
}

