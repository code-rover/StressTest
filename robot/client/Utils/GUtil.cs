using System;
using System.Collections;
using System.Collections.Generic;

public class GUtil
{

    /// <summary>
    /// 获得索引位置
    /// Indexs the of.
    /// </summary>
    /// <returns>
    /// The of.
    /// </returns>
    /// <param name='val'>
    /// Value.
    /// </param>
    /// <param name='array'>
    /// Array.
    /// </param>
    public static int indexOf<T>( T val, T[] array )
    {
        int result = -1;
        if( null != array )
        {

            for( int index = 0; index < array.Length; index++ )
            {
                //				if(array[index].ToString() == val.ToString())
                //					return index;
                if( object.Equals( array[ index ], val ) )
                {
                    return index;
                }
            }
        }
        return result;
    }
    /// 返回一个数据的枚举
    public static Dictionary<TKey, TValue> initDictionary<TKey, TValue>( params object[] args )
    {
        Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();
        for( int index = 0; ( index + 1 ) < args.Length; index += 2 )
        {
            result.Add( ( TKey ) args[ index ], ( TValue ) args[ index + 1 ] );
        }
        return result;
    }

    public static int indexOfStr( byte val, byte[] array )
    {
        int result = -1;
        if( null != array )
        {
            for( int index = 0; index < array.Length; index++ )
            {
                if( array[ index ] == val )
                    return index;
            }
        }
        return result;

    }
    /// <summary>
    /// 解析字符串
    /// </param>
    public static string formatString( string val )
    {
        string result = val;
        if( null != result )
        {
            result = result.Replace( "\\n", "\n" );
        }
        return result;
    }
    /// 补充空格
    public static string repaceStringSpace( string val, int len )
    {
        string result = val;
        while( result.Length < len )
        {
            result += " ";
        }
        return result;
    }
    /// 定长换行
    public static string repaceStringLine( string val, int lenWidth, int minLine )
    {
        if( null == val )
            val = "";
        string[] arr = val.Split( "\n".ToCharArray() );
        List<string> list = new List<string>();
        ///遍历数组
        for( int index = 0; index < arr.Length; index++ )
        {
            ///生成数组长度
            while( arr[ index ].ToString().Length > lenWidth )
            {
                list.Add( arr[ index ].ToString().Substring( 0, lenWidth ) );
                arr[ index ] = arr[ index ].ToString().Substring( lenWidth );
            }
            arr[ index ] = repaceStringSpace( arr[ index ], lenWidth );
            list.Add( arr[ index ] );
        }
        while( list.Count < minLine )
        {
            list.Add( repaceStringSpace( "", lenWidth ) );
        }
        string result = "";
        for( int index = 0; index < list.Count; index++ )
        {
            result += list[ index ];
            if( ( index + 1 ) != list.Count )
                result += "\n";
        }
        return result;
    }
    /// 换行
    public static string repaceStringLineMid( string val, int lenWidth, int minLine )
    {
        if( null == val )
            val = "";


        string[] arr = val.Split( "\n".ToCharArray() );
        List<string> list = new List<string>();
        ///遍历数组
        for( int index = 0; index < arr.Length; index++ )
        {
            ///生成数组长度
            while( arr[ index ].ToString().Length > lenWidth )
            {
                list.Add( arr[ index ].ToString().Substring( 0, lenWidth ) );
                arr[ index ] = arr[ index ].ToString().Substring( lenWidth );
            }
            arr[ index ] = repaceStringSpace( "", ( lenWidth - arr[ index ].Length ) / 2 ) + arr[ index ];
            arr[ index ] = repaceStringSpace( arr[ index ], lenWidth );
            list.Add( arr[ index ] );
        }
        while( list.Count < minLine )
        {
            list.Add( repaceStringSpace( "", lenWidth ) );
        }
        string result = "";
        for( int index = 0; index < list.Count; index++ )
        {
            result += list[ index ];
            if( ( index + 1 ) != list.Count )
                result += "\n";
        }
        return result;
    }

    /// <summary>
    /// 对字符串进行限制,限制长度,限制内容
    /// </summary>
    public static string formatString( string val, string format )
    {
        if( null == format || null == val || format.Length <= 0 || val.Length == format.Length )
            return val;

        if( val.Length > format.Length )
            return val.Substring( val.Length - format.Length );

        return format.Substring( 0, format.Length - val.Length ) + val;
    }
    /// 保留n为消暑的值返回
    public static string numToString( float val, int dot )
    {
        string[] sub = val.ToString().Split( ".".ToCharArray() );
        if( float.IsNaN( val ) )
            val = 1f;
        ///为空的情况下
        if( sub.Length == 0 && dot <= 0 )
            return "0";
        if( sub.Length == 0 )
            return "0" + "." + cloneString( "0", dot );
        ///返回整数
        if( dot <= 0 )
            return sub[ 0 ];
        ///加上0的消暑
        if( sub.Length == 1 )
            return sub[ 0 ] + "." + cloneString( "0", dot );
        /// 截取返回
        if( sub[ 1 ].Length >= dot )
            return sub[ 0 ] + "." + sub[ 1 ].Substring( 0, dot );
        /// 返回数据
        return sub[ 0 ] + "." + sub[ 1 ] + cloneString( "0", dot - sub[ 1 ].Length );
    }
    public static string cloneString( string val, int len )
    {
        string result = "";
        for( int i = 0; i < len; i++ )
        {
            result += val;
        }
        return result;
    }

