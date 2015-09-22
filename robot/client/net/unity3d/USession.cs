using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.ComponentModel;
using System.Threading;
using Ionic.Zlib;
using utils;

namespace net.unity3d
{
    class PackageOne
    {
        public static readonly byte VERSION = 0x00;
		public static UInt32 proNum = 0;
 
        public PackageOne(NetworkStream net_stream)
        {
            _completed = false;
            _buff_stream = new MemoryStream();
            _net_stream = net_stream;
            _header = new HeaderBase(0, 0, 0, 0, 0, 0);
            _head_readed = false;
            _head_buff = new byte[HeaderBase.getHdrLen()];
            _head_buff_pos = 0;
        }

        public PackageOne(HeaderBase h, NetworkStream net_stream)
        {
            _header = h;
            _completed = false;
            _buff_stream = new MemoryStream();
            _net_stream = net_stream;
            _head_readed = true;
            _head_buff = null;
            _head_buff_pos = -1;
        }
		
		bool            _completed;
        MemoryStream    _buff_stream;
        HeaderBase          _header;
        NetworkStream   _net_stream;
        bool _head_readed;
        byte[] _head_buff;
        int _head_buff_pos;

		public void setNetWorkStream(NetworkStream NetStream)
		{
			_net_stream = NetStream;
		}

        /// <summary>
        /// 获取头信息.
        /// </summary>
        /// <returns>返回头</returns>
        public HeaderBase getHeader()
        {
            return _header;
        }

        /// <summary>
        /// 网络数据流.
        /// </summary>
        public NetworkStream StreamNet
        {
            get { return _net_stream; }
        }

        /// <summary>
        /// 是否完成打包.
        /// </summary>
        public bool Completed
        {
            get { return _completed; }
        }

        public Stream BufferStream
        {
            get { return _buff_stream; }
        }
		
		/// <summary>
        /// 获取数据字节流.
        /// </summary>
        /// <returns>字节流数组</returns>
        public byte[] packetToArray()
        {
            return _buff_stream.ToArray();
        }
		
		/// <summary>
        /// 数据打包
        /// </summary>
        /// <param name="stream">数据流</param>
        public void makePacket(Stream stream)
        {
			int index = 0;
            while (stream.CanRead)
            {
//				Debug.Log("我接受包裹的时候whele执行的次数" + index);
				index++;
                if (_head_readed)
                {
                    ///已经读完头部数据,读取包体数据.
                    if (_completed)
                        return;
                    ushort cnt = (ushort)(_header.Length - (ushort)_buff_stream.Position);
                    byte[] buff = new byte[cnt];
                    int readed = stream.Read(buff, 0, cnt);
                    if (readed == 0)
                        break;

                    _buff_stream.Write(buff, 0, readed);
                    if (readed == cnt)
                    {
                        _buff_stream.Seek(0, SeekOrigin.Begin);
                        if (_header != null)
                        {
                            ushort tempCnt = (ushort)_header.Length;
                            byte[] tempBuff = new byte[tempCnt];
                            int tempReaded = _buff_stream.Read(tempBuff, 0, tempCnt);
                            if (_header.EncrypKey > 0)
                            {
                                Encryption.encode_decode((UInt32)tempBuff.Length, tempBuff, _header.EncrypKey);
                            }

                            byte[] realBuff = tempBuff;
                            _buff_stream.Seek(0, SeekOrigin.Begin);
                            int realLen = realBuff.Length;
                            if (1 == _header.bCompress)
                            {
                                realBuff = ZlibStream.UncompressBuffer(tempBuff);
                                realLen = realBuff.Length;
                            }
                            _buff_stream.Write(realBuff, 0, realLen);
                        }
                        _completed = true;
						PackageOne.proNum++;
                        _buff_stream.Seek(0, SeekOrigin.Begin);
                    } 
                }
                else
                {
                    ///读取头部数据.
                    int cnt = HeaderBase.getHdrLen() - _head_buff_pos;
                    int readed = stream.Read(_head_buff, _head_buff_pos, cnt);
                    if(readed == 0)
                        break;
                    if (readed == cnt)
                    {
                        _head_readed = true;
                        _head_buff_pos = HeaderBase.getHdrLen();
                        //USerialize loSerialize = new USerialize();
                        _header.writeBuffer(_head_buff);
                    }
                    else
                    {
                        _head_buff_pos += readed;
                    }
                }
            }
        }

    }

    public class NodeQueue
    {
        public ushort msg;
        public ArgsNetEvent args;
    }

    public class USession
    {

        public static readonly byte REQUEST = 0;
        public static readonly byte NOTIFY = 2;

        public static readonly int BUFF_LEN = 1024;
        public static UInt32 IproNum = 0;

