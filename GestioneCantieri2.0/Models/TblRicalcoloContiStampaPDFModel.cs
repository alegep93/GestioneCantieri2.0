using System;
using System.Collections.Generic;

namespace GestioneCantieri2._0.Models
{

    public class TblRicalcoloContiStampaPDFModel
    {
        public string DataONote { get; set; } = null;
        public string DescriCodArt { get; set; } = null;
        public decimal? Qta { get; set; } = null;
        public decimal? PzzoUniCantiere { get; set; } = null;
        public decimal? Valore { get; set; } = null;
        public DateTime? Doi { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime? Dou { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
