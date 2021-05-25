#region

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using comrade.Domain.Bases;

#endregion

namespace comrade.Domain.Models
{
    [Table("AIRP_AIRPLANE")]
    public class Airplane : Entity
    {
        [Column("AIRP_TX_CODIGO", TypeName = "varchar")]
        [MaxLength(255)]
        [Required(ErrorMessage = "Codigo is required")]
        public string Codigo { get; set; }

        [Column("AIRP_TX_MODELO", TypeName = "varchar")]
        [MaxLength(255)]
        [Required(ErrorMessage = "Modelo is required")]
        public string Modelo { get; set; }

        [Column("AIRP_QT_PASSAGEIRO", TypeName = "int")]
        [Required(ErrorMessage = "QuantidadePassageiro is required")]
        public int QuantidadePassageiro { get; set; }

        [Column("AIRP_DT_REGISTRO", TypeName = "varchar")]
        [Required(ErrorMessage = "DataRegistro is required")]
        public DateTime DataRegistro { get; set; }

        public override string Value => Codigo;
    }
}