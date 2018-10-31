using System;
using System.Collections.Generic;

namespace GestioneCantieri2._0.Models
{
    
    public class TblStampeModel
    {
        public int Id { get; set; } = 0;
        public string NomeStampa { get; set; } = null;
        public DateTime? Doi { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime? Dou { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
