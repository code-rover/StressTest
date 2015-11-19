using System;
using System.Runtime.InteropServices;
using ExFormatter;

namespace net.unity3d
{
    ////Ð­Òé±àºÅ
    public enum E_OPCODE
    {
        ////login server 
        EP_LS2C_ON_LOGIN_SERVER_CONN = 101,
        ////realm server 
        EP_RM2C_ON_REALM_SERVER_CONN,
        ////guest server 
        EP_RM2G_ON_GUEST_SERVER_CONN,
        /// login server close
        EP_LS2C_ON_LOGIN_SERVER_CLOSE,
        /// realm server close
        EP_RM2C_ON_REALM_SERVER_CLOSE,
        /// wet error/close
        EP_RM2C_ON_WEB_CLOSE,
        /// account server conn
        EP_RM2C_ON_ACCOUNT_SERVER_CONN,
        /// realm server close
        EP_RM2C_ON_ACCOUNT_SERVER_CLOSE,


        ///客户端登陆

        EP_C2LS_LOGIN_CLOSE_REPLY = 4500,           ///客户端收到应答，通知服务器关闭login连接
        EP_C2LS_LOGIN_REQUEST = 5000,
        ///返回登陆结果信息                                                        
        EP_LS2C_LOGIN_RESPONSE = 6000,
        ///服务器状态
        EP_LS2C_SERVER_STATE = 6001,



