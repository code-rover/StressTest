using System;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace net
{
    /// <summary>
    /// 网络异常.
    /// </summary>
    public class NetException : Exception
    {
        public NetException(string msg,uint code) : base(msg)
        {
            _code = code;
        }

        /// <summary>
        /// 获取网络错误码.
        /// </summary>
        public uint Code
        {
            get{ return _code; }
        }

        private uint _code;
    }

    /// <summary>
    /// 事件参数.
    /// </summary>
    public class ArgsEvent
    {
        public ArgsEvent(object obj)
        {
            _obj = obj;
        }

        public virtual object Data
        {
            get { return _obj; }
        }

        public virtual T getData<T>()
        {
            return (T)_obj;
        }

        private object _obj;
    }

    /// <summary>
    /// 网络事件参数.
    /// </summary>
    public class ArgsNetEvent : ArgsEvent
    {
        public ArgsNetEvent(IProtocal pro) : base(pro)
        {

        }

        public IProtocal Protocal
        {
            get { return getData<IProtocal>(); }
        }
    }

    /// <summary>
    /// 网络事件声明.
    /// </summary>
    /// <param name="sender">事件发送者</param>
    /// <param name="args">事件参数</param>
    public delegate void HandleNetEvent(object sender,ArgsEvent args);
	
	public delegate void EHandleNetEvent(ArgsEvent args);

    /// <summary>
    /// 网络连接接口.
    /// </summary>
    public interface IConnection
    {
		
		bool send(IProtocal pro, bool isNotify);
		bool isConnected();
		
		bool is_error();
		bool is_relogin();
		void set_is_relogin(bool pbIsReLogin);
		
		void set_account(string psChannel, string psAccount);
		void set_macid(string psMacId);

        void send(IProtocal pro);

        void addMethodToFactory(ushort msg, CreateProtocal fac_mtd);
        void addListenEvent(ushort msg, HandleNetEvent event_handle);

        /// <summary>
        /// 打开一个网络连接.
        /// </summary>
        /// <param name="remote_ip">远程ip地址.</param>
        /// <param name="remote_port">远程端口.</param>
        /// <param name="timeout">超时(秒).</param>
        void open(string remote_ip, short remote_port, int timeout);
        void open(NoteServer sNote);
		void reopen(string id_channel, string id_account, string id_mac); 
		
		/// <summary>
		/// 断网重连后发生的事件.
		/// </summary>
		event HandleNetEvent EventReConnected;
		
		 /// <summary>
        /// 网络连接后，发生的事件.
        /// </summary>
        event HandleNetEvent EventConnected;

        /// <summary>
        /// 网络关闭后，发生的事件.
        /// </summary>
        event HandleNetEvent EventClosed;

        /// <summary>
        /// 网络关闭后，发生的事件.
        /// </summary>
        event HandleNetEvent EventTick;

        /// <summary>
        /// 网络连接前，发生的事件
        /// </summary>
        event HandleNetEvent EventPreConnect;

        /// <summary>
        /// 网络关闭前，发生的事件
        /// </summary>
        event HandleNetEvent EventPreClose;
		
		/// <summary>
        /// 获取远程端口.
        /// </summary>
        short RemotePort
        {
            get;
        }

        /// <summary>
        /// 获取网络通信适配器.
        /// </summary>
        ISession Session
        {
            get;
        }
		
        /// <summary>
        /// 当前连接是否打开.
        /// </summary>
        bool IsOpen
        {
            get;
        }

        /// <summary>
        /// 获取远程IP.
        /// </summary>
        string RemoteIP
        {
            get;
        }


		void tick();

        /// <summary>
        /// 关闭当前连接.
        /// </summary>
        void close();
		
		void begin_close();
		
    }

    public class HeaderBase
    {
        public HeaderBase(byte v, uint l, uint t, byte e, byte c, ushort o)
        {
            _bVer = v;
            _uiLen = l;
            _uiTag = t;
            _bCompress = c;
            _bEncrypKey = e;
            _usOpcode = o;

        }
		
        private uint _uiLen;    //total length, must be the first member
        private byte _bEncrypKey;    //encrypt index,if this is seted by 0,not encrypt
        private byte _bCompress;		//is Compress, is this is seted by 0,not encrypt
        private byte _bVer;     //version & check code
        private ushort _usOpcode; //operate code
        private uint _uiTag;    //the tag which can uniquely identify one msg, host byte order is enough; 0 means no use; 

		public void writeBuffer(byte[] buff)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(buff, 0, buff.Length);
            ms.Seek(0, SeekOrigin.Begin);
            BinaryReader br = new BinaryReader(ms);
            _uiLen = (uint)(System.Net.IPAddress.NetworkToHostOrder(br.ReadInt32()) - getHdrLen());
            _bEncrypKey = br.ReadByte();
            _bCompress = br.ReadByte();
            _bVer = br.ReadByte();
            _usOpcode = (ushort)System.Net.IPAddress.NetworkToHostOrder(br.ReadInt16());
            _uiTag = (uint)System.Net.IPAddress.NetworkToHostOrder(br.ReadInt32());
        }
		
		        /// 数据长度.
        public uint Length
        {
            get { return _uiLen; }
            set { _uiLen = value; }
        }

        /// 协议版本号.
        public byte Version
        {
            get { return _bVer; }
            set { _bVer = value; }
        }
		
		/// 是否压缩
        public byte bCompress
        {
            get { return _bCompress; }
            set { _bCompress = value; }
        }

        ///协议号
        public ushort Opcode
        {
            get { return _usOpcode; }
            set { _usOpcode = value; }
        }

        /// 协议类型.
        public uint Tag
        {
            get { return _uiTag; }
            set { _uiTag = value; }
        }

        /// 加密关键字.
        public byte EncrypKey
        {
            get { return _bEncrypKey; }
            set { _bEncrypKey = value; }
        }


        public byte[] getBuffer()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            bw.Write(_uiLen);
            bw.Write(_bEncrypKey);
            bw.Write(_bCompress);
            bw.Write(_bVer);
            bw.Write(_usOpcode);
            bw.Write(_uiTag);
            return ms.ToArray();
        }


		
		public static int getHdrLen()
        {
            return 13;
        }


    }

    /// <summary>
    /// 与服务器通信的协议接口.
    /// </summary>
    public interface IProtocal
    {
        bool analysisBuffer(byte[] data);
        byte[] getBuffer();

        ushort Message
        {
            get;
        }
    };

    public delegate IProtocal CreateProtocal(ushort msg,HeaderBase h);
	
	public class EListenerNet
    {
        public EListenerNet(ushort msg)
        {
            _message = msg;
        }
		
		private ushort _message;
		
		/// <summary>
        /// 消息类型.
        /// </summary>
        public ushort Message
        {
            get { return _message; }
        }
		
		/// <summary>
        /// 事件监听句柄.
        /// </summary>
        public event EHandleNetEvent ListenerEvent;

        /// <summary>
        /// 调用事件.
        /// </summary>
        /// <param name="sender">事件发送者.</param>
        /// <param name="args">事件参数.</param>
        public void callEvent(ArgsEvent args)
        {
            ListenerEvent(args);
        }

    }

    /// <summary>
    /// 网络监听
    /// </summary>
    public class ListenerNet
    {
        public ListenerNet(ushort msg)
        {
            _message = msg;
        }
		
		private ushort _message;
		
        /// <summary>
        /// 消息类型.
        /// </summary>
        public ushort Message
        {
            get { return _message; }
        }
		
        /// <summary>
        /// 事件监听句柄.
        /// </summary>
        public event HandleNetEvent EventListener;

        /// <summary>
        /// 调用事件.
        /// </summary>
        /// <param name="sender">事件发送者.</param>
        /// <param name="args">事件参数.</param>
        public void callEvent(object sender,ArgsEvent args)
        {
            EventListener(sender, args);
        }
 
    };

    /// <summary>
    /// 网络请求及监听处理.
    /// </summary>
    public interface ISession
    {
		
		bool is_error();
		
		bool is_relogin();
		
		void set_is_relogin(bool pbIsReLogin);
		
		void set_conn(IConnection Conn);
		
		void set_tcp_client(TcpClient Client);
		
		void set_net_stream(NetworkStream net_stream);
		
		/// <summary>
        /// 网络监听器的索引器.
        /// </summary>
        /// <param name="msg">消息类型</param>
        /// <returns>网络监听器</returns>
        ListenerNet this[ushort msg]
        {
            get;
        }
		
		/// <summary>
        /// 添加协议创建的工厂方法.
        /// </summary>
        /// <param name="msg">消息类型.</param>
        /// <param name="fac_mtd">工厂方法.</param>
        void addFactoryMethod(ushort msg, CreateProtocal fac_mtd);
		
        /// <summary>
        /// 向服务器发送请求.
        /// </summary>
        /// <param name="pro">协议</param>
        /// <param name="isNotify">是否为通知请求</param>
        /// <returns>请求是否成功</returns>
        bool request(IProtocal pro, bool isNotify);
    };
}
