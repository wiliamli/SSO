namespace Jwell.Framework.Mvc
{
    public class StandardResult : IStandardResult
    {

        public bool Success { get; set; }

        public string Message { get; set; }

        public void Succeed()
        {
            this.Success = true;
        }

        public void Fail()
        {
            this.Success = false;
        }

        public void Succeed(string message)
        {
            this.Success = true;
            this.Message = message;
        }

        public void Fail(string message)
        {
            this.Success = false;
            this.Message = message;
        }
    }

    public class StandardResult<T> : StandardResult, IStandardResult<T>
    {
        public T Data { get; set; }
    }
}
