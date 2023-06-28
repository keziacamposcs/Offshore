using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffshoreTrack.Models
{
    public class Local
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_local { get; set; }
        public bool? Deletado { get; set; }

        public string? local { get; set; }

        public int? id_cliente { get; set; }
        public Cliente? Cliente { get; set; }

        //Relacionamentos
        public List<Material>? materials { get; set; }

        public List<ParteSolta>? parteSoltas { get; set; }

    }
}

