using Logger.Interface;
using NLog;

namespace Logger
{
    public class NLogger: ILog
    {
        public void Log(string lineToLog)
        {
            ILogger logger = LogManager.GetLogger(lineToLog);
            logger.Error(lineToLog);
        }
    }
}
