using System;
using System.Collections.Generic;
using net.unity3d;

namespace robot.client
{
    class Protocols
    {
        public static void loginSerRegisterProtocol( USession Session )
        {
            if( null == Session )
            {
                return;
            }
            Session.addFactoryMethod( ( ushort ) LS2C_LOGIN_RESPONSE.OPCODE, LS2C_LOGIN_RESPONSE.Create );
            ///2014-05-29
            Session.addFactoryMethod( ( ushort ) LS2C_SERVER_STATE.OPCODE, LS2C_SERVER_STATE.Create );
        }

        public static void accountSerRegisterProtocol( USession Session )
        {
            if( null == Session )
            {
                return;
            }
            Session.addFactoryMethod( ( ushort ) AC2C_ACCOUNT_INFO.OPCODE, AC2C_ACCOUNT_INFO.Create );
            Session.addFactoryMethod( ( ushort ) AC2C_MAC_ID.OPCODE, AC2C_MAC_ID.Create );
            ///2015-07-07
            Session.addFactoryMethod( ( ushort ) AC2C_RECENT_SERVER_INFO.OPCODE, AC2C_RECENT_SERVER_INFO.Create );

        }

        public static void logicRegisterProtocol( USession Session )
        {
            if( null == Session )
            {
                return;
            }
            ///2014-01-18
            Session.addFactoryMethod( ( ushort ) RM2C_ON_REALM_SERVER_CONN.OPCODE, RM2C_ON_REALM_SERVER_CONN.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ON_REALM_SERVER_CLOSE.OPCODE, RM2C_ON_REALM_SERVER_CLOSE.Create );

            Session.addFactoryMethod( ( ushort ) RM2C_MASTER_BASE_INFO.OPCODE, RM2C_MASTER_BASE_INFO.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_PET_INFO_BAG.OPCODE, RM2C_PET_INFO_BAG.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_PET_INFO_BAR.OPCODE, RM2C_PET_INFO_BAR.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_TEAM_INFO.OPCODE, RM2C_TEAM_INFO.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_EQUIP_GROUP.OPCODE, RM2C_EQUIP_GROUP.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_MATERIAL_GROUP.OPCODE, RM2C_MATERIAL_GROUP.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_EQUIPMENT.OPCODE, RM2C_EQUIPMENT.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FB.OPCODE, RM2C_FB.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_GOTO_FB.OPCODE, RM2C_GOTO_FB.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_LEAVE_FB.OPCODE, RM2C_LEAVE_FB.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FB_PK_BEGIN.OPCODE, RM2C_FB_PK_BEGIN.Create );

            Session.addFactoryMethod( ( ushort ) RM2C_LOGIN.OPCODE, RM2C_LOGIN.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_RECOMMEND.OPCODE, RM2C_FRIEND_RECOMMEND.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_NOTIFY_TIME.OPCODE, RM2C_FRIEND_NOTIFY_TIME.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND.OPCODE, RM2C_FRIEND.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_BE_PK_LIST.OPCODE, RM2C_FRIEND_BE_PK_LIST.Create );

            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_FIGHT_SELECT.OPCODE, RM2C_FRIEND_FIGHT_SELECT.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_PET_EAT.OPCODE, RM2C_PET_EAT.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_PET_UP.OPCODE, RM2C_PET_UP.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_SKILL_UP.OPCODE, RM2C_SKILL_UP.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_POWER_ADD.OPCODE, RM2C_POWER_ADD.Create );

            ///2014-02-25
            Session.addFactoryMethod( ( ushort ) RM2C_FB_REWARD.OPCODE, RM2C_FB_REWARD.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_CREATE_PET.OPCODE, RM2C_CREATE_PET.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_CREATE_EQUIP.OPCODE, RM2C_CREATE_EQUIP.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_CREATE_PIECE.OPCODE, RM2C_CREATE_PIECE.Create );

