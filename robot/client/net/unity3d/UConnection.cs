using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using utils;
using net;

namespace net.unity3d
{
    static class NET_CONN_ERR
    {
        public static readonly uint CONN_ERR = 10100;
        /// <summary>
        /// 重复连接.
        /// </summary>
        public static readonly uint ERR_REPEAT_CONN = CONN_ERR + 1;
    }

    public class UConnection
    {
        public UConnection()
        {
            _session = null;
            _tcp_client = new TcpClient();
        }
		
		private USession _session;
        private string _remote_ip;
        private short _remote_port;
		private NoteServer _note;
		private string _channel;
		private string _account;
		private string _password = null;
		private string _macid;
		private string _accountStr;
		
        private TcpClient _tcp_client;

        public AgentNet agentNet;

        private ManualResetEvent timeoutObject = new ManualResetEvent( true );

        private void _OnPreClose(ArgsEvent args)
        {
            HandleNetEvent handle = EventPreClose;
            if (handle != null)
            {
                handle(this, args);
            }
        }

        private void _OnTick(ArgsEvent args)
        {
            HandleNetEvent handle = EventTick;
            if (handle != null)
            {
                handle(this, args);
            }
        }
		
		public void set_account(string pluiChannel, string pluiAccount)
		{
			_channel = pluiChannel;
			_account = pluiAccount;
		}

		public void set_macid(string psMacId)
		{
			_macid = psMacId;
		}
		
		public bool is_error()
		{
			if(null == _session)
			{
				return false;
			}
			else
			{
				return _session.is_error();
			}
		}
		
		public bool is_relogin()
		{
			if(null == _session)
			{
				return false;
			}
			else
			{
				return _session.is_relogin();
			}
		}
		
		public void set_is_relogin(bool pbIsReLogin)
		{
			if(null != _session)
			{
				_session.set_is_relogin(pbIsReLogin);
			}
		}
		
		private void _OnReConnected(ArgsEvent args)
		{
			HandleNetEvent handle = EventReConnected;
            if (handle != null)
            {
                handle(this, args);
            }
		}
		
		private void _OnConnected(ArgsEvent args)
        {
            HandleNetEvent handle = EventConnected;
            if (handle != null)
            {
                handle(this, args);
            }
        }

        private void _OnPreConnect(ArgsEvent args)
        {
            HandleNetEvent handle = EventPreConnect;
            if (handle != null)
            {
                handle(this, args);
            }
        }

