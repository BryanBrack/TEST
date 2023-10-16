using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEST.CORE.Helpers;
using TEST.CORE.Models.Clients;

namespace TEST.CORE.Interface.Clients
{
    public interface IPerson
    {
        Task<ReplyService> CreatePersonAsync(PersonModel data);
        Task<ReplyService> DetailPersonAsync(string contraseña);
        Task<ReplyService> UpdatePersonAsync(PersonModel data);
        Task<ReplyService> DeletePersonAsync(string identificacion);

    }
}