        //////////client to realm server////////////////////////////
        ///请求登陆
        EP_C2RM_LOGIN = 200,
        ///获取背包宠物信息
        EP_C2RM_PET_INFO_BAG = 201,
        ///获取酒馆宠物信息
        EP_C2RM_PET_INFO_BAR = 202,
        ///获取队伍信息
        EP_C2RM_TEAM_INFO = 203,
        ///获取装备材料组
        EP_C2RM_EQUIP_GROUP = 204,
        ///获取材料组
        EP_C2RM_MATERIAL_GROUP = 205,
        ///获取装备
        EP_C2RM_EQUIPMENT = 206,
        ///获取副本信息
        EP_C2RM_FB = 207,
        ///获取好友信息
        EP_C2RM_FRIEND = 208,
        ///创建角色
        EP_C2RM_CREAT_ROLE = 209,
        ///请求推荐好友
        EP_C2RM_FRIEND_RECOMMEND = 210,
        ///请求跟这个人一起打副本
        EP_C2RM_FRIEND_FIGHT_SELECT = 211,
        ///进入副本
        EP_C2RM_GOTO_FB = 212,
        ///卡牌吞噬升级
        EP_C2RM_PET_EAT = 213,
        ///离开副本
        EP_C2RM_LEAVE_FB = 214,
        ///开始副本战斗
        EP_C2RM_FB_PK_BEGIN = 215,
        ///副本战斗验证数据开始
        EP_C2RM_CHECK_FB_PK_BEGIN = 216,
        ///副本战斗验证数据
        EP_C2RM_CHECK_FB_PK_MID = 217,
        ///副本战斗验证数据结束
        EP_C2RM_CHECK_FB_PK_OVER = 218,
        ///卡牌进化
        EP_C2RM_PET_UP = 219,
        ///获取世界活动时间信息
        EP_C2RM_WORLD_EVENT_TIME = 220,
        ///技能升级
        EP_C2RM_SKILL_UP = 221,
        ///获取世界boss信息
        EP_C2RM_WROLD_BOSS_INFO = 222,
        ///长体力
        EP_C2RM_POWER_ADD = 223,
        ///世界boss鼓舞
        EP_C2RM_EXCITE_WORLD_BOSS = 224,
        ///碎片列表
        EP_C2RM_PIECE = 225,
        ///进入世界boss
        EP_C2RM_GOTO_WORLD_BOSS = 226,
        ///把卡牌从背包放到仓库
        EP_C2RM_PET_BAG_TO_BAR = 227,
        ///世界boss战斗验证数据开始
        EP_C2RM_CHECK_WROLD_BOSS_PK_BEGIN = 228,
        ///把卡牌从仓库放到背包
        EP_C2RM_PET_BAR_TO_BAG = 229,
        ///世界boss战斗验证数据
        EP_C2RM_CHECK_WROLD_BOSS_PK_MIN = 230,
        ///穿装备
        EP_C2RM_EQUIP_ON = 231,
        ///世界boss战斗验证数据结束
        EP_C2RM_CHECK_WROLD_BOSS_PK_OVER = 232,
        ///脱装备
        EP_C2RM_EQUIP_OFF = 233,
        ///世界boss战斗复活
        EP_C2RM_WROLD_BOSS_PK_REVIVE = 234,
        ///设置队伍信息
        EP_C2RM_TEAM_SET = 235,
        ///离开世界boss
        EP_C2RM_LEAVE_WROLD_BOSS = 236,
        ///同步脱穿装备
        EP_C2RM_EQUIP_PET_NOTIFY = 237,
        ///请求世界boss奖励
        EP_C2RM_WORLD_BOSS_REWARD = 238,
        ///碎片合成
        EP_C2RM_PIECE_TO_PET = 239,
        ///竞技场信息
        EP_C2RM_PK_INFO = 240,
        ///优先使用万能碎片
        EP_C2RM_PIECE_FIRST = 241,
        ///竞技场排行榜
        EP_C2RM_PK_RANK_LIST = 242,
        ///卡牌分解
        EP_C2RM_PIECE_FROM_PET = 243,
        ///增加PK次数
        EP_C2RM_ADD_PK_CNT = 244,
        ///卖掉卡牌
        EP_C2RM_PET_SELL = 245,
        ///升级爵位
        EP_C2RM_UP_LV_NOBILITY = 246,
        ///设置手动释放的技能
        EP_C2RM_PET_SET_SKILL = 247,
        ///获取爵位奖励
        EP_C2RM_GET_REWARD_NOBILITY = 248,
        ///设置使用中的队伍
        EP_C2RM_TEAM_WORK_SET = 249,
        ///请求竞技场匹配
        EP_C2RM_MATE_PK = 250,
        ///装备出售
        EP_C2RM_EQUIP_SELL = 251,
        ///竞技场战斗验证数据开始
        EP_C2RM_CHECK_PK_BEGIN = 252,
        ///竞技场战斗验证数据
        EP_C2RM_CHECK_PK_MID = 254,
        ///设置副本标识
        EP_C2RM_FB_SET_FLAG = 255,
        ///竞技场战斗验证数据结束
        EP_C2RM_CHECK_PK_OVER = 256,
        ///扫荡副本
        EP_C2RM_FB_SWEEP = 257,
        ///更新竞技场阵容
        EP_C2RM_UPDATE_PK_TEAM = 258,
        ///请求大陆信息
        EP_C2RM_LAND_LIST = 259,
        ///打开邮件
        EP_C2RM_OPEN_EMAIL = 260,
        ///请求副本星奖励
        EP_C2RM_LAND_STAR_REWARD = 261,
        ///摧毁邮件
        EP_C2RM_DESTROY_EMAIL = 262,
        ///设置卡牌保护
        EP_C2RM_PET_PROTECT = 263,
        ///获取邮件信息
        EP_C2RM_EMAIL_LIST = 264,
        ///申请加好友
        EP_C2RM_FRIEND_ASK = 265,
        ///下一个副本	
        EP_C2RM_NEXT_FB = 266,
        ///应答好友申请
        EP_C2RM_FRIEND_ASK_ANSWER = 267,
        ///装备合成
        EP_C2RM_EQUIP_COM = 268,
        ///删除好友
        EP_C2RM_FRIEND_DELETE = 269,
        ///装备强化
        EP_C2RM_EQUIP_UP = 270,
        ///设置别名
        EP_C2RM_FRIEND_NICK_NAME = 271,
        ///一键强化
        EP_C2RM_EQUIP_UP_ONE_KEY = 272,
        ///查找好友
        EP_C2RM_FRIEND_SEARCH = 273,
        ///获取爬塔信息
        EP_C2RM_GET_TOWER_INFO = 274,
        ///购买好友上限
        EP_C2RM_FRIEND_BUY = 275,
        ///爬塔战斗验证数据开始
        EP_C2RM_CHECK_TOWER_PK_BEGIN = 276,
        ///赠送体力
        EP_C2RM_FRIEND_POWER_SEND = 277,
        ///爬塔战斗验证数据中间
        EP_C2RM_CHECK_TOWER_PK_MID = 278,
        ///赠送体力列表
        EP_C2RM_FRIEND_POWER = 279,
        ///爬塔战斗验证数据结束
        EP_C2RM_CHECK_TOWER_PK_OVER = 280,
        ///领取体力
        EP_C2RM_FRIEND_POWER_GET = 281,
        ///爬塔扫荡
        EP_C2RM_TOWER_SWEEP = 282,
        ///好友切磋
        EP_C2RM_FRIEND_PK = 283,
        ///获取通塔奖励
        EP_C2RM_TOWER_REWARD = 284,
        ///校验竞技场队伍
        EP_C2RM_PK_CHECK_TEAM = 285,
        ///获取通塔每日奖励
        EP_C2RM_TOWER_DAY_REWARD = 286,
        ///复仇
        EP_C2RM_FRIEND_PK_BACK = 287,
        ///爬塔重新开始
        EP_C2RM_TOWER_AGAIN = 288,
        ///好友被切磋、被复仇列表
        EP_C2RM_FRIEND_BE_PK_LIST = 289,
        ///刷新爬塔商店
        EP_C2RM_REFRESH_TOWER_SHOP = 290,
        ///申请好友列表
        EP_C2RM_FRIEND_ASK_LIST = 291,
        ///购买爬塔商店物品
        EP_C2RM_BUY_TOWER_SHOP = 292,
        ///起名字
        EP_C2RM_ROLE_NAME = 293,
        ///爬塔时间
        EP_C2RM_GET_TOWER_TIME = 294,
        ///设置个人签名
        EP_C2RM_ROLE_PSIGN = 295,
        ///竞技商店购买物品
        EP_C2RM_PK_SHOP_BUY = 296,
        ///好友切磋开始
        EP_C2RM_FRIEND_PK_BEGIN = 297,
        ///获得竞技场商店
        EP_C2RM_GET_PK_SHOP = 298,
        ///好友切磋过程
        EP_C2RM_FRIEND_PK_MID = 299,
        ///刷新竞技场商店
        EP_C2RM_REFRESH_PK_SHOP = 300,
        ///好友切磋完成
        EP_C2RM_FRIEND_PK_OVER = 301,
        ///获取活动时间
        EP_C2RM_ACTIV_EVENT_TIME = 302,
        ///购买背包上限
        EP_C2RM_BAG_BUY = 303,
        ///获取活动信息
        EP_C2RM_ACTIV_EVENT_INFO = 304,
        ///绑定账号
        EP_C2RM_BIND = 305,
        ///活动开始
        EP_C2RM_ACTIV_EVENT_BEGIN = 306,
        ///请求好友不用实时刷新的信息
        EP_C2RM_FRIEND_NOFRESH_INFO = 307,
        ///活动战斗验证数据开始
        EP_C2RM_CHECK_ACTIV_EVENT_PK_BEGIN = 308,
        ///好友实时数据
        EP_C2RM_FRIEND_FRESH_INFO = 309,
        ///活动战斗验证数据中间
        EP_C2RM_CHECK_ACTIV_EVENT_MID = 310,
        ///购买体力
        EP_C2RM_POWER_BUY = 311,
        ///活动战斗验证数据结束
        EP_C2RM_CHECK_ACTIV_EVENT_OVER = 312,
        ///点金手
        EP_C2RM_STONE = 313,
        ///增加活动次数
        EP_C2RM_ADD_ACTIV_EVENT_CNT = 314,
        ///新手引导字段
        EP_C2RM_NEW_GUIDE = 315,
        ///进入活动
        EE_C2RM_GO_TO_ACTIV_EVENT = 316,
        ///随机名字
        EP_C2RM_NAME_RAND = 317,
        ///离开活动
        EE_C2RM_LEAVE_ACTIV_EVENT = 318,
        ///商城抽奖
        EP_C2RM_LUCKY_SHOP = 319,
        ///副本重新挑战
        EP_C2RM_RE_PK_FB = 320,
        ///充值
        EP_C2RM_CHARGE = 321,
        ///客户端ping包
        EP_C2RM_PING_PRO = 322,
        ///充值记录
        EP_C2RM_CHARGE_LIST = 323,
        ///活动重新战斗
        EP_C2RM_RE_ACTIV_EVENT = 324,
        ///订单记录
        EP_C2RM_ORDER_LIST = 325,
        ///正义徽章兑换
        EP_C2RM_BADGE_SHOP_BUY = 326,
        ///测试充值
        EP_C2RM_CHARGE_TEST = 327,
        ///获取手机信息回复
        EP_C2RM_GET_PHONE_INFO = 328,
        ///领取月卡之类的充值
        EP_C2RM_CHARGE_CARD = 329,
        ///进阶石镶嵌
        EP_C2RM_STONE_INLAY = 330,
        /// 保存非强制引导
        EP_C2RM_FIRST_GUIDE = 331,
        ///宠物进阶石进化
        EP_C2RM_PET_STONE_UP = 332,
        ///副本重置
        EP_C2RM_FB_RESET = 333,
        ///登录界面加载状态
        EP_C2RM_LOAD_STATUS = 334,
        ///停服通知
        EP_C2RM_SERVER_STOP_NOTE = 335,
        ///网络邮件文字
        EP_C2RM_WEB_EMAIL_TEXT = 336,
        ///DDN引导字段
        EP_C2RM_GUIDE_DDN = 337,
        ///闪退通知
        EP_C2RM_SUDDEN_OUT = 338,
        ///管理员发奖励
        EP_C2RM_ADMIN_REWARD = 339,
        ///海山商店购买物品
        EP_C2RM_MOUNTAIN_SHOP_BUY = 340,
        ///酒足饭饱
        EP_C2RM_POWER_MEAL = 341,
        ///获得海山商店
        EP_C2RM_GET_MOUNTAIN_SHOP = 342,
        ///酒足饭饱时间
        EP_C2RM_POWER_MEAL_TIME = 343,
        ///刷新海山商店
        EP_C2RM_REFRESH_MOUNTAIN_SHOP = 344,
        ///签到
        EP_C2RM_SIGN = 345,
        ///获得金币商店信息
        EP_C2RM_GET_SMONEY_SHOP = 346,
        ///补签
        EP_C2RM_SING_RE = 347,
        ///刷新金币商店
        EP_C2RM_REFRESH_SMONEY_SHOP = 348,
        ///任务信息
        EP_C2RM_TASK = 349,
        ///金币商店购买物品
        EP_C2RM_SMONEY_SHOP_BUY = 350,
        ///领取新手任务奖励
        EP_C2RM_TASK_GUIDE_REWARD = 351,
        ///客户端2号ping包
        EP_C2RM_PING_PRO_TWO = 352,
        ///主线任务领取奖励
        EP_C2RM_TASK_FB_REWARD = 353,
        ///零点刷新信息
        EP_C2RM_MIN_NIGHT_REFRESH = 354,
        ///每次任务领取奖励
        EP_C2RM_TASK_EVERYDAY_REWARD = 355,
        ///魂兽信息
        EP_C2RM_BEAST_INFO = 356,
        ///等级任务奖励
        EP_C2RM_TASK_LV = 357,
        ///魂兽吃经验药
        EP_C2RM_BEAST_LV_UP = 358,
        ///卡牌升级
        EP_C2RM_PET_LV_UP = 359,
        ///魂兽卷合成魂兽
        EP_C2RM_BEAST_COM = 360,
        ///卡牌升星
        EP_C2RM_PET_STAR_UP = 361,
        ///魂兽穿装备
        EP_C2RM_BEAST_EQUIP_WEAR = 362,
        ///新的技能升级
        EP_C2RM_SKILL_UP_NEW = 363,
        ///魂兽装备升级
        EP_C2RM_BEAST_EQUIP_UP = 364,
        ///技能点回复
        EP_C2RM_SKILL_POINT_ADD = 365,
        ///获取魂兽商店（星际商人）信息
        EP_C2RM_SOUL_SHOP = 366,
        ///购买技能点
        EP_C2RM_SKILL_POINT_BUY = 367,
        ///魂兽商店（星际商人）购买
        EP_C2RM_SOUL_SHOP_BUY = 368,
        ///卡牌合成
        EP_C2RM_PET_PIECE_TO_PET = 369,
        ///魂兽合成（兑换）
        EP_C2RM_SOUL_COM = 370,
        ///魂侠抽奖
        EP_C2RM_LUCKY_SOUL = 371,
        ///太阳井信息
        EP_C2RM_SUN_WELL_INFO = 372,
        ///出售卡牌碎片
        EP_C2RM_PET_PIECE_SELL = 373,
        ///太阳井重置
        EP_C2RM_SUN_WELL_RESET = 374,
        ///好友出战历史记录
        EP_C2RM_FRIEND_FIGHT_HISTORY = 375,
        ///开礼盒
        EP_C2RM_OPEN_GIFT_BOX = 376,
        ///佣兵
        EP_C2RM_FRIEND_HELPER = 377,
        ///海山魂兽
        EP_C2RM_MOT_BEAST = 378,
        ///请求网络邮件列表
        EP_C2RM_WEB_EMAIL = 379,
        ///开启网络邮件
        EP_C2RM_WEB_EMAIL_OPEN = 381,
        ///进入boss
        EP_C2RM_BOSS_GOTO = 383,
        ///英雄试炼boss信息
        EP_C2RM_BOSS_INFO = 385,
        ///英雄试炼boss重置
        EP_C2RM_BOSS_RESET = 386,
        ///英雄试炼boss过期
        EP_C2RM_BOSS_TIME_OUT = 387,
        ///进入世界boss
        EP_C2RM_GOTO_BOSS = 389,
        ///开始战斗
        EP_C2RM_BOSS_PK_BEGIN = 391,
        ///英雄试炼战斗过程
        EP_C2RM_BOSS_PK_MIN = 393,
        ///英雄试炼战斗结束
        EP_C2RM_BOSS_PK_OVER = 395,
        ///远征信息
        EP_C2RM_MOT_INFO = 397,
        ///远征队伍
        EP_C2RM_MOT_TEAM = 399,
        ///远征卡牌信息
        EP_C2RM_MOT_PET = 401,
        ///远征重置
        EP_C2RM_MOT_RESET = 403,
        ///海山巫医商店购买
        EP_C2RM_MOT_MAGIC_SHOP_BUY = 404,
        ///远征战斗开始
        EP_C2RM_MOT_PK_BEGIN = 405,
        ///远征战斗过程
        EP_C2RM_MOT_PK_MIN = 407,
        ///远征战斗结束
        EP_C2RM_MOT_PK_OVER = 409,
        ///远征宝箱奖励
        EP_C2RM_MOT_BOX = 411,
        ///远征买buffer
        EP_C2RM_MOT_BUY_BUFFER = 413,
        ///进入远征
        EP_C2RM_MOT_GOTO = 415,
        ///卡牌选择佣兵
        EP_C2RM_MOT_HELPER = 417,
        ///登陆请求佣兵信息
        EP_C2RM_MOT_HELPER_INFO = 419,
        ///离开远征
        EP_C2RM_MOT_LEAVE = 421,
        ///尹晓航使用的引导字符串
        EP_C2RM_GUIDE_STR_YXH = 423,
        //发送聊天信息
        EP_C2RM_CHAT = 425,
        //请求最近的N条聊天内容
        EP_C2RM_CHAT_RECENT_REQUEST = 427,
        // 护送 begin
        ///玩家护送信息
        EP_C2RM_ESCORT_INFO = 429,
        ///获取自己护送的商队列表
        EP_C2RM_ESCORT_GROUP_LIST = 431,
        ///获取护送防守商队的信息（动长）
        EP_C2RM_ESCORT_GET_GROUP = 433,
        ///获取一个护送商队的队伍信息
        EP_C2RM_ESCORT_GET_TEAM = 435,
        ///正在进攻护送商队的己方卡牌信息
        EP_C2RM_ESCORT_BEAT_SELF_PET = 437,
        ///正在进攻护送商队的己方魂兽信息
        EP_C2RM_ESCORT_BEAT_SELF_BEAST = 439,
        ///正在进攻的护送商队信息
        EP_C2RM_ESCORT_BEAT_GROUP = 441,
        ///获取正在进攻的护送队伍的信息
        EP_C2RM_ESCORT_BEAT_TEAM = 443,
        ///搜索护送商队
        EP_C2RM_ESCORT_FIND_GROUP = 445,
        ///改变己方护送商队队伍阵法
        EP_C2RM_ESCORT_SET_SELF_TEAM = 447,
        ///进入护送守护
        EP_C2RM_ESCORT_GO_TO = 449,
        ///离开护送守护
        EP_C2RM_ESCORT_LEAVE = 451,
        ///护送选择基友
        EP_C2RM_ESCORT_HELPER = 453,
        ///护送基友信息
        EP_C2RM_ESCORT_HELPER_INFO = 455,
        ///攻打护送商队
        EP_C2RM_ESCORT_BEAT_GROUP_ACT = 457,
        ///护送战斗开始
        EP_C2RM_ESCORT_PK_BEGIN = 459,
        ///护送战斗中间
        EP_C2RM_ESCORT_PK_MIN = 461,
        ///护送战斗结束
        EP_C2RM_ESCORT_PK_OVER = 463,
        ///护送面包定时增加请求
        EP_C2RM_ESCORT_ADD_BREAD = 465,
        ///护送购买面包
        EP_C2RM_ESCORT_BUY_BREAD = 467,
        ///护送防守记录获取面包
        EP_C2RM_ESCORT_LOG_GET_BREAD = 469,
        ///确认己方商队被打败
        EP_C2RM_ESCORT_MAKE_SURE_FAIL = 471,
        ///获取防守记录
        EP_C2RM_ESCORT_LOG = 473,
        ///弃矿
        EP_C2RM_ESCORT_GIVE_UP = 475,
        ///护送成功，获取奖励
        EP_C2RM_ESCORT_SUCCEED = 479,
        ///正在进攻商队超时
        EP_C2RM_ESCORT_BEAT_GROUP_TIME_OUT = 480,
        ///豪华签到
        EP_C2RM_SIGN_VIP = 482,
        ///重置活动
        EP_C2RM_RESET_A_EVENT = 483,
        ///跑马灯信息所有
        EP_C2RM_MESSAGE_SCREEN = 484,
        ///礼包码领奖
        EP_C2RM_GIFT_KEY_REWARD = 485,
        ///后台暴率活动信息
        EP_C2RM_BGND_DROP = 486,
        // 护送end
        // 海山奖励关begin
        EP_C2RM_MY_MOT_PK_INFO = 1000,				// 请求我的海山奖励关信息
        EP_C2RM_MOT_PK_ENEMY_INFO = 1001,			// 请求海山奖励关NPC信息
        EP_C2RM_MOT_PK_RANK_LIST = 1002,			// 海山奖励关排行榜
        EP_C2RM_JOIN_MOT_PK = 1003,					// 报名海山奖励关
        EP_C2RM_MOT_PK_HELPER = 1004,				// 海山奖励关选择佣兵
        EP_C2RM_MOT_PK_BOX = 1005,					// 海山奖励关宝箱奖励
        EP_C2RM_MOT_PK_GOTO = 1006,					///海山奖励关进入
        EP_C2RM_MOT_PK_PK_BEGIN = 1007,				///海山奖励关战斗开始
        EP_C2RM_MOT_PK_PK_MIN = 1008,				///海山奖励关战斗过程
        EP_C2RM_MOT_PK_PK_OVER = 1009,				///海山奖励关战斗结束
        EP_C2RM_MOT_PK_LEAVE = 1010,				// 离开海山奖励关
        EP_C2RM_MOT_PK_REWARD_PKED = 1011,			///海山奖励关领取防守次数奖励
        EP_C2RM_CHANGE_MOT_PK_BUFFER = 1012,		///海山奖励关修改防守Buffer
        /// 海山奖励关end

