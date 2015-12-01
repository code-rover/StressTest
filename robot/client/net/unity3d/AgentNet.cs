using System;
using System.Collections.Generic;
using utils;
using robot.client.common;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using System.Reflection;

namespace net.unity3d
{
    public class AgentNet
    {
        public static readonly string accountServerId = "account_server";

        public workerNet lNetWorker = new workerNet();

        public DataMode dataMode = new DataMode();

        public Transceiver trans = null;

        

        public AgentNet()              
        {
            this.trans = new Transceiver( this );
        }

        public void initClientNoetNew( List<NoteServer> ListNote )
        {
            ManagerNet mgr = ManagerNet.getInstance();
            mgr.init( ListNote );
        }

        public void initAccountNote( List<NoteServer> ListNote )
        {
            ManagerNet mgr = ManagerNet.getInstance();
            mgr.initAccountNote( ListNote );
        }

        public void connectRealmServer()
        {
            try
            {
                _XmanLogic = new UConnection();
                _XmanLogic.agentNet = this;
                _XmanLogic.EventConnected += OnXmanLogicConnected;

                Logger.Info( "logic server: " + m_LogicNote._serverIp + ":" + m_LogicNote._serverProt );
                _XmanLogic.open( m_LogicNote );
            }
            catch( Exception ex )
            {
                Logger.Error( "create realm conn exp = " + ex );

                NodeQueue node = new NodeQueue();
                RM2C_ON_REALM_SERVER_CLOSE pro = new RM2C_ON_REALM_SERVER_CLOSE();
                pro.uiReason = 1;
                ArgsNetEvent largs = new ArgsNetEvent( pro );
                NodeQueue qn = new NodeQueue();
                qn.msg = ( ushort ) RM2C_ON_REALM_SERVER_CLOSE.OPCODE;
                qn.args = largs;
                this.lNetWorker.AddQueue( qn );
                node = this.lNetWorker.tick();
                if( null != node )
                {
                    callEvent( node.msg, node.args );
                }
                close();
            }
        }

        public void OnXmanLogicClosed( object sender, net.ArgsEvent args )
        {
            RM2C_ON_REALM_SERVER_CLOSE pro = new RM2C_ON_REALM_SERVER_CLOSE();
            pro.uiReason = 1;
            ArgsNetEvent largs = new ArgsNetEvent( pro );
            NodeQueue qn = new NodeQueue();
            qn.msg = ( ushort ) RM2C_ON_REALM_SERVER_CLOSE.OPCODE;
            qn.args = largs;
            this.lNetWorker.AddQueue( qn );
        }

        public void OnXmanLogicConnected( object sender, net.ArgsEvent args )
        {
            if( !( bool ) args.Data )
            {
                return;
            }
            UConnection conn = ( UConnection ) sender;
            Logger.Info( "connected : {" + conn.RemoteIP + "}:{" + conn.RemotePort + "}" );

            //Register Protocol Factory
            robot.client.Protocols.logicRegisterProtocol( conn.Session );

            RM2C_ON_REALM_SERVER_CONN pro = new RM2C_ON_REALM_SERVER_CONN();
            pro.uiReason = 1;
            ArgsNetEvent largs = new ArgsNetEvent( pro );
            NodeQueue qn = new NodeQueue();
            qn.msg = ( ushort ) RM2C_ON_REALM_SERVER_CONN.OPCODE;
            qn.args = largs;
            this.lNetWorker.AddQueue( qn );

            sendRMLogin();
        }

        public void sendRMLogin()
        {
            C2RM_LOGIN LogicPro = new C2RM_LOGIN();
            LogicPro.setChannelIdStr( _channelId );
            LogicPro.setAccountIdStr( _accountId );
            LogicPro.sAccountC2AC.setChannelIdStr( _accountId );
            LogicPro.sAccountC2AC.setAccountIdStr( _accountId );
            LogicPro.sAccountC2AC.setAccount( _account );
            LogicPro.sAccountC2AC.setPassword( _password );
            LogicPro.sAccountC2AC.setDeviceInfo( _macId );
            LogicPro.sAccountC2AC.setSession( _webSession );
            LogicPro.sAccountC2AC.serverType = _serverType;
            send( LogicPro );
        }

        public void OnLoginSerConnected( object sender, net.ArgsEvent args )
        {
            if( !( bool ) args.Data )
            {
                return;
            }

            UConnection conn = ( UConnection ) sender;
            Logger.Info( "connected : {" + conn.RemoteIP + "}:{" + conn.RemotePort + "}" );
            robot.client.Protocols.loginSerRegisterProtocol( conn.Session );

            LS2C_ON_LOGIN_SERVER_CONN pro = new LS2C_ON_LOGIN_SERVER_CONN();
            pro.uiReason = 1;
            ArgsNetEvent largs = new ArgsNetEvent( pro );
            NodeQueue qn = new NodeQueue();
            qn.msg = ( ushort ) LS2C_ON_LOGIN_SERVER_CONN.OPCODE;
            qn.args = largs;
            this.lNetWorker.AddQueue( qn );

            C2LS_LOGIN_REQUEST loginPro = new C2LS_LOGIN_REQUEST();
            loginPro.setChannelIdStr( _channelId );
            loginPro.setAccountIdStr( _accountId );
            send( loginPro );
        }

        public void OnAccountSerConnected( object sender, net.ArgsEvent args )
        {
            bool lbTemp = ( bool ) args.Data;
            if( false == lbTemp )
            {
                return;
            }

            UConnection conn = ( UConnection ) sender;
            Logger.Info( "connected : {" + conn.RemoteIP + "}:{" + conn.RemotePort + "}" );
            robot.client.Protocols.accountSerRegisterProtocol( conn.Session );

            sendAccountLogin();
        }

        public void setMacId( String macId )
        {
            _macId = macId;
        }

        public void sendAccountLogin()
        {
            sendAccount( 0, _account, _password, _macId, _serverType, _webSession, _channelId, _accountId );
        }

        public void getMacId()
        {
            C2AC_MAC_ID sender = new C2AC_MAC_ID();
            sender.uiListen = 0;
            send( sender );
        }

