using System;

namespace GestioneCantieri2._0.Models
{
    public class TblForitoriModel
    {
        public int IdFornitori { get; set; } = 0;
        public string RagSocForni { get; set; } = null;
        public string Indirizzo { get; set; } = null;
        public string Cap { get; set; } = null;
        public string Città { get; set; } = null;
        public int? Tel1 { get; set; } = null;
        public int? Cell1 { get; set; } = null;
        public decimal? PartitaIva { get; set; } = null;
        public string CodFiscale { get; set; } = null;
        public string Abbreviato { get; set; } = null;
        public DateTime? Doi { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime? Dou { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
