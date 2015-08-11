using System;
using System.Collections.Generic;
using UnityEngine;

namespace net.unity3d
{
    public class AgentNet
    {
		
		/// <summary>
		/// 客户端连接服务器配置文件路径
		/// </summary>
		
		//57
	//	public static string URL_CONFIG = "http://192.168.1.8/xman_assets/serverConfig/new_ClientNet_config.xml";
		
		//8
		public static string URL_CONFIG = "";
		public static string URL_CONFIG_ACCOUNT = "";
		public static readonly string accountServerId = "account_server";
		
		
		public AgentNet()
        {
//			LoadMngr loadMngr  = LoadMngr.getInstance();
//            ManagerNet mgr = ManagerNet.getInstance();
//			string configXml = "";

//			configXml = loadMngr.GetText(URL_CONFIG);
			
//            mgr.init(configXml);
			
//			configXml = loadMngr.GetText(URL_CONFIG_ACCOUNT);
//			mgr.initAccountNote(configXml);
			ARequestOverTime = DateTime.Now; //现在时间
			ARequestTime = DateTime.Now; //现在时间
			    
        }
		
		public void initClientNoetNew(List<NoteServer> ListNote)
		{
			ManagerNet mgr = ManagerNet.getInstance();
			mgr.init(ListNote);
		}
		
//		public void initClientNote(string xml_str)
//		{
//			ManagerNet mgr = ManagerNet.getInstance();
//			string configXml = "";
//			mgr.init(xml_str);
//		}
		public void initAccountNote(List<NoteServer> ListNote)
		{
			ManagerNet mgr = ManagerNet.getInstance();
			mgr.initAccountNote(ListNote);
		}
//		public void initAccountNote(string xml_str)
//		{
//			LoadMngr loadMngr  = LoadMngr.getInstance();
//			ManagerNet mgr = ManagerNet.getInstance();
//			string configXml = "";
//			configXml = loadMngr.GetText(URL_CONFIG_ACCOUNT);
//			mgr.initAccountNote(configXml);
//		}

		public void openLogicServer(string pluiChannel, string pluiAccount, string psMacId )
        {
            ManagerNet mgr = ManagerNet.getInstance();
			

			try
			{
				mgr.addCurServerId();
				int index = mgr.getCurServerId();
				mgr.addFactory(new net.unity3d.UConnNetFactory("server" + index.ToString()));
				_XmanLogic = mgr.createNetObject<IConnection>("server" + index.ToString());
				
	            _XmanLogic.EventConnected += OnXmanLogicConnected;

				_XmanLogic.set_account(pluiChannel, pluiAccount);
				_XmanLogic.set_macid(psMacId);
				Debug.Log("logic ip = "+ m_LogicNote._serverIp);
				Debug.Log("logic port = "+ m_LogicNote._serverProt);
	            _XmanLogic.open(m_LogicNote);
				Debug.Log("connect realm server ip = " + m_LogicNote._serverIp);
			}
			catch(Exception ex)
			{
				Debug.LogError("create realm conn exp = " + ex);	
				workerNet lNetWorker = workerNet.getInstance();
				NodeQueue node = new NodeQueue();
				RM2C_ON_REALM_SERVER_CLOSE pro = new RM2C_ON_REALM_SERVER_CLOSE();
				pro.uiReason = 1;
				ArgsNetEvent largs = new ArgsNetEvent(pro);
	            NodeQueue qn = new NodeQueue();
	            qn.msg = (ushort)RM2C_ON_REALM_SERVER_CLOSE.OPCODE;
	            qn.args = largs;
	            lNetWorker.AddQueue(qn);
				node = lNetWorker.tick();
	            if (null != node)
	            {
	                callEvent(node.msg, node.args);
	            }
				close();
			}
			
        }
		
		
		
		public void openLogicServerAgain()
        {
            ManagerNet mgr = ManagerNet.getInstance();
			
			if(null == _LoginSer)
			{
				return;
			}
			string lsServer = "server";
			mgr.addCurServerId();
			int index = mgr.getCurServerId();
			mgr.addFactory(new net.unity3d.UConnNetFactory(lsServer + index.ToString()));
            _XmanLogic = mgr.createNetObject<IConnection>(lsServer + index.ToString());
			Debug.Log("logic ip = "+ m_LogicNote._serverIp);
			Debug.Log("logic port = "+ m_LogicNote._serverProt);
            _XmanLogic.open(m_LogicNote);
			Debug.Log("connect realm again server ip =");
			Debug.Log(m_LogicNote._serverIp);
        }
		
		public void openGuestServer()
		{
			ManagerNet mgr = ManagerNet.getInstance();
			
			try
			{
				mgr.addCurServerId();
				int index = mgr.getCurServerId();
				mgr.addFactory(new net.unity3d.UConnNetFactory("guest" + index.ToString()));
				_GuestSer = mgr.createNetObject<IConnection>("guest" + index.ToString());
				
	            _GuestSer.EventConnected += OnGuestSerConnected;
	            _GuestSer.open(m_GuestNote);
			}
			catch(Exception ex)
			{
				Debug.LogError("create guest conn exp = " + ex);	
				workerNet lNetWorker = workerNet.getInstance();
				NodeQueue node = new NodeQueue();
				RM2C_ON_REALM_SERVER_CLOSE pro = new RM2C_ON_REALM_SERVER_CLOSE();
				pro.uiReason = 1;
				ArgsNetEvent largs = new ArgsNetEvent(pro);
	            NodeQueue qn = new NodeQueue();
	            qn.msg = (ushort)RM2C_ON_REALM_SERVER_CLOSE.OPCODE;
	            qn.args = largs;
	            lNetWorker.AddQueue(qn);
				node = lNetWorker.tick();
	            if (null != node)
	            {
	                callEvent(node.msg, node.args);
	            }
				close();
			}
			Debug.Log("connect guest server ip = " + m_GuestNote._serverIp);
		}
		
		public void openGuestServerAgain()
		{
			ManagerNet mgr = ManagerNet.getInstance();
			
			string lsServer = "server";
			mgr.addCurServerId();
			int index = mgr.getCurServerId();
			mgr.addFactory(new net.unity3d.UConnNetFactory(lsServer + index.ToString()));

			
            _GuestSer = mgr.createNetObject<IConnection>(lsServer + index.ToString());
			_GuestSer.EventConnected += OnGuestSerConnected;
            _GuestSer.open(m_GuestNote);
			
			Debug.Log("connect realm again server ip =");
			Debug.Log(m_GuestNote._serverIp);
		}
		
		public static AgentNet getInstance()
        {
            if (_self == null)
                _self = new AgentNet();

            return _self;
        }
		
		public void OnXmanLogicClosed(object sender, net.ArgsEvent args)
		{
			RM2C_ON_REALM_SERVER_CLOSE pro = new RM2C_ON_REALM_SERVER_CLOSE();
			pro.uiReason = 1;
			ArgsNetEvent largs = new ArgsNetEvent(pro);
            NodeQueue qn = new NodeQueue();
            qn.msg = (ushort)RM2C_ON_REALM_SERVER_CLOSE.OPCODE;
            qn.args = largs;
            workerNet lNetWorker = workerNet.getInstance();
            lNetWorker.AddQueue(qn);
		}
		