        public void setLoginInfo( string account, string password, string macId, byte serverType, string webSession, string channelId, string accountId )
        {
            _account = account;
            _password = password;
            _macId = macId;
            _serverType = serverType;
            _webSession = webSession;
            _channelId = channelId;
            _accountId = accountId;
        }

        public void sendAccount( UInt32 puiListen, string account, string password, string macId, byte serverType, string webSession, string channelId, string accountId )
        {
            C2AC_ACCOUNT_INFO accountPro = new C2AC_ACCOUNT_INFO();
            accountPro.uiListen = puiListen;
            accountPro.sAccountC2Ac.setAccount( account );
            accountPro.sAccountC2Ac.setPassword( password );
            accountPro.sAccountC2Ac.setDeviceInfo( macId );
            accountPro.sAccountC2Ac.serverType = serverType;
            accountPro.sAccountC2Ac.setSession( webSession );
            accountPro.sAccountC2Ac.setChannelIdStr( channelId );
            accountPro.sAccountC2Ac.setAccountIdStr( accountId );

            //Logger.Info(" send account real!! account = " + accountPro.sAccountC2Ac.getAccount() + " session_id = " + accountPro.sAccountC2Ac.getSessionId()
            //          + " account id = " + accountId);
            send( accountPro );
        }

        public void OnLogicSerReConnected( object sender, net.ArgsEvent args )
        {
            bool lbTemp = ( bool ) args.Data;
            if( false == lbTemp )
            {
                return;
            }

            RM2C_RELOGIN pro = new RM2C_RELOGIN();
            pro.iResult = 1;

            ArgsNetEvent largs = new ArgsNetEvent( pro );
            NodeQueue qn = new NodeQueue();
            qn.msg = ( ushort ) RM2C_RELOGIN.OPCODE;
            qn.args = largs;
            this.lNetWorker.AddQueue( qn );
        }

        public bool isListenPro( ushort msg )
        {
            return _event_listeners.ContainsKey( msg );
        }

        public void connectAccountServer( string ip, short port, int timeout )
        {
            _AccountSer = new UConnection();
            _AccountSer.agentNet = this;

            _AccountSer.EventConnected += this.OnAccountSerConnected;
            _AccountSer.open( ip, port, timeout );
        }

        public void connectLoginServer( string channelId, string accountId )
        {
            _channelId = channelId;
            _accountId = accountId;

            _LoginSer = new UConnection();
            _LoginSer.agentNet = this;

            _LoginSer.EventConnected += OnLoginSerConnected;
            _LoginSer.open( robot.Program.LOGIN_IP, robot.Program.LOGIN_PORT, robot.Program.LOGIN_TIMEOUT );
        }

        public void callEvent( ushort msg, ArgsNetEvent args )
        {
            if( _event_listeners.ContainsKey( msg ) )
            {
                _event_listeners[ msg ].callEvent( args );
            }
        }

        public void addListenEvent( ushort msg, EHandleNetEvent eventHandle )
        {
            if( !_event_listeners.ContainsKey( msg ) )
            {
                _event_listeners.Add( msg, new EListenerNet( msg ) );
            }
            _event_listeners[ msg ].ListenerEvent += eventHandle;
        }

        /// <summary>
        /// 回调事件加入链表
        /// </summary>
        public void tick()
        {
            NodeQueue node = lNetWorker.tick();

            if( null != node )
            {
                //Logger.Info("====> " + this._account + " recv: " + node.msg );
                callEvent( node.msg, node.args );
            }
        }

