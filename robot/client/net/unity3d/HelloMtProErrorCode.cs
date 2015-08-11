using System;
using System.Runtime.InteropServices;
using ExFormatter;

namespace net.unity3d
{
	public enum EM_CLIENT_ERRORCODE
    {
		EM_M2C_SUCCESSFUL					=	1,	/*所有协议的成功返回*/
		///*服务器错误
		EE_SERVER_ERROR = 2,							
		///*账号不存在*/
		EE_ACCOUNT_NOT_EXIST = 3,						
		///*密码错误*/
		EE_PASSWORD_ERROR = 4,							
		///*账号在该服务器没有创建角色*/
		EE_LEAD_NOT_EXIST = 5,							
		///*角色转换状态失败*/
		EE_M2C_ROLE_SWITCH_STATE_ERROR = 6,				
		///*正在登陆*/
		EE_M2C_ROLE_IS_LOGGING = 7,						
		///*需要创建角色*/
		EE_M2C_NEED_CREATE_ROLE = 8,					
		///*没有在登陆*/
		EE_M2C_ROLE_IS_NOT_LOGGING = 9,					
		///*角色状态错误*/
		EE_M2C_ROLE_STATUS_ERROR =10,					
		///*被人踢掉了*/
		EE_M2C_TICK_BY_OTEHR = 11,
		/*角色创建失败*/
		EE_M2C_CREATE_ROLE_ERROR = 12,
		/*csv 表错误*/
		EE_M2C_CSV_TABLE_ERROR = 13,
		/*无法找到主角信息*/
		EE_M2C_FIEND_NOT_LEAD_INFO = 14,	
		/*阵法宠物错误*/
		EE_M2C_METHOD_PET_ERROR = 15,			
		/*阵法站位错误*/
		EE_M2C_METHOD_LOC_ERROR = 16,		
		/*角色不存在*/
		EE_M2C_ROLE_NOT_EXIST = 17,		
		/*战斗验证错误*/
		EE_M2C_CHECK_FIGHT = 18,	
		/*csv表中不存在该角色*/
		EE_M2C_ROLE_NOT_EXIST_IN_CSV = 19,		
		/*创建宠物失败*/
		EE_M2C_CRATE_PET = 20,				
		/*同一个角色错误*/
		EE_M2C_ROLE_SAME_ERROR = 21,	
		/*创建装备失败*/
		EE_M2C_EQUIP_PET = 22,			
		/*游戏币不足*/
		EE_M2C_SMONEY_NOT_ENOUGH = 23,		
		/*创建宠物碎片失败*/
		EE_M2C_CHIP_PET = 24,			
		/*宠物不是最高级*/
		EE_M2C_PET_LV_NOT_MAX = 25,				
		/*物品不够*/
		EE_M2C_GOODS_NOT_ENOUGH = 27,				
		/*已经是最高品质了*/
		EE_M2C_GRADE_MAX_ERROR = 29,			
		/*宠物在阵上*/
		EE_M2C_PET_IN_TEAM = 31,			
		/*好友伙伴错误*/
		EE_M2C_FRIEND_PET = 33,				
		/*技能错误*/		
		EE_M2C_SKILL_ERROR = 35,		
		/*已经是最高的技能等级了*/
		EE_M2C_SKILL_LV_MAX = 37,		
		/*卡牌的数量错误*/
		EE_M2C_PET_CNT_ERROR = 39,			
		/*物品不存*/
		EE_M2C_EQUIP_NOT_EXIST = 41,				
		/*物品数量错误*/
		EE_M2C_EQUIP_CNT_ERROR = 43,		
		/*物品id错误*/
		EE_M2C_EQUIP_ID_ERROR = 45,		
		/*卡牌id错误*/
		EE_M2C_PET_ID_ERROR = 47,		
		/*消耗的不对*/
		EE_M2C_CONSUME_ERROR = 49,		
		/*体力满的*/
		EE_M2C_POWER_MAX = 51,
		/*爵位奖励领取次数耗尽*/
		EE_NOTILITY_REWARD_ZERO = 52,		
		/*装备限制职业*/
		EE_M2C_EQUIP_LIMIT_JOB = 53,		
		/*竞技次数耗尽*/
		EE_M2C_PK_CNT_ZERO = 54,		
		/*队伍id错误*/
		EE_M2C_TEAM_ID_ERROR = 55,		
		/*没找打竞技角色*/
		EE_M2C_FIND_NOT_PK_ROLE = 56,		
		/*队伍队长id错误*/
		EE_M2C_TEAM_LEAD_ID_ERROR = 57,			
		/*竞技场只有你自己*/
		EE_M2C_ONLY_YOU_IN_PK = 58,				
		/*领导力错误*/
		EE_M2C_LEAD_SHIP_ERROR = 59,			
		/*初始化战斗数据失败*/
		EE_M2C_INIT_PK_FAILED = 60,				
		/*卡牌不存在*/
		EE_M2C_PET_NOT_EXIST = 61,				
		/*竞技数据错误*/
		EE_M2C_PK_DATA_ERROR = 62,				
		/*体力不足*/
		EE_M2C_POWER_ERROR = 63,				
		/*数据库创建邮件失败*/
		EE_M2C_DB_CREATE_EMAIL_ERROR = 64,			
		/*碎片不存在*/
		EE_M2C_PIECE_NOT_EXIST = 65,				
		/*未找到此邮件*/
		EE_M2C_FIND_NOT_EMAIL = 66,						
		/*碎片数量不够*/
		EE_M2C_PIECE_CNT_LESS = 67,				
		/*此邮件繁忙*/
		EE_M2C_EMAIL_BUSY = 68,						
		/*碎片数量错误*/
		EE_M2C_PIECE_CNT_ERROR = 69,				
		/*CSV表中不存在此邮件*/
		EE_M2C_EMAIL_NOT_IN_CSV_TABLE = 70,			
		/*卡牌被保护*/
		EE_M2C_PET_PROTECT = 71,	
		/*邮件已摧毁*/
		EE_M2C_EMAIL_IS_DESTROY = 72,		
		/*技能类型错误*/
		EE_M2C_SKILL_TYPE_ERROR = 73,				
		/*邮件已打开*/
		EE_M2C_EMAIL_IS_OPEN = 74,				
		/*队伍类型错误*/
		EE_M2C_TEAM_TYPE_ERROR = 75,				
		/*邮件已过期*/
		EE_M2C_EMAIL_OUT_OF_DATE = 76,				
		/*副本id错误*/
		EE_M2C_FB_ID_ERROR = 77,					
		/*竞技场剩余购买次数不够*/
		EE_M2C_PK_BUY_CNT_ERROR = 78,				
		/*副本评星太低*/
		EE_M2C_FB_KO_LV_TO_LOW = 79,				
		/*合成信息不存在*/
		EE_M2C_COM_INFO_NOT_FIND = 80,					
		/*创建副本奖励的时候出现错误*/
		EE_M2C_FB_REWARD_ERROR = 81,				
		/*装备或材料不足*/
		EE_M2C_GOODS_NOT_ENOUTH = 82,				
		/*id错误*/
		EE_M2C_LAND_ID_ERROR = 83,						
		/*宠物不存在*/
		EE_M2C_PET_NOT_FIND = 84,					
		/*副本类型错误*/
		EE_M2C_FB_TYPE_ERRO = 85,						
		/*装备位置错误*/
		EE_M2C_EQUIP_LOC_ERROR = 86,				
		/*大陆星数统计错误*/
		EE_M2C_LAND_STAR_CNT_ERROR = 87,			
		/*该位置不存在装备*/
		EE_M2C_LOC_NOT_HAVE_EQUIP = 88,				
		/*宠物类型错误*/
		EE_M2C_PET_TYPE_ERROR = 89,					
		/*装备无法升级到指定id*/
		EE_M2C_EQUIP_UP_ID_ERROR = 90,				
		/*最大的通关次数*/
		EE_M2C_MAX_KO_TIMES = 91,					
		/*宠物id为空*/
		EE_M2C_PET_ID_IS_NULL = 92,					
		/*已经是好友了*/
		EE_M2C_IS_FRIEND = 93,							
		/*csv 表中不存在此物品*/
		EE_M2C_EQUIP_CSV_ERROR = 94,					
		/*id 错误*/
		EE_M2C_FRIEND_ID_ERROR = 95,					
		/*钱不够*/
		EE_M2C_MONEY_NOT_ENOUGH = 96,				
		/*已经申请过了*/
		EE_M2C_IS_ASK_FRIEND = 97,					
		/*装备已经强化到最高级了*/
		EE_M2C_EQUIP_IS_MAX_UP = 98,					
		/*自己的id*/
		EE_M2C_SELF_ID = 99,							
		/*强化的下一级装备不存在*/
		EE_M2C_UP_NEXT_EQUIP_NOT_EXEIT = 100,			
		/*最大好友*/
		EE_M2C_FRIEND_MAX = 101,						
		/*一键合成遍历大于30次（怀疑死循环）*/
		EE_M2C_EQUIP_UP_ONE_KEY_DEAD = 102,			
		/*今天已经赠送过这个人了*/
		EE_M2C_FRIEND_POWER_SEND = 103,					
		/*战斗验证数组错误*/
		EE_M2C_FIGHT_ROUND_NUM_ERROR = 104,				
		/*送完了*/
		EE_M2C_FRIEND_POWER_SEND_MAX = 105,				
		/*爬塔已经到最高级*/
		EE_M2C_TOWER_MAX_LV = 106,						
		/*最大次数*/
		EE_M2C_MAX_CNT_ERROR = 107,						
		/*没找到塔信息*/
		EE_M2C_NOT_FIND_TOWER_ID = 108,					
		/*类型错误*/
		EE_M2C_CONSUME_TYPE_ERROR = 109,				
		/*已经领取过爬塔每日奖励*/
		EE_M2C_HAD_GET_TOWER_REWARD = 110,				
		/*好友今天已经切磋了*/
		EE_M2C_FRIEND_HAS_PK = 111,						
		/*处于塔的第一层，不可重置*/
		EE_M2C_AT_TOWER_FIRST_LV = 112,					
		/*今天好友切磋最大次数*/
		EE_M2C_FRIEND_PK_CNT_MAX = 113,					
		/*爬塔重置次数以为零*/
		EE_M2C_TOWER_AGAIN_ZERO = 114,					
		/*该名字已经存在*/
		EE_M2C_ROLE_NAME_EXIST = 115,					
		/*不可领取通塔奖励*/
		EE_M2C_CAN_NOT_TOWER_REWARD = 116,				
		/*背包类型错误*/
		EE_M2C_BAG_TYPE_ERROR = 117,					
		/*灵石不够*/
		EE_M2C_TOWER_MONEY_NOT_ENOUGHT = 118,			
		/*最大背包购买数量*/
		EE_M2C_BAG_BUY_MAX = 119,						
		/*刷新爬塔商店类型错误*/
		EE_M2C_TOWER_REFRESH_TYPE_REEOR = 120,			
		/*这个账号已经在这个服务器绑定了*/
		EE_M2C_ACCOUNT_HAS_ROLE = 121,				
		/*不是爬塔商店刷新时间*/
		EE_M2C_NOT_REFRESH_TIME = 122,					
		/*已经绑定过了*/
		EE_M2C_ROLE_HAS_BIND = 123,					
		/*爬塔商店购买位置错误*/
		EE_M2C_TOWER_SHOP_LOC_ERROR = 124,				
		/*近战太多*/
		EE_M2C_PET_NEAR_MAX = 125,					
		/*没找到商店物品*/
		EE_M2C_NOT_FIND_SHOP_GOODS = 126,			
		/*卡牌身上有装备*/
		EE_M2C_PET_EQUIP_ERROR = 127,					
		/*爬塔商店物品已售*/
		EE_M2C_TOWER_SHOP_GOODS_HAD_SOLD = 128,			
		/*这个人已经起过名字了*/ 
		EE_M2C_ROLE_RENAME_ERROR = 129,
		/*爬塔商店不出售此类型物品*/
		EE_M2C_TOWER_SHOP_NOT_SELL_TYPE = 130,	
		/*等级限制*/
		EE_M2C_ROLE_LV_LIMT = 131,				
		/*没找到竞技商店物品*/
		EE_M2C_NOT_FIND_PK_SHOP_GOODS = 132,		
		/*竞技积分不足*/
		EE_M2C_SOCRE_FIGHT_NOT_ENOUGH = 134,		
		/*竞技商店不出售此类型物品*/
		EE_M2C_PK_SHOP_NOT_SELL_TYPE = 136,				
		/*竞技阵法没有队长*/
		EE_M2C_PK_TEAM_LEADER_ERROR = 138,				
		/*不是竞技商店刷新时间*/
		EE_M2C_NOT_PK_SHOP_REFRESH_TIME = 140,		
		/*已经有名字了*/
		EE_M2C_HAS_NAME = 141,						
		/*竞技积分不够*/
		EE_M2C_SCORE_FIGHT_NOT_ENOUGHT = 142,			
		/*友情点不够*/
		EE_M2C_FRIEND_SHIP_NOT_ENOUGH = 143,			
		/*刷新竞技商店类型错误*/
		EE_M2C_PK_SHOP_REFRESH_TYPE_REEOR = 144,		
		/*抽奖类型错误*/
		EE_M2C_LUCKY_SHOP_TYPE_ERROR = 145,				
		/*竞技商店物品已售*/
		EE_M2C_PK_SHOP_GOODS_HAD_SOLD = 146,			
		/*系统未开启*/
		EE_M2C_SYSTEM_NOT_OPEN = 147,				
		/*竞技商店购买位置错误*/
		EE_M2C_PK_SHOP_LOC_ERROR = 148,				
		/*账号和账户对不上*/ 
		EE_M2C_AC_ID_ERROR = 149,						
		/*玩家没有此活动*/
		EE_M2C_NOT_FIND_ACTIV_EVENT = 150,				
		/*校验account session 错误*/
		EE_M2C_AC_SESSION_ERROR = 151,					
		/*csv表中没有此活动*/
		EE_M2C_NOT_FIND_ACTIV_EVENT_CSV = 152,			
		/*时间错误*/
		EE_M2C_TIME_ERROR = 153,					
		/*活动没有这么多怪物波次*/
		EE_M2C_ACTIV_EVENT_ENEMY_ID_ERROR = 154,	
		/*订单csv id错误*/
		EE_M2C_CHARGE_CSV_ID_ERROR = 155,			
		/*活动次数用尽*/
		EE_M2C_ACTIV_EVENT_CNT_ZERO = 156,		
		/*充值还未过期*/
		EE_M2C_CHARGE_TIME_ERROR = 157,				
		/*活动购买次数用尽*/
		EE_M2C_ACTIV_EVENT_BUY_ZERO = 158,				
		/*订单错误*/
		EE_M2C_ORDER_ID_ERROR = 159,			
		/*玩家不在村庄中*/
		EE_M2C_IS_NOT_IN_COUNTRY = 160,				
		/*数据库错误*/
		EE_M2C_DATABASE_ERROR = 161,			
		/*活动未开启*/
		EE_M2C_ACTIV_EVENT_NOT_OPEN = 162,			
		/*充值过期了*/
		EE_M2C_CHARGE_TIME_INVALID = 163,				
		/*没有开启副本*/
		EE_M2C_NOT_OPEN_FB = 164,					
		/*今天已经领取了*/
		EE_M2C_CHARGE_TODAY_OVER = 165,					
		/*未找到世界boss鼓舞花费*/
		EE_M2C_NOT_FIND_WORLD_BOSS_EXCITE = 166,		
		/*服务器繁忙等待*/
		EE_M2C_SERVER_BUSY = 167,					
		/*服务器维护*/
		EE_M2C_SERVER_MAINTAIN = 168,	
		/*realm服务器未开启*/
		EE_M2C_REALM_NOT_OPEN = 170,
		/*最大点金石次数*/
		EE_M2C_STONE_MAX_TIME = 171,	
		/*没找到竞技场已经匹配过角色*/
		EE_M2C_FIND_NOT_HAN_PK_ROLE = 172,		
		/*背包上限*/
		EE_M2C_BAG_MAX = 173,	
		/*没找竞技场机器人*/
		EE_M2C_FIND_NOT_PK_BOT = 174,	
		/*没找竞技场机器人总表*/
		EE_M2C_FIND_NOT_PK_BOT_ALL = 175,	
		/*对方好友达到上限*/
		EE_M2C_FRIEND_FRIEND_MAX = 176,		
		/*没有酒足饭饱的次数了*/
		EE_M2C_POWER_MEAL_NO_TIME = 177,		
		/*没找竞技场机器人子表*/
		EE_M2C_FIND_NOT_PK_BOT_SUB = 178,	
		/*今天已经签到了*/
		EE_M2C_SIGN_TODAY = 179,				
		/*竞技场商店忙*/
		EE_M2C_PK_SHOP_BUSY = 180,				
		/*奖励创建失败*/
		EE_M2C_REWARD_ID_ERROR = 181,			
		/*爬塔商店忙*/
		EE_M2C_TOWER_SHOP_BUSY = 182,			
		/*没有漏签*/
		EE_M2C_SIGN_DAYS_FULL = 183,			
		/*正义徽章商店无此物品*/
		EE_M2C_BADGE_SHOP_NOT_FIND = 184,		
		/*补签的最大次数*/
		EE_M2C_SIGN_RE_MAX = 185,				
		/*正义徽章不够*/
		EE_M2C_BADGE_NOT_ENOUTH = 186,			
		/*补签的次数错误*/
		EE_M2C_SIGN_RE_DAYS_ERROR = 187,			
		/*断线重连玩家在在线表中*/
		EE_M2C_BAD_LINE_IN_ONLINE = 188,			
		/*任务未完成*/
		EE_M2C_TASK_NOT_DONE = 189,						
		/*断线重连玩家不再离线表中*/
		EE_M2C_BAD_LINE_NOT_IN_OFFLINE = 190,			
		/*改任务已经领取奖励*/
		EE_M2C_TASK_REWARD_DONE = 191,					
		/*断线重连玩家无法插入到在线表*/
		EE_M2C_BAD_LINE_CAN_NOT_IN_ONLINE = 192,	
		/*任务id错误*/
		EE_M2C_TASK_ID_ERROR = 193,					
		/*队伍中有相同CSV id的卡牌*/
		EE_M2C_TEAM_HAVE_SAME_CSV_ID_PET = 194,		
		/*等级不够*/
		EE_M2C_ROLE_LV_NOT_ENOUGH = 195,			
		/*初始化被动技能错误*/
		EE_M2C_ATTRIBUTE_SKILL_ERROR = 196,
		/*卡牌满星*/
		EE_M2C_PET_STAR_MAX = 197,						
		/*初始化自己战斗数据失败*/
		EE_M2C_INIT_PK_SELF_FAILED = 198,
		/*技能点不足*/
		EE_M2C_SKILL_POINT_NOT_ENOUGH = 199,			
		/*初始敌人化战斗数据失败*/
		EE_M2C_INIT_PK_ENEMY_FAILED = 200,
		/*技能点不是0*/
		EE_M2C_SKILL_POINT_NOT_ZERO = 201,				
		/*初始化自己技能战斗数据失败*/
		EE_M2C_INIT_PK_SELF_SKILL_FAILED = 202,
		/*购买技能点次数错误*/
		EE_M2C_SKILL_POINT_CNT_BUY_ERROR = 203,			
		/*初始化敌人技能战斗数据失败*/
		EE_M2C_INIT_PK_ENEMY_SKILL_FAILED = 204,
		/*存在相同的卡牌*/
		EE_M2C_SAME_PET_EXIT = 205,						
		/*Hero.csv表中宠物不存在*/
		EE_M2C_PET_NOT_FIND_IN_HERO = 206,
		/*免费抽cd错误*/
		EE_M2C_LUCKY_SHOP_CD_ERROR = 207,				
		/*HeroUp.csv表中宠物不存在*/
		EE_M2C_PET_NOT_FIND_IN_HERO_UP = 208,
		/*免费抽次数用尽了*/	
		EE_M2C_LUCKY_SHOP_FREE_TIMES_OVER = 209,		
		/*进阶石嵌入位置错误*/
		EE_M2C_STONE_INLAY_LOC_ERROR = 210,
		/*魂侠表错误*/	
		EE_M2C_LUCKY_SOUL_CSV_ERROR = 211,				
		/*进阶石限制等级错误*/
		EE_M2C_STONE_LIMIT_LV = 212,			
		/*凹槽已经镶嵌进阶石*/
		EE_M2C_STONE_HAD_INLAY = 214,			
		/*没找到进阶石*/
		EE_M2C_STONE_NOT_FIND = 216,		
		/*消耗进阶石错误*/
		EE_M2C_STONE_COST_ERROR = 218,		
		/*未找到合成id*/
		EE_M2C_NOT_FIND_CONSUME_ID = 220,		
		/*Consume.csv表中未找到信息*/
		EE_M2C_NOT_FIND_CONSUME_CSV = 222,		
		/*宠物进阶游戏币不够*/
		EE_M2C_PET_UP_SMONEY = 224,				
		/*进阶石没满，不可进化*/
		EE_M2C_STONE_NOT_ENOUGH = 226,			
		/*宠物已经达到最高阶，不可进化*/
		EE_M2C_PET_MAX_STONE = 228,		
		/*玩家等级没达到活动等级限制*/
		EE_M2C_ACTIC_EVENT_LV = 230,
		/*两次聊天相隔不足10秒*/
		EE_M2C_CHAT_TIME_TOO_CLOSE = 251,	
		/*当日的聊天次数已经用完了*/
		EE_M2C_CHAT_TIMES_OVER = 253,		
		/*聊天空字符串*/
		EE_M2C_CHAT_EMPTY_CONTENT = 255,    
		/*私聊目的角色不存在*/
		EE_M2C_CHAT_NOT_EXIST_DST_ROLE = 257,   
		/* 帮派聊天没有找到相应的帮派*/
		EE_M2C_CHAT_NOT_EXIST_DST_GANG = 259,	
		EE_M2C_CHAT_NOT_ENOUGH_MONEY= 261,				/* 聊天需要扣除砖石时砖石不够用*/