        /// <summary>
        /// 网络流读取状态.
        /// </summary>
        sealed class ReadState
        {
            public ReadState(PackageOne pkg)
            {
                Buffer = new byte[BUFF_LEN];
                Pkg = pkg;
            }

            public PackageOne Pkg;
            public byte[] Buffer;
        }

        public USession(NetworkStream net_stream)
        {
			try
			{
				_net_stream = net_stream;
	            _package = new PackageOne(net_stream);
	            _protocal_facs = new Dictionary<ushort, CreateProtocal>();
	            ReadState read = new ReadState(_package);
	            _net_stream.BeginRead(read.Buffer, 0, read.Buffer.Length, new AsyncCallback(OnRead), read);
				
			}
			catch(Exception ex)
			{
				_is_close = true;
				Logger.Error("create session exp = " + ex);
			}
            
        }
		
		public void set_net_stream(NetworkStream net_stream)
		{
			RequestOverTime = DateTime.Now; //现在时间
			RequestTime = DateTime.Now; //现在时间
			try
			{
				_net_stream = net_stream;
				_package = new PackageOne(net_stream);
				ReadState read = new ReadState(_package);
	            _net_stream.BeginRead(read.Buffer, 0, read.Buffer.Length, new AsyncCallback(OnRead), read);
				
			}
			catch(Exception ex)
			{
				_is_close = true;
				Logger.Error("create session exp = " + ex);
			}
		}
		
		private NetworkStream _net_stream;
        private PackageOne _package;
        private Dictionary<ushort, CreateProtocal> _protocal_facs;
		private bool _is_close = false;
		private TcpClient _u_tcp_client;
		private UConnection _my_conn;
		private bool _is_relogin = false;
		private System.DateTime RequestTime =new System.DateTime();
		private System.DateTime RequestOverTime =new System.DateTime();

        public AgentNet agentNet;
		
		public bool is_relogin()
		{
			return _is_relogin;
		}
		
		public void set_is_relogin(bool pbIsReLogin)
		{
			_is_relogin = pbIsReLogin;
		}

		public void set_tcp_client(TcpClient Client)
		{
			_u_tcp_client = Client;
		}
		
		public void set_conn(UConnection Conn)
		{
			_my_conn = Conn;
		}
		
		public bool is_error()
		{
			return _is_close;
		}
		
		/// <summary>
        /// 添加协议创建的工厂方法.
        /// </summary>
        /// <param name="msg">消息类型.</param>
        /// <param name="fac_mtd">工厂方法.</param>
        public void addFactoryMethod(ushort msg, CreateProtocal fac_mtd)
        {
            if (_protocal_facs.ContainsKey(msg))
            {
                throw new NetException("the protocal factory method existed.", NET_MGR_ERR.NM_ERR_PFAC_EXISTED);
            }
            _protocal_facs.Add(msg, fac_mtd);

        }

		        /// <summary>
        /// 网络读事件.
        /// </summary>
        /// <param name="ar"></param>
        void OnRead(IAsyncResult ar)
        {
            long a = ( DateTime.UtcNow.Ticks - new DateTime( 1970, 1, 1 ).Ticks ) / 10000000; //注意这里有时区问题，用now就要减掉8个小时

            Logger.Info( "OnRead.................." + a );
			if(!_net_stream.CanRead)
			{
				ReadState read = new ReadState(_package);
	            _net_stream.BeginRead(read.Buffer, 0, read.Buffer.Length, new AsyncCallback(OnRead), read);
				return;
			}
			try
			{
				if(!ar.IsCompleted)
				{
					Logger.Error("asyn conn is not completed!!");
				}
				
				ReadState rs = (ReadState)ar.AsyncState;

				if(!rs.Pkg.StreamNet.CanRead)
					return;
				
				int len = 0;
				try
				{
					len =  rs.Pkg.StreamNet.EndRead(ar);
				}
				catch(Exception ex)
				{
					_is_relogin = true;
					Logger.Error("socket end read  message = " + ex.Message + " inner ex = " + ex.InnerException);
					return;
				}
				
				if(len < 1)
				{
					return;
					Logger.Error("socket is close");
					_u_tcp_client.Client.Shutdown(SocketShutdown.Both);
					_u_tcp_client.Client.Close();
				}
				
				//RequestOverTime = DateTime.Now;
				
	            MemoryStream ms = new MemoryStream(rs.Buffer,0,len);
	            rs.Pkg.makePacket(ms);
//				int i = 0;
	            while (rs.Pkg.Completed)
	            {
	//				Debug.Log("读取数据>>>>>>>>>>>>" + (i++));
	                ///创建协议.
	                BinaryReader reader = new BinaryReader(rs.Pkg.BufferStream);
	                reader.BaseStream.Seek(0, SeekOrigin.Begin);
	                ushort msg = rs.Pkg.getHeader().Opcode;

					if (_protocal_facs.ContainsKey(msg))
	                {
	                    IProtocal pro = _protocal_facs[msg](msg, rs.Pkg.getHeader());
						
	                    if (false == pro.analysisBuffer(rs.Pkg.packetToArray()))
	                    {
	                        Logger.Error("pro should not be analysised" +msg );
	                        return;
	                    }
						if(PackageOne.proNum > IproNum)
						{
							ArgsNetEvent args = new ArgsNetEvent(pro);
							NodeQueue qn = new NodeQueue();
							qn.msg = msg;
							qn.args = args;

                            Logger.Info( "<======" + "  " + DateTime.Now.Millisecond);
                            this.agentNet.lNetWorker.AddQueue( qn );
						}
	                }
	                _package = rs.Pkg = new PackageOne(rs.Pkg.StreamNet);
	                rs.Pkg.makePacket(ms);
	            }
	
	            ReadState read = new ReadState(_package);

				_net_stream.BeginRead(read.Buffer, 0, read.Buffer.Length, new AsyncCallback(OnRead), read);
					
			}
			catch(Exception ex)
			{
				//_is_close = true;
//				if(Application.isPlaying)
//				{
					Logger.Error("socket error content = " + ex);
//				}
			}
            
        }