        public bool send( IProtocal pro )
        {
            FieldInfo fi = pro.GetType().GetField( "uiListen" );

            uint uiListen = 0; 
            if( fi != null)
            {
                uiListen = ( uint ) fi.GetValue( pro );    
            }
            Logger.Info( "======> " + this._account + "  " + pro.Message + "  uiListen: " + uiListen + "  time: " + GUtil.getTimeMs() );

            if(uiListen > 0) {
                robot.Program.sendTime[ uiListen ] = GUtil.getMS();
            }

            if( pro.Message >= 200 && pro.Message <= 1999 )
            {
                if( pro.Message == 322 )
                {
                    //set_is_ping_back(false);
                }

                if( _XmanLogic != null )
                {
                    return _XmanLogic.send( pro, false );
                }
                else
                {
                    Logger.Error( "_XmanLogic is null!" );
                }
            }
            else if( pro.Message >= 5000 && pro.Message < 6000 )
            {
                if( _LoginSer != null )
                {
                    return _LoginSer.send( pro, false );
                }
                else
                {
                    Logger.Error( "_LoginSer is null!" );
                }
            }
            else if( pro.Message >= 11000 && pro.Message <= 12000 )
            {
                if( _AccountSer != null )
                {
                    return _AccountSer.send( pro, false );
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

        public void logic_reopen( string id_channel, string id_account, string id_mac )
        {
            _XmanLogic.set_is_relogin( false );
            //_XmanLogic.EventReConnected += OnLogicSerReConnected;
            _XmanLogic.reopen( id_channel, id_account, id_mac );
        }

        public void close()
        {
            if( _AccountSer != null )
            {
                _AccountSer.close();
            }
            if( _LoginSer != null )
            {
                _LoginSer.close();
            }
            if( _XmanLogic != null )
            {
                _XmanLogic.close();
            }
            _AccountSer = null;
            _LoginSer = null;
            _XmanLogic = null;

            conned = false;
        }

        public bool conned;

        public bool isLogicConnected()
        {
            if( _XmanLogic == null )
                return true;

            return _XmanLogic.isConnected();
        }

        public void initLogic( string ip, short port, int timeout )
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

        public string _account;
        private string _password;
        private string _macId;
        private byte _serverType;
        private string _webSession;
        private string _channelId;
        public string _accountId;

        private workerNet _netWorker = new workerNet();

        private Dictionary<ushort, EListenerNet> _event_listeners = new Dictionary<ushort, EListenerNet>();

        private Queue<Action> taskQueue = new Queue<Action>();

        private Queue<Action> open_mail_queue = new Queue<Action>();

        private Queue<Action> fb_pk_queue = new Queue<Action>();

        private Queue<Action> pk_queue = new Queue<Action>();

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

        //聊天
        private Queue<Action> chat_queue = new Queue<Action>();

        //魂侠抽测试
        private Queue<Action> lucky_soul_queue = new Queue<Action>();

        //商队
        private Queue<Action> escort_queue = new Queue<Action>();

        //公会
        private Queue<Action> union_queue = new Queue<Action>();
        

        /// account返回
        public void recvAccountServer( ArgsEvent args )
        {
            AC2C_ACCOUNT_INFO recv = args.getData<AC2C_ACCOUNT_INFO>();
            this.connectLoginServer( recv.sAccountAC2C.getChannelIdStr(), recv.sAccountAC2C.getAccountIdStr() ); //getRelamInfo("2002");
            if( recv.iResult == 1 )
            {
                //ManagerServer.getInstance().lastLoginServerIndex = ( int ) recv.sAccountAC2C.uiServer[ 0 ];
            }
            Logger.Info( this._account + "  RECV SERVER ACCOUNT = " + recv.sAccountAC2C.getAccountIdStr() );
        }

        /// login返回
        public void recvLoginServer( ArgsEvent args )
        {
            LS2C_LOGIN_RESPONSE recv = args.getData<LS2C_LOGIN_RESPONSE>();

            Logger.Info( this._account + " LS2C_LOGIN_RESPONSE >> " + recv.iResult.ToString() );
            if( recv.iResult == 1 )
            {
                Logger.Info( this._account + " Login Authentication Success" );
                Logger.Info( this._account + " realm server info: " + recv.getIp() + ":" + recv.port );

                this.initLogic( recv.getIp(), ( short ) recv.port, ( int ) recv.timeOut );
                this.connectRealmServer();
            }
            else
            {
                Logger.Info( this._account + " recvLoginServer error, iresult = " + recv.iResult );
            }
        }

        ///协议返回 realm返回
        public void recvLogin( ArgsEvent args )
        {
            RM2C_LOGIN recv = args.getData<RM2C_LOGIN>();
            Logger.Info( this._account + " connect Realm Result: " + recv.iResult );
            if( recv.iResult == ( int ) EM_CLIENT_ERRORCODE.EE_M2C_NEED_CREATE_ROLE )
            {
                Logger.Info( this._account + " 需要创建角色" );
                this.trans.sendCreatRole( 61, null );
            }
            else if( recv.iResult == ( int ) EM_CLIENT_ERRORCODE.EE_ACCOUNT_NOT_EXIST )
            {
                Logger.Info( this._account + " Realm msg: 帐号不存在" );
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
            //Logger.Info( "player vip: " + playerInfo.sPlayerBaseInfo. );
            Logger.Info( "player exp: " + playerInfo.sPlayerBaseInfo.luiExp );
            Logger.Info( "player power: " + playerInfo.sLeadPowerInfo.usPower );
            Logger.Info( "=========================================" );

            /// 创建主角
            //ManagerServer.getInstance().isBinded = recv.SPlayerInfo.sPlayerBaseInfo.cIsBind == 0 ? false : true;
            if( null == this.dataMode.getPlayer( recv.SPlayerInfo.sPlayerBaseInfo.uiIdMaster ) )
                this.dataMode._serverPlayer.Add( recv.SPlayerInfo.sPlayerBaseInfo.uiIdMaster, new InfoPlayer( this.dataMode ) );

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

            

            Action task1 = () =>
            {
                this.OpenMailStart( () =>
                {
                    this.doNext();
                } );
            };

            Action task2 = () =>
            {
                this.LuckySoulStart(1, ( UtilListenerEvent evt ) =>
                {
                    this.doNext();
                } );
            };

            Action task3 = () =>
            {
                this.SMoneyShopStart( () =>
                {
                    this.doNext();
                } );
            };

            Action task4 = () =>
            {
                this.PKShopStart( () =>
                {
                    this.doNext();
                } );
            };

            Action task5 = () =>
            {
                this.buyFBStart( () =>
                {
                    this.doNext();
                } );
            };

            Action task6 = () =>
            {
                this.sweepFBStart( () =>
                {
                    this.doNext();
                } );
            };

            Action task7 = () =>
            {
                this.heroStart( () =>
                {
                    this.doNext();
                } );
            };

            Action task8 = () =>
            {
                this.petLvUpStart( () =>
                {
                    this.doNext();
                } );
            };

            Action task9 = () =>
            {
                this.EquipStart( () =>
                {
                    this.doNext();
                } );
            };

            Action task10 = () =>
            {
                this.StoneStart( () =>
                {
                    this.doNext();
                } );
            };

            Action task11 = () =>
            {
                this.EquipUpStart( () =>  //装备强化
                {
                    this.doNext();
                } );
            };

            Action task12 = () =>
            {   //装备合成
                this.EquipComStart( () =>  
                {
                    this.doNext();
                } );
            };

            Action task13 = () =>
            {   //技能升级
                this.SkillStart( () =>
                {
                    this.doNext();
                } );
            };

            Action task14 = () =>
            {   //世界聊天
                this.ChatStart( () =>
                {
                    this.doNext();
                } );
            };

            Action task15 = () =>
            {
                this.pkStart( () =>
                {
                    this.doNext();
                } );
            };

            Action task16 = () =>
            {
                this.FbPKStart(1, 63, () =>
                {
                    this.doNext();
                } );
            };

           
            Action task18 = () =>
            {
                this.escortStart( () =>
                {
                    this.doNext();
                } );
            };

            Action task19 = () =>
            {
                this.unionStart( () =>
                {
                    this.doNext();
                } );
            };

            Dictionary<string, Action> actionIndex = new Dictionary<string, Action>();
            actionIndex.Add("task1", task1);   //打开所有邮件
            actionIndex.Add("task2", task2);   //魂匣
            actionIndex.Add("task3", task3);   //金币商店
            actionIndex.Add("task4", task4);   //pk商店
            actionIndex.Add("task6", task6);   //副本扫荡
            actionIndex.Add("task7", task7);   //英雄升星
            actionIndex.Add("task8", task8);   //吃经验药升级
            actionIndex.Add("task9", task9);   //穿装备
            actionIndex.Add("task10", task10); //镶钻
            actionIndex.Add("task11", task11); //装备强化
            actionIndex.Add("task12", task12); //装备合成
            actionIndex.Add("task13", task13); //技能升级
            actionIndex.Add("task14", task14); //世界聊天
            actionIndex.Add("task15", task15); //竞技场
            actionIndex.Add("task16", task16); //打副本
            actionIndex.Add("task18", task18); //商队
            actionIndex.Add("task19", task19); //公会

            Action endAction = null;
            endAction = () =>
            {
                Logger.Info( "task done." );
            };

            this.taskQueueClear();

            foreach(string item in robot.Program.task_list) {
                Action action = actionIndex[item];
                if(action != null)
                    this.addTask(action);  
            }

            ////this.addTask( task1 );   //email
            //this.addTask( task2 );   //LuckySoul
            //this.addTask( task3 );   //SMoneyShop
            //this.addTask( task4 );
            ////this.addTask( task5 );
            
            ////this.addTask( task7 );     //hero
            ////this.addTask( task8 );     //petLvUp
            ////this.addTask( task9 );     //Equip
            ////this.addTask( task10 );    //Stone
            ////this.addTask( task11 );    //EquipUp
            ////this.addTask( task12 );    //EquipCom
            //this.addTask( task13 );    //Skill 
            ////this.addTask( task14 );  //世界聊天

            //this.addTask( task16 );    //fb
            //this.addTask( task6 );     //fb sweep
            //this.addTask( task15 );    //竞技场pk

            //this.addTask( task17 );    //pk
            //this.addTask( task18 );      //escort
            //this.addTask( task19 );        //union

            this.addTask( endAction );   //add end action

            this.doNext();  //start task
            
        }

        private void taskQueueClear()
        {
            this.taskQueue.Clear();    
        }

        private void addTask(Action task)
        {
            this.taskQueue.Enqueue(task);    
        }

        private bool doNext()
        {
            Action next = this.taskQueue.Dequeue();
            if(next == null )
                return false;

            next();
            return true;    
        }

        //领取邮件奖励
        private void OpenMailStart(Action _cb)
        {
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
                            trans.sendOpenEmail( emailId, ( UtilListenerEvent evt2 ) =>
                            {
                                Logger.Info( this._account + " 打开邮件 " + emailId );

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

                    _cb();

                    //trans.sendPwoerBuy( null );   //购买体力

                    

                };
                this.open_mail_queue.Enqueue( end );

                Action firstAction = this.open_mail_queue.Dequeue();
                firstAction();

            };

            //发送获取邮件请求
            trans.sendWebEmail( null );   //如果没有邮件的话，服务器不会回应,超3秒后，由定时器触发

            func( null );
        }

        //response 
        public void buyFBStart(Action _cb)
        {
            trans.sendFBUpdate( ( UtilListenerEvent evt ) =>
            {
                RM2C_FB recv = (RM2C_FB)evt.eventArgs;
                //Logger.Info( this._account + " RECV:RM2C_FB 成功 " );

                InfoPlayer player = this.dataMode.getPlayer( recv.uiMasterId );

                this.dataMode._serverFB.Clear();

                if( player == null )
                {
                    Debug.Assert( false, "player is null" );
                    Logger.Error( "player is null  " + recv.uiMasterId );
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
                        Logger.Info( this._account + " 购买副本 " + csvFb );

                        trans.sendBuyFBCnt( ( int ) csvFb, ( UtilListenerEvent evt2 ) =>
                        {
                            Action action = this.buy_fb_queue.Dequeue();
                            Debug.Assert( action != null );

                            action();
                        } );
                    };
                    this.buy_fb_queue.Enqueue( func );
                }

                //sweep done!
                Action end = () =>
                {
                    Logger.Info( this._account + " 购买副本重置全部完成 " );
                    _cb();
                };
                this.buy_fb_queue.Enqueue( end );

                Action firstAction = this.buy_fb_queue.Dequeue();
                firstAction();   //do the first task
            } );
        }

        //开始扫荡副本
        private void sweepFBStart(Action _cb)
        {
            this.sweep_queue.Clear();

            //fb sweep one by one
            foreach( var item in this.dataMode._serverFB )
            {
                int csvFb = item.Value.idCsvFB;
                Action func = () =>
                {
                    Logger.Info( this._account + " 开始发送扫荡命令 " + csvFb );

                    trans.sendFBSweep( ( uint ) csvFb, ( UtilListenerEvent evt ) =>
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
                _cb();
            };

            this.sweep_queue.Enqueue( end );

            Action firstAction = this.sweep_queue.Dequeue();
            firstAction();   //do the first task
        }

        //开始金币商店相关操作
        private void SMoneyShopStart( Action _cb )
        {
            trans.sendGetSMoneyShop( ( UtilListenerEvent evt ) =>
            {
                RM2C_GET_SMONEY_SHOP res = ( RM2C_GET_SMONEY_SHOP ) evt.eventArgs;

                Logger.Info( this._account + " 商店列表返回: " + res.iResult );

                if( res.iResult != 1 )
                {
                    Logger.Error( this._account + " 商店列表返回失败 " + res.iResult );
                }

                trans.sendRefreshMoneyShop( 0, ( UtilListenerEvent ev ) =>
                {
                    RM2C_REFRESH_SMONEY_SHOP re = ( RM2C_REFRESH_SMONEY_SHOP ) ev.eventArgs;
                    if( re.iResult != 1 )
                    {
                        Logger.Error( this._account + " 刷新金币商店返回失败 " + re.iResult );
                    }
                    Logger.Info( this._account + " 刷新商店返回" );
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
                            trans.sendBuyMoneyShop( index, ( UtilListenerEvent e ) =>
                            {
                                RM2C_SMONEY_SHOP_BUY buy = ( RM2C_SMONEY_SHOP_BUY ) e.eventArgs;
                                if( buy.iResult != 1 )
                                {
                                    Logger.Error( this._account + " 商店购买返回失败 " + buy.iResult );
                                }
                                Logger.Info( this._account + " 商店购买返回  " + buy.iLoc );

                                Action task = this.buy_goods_queue.Dequeue();

                                task();
                            } );

                        };
                        this.buy_goods_queue.Enqueue( action );
                    }

                    //购买完成任务
                    Action end = () =>
                    {
                        Logger.Info( this._account + " 购买商品完成!" );
                        _cb();
                    };
                    this.buy_goods_queue.Enqueue( end );
                    Action firstAction = this.buy_goods_queue.Dequeue();
                    firstAction();
                } );
            } );
        }

        //开始pk shop相关操作
        private void PKShopStart( Action _cb )
        {
            //get pkshop list
            trans.sendPKShopUpdate( ( UtilListenerEvent evt ) =>
            {
                //得到pk商店列表后，重置
                trans.sendPKShopReset( 1, ( UtilListenerEvent ev ) =>
                {
                    Logger.Info( this._account + " 刷新PK商店返回" );
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
                            trans.sendPKShopBuy( index, ( UtilListenerEvent e ) =>
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
                        _cb();
                    };
                    this.buy_pk_queue.Enqueue( end );

                    Action firstAction = this.buy_pk_queue.Dequeue();
                    firstAction();
                } );
            } );
        }

        //开始爵位商店相关操作
        private void NobilityShopStart(Action _cb)
        {
            //get shop list
            trans.sendGetNobilityShop( ( UtilListenerEvent evt ) =>
            {
                RM2C_GET_NOBILITY_SHOP rec = ( RM2C_GET_NOBILITY_SHOP ) evt.eventArgs;
                //得到爵位商店列表后，重置
                trans.sendNobilityShopReset( 1, ( UtilListenerEvent ev ) =>
                {
                    Logger.Info( this._account + " 刷新爵位商店返回" );

                    RM2C_REFRESH_NOBILITY_SHOP recv = ( RM2C_REFRESH_NOBILITY_SHOP ) ev.eventArgs;
                    if( recv.iResult != 1 )
                    {
                        Logger.Error( this._account + " 爵位商店重置失败！" + recv.iResult );
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
                            trans.sendNobilityShopBuy( _index, ( UtilListenerEvent e ) =>
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
                        _cb();
                    };
                    this.buy_nobility_queue.Enqueue( end );

                    Action firstAction = this.buy_nobility_queue.Dequeue();
                    firstAction();
                } );
            } );
        }

        //开始 Mot shop相关操作
        private void MotShopStart(Action _cb)
        {
            Logger.Info( this._account + " 开始操作远征商店" );

            //获取远征商店列表
            trans.sendGetEDShop( ( UtilListenerEvent evt ) =>
            {
                //得到远征商店列表后，重置
                trans.sendRefreshEDShop( 1, ( UtilListenerEvent ev ) =>
                {
                    Logger.Info( this._account + " 刷新远程商店返回" );
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
                            trans.sendBuyEDShop( index, ( UtilListenerEvent e ) =>
                            {
                                RM2C_MOUNTAIN_SHOP_BUY buy = ( RM2C_MOUNTAIN_SHOP_BUY ) e.eventArgs;
                                if( buy.iResult != 1 )
                                {
                                    Logger.Error( this._account + " 远征商店购买返回失败 " + buy.iResult );
                                }
                                Logger.Info( this._account + " 远征商店购买返回  " + buy.iLoc );

                                Action next = this.buy_mot_queue.Dequeue();
                                next();   //execute next task in queue
                            } );
                        };
                        this.buy_mot_queue.Enqueue( action );
                    }

                    //购买完成任务
                    Action end = () =>
                    {
                        Logger.Info( this._account + " 购买远征商品完成!" );
                        _cb();
                        //this.piece2petStart();
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
            trans.sendHeroChipUpdate( ( UtilListenerEvent evt ) =>
            {
                Dictionary<int, long> chipHero = this.dataMode.infoHeroChip.chipHero;

                this.piece_to_pet_queue.Clear();

                foreach( var item in chipHero )
                {
                    int sameid = item.Key;

                    Action action = () =>
                    {
                        Logger.Info( this._account + " 碎片合成 " + sameid );
                        trans.sendPetChipToPet( sameid, ( UtilListenerEvent evt2 ) =>
                        {
                            RM2C_PET_PIECE_TO_PET p2p = ( RM2C_PET_PIECE_TO_PET ) evt2.eventArgs;

                            Logger.Info( this._account + " 碎片合成返回 " + p2p.sPiece.luiIdPiece );

                            Action next = this.piece_to_pet_queue.Dequeue();
                            next();   //execute next task in queue
                        } );
                    };
                    this.piece_to_pet_queue.Enqueue( action );
                }

                Action end = () =>
                {
                    Logger.Info( this._account + " 碎片合成完成" );

                    //this.heroStart();
                };
                this.piece_to_pet_queue.Enqueue( end );

                Action firstAction = this.piece_to_pet_queue.Dequeue();
                firstAction();  //start
            } );
        }

        //英雄相关操作开始
        public void heroStart(Action _cb)
        {
            Logger.Info( this._account + " 开始获取英雄列表" );
            //获取英雄列表
            trans.sendHeroUpdateBag( ( UtilListenerEvent evt2 ) =>
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
                        trans.sendPetStarUp( serverId, ( UtilListenerEvent evt ) =>
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
                    Logger.Info( this._account + " 英雄升级己完成" );
                    _cb();
                    
                };
                this.star_up_queue.Enqueue( end );

                Action firstAction = this.star_up_queue.Dequeue();
                firstAction();
            } );
        }

        //吃经验药升级开始
        public void petLvUpStart(Action _cb)
        {
            //获取装备信息
            trans.sendEquipUpdate( ( UtilListenerEvent evt ) =>
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
                        trans.sendPetLvUp( serverId, expType, expCnt, ( UtilListenerEvent evt2 ) =>  //32  20*8000  Hard code
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
                    Logger.Info( this._account + "Pet Lv up 完成" );
                    _cb();
                };
                this.pet_lvup_queue.Enqueue( end );

                Action firstAction = this.pet_lvup_queue.Dequeue();
                firstAction();
            } );
        }

        //穿装备
        private void EquipStart(Action _cb)
        {
            this.equip_puton_queue.Clear();

            //获取最新装备信息
            trans.sendEquipUpdate( ( UtilListenerEvent evt ) =>
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
                        trans.sendHeroEquipChange( idServer, equips, ( UtilListenerEvent evt3 ) =>
                        {
                            Action next = this.equip_puton_queue.Dequeue();
                            next();   //execute next task in queue
                        } );
                    };

                    this.equip_puton_queue.Enqueue( action );
                }

                Action end = () =>
                {
                    Logger.Info( this._account + " 穿装备己完成" );
                    _cb();
                };
                this.equip_puton_queue.Enqueue( end );

                Action firstAction = this.equip_puton_queue.Dequeue();
                firstAction();

            } );
        }

        //连接魂侠抽测试
        public void LuckySoulStart( int times, FunctionListenerEvent sListener )
        {
            this.lucky_soul_queue.Clear();

            Dictionary<UInt32,UInt32> lucky_stat = new Dictionary<UInt32, UInt32>();

            for( int i = 0; i < times; i++ )
            {
                Action action = () =>
                {
                    trans.sendLuckySoul( ( UtilListenerEvent e ) =>
                    {
                        RM2C_LUCKY_SOUL recv = ( RM2C_LUCKY_SOUL ) e.eventArgs;

                        if( recv.iResult == 1 )
                        {
                            Debug.Assert( recv.uiRewards.Length == 1, "" );

                            Logger.Info( this._account + " 魂侠抽次数：" + recv.uiLuckySoulCnt );
                            Logger.Info( this._account + " 魂侠物品 csvid: " + recv.uiRewards[ 0 ].uiIdCsv + " 类型：" + recv.uiRewards[ 0 ].cType );

                            UInt32 csvId = recv.uiRewards[ 0 ].uiIdCsv;
                            if(!lucky_stat.ContainsKey(csvId)) {
                                lucky_stat.Add( csvId, 0 );
                            }

                            lucky_stat[ csvId ]++;
                        }
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

                Logger.Info( this._account + " 概率统计结果： " );
                foreach(KeyValuePair<UInt32,UInt32> v in lucky_stat)
                {
                    Logger.Info( "lucky_stat: " + v.Key + " 次数：" + v.Value );
                }

                //if( sListener != null )
                sListener( null );
            };
            this.lucky_soul_queue.Enqueue( end );

            Action firstAction = this.lucky_soul_queue.Dequeue();
            firstAction();
        }

        //钻石镶嵌及升级
        private void StoneStart(Action _cb)
        {
            this.stone_up_queue.Clear();

            //step 1. 先重新获取英雄列表
            trans.sendHeroUpdateBag( ( UtilListenerEvent evt2 ) =>
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
                        if( csvHero.grade == csvHero.gradeMax )    //如果已经到顶级,不能再升
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
                                    trans.sendStoneInLay( sid, index, ( UtilListenerEvent evt ) =>
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
                                                trans.sendPetStoneUp( sid, ( UtilListenerEvent evt3 ) =>
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
                            if( csvHero.grade == csvHero.gradeMax )    //如果已经到顶级,不能再升
                                continue;

                            ulong sid = serverId;
                            Action action = () =>
                            {
                                //进阶
                                trans.sendPetStoneUp( sid, ( UtilListenerEvent evt3 ) =>
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
                    Logger.Info( this._account + " 英雄进阶全部完毕" );
                    //Logger.Info( this._account + " 重新尝试是否有可穿装备..." );
                    _cb();
                };
                this.stone_up_queue.Enqueue( end );

                Action firstAction = this.stone_up_queue.Dequeue();
                firstAction();
            } );
        }

        //装备强化相关操作
        private void EquipUpStart(Action _cb)
        {
            this.equip_up_queue.Clear();

            //step 1. 先重新获取英雄列表
            trans.sendHeroUpdateBag( ( UtilListenerEvent evt2 ) =>
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

                        if( mqu.addNumber >= 9 )  //己经强化到顶级,不能再升
                            continue;

                        Action action = () =>
                        {
                            Logger.Info( this._account + " 开始强化装备 idServer: " + hero.idServer + " loc: " + mqu.local + " 当前级别: " + mqu.addNumber );
                            //装备强化
                            trans.sendEquipUp( mqu.local, hero.idServer, ( UtilListenerEvent evt3 ) =>
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
                    Logger.Info( this._account + " 装备强化完毕!" );
                    //Logger.Info( this._account + "准备装备合成..." );
                    _cb();
                };

                this.equip_up_queue.Enqueue( end );

                Action firstAction = this.equip_up_queue.Dequeue();
                firstAction();
            } );
        }

        //装备合成
        private void EquipComStart(Action _cb)
        {
            this.equip_com_queue.Clear();
            List<TypeCsvEquipCreat> creatCsvList = ManagerCsv.getEquipCreat();

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
                        trans.sendEquipCreat( ( uint ) csvProp, ( UtilListenerEvent evt2 ) =>
                        {
                            Action next = this.equip_com_queue.Dequeue();
                            next();   //execute next task in queue
                        } );
                    };
                    this.equip_com_queue.Enqueue( action );
                }
            }

            Action end = () =>
            {
                Logger.Info( this._account + " 装备合成完毕!" );
                _cb();
            };
            this.equip_com_queue.Enqueue( end );
            Action firstAction = this.equip_com_queue.Dequeue();
            firstAction();
        }

        //技能相关操作
        private void SkillStart(Action _cb)
        {
            this.skill_up_queue.Clear();
            Logger.Info( this._account + " 开始技能操作" );
            //获取英雄列表
            trans.sendHeroUpdateBag( ( UtilListenerEvent evt2 ) =>
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
                            Logger.Info( this._account + " 请求技能升级  " + heroInfo.idServer + " " + _skill.idCsv );
                            trans.sendSkillUpNew( heroInfo.idServer, _skill.idCsv, ( UtilListenerEvent evt ) =>
                            {
                                RM2C_SKILL_UP_NEW recv = ( RM2C_SKILL_UP_NEW ) evt.eventArgs;

                                //System.Timers.Timer timer = new System.Timers.Timer();
                                //timer.Interval = 80;
                                //timer.AutoReset = false; //once
                                //timer.Enabled = true;
                                //timer.Elapsed += new ElapsedEventHandler( ( object source, System.Timers.ElapsedEventArgs e ) =>
                                //{
                                    Action next = this.skill_up_queue.Dequeue();
                                    next();   //execute next task in queue
                                //} );
                            } );
                        };
                        this.skill_up_queue.Enqueue( action );
                    }
                }

                Action end = () =>
                {
                    Logger.Info(this._account + " 技能升级己完成!" );
                    _cb();
                };
                this.skill_up_queue.Enqueue( end );

                Action firstAction = this.skill_up_queue.Dequeue();
                firstAction();
            } );
        }


