using System;

namespace GestioneCantieri2._0.Models
{
    public class TblCompGruppoFrutModel
    {
        public int Id { get; set; } = 0;
        public int IdTblGruppo { get; set; } = 0;
        public int IdTblFrutto { get; set; } = 0;
        public int Qta { get; set; } = 0;
        public DateTime? Doi { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime? Dou { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
