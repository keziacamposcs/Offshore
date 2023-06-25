using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffshoreTrack.Models
{
    public class AtividadeLogPS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_atividadeLogPs { get; set; }
        public string acao { get; set; }
        public DateTime Timestamp { get; set; }

        public int? id_usuario { get; set; }
        public Usuario? usuario { get; set; }

        public int? id_parteSolta { get; set; }
        public ParteSolta? parteSolta { get; set; }

    }
}