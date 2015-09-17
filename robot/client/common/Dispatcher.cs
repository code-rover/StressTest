using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace robot.client.common
{
    class Dispatcher
    {
        private static int _IDSign = 10;
        public static int IDSign
        {
            get
            {
                return _IDSign++;   //Safe?
            }
        }

        public static uint addListener( FunctionListenerEvent sFunction, object sTarget )
        {
            if( null == sFunction )
                return 0;

            uint listenerId = ( uint ) Dispatcher.IDSign;
            Console.WriteLine( "listenerId: " + listenerId );
            string eventListener = "DataModeServerListener_" + listenerId;
            UtilListener.addEventListener( eventListener, sFunction, sTarget );
            return listenerId;
        }

        /// 释放侦听
        public static void dispatchListener( uint sListener, object sArgs )
        {
            string eventListener = "DataModeServerListener_" + sListener;
            UtilListener.dispatch( eventListener, sArgs );
            UtilListener.removeListener( eventListener );
        }
    }
}
