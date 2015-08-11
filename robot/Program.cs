using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading;
using net;
using net.unity3d;

namespace robot
{
    class Program
    {
        public static AgentNet XmanNetAgent;

        static void Main( string[] args )
        {
            Console.WriteLine("===========================================================");
            Console.WriteLine( "stress Test for GameServer" );
            Console.WriteLine( "===========================================================" );
            Console.WriteLine();

            XmanNetAgent = AgentNet.getInstance();

            AgentNet.getInstance().close();
            ///设置登陆信息
            AgentNet.getInstance().setLoginInfo( "2001", "123", "012345", ( byte ) 1, "", "", "");
            AgentNet.getInstance().bingAccountSer();

            initListener();  //消息监听注册

            while( true )
            {
                Thread.Sleep( 1000 );
                XmanNetAgent.tick();
            }

            Console.ReadKey( true );
        }



        /// account返回
        protected static void recvAccountServer( ArgsEvent args )
        {
            AC2C_ACCOUNT_INFO recv = args.getData<AC2C_ACCOUNT_INFO>();

            AgentNet.getInstance().getRelamInfoByAreaId(recv.sAccountAC2C.getChannelIdStr(), recv.sAccountAC2C.getAccountIdStr(), 1); //getRelamInfo("2002");

            if( recv.iResult == 1 )
            {
                //ManagerServer.getInstance().lastLoginServerIndex = ( int ) recv.sAccountAC2C.uiServer[ 0 ];
            }
           
            //ManagerServer.getInstance().AccountId = recv.sAccountAC2C.getAccountIdStr();

            Console.WriteLine( "RECV SERVER ACCOUNT = " + recv.sAccountAC2C.getAccountIdStr() );

            //ManagerNet mgr = ManagerNet.getInstance();
            //mgr.removeFactory( AgentNet.accountServerId );

            //ManagerServer.getInstance().recvAccount( recv.iResult );

        }

        /// login返回
        protected static void recvLoginServer( ArgsEvent args )
        {
            LS2C_LOGIN_RESPONSE recv = args.getData<LS2C_LOGIN_RESPONSE>();

            Console.WriteLine( "LS2C_LOGIN_RESPONSE >> " + recv.iResult.ToString() );
            if( recv.iResult == 1 )
            {
                Console.WriteLine( "Login Authentication Success" );
                Console.WriteLine( "realm server info: " + recv.getIp() + ":" + recv.port);
       
                net.unity3d.AgentNet.getInstance().initLogic( recv.getIp(), ( short ) recv.port, ( int ) recv.timeOut );
                //ManagerServer.getInstance().connecteRealm();
                //AgentNet.getInstance().openLogicServer( _channelID, AccountId, _account );
                AgentNet.getInstance().openLogicServer( "chan1", "2002", "12345" );
            }
            else
            {
                Console.WriteLine( " recvLoginServer error, iresult = " + recv.iResult );
            }
        }

        ///协议返回 realm返回
        protected static void recvLogin( ArgsEvent args )
        {
            RM2C_LOGIN recv = args.getData<RM2C_LOGIN>();
            
            Console.WriteLine( "connect Realm Result: " + recv.iResult );

            if( recv.iResult == ( int ) EM_CLIENT_ERRORCODE.EE_M2C_NEED_CREATE_ROLE )
            {
                Console.WriteLine( "需要创建角色" );
                //ManagerServer.isLoadStatueNew = true;
                /// 剧情
                //ManagerSenceAutoCombat.start( 1 );

                //			/// 直接创建角色s
                //		DataModeServer.sendCreatRole(61, null);

                //			/// 原先选择角色ui
                //			WindowsMngr.getInstance().openWindow(WindowsID.CREAT);
            }
            else if( recv.iResult == ( int ) EM_CLIENT_ERRORCODE.EE_ACCOUNT_NOT_EXIST )
            {
                Console.WriteLine("Realm msg: 帐号不存在");
            }
            
        }

        ///用户基本信息 last update in 14.3.28 by yxh
        protected static void recvMaster( ArgsEvent args )
        {
            RM2C_MASTER_BASE_INFO recv = args.getData<RM2C_MASTER_BASE_INFO>();
            Console.WriteLine( "RECV:RM2C_MASTER_BASE_INFO >> " + recv.SPlayerInfo.sPlayerBaseInfo.uiIdMaster );

            SPlayerInfo playerInfo = recv.SPlayerInfo;

            Console.WriteLine( "玩家基本信息： " );
            Console.WriteLine( "=========================================" );
            Console.WriteLine( "player name: " + playerInfo.sPlayerBaseInfo.getMasterName() );
            Console.WriteLine( "player idServer: " + playerInfo.sPlayerBaseInfo.uiIdMaster );
            Console.WriteLine( "player vip: " + playerInfo.sPlayerBaseInfo.uiVip );
            Console.WriteLine( "player exp: " + playerInfo.sPlayerBaseInfo.luiExp );
            Console.WriteLine( "player power: " + playerInfo.sLeadPowerInfo.usPower );
            Console.WriteLine( "=========================================" );

            
            //TODO ...

        }


