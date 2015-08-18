using System;

[assembly: log4net.Config.XmlConfigurator( Watch = true )]
namespace utils
{
    
    public class Logger
    {
        #region static void WriteLog(string msg)
        public static void Info( string msg )
        {
            log4net.ILog log = log4net.LogManager.GetLogger( "Logger" );
            
            log.Info( msg );
            Console.WriteLine( msg );
        }
        #endregion

        public static void Debug( string msg )
        {
            log4net.ILog log = log4net.LogManager.GetLogger( "Logger" );
            log.Debug( msg );
            Console.WriteLine( msg );
        }

        public static void Error( string msg )
        {
            log4net.ILog log = log4net.LogManager.GetLogger( "Logger" );
            log.Error( msg );
            Console.WriteLine( "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX: " + msg );
        }

        public static void Warn( string msg )
        {
            log4net.ILog log = log4net.LogManager.GetLogger( "Logger" );
            log.Warn( msg );
            Console.WriteLine( msg );
        }
    }    
};
