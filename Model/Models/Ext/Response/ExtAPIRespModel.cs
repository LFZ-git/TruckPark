using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.Ext.Response
{
    public class ExtAPIRespModel
    {
        public ExtAPIRespModel()
        {
            IsSuccess = true;
        }

        public ExtAPIRespModel(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class ExtAPIBaseRespModel
    {
        public ExtAPIBaseRespModel()
        {
            Response = new ExtAPIRespModel();
        }

        public ExtAPIBaseRespModel(bool isSuccess, string message)
        {
            Response = new ExtAPIRespModel(isSuccess, message);
        }

        public ExtAPIRespModel Response { get; set; }
    }
}
