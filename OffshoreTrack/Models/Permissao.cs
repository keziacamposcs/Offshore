using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffshoreTrack.Models
{
    public class Permissao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_permissao { get; set; }
        public bool? Deletado { get; set; }

        public string? nome_permissao { get; set; }

        public bool? pode_criar { get; set; }

        public bool? pode_ler { get; set; }

        public bool? pode_atualizar { get; set; }

        public bool? pode_deletar { get; set; }

        public bool? permissao_admin { get; set; }

        // Permissoes de Paginas
        public bool? permissaoCertificado { get; set; }
        public bool? permissaoCliente { get; set; }
        public bool? permissaoContrato { get; set; }
        public bool? permissaoCriticidade { get; set; }
        public bool? permissaoFornecedor { get; set; }
        public bool? permissaoLocal { get; set; }
        public bool? permissaoManutencao { get; set; }
        public bool? permissaoMaterial { get; set; }
        public bool? permissaoOrdemCompra { get; set; }
        public bool? permissaoParteSolta { get; set; }
        public bool? permissaoRateio { get; set; }
        public bool? permissaoSetor { get; set; }
        public bool? permissaoTipo { get; set; }

        // Relacionamentos
        public List<Usuario>? usuarios { get; set; }
    }

}

