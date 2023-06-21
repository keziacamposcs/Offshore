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
        public string? prioridade { get; set; }

        public double? valor { get; set; }

        public DateOnly? data_compra { get; set; }


        public int? id_fornecedor { get; set; }
        public Fornecedor? fornecedor { get; set; }


        public int? id_fornecedor2 { get; set; }
        public Fornecedor? fornecedor2 { get; set; }


        public int? id_fornecedor3 { get; set; }
        public Fornecedor? fornecedor3 { get; set; }


        public int? id_setor { get; set; }
        public Setor? setor { get; set; }

        public int? id_parteSolta { get; set; }
        public ParteSolta? parteSolta { get; set; }

        public int? id_material { get; set; }
        public Material? material { get; set; }


        //Relacionamentos
        public List<ParteSolta>? parteSoltas { get; set; }
    }
}

