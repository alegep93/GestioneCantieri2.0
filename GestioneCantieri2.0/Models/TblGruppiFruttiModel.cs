using System;

namespace GestioneCantieri2._0.Models
{
    public class TblGruppiFruttiModel
    {
        public int Id { get; set; } = 0;
        public string NomeGruppo { get; set; } = "";
        public string Descrizione { get; set; } = "";
        public bool? Completato { get; set; } = null;
        public bool? Controllato { get; set; } = null;
        public DateTime? Doi { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime? Dou { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
