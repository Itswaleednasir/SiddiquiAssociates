using System;

namespace MyClientCoreProject.Utilities
{
    public interface ICustomLogger
    {
        void Exceptions(Exception ex, string controllerName, string actionName);
        void LogException(Exception ex, string controllerName, string actionName);
    }
}