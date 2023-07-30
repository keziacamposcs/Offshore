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
        public bool? Deletado { get; set; }
        public string? oc { get; set; }
        public string? observacao { get; set; }
        public string? prioridade { get; set; }
        public double? total { get; set; }

        public byte[]? anexo { get; set; }

        public DateTime? data_oc { get; set; }
        public DateTime? data_prevista { get; set; }
        public DateTime? data_conclusao { get; set; }


        public int? id_fornecedor { get; set; }
        public Fornecedor? fornecedor { get; set; }

        public int? id_fornecedor2 { get; set; }
        public Fornecedor? fornecedor2 { get; set; }

        public int? id_fornecedor3 { get; set; }
        public Fornecedor? fornecedor3 { get; set; }


        public int? id_setor { get; set; }
        public Setor? setor { get; set; }

        public int? id_status { get; set; }
        public Status? status { get; set; }

         public int? id_rateio { get; set; }
        public Rateio? rateio { get; set; }

        public int? id_usuario { get; set; }
        public Usuario? usuario { get; set; }
        public int? id_empresa { get; set; }
        public Empresa? empresa { get; set; }

        public int? id_formaPagamento { get; set; }
        public FormaPagamento? formaPagamento { get; set; }

        public int? id_moeda { get; set; }
        public Moeda? moeda { get; set; }



        //Relacionamentos
        public List<ParteSolta>? parteSoltas { get; set; }
        public List<Material>? materials { get; set; }
        public ICollection<Item>? Itens { get; set; }
    }
}
