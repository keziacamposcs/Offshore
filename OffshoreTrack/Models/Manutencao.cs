using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffshoreTrack.Models
{
    public class Manutencao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_manutencao { get; set; }

        public string? manutencao { get; set; }
        public string? descricao { get; set; }
        public int? id_tipo { get; set; }
        public Tipo? tipo { get; set; }

        public int? id_material { get; set; }
        public Material? material { get; set; }

        public int? id_setor { get; set; }
        public Setor? setor { get; set; }

        public int? id_fornecedor { get; set; }
        public Fornecedor? fornecedor { get; set; }

        public int? id_criticidade { get; set; }
        public Criticidade? criticidade { get; set; }

        //Relacionamentos
    }
}

