using System;
using System.Collections.Generic;

namespace GestioneCantieri2._0.Models
{    
    public class TblMatOrdFrutModel
    {
        public int Id { get; set; } = 0;
        public int IdCantiere { get; set; } = 0;
        public int? IdGruppiFrutti { get; set; } = null;
        public int IdLocale { get; set; } = 0;
        public DateTime? DataOrdine { get; set; } = null;
        public string Appartamento { get; set; } = null;
        public int? IdFrutto { get; set; } = null;
        public int? QtaFrutti { get; set; } = null;
        public DateTime? Doi { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime? Dou { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
