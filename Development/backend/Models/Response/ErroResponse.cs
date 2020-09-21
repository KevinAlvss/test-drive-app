namespace backend.Models.Response
{
    public class ErroResponse
    {
        public string Message { get; set; }
        public int Code { get; set; }

        public ErroResponse(string message, int code)
        {
            this.Message = message;
            this.Code = code;
        }
    }
}