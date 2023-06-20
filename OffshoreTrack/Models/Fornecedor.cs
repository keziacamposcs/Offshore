using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffshoreTrack.Models
{
    public class Fornecedor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_fornecedor { get; set; }

        public string? fornecedor { get; set; }

        public string? cnpj { get; set; }

        public string? endereco { get; set; }

        public string? telefone { get; set; }

        public string? email { get; set; }

        public string? vendedor { get; set; }

        //Relacionamentos
        public List<Manutencao>? manutencaos { get; set; }
        public List<Material>? materials { get; set; }

    }
}

