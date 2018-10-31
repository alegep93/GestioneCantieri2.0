using System;

namespace GestioneCantieri2._0.Models
{ 
    public class TblOperaioModel
    {
        public int IdOperaio { get; set; } = 0;
        public string NomeOp { get; set; } = null;
        public string DescrOP { get; set; } = null;
        public string Suffisso { get; set; } = null;
        public string Operaio { get; set; } = null;
        public decimal? CostoOperaio { get; set; } = null;
        public DateTime? Doi { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime? Dou { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
