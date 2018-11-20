using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jwell.Framework.Domain.Uow
{
    public class CurrentUnitOfWork : ICurrentUnitOfWork
    {
        private const string CALL_CONTEXT_NAME = "Jwell.UnitOfWork";

        public IUnitOfWork Current
        {
            get
            {
                object data = CallContext.GetData(CALL_CONTEXT_NAME);
                if (data != null)
                {
                    return (IUnitOfWork)data;
                }

                return null;
            }
            set
            {
                if (value == null)
                {
                    if (CallContext.GetData(CALL_CONTEXT_NAME) != null)
                    {
                        CallContext.SetData(CALL_CONTEXT_NAME, null);
                    }
                }
                else
                {
                    if (CallContext.GetData(CALL_CONTEXT_NAME) != null)
                    {
                        throw new InvalidOperationException("A UnitOfWork instance has been created and not been disposed");
                    }

                    CallContext.SetData(CALL_CONTEXT_NAME, value);
                }
            }
        }
    }
}
