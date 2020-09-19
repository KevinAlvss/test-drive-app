using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("tb_carro")]
    public partial class TbCarro
    {
        public TbCarro()
        {
            TbAgendamento = new HashSet<TbAgendamento>();
        }

        [Key]
        [Column("id_carro")]
        public int IdCarro { get; set; }
        [Column("nm_carro", TypeName = "varchar(100)")]
        public string NmCarro { get; set; }
        [Column("nm_marca", TypeName = "varchar(100)")]
        public string NmMarca { get; set; }
        [Column("ds_modelo", TypeName = "varchar(100)")]
        public string DsModelo { get; set; }
        [Column("nr_ano")]
        public int? NrAno { get; set; }
        [Column("ds_placa", TypeName = "varchar(100)")]
        public string DsPlaca { get; set; }

        [InverseProperty("IdCarroNavigation")]
        public virtual ICollection<TbAgendamento> TbAgendamento { get; set; }
    }
}
