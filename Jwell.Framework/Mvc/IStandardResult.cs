namespace Jwell.Framework.Mvc
{
    public interface IStandardResult
    {
        bool Success { get; set; }
        string Message { get; set; }
    }

    public interface IStandardResult<T> : IStandardResult
    {
        T Data { get; set; }
    }
}
