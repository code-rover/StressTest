using System.Collections;
using System.Collections.Generic;
using System.Threading;
using utils;

/// 函数定义
public delegate void Function();
public delegate void FunctionDouble(double loader_id);
//public delegate void FunctionGameObject(GameObject sObj);
/// 函数返回值的定义
public delegate void FunctionUlong(ulong sData);
/// 函数定义 注册侦听的函数
public delegate void FunctionListenerEvent(UtilListenerEvent sData);
/// 通用类型
public delegate void FunctionObject(object sData);
//public delegate (object)FunctionObject; 

/// 侦听函数
public class UtilListener
{

	/// 我的侦听列表
	protected static Dictionary<string, List<UtilListenerEvent>> _hashListener = new Dictionary<string, List<UtilListenerEvent>>();
	
	/// 通过时间戳调用的侦听
	protected static Dictionary<double, List<UtilListenerEvent>> _hashDispatchTime = new Dictionary<double, List<UtilListenerEvent>>();


	private static double _listenerTime = 0d;

	/// 获得id
	private static uint _signID;
	private static string CALL_TIME_HD
	{
		get{return "public class UtilListener.CALL_TIME_HD" + (_signID++);}
	}

    private static System.Object lockThis = new System.Object();

    //private static int curThread = 0;

	/// 注册侦听函数(注册两次调用两次,需要注意)
	public static void addEventListener(string sEventName, FunctionListenerEvent sEvent, object sTarget)
	{
        //if(curThread == 0)
        //    curThread = Thread.CurrentThread.GetHashCode();

        //Logger.Info( "current thread: " + Thread.CurrentThread.GetHashCode() );

        //if( curThread != Thread.CurrentThread.GetHashCode() )
        //{
            //Logger.Info( "thread error  ffffffffffffffffffffffffffffff" );
        //}
        
        UtilListenerEvent listenerObj = new UtilListenerEvent();
        listenerObj.eventName = sEventName;
        listenerObj.eventTarget = sTarget;
        listenerObj.eventListener = sEvent;

        lock(lockThis)
        {
            /// 放入缓存
            if( !_hashListener.ContainsKey( sEventName ) )
                _hashListener.Add( sEventName, new List<UtilListenerEvent>() );

            /// 添加进去
            _hashListener[ sEventName ].Add( listenerObj );
        }
	}
	/// 注册侦听简写
	public static void addEventListener(string sEventName, FunctionListenerEvent sEvent)
	{
		addEventListener(sEventName, sEvent, null);
	}
	/// 通过名字派调函数
	public static void dispatch(string sEventName, object sArgs)
	{
		/// 我的全部调用
		if(!_hashListener.ContainsKey(sEventName))
		{
			//UIScreenLog.LogError("No listener added " + sEventName);
            utils.Logger.Warn( "No listener added " + sEventName );
			return;
		}
		
		/// 调用全部函数
		foreach(UtilListenerEvent evt in _hashListener[sEventName].ToArray())
		{
			try
			{
				UtilListenerEvent args = evt.clone();
				args.eventArgs = sArgs;
				evt.eventListener(args);
			}catch(System.Exception e){
                //utils.Logger.Error(sEventName + ":" + evt.eventListener.ToString());
                utils.Logger.Error( e.ToString());
            }
		}
		
	}
	/// 这届跑侦听
	public static void dispatch(string sEventName)
	{
		dispatch(sEventName, null);
	}
	
	/// 过了多少秒后派调
	public static void dispatchTime(string sEventName, float time, object sArgs)
	{
		UtilListenerEvent args = new UtilListenerEvent();
		args.eventArgs = sArgs;
		args.eventName = sEventName;
		double timeStamp = time + _listenerTime;
		if(!_hashDispatchTime.ContainsKey(timeStamp))
			_hashDispatchTime.Add(timeStamp, new List<UtilListenerEvent>());
		_hashDispatchTime[timeStamp].Add(args);
	}
	/// 过了多少秒后派调
	public static void dispatchTime(string sEventName, float time)
	{
		dispatchTime(sEventName, time, null);
	}
	
	
	/// 移除侦听
	public static void removeListener(string sEventName, FunctionListenerEvent sEvent)
	{
		if(!_hashListener.ContainsKey(sEventName))
			return;
		/// 遍历侦听
		foreach(UtilListenerEvent listener in _hashListener[sEventName].ToArray())
		{
			/// 移除制定函数的侦听
			if(listener.eventListener == sEvent)
			{
                lock( lockThis )
                {
                    _hashListener[ sEventName ].Remove( listener );
                }
			}
		}
	}
	/// 移除这个名字的全部侦听
	public static void removeListener(string sEventName)
	{
		if(!_hashListener.ContainsKey(sEventName))
			return;

        lock( lockThis )
        {
            _hashListener.Remove( sEventName );
        }

	}
	/// 是否含有侦听
	public static bool hasListener(string sEventName)
	{
		return _hashListener.ContainsKey(sEventName);
	}
	/// 清除侦听
	public static void clearListenerDispatchTime()
	{
		_hashDispatchTime.Clear();
	}
	public static void clearListener()
	{
		_hashListener.Clear();
		_hashDispatchTime.Clear();
	}
	
	
	/// 过n秒后调用的函数
	public static string calledTime(FunctionListenerEvent sFun, float time, object sTarget, object sArgs)
	{
		if(null == sFun) return null;
		string eventListener = CALL_TIME_HD;
		addEventListener(eventListener, sFun, sTarget);
		addEventListener(eventListener, calledTimdHD);
		dispatchTime(eventListener, time, sArgs);
		return eventListener;
	}
	
	/// 过n秒后调用的函数
	public static string calledTime(Function sFun, float time)
	{
		if(null == sFun) return null;
		string eventListener = CALL_TIME_HD;
		addEventListener(eventListener, calledTimdHD);
		dispatchTime(eventListener, time, sFun);
		return eventListener;
	}
	/// 过n秒后调用的函数 侦听实现 
	private static void calledTimdHD(UtilListenerEvent sEvent)
	{
		removeListener(sEvent.eventName);
		if(null != sEvent.eventArgs && sEvent.eventArgs is Function)
		{
			((Function)sEvent.eventArgs)();
		}
	}
}
/// 侦听类型
public class UtilListenerEvent
{
	
	/// 侦听名字
	public string eventName;
	/// 侦听对象
	public object eventArgs;
	/// 侦听的对象调用
	public object eventTarget;
	/// 侦听函数
	public FunctionListenerEvent eventListener;
	
	/// 复制出来一份侦听
	public UtilListenerEvent clone()
	{
		UtilListenerEvent result = new UtilListenerEvent();
		result.eventName = eventName;
		result.eventTarget = eventTarget;
		result.eventListener = eventListener;
		return result;
	}
}
