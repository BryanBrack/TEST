using API_TEST_NEORIS.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEST.CORE.Interface.Clients;
using TEST.CORE.Models.Clients;
using TEST.INFRAESTRUCTURE.Repositories.Clients;

namespace TestProject
{
    public class ClientTest
    {
        private readonly IPerson _client;
        private readonly ClientsController _controller;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientTest()
        {
            _client = new ClientRepository(_httpContextAccessor);
            _controller = new ClientsController(_client);
        }

        [Fact]
        public async void TestCreateClientPOST()
        {
          
            PersonModel db = new PersonModel();
            db.Nombre = "Pepito Perez";
            db.Genero = "Masculino";
            db.Edad = "30";
            db.Identificacion = "1014300468";
            db.Direccion = "Diagonal 123 abc";
            db.Telefono = "312617666";
            db.Contraseña = "987654";
            db.Estado = true;

            var res = await _controller.CreatePersonAsync(db);

            Assert.IsType<OkObjectResult>(res);
        }

        [Fact]
        public async void TestDetailClientGET()
        {

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Client"] = "test_neoris";


            var res = await _controller.DetailPersonAsync("123456");

            Assert.IsType<OkObjectResult>(res);
        }

        [Fact]
        public async void TestUpdateClientPOST()
        {

            PersonModel db = new PersonModel();
            db.Nombre = "Pepito Perez";
            db.Genero = "Masculino";
            db.Edad = "30";
            db.Identificacion = "1014300468";
            db.Direccion = "Diagonal 123 abc";
            db.Telefono = "312617666";
            db.Contraseña = "987654";
            db.Estado = true;

            var res = await _controller.UpdatePersonAsync(db);

            Assert.IsType<OkObjectResult>(res);
        }

        [Fact]
        public async void TestDeleteClientGET()
        {

            var res = await _controller.DeletePersonAsync("1014300468");

            Assert.IsType<OkObjectResult>(res);
        }
    }
}