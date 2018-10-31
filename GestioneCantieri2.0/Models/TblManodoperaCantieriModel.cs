using System;

namespace GestioneCantieri2._0.Models
{
    public class TblManodoperaCantieriModel
    {
        public int IdManodopera { get; set; } = 0;
        public int? IdTbCantieri { get; set; } = null;
        public decimal? Qta { get; set; } = null;
        public string DescriManodopera { get; set; } = null;
        public DateTime? Data { get; set; } = null;
        public int? IdTblOperaio { get; set; } = null;
        public bool? Pagato { get; set; } = null;
        public string Con { get; set; } = null;
        public bool? Visibile { get; set; } = null;
        public string Mano { get; set; } = null;
        public string Note { get; set; } = null;
        public string Note2 { get; set; } = null;
        public string Acquirente { get; set; } = null;
        public int? ProtocolloInterno { get; set; } = null;
        public DateTime? Doi { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime? Dou { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
