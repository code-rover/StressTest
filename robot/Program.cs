using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Timers;
using net;
using net.unity3d;
using utils;
using System.Windows.Forms;
using WindowsFormsApplication1;

namespace robot
{
    class Program
    {
        public static int COUNT          = 4000;//1000;     //玩家人数
        public static int LOGIN_INTERVAL = 500;            //玩家登陆时间间隔(ms)
        public static int ACCOUNT_START  = 1000;            //开始帐号

        //public static string ACCOUNT_IP = "192.168.1.2";
        public static string ACCOUNT_IP = "120.26.14.229";  //"192.168.1.2";
        //public static short ACCOUNT_PORT = 11111;
        public static short ACCOUNT_PORT = 9000; //11111;
        public static int   ACCOUNT_TIMEOUT = 30;

        //public static string LOGIN_IP = "192.168.1.2";
        public static string LOGIN_IP = "120.26.14.229"; //;"192.168.1.2";
        //public static short LOGIN_PORT = 12007;
        public static short LOGIN_PORT = 9005; //;12007;
        public static int    LOGIN_TIMEOUT = 30;

        public static bool isQuit = false;

        public static List<string> task_list = new List<string>();

        public static AgentNet[] agents;

        public static ConcurrentDictionary<uint, double> sendTime = new ConcurrentDictionary<uint, double>();
        public static ConcurrentDictionary<int, List<int>> timeStat = new ConcurrentDictionary<int, List<int>>();

        [STAThread]
        static void Main( string[] args )
        {
            Application.Run(new Form1());

            if (isQuit)
                return;

            Logger.Info("===========================================================");
            Logger.Info( "    Stress Test" );
            Logger.Info( "===========================================================" );
            Logger.Info("");

            load_csv();  //加载csv

            agents = new AgentNet[COUNT];

            Console.WriteLine( "StartTime: " + GUtil.getTimeMs() );

            int account = ACCOUNT_START;    //开始帐号
            string passwd = "123";
            string macId = "";
 
            // create AgentNets
            for(int i=0; i<agents.Length; i++)
            {
                int rand = i * LOGIN_INTERVAL; //new System.Random().Next(500, 5000);
                if (rand == 0)
                    rand = 100;

                System.Timers.Timer timer = new System.Timers.Timer();
                timer.Interval = rand;
                timer.AutoReset = false; //once
                timer.Enabled = true;

                int c = i;
                int _account = account;
                timer.Elapsed += new ElapsedEventHandler((object source, System.Timers.ElapsedEventArgs e) =>
                {
                    agents[c] = new AgentNet();
                    agents[c].close();
                    agents[c].setLoginInfo(_account.ToString(), passwd, macId, (byte)1, "", "", "");
                    agents[c].connectAccountServer(ACCOUNT_IP, ACCOUNT_PORT, ACCOUNT_TIMEOUT);

                    initListener(agents[c]);  //消息监听注册    
                });
                
                account++;
            }
            
            while( true )
            {
                //Thread.Sleep( 500 );
                foreach( AgentNet agent in agents )
                {
                    if (agent != null)
                    {
                        agent.tick();
                    }
                }
                ///Logger.Debug( "----------------------------- " + GUtil.getTimeMs());
            };

            //Logger.Info("=============================================");
            //Logger.Info( "消息回应时间统计:" );

            //foreach(var item in robot.Program.timeStat) {
            //    Logger.Info( "msg: " + item.Key + " time: " + item.Value );    
            //}

            Console.ReadKey( true );
        }

        //加载csv
        public static void load_csv()
        {
            ManagerCsv.init_csv( "Info.csv" );
            Logger.Info( "CSV表加载完成" );
        }

        /// 初始化监听
        public static void initListener(AgentNet agent)
        {
            ///account返回
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_AC2C_ACCOUNT_INFO, agent.recvAccountServer );
            //agent.trans.addListenEvent( ( ushort ) E_OPCODE.EP_AC2C_MAC_ID, recvMacIdServer );

            ///login返回
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_LS2C_LOGIN_RESPONSE, agent.recvLoginServer );

            
            ///断线之后的重连
            //agent.trans.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_ON_WEB_CLOSE, recvWebClose );

            //agent.trans.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_RELOGIN, recvWebOpen );

