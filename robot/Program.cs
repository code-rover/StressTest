using System;
using System.Threading;
using net;
using net.unity3d;
using utils;

namespace robot
{
    class Program
    {
        public static int COUNT = 100;   

        public static AgentNet[] agents = new AgentNet[COUNT];

        static void Main( string[] args )
        {
            Logger.Info("===========================================================");
            Logger.Info( "stress Test for GameServer" );
            Logger.Info( "===========================================================" );
            Logger.Info("");

            int account = 2000;

            // init AgentNets
            for(int i=0; i<agents.Length; i++)
            {
                agents[i] = new AgentNet();
                agents[i].close();
                agents[i].setLoginInfo( account.ToString(), "123", "012345", ( byte ) 1, "", "", "" );
                agents[i].bingAccountSer();

                initListener(agents[i]);  //消息监听注册
                account++;
            }
            
            while( true )
            {
                //Thread.Sleep( 500 );
                foreach( AgentNet agent in agents )
                {
                    agent.tick();
                }
            }
            
            Console.ReadKey( true );
        }

        /// 初始化真挺
        public static void initListener(AgentNet agent)
        {
            ///account返回
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_AC2C_ACCOUNT_INFO, agent.recvAccountServer );
            //agent.addListenEvent( ( ushort ) E_OPCODE.EP_AC2C_MAC_ID, recvMacIdServer );

            ///login返回
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_LS2C_LOGIN_RESPONSE, agent.recvLoginServer );

            
            ///断线之后的重连
            //agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_ON_WEB_CLOSE, recvWebClose );

            //agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_RELOGIN, recvWebOpen );

            ///返回登陆 realmF
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_LOGIN, agent.recvLogin );

            
            ///被踢掉通知客户端
            //agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_TICK_BY_OTHER, recvTick );

            //禁言
            //agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_STOP_INFO, recvStopInfo );

            ///用户基本信息
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MASTER_BASE_INFO, agent.recvMaster );

            /**
            ///发送背包宠物信息
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PET_INFO_BAG, recvHeroBag );

            ///发送酒馆宠物信息
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PET_INFO_BAR, recvHeroWarehouse );

            ///发送队伍信息
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_TEAM_INFO, recvHeroTeam );

            ///发送装备材料组
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_EQUIP_GROUP, recvBagEquip );

            ///发送材料组
            //		agent.addListenEvent((ushort)E_OPCODE.EP_RM2C_EQUIP_GROUP, recvBagStuff);

            ///发送装备信息
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_EQUIPMENT, recvHeroEquip );

            ///上阵好友信息
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_FRIEND_RECOMMEND, recvFriendRecommend );
            ///邀请好友上阵返回
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_FRIEND_FIGHT_SELECT, recvFriendSelect );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_NEXT_FB, recvNextFB );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_EQUIP_UP, recvEquipUp );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_EQUIP_COM, recvEquipCreat );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_EQUIP_UP_ONE_KEY, recvEquipUpAll );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_BIND, recvBingAccount );

            //		agent.addListenEvent((ushort)E_OPCODE.EP_RM2C_TICK_BY_OTHER, recvTickByOther);

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_NAME_RAND, recvRandomName );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_ROLE_NAME, recvChangeName );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_LUCKY_SHOP, recvLuckShop );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_LS2C_SERVER_STATE, recvServerState );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_GET_PHONE_INFO, recvPhoneInfo );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PING_PRO, recvPingPRO );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_SIGN, recvSign );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_SIGN_RE, recvSignRe );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_REWARD_MONEY, recvSignReward );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_TASK, recvTask );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_TASK_GUIDE_REWARD, recvTaskNewReward );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_TASK_EVERYDAY_REWARD, recvTaskEveryDayReward );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_TASK_FB_REWARD, recvTaskFBReward );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_TASK_LV, recvTaskLVReward );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PET_STAR_UP, recvPetStarUp );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_STONE_INLAY, recvPetStoneInLay );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PET_STONE_UP, recvPetStoneUp );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PET_PIECE_TO_PET, recvPetChipToPet );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PET_PIECE_SELL, recvPetChipSell );

            //////by  ddn////////远征商店/////////////////
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_GET_MOUNTAIN_SHOP, recvReplyEDShop );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_REFRESH_MOUNTAIN_SHOP, recvUpdateEDShop );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOUNTAIN_SHOP_BUY, recvBuyEDShop );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOT_TEAM_UPDATE, recvNotTeamUpdate );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOT_INFO, recvNotInfo );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOT_TEAM, recvNotTeam );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOT_RESET, recvMotReset );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOT_PET, recvMotPet );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOT_REWARD, recvMotReward );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOT_BOX, recvMotBox );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOT_BUY_BUFFER, recvMotBuyBuff );
            /////金币商店////////////////////////////
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_GET_SMONEY_SHOP, recvReplyMoneyShop );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_REFRESH_SMONEY_SHOP, recvRefreshMoneyShop );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_SMONEY_SHOP_BUY, recvBuyMoneyShop );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PING_PRO_TWO, recvPingTwo );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_BEAST_INFO, recvBeastInfo );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_BEAST_LV_UP, recvBeastLvUp );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_BEAST_EQUIP_WEAR, recvBeastEquipWear );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_BEAST_EQUIP_UP, recvBeastEquipUp );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_NEW_BEAST, recvNewBeast );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_SOUL_COM, recvSoulCom );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_SOUL_SHOP, recvSoulShop );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_SOUL_SHOP_BUY, recvSoulShopBuy );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_OPEN_GIFT_BOX, recvOpenGiftBox );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_CHAT, recvChat );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_CHAT_RECENT_RESPONSE, recvChatRencent );
            **/
        }

        
    }
}
