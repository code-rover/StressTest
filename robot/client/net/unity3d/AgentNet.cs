using System;
using System.Collections.Generic;
using utils;
using robot.client.common;
using System.Diagnostics;

namespace net.unity3d
{
    public class AgentNet
    {
		public static readonly string accountServerId = "account_server";

        public workerNet lNetWorker = new workerNet();

        private DataMode dataMode = new DataMode();

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
                Logger.Info("====> " + this._account + " recv: " + node.msg );
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


            /// 创建主角
            //ManagerServer.getInstance().isBinded = recv.SPlayerInfo.sPlayerBaseInfo.cIsBind == 0 ? false : true;
            if( null == this.dataMode.getPlayer( recv.SPlayerInfo.sPlayerBaseInfo.uiIdMaster ) )
                this.dataMode._serverPlayer.Add( recv.SPlayerInfo.sPlayerBaseInfo.uiIdMaster, new InfoPlayer(this.dataMode) );

            ///  设定主角的基本信息
            InfoPlayer player = this.dataMode.getPlayer( recv.SPlayerInfo.sPlayerBaseInfo.uiIdMaster );

            /// 0点清0时间戳WILL DONE
            player.timeTeampUpdate = ( double ) recv.SPlayerInfo.sPlayerBaseInfo.uiUpdateTime;
            //ManagerUpdate.lockPlayUpdate = false;

            //名字
            player.name = recv.SPlayerInfo.sPlayerBaseInfo.getMasterName();
            //serverid
            player.idServer = recv.SPlayerInfo.sPlayerBaseInfo.uiIdMaster;
            /// 声望
            player.honer = ( long ) recv.SPlayerInfo.SLeadPkInfo.uiPrestige;
            //经验
            player.exp = recv.SPlayerInfo.sPlayerBaseInfo.luiExp;
            //体力
            player.power = recv.SPlayerInfo.sLeadPowerInfo.usPower;
            //体力上限
            //		player.powerMax = recv.SPlayerInfo.sLeadPowerInfo.usPowerMax;
            //		player.setPowerMax();
            //领取好友赠送的体力的次数
            player.powerFriendCnt = ( int ) recv.SPlayerInfo.sLeadPowerInfo.sFriendPowerCnt;
            ///今天购买体力的次数
            player.powerBuyCnt = ( int ) recv.SPlayerInfo.sLeadPowerInfo.sPowerBuyCnt;
            //今天购买金币次数
            player.stoneBuyCnt = ( int ) recv.SPlayerInfo.sPlayerBaseInfo.sStoneTimes;

            player.maxHeroList = ( int ) recv.SPlayerInfo.sLeadBagInfo.usCntBag;
            player.maxEquipBagList = ( int ) recv.SPlayerInfo.sLeadBagInfo.usCntEquipBag;

            //DataMode.ddnSeverFlag.DDNFlag = recv.SPlayerInfo.sPlayerBaseInfo.uiGuideDDN;

            //游戏币
            player.money_game = ( long ) recv.SPlayerInfo.sPlayerBaseInfo.luiSMoney;
            //人民币
            player.money = ( long ) recv.SPlayerInfo.sPlayerBaseInfo.luiQMoney;
            ///正义徽章
            //player.infoPoint.badge = ( int ) recv.SPlayerInfo.sPlayerBaseInfo.uiBadge;
            //TODO ...

            /// 我的角色
            this.dataMode.myPlayer = player;

            if( player.name == null )
            {
                //发送更名请求
                sendChangeName( "Robot_" + this._accountId, ( UtilListenerEvent e ) =>
                {
                    //改名完成
                    sendWebEmail( null );
                    
                } );
            }
            else
            {
                //发送获取邮件请求
                sendWebEmail( null );        
            }

            if( this.dataMode.myPlayer.exp > 0 )  //经验>0, 认为奖励已经领取
            {
                sendFBUpdate(null);    
            }

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

            //C2RM_NAME_RAND sender = new C2RM_NAME_RAND();


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

        ///向服务器发送邮件信息请求
        public void sendWebEmail( FunctionListenerEvent listener )
        {
            dataMode._emailInfo.Clear();
            Logger.Info( "SEND:C2RM_WEB_EMAIL >> " + "发送邮件信息请求 >> " );

            C2RM_WEB_EMAIL sender = new C2RM_WEB_EMAIL();
            sender.uiListen = Dispatcher.addListener( listener, null);

            this.send( sender );
        }

        private List<int> opendMails = new List<int>();   //已经打开过的mails

