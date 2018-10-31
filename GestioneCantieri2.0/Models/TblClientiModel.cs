using System;

namespace GestioneCantieri2._0.Models
{
    public class TblClientiModel
    {
        public int IdCliente { get; set; } = 0;
        public string RagSocCli { get; set; } = null;
        public string Indirizzo { get; set; } = null;
        public string Cap { get; set; } = null;
        public string Città { get; set; } = null;
        public string Tel1 { get; set; } = null;
        public string Cell1 { get; set; } = null;
        public string PartitaIva { get; set; } = null;
        public string CodFiscale { get; set; } = null;
        public DateTime? Data { get; set; } = null;
        public string Provincia { get; set; } = null;
        public string Note { get; set; } = null;
        public DateTime? Doi { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime? Dou { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