        /// 初始化真挺
        public static void initListener()
        {
            ///account返回
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_AC2C_ACCOUNT_INFO, recvAccountServer );
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_AC2C_MAC_ID, recvMacIdServer );

            ///login返回
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_LS2C_LOGIN_RESPONSE, recvLoginServer );

            
            ///断线之后的重连
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_ON_WEB_CLOSE, recvWebClose );

            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_RELOGIN, recvWebOpen );

            ///返回登陆 realmF
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_LOGIN, recvLogin );

            
            ///被踢掉通知客户端
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_TICK_BY_OTHER, recvTick );

            //禁言
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_STOP_INFO, recvStopInfo );

            ///用户基本信息
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MASTER_BASE_INFO, recvMaster );

            /**
            ///发送背包宠物信息
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PET_INFO_BAG, recvHeroBag );

            ///发送酒馆宠物信息
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PET_INFO_BAR, recvHeroWarehouse );

            ///发送队伍信息
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_TEAM_INFO, recvHeroTeam );

            ///发送装备材料组
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_EQUIP_GROUP, recvBagEquip );

            ///发送材料组
            //		AgentNet.getInstance().addListenEvent((ushort)E_OPCODE.EP_RM2C_EQUIP_GROUP, recvBagStuff);

            ///发送装备信息
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_EQUIPMENT, recvHeroEquip );

            ///上阵好友信息
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_FRIEND_RECOMMEND, recvFriendRecommend );
            ///邀请好友上阵返回
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_FRIEND_FIGHT_SELECT, recvFriendSelect );

            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_NEXT_FB, recvNextFB );

            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_EQUIP_UP, recvEquipUp );

            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_EQUIP_COM, recvEquipCreat );

            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_EQUIP_UP_ONE_KEY, recvEquipUpAll );

            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_BIND, recvBingAccount );

            //		AgentNet.getInstance().addListenEvent((ushort)E_OPCODE.EP_RM2C_TICK_BY_OTHER, recvTickByOther);

            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_NAME_RAND, recvRandomName );

            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_ROLE_NAME, recvChangeName );

            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_LUCKY_SHOP, recvLuckShop );

            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_LS2C_SERVER_STATE, recvServerState );

            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_GET_PHONE_INFO, recvPhoneInfo );

            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PING_PRO, recvPingPRO );

            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_SIGN, recvSign );

            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_SIGN_RE, recvSignRe );

            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_REWARD_MONEY, recvSignReward );

            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_TASK, recvTask );

            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_TASK_GUIDE_REWARD, recvTaskNewReward );

            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_TASK_EVERYDAY_REWARD, recvTaskEveryDayReward );

            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_TASK_FB_REWARD, recvTaskFBReward );

            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_TASK_LV, recvTaskLVReward );

            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PET_STAR_UP, recvPetStarUp );
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_STONE_INLAY, recvPetStoneInLay );
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PET_STONE_UP, recvPetStoneUp );

            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PET_PIECE_TO_PET, recvPetChipToPet );
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PET_PIECE_SELL, recvPetChipSell );

            //////by  ddn////////远征商店/////////////////
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_GET_MOUNTAIN_SHOP, recvReplyEDShop );
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_REFRESH_MOUNTAIN_SHOP, recvUpdateEDShop );
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOUNTAIN_SHOP_BUY, recvBuyEDShop );

            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOT_TEAM_UPDATE, recvNotTeamUpdate );
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOT_INFO, recvNotInfo );
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOT_TEAM, recvNotTeam );
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOT_RESET, recvMotReset );
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOT_PET, recvMotPet );
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOT_REWARD, recvMotReward );
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOT_BOX, recvMotBox );
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOT_BUY_BUFFER, recvMotBuyBuff );
            /////金币商店////////////////////////////
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_GET_SMONEY_SHOP, recvReplyMoneyShop );
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_REFRESH_SMONEY_SHOP, recvRefreshMoneyShop );
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_SMONEY_SHOP_BUY, recvBuyMoneyShop );

            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PING_PRO_TWO, recvPingTwo );
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_BEAST_INFO, recvBeastInfo );
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_BEAST_LV_UP, recvBeastLvUp );
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_BEAST_EQUIP_WEAR, recvBeastEquipWear );
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_BEAST_EQUIP_UP, recvBeastEquipUp );
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_NEW_BEAST, recvNewBeast );

            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_SOUL_COM, recvSoulCom );
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_SOUL_SHOP, recvSoulShop );
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_SOUL_SHOP_BUY, recvSoulShopBuy );
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_OPEN_GIFT_BOX, recvOpenGiftBox );
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_CHAT, recvChat );
            AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_CHAT_RECENT_RESPONSE, recvChatRencent );
            **/
        }

        
    }
}
