using System;
using System.Collections.Generic;
using System.Text;

namespace Jwell.Framework.Ioc
{
    public interface ILifetimeEvents
    {
        void OnActivating();

        void OnActivated();

        void OnRelease();
    }
}
