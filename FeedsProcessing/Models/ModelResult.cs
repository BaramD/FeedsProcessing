namespace FeedsProcessing.Models
{
    public class ModelResult<T>
    {
        public T Model { get; private set; }
        public string Error { get; private set; }
        public static ModelResult<T> Ok(T model) => new ModelResult<T> { Model = model };
        public static ModelResult<T> Fail(string error) => new ModelResult<T> { Error = error };
        public bool IsOk() => Error == null;

    }
}