    /// <summary>
    /// 创建2维数组<测试通过>
    /// </returns>
    public static string[][] stringToArray( string val, string index1, string index2 )
    {
        string[][] result = null;
        if( null != val )
        {
            string[] arr_1 = val.Split( index1.ToCharArray() );
            int index;
            result = new string[ arr_1.Length ][];
            for( index = 0; index < arr_1.Length; index++ )
            {
                result[ index ] = arr_1[ index ].Split( index2.ToCharArray() );
            }

        }

        return result;
    }

    /// <summary>
    /// 生成一个数组
    /// </typeparam>
    public static T[] NewArray<T>( int len )
    {
        T[] result = new T[ len ];
        for( int index = 0; index < result.Length; index++ )
        {
            //result[index] = GameObject.Instantiate(typeof(T));
        }
        return result;
    }



    /// <summary>
    /// 返回列
    /// </param>
    public static int maxRow( string text )
    {
        if( null == text )
            return 0;
        int result = 0;
        string[] texts = text.Split( "\n".ToCharArray() );
        foreach( string val in texts )
        {
            result = Math.Max( val.Length, result );
        }
        return result;
    }
    /// <summary>
    /// 返回行
    /// </param>
    public static int maxCol( string text )
    {
        if( null == text )
            return 0;
        return text.Split( "\n".ToCharArray() ).Length;
    }
    /// 正负查看
    public static int sign( float val )
    {
        if( val >= 0 )
            return 1;
        else
            return -1;
    }

    /// <summary>
    /// Atos the B time.
    /// a到b的重力加速度时间
    /// </param>
    public static float AtoBTime( float s_start, float s_end, float s_speed, float s_gravity )
    {
        float result = 0f;
        float t_start = s_start;
        float t_end = s_end;
        float t_speed = s_speed;
        float t_gravity = s_gravity;
        float t_time = 0.03f;
        while( true )
        {
            ///模拟出来的数据异步
            if( sign( t_speed ) == sign( s_gravity ) )
                //				if((s_start > s_end) != (t_start > t_end))
                if( Math.Abs( Math.Abs( t_start ) - Math.Abs( t_end ) ) < Math.Abs( t_speed * t_time ) )
                {
                    result += t_time;
                    break;
                }
            //			if((s_start > s_end) != (t_start + t_speed > t_end))
            //			{
            //				result += Math.Abs(t_start - t_end) / t_speed;
            //				break;
            //			}
            t_start += t_speed * t_time;
            t_speed += t_gravity * t_time;
            result += t_time;
            //			result += 1f;
            //			t_start += t_speed;
            //			t_speed += s_gravity;
            //			
            if( result >= 100f )
                return 0;
        }
        return result;
    }

    /// 数字矫正
    public static string formatInt( int val )
    {
        if( val > 100000000 )
            return ( val / 100000000 ) + "亿";
        if( val > 1000000 )
            return ( val / 10000 ) + "万";
        return val.ToString();
    }



    /// <summary>
    /// 移除字符串中的（clone）字段
    /// </summary>
    /// <returns>
    /// The clone.
    /// </returns>
    /// <param name='str'>
    /// String.
    /// </param>
    public static string formateStrRemoveClone( string str )
    {
        string ret = str;

        if( !ret.Contains( "(Clone)" ) )
        {
            return ret;
        }


        while( ret.Contains( "(Clone)" ) )
        {
            ret = ret.Substring( 0, ret.LastIndexOf( "(Clone)" ) );
        }

        return ret;
    }


    /// 格式化时间
    public static string formatTime( double time )
    {
        string result = "";
        //我的时间
        time += System.DateTimeOffset.Now.Offset.TotalSeconds;
        //时
        result += GUtil.formatString( ( ( int ) ( time / 3600 ) % 24 ).ToString(), "00" ) + " : ";
        //分
        result += GUtil.formatString( ( ( int ) ( time % 3600 ) / 60 ).ToString(), "00" ) + " : ";
        //		result += GUtil.formatString((t / 60).ToString(), "00") + " : ";
        //秒
        result += GUtil.formatString( ( ( int ) ( time % 60 ) ).ToString(), "00" ) + ".";
        //毫秒
        result += GUtil.formatString( ( ( int ) ( ( time % 1f ) * 100 ) ).ToString(), "00" );

        //返回
        return result;
    }

    //millisecond
    public static double getMS() 
    {
        //DateTime dt1 = new DateTime(1970,1,1);
        TimeSpan ts = DateTime.Now - new DateTime(1970,1,1);
       
        return ts.TotalMilliseconds;
    }

    public static long getEpoch()
    {
        return ( DateTime.UtcNow.Ticks - new DateTime( 1970, 1, 1 ).Ticks ) / 10000; //注意这里有时区问题，用now就要减掉8个小时
    }

    //
    public static string getTimeMs()
    {
        return System.DateTime.Now + "." + DateTime.Now.Millisecond;         
    }
}