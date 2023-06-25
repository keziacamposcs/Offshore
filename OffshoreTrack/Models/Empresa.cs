using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffshoreTrack.Models
{
    public class Empresa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_empresa { get; set; }

        public string? empresa { get; set; }

        public string? razaoSocialEmpresa { get; set; }

        public string? cnpjEmpresa { get; set; }

        public string? inscricaoEstadualEmpresa { get; set; }

        public string? inscricaoMunicipalEmpresa { get; set; }

        public string? enderecoEmpresa { get; set; }

        public string? telefoneEmpresa { get; set; }

        public string? emailEmpresa { get; set; }

        public string? responsavelEmpresa { get; set; }

        public byte[]? logoEmpresa { get; set; }

        public List<OrdemCompra>? ordemCompras { get; set; }

    }
}

