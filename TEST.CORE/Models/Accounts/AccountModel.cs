using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST.CORE.Models.Accounts
{
    public class AccountModel
    {
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public int SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public string Identificacion { get; set; }
    }
    public class DetailAcModel
    {
        public int IdCuenta { get; set; }
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public int SaldoInicial { get; set; }
        public bool Estado { get; set; }
    }
}