        private void ChatStart( Action _cb )
        {
            //this.chat_queue.Clear();

            ChatInfo info = new ChatInfo();
            info.unionID = ( uint ) 0; //DataMode.union.uID;
            info.cName = this.dataMode.myPlayer.name;
            info.Content = "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz";
            info.Type = ( byte ) 1;   //1 world chat
            info.teamLv = this.dataMode.myPlayer.lv;
            info.teamLeaderCsvId = 0; //this.dataMode.myPlayer.infoHeroList.getTeamLeader().idCsv;
            info.TalkTime = ( int ) 0; //Data.serverTime;
            //还差玩家角色id
            info.RoleId = this.dataMode.myPlayer.idServer;

            trans.sendChat( info, ( UtilListenerEvent evt ) =>
            {
                _cb();

            } );
        }

        //竞技场PK
        private void pkStart(Action _cb)
        {
            this.pk_queue.Clear();

            //Action action = () =>
            //{
                trans.sendHeroUpdateBag( ( UtilListenerEvent ev ) =>
                {
                    //get team info
                    trans.sendHeroUpdateTeam( ( UtilListenerEvent evt ) =>
                    {
                        List<InfoHero> infoHero = this.dataMode.myPlayer.infoHeroList.getTeam();
                        ulong[] team = new ulong[] { 0, 0, 0, 0, 0 };
                        for( int index = 0; index < infoHero.Count; index++ )
                        {
                            if( null == infoHero[ index ] )
                                continue;

                            team[ index ] = infoHero[ index ].idServer;
                        }

                        trans.sendNewTeamInfo( ( uint ) this.dataMode.myPlayer.infoHeroList.idTeamSelectPK, team, ( UtilListenerEvent evt2 ) =>
                        {
                            trans.sendPKCombat( ( UtilListenerEvent evt22 ) =>
                            {
                                for( int i = 0; i < this.dataMode.myPlayer.infoPK.cnt; i++ )
                                {
                                    Action action = () => {
                                        trans.sendPKCombat( ( UtilListenerEvent evt3 ) =>
                                        {
                                            trans.sendPKCombatStart();

                                            trans.sendPKCombatEnd( true, ( UtilListenerEvent evt4 ) =>
                                            {
                                                RM2C_CHECK_PK recv = ( RM2C_CHECK_PK ) evt4.eventArgs;

                                                Action next = this.pk_queue.Dequeue();
                                                next();
                                            } ); //sendPKCombatEnd

                                        } ); //sendPKCombat
                                    };
                                    this.pk_queue.Enqueue( action );
                               }; //for

                                Action end = () =>
                                {
                                    Logger.Info( this._account + " 竞技场PK完成!" );
                                    _cb();
                                };
                                this.pk_queue.Enqueue( end );

                                Action firstAction = this.pk_queue.Dequeue();
                                firstAction();

                            } ); //sendPKCombat

                            /*
                            trans.sendPKCombat( ( UtilListenerEvent evt3 ) =>
                            {
                                for( int i = 0; i < this.dataMode.myPlayer.infoPK.cnt; i++ )
                                {
                                    trans.sendPKCombatStart();

                                    trans.sendPKCombatEnd( true, ( UtilListenerEvent evt4 ) =>
                                    {
                                        RM2C_CHECK_PK recv = ( RM2C_CHECK_PK ) evt4.eventArgs;

                                        Action next = this.pk_queue.Dequeue();
                                        next();
                                    } );
                                }

                            } );
                             */
                        } );  //sendNewTeamInfo

                    } );  //sendHeroUpdateTeam
                } ); //sendHeroUpdateBag

            //};

            //this.pk_queue.Enqueue( action );

            //Action end = () =>
            //{
            //    Logger.Info( this._account + " 竞技场PK完成!" );
            //    _cb();
            //};
            //this.pk_queue.Enqueue( end );

            //Action firstAction = this.pk_queue.Dequeue();
            //firstAction();
        }

