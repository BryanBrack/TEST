using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEST.CORE.Helpers;
using TEST.CORE.Interface.Movement;
using TEST.CORE.Models.Accounts;
using TEST.CORE.Models.Movement;

namespace API_TEST_NEORIS.Controllers
{
    [ApiController]
    public class MovementController : ControllerBase
    {
        private readonly IMovement _movement;

        public MovementController (IMovement movement)
        {
            _movement = movement;
        }

        public Reply oReply = new Reply();


        [HttpPost]
        [Route("movimientos/crearMovimiento")]
        public async Task<IActionResult> CreateAccountAsync([FromBody] MovementModel data)
        {
            var res = await _movement.CreateMovementAsync(data);
            oReply.Data = res.Data;
            oReply.Ok = res.Flag;
            oReply.Message = res.Message;

            if (res.Status == 200)
                return Ok(oReply);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = oReply.Message });

        }

        [HttpGet]
        [Route("movimientos/reportes")]
        public async Task<IActionResult> DetailMovementAsync([FromQuery] string fechaInicial, [FromQuery] string fechaFinal)
        {
            var res = await _movement.DetailMovementAsync(fechaInicial, fechaFinal);
            oReply.Data = res.Data;
            oReply.Ok = res.Flag;
            oReply.Message = res.Message;

            if (res.Status == 200)
                return Ok(oReply);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = oReply.Message });

        }

        [HttpPost]
        [Route("movimientos/actualizarMovimiento")]
        public async Task<IActionResult> UpdateMovementAsync([FromBody] UpdateMovementModel data)
        {
            var res = await _movement.UpdateMovementAsync(data);
            oReply.Data = res.Data;
            oReply.Ok = res.Flag;
            oReply.Message = res.Message;

            if (res.Status == 200)
                return Ok(oReply);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = oReply.Message });

        }

        [HttpGet]
        [Route("movimientos/eliminarMovimiento/{fecha}")]
        public async Task<IActionResult> DeleteMovementAsync([FromRoute] string fecha)
        {
            var res = await _movement.DeleteMovementAsync(fecha);
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
