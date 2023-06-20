﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffshoreTrack.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_usuario { get; set; }

        public string? usuario { get; set; }

        public string? senha { get; set; }

        public string? nome { get; set; }

        public string? cpf { get; set; }

        // Relacionamentos
        public List<Material>? materials { get; set; }
    }
}

