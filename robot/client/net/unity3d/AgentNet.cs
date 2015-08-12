using System;
using System.Collections.Generic;
using utils;
using robot.client.common;
using System.Diagnostics;

namespace net.unity3d
{
    public class AgentNet
    {
		/// <summary>
		/// 客户端连接服务器配置文件路径
		/// </summary>
		public static readonly string accountServerId = "account_server";

        public workerNet lNetWorker = new workerNet();

		public AgentNet()
        {
			ARequestOverTime = DateTime.Now; //现在时间
			ARequestTime = DateTime.Now; //现在时间
        }
		
		public void initClientNoetNew(List<NoteServer> ListNote)
		{
			ManagerNet mgr = ManagerNet.getInstance();
			mgr.init(ListNote);
		}
		
		public void initAccountNote(List<NoteServer> ListNote)
		{
			ManagerNet mgr = ManagerNet.getInstance();
			mgr.initAccountNote(ListNote);
		}

		public void connectRealmServer()
        {
			try
			{
                _XmanLogic = new UConnection();
                _XmanLogic.agentNet = this;
	            _XmanLogic.EventConnected += OnXmanLogicConnected;

                Logger.Info( "logic server: " + m_LogicNote._serverIp + ":" + m_LogicNote._serverProt );
	            _XmanLogic.open(m_LogicNote);
			}
			catch(Exception ex)
			{
                Logger.Error( "create realm conn exp = " + ex );
				
				NodeQueue node = new NodeQueue();
				RM2C_ON_REALM_SERVER_CLOSE pro = new RM2C_ON_REALM_SERVER_CLOSE();
				pro.uiReason = 1;
				ArgsNetEvent largs = new ArgsNetEvent(pro);
	            NodeQueue qn = new NodeQueue();
	            qn.msg = (ushort)RM2C_ON_REALM_SERVER_CLOSE.OPCODE;
	            qn.args = largs;
	            this.lNetWorker.AddQueue(qn);
				node = this.lNetWorker.tick();
	            if (null != node)
	            {
	                callEvent(node.msg, node.args);
	            }
				close();
			}
        }
		
		public void OnXmanLogicClosed(object sender, net.ArgsEvent args)
		{
			RM2C_ON_REALM_SERVER_CLOSE pro = new RM2C_ON_REALM_SERVER_CLOSE();
			pro.uiReason = 1;
			ArgsNetEvent largs = new ArgsNetEvent(pro);
            NodeQueue qn = new NodeQueue();
            qn.msg = (ushort)RM2C_ON_REALM_SERVER_CLOSE.OPCODE;
            qn.args = largs;
            this.lNetWorker.AddQueue(qn);
		}
		
		public void OnXmanLogicConnected(object sender, net.ArgsEvent args)
        {
            if( !( bool ) args.Data )
			{
				return;
			}
            UConnection conn = (UConnection)sender;
            Logger.Info( "connected : {" + conn.RemoteIP + "}:{" + conn.RemotePort + "}" );

            //Register Protocol Factory
            robot.client.Protocols.logicRegisterProtocol( conn.Session );

            RM2C_ON_REALM_SERVER_CONN pro = new RM2C_ON_REALM_SERVER_CONN();
            pro.uiReason = 1;
            ArgsNetEvent largs = new ArgsNetEvent(pro);
            NodeQueue qn = new NodeQueue();
            qn.msg = (ushort)RM2C_ON_REALM_SERVER_CONN.OPCODE;
            qn.args = largs;
            this.lNetWorker.AddQueue(qn);

			sendRMLogin();
        }
		
		public void sendRMLogin()
		{
			C2RM_LOGIN LogicPro = new C2RM_LOGIN();
			LogicPro.setChannelIdStr(_channelId);
			LogicPro.setAccountIdStr(_accountId);
			LogicPro.sAccountC2AC.setChannelIdStr(_accountId);
			LogicPro.sAccountC2AC.setAccountIdStr(_accountId);
			LogicPro.sAccountC2AC.setAccount(_account);
			LogicPro.sAccountC2AC.setPassword(_password);
			LogicPro.sAccountC2AC.setDeviceInfo(_macId);
			LogicPro.sAccountC2AC.setSession(_webSession);
			LogicPro.sAccountC2AC.serverType = _serverType;
			send(LogicPro);
		}

