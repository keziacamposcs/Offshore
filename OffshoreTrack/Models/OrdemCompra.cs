using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffshoreTrack.Models
{
    public class OrdemCompra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_oc { get; set; }

        public string? oc { get; set; }

        public double? valor { get; set; }

        public DateOnly? data_compra { get; set; }

        //Relacionamentos
        public List<ParteSolta>? parteSoltas { get; set; }
    }
}