        /// <summary>
        /// 网络写事件.
        /// </summary>
        /// <param name="ar"></param>
        void OnWrite(IAsyncResult ar)
        {
            PackageOne pkg = (PackageOne)ar.AsyncState;
            pkg.StreamNet.EndWrite(ar);
        }
		
		/*
        /// <summary>
        /// 回调事件加入链表
        /// </summary>
        public void tick()
        {
            lock (_recv_queue)
            {
                if (_recv_queue.Count != 0)
                {
                    QueueNode node = _recv_queue.Dequeue();
                    _net_listeners[node.msg].callEvent(this, node.args);
                }
            }
        }*/  
		
		/// <summary>
        /// 网络事件索引器.
        /// </summary>
        /// <param name="msg">消息类型</param>
        /// <returns>网络监听器</returns>
        public ListenerNet this[ushort msg]
        {
            get 
            {
                return null;
            }
        }
        
		/// <summary>
        /// 向服务器发送请求.
        /// </summary>
        /// <param name="pro">协议</param>
        /// <param name="isNotify">是否为通知请求</param>
        /// <returns>请求是否成功</returns>
        public bool request(IProtocal pro, bool isNotify)
        {
            if (!_net_stream.CanWrite)
			{
                return false;
			}
            try
            {
                byte[] bytes = pro.getBuffer();
                if (null == bytes)
                {
                    Logger.Error("pro body is null!" +pro.Message);
                    return false;
                }
                byte[] realBytes = bytes;
                byte isCompress = 0;
                if (bytes.Length > 16000)
                {
                    isCompress = 1;
                    realBytes = ZlibStream.CompressBuffer(bytes);
                }
                
                HeaderBase header = null;
                uint length = (uint)IPAddress.HostToNetworkOrder((realBytes.Length + HeaderBase.getHdrLen()));
                int Message = (int)pro.Message;
                ushort PO = (ushort)IPAddress.HostToNetworkOrder((short)Message);
                byte EncrypKey = Encryption.rand_key_index();
                Encryption.encode_decode((uint)realBytes.Length, realBytes, EncrypKey);
                if (isNotify)
                {
                    header = new HeaderBase(PackageOne.VERSION, length, 1, EncrypKey, isCompress, PO);
                }
                else
                {
                    header = new HeaderBase(PackageOne.VERSION, length, 1, EncrypKey, isCompress, PO);
                }
                PackageOne pkg = new PackageOne(header,_net_stream);
                MemoryStream ms_buff = new MemoryStream();
                BinaryWriter bw = new BinaryWriter(ms_buff);
                bw.Write(header.getBuffer(), 0, header.getBuffer().Length);
                bw.Write(realBytes, 0, realBytes.Length);
                ///写入包头.
                byte[] w_buff = ms_buff.ToArray();
				try
				{
                	_net_stream.BeginWrite(w_buff, 0, w_buff.Length, new AsyncCallback(OnWrite), pkg);
					
				}
				catch(Exception ex)
				{
//					if(Application.isPlaying)
//					{
						Logger.Error("socket begin write  ex = " + ex.Message);
//					}
				}
                
            }
            catch (Exception ex)
            {
				_is_relogin = true;
                Logger.Error(ex.Message);
                return true;
            }
            return true;
        }
    }

    //addFactoryMethod(LOGIN.LOGIN_REQ,LOGIN.CreateLOGIN);

    
}
