namespace BookStore.User.Entity
{
    public class ResponseEntity
    {
        public object? Data { get; set; }
        public string Message { get; set; }
        public bool IsSucess { get; set; }
    }
}
