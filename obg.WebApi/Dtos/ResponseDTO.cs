namespace obg.WebApi.Dtos
{
    public class ResponseDTO
    {
        public object Content { get; set; }
        public bool IsSucess { get; set; }
        public int Code { get; set; }
        public string ErrorMessage { get; set; }
    }
}
