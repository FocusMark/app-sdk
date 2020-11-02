namespace Focusmark.SDK
{
    public class ServiceResponse<T> : ServiceResponse where T : class
    {
        public ServiceResponse(T data) : base()
        {
            this.Data = data ?? throw new System.ArgumentNullException(nameof(data));
        }

        public ServiceResponse(T data, params ResponseError[] errors) : base(errors)
        {

        }

        public ServiceResponse(params ResponseError[] errors) : base(errors)
        {
        }

        public T Data { get; set; }
    }
}
