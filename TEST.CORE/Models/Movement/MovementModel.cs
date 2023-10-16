using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST.CORE.Models.Movement
{
    public class MovementModel
    {
        public string Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public int Valor { get; set; }
        public string Identificacion { get; set; }
        public string TipoCuenta { get; set; }
    }
    public class DetailMovementModel
    {
        public string Fecha { get; set; }
        public string Cliente { get; set; }
        public string NumeroCuenta { get; set; }
        public string TipoMovimiento { get; set; }
        public string TipoCuenta { get; set; }
        public int Movimiento { get; set; }
        public int SaldoDisponible { get; set; }
        public string Estado { get; set; }
    }
    public class UpdateMovementModel
    {
        public string Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public int Valor { get; set; }
        public string Identificacion { get; set; }
        public string TipoCuenta { get; set; }
        public string Estado { get; set; }
    }
}