		EE_M2C_CAN_NOT_FIND_CONTROL_SERVER = 262,		/*无法找到相应服务器*/
		EE_M2C_BOSS_INIT_TEAM = 264,					/*打boss初始化队伍信息失败*/
		EE_M2C_NOT_FIND_BOSS_CSV = 266,					/*未找到boss csv 信息*/
		EE_M2C_NOT_FIND_BOSS_STADN = 268,				/*未找到boss阵法站位*/
		EE_M2C_NOT_FIND_BOSS_INFO = 270,				/*未找到boss信息*/
		EE_M2C_MOT_SHOP_NOT_SELL_TYPE = 272,			/*海山商店不出售此类型物品*/
		EE_M2C_MOT_MONEMY_NOT_ENOUGHT = 274,			/*海山币不够*/
		EE_M2C_MOT_SHOP_REFRESH_TYPE_REEOR = 276,		/*刷新海山商店类型错误*/
		EE_M2C_MOT_SHOP_GOODS_HAD_SOLD = 278,			/*海山商店物品已售*/
		EE_M2C_MOT_SHOP_LOC_ERROR = 280,				/*海山商店购买位置错误*/
		EE_M2C_MOT_SHOP_BUSY = 282,						/*海山场商店忙*/
		EE_M2C_WEB_EMAIL_OPEN = 284,					/*网络邮件已经打开*/
		EE_M2C_NOT_FIND_MOT_ENEMY = 286,				/*未找到远征敌人*/
		EE_M2C_MOT_FRIEND_INIT = 288,					/*远征初始化基友战斗信息失败*/
		EE_M2C_MOT_SELF_INIT = 290,						/*远征初始化本人战斗信息失败*/
		EE_M2C_MOT_SELF_AFTER_INIT = 292,				/*远征初始化本人后续战斗信息失败*/
		EE_M2C_MOT_PET_DEAD = 294,						/*海山宠物已经死亡，不能上阵*/
		EE_M2C_WEB_EMAIL_SEND_TYPE = 296,				/*网络邮件发放类型错误*/
		EE_M2C_CONTROL_SERVER_POWER = 298,				/*后台控制用户权限不够*/
		EE_M2C_SMONEY_SHOP_NOT_SELL_TYPE = 300,			/*金币商店不出售此类型物品*/
		EE_M2C_SMONEY_SHOP_GOODS_HAD_SOLD = 302,		/*金币商店物品已售*/
		EE_M2C_SMONEY_SHOP_BUSY = 304,					/*金币场商店忙*/
		EE_M2C_ATTRIBUTE_TABLE_ERROR = 306,				/*attribute 表错误*/
		EE_M2C_SMONEY_SHOP_MAX_REFRESH = 308,			/*金币商店刷新次数达到上限*/
		EE_M2C_WEB_EMAIL_ID_NOT_FIND = 310,				/*网络邮件id未找到*/
		EE_M2C_WEB_EMAIL_BUSY = 312,					/*网络邮件忙*/
		EE_M2C_FIXTIME_TOO_SMALL = 314,					/*定时时间过短*/
		EE_M2C_SMOENY_SHOP_CSV = 316,					/*金币商店CSV表错误*/
		EE_M2C_REALM_ROLE_CNT_MAX = 318,				/*realm 服务器人数达到上限*/
		EE_M2C_NOT_MID_NIGHT_TIME = 320,				/*未到0点更新时间*/
		EE_M2C_BEAST_ID_ERROR = 322,					/*魂兽id错误*/
		EE_M2C_PRO_EXP_TYPE_ERROR = 324,				/*经验药类型错误*/
		EE_M2C_EQUIP_UP_LOC_ERROR = 326,				/*装备升级时位置错误*/
		EE_M2C_BEAST_NOT_FIND = 328,					/*魂兽不存在*/
		EE_M2C_BEAST_ID_IS_NULL = 330,					/*魂兽id为空*/
		EE_M2C_ACTIV_EVENT_TYPE = 332,					/*活动类型错误*/
		EE_M2C_BEAST_CHIP_NOT_COM = 334,				/*此魂兽碎片不可以合成魂兽*/
		EE_M2C_NOT_FIND_BEAST_CHIP = 336,				/*未找到魂兽碎片*/
		EE_M2C_BEAST_CHIP_CNT = 338,					/*魂兽碎片不足*/
		EE_M2C_BEAST_HAD = 340,							/*魂兽已经存在*/
		EE_M2C_BEAST_COM_ERROR = 342,					/*魂兽合成未知失败*/
		EE_M2C_BEAST_SHOP_LOC = 344,					/*魂兽商店购买位置错误*/
		EE_M2C_BEAST_SHOP_CSV_TABLE = 346,				/*魂兽商店csv表错误*/
		EE_M2C_BEAST_SHOP_NOT_GIVE = 348,				/*魂兽商店不产出此类型*/
		EE_M2C_SUN_WELL_BOSS_NOT_PASS = 350,			/*太阳井前置boss未打败*/
		EE_M2C_BEAST_SHOP_HAD_SELLD = 352,				/*魂兽商店位置已经出售*/
		EE_M2C_SUN_WELL_HAD_RESET = 354,				/*太阳井重置次数达到上限*/
		EE_M2C_NOT_FIND_CSV_GIFT_BOX = 356,				/*未找到csv宝箱信息*/
		EE_M2C_GIFT_BOX_CNT = 358,						/*宝箱数量不够*/
		EE_M2C_NOT_FIND_GIFT_BOX = 360,					/*未找到宝箱*/
		EE_M2C_BEAST_EQU_GRADE_LV = 362,				///玩家等级未达到魂兽装备品阶细分要求
		EE_M2C_BEASE_EQU_CSV_NOT_FIND = 364,			///魂兽装备csv信息未找到
		EE_M2C_BEAST_EQU_GRADE = 366,					///玩家等级未达到魂兽装备品阶要求
		EE_M2C_NOT_FIND_LEAD_LV_CSV = 368,					///玩家等级csv信息未找到
		EE_M2C_SKILL_LV_LIMIT = 369,					///技能等级限制
		EE_M2C_GIFT_BOX_OPEN_MAX_CNT = 370,				///礼盒开启达到上限
		EE_M2C_CHAT_LEVEL_LIMIT = 371,					//聊天等级限制
		EE_M2C_ESCORT_FIND_GROUP_NOT_ENOUTH_MONEY  = 372, ///搜索商队金币不足
		EE_M2C_ESCORT_FIND_GROUP_MAX_TIMES = 373,			/// 搜索商队次数达到上限了 
		EE_M2C_ESCORT_SYSTEM_NOT_OPEN  = 374,   /// 护送相关操作，系统未开启
		EE_M2C_ESCORT_SET_TEAM_GROUP_NOT_EXIST = 375, /// 护送设置队伍，商队不存在
		EE_M2C_ESCORT_SET_TEAM_GROUP_SESSION_WRONG = 376, ///护送设置队伍，商队session错误
		EE_M2C_ESCORT_SET_TEAM_TEAM_NOT_EXIST = 377,			/// 护送设置队伍，队伍不存在
		EE_M2C_ESCORT_SET_TEAM_PET_WRONG = 378,					///护送设置队伍，卡牌信息不对，不错在或者级别不够
		EE_M2C_ESCORT_SET_TEAM_BEAST_NOT_EXIST = 379,			///护送设置队伍，魂兽不存在
		EE_M2C_ESCORT_GO_TO_STATE_ERROR = 380,						/// 进入护送状态错误
		EE_M2C_ESCORT_LEAVE_STATE_ERROR = 381,						///离开护送状态错误
		EE_M2C_ESCORT_HELPER_ID_ERROR = 382,							///选择护送基友id错误
		EE_M2C_ESCORT_HELPER_HAS_BUY = 383,								///选择护送基友，发现已经存在护送基友了
		EE_M2C_ESCORT_BREAD_NOT_ENOUGH = 384,						///护送战斗时，面包不足
		EE_M2C_ESCORT_PK_FRIEND_INFO_ERROR = 385,					///护送战斗时，基友信息对不上了
		EE_M2C_ESCORT_PK_SELF_INIT_ERROR = 386,					///护送战斗时，个人信息初始化出错
		EE_M2C_ESCORT_PK_FRIEND_INIT_ERROR = 387,					///护送战斗时，个人信息初始化出错
		EE_M2C_ESCORT_PK_SELF_INIT_AFTER_ERROR = 388,					///护送战斗时，个人信息初始化后出错
		EE_M2C_ESCORT_PK_ENEMY_INIT_ERROR = 389,					///护送战斗时，敌人信息初始化错误
		EE_M2C_ESCORT_BREAD_RECOVER_TIME_ERROR = 390,			/// 护送恢复面包，时间不到
		EE_M2C_ESCORT_BREAD_RECOVER_MAX_CNT = 391,				/// 护送恢复面包失败，已经达到上限
		EE_M2C_ESCORT_BREAD_BUY_MAX_BREAD_CNT = 392,						/// 护送购买面包，已经达到上限
		EE_M2C_ESCORT_BREAD_BUY_MAX_BUY_CNT = 393,						/// 护送购买面包，购买次数已达到上限
		EE_M2C_ESCORT_BREAD_BUY_NOT_ENOUGH_MONEY = 394,			//// 护送购买面包，qmoney不足
		EE_M2C_ESCORT_LOG_GET_BREAD_NOT_GET_LOG = 395,				///护送从log中取面包，没有找到log
		EE_M2C_ESCORT_LOG_GET_BREAD_NO_BREAD = 396,					/// 护送从log中去面包，次护送log中没有可以取的面包了
		EE_M2C_ESCORT_MAKE_SURE_FAIL_GROUP_ERROR = 397,			/// 护送确认失败，领取奖励时，护送的商队不存在或者sessionid不对
		EE_M2C_ESCORT_MAKE_SURE_FAIL_GROUP_STATE_ERROR = 398,	/// 护送确认失败，领取奖励时，护送的商队信息错误
		EE_M2C_ESCORT_SUCCEED_GROUP_ERROR = 399,							/// 护送领取完成奖励时，商队信息不对，不存在或者session不对
		EE_M2C_ESCORT_SUCCEED_TEAM_ERROR = 400,							/// 护送领取完成奖励时，队伍信息不对，不存在或者session不对
		EE_M2C_ESCORT_SUCCEED_GROUP_INFO_ERROR = 401,					/// 护送领取完成奖励时，商队信息不对，不能领
		EE_M2C_ESCORT_SUCCEED_TEAM_INFO_ERROR = 402,					/// 护送领取完成奖励时，队伍信息不对，不能领
		EE_M2C_ESCORT_GIVE_UP_GROUP_ERROR  = 403,			 				/// 护送放弃队伍时，商队信息不对，不存在或者session不对
		EE_M2C_ESCORT_GIVE_UP_TEAM_ERROR = 404,								/// 护送放弃队伍时，队伍信息不对，不存在或者session不对
		EE_M2C_ESCORT_GIVE_UP_GROUP_INFO_ERROR = 405,					/// 护送放弃队伍时，商队信息不对，不能领
		EE_M2C_ESCORT_GIVE_UP_TEAM_INFO_ERROR = 406,					/// 护送放弃队伍时，队伍信息不对，不能领
		EE_M2C_ESCORT_CHAT_TIME_INTERVAL_ERROR = 407,					///护送分享需要间隔120秒
		
