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
        public bool? Deletado { get; set; }

        public string? partesolta { get; set; }

        public string? descricao { get; set; }
        public string? dimensoes { get; set; } 

        public string? peso { get; set; }

        public int? quantidade { get; set; }

        public byte[]? anexo { get; set; }

        public int? id_oc { get; set; }
        public OrdemCompra? oc { get; set; }

        public int? id_material { get; set; }
        public Material? material { get; set; }

        public int? id_fornecedor { get; set; }
        public Fornecedor? fornecedor { get; set; }

        public int? id_status { get; set; }
        public Status? status { get; set; }

        public int? id_local { get; set; }
        public Local? local { get; set; }

        public int? id_certificacao { get; set; }
        public Certificacao? certificacao { get; set; }

        //Relacionamentos
        public List<Material>? materials { get; set; }

        public List<AtividadeLogPS>? atividadeLogsPS { get; set; }

    }
}

