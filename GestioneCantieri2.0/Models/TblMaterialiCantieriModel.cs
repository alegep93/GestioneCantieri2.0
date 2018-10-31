using System;

namespace GestioneCantieri2._0.Models
{
    public class TblMaterialiCantieriModel
    {
        public int IdMaterialiCantiere { get; set; } = 0;
        public int? IdTblCantieri { get; set; } = null;
        public int? IdTblOperaio { get; set; } = null;
        public string DescriMateriali { get; set; } = null;
        public decimal? Qta { get; set; } = null;
        public bool? Visibile { get; set; } = null;
        public bool? Ricalcolo { get; set; } = null;
        public bool? RicaricoSiNo { get; set; } = null;
        public DateTime? Data { get; set; } = null;
        public decimal? PzzoUniCantiere { get; set; } = null;
        public bool? Rientro { get; set; } = null;
        public string CodArt { get; set; } = null;
        public string DescriCodArt { get; set; } = null;
        public string Tipologia { get; set; } = null;
        public string UnitàDiMisura { get; set; } = null;
        public string ZOldNumeroBolla { get; set; } = null;
        public string Mate { get; set; } = null;
        public int? Fascia { get; set; } = null;
        public decimal? PzzoTemp { get; set; } = null;
        public string Acquirente { get; set; } = null;
        public string Fornitore { get; set; } = null;
        public string NumeroBolla { get; set; } = null;
        public int? ProtocolloInterno { get; set; } = null;
        public string Note { get; set; } = null;
        public string Note2 { get; set; } = null;
        public decimal? PzzoFinCli { get; set; } = null;
        public bool? OperaioPagato { get; set; } = null;
        public DateTime? Doi { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime? Dou { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
