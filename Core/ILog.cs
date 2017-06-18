using System;

namespace Core
{
    public enum Category
    {
        Normal,
        System,
        Chat,
        Login,
        Exception,
    }

    public interface ILog
    {
        void Trace(string message,
            /* [CallerMemberName]*/ string memberName = "",
            /* [CallerFilePath] */string sourceFilePath = "",
            /* [CallerLineNumber]*/ int sourceLineNumber = 0);

        void Debug(string message, Category category = Category.Normal);
        void Debug(string message, Category category = Category.Normal, params object[] args);

        void Info(string message);
        void Info(string message, Category category);
        void Info(string serverId, string channelId, string ip, string message, Category category = Category.Normal);
        void Info(string message, params object[] args);
        void Info(string message, Category category, params object[] args);

        void Warn(string message, Category category = Category.Normal);
        void Warn(string message, Category category = Category.Normal, params object[] args);

        void Error(string message, Category category = Category.Normal);
        void Error(string message, Category category = Category.Normal, params object[] args);

        void Fatal(string message, Category category = Category.Normal);
        void Fatal(string message, Category category = Category.Normal, params object[] args);

        void Exception(Exception exception, string message = "");
    }
}
