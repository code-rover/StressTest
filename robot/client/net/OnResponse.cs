using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using net.unity3d;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using net;

namespace net
{
    class OnResponse
    {
        public static void onGetAccount( ArgsEvent args )
        {
            AC2C_ACCOUNT_INFO info = (AC2C_ACCOUNT_INFO)(args.Data);
        }

        public static void onGetRealmIp(ArgsEvent args)
        {
            //Program.initRealmServer();
            LS2C_LOGIN_RESPONSE info = (LS2C_LOGIN_RESPONSE)(args.Data);
            //Program.initRealmServer();
        }

        public static void onMaseterInfo(ArgsEvent args)
        {
            ///登录成功
            //RM2C_MASTER_BASE_INFO ar = (RM2C_MASTER_BASE_INFO)(args.Data);
            //Program.onLoginBack();
        }

        public static void onFriend(ArgsEvent args)
        {
            ///登录成功
            RM2C_FRIEND ar = (RM2C_FRIEND)(args.Data);
        }

        public static void onFightFriend(ArgsEvent args)
        {
            ///登录成功
            RM2C_FRIEND_RECOMMEND ar = (RM2C_FRIEND_RECOMMEND)(args.Data);
        }

        public static void onFightFriendSelect(ArgsEvent args)
        {
            RM2C_FRIEND_FIGHT_SELECT ar = (RM2C_FRIEND_FIGHT_SELECT)(args.Data);

        }

        public static void onRMLogin(ArgsEvent args)
        {
            ///返回登录结果
            RM2C_LOGIN info = (RM2C_LOGIN)(args.Data);
            if (8 == info.iResult)
            {
                Console.Write("需要创建角色");
                //Program.createRole();
            }
            else
            {
                Console.Write("返回登录结果 : " + info.iResult);
            }
        }

        public static void onPetEat(ArgsEvent args)
        {
            RM2C_PET_EAT info = (RM2C_PET_EAT)(args.Data);

        }

        public static void onPetUp(ArgsEvent args)
        {
            RM2C_PET_UP info = (RM2C_PET_UP)(args.Data);
        }

        public static void onSkillUp(ArgsEvent args)
        {
            RM2C_SKILL_UP info = (RM2C_SKILL_UP)(args.Data);

        }

        public static void onPowerAdd(ArgsEvent args)
        {
            RM2C_POWER_ADD info = (RM2C_POWER_ADD)(args.Data);

        }
        public static void onPieceFromPet(ArgsEvent args)
        {
            RM2C_PIECE_FROM_PET info = (RM2C_PIECE_FROM_PET)(args.Data);

        }
        public static void onPieceToPet(ArgsEvent args)
        {
            RM2C_PIECE_TO_PET info = (RM2C_PIECE_TO_PET)(args.Data);

        }
        public static void onEquipPetNofify(ArgsEvent args)
        {
            RM2C_EQUIP_PET_NOTIFY info = (RM2C_EQUIP_PET_NOTIFY)(args.Data);

        }
        public static void onTeamSet(ArgsEvent args)
        {
            RM2C_TEAM_SET info = (RM2C_TEAM_SET)(args.Data);

        }
        public static void onPetBarToBag(ArgsEvent args)
        {
            RM2C_PET_BAR_TO_BAG info = (RM2C_PET_BAR_TO_BAG)(args.Data);

        }
        public static void onPetBagToBar(ArgsEvent args)
        {
            RM2C_PET_BAG_TO_BAR info = (RM2C_PET_BAG_TO_BAR)(args.Data);

        }
        public static void onPiece(ArgsEvent args)
        {
            RM2C_PIECE info = (RM2C_PIECE)(args.Data);

        }

        public static void onPetSell(ArgsEvent args)
        {
            RM2C_PET_SELL info = (RM2C_PET_SELL)(args.Data);

        }
        public static void onPetSetSkill(ArgsEvent args)
        {
            RM2C_PET_SET_SKILL info = (RM2C_PET_SET_SKILL)(args.Data);

        }
        public static void onTeamWorkSet(ArgsEvent args)
        {
            RM2C_TEAM_WORK_SET info = (RM2C_TEAM_WORK_SET)(args.Data);

        }

        public static void onWorldEventTime(ArgsEvent args)
        {
            RM2C_WORLD_EVENT_TIME info = (RM2C_WORLD_EVENT_TIME)(args.Data);
        }

