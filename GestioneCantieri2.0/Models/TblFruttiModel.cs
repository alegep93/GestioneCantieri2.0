using System;

namespace GestioneCantieri2._0.Models
{
    public class TblFruttiModel
    {
        public int ID1 { get; set; } = 0;
        public string Descr001 { get; set; } = null;
        public DateTime? Doi { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime? Dou { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
