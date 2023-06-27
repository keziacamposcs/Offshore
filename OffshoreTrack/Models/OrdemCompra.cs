using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffshoreTrack.Models
{
    public class OrdemCompra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_oc { get; set; }
        public string? oc { get; set; }
        public string? observacao { get; set; }
        public string? moeda { get; set; }
        public string? item1 { get; set; }
        public string? item2 { get; set; }
        public string? item3 { get; set; }
        public string? item4 { get; set; }
        public string? item5 { get; set; }

        public double? valor1 { get; set; }
        public double? valor2 { get; set; }
        public double? valor3 { get; set; }
        public double? valor4 { get; set; }
        public double? valor5 { get; set; }

        public int? quantidade1 { get; set; }
        public int? quantidade2 { get; set; }
        public int? quantidade3 { get; set; }
        public int? quantidade4 { get; set; }
        public int? quantidade5 { get; set; }


        public string? prioridade { get; set; }
        public byte[]? anexo { get; set; }

        public DateTime? data_oc { get; set; }
        public DateTime? data_prevista { get; set; }
        public DateTime? data_conclusao { get; set; }


        public int? id_fornecedor { get; set; }
        public Fornecedor? fornecedor { get; set; }

        public int? id_fornecedor2 { get; set; }
        public Fornecedor? fornecedor2 { get; set; }

        public int? id_fornecedor3 { get; set; }
        public Fornecedor? fornecedor3 { get; set; }


        public int? id_setor { get; set; }
        public Setor? setor { get; set; }

        public int? id_status { get; set; }
        public Status? status { get; set; }

         public int? id_rateio { get; set; }
        public Rateio? rateio { get; set; }

        public int? id_usuario { get; set; }
        public Usuario? usuario { get; set; }
        public int? id_empresa { get; set; }
        public Empresa? empresa { get; set; }

        //Relacionamentos
        public List<ParteSolta>? parteSoltas { get; set; }

        public List<Material>? materials { get; set; }

        // Calculos
        public double TotalValor 
        {
            get
            {
                return (double)(valor1 ?? 0) + (double)(valor2 ?? 0) + (double)(valor3 ?? 0) + (double)(valor4 ?? 0) + (double)(valor5 ?? 0);
            }
        }
        public double TotalComRateio1
        {
            get
            {
                if (this.rateio != null)
                {
                    var ratio1 = (double)(rateio.porcentagem1 ?? 0) / 100;
                    return this.TotalValor * ratio1;
                }
                else
                {
                    return this.TotalValor;
                }
            }
        }
        public double TotalComRateio2
        {
            get
            {
                if (this.rateio != null)
                {
                    var ratio2 = (double)(rateio.porcentagem2 ?? 0) / 100;
                    return this.TotalValor * ratio2;
                }
                else
                {
                    return this.TotalValor;
                }
            }
        }




    }
}
