using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace utils
{
    class FileReader
    {
        private static FileReader _this;

        public static FileReader getInstance()
        {
            if( _this == null )
            {
                _this = new FileReader();
            }
            return _this;
        }


        private string content;

        private byte[] buff = new byte[1024];

        public delegate void CallBack(string content);

        public CallBack _cb;

        public void read( string path, CallBack cb)
        {
            _cb = cb;

            if( !File.Exists( path ) )
            {
                Console.WriteLine( "file not exists" );
                return;
            }

            FileStream fs = new FileStream( path, FileMode.Open );

            fs.BeginRead( buff, 0, buff.Length, cb_read, fs );
        }


        private void cb_read(IAsyncResult ar )
        {
            FileStream fs = ar.AsyncState as FileStream;
            if( fs == null )
            {
                Console.WriteLine( "fs is null" );
                return;
            }

            int r = fs.EndRead( ar );

            if( r == 0 )
            {
                fs.Close();

                //Console.WriteLine( "content: " );
                //Console.WriteLine( this.content );

                _cb(this.content);
                return;
            }
            else
            {
                this.content = this.content + System.Text.Encoding.Default.GetString( buff, 0, r );

                //Console.WriteLine( "己读取：" + this.content.Length );

                if( this.content.Length < fs.Length )
                {
                    fs.BeginRead( buff, 0, buff.Length, cb_read, fs );
                }    
            }

        }
    }
}
