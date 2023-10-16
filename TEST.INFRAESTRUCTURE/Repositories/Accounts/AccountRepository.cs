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
using TEST.CORE.Interface.Clients;
using TEST.CORE.Models.Accounts;
using TEST.CORE.Interface.Accounts;
using System.Security.Principal;

namespace TEST.INFRAESTRUCTURE.Repositories.Accounts
{
    public class AccountRepository : IAccount
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPerson _person;
        public AccountRepository(IHttpContextAccessor httpContextAccessor, IPerson person) 
        {
            _httpContextAccessor = httpContextAccessor;
            _person = person;
        }

        ReplyService reply = new ReplyService();


        #region Crear Cuenta 
        public async Task<ReplyService> CreateAccountAsync(AccountModel data)
        {
            var tenantInfo = _httpContextAccessor.HttpContext.GetMultiTenantContext<TenantInfo>().TenantInfo;

            try
            {
                using (var sqlcn = new SqlConnection(tenantInfo.ConnectionString))
                {
                    await sqlcn.OpenAsync();

                    SqlCommand cmd = new SqlCommand("sp_create_account", sqlcn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("NumeroCuenta", data.NumeroCuenta));
                    cmd.Parameters.Add(new SqlParameter("TipoCuenta", data.TipoCuenta));
                    cmd.Parameters.Add(new SqlParameter("SaldoInicial", data.SaldoInicial));
                    cmd.Parameters.Add(new SqlParameter("Estado", data.Estado));
                    cmd.Parameters.Add(new SqlParameter("Identificacion", data.Identificacion));
                    await cmd.ExecuteNonQueryAsync();

                    reply.Flag = true;
                    reply.Status = 200;
                    reply.Message = "Cuenta creada con éxito";
                    return reply;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Detalle Cuenta 
        public async Task<ReplyService> DetailAccountAsync(string identificacion)
        {
            var tenantInfo = _httpContextAccessor.HttpContext.GetMultiTenantContext<TenantInfo>().TenantInfo;

            try
            {
                using (var sqlcn = new SqlConnection(tenantInfo.ConnectionString))
                {
                    await sqlcn.OpenAsync();

                    SqlCommand cmd = new SqlCommand("sp_select_account", sqlcn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Identificacion", identificacion));
                    SqlDataReader sqldr = await cmd.ExecuteReaderAsync();

                    List<DetailAcModel> data = new List<DetailAcModel>();

                    if (sqldr.HasRows)
                    {
                        while(await sqldr.ReadAsync())
                        {
                            DetailAcModel db = new DetailAcModel();

                            db.IdCuenta = Convert.ToInt32(sqldr["IdCuenta"]);
                            db.NumeroCuenta = sqldr["NumeroCuenta"].ToString();
                            db.TipoCuenta = sqldr["TipoCuenta"].ToString();
                            db.SaldoInicial = Convert.ToInt32(sqldr["SaldoInicial"]);
                            db.Estado = Convert.ToBoolean(sqldr["Estado"]);
                            data.Add(db);
                        }

                        reply.Data = data;
                        reply.Status = 200;
                        reply.Flag = true;
                        reply.Message = "Detalle de cuenta";
                    }
                    else
                    {
                        reply.Status = 500;
                        reply.Flag = false;
                        reply.Message = "No tiene cuentas asociadas el numero de indentificación";
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

        #region Actualizar Cuenta 
        public async Task<ReplyService> UpdateAccountAsync(int saldoInicial, string identificacion, string TipoCuenta)
        {
            var tenantInfo = _httpContextAccessor.HttpContext.GetMultiTenantContext<TenantInfo>().TenantInfo;

            try
            {
                using (var sqlcn = new SqlConnection(tenantInfo.ConnectionString))
                {
                    await sqlcn.OpenAsync();

                    ReplyService dataMov = await DetailAccountAsync(identificacion);
                    var detailMov = dataMov.Data;
                    int valor = 0;
                    if (dataMov.Flag)
                    {
                        foreach (DetailAcModel detail in (List<DetailAcModel>)detailMov)
                        {
                            if (TipoCuenta == detail.TipoCuenta)
                            {
                                valor = detail.SaldoInicial;
                            }
                        }

                        int UpdateSalgo = valor + saldoInicial;
                        if (UpdateSalgo > 0)
                        {

                            SqlCommand cmd = new SqlCommand("sp_update_account", sqlcn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("SaldoInicial", UpdateSalgo));
                            cmd.Parameters.Add(new SqlParameter("Identificacion", identificacion));
                            cmd.Parameters.Add(new SqlParameter("TipoCuenta", TipoCuenta));
                            await cmd.ExecuteNonQueryAsync();

                            reply.Flag = true;
                            reply.Status = 200;
                            reply.Message = "Registro de movimiento registrado con éxito";
                        }
                        else
                        {
                            reply.Flag = false;
                            reply.Status = 500;
                            reply.Message = "Saldo no disponible";
                            reply.Data = null;

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

        #region Eliminar Cuenta 
        public async Task<ReplyService> DeleteAccountAsync(string NumeroCuenta)
        {
            var tenantInfo = _httpContextAccessor.HttpContext.GetMultiTenantContext<TenantInfo>().TenantInfo;

            try
            {
                using (var sqlcn = new SqlConnection(tenantInfo.ConnectionString))
                {
                    await sqlcn.OpenAsync();

                    SqlCommand cmd = new SqlCommand("sp_delete_account", sqlcn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("NumeroCuenta", NumeroCuenta));
                    await cmd.ExecuteNonQueryAsync();

                    reply.Flag = true;
                    reply.Status = 200;
                    reply.Message = "Cuenta eliminada con éxito";
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
