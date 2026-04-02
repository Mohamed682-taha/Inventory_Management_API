namespace Domain.Exceptions
{
    public class BadRequestException : Exception
    {
        public IEnumerable<string> Errors { get; set; }
        public BadRequestException(IEnumerable<string> errors) : base("Bad Request")
        {
            Errors = errors;
        }
    }
}
