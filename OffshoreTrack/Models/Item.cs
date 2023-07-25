using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffshoreTrack.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_item { get; set; }

        public string? item { get; set; }
        public double? valor { get; set; }
        public int? quantidade { get; set; }

        public int? id_oc { get; set; }
        public OrdemCompra? ordemCompra { get; set; }
    }
}