        ///签到送魂兽
        EP_C2RM_SIGN_BEAST_REQUEST = 1050,			///签到送魂兽请求

        EP_C2RM_GET_ACTIVITY_INFO = 1060,			///获取活动信息
        EP_C2RM_ACTIVITY_REWARD = 1061,				///活动领奖

        EP_C2RM_GET_FIRST_DAY_BUY_POWER_INFO = 1065,   ///获取首日购买活动信息(特殊的活动)
        EP_C2RM_GET_FIRST_DAY_BUY_POWER_REWARD = 1066, ///首日购买活动领奖(特殊的活动领奖)

        EP_C2RM_BUY_FUND = 1067,					///购买基金
        EP_C2RM_GET_ACT_RANK = 1068,				///请求活动排行榜信息
        EP_C2RM_GET_FIRST_RECHARGE_ACT_REWARD = 1069,	///首充活动领奖

        //公会相关协议
        EP_C2RM_GET_GAME_UNION_INFO = 1080,			///请求公会信息
        EP_C2RM_GET_GAME_UNION_LIST = 1081,				///请求公会列表
        EP_C2RM_CREATE_GAME_UNION = 1082,			///创建公会
        EP_C2RM_SET_GAME_UNION = 1083,				///设置公会
        EP_C2RM_SEARCH_GAME_UNION = 1084,			///搜索公会
        EP_C2RM_REQUEST_JOIN_GAME_UNION = 1085,		///申请加入公会
        EP_C2RM_REQUEST_JOINER_LIST = 1086,			///请求申请列表
        EP_C2RM_APPOINT_JOB = 1087,					///委任职务
        EP_C2RM_EMAIL_ALL_MEMBER = 1088,			///全员邮件
        EP_C2RM_DISBAND_GAME_UNION = 1089,			///解散公会
        EP_C2RM_GET_GAME_UNION_LOG = 1090,			///请求公会日志
        EP_C2RM_AGREEN_JOIN = 1091,				///审批申请列表
        EP_C2RM_OPEN_SET_GAME_UNION = 1092,			///打开公会管理界面
        EP_C2RM_SET_GU_ANNOUNCEMENT = 1093,			///修改公会公告
        EP_C2RM_GU_EMAIL_TEXT = 1094,				///请求公会邮件正文
        EP_C2RM_PRESENT_GU_MONTH_CARD = 1095,		///赠送公会月卡
        EP_C2RM_ROLE_UNION_REQUEST = 1096,  /// get role union name by role id
        EP_C2RM_GU_DISPATCH_PET = 1097,				///派驻公会佣兵
        EP_C2RM_GU_WORSHIP = 1098,					///公会膜拜
        EP_C2RM_OPEN_WORSHIP_UI = 1099,				///打开膜拜大神界面
        EP_C2RM_WORSHIP_REWARD = 1100,				///膜拜领奖
        EP_C2RM_GU_ALL_HELPERS = 1101,				///请求佣兵营地所有佣兵
        EP_C2RM_ESCORT_GU_HELPER = 1102,			///选择护送雇佣兵
        EP_C2RM_MOT_GU_HELPER = 1103,				///选择海山佣兵
        EP_C2RM_MOT_PK_GU_HELPER = 1104,			///选择海山防守关佣兵
        EP_C2RM_GU_FRIEND_FIGHT_SELECT = 1105,		///副本选择佣兵
        ///请求竞技场排行榜条目详细信息
        EP_C2RM_GET_PK_RANK_DETAIL = 1106,

