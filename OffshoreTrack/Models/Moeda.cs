using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffshoreTrack.Models
{
    public class Moeda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_moeda { get; set; }
        public bool? Deletado { get; set; }
        public string? moeda_descricao { get; set; }
        public string? simbolo { get; set; }
        public List<OrdemCompra>? ordemCompras { get; set; }
        public List<Item>? items { get; set; }

    }
}