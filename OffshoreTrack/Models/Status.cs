using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffshoreTrack.Models
{
    public class Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_status { get; set; }

        public string? status { get; set; }

        //Relacionamentos
        public List<Manutencao>? manutencaos { get; set; }
        public List<ParteSolta>? parteSoltas { get; set; }
        public List<Material>? materials { get; set; }
        public List<OrdemCompra>? ordemCompras { get; set; }

    }
}