            ///2014-03-03
            Session.addFactoryMethod( ( ushort ) RM2C_WORLD_EVENT_TIME.OPCODE, RM2C_WORLD_EVENT_TIME.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_WROLD_BOSS_INFO.OPCODE, RM2C_WROLD_BOSS_INFO.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_GOTO_WORLD_BOSS.OPCODE, RM2C_GOTO_WORLD_BOSS.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_EXCITE_WORLD_BOSS.OPCODE, RM2C_EXCITE_WORLD_BOSS.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_CHECK_WORLD_BOSS_PK.OPCODE, RM2C_CHECK_WORLD_BOSS_PK.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_WROLD_BOSS_PK_REVIVE.OPCODE, RM2C_WROLD_BOSS_PK_REVIVE.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_WORLD_BOSS_REWARD.OPCODE, RM2C_WORLD_BOSS_REWARD.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_LEAVE_WROLD_BOSS.OPCODE, RM2C_LEAVE_WROLD_BOSS.Create );

            ///2014-03-04
            Session.addFactoryMethod( ( ushort ) RM2C_PK_INFO.OPCODE, RM2C_PK_INFO.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_PK_RANK_LIST.OPCODE, RM2C_PK_RANK_LIST.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ADD_PK_CNT.OPCODE, RM2C_ADD_PK_CNT.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_UP_LV_NOBILITY.OPCODE, RM2C_UP_LV_NOBILITY.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_GET_REWARD_NOBILITY.OPCODE, RM2C_GET_REWARD_NOBILITY.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_MATE_PK.OPCODE, RM2C_MATE_PK.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_CHECK_PK.OPCODE, RM2C_CHECK_PK.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_UPDATE_PK_TEAM.OPCODE, RM2C_UPDATE_PK_TEAM.Create );

            ///2014-03-05
            Session.addFactoryMethod( ( ushort ) RM2C_PIECE.OPCODE, RM2C_PIECE.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_PET_BAG_TO_BAR.OPCODE, RM2C_PET_BAG_TO_BAR.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_PET_BAR_TO_BAG.OPCODE, RM2C_PET_BAR_TO_BAG.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_POWER_INFO.OPCODE, RM2C_POWER_INFO.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_EQUIP_PET_NOTIFY.OPCODE, RM2C_EQUIP_PET_NOTIFY.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_PIECE_TO_PET.OPCODE, RM2C_PIECE_TO_PET.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_PIECE_FROM_PET.OPCODE, RM2C_PIECE_FROM_PET.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_PET_SELL.OPCODE, RM2C_PET_SELL.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_PET_SET_SKILL.OPCODE, RM2C_PET_SET_SKILL.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_TEAM_WORK_SET.OPCODE, RM2C_TEAM_WORK_SET.Create );

            ///2014-03-07
            Session.addFactoryMethod( ( ushort ) RM2C_EQUIP_SELL.OPCODE, RM2C_EQUIP_SELL.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FB_NOTIFY_INFO.OPCODE, RM2C_FB_NOTIFY_INFO.Create );

            ///2014-03-10
            Session.addFactoryMethod( ( ushort ) RM2C_SHOW_PET_REWARD.OPCODE, RM2C_SHOW_PET_REWARD.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_SHOW_PIECE_REWARD.OPCODE, RM2C_SHOW_PIECE_REWARD.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_SHOW_EQUIP_REWARD.OPCODE, RM2C_SHOW_EQUIP_REWARD.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FB_NEW.OPCODE, RM2C_FB_NEW.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FB_SWEEP.OPCODE, RM2C_FB_SWEEP.Create );

            ///2014-03-12
            Session.addFactoryMethod( ( ushort ) RM2C_LAND_LIST.OPCODE, RM2C_LAND_LIST.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_LAND_STAR_REWARD.OPCODE, RM2C_LAND_STAR_REWARD.Create );

            ///2014-03-13
            Session.addFactoryMethod( ( ushort ) RM2C_LAND_NEW.OPCODE, RM2C_LAND_NEW.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_TEAM_SET.OPCODE, RM2C_TEAM_SET.Create );

            ///2014-03-24
            Session.addFactoryMethod( ( ushort ) RM2C_NEXT_FB.OPCODE, RM2C_NEXT_FB.Create );

            ///2014-03-25
            Session.addFactoryMethod( ( ushort ) RM2C_EMAIL_LIST.OPCODE, RM2C_EMAIL_LIST.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_CREATE_EMAIL.OPCODE, RM2C_CREATE_EMAIL.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_OPEN_EMAIL.OPCODE, RM2C_OPEN_EMAIL.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_DESTROY_EMAIL.OPCODE, RM2C_DESTROY_EMAIL.Create );

