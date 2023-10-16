using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEST.CORE.Helpers;
using TEST.CORE.Models.Clients;
using TEST.CORE.Models.Movement;
using TEST.CORE.Interface.Accounts;
using TEST.CORE.Interface.Clients;
using TEST.CORE.Models.Accounts;
using TEST.CORE.Interface.Movement;
using System.Drawing;

namespace TEST.INFRAESTRUCTURE.Repositories.Movements
{
    public class MovementRepository : IMovement
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccount _account;
        public MovementRepository(IHttpContextAccessor httpContextAccessor, IAccount account)
        {
            _httpContextAccessor = httpContextAccessor;
            _account = account;
        }

        ReplyService reply = new ReplyService();

        #region Crear Persona 
        public async Task<ReplyService> CreateMovementAsync(MovementModel data)
        {
            var tenantInfo = _httpContextAccessor.HttpContext.GetMultiTenantContext<TenantInfo>().TenantInfo;

            try
            {
                using (var sqlcn = new SqlConnection(tenantInfo.ConnectionString))
                {
                    await sqlcn.OpenAsync();


                    ReplyService updateAcco = await _account.UpdateAccountAsync(data.Valor, data.Identificacion, data.TipoCuenta);

                    bool Update = updateAcco.Flag;


                    ReplyService dataMov = await _account.DetailAccountAsync(data.Identificacion);
                    if (dataMov.Flag)
                    {
                        var detailMov = dataMov.Data;
                        int id = 0;
                        string status = "";

                        if (Update) status = "Aprovado";
                        else status = "Rechazado";

                        foreach (DetailAcModel detail in (List<DetailAcModel>)detailMov)
                        {
                            if (data.TipoCuenta == detail.TipoCuenta)
                            {
                                id = detail.SaldoInicial;
                            }
                        }


                        SqlCommand cmd = new SqlCommand("sp_create_movement", sqlcn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("Fecha", data.Fecha));
                        cmd.Parameters.Add(new SqlParameter("TipoMovimiento", data.TipoMovimiento));
                        cmd.Parameters.Add(new SqlParameter("Valor", data.Valor));
                        cmd.Parameters.Add(new SqlParameter("Saldo", id));
                        cmd.Parameters.Add(new SqlParameter("Identificacion", data.Identificacion));
                        cmd.Parameters.Add(new SqlParameter("TipoCuenta", data.TipoCuenta));
                        cmd.Parameters.Add(new SqlParameter("Estado", status));
                        await cmd.ExecuteNonQueryAsync();

                        if (Update)
                        {
                            reply.Flag = true;
                            reply.Status = 200;
                            reply.Message = "Registro de movimiento registrado con éxito";
                        }
                        else
                        {
                            reply.Flag = false;
                            reply.Status = 500;
                            reply.Message = "Saldo no disponible";
                        }
                    }
                    else
                    {
                        reply.Flag = false;
                        reply.Status = 500;
                        reply.Message = dataMov.Message;
                    }
                    return reply;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Detalle Persona 
        public async Task<ReplyService> DetailMovementAsync(string fechaInicial, string fechaFinal)
        {
            var tenantInfo = _httpContextAccessor.HttpContext.GetMultiTenantContext<TenantInfo>().TenantInfo;

            try
            {
                using (var sqlcn = new SqlConnection(tenantInfo.ConnectionString))
                {
                    await sqlcn.OpenAsync();

                    SqlCommand cmd = new SqlCommand("sp_select_movement", sqlcn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@fechaInicial", fechaInicial));
                    cmd.Parameters.Add(new SqlParameter("@fechaFinal", fechaFinal));
                    SqlDataReader sqldr = await cmd.ExecuteReaderAsync();

                    List<DetailMovementModel> data = new List<DetailMovementModel>();
                    

                    if (sqldr.HasRows)
                    {
                        while (await sqldr.ReadAsync())
                        {
                            DetailMovementModel db = new DetailMovementModel();
                            ReplyService dataMov = await _account.DetailAccountAsync(sqldr["Identificacion"].ToString());
                            var detailMov = dataMov.Data;
                            string cuenta = "";
                            foreach (DetailAcModel detail in (List<DetailAcModel>)detailMov)
                            {
                                cuenta = detail.NumeroCuenta;                               
                            }
                            db.Fecha = sqldr["Fecha"].ToString();
                            db.TipoMovimiento = sqldr["TipoMovimiento"].ToString();
                            db.Cliente = sqldr["Nombre"].ToString();
                            db.NumeroCuenta = cuenta;
                            db.TipoMovimiento = sqldr["TipoMovimiento"].ToString();
                            db.Movimiento = Convert.ToInt32(sqldr["Valor"]);
                            db.SaldoDisponible = Convert.ToInt32(sqldr["Saldo"]);
                            db.TipoCuenta = sqldr["TipoCuenta"].ToString();
                            db.Estado = sqldr["Estado"].ToString();
                            data.Add(db);
                        }
                        reply.Data = data;
                        reply.Status = 200;
                        reply.Flag = true;
                        reply.Message = "Repote de movimientos";
                    }
                    else
                    {
                        reply.Status = 500;
                        reply.Flag = false;
                        reply.Message = "No hay movimientos en las fechas seleccionadas.";
                        reply.Data = null;
                    }
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
        public async Task<ReplyService> UpdateMovementAsync(UpdateMovementModel data)
        {
            var tenantInfo = _httpContextAccessor.HttpContext.GetMultiTenantContext<TenantInfo>().TenantInfo;

            try
            {
                using (var sqlcn = new SqlConnection(tenantInfo.ConnectionString))
                {
                    await sqlcn.OpenAsync();

                    SqlCommand cmd = new SqlCommand("sp_update_movement", sqlcn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Fecha", data.Fecha));
                    cmd.Parameters.Add(new SqlParameter("Edad", data.TipoMovimiento));
                    cmd.Parameters.Add(new SqlParameter("Valor", data.Valor));
                    cmd.Parameters.Add(new SqlParameter("Identificacion", data.Identificacion));
                    cmd.Parameters.Add(new SqlParameter("TipoCuenta", data.TipoCuenta));
                    cmd.Parameters.Add(new SqlParameter("Estado", data.Estado));
                    await cmd.ExecuteNonQueryAsync();

                    reply.Flag = true;
                    reply.Status = 200;
                    reply.Message = "Movimiento actualizada con éxito";
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
        public async Task<ReplyService> DeleteMovementAsync(string fecha)
        {
            var tenantInfo = _httpContextAccessor.HttpContext.GetMultiTenantContext<TenantInfo>().TenantInfo;

            try
            {
                using (var sqlcn = new SqlConnection(tenantInfo.ConnectionString))
                {
                    await sqlcn.OpenAsync();

                    SqlCommand cmd = new SqlCommand("sp_delete_movement", sqlcn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Identificacion", fecha));
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