        public void OnLoginSerConnected(object sender, net.ArgsEvent args)
        {
            if( !( bool ) args.Data )
			{
				return;
			}
			
            UConnection conn = (UConnection)sender;
            Logger.Info( "connected : {" + conn.RemoteIP + "}:{" + conn.RemotePort + "}" );
            robot.client.Protocols.loginSerRegisterProtocol(conn.Session);

            LS2C_ON_LOGIN_SERVER_CONN pro = new LS2C_ON_LOGIN_SERVER_CONN();
            pro.uiReason = 1;
            ArgsNetEvent largs = new ArgsNetEvent(pro);
            NodeQueue qn = new NodeQueue();
            qn.msg = (ushort)LS2C_ON_LOGIN_SERVER_CONN.OPCODE;
            qn.args = largs;
            this.lNetWorker.AddQueue(qn);
			
			C2LS_LOGIN_REQUEST loginPro = new C2LS_LOGIN_REQUEST();
			loginPro.setChannelIdStr(_channelId);
			loginPro.setAccountIdStr(_accountId);
			send(loginPro);
        }
		
		public void OnAccountSerConnected(object sender, net.ArgsEvent args)
        {
			bool lbTemp = (bool)args.Data;
			if(false == lbTemp)
			{
				return;
			}
			
            UConnection conn = (UConnection)sender;
            Logger.Info( "connected : {" + conn.RemoteIP + "}:{" + conn.RemotePort + "}");
            robot.client.Protocols.accountSerRegisterProtocol( conn.Session );

            /**
            RM2C_ON_ACCOUNT_SERVER_CONN pro = new RM2C_ON_ACCOUNT_SERVER_CONN();
            pro.uiReason = 1;
            ArgsNetEvent largs = new ArgsNetEvent(pro);
            NodeQueue qn = new NodeQueue();
            qn.msg = (ushort)RM2C_ON_ACCOUNT_SERVER_CONN.OPCODE;
            qn.args = largs;
            workerNet lNetWorker = workerNet.getInstance();
            lNetWorker.AddQueue(qn);
             **/

            sendAccountLogin();
        }
		
		public void setMacId(String macId)
		{
			_macId = macId;
		}
		
		public void sendAccountLogin()
		{
			sendAccount(0, _account, _password, _macId, _serverType, _webSession, _channelId, _accountId);
		}
		
		
		
		public void getMacId()
		{
			C2AC_MAC_ID sender = new C2AC_MAC_ID();
			sender.uiListen = 0;
			send(sender);
		}
		
		public void setLoginInfo(string account, string password, string macId, byte serverType, string webSession, string channelId, string accountId)
		{
			_account = account;
			_password = password;
			_macId = macId;
			_serverType = serverType;
			_webSession = webSession;
			_channelId = channelId;
			_accountId = accountId;			
		}
		
		public void sendAccount(UInt32 puiListen, string account, string password, string macId, byte serverType, string webSession, string channelId, string accountId)
		{
			C2AC_ACCOUNT_INFO accountPro = new C2AC_ACCOUNT_INFO();
			accountPro.uiListen = puiListen;
			accountPro.sAccountC2Ac.setAccount(account);
			accountPro.sAccountC2Ac.setPassword(password);
			accountPro.sAccountC2Ac.setDeviceInfo(macId);
			accountPro.sAccountC2Ac.serverType = serverType;
			accountPro.sAccountC2Ac.setSession(webSession);
			accountPro.sAccountC2Ac.setChannelIdStr(channelId);
			accountPro.sAccountC2Ac.setAccountIdStr(accountId);

			//Logger.Info(" send account real!! account = " + accountPro.sAccountC2Ac.getAccount() + " session_id = " + accountPro.sAccountC2Ac.getSessionId()
			//          + " account id = " + accountId);
			send(accountPro);
		}
		
		public void OnLogicSerReConnected(object sender, net.ArgsEvent args)
		{
			bool lbTemp = (bool)args.Data;
			if(false == lbTemp)
			{
				return;
			}
			
			RM2C_RELOGIN pro = new RM2C_RELOGIN();
			pro.iResult = 1;
			
			ArgsNetEvent largs = new ArgsNetEvent(pro);
            NodeQueue qn = new NodeQueue();
            qn.msg = (ushort)RM2C_RELOGIN.OPCODE;
            qn.args = largs;
            this.lNetWorker.AddQueue(qn);
		}

        public bool isListenPro(ushort msg)
        {
            return _event_listeners.ContainsKey( msg );
        }
		
		public void connectAccountServer(string ip, short port, int timeout)
		{
            _AccountSer = new UConnection();
            _AccountSer.agentNet = this;

			_AccountSer.EventConnected += this.OnAccountSerConnected;
			_AccountSer.open(ip, port, timeout);
		}