        EP_C2RM_GET_RANK_LIST = 1150,				///请求排行榜列表
        EP_C2RM_GET_GU_ACTIVE_RANK_DETAIL = 1151,	///请求公会三日活跃度排行榜条目详细信息
        EP_C2RM_GET_ALL_PET_FIGHT_RANK_DETAIL = 1152,///请求全部卡牌排行榜条目详细信息
        EP_C2RM_GET_LEVEL_RANK_DETAIL = 1153,		///请求等级排行榜条目详细信息

        EP_C2RM_ROLE_RENAME = 1500,				    ///修改名字

        EP_C2RM_GET_NOBILITY_SHOP = 1501,		    ///获取爵位商店
        EP_C2RM_NOBILITY_SHOP_BUY = 1502,           ///购买爵位商店
        EP_C2RM_REFRESH_NOBILITY_SHOP = 1503,       ///爵位商店刷新

        EP_C2RM_LUCKY_SOUL_LIST = 1505,             ///魂侠ui请求

        EP_C2RM_USE_PROP_ADD_SP = 1506,             ///使用道具增加技能点
        EP_C2RM_USE_TEMP_VIP = 1508,               ///使用vip体验卡


        /////////realm to client//////////////////////////////////

        EP_RM2C_USE_TEMP_VIP = 2705,               ///使用vip体验卡回应
        EP_RM2C_USE_PROP_ADD_SP = 2704,             ///使用道具增加技能点回应
        //公会相关协议	
        EP_RM2C_GU_HELPER_HISTORY = 2608,			///今日战斗过的佣兵所属的角色列表
        EP_RM2C_GU_FRIEND_FIGHT_SELECT = 2607,		///副本选择佣兵返回
        EP_RM2C_MOT_PK_GU_HELPER = 2606,			///选择海山防守关佣兵返回
        EP_RM2C_MOT_GU_HELPER = 2605,				///选择海山佣兵返回
        EP_RM2C_ESCORT_GU_HELPER = 2604,			///选择护送佣兵返回
        EP_RM2C_GU_ALL_HELPERS = 2603,				///请求佣兵营地所有佣兵
        EP_RM2C_WORSHIP_REWARD = 2602,				///膜拜领奖返回
        EP_RM2C_OPEN_WORSHIP_UI = 2601,				///打开膜拜大神界面
        EP_RM2C_GU_WORSHIP = 2600,				///公会膜拜返回
        EP_RM2C_GU_DISPATCH_PET = 2599,			///派驻公会佣兵
        EP_RM2C_ROLE_UNION_RESPONSE = 2598, ///  get role union name by role id
        EP_RM2C_PRESENT_GU_MONTH_CARD = 2597,		///赠送公会月卡
        EP_RM2C_GU_EMAIL_TEXT = 2596,			///请求公会邮件正文
        EP_RM2C_SET_GU_ANNOUNCEMENT = 2595,	///修改公会公告
        EP_RM2C_OPEN_SET_GAME_UNION = 2594,	///打开公会管理界面
        EP_RM2C_AGREEN_JOIN = 2593,			///审批申请列表
        EP_RM2C_NOTIFY_LEAD_GU_INFO = 2592,	///同步主角公会信息
        EP_RM2C_GET_GAME_UNION_LOG = 2591,		///请求公会日志
        EP_RM2C_DISBAND_GAME_UNION = 2590,		///解散公会
        EP_RM2C_EMAIL_ALL_MEMBER = 2589,		///全员邮件
        EP_RM2C_APPOINT_JOB = 2588,				///委任职务
        EP_RM2C_REQUEST_JOINER_LIST = 2587,			///申请列表返回
        EP_RM2C_REQUEST_JOIN_GAME_UNION = 2586,			///申请加入公会返回
        EP_RM2C_SEARCH_GAME_UNION = 2585,			///搜索公会返回
        EP_RM2C_SET_GAME_UNION = 2584,			///设置公会返回
        EP_RM2C_CREATE_GAME_UNION = 2583,		///创建公会返回
        EP_RM2C_GU_MEMBER_LIST = 2582,			///公会成员列表
        EP_RM2C_GAME_UNION_LIST = 2581,		///公会列表
        EP_RM2C_GET_GAME_UNION_INFO = 2580,		///请求公会信息返回

