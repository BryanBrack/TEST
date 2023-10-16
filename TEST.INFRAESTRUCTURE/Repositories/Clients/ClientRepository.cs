using Microsoft.AspNetCore.Http;
using TEST.CORE.Helpers;
using System.Data.SqlClient;
using Finbuckle.MultiTenant;
using System.Data;
using TEST.CORE.Models.Clients;
using TEST.CORE.Interface.Clients;

namespace TEST.INFRAESTRUCTURE.Repositories.Clients
{
    public class ClientRepository : IPerson
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        //Conexion local para realizar pruebas unitarias. IMPORTANTE (Comentar variable tenantInfo, using con variable tenantInfo y descomentar using con cadena de conexion)
        string ConnectionString = "Persist Security Info=False;Integrated Security=true; Initial Catalog = BD_TEST; Server = DESKTOP-3SRA8UR";

        ReplyService reply = new ReplyService();

        #region Crear Persona 
        public async Task<ReplyService> CreatePersonAsync(PersonModel data)
        {
            var tenantInfo = _httpContextAccessor.HttpContext.GetMultiTenantContext<TenantInfo>().TenantInfo;

            try
            {
                //using (var sqlcn = new SqlConnection(ConnectionString))
                using (var sqlcn = new SqlConnection(tenantInfo.ConnectionString))
                {
                    await sqlcn.OpenAsync();

                    SqlCommand cmd = new SqlCommand("sp_create_persona", sqlcn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Nombre", data.Nombre));
                    cmd.Parameters.Add(new SqlParameter("Genero", data.Genero));
                    cmd.Parameters.Add(new SqlParameter("Edad", data.Edad));
                    cmd.Parameters.Add(new SqlParameter("Identificacion", data.Identificacion));
                    cmd.Parameters.Add(new SqlParameter("Direccion", data.Direccion));
                    cmd.Parameters.Add(new SqlParameter("Telefono", data.Telefono));
                    cmd.Parameters.Add(new SqlParameter("Estado", data.Estado));
                    await cmd.ExecuteNonQueryAsync();

                    var res = await CreatePassword(data);
                    if(res)
                    {
                        reply.Flag = true;
                        reply.Status = 200;
                        reply.Message = "Persona creada con éxito";
                    }
                    else
                    {
                        reply.Message = "No se pudo completar la solicitud de creación de persona.";
                    }
                    return reply;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        private async Task<bool> CreatePassword(PersonModel data)
        {
            var tenantInfo = _httpContextAccessor.HttpContext.GetMultiTenantContext<TenantInfo>().TenantInfo;

            try
            {
                //using (var sqlcn = new SqlConnection(ConnectionString))
                using (var sqlcn = new SqlConnection(tenantInfo.ConnectionString))
                {
                    await sqlcn.OpenAsync();

                    SqlCommand cmd = new SqlCommand("sp_create_cliente", sqlcn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Contraseña", data.Contraseña));
                    cmd.Parameters.Add(new SqlParameter("Identificacion", data.Identificacion));
                    cmd.Parameters.Add(new SqlParameter("Estado", data.Estado));
                    await cmd.ExecuteNonQueryAsync();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Detalle Persona 
        public async Task<ReplyService> DetailPersonAsync(string contraseña)
        {
            var tenantInfo = _httpContextAccessor.HttpContext.GetMultiTenantContext<TenantInfo>().TenantInfo;

            try
            {
                //using (var sqlcn = new SqlConnection(ConnectionString))
                using (var sqlcn = new SqlConnection(tenantInfo.ConnectionString))
                {
                    await sqlcn.OpenAsync();

                    SqlCommand cmd = new SqlCommand("sp_select_persona", sqlcn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@contraseña", contraseña));
                    SqlDataReader sqldr = await cmd.ExecuteReaderAsync();

                    List<DetailModel> data = new List<DetailModel>();

                    if(sqldr.HasRows)
                    {
                        await sqldr.ReadAsync();

                        DetailModel db = new DetailModel();

                        db.Nombre = sqldr["Nombre"].ToString();
                        db.Genero =  sqldr["Genero"].ToString();
                        db.Edad = sqldr["Edad"].ToString();
                        db.Identificacion = sqldr["Identificacion"].ToString();
                        db.Direccion =  sqldr["Direccion"].ToString();
                        db.Telefono =  sqldr["Telefono"].ToString();
                        db.Contraseña = sqldr["Contraseña"].ToString();
                        data.Add(db);
                    }
                    else
                    {
                        reply.Data = null;
                    }    

                    reply.Data = data;
                    reply.Status = 200;
                    reply.Flag = true;
                    reply.Message = "Detalle de persona";
                    return reply;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Actualizar Persona 
        public async Task<ReplyService> UpdatePersonAsync(PersonModel data)
        {
            var tenantInfo = _httpContextAccessor.HttpContext.GetMultiTenantContext<TenantInfo>().TenantInfo;

            try
            {
                //using (var sqlcn = new SqlConnection(ConnectionString))
                using (var sqlcn = new SqlConnection(tenantInfo.ConnectionString))
                {
                    await sqlcn.OpenAsync();

                    SqlCommand cmd = new SqlCommand("sp_update_persona", sqlcn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Nombre", data.Nombre));
                    cmd.Parameters.Add(new SqlParameter("Genero", data.Genero));
                    cmd.Parameters.Add(new SqlParameter("Edad", data.Edad));
                    cmd.Parameters.Add(new SqlParameter("Identificacion", data.Identificacion));
                    cmd.Parameters.Add(new SqlParameter("Direccion", data.Direccion));
                    cmd.Parameters.Add(new SqlParameter("Telefono", data.Telefono));
                    cmd.Parameters.Add(new SqlParameter("Estado", data.Estado));
                    await cmd.ExecuteNonQueryAsync();

                    reply.Flag = true;
                    reply.Status = 200;
                    reply.Message = "Persona actualizada con éxito";
                    return reply;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Eliminar Persona 
        public async Task<ReplyService> DeletePersonAsync(string Identificacion)
        {
            var tenantInfo = _httpContextAccessor.HttpContext.GetMultiTenantContext<TenantInfo>().TenantInfo;

            try
            {
                //using (var sqlcn = new SqlConnection(ConnectionString))
                using (var sqlcn = new SqlConnection(tenantInfo.ConnectionString))
                {
                    await sqlcn.OpenAsync();

                    SqlCommand cmd = new SqlCommand("sp_delete_persona", sqlcn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Identificacion", Identificacion));
                    await cmd.ExecuteNonQueryAsync();

                    reply.Flag = true;
                    reply.Status = 200;
                    reply.Message = "Persona eliminada con éxito";
                    return reply;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
