using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffshoreTrack.Models
{
    public class ViewModel
    {

        // DTOs
        public List<Certificacao> certificacaoParaVencer { get; set; }
        public List<Manutencao> ultimasManutencoes { get; set; }
        public List<OrdemCompra> ordemCompraAtrasado { get; set; }
        public List<Contrato> contratosParaVencer { get; set; }
        public List<OrdemCompra> ultimasOC { get; set; }


    }
}
