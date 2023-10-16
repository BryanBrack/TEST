using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST.CORE.Helpers
{
    public class Reply
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
    public class ReplyService
    {
        public int Status { get; set; }
        public bool Flag { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
