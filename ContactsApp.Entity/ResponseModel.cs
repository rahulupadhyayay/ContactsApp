using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ContactsApp.Entity
{
    /// <summary>
    /// Common response model which will be for every response
    /// </summary>
    public class ResponseModel
    {
        #region Properties

        public bool IsSuccess { get; set; }

        public bool IsExceptionOccured { get; set; }

        public string Status { get; set; }

        public int ErrorCode { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }

        #endregion

        #region Constructors

        public ResponseModel(ResponseStatus responseStatus)
        {
            Status = responseStatus.ToString();

            switch (responseStatus)
            {
                case ResponseStatus.Success:

                    IsSuccess = true;
                    StatusCode = HttpStatusCode.OK;
                    break;

                case ResponseStatus.Failure:

                    IsSuccess = false;
                    StatusCode = HttpStatusCode.InternalServerError;
                    break;

                case ResponseStatus.InvalidInput:

                    IsSuccess = false;
                    StatusCode = HttpStatusCode.BadRequest;
                    break;

                case ResponseStatus.UnAuthorized:
                    IsSuccess = false;
                    StatusCode = HttpStatusCode.Unauthorized;
                    break;
                case ResponseStatus.Duplicate:
                    IsSuccess = false;
                    StatusCode = HttpStatusCode.BadRequest;
                    break;

            }
        }

        #endregion
    }

    public class ResponseModel<T> : ResponseModel
    {
        #region Properties

        public T Result { get; set; }

        #endregion

        #region Constructors        

        public ResponseModel()
            : base(ResponseStatus.Success)
        {

        }

        public ResponseModel(T responseModel)
            : base(ResponseStatus.Success)
        {
            Result = responseModel;
        }

        public ResponseModel(ResponseStatus responseStatus, T responseModel)
            : base(responseStatus)
        {
            Result = responseModel;
        }

        #endregion
    }

   


    public enum ResponseStatus
    {
        Success,
        Failure,
        InvalidInput,
        UnAuthorized,
        Duplicate
    }
}
