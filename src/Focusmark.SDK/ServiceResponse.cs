using System;

namespace FocusMark.SDK
{
    public class ServiceResponse
    {
        public ServiceResponse()
        {
            this.Errors = Array.Empty<ResponseError>();
            this.IsSuccessful = true;
        }

        public ServiceResponse(params ResponseError[] errors)
        {
            this.Errors = errors ?? Array.Empty<ResponseError>();
            if (this.Errors?.Length > 0)
            {
                this.IsSuccessful = false;
            }
        }

        public ResponseError[] Errors { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
