using System;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace net.unity3d
{
    class USerialize
    {
        public USerialize()
        {
        }

        public static string handlePkgStr(string PkgStr, int Len)
        {
            byte[] byteArray = Encoding.Default.GetBytes(PkgStr);
            string GetMessage = Encoding.Default.GetString(byteArray, 0, Len);
            return GetMessage;
        }
    }
}
