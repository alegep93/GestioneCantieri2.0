using System;
using System.Collections.Generic;

namespace GestioneCantieri2._0.Models
{
    
    public class TblSpeseModel
    {
        public int IdSpesa { get; set; } = 0;
        public string Descrizione { get; set; } = null;
        public decimal? Prezzo { get; set; } = null;
        public DateTime? Doi { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime? Dou { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