        //闯副本
        private void FbPKStart(int fbStartId, int fbEndId, Action _cb)
        {
            this.trans.sendHeroUpdateBag( ( UtilListenerEvent evt0 ) =>
            {
                this.buyFBStart( () =>
                {
                    if( this.dataMode._serverFB.Count == 0 )
                    {
                        Logger.Error( "_serverFB.Count == 0" );
                        return;
                    }

                    this.fb_pk_queue.Clear();

                    for( int i = fbStartId; i <= fbEndId; i++ )
                    //foreach( var item in this.dataMode._serverFB )
                    {
                        int fbCsvId = i;  //item.Value.idCsvFB;

                        Action action = () =>
                        {
                            int ff = fbCsvId;

                            //体力不足
                            if( this.dataMode.myPlayer.power < 6 )
                            {
                                this.trans.sendPwoerBuy( ( UtilListenerEvent evt ) =>
                                {
                                } );
                            }

                            this.trans.sendFBIn( ff, ( UtilListenerEvent evt ) =>
                            {
                                RM2C_GOTO_FB recv = ( RM2C_GOTO_FB ) evt.eventArgs;

                                if( recv.iResult != 1 )
                                {
                                    if( recv.iResult == 77 )
                                    {
                                        Logger.Error( " GOTO FB副本未开启" );
                                    }
                                    else if( recv.iResult == 63 )
                                    {
                                        Logger.Error( " GOTO FB体力不足" );    
                                    }
                                    else if( recv.iResult == 91 )
                                    {
                                        Logger.Error( " GOTO FB己达到最大通关次数" );
                                    }

                                    else
                                    {
                                        Logger.Error( " GOTO FB Failed   errno: " + recv.iResult);
                                    }
                                    
                                    Action next = this.fb_pk_queue.Dequeue();
                                    next();
                                    return;
                                }

                                this.trans.sendFBCombatReward( ( UtilListenerEvent evt2 ) =>
                                {
                                    this.trans.sendFBCombatStart();
                                    this.trans.sendFBCombatEnd( true, ( UtilListenerEvent evt3 ) =>
                                    {
                                        //RM2C_CHECK_FB_PK recv3 = ( RM2C_CHECK_FB_PK ) evt3.eventArgs;

                                        this.trans.sendFBCombatStart();
                                        this.trans.sendFBCombatEnd( true, ( UtilListenerEvent evt4 ) =>
                                        {
                                            //RM2C_CHECK_FB_PK recv4 = ( RM2C_CHECK_FB_PK ) evt4.eventArgs;

                                            this.trans.sendFBCombatStart();
                                            this.trans.sendFBCombatEnd( true, ( UtilListenerEvent evt5 ) =>
                                            {
                                                //RM2C_CHECK_FB_PK recv5 = ( RM2C_CHECK_FB_PK ) evt5.eventArgs;
                                                //退出副本
                                                this.trans.sendFBOut( ( UtilListenerEvent evt6 ) =>
                                                {
                                                    Action next = this.fb_pk_queue.Dequeue();
                                                    next();
                                                } );

                                            } );  //sendFBCombatEnd
                                        } );  //sendFBCombatEnd
                                    } );  

                                } );

                            } );  //sendFBIn

                        }; //action

                        this.fb_pk_queue.Enqueue( action );

                    } //foreach

                    Action end = () =>
                    {
                        Logger.Info("fb pk end");
                        _cb();
                    };
                    this.fb_pk_queue.Enqueue( end );

                    Action firstAction = this.fb_pk_queue.Dequeue();
                    firstAction();

                } ); //buyFBStart

            } );  //sendHeroUpdateBag
            
        }

