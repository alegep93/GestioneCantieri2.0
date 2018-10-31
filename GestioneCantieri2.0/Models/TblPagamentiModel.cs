using System;

namespace GestioneCantieri2._0.Models
{
    public class TblPagamentiModel
    {
        public int IdPagamenti { get; set; } = 0;
        public int? IdTblCantieri { get; set; } = null;
        public DateTime? Data { get; set; } = null;
        public decimal? Imporo { get; set; } = null;
        public string DescriPagamenti { get; set; } = null;
        public bool? Acconto { get; set; } = null;
        public bool? Saldo { get; set; } = null;
        public DateTime? Doi { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime? Dou { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
