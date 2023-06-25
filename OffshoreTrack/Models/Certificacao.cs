using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffshoreTrack.Models
{
    public class Certificacao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_certificacao { get; set; }

        public string? certificacao { get; set; }

        public string? orgaoEmissor { get; set; } 

        public DateTime dataEmissao { get; set; } 

        public DateTime? dataValidade { get; set; } 

        public byte[]? anexo { get; set; }

        // Relacionamentos
        public List<Material>? materials { get; set; }
        public List<ParteSolta>? parteSoltas { get; set; }

    }
}
