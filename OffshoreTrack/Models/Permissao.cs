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

    public string? nome_permissao { get; set; }

    public bool pode_criar { get; set; }

    public bool pode_ler { get; set; }

    public bool pode_atualizar { get; set; }

    public bool pode_deletar { get; set; }

    // Relacionamentos
    public List<Usuario>? usuarios { get; set; }
}

}

