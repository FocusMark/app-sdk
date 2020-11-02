namespace FocusMark.SDK
{
    public class ResponseError
    {
        public ResponseError(int errorCode, string message)
        {
            this.Code = errorCode;
            this.Message = message;
        }

        public int Code { get; set; }
        public string Message { get; set; }
    }
}
