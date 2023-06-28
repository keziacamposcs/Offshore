using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffshoreTrack.Models
{
    public class Tipo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_tipo { get; set; }

        public bool? Deletado { get; set; }
        public string? tipo { get; set; }

        //Relacionamentos
        public List<Material>? materials { get; set; }
        public List<Manutencao>? manutencaos { get; set; }

    }
}

