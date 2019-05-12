using System;

namespace Wallee.Utils
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}