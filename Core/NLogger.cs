using System;
using NLog;

namespace Core
{
    public class NLogger : ILog
    {
        private readonly Logger _logger;

        public NLogger(Logger log)
        {
            _logger = log;
        }


        public void Trace(string message, string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
        {
            _logger.Trace(message);
        }

        public void Debug(string message, Category category = Category.Normal)
        {
            _logger.Debug("{0}|{1}", category.ToString().ToUpper(), message);
        }

        public void Debug(string message, Category category = Category.Normal, params object[] args)
        {
            _logger.Debug("{0}|{1}", category.ToString().ToUpper(), string.Format(message, args));
        }

        #region Info

        public void Info(string message)
        {
            _logger.Info("{0}|{1}", Category.Normal.ToString().ToUpper(), message);
        }

        public void Info(string message, Category category)
        {
            _logger.Info("{0}|{1}", category.ToString().ToUpper(), message);
        }

        public void Info(string serverId, string channelId, string ip, string message,
            Category category = Category.Normal)
        {
            _logger.Info("{0}|{1}|{2}|{3}|{4}", category.ToString().ToUpper(), serverId, channelId, ip, message);
        }

        public void Info(string message, params object[] args)
        {
            _logger.Info("{0}|{1}", Category.Normal.ToString().ToUpper(), string.Format(message, args));
        }

        public void Info(string message, Category category, params object[] args)
        {
            _logger.Info("{0}|{1}", category.ToString().ToUpper(), string.Format(message, args));
        }

        #endregion

        public void Warn(string message, Category category = Category.Normal)
        {
            _logger.Warn("{0}|{1}", category.ToString().ToUpper(), message);
        }

        public void Warn(string message, Category category = Category.Normal, params object[] args)
        {
            _logger.Warn("{0}|{1}", category.ToString().ToUpper(), string.Format(message, args));
        }

        public void Error(string message, Category category = Category.Normal)
        {
            //_logger.Error(message);
            _logger.Error("{0}|{1}", category.ToString().ToUpper(), message);
        }

        public void Error(string message, Category category = Category.Normal, params object[] args)
        {
            //_logger.Error(message, args);
            _logger.Warn("{0}|{1}", category.ToString().ToUpper(), string.Format(message, args));
        }

        public void Fatal(string message, Category category = Category.Normal)
        {
            //_logger.Fatal(message);
            _logger.Fatal("{0}|{1}", category.ToString().ToUpper(), message);
        }

        public void Fatal(string message, Category category = Category.Normal, params object[] args)
        {
            //_logger.Fatal(message, args);
            _logger.Fatal("{0}|{1}", category.ToString().ToUpper(), string.Format(message, args));
        }
        
        public void Exception(Exception exception, string message = "")
        {
            _logger.Error("{0}|{1} {2}", Category.Exception, message, exception);
        }
    }
}
