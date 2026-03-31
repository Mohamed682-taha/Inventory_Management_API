namespace Domain.Exceptions
{
    public class BadRequestException : Exception
    {
        public List<string> Errors { get; set; }
        public BadRequestException(List<string> errors) : base("Bad Request")
        {
            Errors = errors;
        }
    }
}