        public void connectLoginServer( string channelId, string accountId)
		{
			_channelId = channelId;
			_accountId = accountId;

             _LoginSer = new UConnection();
             _LoginSer.agentNet = this;

	        _LoginSer.EventConnected += OnLoginSerConnected;
            _LoginSer.open( robot.Program.LOGIN_IP, robot.Program.LOGIN_PORT, robot.Program.LOGIN_TIMEOUT);
		}

        public void callEvent(ushort msg, ArgsNetEvent args)
        {
            if (_event_listeners.ContainsKey(msg))
            {
                _event_listeners[msg].callEvent(args);
            }
        }

        public void addListenEvent(ushort msg, EHandleNetEvent eventHandle)
        {
            if (!_event_listeners.ContainsKey(msg))
            {
                _event_listeners.Add(msg, new EListenerNet(msg));
            }
            _event_listeners[msg].ListenerEvent += eventHandle;
        }

        /// <summary>
        /// 回调事件加入链表
        /// </summary>
        public void tick()
        {
            Debug.Assert(lNetWorker != null);

            NodeQueue node = lNetWorker.tick();
			
            if (null != node)
            {
                Logger.Info( "recv: " + node.msg );
                callEvent(node.msg, node.args);
            }
        }
		
		public bool send(IProtocal pro)
        {

            if (pro.Message >= 200 && pro.Message <= 1999)
            {
				if(pro.Message == 322)
				{
					//set_is_ping_back(false);
				}
				ARequestTime = DateTime.Now; //现在时间
				
				//Logger.Info("ping request time = " + ARequestTime);
				if(_XmanLogic != null)
				{
					return _XmanLogic.send(pro, false);
				}
				else
				{
					Logger.Error("_XmanLogic is null!");
				}
            }
            else if (pro.Message >= 5000 && pro.Message < 6000)
            {
				if(_LoginSer != null)
				{
					return _LoginSer.send(pro, false);
				}
				else
				{
                    Logger.Error( "_LoginSer is null!" );
				}
            }
			else if (pro.Message >= 11000 && pro.Message <= 12000)
			{
				if(_AccountSer != null)
				{
					return _AccountSer.send(pro, false);
				}
				else
				{
                    Logger.Error( "_AccountSer is null!" );
				}
			}
			return false;
        }

        public UConnection getLoginSer()
        {
            return _LoginSer;
        }

        public UConnection getXmanLogic()
        {
            return _XmanLogic;
        }
		
		public void logic_reopen(string id_channel, string id_account, string id_mac) 
		{
			_XmanLogic.set_is_relogin(false);
			//_XmanLogic.EventReConnected += OnLogicSerReConnected;
			_XmanLogic.reopen(id_channel, id_account, id_mac);
		}
		
		public void close()
		{
			if(_AccountSer != null)
			{
				_AccountSer.close();
			}
			if(_LoginSer != null)
			{
				_LoginSer.close();
			}
			if(_XmanLogic != null)
			{
				_XmanLogic.close();
			}
			_AccountSer = null;
			_LoginSer = null;
			_XmanLogic = null;
		}
		
		public bool isLogicConnected()
		{
			return _XmanLogic.isConnected();
		}
		
		public void initLogic(string ip, short port, int timeout)
		{
			m_LogicNote._serverIp = ip;
			m_LogicNote._serverProt = port;
			m_LogicNote._timeOut = timeout;
		}
		
		private System.DateTime ARequestTime =new System.DateTime();
		private System.DateTime ARequestOverTime =new System.DateTime();
		
		private NoteServer m_LogicNote;

		private UConnection _AccountSer = null;
        private UConnection _LoginSer = null;
        private UConnection _XmanLogic = null;
		//private UConnection _GuestSer = null;
		
		private string _account;
		private string _password;
		private string _macId;
		private byte _serverType;
		private string _webSession;
		private string _channelId;
		private string _accountId;

        private workerNet _netWorker = new workerNet();

        private Dictionary<ushort, EListenerNet> _event_listeners = new Dictionary<ushort, EListenerNet>();

        /// account返回
        public void recvAccountServer( ArgsEvent args )
        {
            AC2C_ACCOUNT_INFO recv = args.getData<AC2C_ACCOUNT_INFO>();

            this.connectLoginServer( recv.sAccountAC2C.getChannelIdStr(), recv.sAccountAC2C.getAccountIdStr() ); //getRelamInfo("2002");

            if( recv.iResult == 1 )
            {
                //ManagerServer.getInstance().lastLoginServerIndex = ( int ) recv.sAccountAC2C.uiServer[ 0 ];
            }
            Logger.Info( "RECV SERVER ACCOUNT = " + recv.sAccountAC2C.getAccountIdStr() );
        }

