using System;

namespace GestioneCantieri2._0.Models
{
    public class TblCantieriModel
    {
        public int IdCantieri { get; set; } = 0;
        public int? IdTblClienti { get; set; } = null;
        public DateTime? Data { get; set; } = null;
        public string CodCant { get; set; } = null;
        public string DescriCodCAnt { get; set; } = null;
        public string Indirizzo { get; set; } = null;
        public string Città { get; set; } = null;
        public int? Ricarico { get; set; } = null;
        public decimal? PzzoManodopera { get; set; } = null;
        public bool? Chiuso { get; set; } = null;
        public bool? Riscosso { get; set; } = null;
        public int Numero { get; set; } = 0;
        public decimal? ValorePreventivo { get; set; } = null;
        public int? IVA { get; set; } = null;
        public int? Anno { get; set; } = null;
        public bool? Preventivo { get; set; } = null;
        public int? FasciaTblCantieri { get; set; } = null;
        public bool? DaDividere { get; set; } = null;
        public bool? Diviso { get; set; } = null;
        public bool? Fatturato { get; set; } = null;
        public string CodRiferCant { get; set; } = null;
        public DateTime? Doi { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime? Dou { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
