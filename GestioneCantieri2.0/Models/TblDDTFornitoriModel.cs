using System;

namespace GestioneCantieri2._0.Models
{
    public class TblDDTFornitoriModel
    {
        public int Id { get; set; } = 0;
        public int IdFornitore { get; set; } = 0;
        public DateTime Data { get; set; } = DateTime.Today;
        public long Protocollo { get; set; } = 0;
        public string NumeroDDT { get; set; } = "";
        public string Articolo { get; set; } = "";
        public string DescrizioneFornitore { get; set; } = null;
        public string DescrizioneMau { get; set; } = null;
        public int Qta { get; set; } = 0;
        public decimal Valore { get; set; } = 0;
        public DateTime? Doi { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime? Dou { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
