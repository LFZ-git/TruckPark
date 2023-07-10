using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class ErrorLogModel
    {
        public long LogId { get; set; }

        public string ExceptionMsg { get; set; }

        public string ExceptionType { get; set; }

        public string ExceptionSource { get; set; }

        public string ExceptionURL { get; set; }

        public DateTime Logdate { get; set; }


    }
}