        /// login返回
        public void recvLoginServer( ArgsEvent args )
        {
            LS2C_LOGIN_RESPONSE recv = args.getData<LS2C_LOGIN_RESPONSE>();

            Logger.Info( "LS2C_LOGIN_RESPONSE >> " + recv.iResult.ToString() );
            if( recv.iResult == 1 )
            {
                Logger.Info( "Login Authentication Success" );
                Logger.Info( "realm server info: " + recv.getIp() + ":" + recv.port );

                this.initLogic( recv.getIp(), ( short ) recv.port, ( int ) recv.timeOut );
                this.connectRealmServer();
            }
            else
            {
                Logger.Info( " recvLoginServer error, iresult = " + recv.iResult );
            }
        }

        ///协议返回 realm返回
        public void recvLogin( ArgsEvent args )
        {
            RM2C_LOGIN recv = args.getData<RM2C_LOGIN>();

            Logger.Info( "connect Realm Result: " + recv.iResult );

            if( recv.iResult == ( int ) EM_CLIENT_ERRORCODE.EE_M2C_NEED_CREATE_ROLE )
            {
                Logger.Info( "需要创建角色" );
                this.sendCreatRole( 61, null );

            }
            else if( recv.iResult == ( int ) EM_CLIENT_ERRORCODE.EE_ACCOUNT_NOT_EXIST )
            {
                Logger.Info( "Realm msg: 帐号不存在" );
            }
        }


        ///用户基本信息
        public void recvMaster( ArgsEvent args )
        {
            RM2C_MASTER_BASE_INFO recv = args.getData<RM2C_MASTER_BASE_INFO>();
            Logger.Info( "RECV:RM2C_MASTER_BASE_INFO >> " + recv.SPlayerInfo.sPlayerBaseInfo.uiIdMaster );

            SPlayerInfo playerInfo = recv.SPlayerInfo;

            Logger.Info( "玩家基本信息： " );
            Logger.Info( "=========================================" );
            //Logger.Info( "player IdLead: " + playerInfo. sPlayerBaseInfo.uiIdLead );
            Logger.Info( "player name: " + playerInfo.sPlayerBaseInfo.getMasterName() );
            Logger.Info( "player idServer: " + playerInfo.sPlayerBaseInfo.uiIdMaster );
            Logger.Info( "player vip: " + playerInfo.sPlayerBaseInfo.uiVip );
            Logger.Info( "player exp: " + playerInfo.sPlayerBaseInfo.luiExp );
            Logger.Info( "player power: " + playerInfo.sLeadPowerInfo.usPower );
            Logger.Info( "=========================================" );

            //TODO ...

            //发送更名请求
            sendChangeName( "Robot_" + this._accountId, ( UtilListenerEvent e) =>
            {
                Console.WriteLine("Rename revold");
            } );
        }

        /// 创建角色
        public void sendCreatRole( int roleCsvId, FunctionListenerEvent sListener )
        {
            C2RM_CREAT_ROLE sender = new C2RM_CREAT_ROLE();
            sender.uiPetCsvId = ( uint ) roleCsvId;
            Logger.Info( "创建角色 id: " + roleCsvId);
            this.send( sender );
        }

        /// 改名字
        public void sendChangeName( string name, FunctionListenerEvent listener )
        {
            Logger.Info( "C2RM_ROLE_NAME >> " + "改名字 ->" + name);

            C2RM_ROLE_NAME sender = new C2RM_ROLE_NAME();
            sender.setName( name );
            sender.uiListen = Dispatcher.addListener(listener, null);   
            this.send( sender );
        }

        // 改名字回应
        public void recvChangeName( ArgsEvent args )
        {
            RM2C_ROLE_NAME recv = args.getData<RM2C_ROLE_NAME>();
            if( recv.iResult == 1 )
            {
                Logger.Info( "aid: " + this._accountId + "改名成功：" + recv.getName() );
            }
            else if( recv.iResult == (int)EM_CLIENT_ERRORCODE.EE_M2C_ROLE_RENAME_ERROR)
            {
                Logger.Error( "aid: " + this._accountId + "改名失败: " + recv.iResult + " 这个人已经起过名字了" );
            }
            else
            {
                Logger.Error( "aid: " + this._accountId + "改名失败: " + recv.iResult );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }
    }
	
}
