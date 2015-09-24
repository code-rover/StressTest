using System;
using System.Collections.Generic;
using utils;
using robot.client.common;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace net.unity3d
{
    public class AgentNet
    {
		public static readonly string accountServerId = "account_server";

        public workerNet lNetWorker = new workerNet();

        private DataMode dataMode = new DataMode();

		public AgentNet()
        {

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
            NodeQueue node = lNetWorker.tick();
			
            if (null != node)
            {
                //Logger.Info("====> " + this._account + " recv: " + node.msg );
                callEvent(node.msg, node.args);
            }
        }
		
		public bool send(IProtocal pro)
        {
            Logger.Info( "======> " + this._account + "  " + pro.Message + " time: " + GUtil.getTimeMs() );

            if (pro.Message >= 200 && pro.Message <= 1999)
            {
				if(pro.Message == 322)
				{
					//set_is_ping_back(false);
				}
				
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

        private Queue<Action> open_mail_queue = new Queue<Action>();

        //fb sweep task queue
        private Queue<Action> sweep_queue = new Queue<Action>();

        // buy fb task queue
        private Queue<Action> buy_fb_queue = new Queue<Action>();

        //buy goods task queue 
        private Queue<Action> buy_goods_queue = new Queue<Action>();

        //购买pk
        private Queue<Action> buy_pk_queue = new Queue<Action>();

        //购买爵位商店物品
        private Queue<Action> buy_nobility_queue = new Queue<Action>();

        //购买海山商店
        private Queue<Action> buy_mot_queue = new Queue<Action>();

        //碎片合成队列
        private Queue<Action> piece_to_pet_queue = new Queue<Action>();

        //升星
        private Queue<Action> star_up_queue = new Queue<Action>();

        //pet lv up
        private Queue<Action> pet_lvup_queue = new Queue<Action>();

        //穿装备 queue
        private Queue<Action> equip_puton_queue = new Queue<Action>();

        //进阶石
        private Queue<Action> stone_up_queue = new Queue<Action>();

        //装备强化
        private Queue<Action> equip_up_queue = new Queue<Action>();

        //装备合成
        private Queue<Action> equip_com_queue = new Queue<Action>();

        //技能
        private Queue<Action> skill_up_queue = new Queue<Action>();

        //魂侠抽测试
        private Queue<Action> lucky_soul_queue = new Queue<Action>();

        /// account返回
        public void recvAccountServer( ArgsEvent args )
        {
            AC2C_ACCOUNT_INFO recv = args.getData<AC2C_ACCOUNT_INFO>();
            this.connectLoginServer( recv.sAccountAC2C.getChannelIdStr(), recv.sAccountAC2C.getAccountIdStr() ); //getRelamInfo("2002");
            if( recv.iResult == 1 )
            {
                //ManagerServer.getInstance().lastLoginServerIndex = ( int ) recv.sAccountAC2C.uiServer[ 0 ];
            }
            Logger.Info(this._account + "RECV SERVER ACCOUNT = " + recv.sAccountAC2C.getAccountIdStr() );
        }

        /// login返回
        public void recvLoginServer( ArgsEvent args )
        {
            LS2C_LOGIN_RESPONSE recv = args.getData<LS2C_LOGIN_RESPONSE>();

            Logger.Info(this._account + " LS2C_LOGIN_RESPONSE >> " + recv.iResult.ToString() );
            if( recv.iResult == 1 )
            {
                Logger.Info(this._account + " Login Authentication Success" );
                Logger.Info(this._account + " realm server info: " + recv.getIp() + ":" + recv.port );

                this.initLogic( recv.getIp(), ( short ) recv.port, ( int ) recv.timeOut );
                this.connectRealmServer();
            }
            else
            {
                Logger.Info(this._account + " recvLoginServer error, iresult = " + recv.iResult );
            }
        }

        ///协议返回 realm返回
        public void recvLogin( ArgsEvent args )
        {
            RM2C_LOGIN recv = args.getData<RM2C_LOGIN>();
            Logger.Info(this._account + " connect Realm Result: " + recv.iResult );
            if( recv.iResult == ( int ) EM_CLIENT_ERRORCODE.EE_M2C_NEED_CREATE_ROLE )
            {
                Logger.Info(this._account + " 需要创建角色" );
                this.sendCreatRole( 61, null );
            }
            else if( recv.iResult == ( int ) EM_CLIENT_ERRORCODE.EE_ACCOUNT_NOT_EXIST )
            {
                Logger.Info(this._account + " Realm msg: 帐号不存在" );
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

            //领取好友赠送的体力的次数
            player.powerFriendCnt = ( int ) recv.SPlayerInfo.sLeadPowerInfo.sFriendPowerCnt;
            ///今天购买体力的次数
            player.powerBuyCnt = ( int ) recv.SPlayerInfo.sLeadPowerInfo.sPowerBuyCnt;
            //今天购买金币次数
            player.stoneBuyCnt = ( int ) recv.SPlayerInfo.sPlayerBaseInfo.sStoneTimes;

            player.maxHeroList = ( int ) recv.SPlayerInfo.sLeadBagInfo.usCntBag;
            player.maxEquipBagList = ( int ) recv.SPlayerInfo.sLeadBagInfo.usCntEquipBag;

            //游戏币
            player.money_game = ( long ) recv.SPlayerInfo.sPlayerBaseInfo.luiSMoney;
            //人民币
            player.money = ( long ) recv.SPlayerInfo.sPlayerBaseInfo.luiQMoney;
            //爵位币
            player.nmoney = ( UInt32 ) recv.SPlayerInfo.sPlayerBaseInfo.luiNMoney;
            
            ///正义徽章
            //player.infoPoint.badge = ( int ) recv.SPlayerInfo.sPlayerBaseInfo.uiBadge;
            //TODO ...

            /// 我的角色
            this.dataMode.myPlayer = player;

            //for test
            //this.sendGetNobilityShop(null);
            //this.NobilityShopStart();
            
            /*
            this.sendLuckySoulList( ( UtilListenerEvent evt ) =>
            {
                Logger.Info( "ok" );
            } );  //魂侠抽测试
             */
            //this.LuckySoulStart();

            
            //用道具加技能点
            //this.sendPropAddSP( ( UtilListenerEvent evt ) =>
            //{
            //    Logger.Info( "ok" );
            //} );


            //this.sendTempVip( ( UtilListenerEvent evt ) =>
            //{
            //    Logger.Info( "ok" );
            //} );


            /*
            this.sendLuckySoul( ( UtilListenerEvent evt ) =>
            {
                Logger.Info( "ok" );
            } );  //魂侠抽测试
            */ 
            /*
            sendChangeName( "Robot_" + this._accountId, ( UtilListenerEvent e ) =>
            {
                Logger.Info( "sendChangeName完成" );
                this.OpenMailStart();
            } );
            */

            
            if( player.name == null )
            {
                //发送更名请求
                sendChangeName( "Robot_" + this._accountId, ( UtilListenerEvent e ) =>
                {
                    this.OpenMailStart(); 
                } );
            }
            else
            {
                this.OpenMailStart();        
            }
             
        }

        //领取邮件奖励
        private void OpenMailStart() {
            FunctionListenerEvent func = ( UtilListenerEvent evt ) =>
            {
                Logger.Info( this._account + " 收到邮件总数:" + dataMode._emailInfo.Count );

                this.open_mail_queue.Clear();

                int cnt = 0;
                foreach( var e in dataMode._emailInfo )
                {
                    int csvMailId = e.Value.csvMailId;

                    if( e.Value.isOpen == 0 )
                    {
                        cnt++;
                        ulong emailId = e.Key;

                        Action action = () =>
                        {
                            sendOpenEmail( emailId, ( UtilListenerEvent evt2 ) =>
                            {
                                Logger.Info(this._account + " 打开邮件 " + emailId );

                                Action next = this.open_mail_queue.Dequeue();
                                next();

                            } );
                        };
                        this.open_mail_queue.Enqueue( action );

                    }
                }

                Action end = () =>
                {
                    Logger.Info( this._account + " 打开邮件全部完成  cnt:" + cnt );

                    this.sendPwoerBuy( null );   //购买体力

                    //魂侠抽
                    this.LuckySoulStart(5, ( UtilListenerEvent ee ) =>
                    {
                        this.sendFBUpdate( null );  //打开副本后，获取副本列表    
                    } );

                };
                this.open_mail_queue.Enqueue( end );

                Action firstAction = this.open_mail_queue.Dequeue();
                firstAction();

            };
  
            //发送获取邮件请求
            this.sendWebEmail( null );   //如果没有邮件的话，服务器不会回应,超3秒后，由定时器触发
            
            func(null);
        }

        /// 创建角色
        public void sendCreatRole( int roleCsvId, FunctionListenerEvent sListener )
        {
            C2RM_CREAT_ROLE sender = new C2RM_CREAT_ROLE();
            sender.uiPetCsvId = ( uint ) roleCsvId;
            Logger.Info(this._account + " 创建角色 id: " + roleCsvId);
            this.send( sender );
        }

        /// 改名字
        public void sendChangeName( string name, FunctionListenerEvent listener )
        {
            Logger.Info( "C2RM_ROLE_RENAME >> " + "改名字 ->" + name);

            C2RM_ROLE_RENAME sender = new C2RM_ROLE_RENAME();
            sender.setName( name );

            //C2RM_NAME_RAND sender = new C2RM_NAME_RAND();


            sender.uiListen = Dispatcher.addListener(listener, null);   
            this.send( sender );
        }

        // 改名字回应
        public void recvChangeName( ArgsEvent args )
        {
            RM2C_ROLE_RENAME recv = args.getData<RM2C_ROLE_RENAME>();
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
            Logger.Info(this._account + " SEND:C2RM_WEB_EMAIL >> " + "发送邮件信息请求 >> " );

            C2RM_WEB_EMAIL sender = new C2RM_WEB_EMAIL();
            sender.uiListen = Dispatcher.addListener( listener, null);

            this.send( sender );

        }

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
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        // 打开邮件 - wen
        public void sendOpenEmail( ulong emailId, FunctionListenerEvent sListener )
        {
            C2RM_WEB_EMAIL_OPEN sender = new C2RM_WEB_EMAIL_OPEN();
            sender.uiListen = Dispatcher.addListener( sListener, null);
            sender.uiWebEmailId = emailId;
            this.send( sender );
        }

        /// 打开邮件回调（获取物品）
        public void recvOpenEmail( ArgsEvent args )
        {
            RM2C_WEB_EMAIL_OPEN recv = args.getData<RM2C_WEB_EMAIL_OPEN>();
            
            
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
                        //csvprop = ManagerCsv.getProp( ( int ) recv.vctSEquip[ i ].uiIdCsvEquipment );
                        //...
                    }
                }
                Logger.Info(this._account + " 打开邮件成功!" );
            }
            else
            {
                Logger.Error(this._account + " 打开邮件失败 " + recv.iResult );    
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
            this.dataMode.myPlayer.infoPK.score += ( int ) recv.sRewardMoney.uiScoreFight;
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
            Logger.Info(this._account + " SEND:C2RM_FB" );
            C2RM_FB sender = new C2RM_FB();
            sender.uiListen = Dispatcher.addListener( sListener, null);
            this.send( sender );
        }

        //response 
        public void recvFBInfo( ArgsEvent args )
        {
            RM2C_FB recv = args.getData<RM2C_FB>();
            

            if( recv.iResult == 1 )
            {
                Logger.Info(this._account + " RECV:RM2C_FB 成功 " );

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

                if( recv.vctSFBInfo.Length == 0 )
                    Logger.Error( this._account + "  副本列表为空！" );
                
                this.buy_fb_queue.Clear();

                //fb sweep one by one
                foreach( var item in this.dataMode._serverFB )
                {
                    //if(item.Value.resetFBCnt > ?) {
                    //    continue;
                    //}

                    int csvFb = item.Value.idCsvFB;
                    Action func = () =>
                    {
                        Logger.Info(this._account + " 购买副本 " + csvFb );

                        sendBuyFBCnt((int)csvFb, ( UtilListenerEvent evt ) =>
                        {
                            Action action = this.buy_fb_queue.Dequeue();
                            Debug.Assert( action != null);

                            action();            
                        } );

                    };

                    this.buy_fb_queue.Enqueue( func ); 
                }

                //sweep done!
                Action end = () =>
                {
                    Logger.Info(this._account + " 购买副本重置全部完成 ");

                    this.sweepFBStart();

                };
                this.buy_fb_queue.Enqueue( end );

                Action firstAction = this.buy_fb_queue.Dequeue();
                firstAction();   //do the first task

            }
            else
            {
                Logger.Error( this._account + " RM2C_FB_RESET error: " + recv.iResult );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        //开始扫荡副本
        private void sweepFBStart()
        {
            this.sweep_queue.Clear();

            //fb sweep one by one
            foreach( var item in this.dataMode._serverFB )
            {
                int csvFb = item.Value.idCsvFB;
                Action func = () =>
                {
                    Logger.Info(this._account + " 开始发送扫荡命令 " + csvFb );

                    sendFBSweep( ( uint ) csvFb, ( UtilListenerEvent evt ) =>
                    {
                        RM2C_FB_SWEEP sweep = ( RM2C_FB_SWEEP ) evt.eventArgs;
                        Action action = this.sweep_queue.Dequeue();
                        Debug.Assert( action != null );

                        action();
                    } );

                };

                this.sweep_queue.Enqueue( func );
            }

            //sweep done!
            Action end = () =>
            {
                Logger.Info( this._account + "  扫荡全部完成 " );

                //this.sendFBUpdate(null);//loop
                this.SMoneyShopStart();
            };

            this.sweep_queue.Enqueue( end );

            Action firstAction = this.sweep_queue.Dequeue();
            firstAction();   //do the first task

        }

        //开始金币商店相关操作
        private void SMoneyShopStart()
        {
            this.sendGetSMoneyShop( ( UtilListenerEvent evt ) =>
            {
                RM2C_GET_SMONEY_SHOP res = (RM2C_GET_SMONEY_SHOP)evt.eventArgs;

                Logger.Info(this._account + " 商店列表返回: " + res.iResult );

                if( res.iResult != 1 )
                {
                    Logger.Error( this._account + " 商店列表返回失败 " + res.iResult );
                }

                this.sendRefreshMoneyShop( 0, ( UtilListenerEvent ev ) =>
                {
                    RM2C_REFRESH_SMONEY_SHOP re = ( RM2C_REFRESH_SMONEY_SHOP ) ev.eventArgs;
                    if( re.iResult != 1 )
                    {
                        Logger.Error( this._account + " 刷新金币商店返回失败 " + re.iResult );
                    }
                    Logger.Info(this._account + " 刷新商店返回" );
                    Debug.Assert( this.dataMode.myPlayer.infoMoneyShop.sells.Count == 8 );


                    this.buy_goods_queue.Clear();

                    //遍历购买所有物品 8个
                    foreach( InfoShopObject item in this.dataMode.myPlayer.infoMoneyShop.sells )
                    {
                        if( item.isSell == true )
                            continue;

                        int index = item.index;

                        Action action = () =>
                        {
                            this.sendBuyMoneyShop(index, ( UtilListenerEvent e ) =>
                            {
                                RM2C_SMONEY_SHOP_BUY buy = ( RM2C_SMONEY_SHOP_BUY ) e.eventArgs;
                                if( buy.iResult != 1 )
                                {
                                    Logger.Error( this._account + " 商店购买返回失败 " + buy.iResult );
                                }
                                Logger.Info(this._account + " 商店购买返回  " + buy.iLoc );

                                Action task = this.buy_goods_queue.Dequeue();

                                task();
                            } );     

                        };

                        this.buy_goods_queue.Enqueue( action );
                    }

                    //购买完成任务
                    Action end = () =>
                    {
                        Logger.Info(this._account + " 购买商品完成!" );

                        this.PKShopStart();
                    };

                    this.buy_goods_queue.Enqueue(end);

                    Action firstAction = this.buy_goods_queue.Dequeue();
                    firstAction();

                } );

            } );    
        }

        //副本扫荡
        public void sendFBSweep( uint fb_id, FunctionListenerEvent sListener )
        {
            Logger.Info(this._account + " 副本扫荡  " + fb_id);

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

            if( recv.iResult == 1 )
            {
                // updata to temp datamode
                this.dataMode.infoFBRewardList.addMoneySweep( ( int ) recv.uiSMoney );
                this.dataMode.infoFBRewardList.exp += ( ulong ) recv.uiExp;
                this.dataMode.infoFBRewardList.expBegin = ( ulong ) recv.uiPreExp;
                this.dataMode.infoFBRewardList.isDataOver = true;
                this.dataMode.infoFBRewardList.curFbCsvID = ( int ) recv.uiFbCsvId;

                Logger.Info(this._account+ " 扫荡返回成功  SMoney: " + recv.uiSMoney + " Exp: " + recv.uiExp + " csvId: " + recv.uiFbCsvId );
            }
            else
            {
                Logger.Error(this._account + " 副本扫荡错误  " + recv.iResult );    
            }
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
                Logger.Info( this._account + " 购买副本成功 " + recv.sFb.uiIdCsvFB);
                this.dataMode.myPlayer.money_game += recv.sMoney.iSMoney;
                this.dataMode.myPlayer.money += recv.sMoney.iQMoney;

                InfoFB _fb = this.dataMode.getFB( recv.sFb.luiIdFB );
                _fb.score = ( int ) recv.sFb.cLvKo;
                _fb.comTimes = ( int ) recv.sFb.sKoTodayTimes;
                _fb.resetFBCnt = ( int ) recv.sFb.cResetTimes;
                _fb.idCsvFB = ( int ) recv.sFb.uiIdCsvFB;
            }
            else
            {
                Logger.Error( this._account + " 购买副本失败 " + recv.iResult );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }


        //C2RM_GET_SMONEY_SHOP
        public void sendGetSMoneyShop( FunctionListenerEvent listener )
        {
            Logger.Info( this._account + " 发送金币商店信息获取命令");
            C2RM_GET_SMONEY_SHOP sender = new C2RM_GET_SMONEY_SHOP();
            sender.uiListen = Dispatcher.addListener( listener, null);
            this.send( sender );
        }

        ///获取金币商店回复
        public void recvReplyMoneyShop( ArgsEvent args )
        {
            RM2C_GET_SMONEY_SHOP recv = args.getData<RM2C_GET_SMONEY_SHOP>();
            //this.dataMode.myPlayer.infoMoneyShop.infoCD.timeTeamp = ( double ) recv.uiRefreshTime;

            if( recv.iResult == 1 )
            {
                Logger.Info( this._account + " 金币商店信息获取成功");

                this.dataMode.myPlayer.infoMoneyShop.timesReset = recv.iCntRefresh;

                int count = recv.m_vctShopGoodsp.Length;
                this.dataMode.myPlayer.infoMoneyShop.sells.Clear();

                for( int i = 0; i < count; i++ )
                {
                    InfoShopObject obj = new InfoShopObject();
                    obj.index = i;
                    obj.idCsvShop = ( int ) recv.m_vctShopGoodsp[ i ].uiIdCsvGoods;
                    if( recv.m_vctShopGoodsp[ i ].cIsBuy == 0 )
                        obj.isSell = false;
                    else
                        obj.isSell = true;
                    this.dataMode.myPlayer.infoMoneyShop.sells.Add( obj );
                }
            }
            else
            {
                Logger.Error( this._account + " 金币商店信息获取失败  " + recv.iResult );
            }
              
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        //刷新金币商店
        public void sendRefreshMoneyShop( int type, FunctionListenerEvent listener )
        {
            Logger.Info(this._account + " SEND:C2RM_REFRESH_SMONEY_SHOP >> " + "刷新金币商店 >> " );
            C2RM_REFRESH_SMONEY_SHOP sender = new C2RM_REFRESH_SMONEY_SHOP();
            sender.cType = ( byte ) type;
            sender.uiListen = Dispatcher.addListener( listener,null);
            this.send( sender );
        }

        ///金币商店购买
        public void sendBuyMoneyShop( int index, FunctionListenerEvent listener )
        {
            Logger.Info(this._account + " SEND:C2RM_SMONEY_SHOP_BUY >> " + "购买金币商店 >> " + index );
            C2RM_SMONEY_SHOP_BUY sender = new C2RM_SMONEY_SHOP_BUY();
            sender.iLoc = index;
            sender.uiListen = Dispatcher.addListener( listener, null);
            this.send( sender );
        }

        ///刷新金币商店回复
        public void recvRefreshMoneyShop( ArgsEvent args )
        {
            RM2C_REFRESH_SMONEY_SHOP recv = args.getData<RM2C_REFRESH_SMONEY_SHOP>();
            //this.dataMode.myPlayer.infoMoneyShop.infoCD.timeTeamp = ( double ) recv.uiRefreshTime;
            if( recv.iResult == 1 )
            {
                Logger.Info( this._account + " 金币商店刷新成功");
   
                this.dataMode.myPlayer.infoMoneyShop.timesReset = recv.iCntRefresh;

                this.dataMode.myPlayer.money += recv.iCost;

                int count = recv.m_vctShopGoodsp.Length;
                this.dataMode.myPlayer.infoMoneyShop.sells.Clear();

                for( int i = 0; i < count; i++ )
                {
                    InfoShopObject obj = new InfoShopObject();
                    obj.index = i;
                    obj.idCsvShop = ( int ) recv.m_vctShopGoodsp[ i ].uiIdCsvGoods;
                    if( recv.m_vctShopGoodsp[ i ].cIsBuy == 0 )
                        obj.isSell = false;
                    else
                        obj.isSell = true;
                    this.dataMode.myPlayer.infoMoneyShop.sells.Add( obj );
                }
            }
            else
            {
                Logger.Error( this._account + " 金币商店刷新失败  " + recv.iResult );   
            }

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }
        
        ///购买金币商店物品回复
        public void recvBuyMoneyShop( ArgsEvent args )
        {
            RM2C_SMONEY_SHOP_BUY recv = args.getData<RM2C_SMONEY_SHOP_BUY>();
            if( recv.iResult == 1 )
            {
                Logger.Info( this._account + " 金币商店物品购买成功");  

                this.dataMode.myPlayer.infoMoneyShop.sells[ recv.iLoc ].isSell = true;
                this.dataMode.myPlayer.money_game += recv.iCost;

                if( recv.SPiece.uiCsvId > 0 )
                    this.dataMode.infoHeroChip.setHeroChip( ( int ) recv.SPiece.uiCsvId, recv.SPiece.iCnt );

                if( recv.SEquip.uiIdCsvEquipment > 0 )
                {
                    TypeCsvProp csvprop = ManagerCsv.getProp( ( int ) recv.SEquip.uiIdCsvEquipment );
                    if( csvprop.isPropBeast() )
                    {
                        //this.dataMode.myPlayer.infoPropBeastList.changeProp( ( int ) recv.SEquip.uiIdCsvEquipment, recv.SEquip.iCnt );
                    }
                    else
                    {
                        this.dataMode.myPlayer.infoPropList.changeProp( ( int ) recv.SEquip.uiIdCsvEquipment, recv.SEquip.iCnt );
                    }
                }
                //TODO
            }
            else
            {
                Logger.Error( this._account + " 金币商店物品购买失败  " + recv.iResult );     
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        //开始pk shop相关操作
        private void PKShopStart()
        {
            //get pkshop list
            this.sendPKShopUpdate( ( UtilListenerEvent evt ) =>
            {
                //得到pk商店列表后，重置
                this.sendPKShopReset(1, ( UtilListenerEvent ev ) =>
                {
                    Logger.Info(this._account + " 刷新PK商店返回" );
                    Debug.Assert( this.dataMode.myPlayer.infoPK.infoShop.sells.Count == 8 );

                    this.buy_pk_queue.Clear();

                    //遍历购买所有物品 8个
                    foreach( InfoShopObject item in this.dataMode.myPlayer.infoPK.infoShop.sells )
                    {
                        if( item.isSell == true )
                            continue;

                        int index = item.index;

                        Action action = () =>
                        {
                            //购买pk商店物品
                            this.sendPKShopBuy( index, ( UtilListenerEvent e ) =>
                            {
                                RM2C_PK_SHOP_BUY buy = ( RM2C_PK_SHOP_BUY ) e.eventArgs;

                                Action next = this.buy_pk_queue.Dequeue();
                                next();   //execute next task in queue
                            } );
                        };
                        this.buy_pk_queue.Enqueue( action );
                    }

                    //购买完成任务
                    Action end = () =>
                    {
                        Logger.Info( this._account + " ======================= 购买PK商品完成!" );

                        //this.MotShopStart();
                        this.piece2petStart();
                    };
                    this.buy_pk_queue.Enqueue( end );

                    Action firstAction = this.buy_pk_queue.Dequeue();
                    firstAction();
                } );
            } );      
        }

        //开始爵位商店相关操作
        private void NobilityShopStart()
        {
            //get shop list
            this.sendGetNobilityShop( ( UtilListenerEvent evt ) =>
            {
                RM2C_GET_NOBILITY_SHOP rec = ( RM2C_GET_NOBILITY_SHOP ) evt.eventArgs;
                //得到爵位商店列表后，重置
                this.sendNobilityShopReset( 1, ( UtilListenerEvent ev ) =>
                {
                    Logger.Info( this._account + " 刷新爵位商店返回" );

                    RM2C_REFRESH_NOBILITY_SHOP recv = ( RM2C_REFRESH_NOBILITY_SHOP ) ev.eventArgs;
                    if( recv.iResult != 1 )
                    {
                        Logger.Error(this._account + " 爵位商店重置失败！" + recv.iResult);
                    }
                    this.buy_nobility_queue.Clear();

                    //遍历购买所有物品 8个
                    int index = 0;
                    foreach( STowerShopGood item in recv.m_vctShopGoodsp )
                    {
                        if( item.cIsBuy == 1 )
                            continue;

                        int _index = index;
                        Action action = () =>
                        {
                            //购买爵位商店物品
                            this.sendNobilityShopBuy( _index, ( UtilListenerEvent e ) =>
                            {
                                RM2C_NOBILITY_SHOP_BUY buy = ( RM2C_NOBILITY_SHOP_BUY ) e.eventArgs;

                                Action next = this.buy_nobility_queue.Dequeue();
                                next();   //execute next task in queue
                            } );
                        };
                        this.buy_nobility_queue.Enqueue( action );
                        index++;
                    }

                    //购买完成任务
                    Action end = () =>
                    {
                        Logger.Info( this._account + " ======================= 购买爵位商品完成!" );
                        this.NobilityShopStart();  //restart for loop
                    };
                    this.buy_nobility_queue.Enqueue( end );

                    Action firstAction = this.buy_nobility_queue.Dequeue();
                    firstAction();
                } );
            } );
        }

        //开始 Mot shop相关操作
        private void MotShopStart()
        {
            Logger.Info( this._account + " 开始操作远征商店" );

            //获取远征商店列表
            this.sendGetEDShop( ( UtilListenerEvent evt ) =>
            {
                //得到远征商店列表后，重置
                this.sendRefreshEDShop( 1, ( UtilListenerEvent ev ) =>
                {
                    Logger.Info(this._account + " 刷新远程商店返回" );
                    Debug.Assert( this.dataMode.myPlayer.infoEDShop.sells.Count == 8 );

                    this.buy_mot_queue.Clear();

                    //遍历购买所有物品 8个
                    foreach( InfoShopObject item in this.dataMode.myPlayer.infoEDShop.sells )
                    {
                        if( item.isSell == true )
                            continue;

                        int index = item.index;

                        Action action = () =>
                        {
                            //购买远征商店物品
                            this.sendBuyEDShop( index, ( UtilListenerEvent e ) =>
                            {
                                RM2C_MOUNTAIN_SHOP_BUY buy = ( RM2C_MOUNTAIN_SHOP_BUY ) e.eventArgs;
                                if( buy.iResult != 1 )
                                {
                                    Logger.Error( this._account + " 远征商店购买返回失败 " + buy.iResult );
                                }
                                Logger.Info(this._account + " 远征商店购买返回  " + buy.iLoc );

                                Action next = this.buy_mot_queue.Dequeue();
                                next();   //execute next task in queue
                            } );
                        };
                        this.buy_mot_queue.Enqueue( action );
                    }

                    //购买完成任务
                    Action end = () =>
                    {
                        Logger.Info(this._account + " 购买远征商品完成!" );

                        this.piece2petStart();    
                    };
                    this.buy_mot_queue.Enqueue( end );

                    Action firstAction = this.buy_mot_queue.Dequeue();
                    firstAction();
                } );
            } );
        }

        //碎片合成
        public void piece2petStart()
        {
            //获取碎片列表，准备合成英雄
            this.sendHeroChipUpdate( ( UtilListenerEvent evt ) =>
            {
                Dictionary<int, long> chipHero = this.dataMode.infoHeroChip.chipHero;

                this.piece_to_pet_queue.Clear();

                foreach( var item in chipHero )
                {
                    int sameid = item.Key;

                    Action action = () =>
                    {
                        Logger.Info( this._account + " 碎片合成 " + sameid );
                        this.sendPetChipToPet( sameid, ( UtilListenerEvent evt2 ) =>
                        {
                            RM2C_PET_PIECE_TO_PET p2p = ( RM2C_PET_PIECE_TO_PET ) evt2.eventArgs;

                            Logger.Info( this._account + " 碎片合成返回 " + p2p.sPiece.luiIdPiece );

                            Action next = this.piece_to_pet_queue.Dequeue();
                            next();   //execute next task in queue
                        } );   
                    };
                    this.piece_to_pet_queue.Enqueue( action );
                }

                Action end = () => {
                    Logger.Info(this._account + " 碎片合成完成");

                    this.heroStart();
                };
                this.piece_to_pet_queue.Enqueue( end );

                Action firstAction = this.piece_to_pet_queue.Dequeue();
                firstAction();  //start
            } );
        }

        //英雄相关操作开始
        public void heroStart()
        {
            Logger.Info( this._account + " 开始获取英雄列表" );
            //获取英雄列表
            this.sendHeroUpdateBag( ( UtilListenerEvent evt2 ) =>
            {
                RM2C_PET_INFO_BAG pet_bag = ( RM2C_PET_INFO_BAG ) evt2.eventArgs;

                List<ulong> heroList = this.dataMode.myPlayer.infoHeroList.getHeroList();

                foreach( ulong hid in heroList )
                {
                    ulong serverId = hid;
                    InfoHero heroInfo = this.dataMode.getHero( serverId );
                    //Debug.Assert( heroInfo != null, "heroInfo is null" );
                    if( heroInfo == null )
                        continue;

                    if( heroInfo.star == 10 )
                        continue;

                    Action action = () =>
                    {
                        Logger.Info( this._account + " pet开始升星  " + serverId );
                        sendPetStarUp( serverId, ( UtilListenerEvent evt ) =>
                        {
                            RM2C_PET_STAR_UP starup = ( RM2C_PET_STAR_UP ) evt.eventArgs;
                            Logger.Info( this._account + " 卡牌升级返回  star: " + starup.sPetStarInfo.uiStar );

                            Action next = this.star_up_queue.Dequeue();
                            next();   //execute next task in queue
                        } );    
                    };

                    this.star_up_queue.Enqueue( action );
                }

                Action end = () =>
                {
                    Logger.Info(this._account + " 英雄升级己完成");

                    this.petLvUpStart();
                };
                this.star_up_queue.Enqueue( end );

                Action firstAction = this.star_up_queue.Dequeue();
                firstAction();
            } );
        }

        //吃经验药升级开始
        public void petLvUpStart()
        {
            //获取装备信息
            this.sendEquipUpdate( (UtilListenerEvent evt) =>
            {
                List<ulong> heroList = this.dataMode.myPlayer.infoHeroList.getHeroList();

                this.pet_lvup_queue.Clear();

                foreach( ulong hid in heroList )
                {
                    ulong serverId = hid;
                    InfoHero heroInfo = this.dataMode.getHero( serverId );
                    //Debug.Assert( heroInfo != null, "heroInfo is null-" );
                    if( heroInfo == null )
                        continue;

                    if( heroInfo.lv >= 60 )  //己达到60级，无需再升
                        continue;

                    List<InfoProp> props = this.dataMode.myPlayer.infoPropList.getProps();
                    Dictionary<int, int> propExp = new Dictionary<int, int>();  //idCsv,cnt
                    foreach( InfoProp item in props )
                    {
                        if( item.idCsv >= 29 && item.idCsv <= 32 )  //29:小药瓶  30：中瓶 31：大瓶 32：特大瓶
                        {
                            propExp.Add( item.idCsv, item.cnt );
                        }
                    }

                    int expType = 0;  //药瓶类型
                    int expCnt = 0;   //药瓶数量

                    for( int i = 32; i >= 29; i-- )
                    {
                        if( propExp.ContainsKey( i ) )
                        {
                            if( i == 32 && propExp[ i ] >= 20 )   //32 巨量药瓶需要20个    160000
                            {
                                expType = 32;
                                expCnt = 20;
                            }
                            else if( i == 31 && propExp[ i ] >= 80 )  //31 大量药瓶需要80个    2000*80 
                            {
                                expType = 31;
                                expCnt = 80;
                            }
                            else if( i == 30 && propExp[ i ] >= 400 ) //30 中瓶需要 400个     400*400
                            {
                                expType = 30;
                                expCnt = 400;
                            }
                            else if( i == 29 && propExp[ i ] >= 2000 ) //29 小瓶需要 2000个  80*2000
                            {
                                expType = 29;
                                expCnt = 2000;
                            }
                        }
                    }

                    if( expType == 0 )
                    {
                        Logger.Error( this._account + " 经验药不足x" );
                        //return;
                    }

                    Action action = () =>
                    {
                        Logger.Info( this._account + " pet吃经验药升级  " + serverId );
                        //this.sendPetLvUp( serverId, 32, 4, ( UtilListenerEvent evt2 ) =>  //32  4  Hard code
                        this.sendPetLvUp( serverId, expType, expCnt, ( UtilListenerEvent evt2 ) =>  //32  20*8000  Hard code
                        {
                            RM2C_PET_LV_UP ret = ( RM2C_PET_LV_UP ) evt2.eventArgs;

                            if( ret.iResult == 1 )
                                Logger.Info( this._account + " 卡牌吃经验药升级返回 : " + ret.sPetEat.uiExp );

                            Action next = this.pet_lvup_queue.Dequeue();
                            next();   //execute next task in queue
                        } );
                    };

                    this.pet_lvup_queue.Enqueue( action );
                }

                Action end = () =>
                {
                    Logger.Info(this._account +  "Pet Lv up 完成" );

                    this.EquipStart(false);  //升级完成，开始进行装备相关操作

                };
                this.pet_lvup_queue.Enqueue( end );

                Action firstAction = this.pet_lvup_queue.Dequeue();
                firstAction();
            });
        }

        //穿装备
        private void EquipStart(bool isDoNext)
        {
            this.equip_puton_queue.Clear();

            //获取最新装备信息
            this.sendEquipUpdate( ( UtilListenerEvent evt ) =>
            {
                Dictionary<ulong, InfoHero> heroMap = this.dataMode._serverHero;
                
                foreach( var item in heroMap )
                {
                    InfoHero hero = item.Value;
                    TypeCsvHero heroCsv = ManagerCsv.getHero( hero.idCsv );

                    int[] equips = new int[] { 0, 0, 0, 0, 0, 0 };

                    //没有选择装备
                    List<InfoProp> po = hero.infoEquip.getProps();

                    //保存原先的装备
                    for( int m = 0; m < po.Count; m++ )
                    {
                        TypeCsvPropEquip mqu = ManagerCsv.getPropEquip( po[ m ].idCsv );
                        equips[ mqu.local ] = mqu.id;
                    }

                    List<InfoProp> props = this.dataMode.myPlayer.infoPropList.getProps();

                    foreach( InfoProp prop in props )
                    {
                        TypeCsvPropEquip csvequip = ManagerCsv.getPropEquip( prop.idCsv );
                        if( csvequip == null )
                            continue;

                        if( equips[ csvequip.local ] != 0 )  //如果此处已经被占
                            continue;

                        bool isCanUse = false;
                        for( int i = 0; i < csvequip.limitJob.Length; i++ )
                        {
                            //如果职业&&等级符合条件
                            if( csvequip.limitJob[ i ].ToString() == heroCsv.job.ToString() && csvequip.limitLv <= hero.lv )
                            {
                                isCanUse = true;
                                break;
                            }
                        }
                        if( !isCanUse )
                            continue;

                        equips[ csvequip.local ] = csvequip.id;

                        this.dataMode.myPlayer.infoPropList.removeProp( csvequip.id, 1 );
                        
                    }

                    ulong idServer = hero.idServer;
                    Action action = () =>
                    {
                        this.sendHeroEquipChange( idServer, equips, ( UtilListenerEvent evt3 ) =>
                        {
                            Action next = this.equip_puton_queue.Dequeue();
                            next();   //execute next task in queue
                        } );
                    };

                    this.equip_puton_queue.Enqueue( action );

                }

                Action end = () =>
                {
                    Logger.Info(this._account + " 穿装备己完成" );

                    if( isDoNext )
                        this.StoneStart();

                    //for loop
                    this.LuckySoulStart( 5, ( UtilListenerEvent ee ) =>
                    {
                        this.sendFBUpdate( null );  //打开副本后，获取副本列表    
                    } );

                };
                this.equip_puton_queue.Enqueue( end );

                Action firstAction = this.equip_puton_queue.Dequeue();
                firstAction();

            } );
        }
        
        //钻石镶嵌及升级
        private void StoneStart() {
            this.stone_up_queue.Clear();

            //step 1. 先重新获取英雄列表
            this.sendHeroUpdateBag( ( UtilListenerEvent evt2 ) =>
            {
                RM2C_PET_INFO_BAG pet_bag = ( RM2C_PET_INFO_BAG ) evt2.eventArgs;

                List<ulong> heroList = this.dataMode.myPlayer.infoHeroList.getHeroList();

                //for( int x = 0; x < 10; x++ )
                {

                    foreach( ulong hid in heroList )
                    {
                        ulong serverId = hid;
                        InfoHero heroInfo = this.dataMode.getHero( serverId );
                        if( heroInfo == null )
                            continue;

                        TypeCsvHero csvHero = ManagerCsv.getHero( heroInfo.idCsv );
                        //if( csvHero.grade == csvHero.gradeMax && csvHero.gradeLv == 3 )    //如果已经到顶级,不能再升
                        if( csvHero.grade == csvHero.gradeMax)    //如果已经到顶级,不能再升
                            continue;

                        TypeCsvHeroUp heroUp = ManagerCsv.getHeroUp( heroInfo.idCsv );

                        int needStoneCnt = 0;  //计算需要镶嵌数量
                        for( int i = 0; i < 6; i++ )
                        {
                            if( heroUp.GetStoneId( i ) == -1 )
                                break;
                            needStoneCnt++;
                        }

                        int inlayCnt = 0;
                        for( int i = 0; i < needStoneCnt; i++ )
                        {
                            heroInfo = this.dataMode.getHero( serverId ); //升级后，英雄csv会改变
                       
                            if( !heroInfo.infoStone.hasStone( i ) )  //如果此位置没有镶钻石
                            {   
                                int index = i;
                                ulong sid = serverId;
                                Action action = () =>
                                {
                                    //执行前重新确认该位置是否有石头
                                    InfoHero h = this.dataMode.getHero( sid );
                                    if( h.infoStone.hasStone( index ) )
                                        return;
    
                                    //镶钻
                                    this.sendStoneInLay( sid, index, ( UtilListenerEvent evt ) =>
                                    {
                                        RM2C_STONE_INLAY recv = ( RM2C_STONE_INLAY ) evt.eventArgs;
                                        Debug.Assert( sid == recv.luiIdPet );

                                        if( recv.iResult == 1 )
                                        {
                                            InfoHero hero = this.dataMode.getHero( recv.luiIdPet );

                                            //镶完钻石后，判断是不是满了
                                            int n = 0;
                                            for( int j = 0; j < needStoneCnt; j++ )
                                            {
                                                if( hero.infoStone.hasStone( j ) )
                                                    n++;
                                            }

                                            if( n == needStoneCnt )  //石槽己满,该进阶了
                                            {
                                                //进阶
                                                this.sendPetStoneUp( sid, ( UtilListenerEvent evt3 ) =>
                                                {
                                                    RM2C_PET_STONE_UP recv2 = ( RM2C_PET_STONE_UP ) evt3.eventArgs;

                                                    heroInfo = this.dataMode.getHero( recv2.luiIdPet );

                                                    Action next = this.stone_up_queue.Dequeue();
                                                    next();   //execute next task in queue
                                                } );
                                                return;
                                            }
                                        }
                                            Action next1 = this.stone_up_queue.Dequeue();
                                            next1();   //execute next task in queue
                                    } );
                                };

                                this.stone_up_queue.Enqueue( action );
                            }
                            else
                            {
                                inlayCnt++;
                            }
                        }

                        if( inlayCnt == needStoneCnt )  //钻石己镶满，准备进阶
                        {
                            //if( csvHero.grade == csvHero.gradeMax && csvHero.gradeLv == 3 )    //如果已经到顶级,不能再升
                            if( csvHero.grade == csvHero.gradeMax)    //如果已经到顶级,不能再升
                                continue;

                            ulong sid = serverId;
                            Action action = () =>
                            {
                                //进阶
                                this.sendPetStoneUp( sid, ( UtilListenerEvent evt3 ) =>
                                {
                                    RM2C_PET_STONE_UP recv2 = ( RM2C_PET_STONE_UP ) evt3.eventArgs;
                                    heroInfo = this.dataMode.getHero( recv2.luiIdPet );
                                    Action next = this.stone_up_queue.Dequeue();
                                    next();
                                } );
                            };
                            this.stone_up_queue.Enqueue( action );
                        }

                    };
                }
                Action end = () =>
                {
                    Logger.Info(this._account + " 英雄进阶全部完毕" );
                    Logger.Info(this._account + " 重新尝试是否有可穿装备..." );

                    this.EquipStart(false);

                    this.EquipUpStart(); //装备强化
                    
                };
                this.stone_up_queue.Enqueue( end );

                Action firstAction = this.stone_up_queue.Dequeue();
                firstAction();  

            } );
        }

        //装备强化相关操作
        private void EquipUpStart()
        {
            this.equip_up_queue.Clear();

            //step 1. 先重新获取英雄列表
            this.sendHeroUpdateBag( ( UtilListenerEvent evt2 ) =>
            {
                RM2C_PET_INFO_BAG pet_bag = ( RM2C_PET_INFO_BAG ) evt2.eventArgs;

                List<ulong> heroList = this.dataMode.myPlayer.infoHeroList.getHeroList();


                Dictionary<ulong, InfoHero> heroMap = this.dataMode._serverHero;
                
                foreach( var item in heroMap )
                {
                    InfoHero hero = item.Value;
                    TypeCsvHero heroCsv = ManagerCsv.getHero( hero.idCsv );

                    List<InfoProp> po = hero.infoEquip.getProps();

                    for( int m = 0; m < po.Count; m++ )
                    {
                        TypeCsvPropEquip mqu = ManagerCsv.getPropEquip( po[ m ].idCsv );

                        if(mqu.addNumber >= 9)  //己经强化到顶级,不能再升
                            continue;

                        Action action = () =>
                        {
                            Logger.Info( this._account + " 开始强化装备 idServer: " + hero.idServer + " loc: " + mqu.local + " 当前级别: " + mqu.addNumber);
                            //装备强化
                            this.sendEquipUp( mqu.local, hero.idServer, ( UtilListenerEvent evt3 ) =>
                            {
                                Action next = this.equip_up_queue.Dequeue();
                                next();   //execute next task in queue
                            } );
                        };

                        this.equip_up_queue.Enqueue( action ); 
                    }
                }
               
                Action end = () =>
                {
                    Logger.Info(this._account + " 装备强化完毕!" );
                    Logger.Info(this._account + "准备装备合成..." );

                    this.EquipComStart();
                };

                this.equip_up_queue.Enqueue( end );

                Action firstAction = this.equip_up_queue.Dequeue();
                firstAction();  

            } );
        }

        //装备合成
        private void EquipComStart()
        {
            this.equip_com_queue.Clear();
            List<TypeCsvEquipCreat>  creatCsvList = ManagerCsv.getEquipCreat();

            foreach( TypeCsvEquipCreat creat in creatCsvList )      //18
            {
                TypeCsvProp csv = ManagerCsv.getProp( creat.idEquip );
                TypeCsvConsume consum = ManagerCsv.getConsume( csv.idCom );

                bool propOk = true;
                int needProps = consum.idProps.GetLength( 0 );
                for( int i = 0; i < needProps; i++ )
                {
                    TypeCsvProp tmp = ManagerCsv.getProp( ( int ) double.Parse( consum.idProps[ i ][ 0 ] ) );
                    if( this.dataMode.myPlayer.infoPropList.getProp( tmp.id ) == null )  //如果包里没有此材料
                    {
                        propOk = false;
                        break;
                    };
                }
                
                if( propOk ) //条件具备，准备合成装备
                {
                    int csvProp = creat.idEquip;
                    Action action = () =>
                    {
                        //TODO  需要重新确认合成需要材料是否具备
                        /*
                        bool check = true;
                        for( int i = 0; i < consum.idProps.GetLength( 0 ); i++ )
                        {
                            TypeCsvProp c = ManagerCsv.getProp( ( int ) double.Parse( consum.idProps[ i ][ 0 ] ) );

                            if( this.dataMode.myPlayer.infoPropList.getProp( c.id ) == null )  //如果包里没有此材料
                            {
                                check = false;
                                break;
                            };
                        }
                        if( !check )
                            return;
                        */
                        //装备合成
                        this.sendEquipCreat( ( uint ) csvProp, ( UtilListenerEvent evt2 ) =>
                        {
                            Action next = this.equip_com_queue.Dequeue();
                            next();   //execute next task in queue
                        }); 
                    };
                    this.equip_com_queue.Enqueue( action );      
                }
            }

            Action end = () =>
            {
                Logger.Info( this._account + " 装备合成完毕!" );

                Logger.Info(this._account + " 重新尝试是否有可穿装备..." );

                this.EquipStart( false );

                this.SkillStart(); //技能升级
            };
            this.equip_com_queue.Enqueue( end );
            Action firstAction = this.equip_com_queue.Dequeue();
            firstAction(); 
        }

        //技能相关操作
        private void SkillStart()
        {
            
            this.skill_up_queue.Clear();
            Logger.Info( this._account + " 开始技能操作" );
            //获取英雄列表
            this.sendHeroUpdateBag( ( UtilListenerEvent evt2 ) =>
            {
                RM2C_PET_INFO_BAG pet_bag = ( RM2C_PET_INFO_BAG ) evt2.eventArgs;

                Dictionary<ulong, InfoHero> heroMap = this.dataMode._serverHero;
                foreach( var item in heroMap )
                {
                    InfoHero heroInfo = item.Value;
                    Debug.Assert( heroInfo != null, "heroInfo is null" );

                    List<InfoSkill> skills = heroInfo.infoSkill.getSkillAll();

                    foreach( InfoSkill skill in skills )
                    {
                        //if(ManagerCsv.getHeroSkillBase(skill.idCsv).grade < )
                        InfoSkill _skill = skill;
                        Action action = () =>
                        {
                            Logger.Info( this._account + " 请求技能升级  " + heroInfo.idServer + " " + _skill.idCsv);
                            this.sendSkillUpNew( heroInfo.idServer, _skill.idCsv, ( UtilListenerEvent evt ) =>
                            {
                                RM2C_SKILL_UP_NEW recv = ( RM2C_SKILL_UP_NEW ) evt.eventArgs;

                                System.Timers.Timer timer = new System.Timers.Timer();
                                timer.Interval = 80;
                                timer.AutoReset = false; //once
                                timer.Enabled = true;
                                timer.Elapsed += new ElapsedEventHandler( ( object source, System.Timers.ElapsedEventArgs e ) =>
                                {
                                    Action next = this.skill_up_queue.Dequeue();
                                    next();   //execute next task in queue
                                } );
                            } );
                        };
                        this.skill_up_queue.Enqueue( action );
                    }
                }

                Action end = () =>
                {
                    Logger.Info( "技能升级己完成!" );


                    
                };
                this.skill_up_queue.Enqueue( end );

                Action firstAction = this.skill_up_queue.Dequeue();
                firstAction();
            } );
            
        }


        /////////////////////////////////////////////////////////////////////////////////////


        /// 竞技 商店 更新
        public void sendPKShopUpdate( FunctionListenerEvent sListener )
        {
            Logger.Info(this._account + " SEND:C2RM_GET_PK_SHOP >> 竞技 商店 更新" );
            C2RM_GET_PK_SHOP sender = new C2RM_GET_PK_SHOP();
            sender.uiListen = Dispatcher.addListener( sListener, null);
            this.send( sender );
        }

        /// 竞技 商店 刷新 0系统定时自动刷新，1竞技积分刷新
        public void sendPKShopReset( byte sType, FunctionListenerEvent sListener )
        {
            Logger.Info(this._account + " 竞技商店刷新" );
            C2RM_REFRESH_PK_SHOP sender = new C2RM_REFRESH_PK_SHOP();
            sender.uiListen = Dispatcher.addListener( sListener, null);
            sender.cType = sType;
            this.send( sender );
        }

        /// 竞技 商店 购买
        public void sendPKShopBuy( int sIndex, FunctionListenerEvent sListener )
        {
            Logger.Info(this._account + " 竞技商店购买 " + sIndex );
            C2RM_PK_SHOP_BUY sender = new C2RM_PK_SHOP_BUY();
            sender.uiListen = Dispatcher.addListener( sListener, null);
            sender.iLoc = sIndex;
            this.send( sender );
        }

        /// 竞技 商店 更新
        public void recvPKShopUpdate( ArgsEvent args )
        {
            RM2C_GET_PK_SHOP recv = args.getData<RM2C_GET_PK_SHOP>();
            

            if( 1 == recv.iResult )
            {
                Logger.Info(this._account + "PK商店列表成功回应");
                this.dataMode.myPlayer.infoPK.infoShop.sells.Clear();
                //this.dataMode.myPlayer.infoPK.infoShop.infoCD.timeTeamp = ( double ) recv.uiRefreshTime;
                this.dataMode.myPlayer.infoPK.infoShop.timesReset = recv.iCntRefresh;

                for( int index = 0; index < recv.m_vctShopGoodsp.Length; index++ )
                {
                    InfoShopObject shopObj = new InfoShopObject();
                    shopObj.idCsvShop = ( int ) recv.m_vctShopGoodsp[ index ].uiIdCsvGoods;
                    shopObj.isSell = ( recv.m_vctShopGoodsp[ index ].cIsBuy == 1 );
                    shopObj.index = index;
                    this.dataMode.myPlayer.infoPK.infoShop.sells.Add( shopObj );
                }
            }
            else
            {
                Logger.Error( this._account + " PK商店列表返回失败 " + recv.iResult );
                return;
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 竞技 商店 刷新 0系统定时自动刷新，1竞技积分刷新
        public void recvPKShopReset( ArgsEvent args )
        {
            RM2C_REFRESH_PK_SHOP recv = args.getData<RM2C_REFRESH_PK_SHOP>();

            if( 1 == recv.iResult )
            {
                Logger.Info( this._account + "PK商店刷新成功回应" );
                this.dataMode.myPlayer.infoPK.score += recv.iCostScoreFight;

                this.dataMode.myPlayer.infoPK.infoShop.sells.Clear();
                //this.dataMode.myPlayer.infoPK.infoShop.infoCD.timeTeamp = ( double ) recv.uiRefreshTime;
                this.dataMode.myPlayer.infoPK.infoShop.timesReset = recv.iCntRefresh;

                for( int index = 0; index < recv.m_vctShopGoodsp.Length; index++ )
                {
                    InfoShopObject shopObj = new InfoShopObject();
                    shopObj.idCsvShop = ( int ) recv.m_vctShopGoodsp[ index ].uiIdCsvGoods;
                    shopObj.isSell = ( recv.m_vctShopGoodsp[ index ].cIsBuy == 1 );
                    shopObj.index = index;
                    this.dataMode.myPlayer.infoPK.infoShop.sells.Add( shopObj );
                }
            }
            else
            {
                Logger.Error( this._account + " PK商店刷新返回失败 " + recv.iResult );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 竞技 商店 购买
        public void recvPKShopBuy( ArgsEvent args )
        {
            RM2C_PK_SHOP_BUY recv = args.getData<RM2C_PK_SHOP_BUY>();
            if( recv.iResult == 1 )
            {
                Logger.Info(this._account + "竞技商店购买成功返回 " + recv.iLoc );
            }
            else
            {
                Logger.Error( this._account + "竞技商店购买失败 " + recv.iResult );    
            }
            

            //TODO ...

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }


        /// 爵位商店 刷新 0系统定时自动刷新，1爵位币刷新
        public void sendNobilityShopReset( byte sType, FunctionListenerEvent sListener )
        {
            Logger.Info( this._account + " 爵位商店刷新" );
            C2RM_REFRESH_NOBILITY_SHOP sender = new C2RM_REFRESH_NOBILITY_SHOP();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            sender.cType = sType;
            this.send( sender );
        }

        /// 爵位商店购买
        public void sendNobilityShopBuy( int sIndex, FunctionListenerEvent sListener )
        {
            Logger.Info( this._account + " 爵位商店购买 " + sIndex );
            C2RM_NOBILITY_SHOP_BUY sender = new C2RM_NOBILITY_SHOP_BUY();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            sender.iLoc = sIndex;
            this.send( sender );
        }

        /// 爵位商店 刷新 0系统定时自动刷新，1钻石刷新
        public void recvNobilityShopReset( ArgsEvent args )
        {
            RM2C_REFRESH_NOBILITY_SHOP recv = args.getData<RM2C_REFRESH_NOBILITY_SHOP>();

            if( 1 == recv.iResult )
            {
                Logger.Info( this._account + "爵位商店刷新成功回应" );
                

                for( int index = 0; index < recv.m_vctShopGoodsp.Length; index++ )
                {
                    InfoShopObject shopObj = new InfoShopObject();
                    shopObj.idCsvShop = ( int ) recv.m_vctShopGoodsp[ index ].uiIdCsvGoods;
                    shopObj.isSell = ( recv.m_vctShopGoodsp[ index ].cIsBuy == 1 );
                    shopObj.index = index;
                }
            }
            else
            {
                Logger.Error( this._account + " 爵位商店刷新返回失败 " + recv.iResult );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 爵位商店购买回应
        public void recvNobilityShopBuy( ArgsEvent args )
        {
            RM2C_NOBILITY_SHOP_BUY recv = args.getData<RM2C_NOBILITY_SHOP_BUY>();
            if( recv.iResult == 1 )
            {
                Logger.Info( this._account + "爵位商店购买成功返回 " + recv.iLoc );
            }
            else
            {
                Logger.Error( this._account + "爵位商店购买失败 " + recv.iResult );
            }


            //TODO ...

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }



        ///获取海加尔山商店信息
        public void sendGetEDShop( FunctionListenerEvent listener )
        {
            Logger.Info(this._account + " 远征商店信息获取" );
            C2RM_GET_MOUNTAIN_SHOP sender = new C2RM_GET_MOUNTAIN_SHOP();
            sender.uiListen = Dispatcher.addListener( listener, null);
            this.send( sender );
        }

        ///刷新海加尔山商店
        public void sendRefreshEDShop( int type, FunctionListenerEvent listener )
        {
            Logger.Info( this._account + " 刷新远征商店" );
            C2RM_REFRESH_MOUNTAIN_SHOP sender = new C2RM_REFRESH_MOUNTAIN_SHOP();
            sender.cType = ( byte ) type;
            sender.uiListen = Dispatcher.addListener( listener, null);
            this.send( sender );
        }

        ///海加尔山商店购买
        public void sendBuyEDShop( int index, FunctionListenerEvent listener )
        {
            Logger.Info(this._account + " 购买远征商店 " + index );
            C2RM_MOUNTAIN_SHOP_BUY sender = new C2RM_MOUNTAIN_SHOP_BUY();
            sender.iLoc = index;
            sender.uiListen = Dispatcher.addListener( listener, null);
            this.send( sender );
        }

        //获取海加尔山商店回复
        public void recvReplyEDShop( ArgsEvent args )
        {
            RM2C_GET_MOUNTAIN_SHOP recv = args.getData<RM2C_GET_MOUNTAIN_SHOP>();
            if( recv.iResult == 1 )
            {
                Logger.Info( this._account + " 远征商店获取成功" );
                this.dataMode.myPlayer.infoEDShop.timesReset = recv.iCntRefresh;
                //this.dataMode.myPlayer.infoEDShop.infoCD.timeTeamp = ( double ) recv.uiRefreshTime;
                int count = recv.m_vctShopGoodsp.Length;
                this.dataMode.myPlayer.infoEDShop.sells.Clear();
                for( int i = 0; i < count; i++ )
                {
                    InfoShopObject obj = new InfoShopObject();
                    obj.index = i;
                    obj.idCsvShop = ( int ) recv.m_vctShopGoodsp[ i ].uiIdCsvGoods;
                    if( recv.m_vctShopGoodsp[ i ].cIsBuy == 0 )
                    {
                        obj.isSell = false;
                    }
                    else
                    {
                        obj.isSell = true;
                    }
                    this.dataMode.myPlayer.infoEDShop.sells.Add( obj );
                }

            }
            else
            {
                Logger.Error(this._account + " 远征商店获取失败 " + recv.iResult );
  
            }

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        ///刷新海加尔山商店
        public void recvUpdateEDShop( ArgsEvent args )
        {
            RM2C_REFRESH_MOUNTAIN_SHOP recv = args.getData<RM2C_REFRESH_MOUNTAIN_SHOP>();
            if( recv.iResult == 1 )
            {
                Logger.Info( this._account + " 刷新远征商店成功");
                this.dataMode.myPlayer.infoEDShop.timesReset = recv.iCntRefresh;
                //this.dataMode.myPlayer.infoEDShop.infoCD.timeTeamp = ( double ) recv.uiRefreshTime;

                //这里面需要确认是否+=
                //this.dataMode.myPlayer.infoPoint.moneyTBC += recv.iCost;

                int count = recv.m_vctShopGoodsp.Length;
                this.dataMode.myPlayer.infoEDShop.sells.Clear();
                for( int i = 0; i < count; i++ )
                {
                    InfoShopObject obj = new InfoShopObject();
                    obj.index = i;
                    obj.idCsvShop = ( int ) recv.m_vctShopGoodsp[ i ].uiIdCsvGoods;
                    if( recv.m_vctShopGoodsp[ i ].cIsBuy == 0 )
                    {
                        obj.isSell = false;
                    }
                    else
                    {
                        obj.isSell = true;
                    }
                    this.dataMode.myPlayer.infoEDShop.sells.Add( obj );
                }
                
            }
            else
            {
                Logger.Error( this._account + " 刷新远征商店失败 " + recv.iResult );

            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        ///海加尔山商店购买回复
        public void recvBuyEDShop( ArgsEvent args )
        {
            RM2C_MOUNTAIN_SHOP_BUY recv = args.getData<RM2C_MOUNTAIN_SHOP_BUY>();
            if( recv.iResult == 1 )
            {
                Logger.Info( this._account + " 远征商店刷新成功" );
            }
            else
            {
                Logger.Error( this._account + " 远征商店刷新失败 " + recv.iResult );    
            }

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        ///获取背包宠物信息
        public void sendHeroUpdateBag( FunctionListenerEvent sListener )
        {
            C2RM_PET_INFO_BAG sender = new C2RM_PET_INFO_BAG();
            sender.uiListen = Dispatcher.addListener( sListener, null);
            this.send( sender );
        }

        ///发送背包宠物信息
        public void recvHeroBag( ArgsEvent args )
        {
            RM2C_PET_INFO_BAG recv = args.getData<RM2C_PET_INFO_BAG>();
            /// 返回数据1
            if( recv.iResult == 1 )
            {
                InfoPlayer player = this.dataMode.getPlayer( recv.uiMasterId );
                if( recv.cIsBegin == 1 )
                    player.infoHeroList.clear();

                for( int index = 0; index < recv.vctSPetInfo.Length; index++ )
                {
                    if( recv.vctSPetInfo[ index ].uiIdPet == 0 )
                        continue;
                    
                    /// 创建卡片
                    if( null == this.dataMode.getHero( recv.vctSPetInfo[ index ].uiIdPet ) )
                        this.dataMode._serverHero.Add( recv.vctSPetInfo[ index ].uiIdPet, new InfoHero() );

                    player.infoHeroList.addHero( recv.vctSPetInfo[ index ].uiIdPet );
                    /// 设计角色基本信息
                    InfoHero hero = this.dataMode.getHero( recv.vctSPetInfo[ index ].uiIdPet );

                    hero.exp = recv.vctSPetInfo[ index ].luiExp;
                    hero.idCsv = ( int ) recv.vctSPetInfo[ index ].uiIdCsvPet;
                    hero.idServer = recv.vctSPetInfo[ index ].uiIdPet;
                    //				hero.addNumber = recv.vctSPetInfo[index].sAddNum;
                    hero.isProtected = recv.vctSPetInfo[ index ].cIsProtect == 0 ? false : true;
                    hero.star = ( int ) recv.vctSPetInfo[ index ].uiStar;
                    hero.infoStone.setStone( ( ulong ) recv.vctSPetInfo[ index ].uiStone );

                    TypeCsvHero csvHero = ManagerCsv.getHero( hero.idCsv );
                    /// 设置技能信息
                    hero.infoSkill.clear();
                    if( csvHero == null )
                    {
                        Logger.Error( "hero not exist in csv hero id = " + hero.idCsv );
                    }
                    if( null != csvHero.idSkill )
                    {
                        for( int i = 0; i < csvHero.idSkill.Length; i++ )
                        {
                            InfoSkill infoSkill = new InfoSkill();
                            uint sid = recv.vctSPetInfo[ index ].vctUiIdCsvSkill[ i ];
                            if( sid == 0 )
                            {
                                infoSkill.idCsv = (int)float.Parse( csvHero.idSkill[ i ] );
                            }
                            else
                            {
                                infoSkill.idCsv = ( int ) sid;
                                infoSkill.isActive = true;
                            }
                            hero.infoSkill.skillSort.Add( infoSkill.idCsv );
                            hero.infoSkill.setInfoSkill( infoSkill );
                        }
                    }
                    /// 设置释放的技能
                    hero.infoSkill.setSkillRelease( ( int ) recv.vctSPetInfo[ index ].uiIdCsvHandSkill );

                    hero.infoEquip.clear();
                    for( int i = 0; i < recv.vctSPetInfo[ index ].vctLuiIdEquip.Length; i++ )
                    {
                        hero.infoEquip.setProp( i, ( int ) recv.vctSPetInfo[ index ].vctLuiIdEquip[ i ] );
                    }
                }
            }
            else
            {
                Logger.Error(this._account + " RM2C_PET_INFO_BAG error " + recv.iResult );
            }

            if( recv.cIsOver == 1 )  //本命令完成
            {
                Dispatcher.dispatchListener( recv.uiListen, recv );    
            }
            
        }

        /// 升级星级
        public void sendPetStarUp( ulong serverId, FunctionListenerEvent sListener )
        {
            Logger.Info(this._account +  " SEND : C2RM_PET_STAR_UP >> " + serverId );

            C2RM_PET_STAR_UP sender = new C2RM_PET_STAR_UP();
            sender.uiPetId = serverId;
            sender.uiListen = Dispatcher.addListener( sListener, null);
            this.send( sender );

        }

        /// 升星
        public void recvPetStarUp( ArgsEvent args )
        {
            RM2C_PET_STAR_UP recv = args.getData<RM2C_PET_STAR_UP>();

            if( recv.iResult == 1 )
            {
                Logger.Info(this._account + " RM2C_PET_STAR_UP" );
            }
            else
            {
                Logger.Error( this._account + " RM2C_PET_STAR_UP >> iResult = " + recv.iResult );   
            }
            
            //TODO ...

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 碎片 信息更新
        public void sendHeroChipUpdate( FunctionListenerEvent sListener )
        {
            C2RM_PIECE sender = new C2RM_PIECE();
            Logger.Info(this._account + " SEND:C2RM_PIECE >> " + "碎片 信息更新" );
            sender.uiListen = Dispatcher.addListener( sListener, null);
            this.send( sender );
        }

        /// 碎片 信息更新
        public void recvHeroChipUpdate( ArgsEvent args )
        {
            RM2C_PIECE recv = args.getData<RM2C_PIECE>();

            Logger.Info(this._account + "  RECV:RM2C_PIECE >> " + "碎片 信息更新" );

            this.dataMode.infoHeroChip.Clear();

            /// 更新我的背包信息
            for( int i = 0; i < recv.sPiece.Length; i++ )
            {
                this.dataMode.infoHeroChip.setHeroChip( ( int ) recv.sPiece[ i ].uiCsvId, recv.sPiece[ i ].iCnt );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 合成卡牌
        public void sendPetChipToPet( int sameId, FunctionListenerEvent sListener )
        {
            Logger.Info(this._account +  "卡牌合成 sameId: " + sameId);
            C2RM_PET_PIECE_TO_PET sender = new C2RM_PET_PIECE_TO_PET();
            sender.uiCsvId = ( uint ) sameId;
            sender.uiListen = Dispatcher.addListener( sListener, null);
            this.send( sender );
        }

        /// 合成卡牌
        public void recvPetChipToPet( ArgsEvent args )
        {
            RM2C_PET_PIECE_TO_PET recv = args.getData<RM2C_PET_PIECE_TO_PET>();
            

            if( recv.iResult == 1 )
            {
                Logger.Info(this._account +  "卡牌合成成功 " + recv.sPetInfo.uiIdCsvPet);
                this.dataMode.myPlayer.money += ( long ) recv.sMoney.iQMoney;
                this.dataMode.myPlayer.money_game += ( long ) recv.sMoney.iSMoney;

                /// 创建出来卡牌信息
                if( !this.dataMode._serverHero.ContainsKey( recv.sPetInfo.uiIdPet ) )
                    this.dataMode._serverHero.Add( recv.sPetInfo.uiIdPet, new InfoHero() );

                InfoHero hero = this.dataMode.getHero( recv.sPetInfo.uiIdPet );
                hero.exp = recv.sPetInfo.luiExp;
                hero.idServer = recv.sPetInfo.uiIdPet;
                hero.idCsv = ( int ) recv.sPetInfo.uiIdCsvPet;
                hero.star = ( int ) recv.sPetInfo.uiStar;
                hero.infoStone.setStone( ( ulong ) recv.sPetInfo.uiStone );

                this.dataMode.infoHeroChip.setHeroChip( ( int ) recv.sPiece.uiCsvId, recv.sPiece.iCnt );

                TypeCsvHero csvHero = ManagerCsv.getHero( hero.idCsv );
                /// 设置技能信息
                /*
                hero.infoSkill.clear();
                if( csvHero == null )
                    Logger.Error( "hero not exist in csv hero id = " + hero.idCsv );

                if( null != csvHero.idSkill )
                {
                    for( int i = 0; i < csvHero.idSkill.Length; i++ )
                    {
                        InfoSkill infoSkill = new InfoSkill();
                        if( recv.vctSPetInfo[ index ].vctUiIdCsvSkill[ i ] == 0 )
                        {
                            infoSkill.idCsv = ( int ) float.Parse( csvHero.idSkill[ i ] );
                        }
                        if( recv.vctSPetInfo[ index ].vctUiIdCsvSkill[ i ] != 0 )
                        {
                            infoSkill.idCsv = ( int ) recv.vctSPetInfo[ index ].vctUiIdCsvSkill[ i ];
                            infoSkill.isActive = true;
                        }
                        hero.infoSkill.skillSort.Add( infoSkill.idCsv );
                        hero.infoSkill.setInfoSkill( infoSkill );
                    }
                }
                /// 设置释放的技能
                hero.infoSkill.setSkillRelease( ( int ) recv.vctSPetInfo[ index ].uiIdCsvHandSkill );
                 */ 
                hero.infoEquip.clear();
                for( int i = 0; i < recv.sPetInfo.vctLuiIdEquip.Length; i++ )
                {
                    hero.infoEquip.setProp( i, ( int ) recv.sPetInfo.vctLuiIdEquip[ i ] );
                }

                /// 保存卡牌
                this.dataMode.myPlayer.infoHeroList.addHero( hero.idServer );
            }
            else
            {
                Logger.Error(this._account +  "卡牌合成失败 " + recv.iResult );
            }

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        ///获取装备
        public void sendEquipUpdate( FunctionListenerEvent sListener )
        {
            C2RM_EQUIPMENT sender = new C2RM_EQUIPMENT();
            sender.uiListen = Dispatcher.addListener( sListener, null);
            this.send( sender );
        }

        ///Response 装备信息
        public void recvHeroEquip( ArgsEvent args )
        {
            
            RM2C_EQUIPMENT recv = args.getData<RM2C_EQUIPMENT>();
            if( recv.iResult == 1 )
            {
                Logger.Info( this._account + " 获取装备信息成功" );
                InfoPlayer player = this.dataMode.getPlayer( recv.uiMasterId );
                if( recv.cIsBegin != 0 )
                {
                    player.infoPropList.clear();
                    //player.infoPropBeastList.clear();
                }

                //bool b = myPlayer.idServer == player.idServer ? true : false;
                TypeCsvProp csvprop = null;
                for( int i = 0; i < recv.vctSEquipment.Length; i++ )
                {
                    if( recv.vctSEquipment[ i ].uiIdCsvEquipment <= 0 )
                    {
                        Logger.Error(this._account + " 无意义的物品" );
                        continue;
                    }
                    csvprop = ManagerCsv.getProp( ( int ) recv.vctSEquipment[ i ].uiIdCsvEquipment );
                    if( csvprop == null )
                        continue;

                    if( csvprop.isPropBeast() )
                    {
                        //player.infoPropBeastList.changeProp( ( int ) recv.vctSEquipment[ i ].uiIdCsvEquipment, recv.vctSEquipment[ i ].iCnt );
                    }
                    else
                    {
                        player.infoPropList.changeProp( ( int ) recv.vctSEquipment[ i ].uiIdCsvEquipment, recv.vctSEquipment[ i ].iCnt );
                    }
                }
            }
            else
            {
                Logger.Error( this._account + " 获取装备信息失败 " + recv.iResult);
            }
            if( recv.cIsOver == 1 )
            {
                Dispatcher.dispatchListener( recv.uiListen, recv );
            }
            
        }

        /// 升级卡牌 吃药
        public void sendPetLvUp( ulong serverId, int csvPropId, int propCnt, FunctionListenerEvent sListener )
        {
            Logger.Info(this._account + " SEND : C2RM_PET_LV_UP >> " + serverId );
            C2RM_PET_LV_UP sender = new C2RM_PET_LV_UP();
            sender.uiPetId = serverId;
            sender.uiPropCsvId = ( uint ) csvPropId;
            sender.uiCnt = ( uint ) propCnt;
            sender.uiListen = Dispatcher.addListener( sListener, null);
            this.send( sender );
        }

        ///卡牌升级
        public void recvPetLvUp_New( ArgsEvent args )
        {
            RM2C_PET_LV_UP recv = args.getData<RM2C_PET_LV_UP>();
            if( recv.iResult == 1 )
            {
                if( null == this.dataMode.getHero( recv.sPetEat.uiPetId ) )
                    this.dataMode._serverHero.Add( recv.sPetEat.uiPetId, new InfoHero() );

                this.dataMode.myPlayer.infoHeroList.addHero( recv.sPetEat.uiPetId );
                /// 设计角色基本信息
                InfoHero hero = this.dataMode.getHero( recv.sPetEat.uiPetId );
                hero.exp = recv.sPetEat.uiExp;
                hero.idCsv = ( int ) recv.sPetEat.uiCsvId;
                hero.idServer = recv.sPetEat.uiPetId;

                if( recv.sProp.uiIdCsvEquipment > 0 )
                {
                    TypeCsvProp csvprop = ManagerCsv.getProp( ( int ) recv.sProp.uiIdCsvEquipment );
                    if( csvprop.isPropBeast() )
                    {
                        //this.dataMode.myPlayer.infoPropBeastList.changeProp( ( int ) recv.sProp.uiIdCsvEquipment, recv.sProp.iCnt );
                    }
                    else
                    {
                        this.dataMode.myPlayer.infoPropList.changeProp( ( int ) recv.sProp.uiIdCsvEquipment, recv.sProp.iCnt );
                    }
                }

                TypeCsvHeroLv csvherolv = ManagerCsv.getHeroLv( this.dataMode.myPlayer.lv );

                Logger.Info( this._account + " 卡牌升级成功 " );
            }
            else
            {
                Logger.Error( this._account + " 卡牌升级失败 " + recv.iResult );
            }

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 增加体力
        public void sendPowerAdd( FunctionListenerEvent sListener )
        {
            Logger.Info(this._account + "SEND:C2RM_POWER_ADD >> 体力 增加" );
            C2RM_POWER_ADD sender = new C2RM_POWER_ADD();
            sender.uiListen = Dispatcher.addListener( sListener,null);
            this.send( sender );
        }

        /// 体力购买
        public void sendPwoerBuy( FunctionListenerEvent sListener )
        {
            Logger.Info(this._account + " 体力购买" );
            C2RM_POWER_BUY sender = new C2RM_POWER_BUY();
            sender.uiListen = Dispatcher.addListener( sListener,null);
            this.send( sender );
        }

        /// 购买体力回调
        public void recvPowerBuy( ArgsEvent args )
        {
            RM2C_POWER_BUY recv = args.getData<RM2C_POWER_BUY>();
            if( recv.iResult != 1 )
            {
                Logger.Info( this._account + " 体力购买成功 ");
            }
            else
            {
                Logger.Error( this._account + " 体力购买失败 " + recv.iResult );    
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 角色 装备穿上
        public void sendHeroEquipChange( ulong idServerHero, int[] idCsvEquip, FunctionListenerEvent sListener )
        {
            C2RM_EQUIP_PET_NOTIFY sender = new C2RM_EQUIP_PET_NOTIFY();

            string csvEq = "";
            for( int i = 0; i < idCsvEquip.Length; i++ )
            {
                csvEq  += idCsvEquip[ i ] + " ";
            }

            Logger.Info(this._account + " 装备变更  " + idServerHero + " - " + csvEq );

            uint[] idCsvEquipUnit = new uint[ idCsvEquip.Length ];
            for( int i = 0; i < idCsvEquip.Length; i++ )
            {
                idCsvEquipUnit[ i ] = ( uint ) idCsvEquip[ i ];
            }
            sender.sPetEquip.uiPetId = idServerHero;
            sender.sPetEquip.uiIdEquip = idCsvEquipUnit;
            sender.uiListen = Dispatcher.addListener( sListener,null);
            this.send( sender );
        }

        /// 角色 装备穿上
        public void recvHeroEquipChange( ArgsEvent args )
        {
            RM2C_EQUIP_PET_NOTIFY recv = args.getData<RM2C_EQUIP_PET_NOTIFY>();

            if( recv.iResult == 1 )
            {
                /// 装备信息剖析
                InfoHero infoHero = this.dataMode.getHero( recv.sPetEquip.uiPetId );
                infoHero.infoEquip.clear();
                for( int i = 0; i < recv.sPetEquip.uiIdEquip.Length; i++ )
                {
                    infoHero.infoEquip.setProp( i, ( int ) recv.sPetEquip.uiIdEquip[ i ] );
                }
                /// 更新我的背包信息
                TypeCsvProp csvprop = null;
                for( int i = 0; i < recv.sEquip.Length; i++ )
                {
                    if( recv.sEquip[ i ].uiIdCsvEquipment <= 0 )
                    {
                        Logger.Error( "无意义的物品" );
                        continue;
                    }
                    csvprop = ManagerCsv.getProp( ( int ) recv.sEquip[ i ].uiIdCsvEquipment );
                    if( csvprop.isPropBeast() )
                    {
                        //this.dataMode.myPlayer.infoPropBeastList.changeProp( ( int ) recv.sEquip[ i ].uiIdCsvEquipment, recv.sEquip[ i ].iCnt );
                    }
                    else
                    {
                        this.dataMode.myPlayer.infoPropList.changeProp( ( int ) recv.sEquip[ i ].uiIdCsvEquipment, recv.sEquip[ i ].iCnt );
                    }
                }
                Logger.Info(this._account + " 装备变更消息回应成功 " + recv.sPetEquip.uiPetId);
            }
            else
            {
                Logger.Error( this._account + " 装备变更消息回应失败 " + recv.iResult );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        public void recvPowerAdd( ArgsEvent args )
        {
            RM2C_POWER_ADD recv = args.getData<RM2C_POWER_ADD>();
            Logger.Info( "RECV:RM2C_POWER_ADD >> " + recv.iResult + "体力 增加回复" );

            if( recv.iResult == 1 )
            {
                /// 下一次获得体力的时间戳子
                //this.dataMode.myPlayer.powerCD.timeTeamp = ( double ) recv.sLeadPowerInfo.uiPowerLessTime + ManagerCsv.getAttribute().powerAddTime + 1f;
                this.dataMode.myPlayer.power = recv.sLeadPowerInfo.usPower;
            }
            else
            {
                Logger.Error( this._account + " 体力增加失败 " + recv.iResult );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 镶嵌
        public void sendStoneInLay( ulong petServerId, int index, FunctionListenerEvent sListener )
        {
            Logger.Info(this._account + " SEND : C2RM_STONE_INLAY >>  petServerId: " + petServerId + " index: " + index);
            C2RM_STONE_INLAY sender = new C2RM_STONE_INLAY();
            sender.luiIdPet = petServerId;
            sender.usLoc = ( ushort ) index;
            sender.uiListen = Dispatcher.addListener( sListener,null);
            this.send( sender );
        }

        /// 进阶
        public void sendPetStoneUp( ulong petServerId, FunctionListenerEvent sListener )
        {
            Logger.Info(this._account + " SEND : C2RM_PET_STONE_UP >> petServerId:" + petServerId );
            C2RM_PET_STONE_UP sender = new C2RM_PET_STONE_UP();
            sender.luiIdPet = petServerId;
            sender.uiListen = Dispatcher.addListener( sListener,null);
            this.send( sender );
        }

        /// 进阶回应
        public void recvPetStoneUp( ArgsEvent args )
        {
            RM2C_PET_STONE_UP recv = args.getData<RM2C_PET_STONE_UP>();
            
            if( recv.iResult == 1 )
            {
                InfoHero hero = this.dataMode.getHero( recv.luiIdPet );
                hero.idCsv = ( int ) recv.uiIdCsvPet;
                hero.infoStone.resetStone();
                this.dataMode.myPlayer.money += ( long ) recv.SCostMoney.iQMoney;
                this.dataMode.myPlayer.money_game += ( long ) recv.SCostMoney.iSMoney;

                Logger.Info( this._account + " 进阶回应成功  luiIdPet:" + recv.luiIdPet );
            }
            else
            {
                Logger.Error( this._account + " 进阶回应失败 " + recv.iResult );
            }

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 镶嵌回应
        public void recvPetStoneInLay( ArgsEvent args )
        {
            RM2C_STONE_INLAY recv = args.getData<RM2C_STONE_INLAY>();
            
            if( recv.iResult == 1 )
            {
                InfoHero hero = this.dataMode.getHero( recv.luiIdPet );
                hero.infoStone.setStone( recv.uiStoneInfo );

                this.dataMode.myPlayer.infoPropList.changeProp( ( int ) recv.SCostStone.uiId, recv.SCostStone.iCnt );
                this.dataMode.myPlayer.money += ( long ) recv.SCostMoney.iQMoney;
                this.dataMode.myPlayer.money_game += ( long ) recv.SCostMoney.iSMoney;

                Logger.Info(this._account +  " 进阶石镶嵌成功");
            }
            else
            {
                Logger.Error( this._account + " 进阶石镶嵌失败 " + recv.iResult );
            }

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 装备强化
        public void sendEquipUp( int location, ulong petId, FunctionListenerEvent sListener )
        {
            Logger.Info( this._account + " 装备强化 " + petId + " " + location );

            C2RM_EQUIP_UP sender = new C2RM_EQUIP_UP();
            sender.iLoc = location;
            sender.luiIdPet = petId;
            sender.uiListen = Dispatcher.addListener( sListener,null);
            this.send( sender );
        }
        /// 装备强化
        public void recvEquipUp( ArgsEvent args )
        {
            RM2C_EQUIP_UP recv = args.getData<RM2C_EQUIP_UP>();

            if( recv.iResult == 1 )
            {
                Logger.Info( this._account + " 装备强化回应成功  " + recv.uiIdCsvEqu);
                TypeCsvProp csvprop = null;
                foreach( SCostInfo info in recv.vctCostInfo )
                {
                    if( info.cType == 0 )
                    {
                        if( info.uiId <= 0 )
                        {
                            Logger.Error( "无意义的物品" );
                            continue;
                        }
                        csvprop = ManagerCsv.getProp( ( int ) info.uiId );
                        if( csvprop.isPropBeast() )
                        {
                            //this.dataMode.myPlayer.infoPropBeastList.changeProp( ( int ) info.uiId, info.iCnt );
                        }
                        else
                        {
                            this.dataMode.myPlayer.infoPropList.changeProp( ( int ) info.uiId, info.iCnt );
                        }
                    }
                }

                InfoHero hero = this.dataMode.getHero( recv.luiIdPet );
                hero.infoEquip.setProp( recv.iLoc, ( int ) recv.uiIdCsvEqu );

                this.dataMode.myPlayer.money += ( long ) recv.SCostMoney.iQMoney;
                this.dataMode.myPlayer.money_game += ( long ) recv.SCostMoney.iSMoney;
            }
            else
            {
                Logger.Error( this._account + " 装备强化回应失败  " + recv.iResult );    
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 一键强化
        public void sendEquipUpAll( int location, ulong petId, FunctionListenerEvent sListener )
        {
            Logger.Info( this._account + " 一键强化装备 " + petId + " " + location );

            C2RM_EQUIP_UP_ONE_KEY sender = new C2RM_EQUIP_UP_ONE_KEY();
            sender.iLoc = location;
            sender.luiIdPet = petId;
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.send( sender );
        }

        /// 一键强化
        public void recvEquipUpAll( ArgsEvent args )
        {
            RM2C_EQUIP_UP_ONE_KEY recv = args.getData<RM2C_EQUIP_UP_ONE_KEY>();

            if( recv.iResult == 1 )
            {
                Logger.Info( this._account + " 装备一键强化回应成功  " + recv.uiIdCsvEqu );
                TypeCsvProp csvprop = null;
                foreach( SCostInfo info in recv.vctCostInfo )
                {
                    if( info.cType == 0 )
                    {
                        if( info.uiId <= 0 )
                        {
                            Logger.Info( "无意义的物品" );
                            continue;
                        }
                        csvprop = ManagerCsv.getProp( ( int ) info.uiId );
                        if( csvprop.isPropBeast() )
                        {
                            //this.dataMode.myPlayer.infoPropBeastList.changeProp( ( int ) info.uiId, info.iCnt );
                        }
                        else
                        {
                            this.dataMode.myPlayer.infoPropList.changeProp( ( int ) info.uiId, info.iCnt );
                        }
                    }
                }

                InfoHero hero = this.dataMode.getHero( recv.luiIdPet );
                hero.infoEquip.setProp( recv.iLoc, ( int ) recv.uiIdCsvEqu );

                this.dataMode.myPlayer.money += ( long ) recv.SCostMoney.iQMoney;
                this.dataMode.myPlayer.money_game += ( long ) recv.SCostMoney.iSMoney;
            }
            else
            {
                Logger.Error( this._account + " 装备一键强化回应失败  " + recv.iResult );    
            }

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 装备合成
        public void sendEquipCreat( uint csvProp, FunctionListenerEvent sListener )
        {
            Logger.Info(this._account +  " 合成装备 csvProp: " + csvProp);
            C2RM_EQUIP_COM sender = new C2RM_EQUIP_COM();
            sender.uiIdGoods = csvProp;
            sender.uiListen = Dispatcher.addListener( sListener,null);
            this.send( sender );
        }

        /// 合成装备
        public void recvEquipCreat( ArgsEvent args )
        {
            RM2C_EQUIP_COM recv = args.getData<RM2C_EQUIP_COM>();
            
            if( recv.iResult == 1 )
            {
                Logger.Info(this._account + " 装备合成回应成功 " + recv.SEqu.luiIdEquipment);
                TypeCsvProp csvprop = null;
                for( int i = 0; i < recv.vctCostInfo.Length; i++ )
                {
                    if( recv.vctCostInfo[ i ].cType == 0 )
                    {
                        if( recv.vctCostInfo[ i ].uiId <= 0 )
                        {
                            Logger.Error("无意义的物品" );
                            continue;
                        }
                        csvprop = ManagerCsv.getProp( ( int ) recv.vctCostInfo[ i ].uiId );
                        if( csvprop.isPropBeast() )
                        {
                            //this.dataMode.myPlayer.infoPropBeastList.changeProp( ( int ) recv.vctCostInfo[ i ].uiId, recv.vctCostInfo[ i ].iCnt );
                        }
                        else
                        {
                            this.dataMode.myPlayer.infoPropList.changeProp( ( int ) recv.vctCostInfo[ i ].uiId, recv.vctCostInfo[ i ].iCnt );
                        }
                    }
                }
                if( recv.SEqu.uiIdCsvEquipment > 0 )
                {
                    csvprop = ManagerCsv.getProp( ( int ) recv.SEqu.uiIdCsvEquipment );
                    if( csvprop.isPropBeast() )
                    {
                        //this.dataMode.myPlayer.infoPropBeastList.changeProp( ( int ) recv.SEqu.uiIdCsvEquipment, recv.SEqu.iCnt );
                    }
                    else
                    {
                        this.dataMode.myPlayer.infoPropList.changeProp( ( int ) recv.SEqu.uiIdCsvEquipment, recv.SEqu.iCnt );
                    }
                }

                this.dataMode.myPlayer.money += ( long ) recv.SCostMoney.iQMoney;
                this.dataMode.myPlayer.money_game += ( long ) recv.SCostMoney.iSMoney;
            }
            else
            {
                Logger.Error( this._account + " 装备合成回应失败 " + recv.iResult );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        //请求升级卡牌技能
        public void sendPetSkillUp( UInt64 _uiPetId, UInt32 _uiCsvSkillId, List<int> _consumeList, FunctionListenerEvent _sListener )
        {
            Logger.Info( this._account + " 请求升级卡牌技能  PetId:" + _uiPetId );
            
            C2RM_SKILL_UP sender = new C2RM_SKILL_UP();
            sender.iCnt = 1;
            sender.uiListen = Dispatcher.addListener( _sListener,null);
            sender.uiPetId = _uiPetId;
            sender.uiCsvSkillId = _uiCsvSkillId;

            this.send( sender );
        }

        ///学习技能
        public void sendSkillUpNew( ulong serverId, int skillId, FunctionListenerEvent sListener )
        {
            Logger.Info( this._account + " 请求学习技能  serverId:" + serverId + " skillId:" + skillId );
            C2RM_SKILL_UP_NEW sender = new C2RM_SKILL_UP_NEW();
            sender.uiPetId = serverId;
            sender.uiCsvSkillId = ( uint ) skillId;
            sender.uiListen = Dispatcher.addListener( sListener,null);
            this.send( sender );
        }

        /// 卡牌技能升级回应
        public void recvSkillUp_New( ArgsEvent args )
        {
            RM2C_SKILL_UP_NEW recv = args.getData<RM2C_SKILL_UP_NEW>();
            if( recv.iResult == 1 )
            {
                Logger.Info( this._account + " 技能升级成功  PetId: " + recv.sKillUp.uiPetId + "   " + recv.sKillUp.uiOldSkillId + " -> " + recv.sKillUp.uiNewSkillId );
                this.dataMode.myPlayer.skillPoint = ( ( this.dataMode.myPlayer.skillPoint - 1 <= 0 ) ? 0 : this.dataMode.myPlayer.skillPoint -= 1 );

                this.dataMode.myPlayer.money_game += ( long ) recv.sMoney.iSMoney;
                InfoHero _hero = this.dataMode.getHero( recv.sKillUp.uiPetId );
                _hero.infoSkill.setSkillRelease( ( int ) recv.sKillUp.uiHandSkillId );

                _hero.infoSkill.setSkillChange( ( int ) recv.sKillUp.uiOldSkillId, ( int ) recv.sKillUp.uiNewSkillId );
            }
            else
            {
                Logger.Error( this._account + " 技能升级回应失败 " + recv.iResult );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        public void recvTickByOther( ArgsEvent args )
        {
            RM2C_TICK_BY_OTHER recv = args.getData<RM2C_TICK_BY_OTHER>();

            Logger.Error( this._account + " 被踢  reson: " + recv.iReason );
        }


        /// 爵位 商店
        public void sendGetNobilityShop( FunctionListenerEvent sListener )
        {
            Logger.Info(this._account + " SEND:C2RM_GET_NOBILITY_SHOP >> 爵位 商店获取" );
            C2RM_GET_NOBILITY_SHOP sender = new C2RM_GET_NOBILITY_SHOP();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.send( sender );
        }

        /// 爵位商店回应
        public void recvGetNobilityShop( ArgsEvent args )
        {
            RM2C_GET_NOBILITY_SHOP recv = args.getData<RM2C_GET_NOBILITY_SHOP>();

            if( 1 == recv.iResult )
            {
                Logger.Info( this._account + "爵位商店列表成功回应" );
                ///this.dataMode.myPlayer.infoPK.infoShop.sells.Clear();
                ///this.dataMode.myPlayer.infoPK.infoShop.infoCD.timeTeamp = ( double ) recv.uiRefreshTime;
                ///this.dataMode.myPlayer.infoPK.infoShop.timesReset = recv.iCntRefresh;

                for( int index = 0; index < recv.m_vctShopGoodsp.Length; index++ )
                {
                    InfoShopObject shopObj = new InfoShopObject();
                    shopObj.idCsvShop = ( int ) recv.m_vctShopGoodsp[ index ].uiIdCsvGoods;
                    shopObj.isSell = ( recv.m_vctShopGoodsp[ index ].cIsBuy == 1 );
                    shopObj.index = index;
                    ///this.dataMode.myPlayer.infoPK.infoShop.sells.Add( shopObj );
                }
            }
            else
            {
                Logger.Error( this._account + " 爵位商店列表返回失败 " + recv.iResult );
                return;
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        ///魂侠抽UI
        public void sendLuckySoulList( FunctionListenerEvent sListener )
        {
            Logger.Info(this._account + " SEND:C2RM_LUCKY_SOUL_LIST >> 魂侠抽UI" );
            C2RM_LUCKY_SOUL_LIST sender = new C2RM_LUCKY_SOUL_LIST();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.send( sender );
        }

        ///魂侠抽UI回应
        public void recvLuckySoulList( ArgsEvent args )
        {
            RM2C_LUCKY_SOUL_LIST recv = args.getData<RM2C_LUCKY_SOUL_LIST>();

            if( 1 == recv.iResult )
            {
                Logger.Info( this._account + "魂侠UI成功回应" );

            }
            else
            {
                Logger.Error( this._account + " 魂侠UI返回失败 " + recv.iResult );
                return;
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        //连接魂侠抽测试
        public void LuckySoulStart(int times,  FunctionListenerEvent sListener )
        {
            this.lucky_soul_queue.Clear();

            //Dictionary<UInt32,UInt32> lucky_stat = new Dictionary<UInt32, UInt32>();

            for( int i = 0; i < times; i++ )
            {
                Action action = () =>
                {
                    this.sendLuckySoul( ( UtilListenerEvent e ) =>
                    {
                        RM2C_LUCKY_SOUL recv = (RM2C_LUCKY_SOUL)e.eventArgs;

                        Debug.Assert( recv.uiRewards.Length == 1, "" );

                        Logger.Info(this._account + " 魂侠抽次数：" + recv.uiLuckySoulCnt );
                        Logger.Info(this._account + " 魂侠物品 csvid: " + recv.uiRewards[0].uiIdCsv + " 类型：" + recv.uiRewards[ 0 ].cType );

                        UInt32 csvId = recv.uiRewards[0].uiIdCsv;
                        //if(!lucky_stat.ContainsKey(csvId)) {
                        //    lucky_stat.Add( csvId, 0 );
                        //}

                        //lucky_stat[ csvId ]++;

                        Action next = this.lucky_soul_queue.Dequeue();
                        next();   //execute next task in queue
                    } );
                };

                this.lucky_soul_queue.Enqueue( action ); 
            }

            //购买完成任务
            Action end = () =>
            {
                Logger.Info( this._account + " ======================= 魂侠抽完成!" );

                Logger.Info(this._account + " 概述统计结果： " );
                //foreach(KeyValuePair<UInt32,UInt32> v in lucky_stat)
                //{
                //    Logger.Info( "lucky_stat: " + v.Key + " 次数：" + v.Value );
                //}

                if( sListener != null )
                    sListener(null);
            };
            this.lucky_soul_queue.Enqueue( end );

            Action firstAction = this.lucky_soul_queue.Dequeue();
            firstAction();    
        }

        ///魂侠抽
        public void sendLuckySoul( FunctionListenerEvent sListener )
        {
            //Logger.Info( "SEND:C2RM_LUCKY_SOUL >> 魂侠抽" );
            C2RM_LUCKY_SOUL sender = new C2RM_LUCKY_SOUL();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            sender.m_bIsTimes_10 = 0;  //单抽
            this.send( sender );
        }

        ///魂侠抽回应
        public void recvLuckySoul( ArgsEvent args )
        {
            RM2C_LUCKY_SOUL recv = args.getData<RM2C_LUCKY_SOUL>();

            if( 1 == recv.iResult )
            {
                Logger.Info( this._account + "魂侠成功回应" );

                
            }
            else
            {
                Logger.Error( this._account + " 魂侠返回失败 " + recv.iResult );
                return;
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        //使用道具增加技能点
        void sendPropAddSP( FunctionListenerEvent sListener )
        {
            C2RM_USE_PROP_ADD_SP sender = new C2RM_USE_PROP_ADD_SP();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            sender.propId = 10060;
            this.send( sender );
        }

        ///使用道具增加技能点回应
        public void recvPropAddSP( ArgsEvent args )
        {
            RM2C_USE_PROP_ADD_SP recv = args.getData<RM2C_USE_PROP_ADD_SP>();

            if( 1 == recv.iResult )
            {
                Logger.Info( this._account + "使用道具增加技能点成功回应" );

            }
            else
            {
                Logger.Error( this._account + " 使用道具增加技能点返回失败 " + recv.iResult );
                return;
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }


        //使用vip体验卡
        void sendTempVip( FunctionListenerEvent sListener )
        {
            C2RM_USE_TEMP_VIP sender = new C2RM_USE_TEMP_VIP();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            sender.propId = 10061;
            this.send( sender );
        }

        ///使用vip体验卡回应
        public void recvTempVip( ArgsEvent args )
        {
            RM2C_USE_TEMP_VIP recv = args.getData<RM2C_USE_TEMP_VIP>();

            if( 1 == recv.iResult )
            {
                Logger.Info( this._account + "使用vip体验卡成功回应" );

            }
            else
            {
                Logger.Error( this._account + " 使用vip体验卡返回失败 " + recv.iResult );
                return;
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }
    }
	
}
