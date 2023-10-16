using TEST.CORE.Helpers;
using TEST.CORE.Models.Accounts;

namespace TEST.CORE.Interface.Accounts
{
    public interface IAccount
    {
        Task<ReplyService> CreateAccountAsync(AccountModel data);
        Task<ReplyService> DetailAccountAsync(string identificacion);
        Task<ReplyService> UpdateAccountAsync(int saldoInicial, string identificacion, string TipoCuenta);
        Task<ReplyService> DeleteAccountAsync(string NumeroCuenta);
    }
}
