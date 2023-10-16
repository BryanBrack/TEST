using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEST.CORE.Helpers;
using TEST.CORE.Interface.Accounts;
using TEST.CORE.Interface.Clients;
using TEST.CORE.Models.Accounts;
using TEST.CORE.Models.Clients;

namespace API_TEST_NEORIS.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _account;

        public AccountController(IAccount account) 
        {
            _account = account;
        }  

        public Reply oReply = new Reply();


        [HttpPost]
        [Route("cuentas/crearCuenta")]
        public async Task<IActionResult> CreateAccountAsync([FromBody] AccountModel data)
        {
            var res = await _account.CreateAccountAsync(data);
            oReply.Data = res.Data;
            oReply.Ok = res.Flag;
            oReply.Message = res.Message;

            if (res.Status == 200)
                return Ok(oReply);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = oReply.Message });

        }

        [HttpGet]
        [Route("cuentas/detalleCuenta/{identificacion}")]
        public async Task<IActionResult> DetailAccountAsync([FromRoute] string identificacion)
        {
            var res = await _account.DetailAccountAsync(identificacion);
            oReply.Data = res.Data;
            oReply.Ok = res.Flag;
            oReply.Message = res.Message;

            if (res.Status == 200)
                return Ok(oReply);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = oReply.Message });

        }

        [HttpGet]
        [Route("cuentas/actualizarCuenta")]
        public async Task<IActionResult> UpdateAccountAsync([FromQuery] int saldoInicial, [FromQuery] string identificacion, [FromQuery] string TipoCuenta)
        {
            var res = await _account.UpdateAccountAsync(saldoInicial, identificacion, TipoCuenta);
            oReply.Data = res.Data;
            oReply.Ok = res.Flag;
            oReply.Message = res.Message;

            if (res.Status == 200)
                return Ok(oReply);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = oReply.Message });

        }

        [HttpGet]
        [Route("cuentas/eliminarCuenta/{NumeroCuenta}")]
        public async Task<IActionResult> DeletePersonAsync([FromRoute] string NumeroCuenta)
        {
            var res = await _account.DeleteAccountAsync(NumeroCuenta);
            oReply.Data = res.Data;
            oReply.Ok = res.Flag;
            oReply.Message = res.Message;

            if (res.Status == 200)
                return Ok(oReply);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = oReply.Message });

        }
    }
}