        //response web email
        ///接受所有邮件信息
        public void recvWebEmail( ArgsEvent args )
        {
            RM2C_WEB_EMAIL recv = args.getData<RM2C_WEB_EMAIL>();

            //Logger.Info( "接收邮件回应 << " + recv.Message );

            for( int i = 0; i < recv.sWebEmail.Length; i++ )
            {
                if( recv.sWebEmail[ i ].uiIdServer == 237275 )
                {
                    Logger.Error( "---237275" );
                }

                if( null == dataMode.getEmailInfo( recv.sWebEmail[ i ].uiIdServer ) )
                {
                    dataMode._emailInfo.Add( recv.sWebEmail[ i ].uiIdServer, new EmailInfo() );    
                }
                
                EmailInfo reward = dataMode.getEmailInfo( recv.sWebEmail[ i ].uiIdServer );
                reward.serverId = recv.sWebEmail[ i ].uiIdServer;
                reward.isDes = recv.sWebEmail[ i ].isDes;
                reward.isLoc = recv.sWebEmail[ i ].isLoc;
                reward.isSpe = recv.sWebEmail[ i ].isSpe;
                reward.isOpen = recv.sWebEmail[ i ].isOpen;
                reward.csvId = ( int ) recv.sWebEmail[ i ].uiRoleId;
                reward.limiltLv = ( int ) recv.sWebEmail[ i ].usLimitLv;
                reward.outTime = ( int ) recv.sWebEmail[ i ].uiOutTime;
                reward.sendTime = ( int ) recv.sWebEmail[ i ].uiSendTime;
                reward.csvMailId = ( int ) recv.sWebEmail[ i ].uiIdWebEmail;
                reward.title = recv.sWebEmail[ i ].getTitle();
                reward.nameSend = recv.sWebEmail[ i ].getSendName();
                
                reward.Clear();
                for( int m = 0; m < recv.sWebEmail[ i ].vctSWebGoodBase.Length; m++ )
                {
                    if( recv.sWebEmail[ i ].vctSWebGoodBase[ m ].cTypeGoods != 0 )
                    {
                        RewardItems item = new RewardItems();
                        item.prop = new InfoProp();
                        item.prop.idCsv = ( int ) recv.sWebEmail[ i ].vctSWebGoodBase[ m ].uiIdCsvGoods;
                        item.prop.cnt = ( int ) recv.sWebEmail[ i ].vctSWebGoodBase[ m ].m_liCnt;
                        item.emailType = recv.sWebEmail[ i ].vctSWebGoodBase[ m ].cTypeGoods;

                        reward.addReward( item );
                    }
                }
                
            }

            Logger.Info( "收到邮件总数:" + dataMode._emailInfo.Count );

            int cnt = 0;
            foreach( var e in dataMode._emailInfo )
            {
                int csvMailId = e.Value.csvMailId;

                if( e.Value.isOpen == 0 )
                {
                    cnt++;
                }

                if( e.Value.isOpen == 0 && !opendMails.Contains( csvMailId ) )
                {
                    //开通全部副本
                    if(csvMailId == 78 )  
                    {
                        opendMails.Add(csvMailId );
                        sendOpenEmail( e.Key, ( UtilListenerEvent evt ) =>
                        {
                            Logger.Info( "打开全部副本 完成 " + evt );

                            sendFBUpdate( null );  //打开副本后，获取副本列表
                        } );
                    }

                    //领取很多钱
                    else if(csvMailId == 8)
                    {
                        opendMails.Add(csvMailId );
                        sendOpenEmail( e.Key, ( UtilListenerEvent evt ) =>
                        {
                            Logger.Info( "打开金钱奖励 完成 " + evt );

                        } );
                    }

                    //开通60级
                    else if(csvMailId == 137 )
                    {
                        opendMails.Add(csvMailId );
                        sendOpenEmail( e.Key, ( UtilListenerEvent evt ) =>
                        {
                            Logger.Info( "打开60级奖励 完成 " + evt );

                        } );       
                    }
                }
                
            }

            Logger.Info( "收到未读邮件数量:" + cnt );
        }

        // 打开邮件 - wen
        public void sendOpenEmail( ulong emailId, FunctionListenerEvent sListener )
        {
            Logger.Info( "打开邮件" );
            C2RM_WEB_EMAIL_OPEN sender = new C2RM_WEB_EMAIL_OPEN();
            sender.uiListen = Dispatcher.addListener( sListener, null);
            sender.uiWebEmailId = emailId;
            this.send( sender );
        }

