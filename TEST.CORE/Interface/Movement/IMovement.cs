using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEST.CORE.Helpers;
using TEST.CORE.Models.Accounts;
using TEST.CORE.Models.Movement;

namespace TEST.CORE.Interface.Movement
{
    public interface IMovement
    {
        Task<ReplyService> CreateMovementAsync(MovementModel data);
        Task<ReplyService> DetailMovementAsync(string fechaInicial, string fechaFinal);
        Task<ReplyService> UpdateMovementAsync(UpdateMovementModel data);
        Task<ReplyService> DeleteMovementAsync(string fecha);
    }
}