		EE_M2C_MOT_PK_NOT_INIT = 408,					//海山奖励关信息未初始化完毕
		EE_M2C_MOT_PK_JOIN_ERR = 409, 					//海山奖励关报名失败(已报名)
		EE_M2C_MOT_PK_CHANGE_ERR = 410,					//海山奖励关更换失败(未报名)
		EE_M2C_MOT_PK_ACTIVE_ERR = 411,					//海山奖励关激活NPC失败
		EE_M2C_MOT_PK_HELPER_ID_ERROR = 412,			//海山奖励关佣兵id错误
		EE_M2C_MOT_PK_HELPER_HAS_BUY = 413,				/*海山奖励关已经有佣兵了*/
		EE_M2C_MOT_PK_BOX_REWARD_ERR = 414,				/*海山奖励关宝箱开启失败*/
		EE_M2C_MOT_PK_BOX_ID_ERROR = 415, 				/*海山奖励关宝箱ID错误*/
		EE_M2C_MOT_PK_GOTO_ERROR = 416,					/*海山奖励关进入错误*/
		EE_M2C_MOT_PK_PK_CHECK_ERROR = 417, 				/*海山奖励关校验错误*/
		EE_M2C_MOT_PK_REWARD_PKED_ERR = 418,				/*海山奖励关领取防守次数奖励失败*/
		
