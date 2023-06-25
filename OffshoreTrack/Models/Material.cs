using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffshoreTrack.Models
{
    public class Material
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_material { get; set; }

        public string? material { get; set; }
        public string? descricao { get; set; }

        public string? dimensoes { get; set; } 

        public string? peso { get; set; }

        public DateTime? dataFabricacao { get; set; }

        public DateTime? dataValidade { get; set; }

        public string? numeroSerie { get; set; }

        public string? qrcode { get; set; }
        public byte[]? anexo { get; set; }

        public int? id_tipo { get; set; }
        public Tipo? tipo { get; set; }

        public int? id_criticidade { get; set; }
        public Criticidade? criticidade { get; set; }

        public int? id_setor { get; set; }
        public Setor? setor { get; set; }

        public int? id_cliente { get; set; }
        public Cliente? cliente { get; set; }

        public int? id_local { get; set; }
        public Local? local { get; set; }

        public int? id_usuario { get; set; }
        public Usuario? usuario { get; set; }

        public int? id_fornecedor { get; set; }
        public Fornecedor? fornecedor { get; set; }

        public int? id_manutencao { get; set; }
        public Manutencao? manutencao { get; set; }

        public int? id_partesolta { get; set; }
        public ParteSolta? parteSolta { get; set; }

        public int? id_ordemcompra { get; set; }
        public OrdemCompra? ordemCompra { get; set; }

        public int? id_status { get; set; }
        public Status? status { get; set; }

        public int? id_certificacao { get; set; }
        public Certificacao? certificacao { get; set; }


        //Relacionamentos
        public List<Manutencao>? manutencaos { get; set; }
        public List<ParteSolta>? parteSoltas { get; set; }
        public List<AtividadeLog>? atividadeLogs { get; set; }

        public List<Certificacao>? certificacaos { get; set; }
    }
}