        EP_RM2C_FIRST_RECHARGE_ACT_REWARD = 2569,	///首充活动领奖返回
        EP_RM2C_GET_ACT_RANK = 2568,				///请求活动排行榜信息返回
        EP_RM2C_BUY_FUND = 2567,					///购买基金返回

        EP_RM2C_FIRST_DAY_BUY_POWER_ACT_REWARD = 2566,	//首日购买体力活动领奖(特殊的活动领奖)
        EP_RM2C_FIRST_DAY_BUY_POWER_ACT_INFO = 2565,	//首日购买体力活动信息(特殊的活动)

        EP_RM2C_GET_ACTIVITY_REWARD_INFO = 2562,	///活动奖励信息返回
        EP_RM2C_GET_ACTIVITY_REWARD = 2561,			///活动领奖返回
        EP_RM2C_GET_ACTIVITY_INFO = 2560,			///活动信息返回
        ///重置活动
        EP_RM2C_RESET_A_EVENT = 2552,
        ///豪华签到返回
        EP_RM2C_SIGN_VIP = 2551,
        /// //签到送魂兽
        EP_RM2C_SIGN_BEAST_REQUEST = 2550,			//签到送魂兽请求返回

        EP_RM2C_GET_LEVEL_RANK_DETAIL = 2523,		///请求等级排行榜条目详细信息返回
        EP_RM2C_GET_ALL_PET_FIGHT_RANK_DETAIL = 2522,///请求全部卡牌战斗力排行榜条目详细信息返回
        EP_RM2C_GET_GU_ACTIVE_RANK_DETAIL = 2521,	///请求公会三日活跃度排行榜条目详细信息返回
        EP_RM2C_GET_RANK_LIST = 2520,				///请求排行榜列表返回

        // 海山奖励关begin	
        EP_RM2C_CHANGE_MOT_PK_BUFFER = 2511,		///海山奖励关修改防守Buffer返回
        EP_RM2C_MOT_PK_REWARD_PKED = 2510,			///海山奖励关领取防守次数奖励返回
        EP_RM2C_MOT_PK_LEAVE = 2509,				// 离开海山奖励关
        EP_RM2C_MOT_PK_PK_ERROR = 2508,				///海山奖励关战斗错误
        EP_RM2C_MOT_PK_REWARD = 2507,				///海山奖励关奖励
        EP_RM2C_MOT_PK_GOTO = 2506,					///返回进入海山奖励关
        EP_RM2C_MOT_PK_BOX = 2505,					// 海山奖励关宝箱奖励返回
        EP_RM2C_MOT_PK_HELPER = 2504,				// 海山奖励关选择佣兵返回
        EP_RM2C_JOIN_MOT_PK = 2503,					// 报名海山奖励关返回
        EP_RM2C_MOT_PK_RANK_LIST = 2502,			// 海山奖励关排行榜回复
        EP_RM2C_MOT_PK_ENEMY = 2501,				///海山奖励关NPC信息
        EP_RM2C_MY_MOT_PK_INFO = 2500,				// 返回我的海山奖励关信息	
        // 海山奖励关end

        ///竞技场排行榜条目详细信息回复
        EP_RM2C_GET_PK_RANK_DETAIL = 2358,
        ///设置一个后台暴率活动
        EP_RM2C_BGND_DROP_SET = 2357,
        ///后台暴率活动信息
        EP_RM2C_BGND_DROP = 2356,
        ///礼包码领奖
        EP_RM2C_GIFT_KEY_REWARD = 2355,
        ///跑马灯信息所有
        EP_RM2C_MESSAGE_SCREEN = 2354,
        ///设置跑马灯信息
        EP_RM2C_MESSAGE_SCREEN_SET = 2353,
        ///删除跑马灯信息
        EP_RM2C_MESSAGE_SCREEN_DELETE = 2352,
        ///通知客户端服务器id
        EP_RM2C_SEND_SERVER_ID = 2351,

        ///封停begin			
        ///玩家封停信息
        EP_RM2C_STOP_INFO = 2350,
        ///封停end