        public static void onWorldBossInfo(ArgsEvent args)
        {
            RM2C_WROLD_BOSS_INFO info = (RM2C_WROLD_BOSS_INFO)(args.Data);
        }

        public static void onExciteWorldBoss(ArgsEvent args)
        {
            RM2C_EXCITE_WORLD_BOSS info = (RM2C_EXCITE_WORLD_BOSS)(args.Data);
        }

        public static void onPkInfo(ArgsEvent args)
        {
            RM2C_PK_INFO info = (RM2C_PK_INFO)(args.Data);
        }

        public static void onPkRankList(ArgsEvent args)
        {
            RM2C_PK_RANK_LIST info = (RM2C_PK_RANK_LIST)(args.Data);
        }

        public static void onUpdatePkTeam(ArgsEvent args)
        {
            RM2C_UPDATE_PK_TEAM info = (RM2C_UPDATE_PK_TEAM)(args.Data);
        }

        public static void onAddPkCnt(ArgsEvent args)
        {
            RM2C_ADD_PK_CNT info = (RM2C_ADD_PK_CNT)(args.Data);
        }

        public static void onUpLvNobility(ArgsEvent args)
        {
            RM2C_UP_LV_NOBILITY info = (RM2C_UP_LV_NOBILITY)(args.Data);
        }

        public static void onGetRewardNobility(ArgsEvent args)
        {
            RM2C_GET_REWARD_NOBILITY info = (RM2C_GET_REWARD_NOBILITY)(args.Data);
        }

        public static void onGotoNextFB(ArgsEvent args)
        {
            RM2C_NEXT_FB info = (RM2C_NEXT_FB)(args.Data);
        }

        public static void onMatePk(ArgsEvent args)
        {
            RM2C_MATE_PK info = (RM2C_MATE_PK)(args.Data);
        }

        public static void onGetEmail(ArgsEvent args)
        {
            RM2C_EMAIL_LIST info = (RM2C_EMAIL_LIST)(args.Data);
        }

        public static void onCreatEmail(ArgsEvent args)
        {
            RM2C_CREATE_EMAIL info = (RM2C_CREATE_EMAIL)(args.Data);
        }

        public static void onOpenEmail(ArgsEvent args)
        {
            RM2C_OPEN_EMAIL info = (RM2C_OPEN_EMAIL)(args.Data);
        }

        public static void onDestroyEmail(ArgsEvent args)
        {
            RM2C_DESTROY_EMAIL info = (RM2C_DESTROY_EMAIL)(args.Data);
        }

        public static void onEquipCom(ArgsEvent args)
        {
            RM2C_EQUIP_COM info = (RM2C_EQUIP_COM)(args.Data);
        }

        public static void onEquipUp(ArgsEvent args)
        {
            RM2C_EQUIP_UP info = (RM2C_EQUIP_UP)(args.Data);
        }

        public static void onEquipUpOneKey(ArgsEvent args)
        {
            RM2C_EQUIP_UP_ONE_KEY info = (RM2C_EQUIP_UP_ONE_KEY)(args.Data);
        }

        public static void onRefreshTowerShop(ArgsEvent args)
        {
            RM2C_REFRESH_TOWER_SHOP info = (RM2C_REFRESH_TOWER_SHOP)(args.Data);
        }

        public static void onBuyTowerShop(ArgsEvent args)
        {
            RM2C_BUY_TOWER_SHOP info = (RM2C_BUY_TOWER_SHOP)(args.Data);
        }

        public static void onBuyPkShop(ArgsEvent args)
        {
            RM2C_PK_SHOP_BUY info = (RM2C_PK_SHOP_BUY)(args.Data);
        }

        public static void onGetPkShop(ArgsEvent args)
        {
            RM2C_GET_PK_SHOP info = (RM2C_GET_PK_SHOP)(args.Data);
        }

        public static void onRefreshPkShop(ArgsEvent args)
        {
            RM2C_REFRESH_PK_SHOP info = (RM2C_REFRESH_PK_SHOP)(args.Data);
        }

        public static void onBadgeShopBuy(ArgsEvent args)
        {
            RM2C_BADGE_SHOP_BUY info = (RM2C_BADGE_SHOP_BUY)(args.Data);
        }

        public static void onGetMotShop(ArgsEvent args)
        {
            RM2C_GET_MOUNTAIN_SHOP info = (RM2C_GET_MOUNTAIN_SHOP)(args.Data);
        }
    }
}