        //商队
        private void escortStart( Action _cb )
        {
            this.escort_queue.Clear();

            //搜索商队
            this.trans.sendEscortFindRob( ( UtilListenerEvent evt0 ) =>
            {
                RM2C_ESCORT_FIND_GROUP recv = ( RM2C_ESCORT_FIND_GROUP ) evt0.eventArgs;

                //当前正在进攻的 商队信息
                this.trans.sendEscortDataRobInfo( ( UtilListenerEvent evt1 ) =>  //C2RM_ESCORT_BEAT_GROUP
                {
                    RM2C_ESCORT_BEAT_GROUP recv1 = ( RM2C_ESCORT_BEAT_GROUP ) evt1.eventArgs;

                    bool canAttack = false;
                    //防守队伍
                    foreach( SEscortTeamShowBase item in recv1.EscortInfo.vctTeamShow )
                    {
                        if(item.cType == 0 && item.luiIdETid > 0) {   //可以攻打的队伍  
                            canAttack = true;
                            ulong tid = item.luiIdETid;    //upvalue
                            uint tsid = item.uiSessionId;
                            Action action = () =>
                            {
                                //GOTO
                                this.trans.sendEscortRobIn(tid, ( UtilListenerEvent evt2 ) =>
                                {
                                    RM2C_ESCORT_GO_TO recv2 = ( RM2C_ESCORT_GO_TO ) evt2.eventArgs;

                                    if( recv2.iResult == 1 )
                                    {
                                        this.trans.sendEscortCombatStart();
                                        this.trans.sendEscortCombatEnd(true, 0, ( UtilListenerEvent evt3 ) =>
                                        {
                                            RM2C_ESCORT_PK_OVER recv3 = ( RM2C_ESCORT_PK_OVER ) evt3.eventArgs;

                                            //LEAVE
                                            this.trans.sendEscortRobOut( ( UtilListenerEvent evt5 ) =>
                                            {
                                                RM2C_ESCORT_LEAVE recv5 = ( RM2C_ESCORT_LEAVE ) evt5.eventArgs;

                                                //this.trans.sendEscortGiveUp(0, ( UtilListenerEvent evt6 ) =>
                                                //{
                                                    Action next = this.escort_queue.Dequeue();
                                                    next();
                                                //} );


                                            } );
                                        } );
                                    }
                                    else   //goto failed, just skip it and do next task in the queue
                                    {
                                        Action next = this.escort_queue.Dequeue();
                                        next();    
                                    }

                                } ); //sendEscortRobIn
                            };

                            this.escort_queue.Enqueue( action );
                        }

                    }; //foreach

                    if( canAttack )
                    {
                        
                    }

                    Action end = () =>
                    {
                        Logger.Info( "escort end" );
                        _cb();
                    };
                    this.escort_queue.Enqueue( end );

                    Action firstAction = this.escort_queue.Dequeue();
                    firstAction();

                } ); //sendEscortDataRobInfo

            } );  //sendEscortFindRob
        }



        //公会
        private void unionStart( Action _cb )
        {
            //this.union_queue.Clear();

            //创建公会
            this.trans.sendCreateUnion("union_" + this._account, 1, 1, 0, 1, (UtilListenerEvent evt0) =>
            {
                RM2C_CREATE_GAME_UNION recv = (RM2C_CREATE_GAME_UNION)evt0.eventArgs;

                if (recv.iResult == 504 || recv.iResult == 1)
                {
                    //解散公会
                    this.trans.sendDisbandUnion((int)recv.m_stGUInfo.m_uiID, (UtilListenerEvent evt1) =>
                    {
                        RM2C_DISBAND_GAME_UNION recv1 = (RM2C_DISBAND_GAME_UNION)evt1.eventArgs;

                        _cb();
                    }); //sendDisbandUnion
                }
                else  //failed
                {
                    _cb();
                }

                //_cb();

            });  //sendCreateUnion
        }
    }
  
	
}
