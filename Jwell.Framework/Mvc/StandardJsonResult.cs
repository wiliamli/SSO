using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Jwell.Framework.Utilities;

namespace Jwell.Framework.Mvc
{
    public class StandardJsonResult : ActionResult, IStandardResult
    {
        public string ContentType { get; set; }

        #region  统一JSON数据结构

        public bool Success { get; set; }

        public string Message { get; set; }

        public StandardJsonResult()
        {
            this.ContentType = "application/json";
        }


        public void StandardAction(Action action)
        {
            try
            {
                action();
                Success = true;
            }
            catch (Exception ex)
            {
                Success = false;
                throw ex;
            }
        }

        #endregion

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = this.ContentType;
            response.ContentEncoding = Encoding.UTF8;
            response.Write(Serializer.ToJson(this.ToJsonObject()));
        }

        protected virtual IStandardResult ToJsonObject()
        {
            var result = new StandardResult
            {
                Success = this.Success,
                Message = this.Message
            };
            return result;
        }
    }

    public class StandardJsonResult<T> : StandardJsonResult, IStandardResult<T>
    {
        public T Data { get; set; }

        protected override IStandardResult ToJsonObject()
        {
            var result = new StandardResult<T>
            {
                Success = this.Success,
                Message = this.Message,
                Data = this.Data
            };
            return result;
        }
    }
}