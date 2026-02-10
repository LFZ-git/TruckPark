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
        public bool IsUserMapFailed { get; set; } = false;
        public bool IsMaterialTypeMapFailed { get; set; } = false;
        public bool IsTransferTypeMapFailed { get; set; } = false;
        public bool IsTruckCapacityMapFailed { get; set; } = false;
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

        public ExtAPIBaseRespModel(bool isSuccess, string message, bool isUserMapFailed, bool isMaterialTypeMapFailed, bool isTransferTypeMapFailed, bool isTruckCapacityMapFailed)
        {
            Response = new ExtAPIRespModel(isSuccess, message)
            {
                IsUserMapFailed = isUserMapFailed,
                IsMaterialTypeMapFailed = isMaterialTypeMapFailed,
                IsTransferTypeMapFailed = isTransferTypeMapFailed,
                IsTruckCapacityMapFailed = isTruckCapacityMapFailed
            };
        }

        public ExtAPIRespModel Response { get; set; }
    }
}