        // 护送 begin
        ///正在进攻商队超时回复
        EP_RM2C_ESCORT_BEAT_GROUP_TIME_OUT = 2314,
        ///护送成功，获取奖励回复
        EP_RM2C_ESCORT_SUCCEED = 2313,
        ///创建护送log
        EP_RM2C_ESCORT_CREAT_LOG = 2309,
        ///弃矿回复
        EP_RM2C_ESCORT_GIVE_UP = 2307,
        ///防守记录回复
        EP_RM2C_ESCORT_LOG = 2305,
        ///确认己方商队被打败回复
        EP_RM2C_ESCORT_MAKE_SURE_FAIL = 2303,
        ///护送防守记录获取面包回复
        EP_RM2C_ESCORT_LOG_GET_BREAD = 2301,
        ///护送购买面包回复
        EP_RM2C_ESCORT_BUY_BREAD = 2299,
        ///护送面包定时增加请求回复
        EP_RM2C_ESCORT_ADD_BREAD = 2297,
        ///护送商队被打结果通知
        EP_RM2C_ESCORT_PK_GROUP = 2295,
        ///护送守护战斗回复
        EP_RM2C_ESCORT_PK_OVER = 2293,
        ///攻打护送商队结果回复
        EP_RM2C_ESCORT_BEAT_GROUP_ACT = 2291,
        ///护送守护基友信息回复
        EP_RM2C_ESCORT_HELPER_INFO = 2289,
        ///护送守护选择基友回复
        EP_RM2C_ESCORT_HELPER = 2287,
        ///离开护送守护回复
        EP_RM2C_ESCORT_LEAVE = 2285,
        ///进入护送守护回复
        EP_RM2C_ESCORT_GO_TO = 2283,
        ///改变己方护送商队队伍阵
        EP_RM2C_ESCORT_SET_SELF_TEAM = 2281,
        ///搜索护送商队回复
        EP_RM2C_ESCORT_FIND_GROUP = 2279,
        ///获取正在进攻护送队伍的信息
        EP_RM2C_ESCORT_BEAT_TEAM = 2277,
        ///正在进攻的护送商队信息回复
        EP_RM2C_ESCORT_BEAT_GROUP = 2275,
        ///正在进攻护送商队的己方魂兽信息回复
        EP_RM2C_ESCORT_BEAT_SELF_BEAST = 2273,
        ///正在进攻护送商队的己方卡牌信息回复
        EP_RM2C_ESCORT_BEAT_SELF_PET = 2271,
        ///获取一个护送商队的队伍信息回复
        EP_RM2C_ESCORT_GET_TEAM = 2269,
        ///获取防守护送商队的信息(动长)回复
        EP_RM2C_ESCORT_GET_GROUP = 2267,
        ///自己守护的护送商队列表回复   (realm 去 pks 要)
        EP_RM2C_EESCORT_GROUP_LIST = 2265,
        ///玩家护送信息回复
        EP_RM2C_ESCORT_INFO = 2263,
        // 护送 end
        /// 回复最近的条聊天内容
        EP_RM2C_CHAT_RECENT_RESPONSE = 2261,
        /// 发送聊天信息
        EP_RM2C_CHAT = 2259,
        ///升级给的体力，只用来显示
        EP_RM2C_POWER_LV_ADD = 2257,
        ///是否在阵法上
        EP_RM2C_MOT_HELPER_IS_IN_TEAM = 2255,
        ///离开远征
        EP_RM2C_MOT_LEAVE = 2253,
        ///远征战斗错误
        EP_RM2C_MOT_PK_ERROR = 2251,
        ///返回佣兵信息
        EP_RM2C_MOT_HELPER_INFO = 2249,
        ///卡牌选择佣兵
        EP_RM2C_MOT_HELPER = 2247,
        ///敌人队伍信息更新
        EP_RM2C_MOT_TEAM_UPDATE = 2245,
        ///返回进入远征
        EP_RM2C_MOT_GOTO = 2243,
        ///远征买buffer
        EP_RM2C_MOT_BUY_BUFFER = 2241,
        ///远征宝箱奖励
        EP_RM2C_MOT_BOX = 2239,
        ///远征奖励
        EP_RM2C_MOT_REWARD = 2237,
        ///海山巫医商店购买返回
        EP_RM2C_MOT_MAGIC_SHOP_BUY = 2236,
        ///远征重置
        EP_RM2C_MOT_RESET = 2235,
        ///远征卡牌信息
        EP_RM2C_MOT_PET = 2233,
        ///远征队伍
        EP_RM2C_MOT_TEAM = 2231,
        ///返回远征信息
        EP_RM2C_MOT_INFO = 2229,
        ///世界boss评价奖励结束发送
        EP_RM2C_BOSS_VALUE_REWARD_END = 2227,
        ///世界boss评价奖励开始发送
        EP_RM2C_BOSS_VALUE_REWARD_BEGIN = 2225,
        ///世界boss钱奖励
        EP_RM2C_BOSS_MONEY_REWARD = 2223,
        ///世界boss的评价
        EP_RM2C_BOSS_VALUE = 2221,
        ///世界boss战斗校验错误
        EP_RM2C_BOSS_CHECK_ERROR = 2219,
        ///更新世界boss
        EP_RM2C_BOSS_UPDATE = 2217,
        ///返回英雄试炼boss过期刷新
        EP_RM2C_BOSS_TIME_OUT = 2215,
        ///返回英雄试炼boss重置
        EP_RM2C_BOSS_RESET = 2214,
        ///返回英雄试炼boss信息
        EP_RM2C_BOSS_INFO = 2213,
        ///返回进入boss
        EP_RM2C_BOSS_GOTO = 2211,
        /// 同步战队信息
        EP_RM2C_TEAM_NOTIFY = 2209,
        ///同步主角副本信息
        EP_RM2C_NOTIFY_LEAD_FB = 2207,
        ///开启网络邮件
        EP_RM2C_WEB_EMAIL_OPEN = 2205,
        ///请求网路邮件列表
        EP_RM2C_WEB_EMAIL = 2203,
        ///通知客户端有新的网络邮件
        EP_RM2C_WEB_EMAIL_NOTIFY_NEW = 2201,
        ///返回佣兵
        EP_RM2C_FRIEND_HELPER = 2199,
        ///好友出战历史记录
        EP_RM2C_FRIEND_FIGHT_HISTORY = 2197,
        ///返回出售卡牌碎片
        EP_RM2C_PET_PIECE_SELL = 2195,
        ///魂侠界面
        EP_RM2C_LUCKY_SOUL_LIST = 2194,
        ///魂侠抽奖
        EP_RM2C_LUCKY_SOUL = 2193,
        ///卡牌合成
        EP_RM2C_PET_PIECE_TO_PET = 2191,
        ///返回购买技能点
        EP_RM2C_SKILL_POINT_BUY = 2189,
        ///返回技能点回复
        EP_RM2C_SKILL_POINT_ADD = 2187,
        ///新的技能升级
        EP_RM2C_SKILL_UP_NEW = 2185,
        /// 返回卡牌升星
        EP_RM2C_PET_STAR_UP = 2183,
        ///卡牌升级
        EP_RM2C_PET_LV_UP = 2181,
        ///返回等级任务奖励
        EP_RM2C_TASK_LV = 2179,
        ///返回每日任务奖励
        EP_RM2C_TASK_EVERYDAY_REWARD = 2177,
        ///海山魂兽回复
        EP_RM2C_MOT_BEAST = 2176,
        ///返回主线任务奖励
        EP_RM2C_TASK_FB_REWARD = 2175,
        ///开礼盒回复
        EP_RM2C_OPEN_GIFT_BOX = 2174,
        ///返回新手任务奖励
        EP_RM2C_TASK_GUIDE_REWARD = 2173,
        ///重置太阳井
        EP_RM2C_SUN_WELL_RESET = 2172,
        ///返回任务信息
        EP_RM2C_TASK = 2171,
        ///太阳井信息
        EP_RM2C_SUN_WELL_INFO = 2170,
        ///返回补签
        EP_RM2C_SIGN_RE = 2169,
        ///魂兽合成（兑换）
        EP_RM2C_SOUL_COM = 2168,
        ///奖励
        EP_RM2C_REWARD_MONEY = 2167,
        ///魂兽商店（星际商人）购买
        EP_RM2C_SOUL_SHOP_BUY = 2166,
        ///返回签到
        EP_RM2C_SIGN = 2165,
        ///魂兽装备升级回复
        EP_RM2C_BEAST_EQUIP_UP = 2164,
        ///酒足饭饱时间
        EP_RM2C_POWER_MEAL_TIME = 2163,
        ///魂兽穿装备回复
        EP_RM2C_BEAST_EQUIP_WEAR = 2162,
        ///酒足饭饱
        EP_RM2C_POWER_MEAL = 2161,
        ///新增魂兽
        EP_RM2C_NEW_BEAST = 2160,
        ///管理员发奖励
        EP_RM2C_ADMIN_REWARD = 2159,
        ///魂兽商店（星际商人）信息回复
        EP_RM2C_SOUL_SHOP = 2158,
        ///通知停服
        EP_RM2C_SERVER_STOP_NOTE = 2157,
        ///魂兽吃经验药回复
        EP_RM2C_BEAST_LV_UP = 2156,
        ///返回副本重置
        EP_RM2C_FB_RESET = 2155,
        ///魂兽信息回复
        EP_RM2C_BEAST_INFO = 2154,
        ///返回领取月卡之类的充值
        EP_RM2C_CHARGE_CARD = 2153,
        ///零点刷新信息回复
        EP_RM2C_MIN_NIGHT_REFRESH = 2152,
        ///返回订单记录
        EP_RM2C_ORDER_LIST = 2151,
        ///客户端2号ping包回复
        EP_RM2C_PING_PRO_TWO = 2150,
        ///返回充值记录
        EP_RM2C_CHARGE_LIST = 2149,
        ///金币商店购买物品回复
        EP_RM2C_SMONEY_SHOP_BUY = 2148,
        ///完成订单
        EP_RM2C_CHARGE_ORDER_FINISH = 2147,
        ///刷新金币商店回复
        EP_RM2C_REFRESH_SMONEY_SHOP = 2146,
        ///返回充值
        EP_RM2C_CHARGE = 2145,
        ///获得金币商店信息回复
        EP_RM2C_GET_SMONEY_SHOP = 2144,
        ///商城抽奖
        EP_RM2C_LUCKY_SHOP = 2143,
        ///刷新海山商店回复
        EP_RM2C_REFRESH_MOUNTAIN_SHOP = 2142,
        ///随机名字
        EP_RM2C_NAME_RAND = 2141,
        ///获得海山商店信息回复
        EP_RM2C_GET_MOUNTAIN_SHOP = 2140,
        ///海山商店购买物品回复
        EP_RM2C_MOUNTAIN_SHOP_BUY = 2138,
        ///返回点金手
        EP_RM2C_STONE = 2137,
        ///网络邮件文字回复
        EP_RM2C_WEB_EMAIL_TEXT = 2136,
        ///返回备注名
        EP_RM2C_FRIEND_NICK_NAME = 2135,
        ///返回显示扫荡奖励经验药
        EP_RM2C_SHOW_SWEEP_REWARD = 2134,
        ///返回购买体力
        EP_RM2C_POWER_BUY = 2133,
        ///宠物经验增长
        EP_RM2C_TEAM_EXP_ADD = 2132,
        ///返回好友实时数据
        EP_RM2C_FRIEND_FRESH_INFO = 2131,
        ///宠物进阶石进化
        EP_RM2C_PET_STONE_UP = 2130,
        ///返回好友不用实时刷新的信息
        EP_RM2C_FRIEND_NOFRESH_INFO = 2129,
        ///进阶石镶嵌
        EP_RM2C_STONE_INLAY = 2128,
        /// 返回绑定账号
        EP_RM2C_BIND = 2127,
        ///获取手机信息
        EP_RM2C_GET_PHONE_INFO = 2126,
        ///返回购买背包上限
        EP_RM2C_BAG_BUY = 2125,
        ///正义徽章兑换回复
        EP_RM2C_BADGE_SHOP_BUY = 2124,
        ///好友复仇战斗结束
        EP_RM2C_FRIEND_PK_BACK_OVER = 2123,
        ///活动重新战斗回复
        EP_RM2C_RE_ACTIV_EVENT = 2122,
        ///好友复仇
        EP_RM2C_FRIEND_PK_BACK = 2121,
        ///客户端ping包回复
        EP_RM2C_PING_PRO = 2120,
        ///好友切磋结束
        EP_RM2C_FRIEND_PK_OVER = 2119,
        ///副本重新挑战回复
        EP_RM2C_RE_PK_FB = 2118,
        ///离开活动回复
        EP_RM2C_LEAVE_ACTIV_EVENT = 2116,
        ///返回个人签名
        EP_RM2C_ROLE_PSIGN = 2115,
        ///进入活动回复;
        EP_RM2C_GO_TO_ACTIV_EVENT = 2114,
        ///返回起名字
        EP_RM2C_ROLE_NAME = 2113,
        ///增加活动次数回复
        EP_RM2C_ADD_ACTIV_EVENT_CNT = 2112,
        ///返回申请好友列表
        EP_RM2C_FRIEND_ASK_LIST = 2111,
        ///活动战斗验证回复
        EP_RM2C_CHECK_ACTIV_EVENT = 2110,
        ///返回被切磋、被复仇列表
        EP_RM2C_FRIEND_BE_PK_LIST = 2109,
        ///活动开始回复
        EP_RM2C_ACTIV_EVENT_BEGIN = 2108,
        ///同步客户端被切磋或者被复仇
        EP_RM2C_FRIEND_NOTIFY_BE_PK = 2107,
        ///获取活动信息回复
        EP_RM2C_ACTIV_EVENT_INFO = 2106,
        ///返回校验竞技场队伍
        EP_RM2C_PK_CHECK_TEAM = 2105,
        ///获取活动时间回复
        EP_RM2C_ACTIV_EVENT_TIME = 2104,
        ///返回好友pk
        EP_RM2C_FRIEEND_PK = 2103,
        ///刷新竞技场商店回复
        EP_RM2C_REFRESH_PK_SHOP = 2102,
        ///返回领取体力
        EP_RM2C_FRIEND_POWER_GET = 2101,
        ///获得竞技场商店信息回复
        EP_RM2C_GET_PK_SHOP = 2100,
        ///返回赠送体力列表
        EP_RM2C_FRIEND_POWER = 2099,
        ///竞技商店购买物品回复
        EP_RM2C_PK_SHOP_BUY = 2098,
        ///有人赠送体力
        EP_RM2C_FRIEND_POWER_NEW = 2097,
        ///爬塔时间回复
        EP_RM2C_GET_TOWER_TIME = 2096,
        ///返回赠送体力
        EP_RM2C_FRIEND_POWER_SEND = 2095,
        ///购买爬塔商店物品回复
        EP_RM2C_BUY_TOWER_SHOP = 2094,
        ///返回买好友上限
        EP_RM2C_FRIEND_BUY = 2093,
        ///刷新爬塔商店回复
        EP_RM2C_REFRESH_TOWER_SHOP = 2092,
        ///返回查找好友
        EP_RM2C_FRIEND_SEARCH = 2091,
        ///爬塔重新开始回复
        EP_RM2C_TOWER_AGAIN = 2090,
        ///通知删除申请好友
        EP_RM2C_FRIEND_ASK_NOTIFY_DELETE = 2089,
        ///爬塔每日奖励回复
        EP_RM2C_TOWER_DAY_REWARD = 2088,
        ///删除好友
        EP_RM2C_FRIEND_DELETE = 2087,
        ///爬塔通塔奖励回复
        EP_RM2C_TOWER_REWARD = 2086,
        ///新好友
        EP_RM2C_FRIEND_NEW = 2085,
        ///爬塔扫荡回复
        EP_RM2C_TOWER_SWEEP = 2084,
        ///应答好友申请
        EP_RM2C_FRIEND_ASK_ANSWER = 2083,
        ///通知有人加好友
        EP_RM2C_FRIEND_ASK_NOTIFY = 2081,
        ///验证爬塔战斗
        EP_RM2C_CHECK_TOWER_PK = 2080,
        ///返回申请加好友
        EP_RM2C_FRIEND_ASK = 2079,
        ///获取爬塔信息	
        EP_RM2C_GET_TOWER_INFO = 2078,
        ///返回星奖励经验和钱
        EP_RM2C_LAND_STAR_REWARD_EXP_SMONEY = 2077,
        ///断线重连回复
        EP_RM2C_RELOGIN = 2076,
        ///返回新的大陆
        EP_RM2C_LAND_NEW = 2075,
        ///一键强化回复
        EP_RM2C_EQUIP_UP_ONE_KEY = 2074,
        ///副本扫荡得到钱
        EP_RM2C_FB_SWEEP_REWARD = 2073,
        ///装备强化回复
        EP_RM2C_EQUIP_UP = 2072,
        ///返回大陆领取奖励次数信息
        EP_RM2C_LAND_STAR_REWARD = 2071,
        ///装备合成回复
        EP_RM2C_EQUIP_COM = 2070,
        ///返回大陆信息
        EP_RM2C_LAND_LIST = 2069,
        ///下一个副本回复
        EP_RM2C_NEXT_FB = 2068,
        ///返回显示卡牌奖励
        EP_RM2C_SHOW_PET_REWARD = 2067,
        ///摧毁邮件回复
        EP_RM2C_DESTROY_EMAIL = 2066,
        ///返回显示奖励碎片
        EP_RM2C_SHOW_PIECE_REWARD = 2065,
        ///打开邮件
        EP_RM2C_OPEN_EMAIL = 2064,
        ///返回显示奖励装备
        EP_RM2C_SHOW_EQUIP_REWARD = 2063,
        ///添加邮件
        EP_RM2C_CREATE_EMAIL = 2062,
        ///返回副本扫荡
        EP_RM2C_FB_SWEEP = 2061,
        ///邮件列表	
        EP_RM2C_EMAIL_LIST = 2060,
        ///更新副本信息
        EP_RM2C_FB_NOTIFY_INFO = 2059,
        ///更新竞技场阵容回复
        EP_RM2C_UPDATE_PK_TEAM = 2058,
        ///创建新的副本
        EP_RM2C_FB_NEW = 2057,
        ///竞技场pk验证结果回复
        EP_RM2C_CHECK_PK = 2056,
        ///返回装备出售
        EP_RM2C_EQUIP_SELL = 2055,
        ///返回登陆角色的一些标志信息
        EP_RM2C_MASTER_FLAG_INFO = 2054,
        ///返回使用中的队伍
        EP_RM2C_TEAM_WORK_SET = 2053,
        ///请求竞技场匹配回复
        EP_RM2C_MATE_PK = 2052,
        ///返回设置手动释放的技能
        EP_RM2C_PET_SET_SKILL = 2051,
        ///获取爵位奖励
        EP_RM2C_GET_REWARD_NOBILITY = 2050,
        ///返回卖卡牌
        EP_RM2C_PET_SELL = 2049,
        ///升级爵位回复
        EP_RM2C_UP_LV_NOBILITY = 2048,
        ///返回卡牌分解
        EP_RM2C_PIECE_FROM_PET = 2047,
        ///增加PK次数回复
        EP_RM2C_ADD_PK_CNT = 2046,
        ///返回碎片合成
        EP_RM2C_PIECE_TO_PET = 2045,
        ///竞技场排行榜回复
        EP_RM2C_PK_RANK_LIST = 2044,
        ///返回同步装备
        EP_RM2C_EQUIP_PET_NOTIFY = 2043,
        ///竞技场信息回复
        EP_RM2C_PK_INFO = 2042,
        ///返回充值队伍信息
        EP_RM2C_TEAM_SET = 2041,
        ///请求世界boss奖励回复
        EP_RM2C_WORLD_BOSS_REWARD = 2040,
        ///同步体力信息
        EP_RM2C_POWER_INFO = 2039,
        ///离开世界boss回复
        EP_RM2C_LEAVE_WROLD_BOSS = 2038,
        ///返回脱装备
        EP_RM2C_EQUIP_OFF = 2037,
        ///世界boss战斗复活回复
        EP_RM2C_WROLD_BOSS_PK_REVIVE = 2036,
        ///返回穿衣服
        EP_RM2C_EQUIP_ON = 2035,
        ///世界boss战斗验证回复
        EP_RM2C_CHECK_WORLD_BOSS_PK = 2034,
        ///返回卡牌仓库到背包
        EP_RM2C_PET_BAR_TO_BAG = 2033,
        ///进入世界boss回复
        EP_RM2C_GOTO_WORLD_BOSS = 2032,
        ///返回卡牌背包到仓库
        EP_RM2C_PET_BAG_TO_BAR = 2031,
        ///世界boss鼓舞回复
        EP_RM2C_EXCITE_WORLD_BOSS = 2030,
        ///返回碎片列表
        EP_RM2C_PIECE = 2029,
        ///世界boss信息回复
        EP_RM2C_WROLD_BOSS_INFO = 2028,
        ///长体力
        EP_RM2C_POWER_ADD = 2027,
        ///世界活动时间信息返回
        EP_RM2C_WORLD_EVENT_TIME = 2026,
        ///返回技能升级
        EP_RM2C_SKILL_UP = 2025,
        ///创建宠物碎片
        EP_RM2C_CREATE_PIECE = 2024,
        ///返回卡牌进化
        EP_RM2C_PET_UP = 2023,
        ///创建装备
        EP_RM2C_CREATE_EQUIP = 2022,
        ///返回卡牌吞噬升级
        EP_RM2C_PET_EAT = 2021,
        ///创建宠物
        EP_RM2C_CREATE_PET = 2020,
        ///返回推荐的好友
        EP_RM2C_FRIEND_RECOMMEND = 2019,
        ///副本奖励返回
        EP_RM2C_FB_REWARD = 2018,
        ///同步好友战斗时间
        EP_RM2C_FRIEND_NOTIFY_TIME = 2017,
        ///副本战斗验证回复
        EP_RM2C_CHECK_FB_PK = 2016,
        ///开始副本战斗回复
        EP_RM2C_FB_PK_BEGIN = 2015,
        ///离开副本回复
        EP_RM2C_LEAVE_FB = 2014,
        ///发送好友信息
        EP_RM2C_FRIEND = 2013,
        ///进入副本回复
        EP_RM2C_GOTO_FB = 2012,
        ///返回选择战斗好友
        EP_RM2C_FRIEND_FIGHT_SELECT = 2011,
        ///发送副本信息
        EP_RM2C_FB = 2010,
        ///发送装备信息
        EP_RM2C_EQUIPMENT = 2009,
        ///发送材料组
        EP_RM2C_MATERIAL_GROUP = 2008,
        ///发送装备材料组
        EP_RM2C_EQUIP_GROUP = 2007,
        ///发送队伍信息
        EP_RM2C_TEAM_INFO = 2006,
        ///发送宠物信息
        EP_RM2C_PET_INFO_BAR = 2005,
        ///发送宠物信息
        EP_RM2C_PET_INFO_BAG = 2004,
        ///发送用户基本信息
        EP_RM2C_MASTER_BASE_INFO = 2003,
        ///被踢掉通知客户端
        EP_RM2C_TICK_BY_OTHER = 2002,
        ///沙箱回复
        EP_RM2C_SOCK_POL = 2001,
        ///返回登陆
        EP_RM2C_LOGIN = 2000,

        EP_RM2C_ROLE_RENAME = 2700,				    ///修改名字

        EP_RM2C_GET_NOBILITY_SHOP = 2701,			///爵位商店回应
        EP_RM2C_NOBILITY_SHOP_BUY = 2703,           ///购买爵位商店
        EP_RM2C_REFRESH_NOBILITY_SHOP = 2702,       ///爵位商店刷新


        /// c2ac

        ///验证密码，并获取account对应信息
        EP_C2AC_ACCOUNT_INFO = 11000,
        ///客户端向服务器要一个macid
        EP_C2AC_MAC_ID = 11001,
        ///请求最近服务器登陆信息
        EP_C2AC_RECENT_SERVER_INFO = 11002,

        ///ac2c
        ///返回验证密码和account对应信息
        EP_AC2C_ACCOUNT_INFO = 12000,
        ///返回macId
        EP_AC2C_MAC_ID = 12001,
        ///返回最近服务器登陆信息
        EP_AC2C_RECENT_SERVER_INFO = 12002,
    }
}