using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffshoreTrack.Models
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_cliente { get; set; }

        public string? cliente { get; set; }

        public string? razaoSocial { get; set; }

        public string? cnpj { get; set; }

        public string? endereco { get; set; }

        public string? telefone { get; set; }


        //Relacionamentos
        public List<Material> materials { get; set; } = new List<Material>();
        public List<Local> locals { get; set; } = new List<Local>();
        public List<Contrato>? contratos { get; set; }

    }
}