        /// 打开邮件回调（获取物品）
        public void recvOpenEmail( ArgsEvent args )
        {
            RM2C_WEB_EMAIL_OPEN recv = args.getData<RM2C_WEB_EMAIL_OPEN>();
            Logger.Info( "打开邮件" + recv.iResult );
            
            if( recv.iResult == 1 )
            {
                EmailInfo ei = dataMode.getEmailInfo( recv.uiWebEmailId );
                if( ei != null )
                {
                    ei.isOpen = 1;
                }
                
                for( int i = 0; i < recv.vctSPiece.Length; i++ )
                {
                    if( recv.vctSPiece[ i ].luiIdPiece != 0 )
                        dataMode.infoHeroChip.setHeroChip( ( int ) recv.vctSPiece[ i ].uiCsvId, recv.vctSPiece[ i ].iCnt );
                }
                
                TypeCsvProp csvprop = null;
                for( int i = 0; i < recv.vctSEquip.Length; i++ )
                {
                    if( recv.vctSEquip[ i ].uiIdCsvEquipment > 0 )
                    {
                        csvprop = ManagerCsv.getProp( ( int ) recv.vctSEquip[ i ].uiIdCsvEquipment );
                        if( csvprop.isPropBeast() )
                        {
                            //DataMode.myPlayer.infoPropBeastList.changeProp( ( int ) recv.vctSEquip[ i ].uiIdCsvEquipment, recv.vctSEquip[ i ].iCnt );
                        }
                        else
                        {
                            //DataMode.myPlayer.infoPropList.changeProp( ( int ) recv.vctSEquip[ i ].uiIdCsvEquipment, recv.vctSEquip[ i ].iCnt );
                        }
                    }
                }
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );

        }

        //除了物品奖励的其他奖励的发放
        public void recvSignReward( ArgsEvent args )
        {
            RM2C_REWARD_MONEY recv = args.getData<RM2C_REWARD_MONEY>();

            Logger.Info( "RM2C_REWARD_MONEY" );

            
            this.dataMode.myPlayer.money_game += ( long ) recv.sRewardMoney.uiSMoney;
            this.dataMode.myPlayer.money += ( long ) recv.sRewardMoney.uiQMoney;
            //this.dataMode.myPlayer.infoPK.score += ( int ) recv.sRewardMoney.uiScoreFight;
            //this.dataMode.myPlayer.infoPoint.moneyTower += ( int ) recv.sRewardMoney.uiTiowerMoney;
            //this.dataMode.myPlayer.infoPoint.badge += ( int ) recv.sRewardMoney.uiBadge;
            this.dataMode.myPlayer.power += recv.sRewardMoney.uiPower;
            this.dataMode.myPlayer.friendShip += ( int ) recv.sRewardMoney.uiFriendShip;
            this.dataMode.myPlayer.honer += ( long ) recv.sRewardMoney.uiPrestige;
            this.dataMode.myPlayer.exp += ( ulong ) recv.sRewardMoney.uiExp;
            //this.dataMode.myPlayer.infoPoint.moneyTBC += ( long ) recv.sRewardMoney.uiMot;
            
        }


        ///获取副本信息
        //request
        public void sendFBUpdate( FunctionListenerEvent sListener )
        {
            Logger.Info( "SEND:C2RM_FB" );
            C2RM_FB sender = new C2RM_FB();
            sender.uiListen = Dispatcher.addListener( sListener, null);
            this.send( sender );
        }

        //fb sweep task queue
        private Queue<Action> sweep_queue = new Queue<Action>();