            ///2014-04-01
            Session.addFactoryMethod( ( ushort ) RM2C_ON_WEB_CLOSE.OPCODE, RM2C_ON_WEB_CLOSE.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_EQUIP_COM.OPCODE, RM2C_EQUIP_COM.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_EQUIP_UP.OPCODE, RM2C_EQUIP_UP.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_EQUIP_UP_ONE_KEY.OPCODE, RM2C_EQUIP_UP_ONE_KEY.Create );

            ///2014-04-04
            Session.addFactoryMethod( ( ushort ) RM2C_RELOGIN.OPCODE, RM2C_RELOGIN.Create );

            ///2014-04-08
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_ASK.OPCODE, RM2C_FRIEND_ASK.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_ASK_NOTIFY.OPCODE, RM2C_FRIEND_ASK_NOTIFY.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_ASK_ANSWER.OPCODE, RM2C_FRIEND_ASK_ANSWER.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_NEW.OPCODE, RM2C_FRIEND_NEW.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_DELETE.OPCODE, RM2C_FRIEND_DELETE.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_ASK_NOTIFY_DELETE.OPCODE, RM2C_FRIEND_ASK_NOTIFY_DELETE.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_SEARCH.OPCODE, RM2C_FRIEND_SEARCH.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_BUY.OPCODE, RM2C_FRIEND_BUY.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_POWER_SEND.OPCODE, RM2C_FRIEND_POWER_SEND.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_POWER.OPCODE, RM2C_FRIEND_POWER.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_POWER_NEW.OPCODE, RM2C_FRIEND_POWER_NEW.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_POWER_GET.OPCODE, RM2C_FRIEND_POWER_GET.Create );


