using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffshoreTrack.Models
{
    public class Contrato
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_contrato { get; set; }
        public string? contrato { get; set; }
        public DateTime dataInicio { get; set; }
        public DateTime? dataFim { get; set; }
        public byte[]? anexo { get; set; }

        // Relacionamentos
        public int? id_fornecedor { get; set; }
        public Fornecedor? fornecedor { get; set; }

        public int? id_cliente { get; set; }
        public Cliente? cliente { get; set; }

        public int? id_status { get; set; }
        public Status? status { get; set; }

        public int? id_setor { get; set; }
        public Setor? setor { get; set; }
    }
}