		public void OnXmanLogicConnected(object sender, net.ArgsEvent args)
        {
			bool lbTemp = (bool)args.Data;
			if(false == lbTemp)
			{
				return;
			}
            IConnection conn = (IConnection)sender;
            System.Console.WriteLine("connected : {0}:{1}", conn.RemoteIP, conn.RemotePort);
            logicRegisterProtocol(conn.Session);

            RM2C_ON_REALM_SERVER_CONN pro = new RM2C_ON_REALM_SERVER_CONN();
            pro.uiReason = 1;
            ArgsNetEvent largs = new ArgsNetEvent(pro);
            NodeQueue qn = new NodeQueue();
            qn.msg = (ushort)RM2C_ON_REALM_SERVER_CONN.OPCODE;
            qn.args = largs;
            workerNet lNetWorker = workerNet.getInstance();
            lNetWorker.AddQueue(qn);

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

		public void OnGuestSerConnected(object sender, net.ArgsEvent args)
		{
			bool lbTemp = (bool)args.Data;
			if(false == lbTemp)
			{
				return;
			}
            IConnection conn = (IConnection)sender;
            System.Console.WriteLine("guest connected : {0}:{1}", conn.RemoteIP, conn.RemotePort);
            guestRegisterProtocol(conn.Session);
			
			RM2G_ON_GUEST_SERVER_CONN pro = new RM2G_ON_GUEST_SERVER_CONN();
            pro.uiReason = 1;
            ArgsNetEvent largs = new ArgsNetEvent(pro);
            NodeQueue qn = new NodeQueue();
            qn.msg = (ushort)RM2G_ON_GUEST_SERVER_CONN.OPCODE;
            qn.args = largs;
            workerNet lNetWorker = workerNet.getInstance();
            lNetWorker.AddQueue(qn);
		}

        public void OnLoginSerConnected(object sender, net.ArgsEvent args)
        {
			bool lbTemp = (bool)args.Data;
			if(false == lbTemp)
			{
				return;
			}
			
            IConnection conn = (IConnection)sender;
            System.Console.WriteLine("connected : {0}:{1}", conn.RemoteIP, conn.RemotePort);
            loginSerRegisterProtocol(conn.Session);

            LS2C_ON_LOGIN_SERVER_CONN pro = new LS2C_ON_LOGIN_SERVER_CONN();
            pro.uiReason = 1;
            ArgsNetEvent largs = new ArgsNetEvent(pro);
            NodeQueue qn = new NodeQueue();
            qn.msg = (ushort)LS2C_ON_LOGIN_SERVER_CONN.OPCODE;
            qn.args = largs;
            workerNet lNetWorker = workerNet.getInstance();
            lNetWorker.AddQueue(qn);
			
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
			
            IConnection conn = (IConnection)sender;
            System.Console.WriteLine("connected : {0}:{1}", conn.RemoteIP, conn.RemotePort);
            accountSerRegisterProtocol(conn.Session);

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

            /*
			if((_macId.Length == 0)||(_macId == ""))
			{
				getMacId();
			}
			else
			{
				sendAccountLogin();
			}**/		
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

			//Debug.Log(" send account real!! account = " + accountPro.sAccountC2Ac.getAccount() + " session_id = " + accountPro.sAccountC2Ac.getSessionId()
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
            workerNet lNetWorker = workerNet.getInstance();
            lNetWorker.AddQueue(qn);
		}

        public void loginSerRegisterProtocol(ISession Session)
        {
            if (null == Session)
            {
                return;
            }
			Session.addFactoryMethod((ushort)LS2C_LOGIN_RESPONSE.OPCODE , LS2C_LOGIN_RESPONSE.Create);
			///2014-05-29
			Session.addFactoryMethod((ushort)LS2C_SERVER_STATE.OPCODE, LS2C_SERVER_STATE.Create);
        }
		
		public void accountSerRegisterProtocol(ISession Session)
        {
            if (null == Session)
            {
                return;
            }
			Session.addFactoryMethod((ushort)AC2C_ACCOUNT_INFO.OPCODE , AC2C_ACCOUNT_INFO.Create);
			Session.addFactoryMethod((ushort)AC2C_MAC_ID.OPCODE , AC2C_MAC_ID.Create);
			///2015-07-07
			Session.addFactoryMethod((ushort)AC2C_RECENT_SERVER_INFO.OPCODE, AC2C_RECENT_SERVER_INFO.Create);
			
        }

        public void logicRegisterProtocol(ISession Session)
        {
            if (null == Session)
            {
                return;
            }
			///2014-01-18
            Session.addFactoryMethod((ushort)RM2C_ON_REALM_SERVER_CONN.OPCODE, RM2C_ON_REALM_SERVER_CONN.Create);
			Session.addFactoryMethod((ushort)RM2C_ON_REALM_SERVER_CLOSE.OPCODE, RM2C_ON_REALM_SERVER_CLOSE.Create);
			
			Session.addFactoryMethod((ushort)RM2C_MASTER_BASE_INFO.OPCODE, RM2C_MASTER_BASE_INFO.Create);
			Session.addFactoryMethod((ushort)RM2C_PET_INFO_BAG.OPCODE, RM2C_PET_INFO_BAG.Create);
			Session.addFactoryMethod((ushort)RM2C_PET_INFO_BAR.OPCODE, RM2C_PET_INFO_BAR.Create);
			Session.addFactoryMethod((ushort)RM2C_TEAM_INFO.OPCODE, RM2C_TEAM_INFO.Create);
			Session.addFactoryMethod((ushort)RM2C_EQUIP_GROUP.OPCODE, RM2C_EQUIP_GROUP.Create);
			Session.addFactoryMethod((ushort)RM2C_MATERIAL_GROUP.OPCODE, RM2C_MATERIAL_GROUP.Create);
			Session.addFactoryMethod((ushort)RM2C_EQUIPMENT.OPCODE, RM2C_EQUIPMENT.Create);
			Session.addFactoryMethod((ushort)RM2C_FB.OPCODE, RM2C_FB.Create);
			Session.addFactoryMethod((ushort)RM2C_GOTO_FB.OPCODE, RM2C_GOTO_FB.Create);
			Session.addFactoryMethod((ushort)RM2C_LEAVE_FB.OPCODE, RM2C_LEAVE_FB.Create);
			Session.addFactoryMethod((ushort)RM2C_FB_PK_BEGIN.OPCODE, RM2C_FB_PK_BEGIN.Create);
			
			Session.addFactoryMethod((ushort)RM2C_LOGIN.OPCODE, RM2C_LOGIN.Create);
			Session.addFactoryMethod((ushort)RM2C_FRIEND_RECOMMEND.OPCODE, RM2C_FRIEND_RECOMMEND.Create);
			Session.addFactoryMethod((ushort)RM2C_FRIEND_NOTIFY_TIME.OPCODE, RM2C_FRIEND_NOTIFY_TIME.Create);
			Session.addFactoryMethod((ushort)RM2C_FRIEND.OPCODE, RM2C_FRIEND.Create);
			Session.addFactoryMethod((ushort)RM2C_FRIEND_BE_PK_LIST.OPCODE, RM2C_FRIEND_BE_PK_LIST.Create);
			
			Session.addFactoryMethod((ushort)RM2C_FRIEND_FIGHT_SELECT.OPCODE, RM2C_FRIEND_FIGHT_SELECT.Create);
			Session.addFactoryMethod((ushort)RM2C_PET_EAT.OPCODE, RM2C_PET_EAT.Create);
			Session.addFactoryMethod((ushort)RM2C_PET_UP.OPCODE, RM2C_PET_UP.Create);
			Session.addFactoryMethod((ushort)RM2C_SKILL_UP.OPCODE, RM2C_SKILL_UP.Create);
			Session.addFactoryMethod((ushort)RM2C_POWER_ADD.OPCODE, RM2C_POWER_ADD.Create);
			
			///2014-02-25
			Session.addFactoryMethod((ushort)RM2C_FB_REWARD.OPCODE, RM2C_FB_REWARD.Create);
			Session.addFactoryMethod((ushort)RM2C_CREATE_PET.OPCODE, RM2C_CREATE_PET.Create);
			Session.addFactoryMethod((ushort)RM2C_CREATE_EQUIP.OPCODE, RM2C_CREATE_EQUIP.Create);
			Session.addFactoryMethod((ushort)RM2C_CREATE_PIECE.OPCODE, RM2C_CREATE_PIECE.Create);
			
			///2014-03-03
			Session.addFactoryMethod((ushort)RM2C_WORLD_EVENT_TIME.OPCODE, RM2C_WORLD_EVENT_TIME.Create);
			Session.addFactoryMethod((ushort)RM2C_WROLD_BOSS_INFO.OPCODE, RM2C_WROLD_BOSS_INFO.Create);
			Session.addFactoryMethod((ushort)RM2C_GOTO_WORLD_BOSS.OPCODE, RM2C_GOTO_WORLD_BOSS.Create);
			Session.addFactoryMethod((ushort)RM2C_EXCITE_WORLD_BOSS.OPCODE, RM2C_EXCITE_WORLD_BOSS.Create);
			Session.addFactoryMethod((ushort)RM2C_CHECK_WORLD_BOSS_PK.OPCODE, RM2C_CHECK_WORLD_BOSS_PK.Create);
			Session.addFactoryMethod((ushort)RM2C_WROLD_BOSS_PK_REVIVE.OPCODE, RM2C_WROLD_BOSS_PK_REVIVE.Create);
			Session.addFactoryMethod((ushort)RM2C_WORLD_BOSS_REWARD.OPCODE, RM2C_WORLD_BOSS_REWARD.Create);
			Session.addFactoryMethod((ushort)RM2C_LEAVE_WROLD_BOSS.OPCODE, RM2C_LEAVE_WROLD_BOSS.Create);
			
			///2014-03-04
			Session.addFactoryMethod((ushort)RM2C_PK_INFO.OPCODE, RM2C_PK_INFO.Create);
			Session.addFactoryMethod((ushort)RM2C_PK_RANK_LIST.OPCODE, RM2C_PK_RANK_LIST.Create);
			Session.addFactoryMethod((ushort)RM2C_ADD_PK_CNT.OPCODE, RM2C_ADD_PK_CNT.Create);
			Session.addFactoryMethod((ushort)RM2C_UP_LV_NOBILITY.OPCODE, RM2C_UP_LV_NOBILITY.Create);
			Session.addFactoryMethod((ushort)RM2C_GET_REWARD_NOBILITY.OPCODE, RM2C_GET_REWARD_NOBILITY.Create);
			Session.addFactoryMethod((ushort)RM2C_MATE_PK.OPCODE, RM2C_MATE_PK.Create);
			Session.addFactoryMethod((ushort)RM2C_CHECK_PK.OPCODE, RM2C_CHECK_PK.Create);
			Session.addFactoryMethod((ushort)RM2C_UPDATE_PK_TEAM.OPCODE, RM2C_UPDATE_PK_TEAM.Create);
			
			///2014-03-05
			Session.addFactoryMethod((ushort)RM2C_PIECE.OPCODE, RM2C_PIECE.Create);
			Session.addFactoryMethod((ushort)RM2C_PET_BAG_TO_BAR.OPCODE, RM2C_PET_BAG_TO_BAR.Create);
			Session.addFactoryMethod((ushort)RM2C_PET_BAR_TO_BAG.OPCODE, RM2C_PET_BAR_TO_BAG.Create);
			Session.addFactoryMethod((ushort)RM2C_POWER_INFO.OPCODE, RM2C_POWER_INFO.Create);
			Session.addFactoryMethod((ushort)RM2C_EQUIP_PET_NOTIFY.OPCODE, RM2C_EQUIP_PET_NOTIFY.Create);
			Session.addFactoryMethod((ushort)RM2C_PIECE_TO_PET.OPCODE, RM2C_PIECE_TO_PET.Create);
			Session.addFactoryMethod((ushort)RM2C_PIECE_FROM_PET.OPCODE, RM2C_PIECE_FROM_PET.Create);
			Session.addFactoryMethod((ushort)RM2C_PET_SELL.OPCODE, RM2C_PET_SELL.Create);
			Session.addFactoryMethod((ushort)RM2C_PET_SET_SKILL.OPCODE, RM2C_PET_SET_SKILL.Create);
			Session.addFactoryMethod((ushort)RM2C_TEAM_WORK_SET.OPCODE, RM2C_TEAM_WORK_SET.Create);
			
			///2014-03-07
			Session.addFactoryMethod((ushort)RM2C_EQUIP_SELL.OPCODE, RM2C_EQUIP_SELL.Create);
			Session.addFactoryMethod((ushort)RM2C_FB_NOTIFY_INFO.OPCODE, RM2C_FB_NOTIFY_INFO.Create);
			
			///2014-03-10
			Session.addFactoryMethod((ushort)RM2C_SHOW_PET_REWARD.OPCODE, RM2C_SHOW_PET_REWARD.Create);
			Session.addFactoryMethod((ushort)RM2C_SHOW_PIECE_REWARD.OPCODE, RM2C_SHOW_PIECE_REWARD.Create);
			Session.addFactoryMethod((ushort)RM2C_SHOW_EQUIP_REWARD.OPCODE, RM2C_SHOW_EQUIP_REWARD.Create);
			Session.addFactoryMethod((ushort)RM2C_FB_NEW.OPCODE, RM2C_FB_NEW.Create);
			Session.addFactoryMethod((ushort)RM2C_FB_SWEEP.OPCODE, RM2C_FB_SWEEP.Create);
			
			///2014-03-12
			Session.addFactoryMethod((ushort)RM2C_LAND_LIST.OPCODE, RM2C_LAND_LIST.Create);
			Session.addFactoryMethod((ushort)RM2C_LAND_STAR_REWARD.OPCODE, RM2C_LAND_STAR_REWARD.Create);
			
			///2014-03-13
			Session.addFactoryMethod((ushort)RM2C_LAND_NEW.OPCODE, RM2C_LAND_NEW.Create);
			Session.addFactoryMethod((ushort)RM2C_TEAM_SET.OPCODE, RM2C_TEAM_SET.Create);
			
			///2014-03-24
			Session.addFactoryMethod((ushort)RM2C_NEXT_FB.OPCODE, RM2C_NEXT_FB.Create);
			
			///2014-03-25
			Session.addFactoryMethod((ushort)RM2C_EMAIL_LIST.OPCODE, RM2C_EMAIL_LIST.Create);
			Session.addFactoryMethod((ushort)RM2C_CREATE_EMAIL.OPCODE, RM2C_CREATE_EMAIL.Create);
			Session.addFactoryMethod((ushort)RM2C_OPEN_EMAIL.OPCODE, RM2C_OPEN_EMAIL.Create);
			Session.addFactoryMethod((ushort)RM2C_DESTROY_EMAIL.OPCODE, RM2C_DESTROY_EMAIL.Create);
			
			///2014-04-01
			Session.addFactoryMethod((ushort)RM2C_ON_WEB_CLOSE.OPCODE, RM2C_ON_WEB_CLOSE.Create);
			Session.addFactoryMethod((ushort)RM2C_EQUIP_COM.OPCODE, RM2C_EQUIP_COM.Create);
			Session.addFactoryMethod((ushort)RM2C_EQUIP_UP.OPCODE, RM2C_EQUIP_UP.Create);
			Session.addFactoryMethod((ushort)RM2C_EQUIP_UP_ONE_KEY.OPCODE, RM2C_EQUIP_UP_ONE_KEY.Create);
			
			///2014-04-04
			Session.addFactoryMethod((ushort)RM2C_RELOGIN.OPCODE, RM2C_RELOGIN.Create);
			
			///2014-04-08
			Session.addFactoryMethod((ushort)RM2C_FRIEND_ASK.OPCODE, RM2C_FRIEND_ASK.Create);
			Session.addFactoryMethod((ushort)RM2C_FRIEND_ASK_NOTIFY.OPCODE, RM2C_FRIEND_ASK_NOTIFY.Create);
			Session.addFactoryMethod((ushort)RM2C_FRIEND_ASK_ANSWER.OPCODE, RM2C_FRIEND_ASK_ANSWER.Create);
			Session.addFactoryMethod((ushort)RM2C_FRIEND_NEW.OPCODE, RM2C_FRIEND_NEW.Create);
			Session.addFactoryMethod((ushort)RM2C_FRIEND_DELETE.OPCODE, RM2C_FRIEND_DELETE.Create);
			Session.addFactoryMethod((ushort)RM2C_FRIEND_ASK_NOTIFY_DELETE.OPCODE, RM2C_FRIEND_ASK_NOTIFY_DELETE.Create);
			Session.addFactoryMethod((ushort)RM2C_FRIEND_SEARCH.OPCODE, RM2C_FRIEND_SEARCH.Create);
			Session.addFactoryMethod((ushort)RM2C_FRIEND_BUY.OPCODE, RM2C_FRIEND_BUY.Create);
			Session.addFactoryMethod((ushort)RM2C_FRIEND_POWER_SEND.OPCODE, RM2C_FRIEND_POWER_SEND.Create);
			Session.addFactoryMethod((ushort)RM2C_FRIEND_POWER.OPCODE, RM2C_FRIEND_POWER.Create);
			Session.addFactoryMethod((ushort)RM2C_FRIEND_POWER_NEW.OPCODE, RM2C_FRIEND_POWER_NEW.Create);
			Session.addFactoryMethod((ushort)RM2C_FRIEND_POWER_GET.OPCODE, RM2C_FRIEND_POWER_GET.Create);
			Session.addFactoryMethod((ushort)RM2C_PK_CHECK_TEAM.OPCODE, RM2C_PK_CHECK_TEAM.Create);
			


			///2014-04-09
			Session.addFactoryMethod((ushort)RM2C_FRIEND_ASK_LIST.OPCODE, RM2C_FRIEND_ASK_LIST.Create);
			Session.addFactoryMethod((ushort)RM2C_PK_CHECK_TEAM.OPCODE, RM2C_PK_CHECK_TEAM.Create);
			Session.addFactoryMethod((ushort)RM2C_FRIEND_NOTIFY_BE_PK.OPCODE, RM2C_FRIEND_NOTIFY_BE_PK.Create);
			
			///2014-04-11
			Session.addFactoryMethod((ushort)RM2C_ROLE_PSIGN.OPCODE, RM2C_ROLE_PSIGN.Create);
			Session.addFactoryMethod((ushort)RM2C_BIND.OPCODE, RM2C_BIND.Create);
			
			///2014-04-14
			Session.addFactoryMethod((ushort)RM2C_GET_TOWER_INFO.OPCODE, RM2C_GET_TOWER_INFO.Create);
			Session.addFactoryMethod((ushort)RM2C_CHECK_TOWER_PK.OPCODE, RM2C_CHECK_TOWER_PK.Create);
			Session.addFactoryMethod((ushort)RM2C_TOWER_SWEEP.OPCODE, RM2C_TOWER_SWEEP.Create);
			Session.addFactoryMethod((ushort)RM2C_TOWER_REWARD.OPCODE, RM2C_TOWER_REWARD.Create);
			Session.addFactoryMethod((ushort)RM2C_TOWER_DAY_REWARD.OPCODE, RM2C_TOWER_DAY_REWARD.Create);
			Session.addFactoryMethod((ushort)RM2C_TOWER_AGAIN.OPCODE, RM2C_TOWER_AGAIN.Create);
			Session.addFactoryMethod((ushort)RM2C_REFRESH_TOWER_SHOP.OPCODE, RM2C_REFRESH_TOWER_SHOP.Create);
			Session.addFactoryMethod((ushort)RM2C_BUY_TOWER_SHOP.OPCODE, RM2C_BUY_TOWER_SHOP.Create);
			Session.addFactoryMethod((ushort)RM2C_GET_TOWER_TIME.OPCODE, RM2C_GET_TOWER_TIME.Create);
			
			///2014-04-15
			Session.addFactoryMethod((ushort)RM2C_FRIEND_NOFRESH_INFO.OPCODE, RM2C_FRIEND_NOFRESH_INFO.Create);
			Session.addFactoryMethod((ushort)RM2C_FRIEND_FRESH_INFO.OPCODE, RM2C_FRIEND_FRESH_INFO.Create);

			///2014-04-17
			Session.addFactoryMethod((ushort)RM2C_FRIEND_PK_BACK_OVER.OPCODE, RM2C_FRIEND_PK_BACK_OVER.Create);
			Session.addFactoryMethod((ushort)RM2C_FRIEND_PK_OVER.OPCODE, RM2C_FRIEND_PK_OVER.Create);
			Session.addFactoryMethod((ushort)RM2C_FRIEND_PK_BACK.OPCODE, RM2C_FRIEND_PK_BACK.Create);
			Session.addFactoryMethod((ushort)RM2C_FRIEEND_PK.OPCODE, RM2C_FRIEEND_PK.Create);
			Session.addFactoryMethod((ushort)RM2C_POWER_BUY.OPCODE, RM2C_POWER_BUY.Create);
			
			///2014-04-18
			Session.addFactoryMethod((ushort)RM2C_ROLE_NAME.OPCODE, RM2C_ROLE_NAME.Create);				
			Session.addFactoryMethod((ushort)RM2C_FRIEND_NICK_NAME.OPCODE, RM2C_FRIEND_NICK_NAME.Create);		
			Session.addFactoryMethod((ushort)RM2C_TICK_BY_OTHER.OPCODE, RM2C_TICK_BY_OTHER.Create);
			
			///2014-05-10
			Session.addFactoryMethod((ushort)RM2C_NAME_RAND.OPCODE, RM2C_NAME_RAND.Create);
			Session.addFactoryMethod((ushort)RM2C_PK_SHOP_BUY.OPCODE, RM2C_PK_SHOP_BUY.Create);				
			Session.addFactoryMethod((ushort)RM2C_REFRESH_PK_SHOP.OPCODE, RM2C_REFRESH_PK_SHOP.Create);		
			Session.addFactoryMethod((ushort)RM2C_GET_PK_SHOP.OPCODE, RM2C_GET_PK_SHOP.Create);
			Session.addFactoryMethod((ushort)RM2C_ACTIV_EVENT_TIME.OPCODE, RM2C_ACTIV_EVENT_TIME.Create);				
			Session.addFactoryMethod((ushort)RM2C_ACTIV_EVENT_INFO.OPCODE, RM2C_ACTIV_EVENT_INFO.Create);		
			Session.addFactoryMethod((ushort)RM2C_ACTIV_EVENT_BEGIN.OPCODE, RM2C_ACTIV_EVENT_BEGIN.Create);
			Session.addFactoryMethod((ushort)RM2C_CHECK_ACTIV_EVENT.OPCODE, RM2C_CHECK_ACTIV_EVENT.Create);				
			Session.addFactoryMethod((ushort)RM2C_ADD_ACTIV_EVENT_CNT.OPCODE, RM2C_ADD_ACTIV_EVENT_CNT.Create);						
			
			///2014-04-13
			Session.addFactoryMethod((ushort)RM2C_LUCKY_SHOP.OPCODE, RM2C_LUCKY_SHOP.Create);		
			
			///2014-05-14
			Session.addFactoryMethod((ushort)RM2C_GO_TO_ACTIV_EVENT.OPCODE, RM2C_GO_TO_ACTIV_EVENT.Create);				
			Session.addFactoryMethod((ushort)RM2C_LEAVE_ACTIV_EVENT.OPCODE, RM2C_LEAVE_ACTIV_EVENT.Create);	
			///2014-05-15
			Session.addFactoryMethod((ushort)RM2C_RE_PK_FB.OPCODE, RM2C_RE_PK_FB.Create);	
			
			///2014-05-20
			Session.addFactoryMethod((ushort)RM2C_CHARGE.OPCODE, RM2C_CHARGE.Create);	
			Session.addFactoryMethod((ushort)RM2C_CHARGE_ORDER_FINISH.OPCODE, RM2C_CHARGE_ORDER_FINISH.Create);	
			Session.addFactoryMethod((ushort)RM2C_CHARGE_LIST.OPCODE, RM2C_CHARGE_LIST.Create);	
			Session.addFactoryMethod((ushort)RM2C_ORDER_LIST.OPCODE, RM2C_ORDER_LIST.Create);	
			
			///2014-05-21
			Session.addFactoryMethod((ushort)RM2C_PING_PRO.OPCODE, RM2C_PING_PRO.Create);
			
			///2014-05-27
			Session.addFactoryMethod((ushort)RM2C_RE_ACTIV_EVENT.OPCODE, RM2C_RE_ACTIV_EVENT.Create);
			
			///2014-05-28
			Session.addFactoryMethod((ushort)RM2C_CHARGE_CARD.OPCODE, RM2C_CHARGE_CARD.Create);
			///2014-06-18
			Session.addFactoryMethod((ushort)RM2C_FB_RESET.OPCODE, RM2C_FB_RESET.Create);
			Session.addFactoryMethod((ushort)RM2C_STONE.OPCODE, RM2C_STONE.Create);
			
			///2014-06-19	
			Session.addFactoryMethod((ushort)RM2C_BADGE_SHOP_BUY.OPCODE, RM2C_BADGE_SHOP_BUY.Create);
			Session.addFactoryMethod((ushort)RM2C_BAG_BUY.OPCODE, RM2C_BAG_BUY.Create);
			
			///2014-07-08	
			Session.addFactoryMethod((ushort)RM2C_GET_PHONE_INFO.OPCODE, RM2C_GET_PHONE_INFO.Create);
			
			///2014-07-16
			Session.addFactoryMethod((ushort)RM2C_CHECK_FB_PK.OPCODE, RM2C_CHECK_FB_PK.Create);
			///2014-07-17
			Session.addFactoryMethod((ushort)RM2C_SERVER_STOP_NOTE.OPCODE, RM2C_SERVER_STOP_NOTE.Create);
			///2014-08-04
			Session.addFactoryMethod((ushort)RM2C_POWER_MEAL.OPCODE, RM2C_POWER_MEAL.Create);
			///2014-08-07
			Session.addFactoryMethod((ushort)RM2C_POWER_MEAL_TIME.OPCODE, RM2C_POWER_MEAL_TIME.Create);
			
			///2014-08-14
			Session.addFactoryMethod((ushort)RM2C_SIGN_RE.OPCODE, RM2C_SIGN_RE.Create);
			Session.addFactoryMethod((ushort)RM2C_REWARD_MONEY.OPCODE, RM2C_REWARD_MONEY.Create);
			Session.addFactoryMethod((ushort)RM2C_SIGN.OPCODE, RM2C_SIGN.Create);
			
			///2014-08-21
			Session.addFactoryMethod((ushort)RM2C_TASK_LV.OPCODE, RM2C_TASK_LV.Create);
			Session.addFactoryMethod((ushort)RM2C_TASK_EVERYDAY_REWARD.OPCODE, RM2C_TASK_EVERYDAY_REWARD.Create);
			Session.addFactoryMethod((ushort)RM2C_TASK_FB_REWARD.OPCODE, RM2C_TASK_FB_REWARD.Create);
			Session.addFactoryMethod((ushort)RM2C_TASK_GUIDE_REWARD.OPCODE, RM2C_TASK_GUIDE_REWARD.Create);
			Session.addFactoryMethod((ushort)RM2C_TASK.OPCODE, RM2C_TASK.Create);
			///2014-08-23
			Session.addFactoryMethod((ushort)RM2C_PET_LV_UP.OPCODE, RM2C_PET_LV_UP.Create);
			///2014-08-25
			Session.addFactoryMethod((ushort)RM2C_PET_STAR_UP.OPCODE, RM2C_PET_STAR_UP.Create);
			
			///2014-08-26
			Session.addFactoryMethod((ushort)RM2C_SKILL_POINT_BUY.OPCODE, RM2C_SKILL_POINT_BUY.Create);
			Session.addFactoryMethod((ushort)RM2C_SKILL_POINT_ADD.OPCODE, RM2C_SKILL_POINT_ADD.Create);
			Session.addFactoryMethod((ushort)RM2C_SKILL_UP_NEW.OPCODE, RM2C_SKILL_UP_NEW.Create);
			
			///2014-08-30
			Session.addFactoryMethod((ushort)RM2C_LUCKY_SOUL.OPCODE, RM2C_LUCKY_SOUL.Create);
			Session.addFactoryMethod((ushort)RM2C_PET_PIECE_TO_PET.OPCODE, RM2C_PET_PIECE_TO_PET.Create);
			
			///2014-09-02
			Session.addFactoryMethod((ushort)RM2C_STONE_INLAY.OPCODE, RM2C_STONE_INLAY.Create);
			Session.addFactoryMethod((ushort)RM2C_PET_STONE_UP.OPCODE, RM2C_PET_STONE_UP.Create);
			
			///2014-09-03
			Session.addFactoryMethod((ushort)RM2C_TEAM_EXP_ADD.OPCODE, RM2C_TEAM_EXP_ADD.Create);
			
			///2014-09-5
			Session.addFactoryMethod((ushort)RM2C_PET_PIECE_SELL.OPCODE, RM2C_PET_PIECE_SELL.Create);
			
			Session.addFactoryMethod((ushort)RM2C_SHOW_SWEEP_REWARD.OPCODE, RM2C_SHOW_SWEEP_REWARD.Create);
			///2014-09-12
			Session.addFactoryMethod((ushort)RM2C_FRIEND_HELPER.OPCODE, RM2C_FRIEND_HELPER.Create);
			Session.addFactoryMethod((ushort)RM2C_FRIEND_FIGHT_HISTORY.OPCODE, RM2C_FRIEND_FIGHT_HISTORY.Create);
			
			///2014-09-28
			Session.addFactoryMethod((ushort)RM2C_WEB_EMAIL_OPEN.OPCODE, RM2C_WEB_EMAIL_OPEN.Create);
			Session.addFactoryMethod((ushort)RM2C_WEB_EMAIL.OPCODE, RM2C_WEB_EMAIL.Create);
			Session.addFactoryMethod((ushort)RM2C_WEB_EMAIL_NOTIFY_NEW.OPCODE, RM2C_WEB_EMAIL_NOTIFY_NEW.Create);
			Session.addFactoryMethod((ushort)RM2C_NOTIFY_LEAD_FB.OPCODE, RM2C_NOTIFY_LEAD_FB.Create);
			
			///2014-10-08
			Session.addFactoryMethod((ushort)RM2C_TEAM_NOTIFY.OPCODE, RM2C_TEAM_NOTIFY.Create);
			
			///2014-10-14
			Session.addFactoryMethod((ushort)RM2C_BOSS_UPDATE.OPCODE, RM2C_BOSS_UPDATE.Create);
			Session.addFactoryMethod((ushort)RM2C_BOSS_TIME_OUT.OPCODE, RM2C_BOSS_TIME_OUT.Create);
			Session.addFactoryMethod((ushort)RM2C_BOSS_INFO.OPCODE, RM2C_BOSS_INFO.Create);
			Session.addFactoryMethod((ushort)RM2C_BOSS_GOTO.OPCODE, RM2C_BOSS_GOTO .Create);
			
			///2014-10-16
			Session.addFactoryMethod((ushort)RM2C_BOSS_MONEY_REWARD.OPCODE, RM2C_BOSS_MONEY_REWARD .Create);
			Session.addFactoryMethod((ushort)RM2C_BOSS_VALUE.OPCODE, RM2C_BOSS_VALUE .Create);
			Session.addFactoryMethod((ushort)RM2C_BOSS_CHECK_ERROR.OPCODE, RM2C_BOSS_CHECK_ERROR.Create);
			
			///2014-10-18
			Session.addFactoryMethod((ushort)RM2C_BOSS_VALUE_REWARD_END.OPCODE, RM2C_BOSS_VALUE_REWARD_END.Create);
			Session.addFactoryMethod((ushort)RM2C_BOSS_VALUE_REWARD_BEGIN.OPCODE, RM2C_BOSS_VALUE_REWARD_BEGIN.Create);
			
			///2014-10-23
			Session.addFactoryMethod((ushort)RM2C_MOT_GOTO.OPCODE, RM2C_MOT_GOTO.Create);
			Session.addFactoryMethod((ushort)RM2C_MOT_BUY_BUFFER.OPCODE, RM2C_MOT_BUY_BUFFER.Create);
			Session.addFactoryMethod((ushort)RM2C_MOT_BOX.OPCODE, RM2C_MOT_BOX.Create);
			Session.addFactoryMethod((ushort)RM2C_MOT_REWARD.OPCODE, RM2C_MOT_REWARD.Create);
			Session.addFactoryMethod((ushort)RM2C_MOT_RESET.OPCODE, RM2C_MOT_RESET.Create);
			Session.addFactoryMethod((ushort)RM2C_MOT_PET.OPCODE, RM2C_MOT_PET.Create);
			Session.addFactoryMethod((ushort)RM2C_MOT_TEAM.OPCODE, RM2C_MOT_TEAM.Create);
			Session.addFactoryMethod((ushort)RM2C_MOT_INFO.OPCODE, RM2C_MOT_INFO.Create);
			Session.addFactoryMethod((ushort)RM2C_MOT_TEAM_UPDATE.OPCODE, RM2C_MOT_TEAM_UPDATE.Create);
			///2014-10-24
			Session.addFactoryMethod((ushort)RM2C_MOT_PK_ERROR.OPCODE, RM2C_MOT_PK_ERROR.Create);
			Session.addFactoryMethod((ushort)RM2C_MOT_HELPER_INFO.OPCODE, RM2C_MOT_HELPER_INFO.Create);
			Session.addFactoryMethod((ushort)RM2C_MOT_HELPER.OPCODE, RM2C_MOT_HELPER.Create);
			
			///2014-10-27
			Session.addFactoryMethod((ushort)RM2C_MOUNTAIN_SHOP_BUY.OPCODE, RM2C_MOUNTAIN_SHOP_BUY.Create);
			Session.addFactoryMethod((ushort)RM2C_REFRESH_MOUNTAIN_SHOP.OPCODE, RM2C_REFRESH_MOUNTAIN_SHOP.Create);
			Session.addFactoryMethod((ushort)RM2C_GET_MOUNTAIN_SHOP.OPCODE, RM2C_GET_MOUNTAIN_SHOP.Create);
			
			///2014-10-28
			Session.addFactoryMethod((ushort)RM2C_MOT_LEAVE.OPCODE, RM2C_MOT_LEAVE.Create);
			///2014-10-29
			Session.addFactoryMethod((ushort)RM2C_MOT_HELPER_IS_IN_TEAM.OPCODE, RM2C_MOT_HELPER_IS_IN_TEAM.Create);
			///2014-10-30
			Session.addFactoryMethod((ushort)RM2C_WEB_EMAIL_TEXT.OPCODE, RM2C_WEB_EMAIL_TEXT.Create);
			
			///2014-11-29
			Session.addFactoryMethod((ushort)RM2C_SMONEY_SHOP_BUY.OPCODE, RM2C_SMONEY_SHOP_BUY.Create);
			Session.addFactoryMethod((ushort)RM2C_REFRESH_SMONEY_SHOP.OPCODE, RM2C_REFRESH_SMONEY_SHOP.Create);
			Session.addFactoryMethod((ushort)RM2C_GET_SMONEY_SHOP.OPCODE, RM2C_GET_SMONEY_SHOP.Create);
			
			///2014-12-18
			Session.addFactoryMethod((ushort)RM2C_PING_PRO_TWO.OPCODE, RM2C_PING_PRO_TWO.Create);
			
			///2015-01-08
			Session.addFactoryMethod((ushort)RM2C_MIN_NIGHT_REFRESH.OPCODE, RM2C_MIN_NIGHT_REFRESH.Create);

			///2015-01-19
			Session.addFactoryMethod((ushort)RM2C_POWER_LV_ADD.OPCODE, RM2C_POWER_LV_ADD.Create);

			///2015-03-07
			Session.addFactoryMethod((ushort)RM2C_BEAST_INFO.OPCODE, RM2C_BEAST_INFO.Create);
			Session.addFactoryMethod((ushort)RM2C_BEAST_LV_UP.OPCODE, RM2C_BEAST_LV_UP.Create);
			Session.addFactoryMethod((ushort)RM2C_NEW_BEAST.OPCODE, RM2C_NEW_BEAST.Create);
			Session.addFactoryMethod((ushort)RM2C_BEAST_EQUIP_WEAR.OPCODE, RM2C_BEAST_EQUIP_WEAR.Create);
			Session.addFactoryMethod((ushort)RM2C_BEAST_EQUIP_UP.OPCODE, RM2C_BEAST_EQUIP_UP.Create);

			///2015-03-14
			Session.addFactoryMethod((ushort)RM2C_SOUL_SHOP.OPCODE, RM2C_SOUL_SHOP.Create);
			Session.addFactoryMethod((ushort)RM2C_SOUL_SHOP_BUY.OPCODE, RM2C_SOUL_SHOP_BUY.Create);
			Session.addFactoryMethod((ushort)RM2C_SOUL_COM.OPCODE, RM2C_SOUL_COM.Create);

			///2015-03-19
			Session.addFactoryMethod((ushort)RM2C_SUN_WELL_INFO.OPCODE, RM2C_SUN_WELL_INFO.Create);
			Session.addFactoryMethod((ushort)RM2C_SUN_WELL_RESET.OPCODE, RM2C_SUN_WELL_RESET.Create);

			///2015-03-20
			Session.addFactoryMethod((ushort)RM2C_OPEN_GIFT_BOX.OPCODE, RM2C_OPEN_GIFT_BOX.Create);
			Session.addFactoryMethod((ushort)RM2C_MOT_BEAST.OPCODE, RM2C_MOT_BEAST.Create);

			///2015-03-24
			Session.addFactoryMethod((ushort)RM2C_CHAT.OPCODE, RM2C_CHAT.Create);
			Session.addFactoryMethod((ushort)RM2C_CHAT_RECENT_RESPONSE.OPCODE, RM2C_CHAT_RECENT_RESPONSE.Create);

			///2015-04-03
			Session.addFactoryMethod((ushort)RM2C_ESCORT_INFO.OPCODE, RM2C_ESCORT_INFO.Create);
			Session.addFactoryMethod((ushort)RM2C_EESCORT_GROUP_LIST.OPCODE, RM2C_EESCORT_GROUP_LIST.Create);
			Session.addFactoryMethod((ushort)RM2C_ESCORT_GET_GROUP.OPCODE, RM2C_ESCORT_GET_GROUP.Create);
			Session.addFactoryMethod((ushort)RM2C_ESCORT_GET_TEAM.OPCODE, RM2C_ESCORT_GET_TEAM.Create);
			Session.addFactoryMethod((ushort)RM2C_ESCORT_BEAT_SELF_PET.OPCODE, RM2C_ESCORT_BEAT_SELF_PET.Create);
			Session.addFactoryMethod((ushort)RM2C_ESCORT_BEAT_SELF_BEAST.OPCODE, RM2C_ESCORT_BEAT_SELF_BEAST.Create);
			Session.addFactoryMethod((ushort)RM2C_ESCORT_BEAT_GROUP.OPCODE, RM2C_ESCORT_BEAT_GROUP.Create);
			Session.addFactoryMethod((ushort)RM2C_ESCORT_BEAT_TEAM.OPCODE, RM2C_ESCORT_BEAT_TEAM.Create);
			Session.addFactoryMethod((ushort)RM2C_ESCORT_FIND_GROUP.OPCODE, RM2C_ESCORT_FIND_GROUP.Create);
			Session.addFactoryMethod((ushort)RM2C_ESCORT_SET_SELF_TEAM.OPCODE, RM2C_ESCORT_SET_SELF_TEAM.Create);
			Session.addFactoryMethod((ushort)RM2C_ESCORT_GO_TO.OPCODE, RM2C_ESCORT_GO_TO.Create);
			Session.addFactoryMethod((ushort)RM2C_ESCORT_LEAVE.OPCODE, RM2C_ESCORT_LEAVE.Create);
			Session.addFactoryMethod((ushort)RM2C_ESCORT_HELPER.OPCODE, RM2C_ESCORT_HELPER.Create);
			Session.addFactoryMethod((ushort)RM2C_ESCORT_HELPER_INFO.OPCODE, RM2C_ESCORT_HELPER_INFO.Create);
			Session.addFactoryMethod((ushort)RM2C_ESCORT_BEAT_GROUP_ACT.OPCODE, RM2C_ESCORT_BEAT_GROUP_ACT.Create);
			Session.addFactoryMethod((ushort)RM2C_ESCORT_PK_OVER.OPCODE, RM2C_ESCORT_PK_OVER.Create);
			Session.addFactoryMethod((ushort)RM2C_ESCORT_ADD_BREAD.OPCODE, RM2C_ESCORT_ADD_BREAD.Create);
			Session.addFactoryMethod((ushort)RM2C_ESCORT_BUY_BREAD.OPCODE, RM2C_ESCORT_BUY_BREAD.Create);
			Session.addFactoryMethod((ushort)RM2C_ESCORT_LOG_GET_BREAD.OPCODE, RM2C_ESCORT_LOG_GET_BREAD.Create);
			Session.addFactoryMethod((ushort)RM2C_ESCORT_MAKE_SURE_FAIL.OPCODE, RM2C_ESCORT_MAKE_SURE_FAIL.Create);
			Session.addFactoryMethod((ushort)RM2C_ESCORT_LOG.OPCODE, RM2C_ESCORT_LOG.Create);
			Session.addFactoryMethod((ushort)RM2C_ESCORT_PK_GROUP.OPCODE, RM2C_ESCORT_PK_GROUP.Create);
			Session.addFactoryMethod((ushort)RM2C_ESCORT_GIVE_UP.OPCODE, RM2C_ESCORT_GIVE_UP.Create);
			Session.addFactoryMethod((ushort)RM2C_ESCORT_PK_GROUP.OPCODE, RM2C_ESCORT_PK_GROUP.Create);
			Session.addFactoryMethod((ushort)RM2C_ESCORT_CREAT_LOG.OPCODE, RM2C_ESCORT_CREAT_LOG.Create);
			Session.addFactoryMethod((ushort)RM2C_ESCORT_SUCCEED.OPCODE, RM2C_ESCORT_SUCCEED.Create);

			///2015-04-13
			Session.addFactoryMethod((ushort)RM2C_MY_MOT_PK_INFO.OPCODE, RM2C_MY_MOT_PK_INFO.Create);
			Session.addFactoryMethod((ushort)RM2C_MOT_PK_ENEMY_INFO.OPCODE, RM2C_MOT_PK_ENEMY_INFO.Create);
			Session.addFactoryMethod((ushort)RM2C_MOT_PK_RANK_LIST.OPCODE, RM2C_MOT_PK_RANK_LIST.Create);
			Session.addFactoryMethod((ushort)RM2C_JOIN_MOT_PK.OPCODE, RM2C_JOIN_MOT_PK.Create);
			Session.addFactoryMethod((ushort)RM2C_MOT_PK_HELPER.OPCODE, RM2C_MOT_PK_HELPER.Create);
			Session.addFactoryMethod((ushort)RM2C_MOT_PK_BOX.OPCODE, RM2C_MOT_PK_BOX.Create);
			Session.addFactoryMethod((ushort)RM2C_MOT_PK_GOTO.OPCODE, RM2C_MOT_PK_GOTO.Create);
			Session.addFactoryMethod((ushort)RM2C_MOT_PK_REWARD.OPCODE, RM2C_MOT_PK_REWARD.Create);
			Session.addFactoryMethod((ushort)RM2C_MOT_PK_PK_ERROR.OPCODE, RM2C_MOT_PK_PK_ERROR.Create);
			Session.addFactoryMethod((ushort)RM2C_MOT_PK_LEAVE.OPCODE, RM2C_MOT_PK_LEAVE.Create);

			///2015-04-20
			Session.addFactoryMethod((ushort)RM2C_SIGN_BEAST_REQUEST.OPCODE, RM2C_SIGN_BEAST_REQUEST.Create);
			///2015-04-22
			Session.addFactoryMethod((ushort)RM2C_MOT_PK_REWARD_PKED.OPCODE, RM2C_MOT_PK_REWARD_PKED.Create);
			///2015-04-22
			Session.addFactoryMethod((ushort)RM2C_BOSS_RESET.OPCODE, RM2C_BOSS_RESET.Create);

			///2015-05-07
			Session.addFactoryMethod((ushort)RM2C_GET_ACTIVITY_INFO.OPCODE, RM2C_GET_ACTIVITY_INFO.Create);
			Session.addFactoryMethod((ushort)RM2C_GET_ACTIVITY_REWARD.OPCODE, RM2C_GET_ACTIVITY_REWARD.Create);

			///2015_05_15
			Session.addFactoryMethod((ushort)RM2C_ESCORT_BEAT_GROUP_TIME_OUT.OPCODE, RM2C_ESCORT_BEAT_GROUP_TIME_OUT.Create);

			///2015-06-01
			Session.addFactoryMethod((ushort)RM2C_FIRST_DAY_BUY_POWER_ACT_INFO.OPCODE, RM2C_FIRST_DAY_BUY_POWER_ACT_INFO.Create);
			Session.addFactoryMethod((ushort)RM2C_FIRST_DAY_BUY_POWER_ACT_REWARD.OPCODE, RM2C_FIRST_DAY_BUY_POWER_ACT_REWARD.Create);

			///2015-06-23
			Session.addFactoryMethod((ushort)RM2C_SEND_SERVER_ID.OPCODE, RM2C_SEND_SERVER_ID.Create);

			///2015-08-03
			Session.addFactoryMethod((ushort)RM2C_GU_MEMBER_LIST.OPCODE, RM2C_GU_MEMBER_LIST.Create);
			Session.addFactoryMethod((ushort)RM2C_GET_GAME_UNION_INFO.OPCODE, RM2C_GET_GAME_UNION_INFO.Create);
			Session.addFactoryMethod((ushort)RM2C_GAME_UNION_LIST.OPCODE, RM2C_GAME_UNION_LIST.Create);
			Session.addFactoryMethod((ushort)RM2C_SEARCH_GAME_UNION.OPCODE, RM2C_SEARCH_GAME_UNION.Create);
			Session.addFactoryMethod((ushort)RM2C_CREATE_GAME_UNION.OPCODE, RM2C_CREATE_GAME_UNION.Create);
			Session.addFactoryMethod((ushort)RM2C_OPEN_SET_GAME_UNION.OPCODE, RM2C_OPEN_SET_GAME_UNION.Create);
			Session.addFactoryMethod((ushort)RM2C_SET_GAME_UNION.OPCODE, RM2C_SET_GAME_UNION.Create);
			Session.addFactoryMethod((ushort)RM2C_REQUEST_JOIN_GAME_UNION.OPCODE, RM2C_REQUEST_JOIN_GAME_UNION.Create);
			Session.addFactoryMethod((ushort)RM2C_REQUEST_JOINER_LIST.OPCODE, RM2C_REQUEST_JOINER_LIST.Create);
			Session.addFactoryMethod((ushort)RM2C_AGREEN_JOIN.OPCODE, RM2C_AGREEN_JOIN.Create);
			Session.addFactoryMethod((ushort)RM2C_APPOINT_JOB.OPCODE, RM2C_APPOINT_JOB.Create);
			Session.addFactoryMethod((ushort)RM2C_EMAIL_ALL_MEMBER.OPCODE, RM2C_EMAIL_ALL_MEMBER.Create);
			Session.addFactoryMethod((ushort)RM2C_DISBAND_GAME_UNION.OPCODE, RM2C_DISBAND_GAME_UNION.Create);
			Session.addFactoryMethod((ushort)RM2C_GET_GAME_UNION_LOG.OPCODE, RM2C_GET_GAME_UNION_LOG.Create);
			Session.addFactoryMethod((ushort)RM2C_NOTIFY_LEAD_GU_INFO.OPCODE, RM2C_NOTIFY_LEAD_GU_INFO.Create);

			///2015-08-06
			Session.addFactoryMethod((ushort)RM2C_STOP_INFO.OPCODE, RM2C_STOP_INFO.Create);

		}
		

		public void guestRegisterProtocol(ISession Session)
		{
            if (null == Session)
            {
                return;
            }		

		}
		
        public bool isListenPro(ushort msg)
        {
            if (_event_listeners.ContainsKey(msg))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
		
		public void bingAccountSer()
		{
			ManagerNet mgr = ManagerNet.getInstance();
			mgr.addFactory(new net.unity3d.UConnNetFactory(accountServerId));
			_AccountSer = mgr.createNetObject<IConnection>(accountServerId);
			NoteServer sAccount = mgr.getAccountNoteServer();

            sAccount._serverIp = "192.168.1.2";
            sAccount._serverProt = 11111;
            sAccount._timeOut = 30;
            sAccount._serverId = 2;

			_AccountSer.EventConnected += OnAccountSerConnected;
			_AccountSer.open(sAccount);
		}

		public void getRelamInfo(string psAccount)
		{
			ManagerNet mgr = ManagerNet.getInstance();
			string lsServer = "server";
			mgr.addCurServerId();
			int index = mgr.getCurServerId();
			mgr.addFactory(new net.unity3d.UConnNetFactory(lsServer + index.ToString()));
            _LoginSer = mgr.createNetObject<IConnection>(lsServer + index.ToString());
	        NoteServer sLogin = mgr.getNoteServer(0);
            //TODO
            sLogin._serverId = 3;
            sLogin._serverIp = "192.168.1.2";
            sLogin._serverProt = 12007;
            sLogin._timeOut = 30;

	        _LoginSer.EventConnected += OnLoginSerConnected;
	        _LoginSer.open(sLogin);
			//_account = psAccount;
		}
		
		public void getRelamInfoByAreaId (string channelId, string accountId, int area)
		{
			ManagerNet mgr = ManagerNet.getInstance();
			string lsServer = "server";
			mgr.addCurServerId();
			int index = mgr.getCurServerId();
			Debug.Log("getRealmInfobyareaid set acount id = " + accountId);
			_channelId = channelId;
			_accountId = accountId;
			mgr.addFactory(new net.unity3d.UConnNetFactory(lsServer + index.ToString()));
			_LoginSer = mgr.createNetObject<IConnection>(lsServer + index.ToString());
	         NoteServer sLogin = mgr.getNoteServer(area);
	        _LoginSer.EventConnected += OnLoginSerConnected;
	        _LoginSer.open(sLogin);
		}

        public void callEvent(ushort msg, ArgsNetEvent args)
        {
            if (_event_listeners.ContainsKey(msg))
            {
                _event_listeners[msg].callEvent(args);
            }
            
        }

        //public void 

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
			workerNet lNetWorker = workerNet.getInstance();
			NodeQueue node = new NodeQueue();
			if(null != _XmanLogic)
			{
				if(true == _XmanLogic.is_error())
				{
					RM2C_ON_REALM_SERVER_CLOSE pro = new RM2C_ON_REALM_SERVER_CLOSE();
					pro.uiReason = 1;
					ArgsNetEvent largs = new ArgsNetEvent(pro);
		            NodeQueue qn = new NodeQueue();
		            qn.msg = (ushort)RM2C_ON_REALM_SERVER_CLOSE.OPCODE;
		            qn.args = largs;
		            lNetWorker.AddQueue(qn);
					node = lNetWorker.tick();
		            if (null != node)
		            {
		                //callEvent(node.msg, node.args);
		            }
					return;
					//close();
				}
				else if(true == _XmanLogic.is_relogin())
				{
					RM2C_ON_WEB_CLOSE pro = new RM2C_ON_WEB_CLOSE();
					pro.uiReason = 1;
					ArgsNetEvent largs = new ArgsNetEvent(pro);
		            NodeQueue qn = new NodeQueue();
		            qn.msg = (ushort)RM2C_ON_WEB_CLOSE.OPCODE;
		            qn.args = largs;
		            lNetWorker.AddQueue(qn);
					node = lNetWorker.tick();
					_XmanLogic.set_is_relogin(false);
					
		            if (null != node)
		            {
		                callEvent(node.msg, node.args);
		            }
					return;
				}
			}

            node = lNetWorker.tick();
            if (null != node)
            {
				set_is_ping_back(true);
				ARequestOverTime = DateTime.Now; //现在时间
				//Debug.Log("ping request over time = " + ARequestOverTime);
                callEvent(node.msg, node.args);
            }
        }
		
		public bool send(IProtocal pro)
        {

            if (pro.Message >= 200 && pro.Message <= 1999)
            {
				if(pro.Message == 322)
				{
					set_is_ping_back(false);
				}
				ARequestTime = DateTime.Now; //现在时间
				
				//Debug.Log("ping request time = " + ARequestTime);
				if(_XmanLogic != null)
				{
					return _XmanLogic.send(pro, false);
				}
				else
				{
					Debug.LogError("_XmanLogic is null!");
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
					Debug.LogError("_LoginSer is null!");
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
					Debug.LogError("_AccountSer is null!");
				}
			}
			return false;
        }

        public IConnection getLoginSer()
        {
            return _LoginSer;
        }

        public IConnection getXmanLogic()
        {
            return _XmanLogic;
        }
		
		public IConnection getGuestSer()
		{
			return _GuestSer;
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
			//_GuestSer.close();
			_AccountSer = null;
			_LoginSer = null;
			_XmanLogic = null;
			_GuestSer = null;
		}
		
		public bool isLogicConnected()
		{
			return _XmanLogic.isConnected();
		}

		public bool isGuestConnected()
		{
			return _GuestSer.isConnected();
		}
		
		public void initGuest(string ip, short port, int timeout)
		{
			m_GuestNote._serverIp = ip;
			m_GuestNote._serverProt = port;
			m_GuestNote._timeOut = timeout;
		}
		
		public void initLogic(string ip, short port, int timeout)
		{
			m_LogicNote._serverIp = ip;
			m_LogicNote._serverProt = port;
			m_LogicNote._timeOut = timeout;
		}
		
		public bool is_ping_back()
		{
			return _is_ping_back;
		}
		
		public void set_is_ping_back(bool pbIsPingBack)
		{
			_is_ping_back = pbIsPingBack;
		}
		
		public bool is_ping_out_time()
		{
			bool lbRes = false;
			TimeSpan ts1 = new TimeSpan(ARequestTime.Ticks);  
	        TimeSpan ts2 = new TimeSpan(ARequestOverTime.Ticks);  
	        TimeSpan ts = ts1.Subtract(ts2).Duration();  
	        
			int liDTime = ts.Seconds + ts.Minutes * 60 + ts.Hours * 60 * 60 + ts.Days * 60 * 60 * 24;
			//Debug.Log("ping wait DTime = " + liDTime);
			///如果3秒钟还没有收到异步调用回复，认为网络断开
			if(liDTime >= 2.1)
			{
				lbRes = true;
			}
			return lbRes;
		}
		
		public bool is_bad_web()
		{
			bool lbRes = false;
			if(!_is_ping_back)
			{
				if(is_ping_out_time())
				{
					lbRes = true;
				}
			}
			return lbRes;
		}
		
		
		private bool _is_ping_back = true;

		
		private System.DateTime ARequestTime =new System.DateTime();
		private System.DateTime ARequestOverTime =new System.DateTime();
		
		private NoteServer m_GuestNote;
		private NoteServer m_LogicNote;

		private IConnection _AccountSer = null;
        private IConnection _LoginSer = null;
        private IConnection _XmanLogic = null;
		private IConnection _GuestSer = null;
		
		
		private string _account;
		private string _password;
		private string _macId;
		private byte _serverType;
		private string _webSession;
		private string _channelId;
		private string _accountId;

        private workerNet _netWorker = new workerNet();

        private Dictionary<ushort, EListenerNet> _event_listeners = new Dictionary<ushort, EListenerNet>();

        private static AgentNet _self;

    }
	
}