        //response 
        public void recvFBInfo( ArgsEvent args )
        {
            RM2C_FB recv = args.getData<RM2C_FB>();
            Logger.Info( "RECV:RM2C_FB >> " + recv.iResult );
            if( recv.iResult == 1 )
            {
                InfoPlayer player = this.dataMode.getPlayer( recv.uiMasterId );

                this.dataMode._serverFB.Clear();

                if(player == null) {
                    Debug.Assert(false, "player is null");
                    Logger.Error("player is null  " + recv.uiMasterId);
                    return;
                }
                player.clearAllFB();

                // add end
                for( int index = 0; index < recv.vctSFBInfo.Length; index++ )
                {
                    if( recv.vctSFBInfo[ index ].luiIdFB == 0 )
                        continue;

                    //changed by ssy diff type
                    if( null == this.dataMode.getFB( recv.vctSFBInfo[ index ].luiIdFB ) )
                        this.dataMode._serverFB.Add( recv.vctSFBInfo[ index ].luiIdFB, new InfoFB( recv.vctSFBInfo[ index ] ) );

                    player.addFB( ( int ) recv.vctSFBInfo[ index ].uiIdCsvFB, recv.vctSFBInfo[ index ].luiIdFB );
                    // changed end

                }

                Debug.Assert( recv.vctSFBInfo.Length > 0, "副本列表为空" );
                Logger.Error( this._account + "  副本列表为空！");

                this.sweep_queue.Clear();

                //fb sweep one by one
                foreach( var item in this.dataMode._serverFB )
                {
                    int csvFb = item.Value.idCsvFB;
                    Action func = () =>
                    {
                        Logger.Info( "开始发送扫荡命令 " + csvFb );

                        sendFBSweep( ( uint ) csvFb, ( UtilListenerEvent evt ) =>
                        {
                            RM2C_FB_SWEEP sweep = ( RM2C_FB_SWEEP ) evt.eventArgs;
                            Action action = this.sweep_queue.Dequeue();
                            Debug.Assert( action != null);

                            action();
                        } );

                    };

                    this.sweep_queue.Enqueue( func ); 
                }

                //sweep done!
                Action end = () =>
                {
                    Logger.Info( "扫荡全部完成 ");

                };
                this.sweep_queue.Enqueue( end );

                Action firstAction = this.sweep_queue.Dequeue();
                firstAction();   //do the first task

            }
            else
            {
                Logger.Error( this._account + " RM2C_FB error: " + recv.iResult );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        //副本扫荡
        public void sendFBSweep( uint fb_id, FunctionListenerEvent sListener )
        {
            Logger.Info( "ac: " + this._account + " SEND:C2RM_FB_SWEEP " + fb_id);

            C2RM_FB_SWEEP sender = new C2RM_FB_SWEEP();
            sender.uiFbId = fb_id;
            sender.uiListen = Dispatcher.addListener( sListener, null);

            this.send( sender );

            //sendPingTwo();  ? why
        }

        //扫荡副本返回
        public void recvSweep( ArgsEvent args )
        {
            RM2C_FB_SWEEP recv = args.getData<RM2C_FB_SWEEP>();

            Logger.Info( "RECV:RM2C_FB_SWEEP ret = " + recv.iResult );

            if( recv.Message == (int)EM_CLIENT_ERRORCODE.EE_M2C_FB_ID_ERROR )
            {
                Logger.Error( "副本id错误" );
                return;
            }

            // updata to temp datamode
            this.dataMode.infoFBRewardList.addMoneySweep( ( int ) recv.uiSMoney );
            this.dataMode.infoFBRewardList.exp += ( ulong ) recv.uiExp;
            this.dataMode.infoFBRewardList.expBegin = ( ulong ) recv.uiPreExp;
            this.dataMode.infoFBRewardList.isDataOver = true;
            this.dataMode.infoFBRewardList.curFbCsvID = ( int ) recv.uiFbCsvId;

            Logger.Info( "扫荡返回  SMoney: " + recv.uiSMoney + " Exp: " + recv.uiExp + " csvId: " + recv.uiFbCsvId);

            // dispatch
            Dispatcher.dispatchListener( recv.uiListen, ( object ) recv );
        }

        /// 购买副本次数 request
        public void sendBuyFBCnt( int idCsvFb, FunctionListenerEvent sListener )
        {
            C2RM_FB_RESET sender = new C2RM_FB_RESET();
            sender.uiFbCsvId = ( uint ) idCsvFb;
            sender.uiListen = Dispatcher.addListener( sListener, null);
            this.send( sender );
        }

        //response 购买副本次数 
        public void recvBuyFBCnt( ArgsEvent args )
        {
            RM2C_FB_RESET recv = args.getData<RM2C_FB_RESET>();
            if( recv.iResult == 1 )
            {
                this.dataMode.myPlayer.money_game += recv.sMoney.iSMoney;
                this.dataMode.myPlayer.money += recv.sMoney.iQMoney;

                InfoFB _fb = this.dataMode.getFB( recv.sFb.luiIdFB );
                _fb.score = ( int ) recv.sFb.cLvKo;
                _fb.comTimes = ( int ) recv.sFb.sKoTodayTimes;
                _fb.resetFBCnt = ( int ) recv.sFb.cResetTimes;
                _fb.idCsvFB = ( int ) recv.sFb.uiIdCsvFB;
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }
    }
	
}
