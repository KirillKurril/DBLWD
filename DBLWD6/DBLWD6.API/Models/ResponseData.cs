namespace DBLWD6.API.Models
{
    public class ResponseData<T>
    {
        public T? Data { get; set; }
        public bool Success {  get; set; }
        public string ErrorMessage { get; set; }
        public ResponseData(T? data)
        {
            Data = data;
            Success = true;
            ErrorMessage = string.Empty;
        }

        public ResponseData(bool success, string errorMessage)
        {
            Data = default(T);
            Success = success;
            ErrorMessage = errorMessage;
        }
    }
}