		EE_M2C_SIGN_BEAST_ERROR = 420,				//签到送魂兽错误
		EE_M2C_BOSS_RESET_ERROR = 421,					/*boss重置错误*/
		EE_M2C_BOSS_TEAM_SET_ERROR = 422,			//世界boss队伍设置错误
		
		EE_M2C_ESCORT_PKS_GROUP_CNT_MAX = 1000,					/// 商队数达到上限
		EE_M2C_ESCORT_PKS_GROUP_SEARCH_CNT_MAX = 1001,			/// 搜索商队次数达到上限
		EE_M2C_ESCORT_PKS_GROUP_TYPE = 1002,					/// 商队类型异常
		EE_M2C_ESCORT_PKS_TEAM_OUTTIME = 1003,					/// 队伍已经过期
		EE_M2C_ESCORT_PKS_TEAM_NOT_FIND = 1004,					/// 队伍未找到
		EE_M2C_ESCORT_PKS_TEAM_BOT_NOT_FIND = 1005,				/// 机器人队伍未找到
		EE_M2C_ESCORT_PKS_TEAM_ID = 1006,						/// 队伍id错误
		EE_M2C_ESCORT_PKS_TEAM_HAD_SET = 1007,					/// 护送队伍已经被设置
		EE_M2C_ESCORT_PKS_PET_USED = 1008,						///卡牌已经被设置在其他护送阵容中
		EE_M2C_ESCORT_PKS_FIGHT_CAN_NOT_SET = 1009,				///被攻击期间无法设置队伍
		EE_M2C_ESCORT_PKS_LEAD_INFO = 1010,						///护送阵容队长信息错误
		EE_M2C_ESCORT_PKS_NOT_LEAD = 1011,						///护送阵容必须有队长
		EE_M2C_ESCORT_PKS_NOT_BEAST = 1012,						///护送阵容必须有魂兽
		EE_M2C_ESCORT_PKS_LOC_ERROR = 1013,						///护送队伍位置错误
		EE_M2C_ESCORT_PKS_GROUP_OUTTIME = 1014,					///护送商队已经过期
		EE_M2C_ESCORT_PKS_GROUP_ID = 1015,						///护送商队id错误
		EE_M2C_ESCORT_PKS_BUSY = 1016,							///护送服务器繁忙
		EE_M2C_ESCORT_PKS_GROUP_HAD_FAIL = 1017,				///商队已经被打败了
		EE_M2C_ESCORT_PKS_GROUP_BOT_NOT_FIND = 1018,			///机器人商队未找到
		EE_M2C_ESCORT_PKS_FIGHT_TIME_OUT = 1019,				///护送战斗超时
		EE_M2C_ESCORT_PKS_GET_REWARD = 1020,					///奖励已经领取过了
		EE_M2C_ESCORT_PKS_TEAM_NOT_IN_GROUP = 1021,				///队伍不在该商队中
		EE_M2C_ESCORT_PKS_GROUP_NOT_FIAL = 1022,				///该商队未被击败
		EE_M2C_ESCORT_GROUP_NOT_OVER = 1023,					///护送未结束
		EE_M2C_ESCORT_GROUP_IS_FIGHTING = 1024,					///商店正在被进攻，请等待，或换一个商队

	}
}