            ///返回登陆 realmF
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_LOGIN, agent.recvLogin );

            
            ///被踢掉通知客户端
            //agent.trans.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_TICK_BY_OTHER, recvTick );

            //禁言
            //agent.trans.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_STOP_INFO, recvStopInfo );

            ///用户基本信息
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MASTER_BASE_INFO, agent.recvMaster );

            ///改名返回消息
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_ROLE_RENAME, agent.trans.recvChangeName );

            ///返回web email
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_WEB_EMAIL, agent.trans.recvWebEmail );

            ///返回web email打开
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_WEB_EMAIL_OPEN, agent.trans.recvOpenEmail );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_REWARD_MONEY, agent.trans.recvSignReward );

            //返回副本信息
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_FB, agent.trans.recvFBInfo );

            //返回副本进入
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_GOTO_FB, agent.trans.recvFBIn );

            /// 副本 离开
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_LEAVE_FB, agent.trans.recvFBOut );

            /// 副本 战斗奖励返回
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_FB_PK_BEGIN, agent.trans.recvFBCombatReward );

            //副本 验证回复
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_CHECK_FB_PK, agent.trans.recvFBCombatCheck );

            //竞技场pk验证回复
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_CHECK_PK, agent.trans.recvPKCombatResult );

            //返回副本扫荡
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_FB_SWEEP, agent.trans.recvSweep );


            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_FB_RESET, agent.trans.recvBuyFBCnt );


            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_TICK_BY_OTHER, agent.trans.recvTickByOther );

            /////金币商店////////////////////////////
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_GET_SMONEY_SHOP, agent.trans.recvReplyMoneyShop );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_REFRESH_SMONEY_SHOP, agent.trans.recvRefreshMoneyShop );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_SMONEY_SHOP_BUY, agent.trans.recvBuyMoneyShop );

            /////PK商店//////////////////////////////
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_GET_PK_SHOP, agent.trans.recvPKShopUpdate );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_REFRESH_PK_SHOP, agent.trans.recvPKShopReset );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PK_SHOP_BUY, agent.trans.recvPKShopBuy );

            /////远征商店////////////////////////////
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_GET_MOUNTAIN_SHOP, agent.trans.recvReplyEDShop );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_REFRESH_MOUNTAIN_SHOP, agent.trans.recvUpdateEDShop );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOUNTAIN_SHOP_BUY, agent.trans.recvBuyEDShop );

            ///发送背包宠物信息
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PET_INFO_BAG, agent.trans.recvHeroBag );

            //piece 列表返回
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PIECE, agent.trans.recvHeroChipUpdate );

            //碎片合成返回
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PET_PIECE_TO_PET, agent.trans.recvPetChipToPet );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PET_STAR_UP, agent.trans.recvPetStarUp );

            //吃经验药升级返回
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PET_LV_UP, agent.trans.recvPetLvUp_New );

            ///发送装备信息
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_EQUIPMENT, agent.trans.recvHeroEquip );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_POWER_ADD, agent.trans.recvPowerAdd );

            ///购买体力回调
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_POWER_BUY, agent.trans.recvPowerBuy );

            //穿装备
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_EQUIP_PET_NOTIFY, agent.trans.recvHeroEquipChange );

            //镶钻石返回
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_STONE_INLAY, agent.trans.recvPetStoneInLay );

            //进阶石返回
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PET_STONE_UP, agent.trans.recvPetStoneUp );

            //装备强化回应
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_EQUIP_UP, agent.trans.recvEquipUp );

            //装备一键强化回应
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_EQUIP_UP_ONE_KEY, agent.trans.recvEquipUpAll );

            //装备合成回应
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_EQUIP_COM, agent.trans.recvEquipCreat );

            //技能升级回应
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_SKILL_UP_NEW, agent.trans.recvSkillUp_New );


            /////爵位商店//////////////////////////////
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_GET_NOBILITY_SHOP, agent.trans.recvGetNobilityShop );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_REFRESH_NOBILITY_SHOP, agent.trans.recvNobilityShopReset );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_NOBILITY_SHOP_BUY, agent.trans.recvNobilityShopBuy );

            //魂侠抽回应
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_LUCKY_SOUL, agent.trans.recvLuckySoul );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_LUCKY_SOUL_LIST, agent.trans.recvLuckySoulList );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_USE_PROP_ADD_SP, agent.trans.recvPropAddSP );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_USE_TEMP_VIP, agent.trans.recvTempVip );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_CHAT, agent.trans.recvChat );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_CHAT_RECENT_RESPONSE, agent.trans.recvChatRencent );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MATE_PK, agent.trans.recvPKCombat );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_TEAM_SET, agent.trans.recvHeroTeamChange );

            ///发送队伍信息
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_TEAM_INFO, agent.trans.recvHeroTeam );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_ESCORT_FIND_GROUP, agent.trans.recvEscortFindRob );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_ESCORT_BEAT_GROUP, agent.trans.recvEscortDataRobInfo );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_ESCORT_BEAT_TEAM, agent.trans.recvEscortDataRobTeam );
            //agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_ESCORT_GET_GROUP, agent.trans.recvEscortDataList );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_ESCORT_GET_TEAM, agent.trans.recvEscortDataTeam );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_ESCORT_GO_TO, agent.trans.recvEscortRobIn );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_ESCORT_LEAVE, agent.trans.recvEscortRobOut );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_ESCORT_PK_OVER, agent.trans.recvEscortRobResult );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_ESCORT_GIVE_UP, agent.trans.recvEscortGiveUp );



            //公会协议
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_GAME_UNION_LIST, agent.trans.recvGetUnionList );
            ////请求加入公会回调
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_REQUEST_JOIN_GAME_UNION, agent.trans.recvRequestJoinUnion );
            ////创建公会回调
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_CREATE_GAME_UNION, agent.trans.recvCreateUnion );
            ////解散公会
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_DISBAND_GAME_UNION, agent.trans.recvDisbandUnion );

            ////公会成员列表
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_GU_MEMBER_LIST, recvMemberList );
            ////搜索公会
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_SEARCH_GAME_UNION, recvSearchUnion );
            ////申请加入公会的列表(其他玩家加入我的公会)
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_REQUEST_JOINER_LIST, recvRequestJoinUnionList );
            ////申请加入公会的列表(同意或者拒绝)
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_AGREEN_JOIN, recvAgreeJoinUnion );
            ////修改公会宣言
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_SET_GU_ANNOUNCEMENT, recvSetUnionAnnoun );
            
            ////设置公会属性
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_SET_GAME_UNION, recvSetUnion );
            ////获取公会详细属性
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_GET_GAME_UNION_INFO, recvUnionInfo );
            ////设置公会职业
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_APPOINT_JOB, recvAppointJob );
            ////公会日志
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_GET_GAME_UNION_LOG, recvUnionLogInfo );
            //// get union name
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_ROLE_UNION_RESPONSE, recvRoleUnion );
            ////膜拜
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_GU_WORSHIP, recvWorship );
            ////膜拜大神按钮，显示可领取奖励
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_OPEN_WORSHIP_UI, recvShowWorshipReward );
            ////回复派遣佣兵协议 类型0-派驻，1-归队
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_GU_DISPATCH_PET, recvDispatchPet );
            ////领取膜拜奖励
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_WORSHIP_REWARD, recvGetWorshipReward );
            ////公会成员全体邮件
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_EMAIL_ALL_MEMBER, recvMaillAllMember );
            ////接受公会全体成员邮件内容
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_GU_EMAIL_TEXT, recvUnionEmailContent );
            ////接受公会月卡协议
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PRESENT_GU_MONTH_CARD, recvGuMonthCard );
            ////重新接受公会信息
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_NOTIFY_LEAD_GU_INFO, recvNotifyLeadGuInfo );
            ////多倍掉率
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_BGND_DROP, recvBgndDrop );
            ////多倍掉率 更新掉率信息
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_BGND_DROP_SET, recvBgndDropUpdate );
            ////是否有其他玩家  打开了设置公会界面
            //AgentNet.getInstance().addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_OPEN_SET_GAME_UNION, recvOpenSetUnion );


            //agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PING_PRO_TWO, recvPingTwo );
            //agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_BEAST_INFO, recvBeastInfo );
            //agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_BEAST_LV_UP, recvBeastLvUp );
            //agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_BEAST_EQUIP_WEAR, recvBeastEquipWear );
            //agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_BEAST_EQUIP_UP, recvBeastEquipUp );
            //agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_NEW_BEAST, recvNewBeast );

            //agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_SOUL_COM, recvSoulCom );
            //agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_SOUL_SHOP, recvSoulShop );
            //agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_SOUL_SHOP_BUY, recvSoulShopBuy );
            //agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_OPEN_GIFT_BOX, recvOpenGiftBox );

            ///
            /**
            

            ///发送酒馆宠物信息
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PET_INFO_BAR, recvHeroWarehouse );

            

            ///发送装备材料组
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_EQUIP_GROUP, recvBagEquip );

            ///发送材料组
            //		agent.addListenEvent((ushort)E_OPCODE.EP_RM2C_EQUIP_GROUP, recvBagStuff);

            

            ///上阵好友信息
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_FRIEND_RECOMMEND, recvFriendRecommend );
            ///邀请好友上阵返回
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_FRIEND_FIGHT_SELECT, recvFriendSelect );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_NEXT_FB, recvNextFB );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_EQUIP_UP, recvEquipUp );

            

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_EQUIP_UP_ONE_KEY, recvEquipUpAll );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_BIND, recvBingAccount );

            //		

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_NAME_RAND, recvRandomName );

            


            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_LUCKY_SHOP, recvLuckShop );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_LS2C_SERVER_STATE, recvServerState );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_GET_PHONE_INFO, recvPhoneInfo );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PING_PRO, recvPingPRO );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_SIGN, recvSign );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_SIGN_RE, recvSignRe );

            

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_TASK, recvTask );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_TASK_GUIDE_REWARD, recvTaskNewReward );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_TASK_EVERYDAY_REWARD, recvTaskEveryDayReward );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_TASK_FB_REWARD, recvTaskFBReward );

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_TASK_LV, recvTaskLVReward );

            

            
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_PET_PIECE_SELL, recvPetChipSell );

            

            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOT_TEAM_UPDATE, recvNotTeamUpdate );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOT_INFO, recvNotInfo );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOT_TEAM, recvNotTeam );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOT_RESET, recvMotReset );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOT_PET, recvMotPet );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOT_REWARD, recvMotReward );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOT_BOX, recvMotBox );
            agent.addListenEvent( ( ushort ) E_OPCODE.EP_RM2C_MOT_BUY_BUFFER, recvMotBuyBuff );
         **/   
        }

        
    }
}
