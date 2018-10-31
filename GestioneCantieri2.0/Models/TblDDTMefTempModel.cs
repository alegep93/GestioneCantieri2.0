using System;

namespace GestioneCantieri2._0.Models
{
    public class TblDDTMefTempModel
    {
        public int IdDDTMef { get; set; } = 0;
        public int? Anno { get; set; } = null;
        public DateTime? Data { get; set; } = null;
        public int? NDDT { get; set; } = null;
        public string CodArt { get; set; } = null;
        public string DescriCodArt { get; set; } = null;
        public int? Qta { get; set; } = null;
        public decimal? Importo { get; set; } = null;
        public string Acquirente { get; set; } = null;
        public decimal? PrezzoUnitario { get; set; } = null;
        public int? AnnoNDDT { get; set; } = null;
        public DateTime? Doi { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime? Dou { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
