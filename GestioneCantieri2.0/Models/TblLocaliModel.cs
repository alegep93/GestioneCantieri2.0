using System;
using System.Collections.Generic;

namespace GestioneCantieri2._0.Models
{
    public class TblLocaliModel
    {
        public int IdLocali { get; set; } = 0;
        public string NomeLocale { get; set; } = null;
        public DateTime? Doi { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime? Dou { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
