﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffshoreTrack.Models
{
    public class Setor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_setor { get; set; }
        public bool? Deletado { get; set; }

        public string? setor { get; set; }

        //Relacionamentos
        public List<Manutencao>? manutencaos { get; set; }
        public List<Material>? materials { get; set; }
        public List<Rateio>? rateios1 { get; set; }
        public List<Rateio>? rateios2 { get; set; }

        public List<OrdemCompra>? ordemCompras { get; set; }
        public List<Contrato>? contratos { get; set; }

    }
}

