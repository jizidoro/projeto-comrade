#region

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using comrade.Domain.Bases;

#endregion

namespace comrade.Domain.Models
{
    [Table("USSI_USUARIO_SISTEMA")]
    public class UsuarioSistema : Entity
    {
        [Column("USSI_TX_NOME", TypeName = "varchar")]
        [MaxLength(255)]
        [Required(ErrorMessage = "NOME USU is required")]
        public string Nome { get; set; } // varchar(255), not null

        [Column("USSI_TX_EMAIL", TypeName = "varchar")]
        [MaxLength(255)]
        public string Email { get; set; } // varchar(255), null

        [Column("USSI_TX_SENHA", TypeName = "varchar")]
        [MaxLength(1023)]
        [Required(ErrorMessage = "SENHA is required")]
        public string Senha { get; set; } // varchar(1023), not null

        [Column("USSI_ST_SITUACAO", TypeName = "int")]
        [Required(ErrorMessage = "STS USU is required")]
        public bool Situacao { get; set; } // int, not null

        [Column("USSI_TX_MATRICULA", TypeName = "varchar")]
        [MaxLength(255)]
        [Required(ErrorMessage = "MATRICULA is required")]
        public string Matricula { get; set; } // varchar(255), not null

        [Column("USSI_DT_REGISTRO", TypeName = "varchar")]
        [Required(ErrorMessage = "DataRegistro is required")]
        public DateTime DataRegistro { get; set; }

        public override string Value => Nome;
    }
}