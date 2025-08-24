namespace Domain.Entities
{
    public record class Result<T>
    {
        public T Value { get; set; }
        public bool IsSuccess { get; set; }
        public string Error { get; set; }

        private Result(T value, bool isSuccess, string error)
        {
            Value = value;
            IsSuccess = isSuccess;
            Error = error;
        }


        public static Result<T> Success(T value) => new Result<T>(value, true, null);
        public static Result<T> Failure(string error) => new Result<T>(default, false, error);
    }
}
