using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEST.CORE.Helpers;
using TEST.CORE.Interface.Clients;
using TEST.CORE.Models.Clients;

namespace API_TEST_NEORIS.Controllers
{
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IPerson _person;

        public ClientsController(IPerson person)
        {
            _person = person;
        }

        public Reply oReply = new Reply();


        [HttpPost]
        [Route("clientes/crearPersona")]
        public async Task<IActionResult> CreatePersonAsync([FromBody] PersonModel data)
        {
            var res = await _person.CreatePersonAsync(data);
            oReply.Data = res.Data;
            oReply.Ok = res.Flag;
            oReply.Message = res.Message;

            if (res.Status == 200)
                return Ok(oReply);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = oReply.Message });

        }

        [HttpGet]
        [Route("clientes/detallePersona/{contraseña}")]
        public async Task<IActionResult> DetailPersonAsync([FromRoute] string contraseña)
        {
            var res = await _person.DetailPersonAsync(contraseña);
            oReply.Data = res.Data;
            oReply.Ok = res.Flag;
            oReply.Message = res.Message;

            if (res.Status == 200)
                return Ok(oReply);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = oReply.Message });

        }

        [HttpPost]
        [Route("clientes/actualizarPersona")]
        public async Task<IActionResult> UpdatePersonAsync([FromBody] PersonModel data)
        {
            var res = await _person.UpdatePersonAsync(data);
            oReply.Data = res.Data;
            oReply.Ok = res.Flag;
            oReply.Message = res.Message;

            if (res.Status == 200)
                return Ok(oReply);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = oReply.Message });

        }

        [HttpGet]
        [Route("clientes/eliminarPersona/{identificacion}")]
        public async Task<IActionResult> DeletePersonAsync([FromRoute] string identificacion)
        {
            var res = await _person.DeletePersonAsync(identificacion);
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
