using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffshoreTrack.Models
{
    public class ParteSolta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_partesolta { get; set; }

        public string? partesolta { get; set; }

        public int? quantidade { get; set; }

        public string? anexo { get; set; }

        public int? id_oc { get; set; }
        public OrdemCompra? oc { get; set; }

        public int? id_material { get; set; }
        public Material? material { get; set; }

        //Relacionamentos
        public List<OrdemCompra>? ordemCompras { get; set; }

    }
}

