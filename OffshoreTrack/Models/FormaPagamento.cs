using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffshoreTrack.Models
{
    public class FormaPagamento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_formaPagamento { get; set; }
        public bool? Deletado { get; set; }

        public string? formaPagamento { get; set; }

        public string? descricao { get; set; }

        //Relacionamentos
        public List<OrdemCompra>? ordemCompras { get; set; }
    }
}