        /// <summary>
        /// 断开连接时触发的事件.
        /// </summary>
        /// <param name="ar">事件参数.</param>
        void OnDisConnection(IAsyncResult ar)
        {
            TcpClient tcp = (TcpClient)ar.AsyncState;
            try
            {
                ArgsEvent args;
                if (!tcp.Connected)
                {
                    //断开连接成功.
                    args = new ArgsEvent(true);
                    _session = null;
                }
                else
                {
                    //断开连接失败.
                    args = new ArgsEvent(false);
                    
                }

                _OnClosed(args);
                tcp.Client.EndDisconnect(ar);
				tcp.Client.Close();
				
            }
            catch (SocketException ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private void _OnClosed(ArgsEvent args)
        {
            HandleNetEvent handle = EventClosed;
            if (handle != null)
            {
                handle(this, args);
            }
        }
		
		/// <summary>
        /// Tcp连接事件.
        /// </summary>
        /// <param name="ar">异步通信结果</param>
        void OnReConnected(IAsyncResult ar)
        {
//			Debug.Log("!!!!@@@@@----0");
			if(null == _tcp_client)
			{
				Logger.Error("conned error!!");
				return;
			}
			if(!_tcp_client.Connected)
			{
				Logger.Error("conned error!!");
				reopen(_channel, _account, _macid);
				return;
			}
				
            TcpClient tcp = null;//new TcpClient ();
			try
			{
				tcp = (TcpClient)ar.AsyncState;
				//	tcp.GetStream();
			}
            catch (SocketException ex)
            {
				Logger.Error(ex.Message);
                //Debug.Log("error ======  " + ex.ToString());
            }
            try
            {
                ArgsEvent args;
				//Debug.Log("error info " + tcp.Available)
                if (tcp.Connected)
                {
                    //连接成功.
                    args = new ArgsEvent(true);
//					Debug.Log("!!!!@@@@@----1");
                    
					_session.set_tcp_client(tcp);
					_session.set_net_stream(tcp.GetStream());
					_session.set_conn(this);
					C2RM_LOGIN sender = new C2RM_LOGIN();
					sender.setChannelIdStr(_channel);
					sender.setAccountIdStr(_account);
					sender.sAccountC2AC.setChannelIdStr(_channel);
					sender.sAccountC2AC.setAccountIdStr(_account);
					sender.sAccountC2AC.setPassword(_password);
					sender.sAccountC2AC.setDeviceInfo(_macid);
					sender.sAccountC2AC.serverType = 1;
					sender.cIsReLogin = 1;
					send(sender);
                }
                else
                {
//					Debug.Log("!!!!@@@@@----2");
					Logger.Warning("conn failed!!");
                    //连接失败.
                    args = new ArgsEvent(false);
                    _session = null;
                    
                }
				
				tcp.EndConnect(ar);
                _OnReConnected(args);
            }
            catch (SocketException ex)
            {
                Logger.Error(ex.Message);
            }
        }
		
		/// <summary>
		/// 断网重连后发生的事件.
		/// </summary>
		public event HandleNetEvent EventReConnected;
		
		/// <summary>
        /// 网络连接后，发生的事件.
        /// </summary>
        public event HandleNetEvent EventConnected;

        /// <summary>
        /// 网络关闭后，发生的事件.
        /// </summary>
        public event HandleNetEvent EventClosed;

        /// <summary>
        /// 网络关闭后，发生的事件.
        /// </summary>
        public event HandleNetEvent EventTick;

        /// <summary>
        /// 网络连接前，发生的事件
        /// </summary>
        public event HandleNetEvent EventPreConnect;

        /// <summary>
        /// 网络关闭前，发生的事件
        /// </summary>
        public event HandleNetEvent EventPreClose;

        public bool send(IProtocal pro, bool isNotify)
        {
            if (null == _session)
            {
                return false;
            }
            return Session.request(pro, isNotify);
        }

        public void send(IProtocal pro)
        {
            send(pro, false);
        }

        public bool IsOpen
        {
            get { return _tcp_client.Connected; }
        }

        public string RemoteIP
        {
            get { return _remote_ip; }
        }

        public short RemotePort
        {
            get { return _remote_port; }
        }


        public USession Session
        {
            get { return _session; }
        }
		
		public bool isConnected()
		{
			//return _tcp_client.Client.Connected;
			if(null == _session)
			{
				return false;
			}
			else
			{
				if(null == _tcp_client)
				{
					return false;
				}
				else
				{
					return _tcp_client.Connected;
				}
			}
		}

        

        /// <summary>
        /// Tcp连接事件.
        /// </summary>
        /// <param name="ar">异步通信结果</param>
        void OnConnected(IAsyncResult ar)
        {
            this.timeoutObject.Set();

			if(null == _tcp_client)
			{
				Logger.Error("conned error!!");
				return;
			}
			
			TcpClient tcp = null;
			try
			{
				tcp = (TcpClient)ar.AsyncState;

			}
			catch (SocketException ex)
            {
				Logger.Error(ex.Message);
            }
			
			if(!_tcp_client.Connected)
			{
				try
				{
					tcp.EndConnect(ar);
				}
				catch//(Exception e)
				{
					//dosomething about e
					//需要重试的话
					tcp.BeginConnect(_remote_ip,_remote_port,OnConnected,tcp);
				}
				return;
			}

            try
            {
                ArgsEvent args;

                if (tcp.Connected)
                {
                    //连接成功.
                    args = new ArgsEvent(true);

                    _session = new USession(tcp.GetStream());
                    _session.agentNet = this.agentNet;

                }
                else
                {

					Logger.Warning("conn failed!!");
                    //连接失败.
                    args = new ArgsEvent(false);
                    _session = null;
                    
                }
				
				tcp.EndConnect(ar);
                _OnConnected(args);
            }
            catch (SocketException ex)
            {
                Logger.Error(ex.Message);
            }
        }
		
		        

        
		public void begin_close()
        {
            try
            {
				Logger.Info("call ~BeginDisconnect()");
                ArgsEvent args = new ArgsEvent(_tcp_client);
                _OnPreClose(args);
                //_tcp_client.Client.BeginDisconnect(true, new AsyncCallback(OnDisConnection), _tcp_client);
				//#if UNITY_EDITOR
				
				
				//_tcp_client.Client.Close();
				//#endif

            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }
		
        /// <summary>
        /// 添加注册协议
        /// </summary>
        public void addMethodToFactory(ushort msg, CreateProtocal fac_mtd)
        {
            if (null == _session)
            {
                return;
            }
            _session.addFactoryMethod(msg, fac_mtd);
        }
		

        /// <summary>
        /// 添加监听协议
        /// </summary>
        public void addListenEvent(ushort msg, HandleNetEvent fac_mtd)
        {
            if (null == _session)
            {
                return;
            }
            _session[msg].EventListener += fac_mtd;

        }
        

        /// <summary>
        /// 关闭当前连接.
        /// </summary>
        public void close()
        {
            try
            {
				//if(UtilLog.isBulidLog)UtilLog.Log("call ~BeginDisconnect()");
				Logger.Info("call ~BeginDisconnect()");
                ArgsEvent args = new ArgsEvent(_tcp_client);
                _OnPreClose(args);
                //_tcp_client.Client.BeginDisconnect(true, new AsyncCallback(OnDisConnection), _tcp_client);
				//#if UNITY_EDITOR
				
				_tcp_client.Client.Shutdown(SocketShutdown.Both);
				_tcp_client.Client.Close();
				//_tcp_client.Client.Close();
				//#endif

            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }


		public void open(string remote_ip, short remote_port, int timeout)
        {
            if (_tcp_client.Connected)
            {
                throw new NetException("this connection already connected!", NET_CONN_ERR.ERR_REPEAT_CONN);
            }

            try
            {
                _remote_ip = remote_ip;
                _remote_port = remote_port;
				
				_note._serverIp = remote_ip;
				_note._serverProt = remote_port;
				_note._timeOut = timeout;

                //设置连接超时参数.
                _tcp_client.ReceiveTimeout = timeout;
                _tcp_client.SendTimeout = timeout;

                ArgsEvent args = new ArgsEvent(_tcp_client);
                _OnPreConnect(args);

                Logger.Info( "BeginConnect  " + remote_ip + ":" + remote_port );

                this.timeoutObject.Reset();
  
                //异步连接.
                _tcp_client.BeginConnect(remote_ip, remote_port, new AsyncCallback(OnConnected), _tcp_client);

                /**
                if(!this.timeoutObject.WaitOne(1000, false ) )    //timeout 5 sec
                {
                    throw new Exception("连接超时  " + remote_ip + ":" + remote_port);
                }  
				**/
            }
            catch (Exception e)
            {
				//UIScreenLog.LogError(e.Message);
                Logger.Error("tcp conn open " + e.Message);
            }
			//Debug.Log("conn succeed");
        }

        public void open(NoteServer sNote)
        {
            open(sNote._serverIp, sNote._serverProt, sNote._timeOut);
        }
		
		public void reopen(string id_channel, string id_account, string id_mac )
		{
			
			//_tcp_client.Dispose();
			_tcp_client.Close();
			_tcp_client = new TcpClient();
			
			if (_tcp_client.Connected)
            {
				
                throw new NetException("this connection already connected!", NET_CONN_ERR.ERR_REPEAT_CONN);
            }

			
			
            try
            {
                //设置连接超时参数.
                _tcp_client.ReceiveTimeout = _note._timeOut;
                _tcp_client.SendTimeout = _note._timeOut;

                ArgsEvent args = new ArgsEvent(_tcp_client);
                _OnPreConnect(args);
				//IPAddress;
                //异步连接.
                _macid = id_mac;
				_channel = id_channel;
				_account = id_account;
				_password = "";
                _tcp_client.BeginConnect(_note._serverIp, _note._serverProt, new AsyncCallback(OnReConnected), _tcp_client);
				//_tcp_client.Connect(_remote_ip, _remote_port);
				
            }
            catch (Exception e)
            {
				//UIScreenLog.LogError(e.Message);
                Logger.Error("tcp conn reopen " + e.Message);
            }
		}

        public void tick()
        {
            try
            {
                ArgsEvent args = new ArgsEvent(_tcp_client);
                _OnTick(args);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            	
            }
        }

    }

    class UConnNetFactory : FactoryNet
    {
        public UConnNetFactory(string IID)
        {
            _ID = IID;
        }

        public T createObject<T>()
        {
            return (T)(object)(new UConnection());
        }
        public string Name
        {
            get { return "UConnection Factory"; }
        }
        public string IID
        {
            get { return _ID; }
        }
        public string Discription
        {
            get { return "unity3d client connection."; } 
        }

        private string _ID;

    }
}
