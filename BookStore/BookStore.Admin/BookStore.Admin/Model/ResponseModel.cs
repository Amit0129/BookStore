namespace BookStore.Admin.Model
{
    public class ResponseModel<T>
    {
        public bool Sucess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