            ///2014-04-09
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_ASK_LIST.OPCODE, RM2C_FRIEND_ASK_LIST.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_PK_CHECK_TEAM.OPCODE, RM2C_PK_CHECK_TEAM.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_NOTIFY_BE_PK.OPCODE, RM2C_FRIEND_NOTIFY_BE_PK.Create );

            ///2014-04-11
            Session.addFactoryMethod( ( ushort ) RM2C_ROLE_PSIGN.OPCODE, RM2C_ROLE_PSIGN.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_BIND.OPCODE, RM2C_BIND.Create );

            ///2014-04-14
            Session.addFactoryMethod( ( ushort ) RM2C_GET_TOWER_INFO.OPCODE, RM2C_GET_TOWER_INFO.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_CHECK_TOWER_PK.OPCODE, RM2C_CHECK_TOWER_PK.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_TOWER_SWEEP.OPCODE, RM2C_TOWER_SWEEP.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_TOWER_REWARD.OPCODE, RM2C_TOWER_REWARD.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_TOWER_DAY_REWARD.OPCODE, RM2C_TOWER_DAY_REWARD.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_TOWER_AGAIN.OPCODE, RM2C_TOWER_AGAIN.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_REFRESH_TOWER_SHOP.OPCODE, RM2C_REFRESH_TOWER_SHOP.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_BUY_TOWER_SHOP.OPCODE, RM2C_BUY_TOWER_SHOP.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_GET_TOWER_TIME.OPCODE, RM2C_GET_TOWER_TIME.Create );

            ///2014-04-15
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_NOFRESH_INFO.OPCODE, RM2C_FRIEND_NOFRESH_INFO.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_FRESH_INFO.OPCODE, RM2C_FRIEND_FRESH_INFO.Create );

            ///2014-04-17
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_PK_BACK_OVER.OPCODE, RM2C_FRIEND_PK_BACK_OVER.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_PK_OVER.OPCODE, RM2C_FRIEND_PK_OVER.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_PK_BACK.OPCODE, RM2C_FRIEND_PK_BACK.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEEND_PK.OPCODE, RM2C_FRIEEND_PK.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_POWER_BUY.OPCODE, RM2C_POWER_BUY.Create );

            ///2014-04-18
            Session.addFactoryMethod( ( ushort ) RM2C_ROLE_NAME.OPCODE, RM2C_ROLE_NAME.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_NICK_NAME.OPCODE, RM2C_FRIEND_NICK_NAME.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_TICK_BY_OTHER.OPCODE, RM2C_TICK_BY_OTHER.Create );

            ///2014-05-10
            Session.addFactoryMethod( ( ushort ) RM2C_NAME_RAND.OPCODE, RM2C_NAME_RAND.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_PK_SHOP_BUY.OPCODE, RM2C_PK_SHOP_BUY.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_REFRESH_PK_SHOP.OPCODE, RM2C_REFRESH_PK_SHOP.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_GET_PK_SHOP.OPCODE, RM2C_GET_PK_SHOP.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ACTIV_EVENT_TIME.OPCODE, RM2C_ACTIV_EVENT_TIME.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ACTIV_EVENT_INFO.OPCODE, RM2C_ACTIV_EVENT_INFO.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ACTIV_EVENT_BEGIN.OPCODE, RM2C_ACTIV_EVENT_BEGIN.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_CHECK_ACTIV_EVENT.OPCODE, RM2C_CHECK_ACTIV_EVENT.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ADD_ACTIV_EVENT_CNT.OPCODE, RM2C_ADD_ACTIV_EVENT_CNT.Create );

            ///2014-04-13
            Session.addFactoryMethod( ( ushort ) RM2C_LUCKY_SHOP.OPCODE, RM2C_LUCKY_SHOP.Create );

            ///2014-05-14
            Session.addFactoryMethod( ( ushort ) RM2C_GO_TO_ACTIV_EVENT.OPCODE, RM2C_GO_TO_ACTIV_EVENT.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_LEAVE_ACTIV_EVENT.OPCODE, RM2C_LEAVE_ACTIV_EVENT.Create );
            ///2014-05-15
            Session.addFactoryMethod( ( ushort ) RM2C_RE_PK_FB.OPCODE, RM2C_RE_PK_FB.Create );

            ///2014-05-20
            Session.addFactoryMethod( ( ushort ) RM2C_CHARGE.OPCODE, RM2C_CHARGE.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_CHARGE_ORDER_FINISH.OPCODE, RM2C_CHARGE_ORDER_FINISH.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_CHARGE_LIST.OPCODE, RM2C_CHARGE_LIST.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ORDER_LIST.OPCODE, RM2C_ORDER_LIST.Create );

            ///2014-05-21
            Session.addFactoryMethod( ( ushort ) RM2C_PING_PRO.OPCODE, RM2C_PING_PRO.Create );

            ///2014-05-27
            Session.addFactoryMethod( ( ushort ) RM2C_RE_ACTIV_EVENT.OPCODE, RM2C_RE_ACTIV_EVENT.Create );

            ///2014-05-28
            Session.addFactoryMethod( ( ushort ) RM2C_CHARGE_CARD.OPCODE, RM2C_CHARGE_CARD.Create );
            ///2014-06-18
            Session.addFactoryMethod( ( ushort ) RM2C_FB_RESET.OPCODE, RM2C_FB_RESET.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_STONE.OPCODE, RM2C_STONE.Create );

            ///2014-06-19	
            Session.addFactoryMethod( ( ushort ) RM2C_BADGE_SHOP_BUY.OPCODE, RM2C_BADGE_SHOP_BUY.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_BAG_BUY.OPCODE, RM2C_BAG_BUY.Create );

            ///2014-07-08	
            Session.addFactoryMethod( ( ushort ) RM2C_GET_PHONE_INFO.OPCODE, RM2C_GET_PHONE_INFO.Create );

            ///2014-07-16
            Session.addFactoryMethod( ( ushort ) RM2C_CHECK_FB_PK.OPCODE, RM2C_CHECK_FB_PK.Create );
            ///2014-07-17
            Session.addFactoryMethod( ( ushort ) RM2C_SERVER_STOP_NOTE.OPCODE, RM2C_SERVER_STOP_NOTE.Create );
            ///2014-08-04
            Session.addFactoryMethod( ( ushort ) RM2C_POWER_MEAL.OPCODE, RM2C_POWER_MEAL.Create );
            ///2014-08-07
            Session.addFactoryMethod( ( ushort ) RM2C_POWER_MEAL_TIME.OPCODE, RM2C_POWER_MEAL_TIME.Create );

            ///2014-08-14
            Session.addFactoryMethod( ( ushort ) RM2C_SIGN_RE.OPCODE, RM2C_SIGN_RE.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_REWARD_MONEY.OPCODE, RM2C_REWARD_MONEY.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_SIGN.OPCODE, RM2C_SIGN.Create );

            ///2014-08-21
            Session.addFactoryMethod( ( ushort ) RM2C_TASK_LV.OPCODE, RM2C_TASK_LV.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_TASK_EVERYDAY_REWARD.OPCODE, RM2C_TASK_EVERYDAY_REWARD.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_TASK_FB_REWARD.OPCODE, RM2C_TASK_FB_REWARD.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_TASK_GUIDE_REWARD.OPCODE, RM2C_TASK_GUIDE_REWARD.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_TASK.OPCODE, RM2C_TASK.Create );
            ///2014-08-23
            Session.addFactoryMethod( ( ushort ) RM2C_PET_LV_UP.OPCODE, RM2C_PET_LV_UP.Create );
            ///2014-08-25
            Session.addFactoryMethod( ( ushort ) RM2C_PET_STAR_UP.OPCODE, RM2C_PET_STAR_UP.Create );

            ///2014-08-26
            Session.addFactoryMethod( ( ushort ) RM2C_SKILL_POINT_BUY.OPCODE, RM2C_SKILL_POINT_BUY.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_SKILL_POINT_ADD.OPCODE, RM2C_SKILL_POINT_ADD.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_SKILL_UP_NEW.OPCODE, RM2C_SKILL_UP_NEW.Create );

            ///2014-08-30
            Session.addFactoryMethod( ( ushort ) RM2C_LUCKY_SOUL.OPCODE, RM2C_LUCKY_SOUL.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_PET_PIECE_TO_PET.OPCODE, RM2C_PET_PIECE_TO_PET.Create );

            ///2014-09-02
            Session.addFactoryMethod( ( ushort ) RM2C_STONE_INLAY.OPCODE, RM2C_STONE_INLAY.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_PET_STONE_UP.OPCODE, RM2C_PET_STONE_UP.Create );

            ///2014-09-03
            Session.addFactoryMethod( ( ushort ) RM2C_TEAM_EXP_ADD.OPCODE, RM2C_TEAM_EXP_ADD.Create );

            ///2014-09-5
            Session.addFactoryMethod( ( ushort ) RM2C_PET_PIECE_SELL.OPCODE, RM2C_PET_PIECE_SELL.Create );

            Session.addFactoryMethod( ( ushort ) RM2C_SHOW_SWEEP_REWARD.OPCODE, RM2C_SHOW_SWEEP_REWARD.Create );
            ///2014-09-12
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_HELPER.OPCODE, RM2C_FRIEND_HELPER.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FRIEND_FIGHT_HISTORY.OPCODE, RM2C_FRIEND_FIGHT_HISTORY.Create );

            ///2014-09-28
            Session.addFactoryMethod( ( ushort ) RM2C_WEB_EMAIL_OPEN.OPCODE, RM2C_WEB_EMAIL_OPEN.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_WEB_EMAIL.OPCODE, RM2C_WEB_EMAIL.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_WEB_EMAIL_NOTIFY_NEW.OPCODE, RM2C_WEB_EMAIL_NOTIFY_NEW.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_NOTIFY_LEAD_FB.OPCODE, RM2C_NOTIFY_LEAD_FB.Create );

            ///2014-10-08
            Session.addFactoryMethod( ( ushort ) RM2C_TEAM_NOTIFY.OPCODE, RM2C_TEAM_NOTIFY.Create );

            ///2014-10-14
            Session.addFactoryMethod( ( ushort ) RM2C_BOSS_UPDATE.OPCODE, RM2C_BOSS_UPDATE.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_BOSS_TIME_OUT.OPCODE, RM2C_BOSS_TIME_OUT.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_BOSS_INFO.OPCODE, RM2C_BOSS_INFO.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_BOSS_GOTO.OPCODE, RM2C_BOSS_GOTO.Create );

            ///2014-10-16
            Session.addFactoryMethod( ( ushort ) RM2C_BOSS_MONEY_REWARD.OPCODE, RM2C_BOSS_MONEY_REWARD.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_BOSS_VALUE.OPCODE, RM2C_BOSS_VALUE.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_BOSS_CHECK_ERROR.OPCODE, RM2C_BOSS_CHECK_ERROR.Create );

            ///2014-10-18
            Session.addFactoryMethod( ( ushort ) RM2C_BOSS_VALUE_REWARD_END.OPCODE, RM2C_BOSS_VALUE_REWARD_END.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_BOSS_VALUE_REWARD_BEGIN.OPCODE, RM2C_BOSS_VALUE_REWARD_BEGIN.Create );

            ///2014-10-23
            Session.addFactoryMethod( ( ushort ) RM2C_MOT_GOTO.OPCODE, RM2C_MOT_GOTO.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_MOT_BUY_BUFFER.OPCODE, RM2C_MOT_BUY_BUFFER.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_MOT_BOX.OPCODE, RM2C_MOT_BOX.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_MOT_REWARD.OPCODE, RM2C_MOT_REWARD.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_MOT_RESET.OPCODE, RM2C_MOT_RESET.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_MOT_PET.OPCODE, RM2C_MOT_PET.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_MOT_TEAM.OPCODE, RM2C_MOT_TEAM.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_MOT_INFO.OPCODE, RM2C_MOT_INFO.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_MOT_TEAM_UPDATE.OPCODE, RM2C_MOT_TEAM_UPDATE.Create );
            ///2014-10-24
            Session.addFactoryMethod( ( ushort ) RM2C_MOT_PK_ERROR.OPCODE, RM2C_MOT_PK_ERROR.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_MOT_HELPER_INFO.OPCODE, RM2C_MOT_HELPER_INFO.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_MOT_HELPER.OPCODE, RM2C_MOT_HELPER.Create );

            ///2014-10-27
            Session.addFactoryMethod( ( ushort ) RM2C_MOUNTAIN_SHOP_BUY.OPCODE, RM2C_MOUNTAIN_SHOP_BUY.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_REFRESH_MOUNTAIN_SHOP.OPCODE, RM2C_REFRESH_MOUNTAIN_SHOP.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_GET_MOUNTAIN_SHOP.OPCODE, RM2C_GET_MOUNTAIN_SHOP.Create );

            ///2014-10-28
            Session.addFactoryMethod( ( ushort ) RM2C_MOT_LEAVE.OPCODE, RM2C_MOT_LEAVE.Create );
            ///2014-10-29
            Session.addFactoryMethod( ( ushort ) RM2C_MOT_HELPER_IS_IN_TEAM.OPCODE, RM2C_MOT_HELPER_IS_IN_TEAM.Create );
            ///2014-10-30
            Session.addFactoryMethod( ( ushort ) RM2C_WEB_EMAIL_TEXT.OPCODE, RM2C_WEB_EMAIL_TEXT.Create );

            ///2014-11-29
            Session.addFactoryMethod( ( ushort ) RM2C_SMONEY_SHOP_BUY.OPCODE, RM2C_SMONEY_SHOP_BUY.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_REFRESH_SMONEY_SHOP.OPCODE, RM2C_REFRESH_SMONEY_SHOP.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_GET_SMONEY_SHOP.OPCODE, RM2C_GET_SMONEY_SHOP.Create );

            ///2014-12-18
            Session.addFactoryMethod( ( ushort ) RM2C_PING_PRO_TWO.OPCODE, RM2C_PING_PRO_TWO.Create );

            ///2015-01-08
            Session.addFactoryMethod( ( ushort ) RM2C_MIN_NIGHT_REFRESH.OPCODE, RM2C_MIN_NIGHT_REFRESH.Create );

            ///2015-01-19
            Session.addFactoryMethod( ( ushort ) RM2C_POWER_LV_ADD.OPCODE, RM2C_POWER_LV_ADD.Create );

            ///2015-03-07
            Session.addFactoryMethod( ( ushort ) RM2C_BEAST_INFO.OPCODE, RM2C_BEAST_INFO.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_BEAST_LV_UP.OPCODE, RM2C_BEAST_LV_UP.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_NEW_BEAST.OPCODE, RM2C_NEW_BEAST.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_BEAST_EQUIP_WEAR.OPCODE, RM2C_BEAST_EQUIP_WEAR.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_BEAST_EQUIP_UP.OPCODE, RM2C_BEAST_EQUIP_UP.Create );

            ///2015-03-14
            Session.addFactoryMethod( ( ushort ) RM2C_SOUL_SHOP.OPCODE, RM2C_SOUL_SHOP.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_SOUL_SHOP_BUY.OPCODE, RM2C_SOUL_SHOP_BUY.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_SOUL_COM.OPCODE, RM2C_SOUL_COM.Create );

            ///2015-03-19
            Session.addFactoryMethod( ( ushort ) RM2C_SUN_WELL_INFO.OPCODE, RM2C_SUN_WELL_INFO.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_SUN_WELL_RESET.OPCODE, RM2C_SUN_WELL_RESET.Create );

            ///2015-03-20
            Session.addFactoryMethod( ( ushort ) RM2C_OPEN_GIFT_BOX.OPCODE, RM2C_OPEN_GIFT_BOX.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_MOT_BEAST.OPCODE, RM2C_MOT_BEAST.Create );

            ///2015-03-24
            Session.addFactoryMethod( ( ushort ) RM2C_CHAT.OPCODE, RM2C_CHAT.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_CHAT_RECENT_RESPONSE.OPCODE, RM2C_CHAT_RECENT_RESPONSE.Create );

            ///2015-04-03
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_INFO.OPCODE, RM2C_ESCORT_INFO.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_EESCORT_GROUP_LIST.OPCODE, RM2C_EESCORT_GROUP_LIST.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_GET_GROUP.OPCODE, RM2C_ESCORT_GET_GROUP.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_GET_TEAM.OPCODE, RM2C_ESCORT_GET_TEAM.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_BEAT_SELF_PET.OPCODE, RM2C_ESCORT_BEAT_SELF_PET.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_BEAT_SELF_BEAST.OPCODE, RM2C_ESCORT_BEAT_SELF_BEAST.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_BEAT_GROUP.OPCODE, RM2C_ESCORT_BEAT_GROUP.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_BEAT_TEAM.OPCODE, RM2C_ESCORT_BEAT_TEAM.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_FIND_GROUP.OPCODE, RM2C_ESCORT_FIND_GROUP.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_SET_SELF_TEAM.OPCODE, RM2C_ESCORT_SET_SELF_TEAM.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_GO_TO.OPCODE, RM2C_ESCORT_GO_TO.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_LEAVE.OPCODE, RM2C_ESCORT_LEAVE.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_HELPER.OPCODE, RM2C_ESCORT_HELPER.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_HELPER_INFO.OPCODE, RM2C_ESCORT_HELPER_INFO.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_BEAT_GROUP_ACT.OPCODE, RM2C_ESCORT_BEAT_GROUP_ACT.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_PK_OVER.OPCODE, RM2C_ESCORT_PK_OVER.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_ADD_BREAD.OPCODE, RM2C_ESCORT_ADD_BREAD.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_BUY_BREAD.OPCODE, RM2C_ESCORT_BUY_BREAD.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_LOG_GET_BREAD.OPCODE, RM2C_ESCORT_LOG_GET_BREAD.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_MAKE_SURE_FAIL.OPCODE, RM2C_ESCORT_MAKE_SURE_FAIL.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_LOG.OPCODE, RM2C_ESCORT_LOG.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_PK_GROUP.OPCODE, RM2C_ESCORT_PK_GROUP.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_GIVE_UP.OPCODE, RM2C_ESCORT_GIVE_UP.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_CREAT_LOG.OPCODE, RM2C_ESCORT_CREAT_LOG.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_SUCCEED.OPCODE, RM2C_ESCORT_SUCCEED.Create );

            ///2015-04-13
            Session.addFactoryMethod( ( ushort ) RM2C_MY_MOT_PK_INFO.OPCODE, RM2C_MY_MOT_PK_INFO.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_MOT_PK_ENEMY_INFO.OPCODE, RM2C_MOT_PK_ENEMY_INFO.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_MOT_PK_RANK_LIST.OPCODE, RM2C_MOT_PK_RANK_LIST.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_JOIN_MOT_PK.OPCODE, RM2C_JOIN_MOT_PK.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_MOT_PK_HELPER.OPCODE, RM2C_MOT_PK_HELPER.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_MOT_PK_BOX.OPCODE, RM2C_MOT_PK_BOX.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_MOT_PK_GOTO.OPCODE, RM2C_MOT_PK_GOTO.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_MOT_PK_REWARD.OPCODE, RM2C_MOT_PK_REWARD.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_MOT_PK_PK_ERROR.OPCODE, RM2C_MOT_PK_PK_ERROR.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_MOT_PK_LEAVE.OPCODE, RM2C_MOT_PK_LEAVE.Create );

            ///2015-04-20
            Session.addFactoryMethod( ( ushort ) RM2C_SIGN_BEAST_REQUEST.OPCODE, RM2C_SIGN_BEAST_REQUEST.Create );
            ///2015-04-22
            Session.addFactoryMethod( ( ushort ) RM2C_MOT_PK_REWARD_PKED.OPCODE, RM2C_MOT_PK_REWARD_PKED.Create );
            ///2015-04-22
            Session.addFactoryMethod( ( ushort ) RM2C_BOSS_RESET.OPCODE, RM2C_BOSS_RESET.Create );

            ///2015-05-07
            Session.addFactoryMethod( ( ushort ) RM2C_GET_ACTIVITY_INFO.OPCODE, RM2C_GET_ACTIVITY_INFO.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_GET_ACTIVITY_REWARD.OPCODE, RM2C_GET_ACTIVITY_REWARD.Create );

            ///2015_05_15
            Session.addFactoryMethod( ( ushort ) RM2C_ESCORT_BEAT_GROUP_TIME_OUT.OPCODE, RM2C_ESCORT_BEAT_GROUP_TIME_OUT.Create );

            ///2015-06-01
            Session.addFactoryMethod( ( ushort ) RM2C_FIRST_DAY_BUY_POWER_ACT_INFO.OPCODE, RM2C_FIRST_DAY_BUY_POWER_ACT_INFO.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_FIRST_DAY_BUY_POWER_ACT_REWARD.OPCODE, RM2C_FIRST_DAY_BUY_POWER_ACT_REWARD.Create );

            ///2015-06-23
            Session.addFactoryMethod( ( ushort ) RM2C_SEND_SERVER_ID.OPCODE, RM2C_SEND_SERVER_ID.Create );

            ///2015-08-03
            Session.addFactoryMethod( ( ushort ) RM2C_GU_MEMBER_LIST.OPCODE, RM2C_GU_MEMBER_LIST.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_GET_GAME_UNION_INFO.OPCODE, RM2C_GET_GAME_UNION_INFO.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_GAME_UNION_LIST.OPCODE, RM2C_GAME_UNION_LIST.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_SEARCH_GAME_UNION.OPCODE, RM2C_SEARCH_GAME_UNION.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_CREATE_GAME_UNION.OPCODE, RM2C_CREATE_GAME_UNION.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_OPEN_SET_GAME_UNION.OPCODE, RM2C_OPEN_SET_GAME_UNION.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_SET_GAME_UNION.OPCODE, RM2C_SET_GAME_UNION.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_REQUEST_JOIN_GAME_UNION.OPCODE, RM2C_REQUEST_JOIN_GAME_UNION.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_REQUEST_JOINER_LIST.OPCODE, RM2C_REQUEST_JOINER_LIST.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_AGREEN_JOIN.OPCODE, RM2C_AGREEN_JOIN.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_APPOINT_JOB.OPCODE, RM2C_APPOINT_JOB.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_EMAIL_ALL_MEMBER.OPCODE, RM2C_EMAIL_ALL_MEMBER.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_DISBAND_GAME_UNION.OPCODE, RM2C_DISBAND_GAME_UNION.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_GET_GAME_UNION_LOG.OPCODE, RM2C_GET_GAME_UNION_LOG.Create );
            Session.addFactoryMethod( ( ushort ) RM2C_NOTIFY_LEAD_GU_INFO.OPCODE, RM2C_NOTIFY_LEAD_GU_INFO.Create );

            ///2015-08-06
            Session.addFactoryMethod( ( ushort ) RM2C_STOP_INFO.OPCODE, RM2C_STOP_INFO.Create );

        }
    }
}
