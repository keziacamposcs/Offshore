using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffshoreTrack.Models
{
    public class AtividadeLog
    {
        public int Id { get; set; }
        public string Acao { get; set; }
        public DateTime Timestamp { get; set; }

        public int? id_usuario { get; set; }
        public Usuario? usuario { get; set; }

        public int? id_material { get; set; }
        public Material material { get; set; }

    }
}