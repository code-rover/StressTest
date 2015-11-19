using System;
using System.Runtime.InteropServices;
using ExFormatter;

namespace net.unity3d
{
    public class SPlayerBaseInfo : ExFormatterBinary
    {
        public string getMasterName()
        {
            return System.Text.Encoding.UTF8.GetString( cNameMaster, 0, IndexByteToString( cNameMaster ) );
        }

        public string getPSign()
        {
            return System.Text.Encoding.UTF8.GetString( cPSign, 0, IndexByteToString( cPSign ) );
        }

        ///玩家id
        public UInt32 uiIdMaster = new UInt32();
        ///玩家昵称
        public byte[] cNameMaster = new byte[ 32 ];
        ///经验
        public UInt64 luiExp = new UInt64();
        ///人民币
        public UInt32 luiQMoney = new UInt32();
        ///游戏币
        public UInt64 luiSMoney = new UInt64();
        ///爵位币
        public UInt32 luiNMoney = new UInt32();
        ///正义徽章
        public UInt32 uiBadge = new UInt32();
        ///总dkp
        public UInt32 uiDkpAll = new UInt32();
        ///当前dkp
        public UInt32 uiDkpLast = new UInt32();
        ///待学队长技能csvid
        public UInt32 uiIdCsvLeadSkill = new UInt32();
        ///队长id
        public UInt64 uiIdLead = new UInt64();
        ///使用中的队伍id
        //public UInt32 usIdTeam = new UInt32();
        ///当前副本csvid
        public UInt32 uiIdCsvCurFb = new UInt32();
        ///当前副本波次
        public UInt16 usBatchFb = new UInt16();
        ///万能碎片
        public UInt32 uiPieceCnt = new UInt32();
        ///是否优先使用万能碎片 0否 1是
        public byte cPieceFirst = new byte();
        ///服务器当前时间戳，用于与客户端校对时间
        public UInt32 uiServerTime = new UInt32();
        ///上线时间
        public UInt32 uiLoginTime = new UInt32();
        ///下线时间
        public UInt32 uiLogOutTime = new UInt32();
        ///注册时间
        public UInt32 uiRegisterTime = new UInt32();
        ///客户端用的副本标识
        public int iFbFlag = new int();
        ///是否鼓舞了世界boss
        public byte cIsExciteBoss = new byte();
        ///当前世界boss的活动csv id
        public UInt32 uiIdCurWorldBoss = new UInt32();
        ///当前世界boss是否需要复活
        public byte cIsDeadInWorldBoss = new byte();
        ///战斗力
        public UInt32 uiFight = new UInt32();
        ///个人签名
        public byte[] cPSign = new byte[ 130 ];
        ///是否绑定过
        public byte cIsBind = new byte();
        ///点金石
        public short sStoneTimes = new short();
        ///新手引导字段
        public UInt64 uiNewGuide = new UInt64();
        ///新手引导字段
        public UInt64 uiFirstGuide = new UInt64();
        ///总在线时长
        public UInt32 uiOnlineTime = new UInt32();
        ///DDN引导
        public UInt64 uiGuideDDN = new UInt64();
        ///精英副本通关次数
        public UInt16 usCntEliteFb = new UInt16();
        ///魂侠次数m_uiCntChatWorld
        public UInt32 uiLuckySoul = new UInt32();
        ///队长战斗力
        public UInt32 uiLeadFight = new UInt32();
        ///信息刷新时间
        public UInt32 uiUpdateTime = new UInt32();
        ///太阳井已经重置次数
        public UInt32 uiSWCntReset = new UInt32();
        /// 聊天次数
        public UInt32 uiCntChatWorld = new UInt32();
        /// 角色所有队伍的最大战斗力
        public UInt32 uiMaxFightValue = new UInt32();
        ///重读数据库
        public UInt32 uiLoadDb = new UInt32();
    }

    public class SLeadVipInfo : ExFormatterBinary
    {
        ///vip等级
        public UInt32 uiVipLv = new UInt32();
        ///今日充值钻石数
        public UInt32 uiRechargeToday = new UInt32();
        ///第一次购买充值物品记录
        public UInt32 uiPayFirst = new UInt32();
        ///vip经验
        public UInt32 uiVipExp = new UInt32();
        ///累积充值
        public UInt32 uiRechargeAll = new UInt32();

        public UInt32 uiTempVip = new UInt32();         ///临时VIP等级
        public UInt32 uiTempVipEndTime = new UInt32();  ///临时vip结束时间

    }

    public class SLeadFbInfo : ExFormatterBinary
    {
        /// 打精英副本的次数
        public int iFbElitTimes = new int();
        ///打普通副本的次数
        public int iFbDefTimes = new int();
        ///今天副本扫荡的次数
        public int iSweepTodayTimes = new int();
    }

    public class SLeadBagInfo : ExFormatterBinary
    {
        ///宠物背包上限
        public UInt16 usCntBag = new UInt16();
        ///酒馆背包上限
        public UInt16 usCntBar = new UInt16();
        ///装备背包上限
        public UInt16 usCntEquipBag = new UInt16();
        ///购买的材料背包
        public UInt16 uiCntBagProp = new UInt16();
    }

    public class SLeadPowerInfo : ExFormatterBinary
    {
        ///当前体力
        public UInt16 usPower = new UInt16();
        ///体力上限
        public UInt16 usPowerMax = new UInt16();
        ///体力小于上限的时间戳
        public UInt32 uiPowerLessTime = new UInt32();
        ///今天领取体力的次数
        public short sFriendPowerCnt = new short();
        ///今天购买体力的次数
        public short sPowerBuyCnt = new short();
        ///酒足饭饱开始时间
        public UInt32 uiPowerMealBeginTime = new UInt32();
        ///酒足饭饱结束时间
        public UInt32 uiPowerMealEndTime = new UInt32();
        ///本次的剩余次数
        public UInt32 uiPowerMealTimes = new UInt32();
    }

    public class SPkInfo : ExFormatterBinary
    {
        ///历史最高匹配等级
        public UInt32 uiLvFight = new UInt32();
        ///本周匹配等级
        public UInt32 uiLvFightWeek = new UInt32();
        ///竞技积分
        public UInt32 uiSocreFight = new UInt32();
        ///声望
        public UInt32 uiPrestige = new UInt32();
        ///本周胜场
        public UInt16 usCntWinWeek = new UInt16();
        ///剩余竞技次数
        public UInt16 usCntPk = new UInt16();
        ///剩余竞技购买次数
        public UInt16 usCntPkBuy = new UInt16();
        ///爵位
        public byte cNobility = new byte();
        ///剩余爵位奖励次数
        public UInt16 usCntNotilityReward = new UInt16();
        ///累计竞技次数
        public UInt32 uiCntPkAll = new UInt32();
    };

    public class SLeadFriend : ExFormatterBinary
    {
        /// 购买好友的上限,总的好友数就是购买的+lv 基础的
        public int iFriendBuy = new int();
        /// 友情点数
        public UInt32 uiFriendShip = new UInt32();
        /// 今天赠送体力的好友id
        public UInt32[] uiFriendPower = new UInt32[ 7 ];
    };

    public class SLeadFriendPk : ExFormatterBinary
    {
        ///今天切磋的好友id
        public UInt32[] uiFriendPk = new UInt32[ 10 ];
    };

    public class SLeadLuckyShop : ExFormatterBinary
    {
        ///钻石抽
        public short sLuckyShopQMoney = new short();
        ///友情抽
        public short sLuckyShopFriend = new short();
        ///钻石抽上次抽奖时间戳
        public UInt32 uiLastTimeQMoney = new UInt32();
        ///友情抽上次抽奖时间戳
        public UInt32 uiLastTimeFriend = new UInt32();
        ///友情抽今天免费的次数
        public UInt32 uiFriendTodayFreeTimes = new UInt32();
        ///友情抽大暴击累计次数
        public UInt32 uiFriendTimes = new UInt32();
    };

    public class SLeadSign : ExFormatterBinary
    {
        ///本月总累计签到次数（实际+补签）
        public UInt32 uiSignSum = new UInt32();
        ///已补签的次数
        public UInt32 uiSignRe = new UInt32();
        ///上次签到时间（判断今天签没签到）
        public UInt32 uiSignTime = new UInt32();
        ///漏签的次数
        public UInt32 uiSignMiss = new UInt32();
        ///上次VIP签到日期
        public UInt32 uiSignVip = new UInt32();
    };

    public class SLeadSignBeast : ExFormatterBinary
    {
        ///签到位
        public UInt32 uiSignBit = new UInt32();
        ///领取魂兽位
        public UInt32 uiRewardBit = new UInt32();
    };

    public class SLeadSkill : ExFormatterBinary
    {
        ///少于最大值的时间戳
        public UInt32 uiTime = new UInt32();
        ///当前技能点数
        public UInt32 uiSkillPoint = new UInt32();
        ///今天购买的技能点次数
        public UInt32 uiBuyCnt = new UInt32();
    };

    public class SLeadGuide : ExFormatterBinary
    {
        public string getGuideStr()
        {
            return System.Text.Encoding.UTF8.GetString( cGuideStrYXH, 0, IndexByteToString( cGuideStrYXH ) );
        }
        ///晓航的新手以引导字段	
        public byte[] cGuideStrYXH = new byte[ 300 ];
        ///lz的新手引导字段,客户端用不到
        public UInt64 uiGuideLz = new UInt64();
    };

    //公会
    public enum EGU_SetType
    {
        ESet_Pic = 1,
        ESet_PicFrame = 2,
        ESet_Name = 3,
        ESet_JoinType = 4,
        ESet_JoinLevel = 5,
    };

    public enum EGU_LogType
    {
        ELog_Join = 1,
        ELog_Exit = 2,
        ELog_Kick = 3,
        ELog_SetPic = 4,
        ELog_SetPicFrame = 5,
        ELog_SetName = 6,
        ELog_SetJoinType = 7,
        ELog_SetJoinLevel = 8,
        ELog_SetAnnouncement = 9,
    };

    public class stGameUnionLog : ExFormatterBinary
    {
        public void setSrcName( string psContent )
        {
            byte[] contentByte = System.Text.Encoding.UTF8.GetBytes( psContent );
            for( int i = 0; i < contentByte.Length; i++ )
            {
                m_cSrcName[ i ] = contentByte[ i ];
            }
        }
        public string getSrcName()
        {
            return System.Text.Encoding.UTF8.GetString( m_cSrcName, 0, byteToStringIndex( m_cSrcName ) );
        }

        public void setDstName( string psContent )
        {
            byte[] contentByte = System.Text.Encoding.UTF8.GetBytes( psContent );
            for( int i = 0; i < contentByte.Length; i++ )
            {
                m_cDstName[ i ] = contentByte[ i ];
            }
        }
        public string getDstName()
        {
            return System.Text.Encoding.UTF8.GetString( m_cDstName, 0, byteToStringIndex( m_cDstName ) );
        }

        public UInt32 m_uiLogTime = new UInt32();
        public byte m_ucLogType = new byte();
        public byte m_ucSrcJob = new byte();
        public byte[] m_cSrcName = new byte[ 32 ];
        public byte[] m_cDstName = new byte[ 32 ];
        public byte m_ucDstValue = new byte();
    };

    public class stGameUnionRequest : ExFormatterBinary
    {
        public void setRoleName( string psContent )
        {
            byte[] contentByte = System.Text.Encoding.UTF8.GetBytes( psContent );
            for( int i = 0; i < contentByte.Length; i++ )
            {
                m_cRoleName[ i ] = contentByte[ i ];
            }
        }
        public string getRoleName()
        {
            return System.Text.Encoding.UTF8.GetString( m_cRoleName, 0, byteToStringIndex( m_cRoleName ) );
        }

        public UInt32 m_uiRequestID = new UInt32();
        public UInt32 m_uiGameUnionID = new UInt32();
        public UInt32 m_uiRoleID = new UInt32();
        public byte[] m_cRoleName = new byte[ 32 ];
        public UInt16 m_usRoleLevel = new UInt16();
        public UInt32 m_uiRequestTime = new UInt32();
        public UInt32 m_uiLeadPic = new UInt32();
    };

    public class stGameUnionMemberBase : ExFormatterBinary
    {
        public UInt32 m_uiGUID = new UInt32();							//公会ID
        public byte m_ucJob = new byte();							//职务：0-成员，1-会长，2-长老
        public UInt32[] m_uiActiveValue = new UInt32[ 8 ];	//7天活跃值
    };
    public class stGameUnionMemberClient : ExFormatterBinary
    {
        public void setRoleName( string psContent )
        {
            byte[] contentByte = System.Text.Encoding.UTF8.GetBytes( psContent );
            for( int i = 0; i < contentByte.Length; i++ )
            {
                m_cRoleName[ i ] = contentByte[ i ];
            }
        }
        public string getRoleName()
        {
            return System.Text.Encoding.UTF8.GetString( m_cRoleName, 0, byteToStringIndex( m_cRoleName ) );
        }

        public stGameUnionMemberBase m_sMemberBase = new stGameUnionMemberBase();
        public UInt32 m_uiRoleID = new UInt32();
        public byte[] m_cRoleName = new byte[ 32 ];
        public UInt32 m_uiStartTime = new UInt32();					//7天活跃值的起始时间
        public UInt32 m_uiLastLoginTime = new UInt32();				//最后登录时间
        public UInt16 m_usRoleLevel = new UInt16();
        public UInt32 m_uiLeadPic = new UInt32();
        public Int32 m_iGUMCardLeftTime = new Int32();				//公会月卡剩余秒数
    };

    public class stGameUnion : ExFormatterBinary
    {
        public void setName( string psContent )
        {
            byte[] contentByte = System.Text.Encoding.UTF8.GetBytes( psContent );
            for( int i = 0; i < contentByte.Length; i++ )
            {
                m_cName[ i ] = contentByte[ i ];
            }
        }
        public string getName()
        {
            return System.Text.Encoding.UTF8.GetString( m_cName, 0, byteToStringIndex( m_cName ) );
        }

        public void setAnnouncement( string psContent )
        {
            byte[] contentByte = System.Text.Encoding.UTF8.GetBytes( psContent );
            for( int i = 0; i < contentByte.Length; i++ )
            {
                m_cAnnouncement[ i ] = contentByte[ i ];
            }
        }
        public string getAnnouncement()
        {
            return System.Text.Encoding.UTF8.GetString( m_cAnnouncement, 0, byteToStringIndex( m_cAnnouncement ) );
        }

        public UInt32 m_uiID = new UInt32();
        public byte[] m_cName = new byte[ 32 ];
        public byte m_ucType = new byte();			//类型：0-不允许加入，1-验证加入，2-允许加入
        public UInt32 m_uiJoinLevel = new UInt32();	//加入等级限制
        public UInt32 m_uiPic = new UInt32();			//公会图标
        public UInt32 m_uiPicFrame = new UInt32();		//公会图标框
        public UInt32 m_uiActiveValue = new UInt32();	//公会活跃值
        public byte[] m_cAnnouncement = new byte[ 96 ];	//公告
        public byte m_ucTotalMemberCount = new byte();	//公会最大人数
        public byte m_ucCurMemberCount = new byte();		//公会当前人数
        public UInt32 m_uiDayRefreshTime = new UInt32();	//公会刷新时间
        public byte m_ucKickCount = new byte();	//已踢人数量
        public UInt16 m_usActiveAdd = new UInt16();	//已增加的活跃度值
    };

    public class SPlayerInfo : ExFormatterBinary
    {
        public SPlayerBaseInfo sPlayerBaseInfo = new SPlayerBaseInfo();

        ///VIP信息
        public SLeadVipInfo sLeadVipInfo = new SLeadVipInfo();
        ///体力信息
        public SLeadPowerInfo sLeadPowerInfo = new SLeadPowerInfo();
        ///背包信息
        public SLeadBagInfo sLeadBagInfo = new SLeadBagInfo();
        ///副本统计信息
        public SLeadFbInfo sLeadFbInfo = new SLeadFbInfo();
        ///竞技场信息
        public SPkInfo SLeadPkInfo = new SPkInfo();
        ///好友的信息
        public SLeadFriend sLeadFriend = new SLeadFriend();
        ///好友pk
        public SLeadFriendPk sLeadFriendPk = new SLeadFriendPk();
        ///商城抽奖
        public SLeadLuckyShop sLeadLuckyShop = new SLeadLuckyShop();
        ///签到信息
        public SLeadSign sLeadSign = new SLeadSign();
        ///技能信息
        public SLeadSkill sLeadSkill = new SLeadSkill();
        ///新手引导相关
        public SLeadGuide sLeadGuide = new SLeadGuide();
        ///签到魂兽
        public SLeadSignBeast sLeadSignBeast = new SLeadSignBeast();
        ///公会信息
        public stGameUnionMemberBase sGameUnionMemberBase = new stGameUnionMemberBase();
    };

    ///魂兽（畜生）数据模型
    public class SBeast : ExFormatterBinary
    {
        ///宠物id
        public UInt64 uiIdPet = new UInt64();
        ///宠物csvid
        public UInt32 uiIdCsvPet = new UInt32();
        ///主人Id
        public UInt32 uiIdMaster = new UInt32();
        ///经验
        public UInt64 luiExp = new UInt64();
        ///穿戴装备
        public UInt32[] vctLuiIdEquip = new UInt32[ 3 ];
    }

    ///魂兽商店
    public class SBeastShop : ExFormatterBinary
    {
        ///玩家id
        public UInt32 uiIdMaster = new UInt32();
        ///购买情况0未购买，1已经购买
        public byte[] vctcBuyInfo = new byte[ 3 ];
        ///刷新时间
        public UInt32 uiRefreshTime = new UInt32();
    };

    public class SPetInfo : ExFormatterBinary
    {
        ///宠物id
        public UInt64 uiIdPet = new UInt64();
        ///宠物csvid
        public UInt32 uiIdCsvPet = new UInt32();
        ///主人Id
        public UInt32 uiIdMaster = new UInt32();
        ///是否摧毁
        public byte cIsDestory = new byte();
        ///宠物位置类型0背包1酒馆
        public byte cTypeLoc = new byte();
        ///经验
        public UInt64 luiExp = new UInt64();
        ///宠物技能组
        public UInt32[] vctUiIdCsvSkill = new UInt32[ 5 ];
        ///手动释放技能
        public UInt32 uiIdCsvHandSkill = new UInt32();
        ///穿戴装备
        public UInt32[] vctLuiIdEquip = new UInt32[ 6 ];
        ///进阶石信息
        public UInt32 uiStone = new UInt32();
        ///卡牌加几
        public short sAddNum = new short();
        ///是否被保护
        public byte cIsProtect = new byte();
        ///星数
        public UInt32 uiStar = new UInt32();
    }

    public class STeam : ExFormatterBinary
    {
        ///队伍id
        public UInt32 uiIdTeam = new UInt32();
        ///主人Id
        public UInt32 uiIdMaster = new UInt32();
        ///队伍位置
        public byte cLoc = new byte();
        ///队长id
        public UInt64 uiIdLead = new UInt64();
        ///队员id
        public UInt64[] vctUiIdPet = new UInt64[ 4 ];
        ///魂兽id
        public UInt64 luiIdBeast = new UInt64();
    }

    public class SMaterial : ExFormatterBinary
    {
        ///csv id
        public UInt32 uiIdCsv = new UInt32();
        ///数量
        public UInt16 usCnt = new UInt16();

    }

    public class SRewardMaterial : ExFormatterBinary
    {
        ///csv id
        public UInt32 uiIdCsv = new UInt32();
        ///数量
        public UInt16 usCnt = new UInt16();
        ///掉落分类5物品 7碎片 
        public byte cType = new byte();

    }

    public class SRewardMaterialDB : ExFormatterBinary
    {
        ///csv id
        public UInt32 uiIdCsv = new UInt32();
        ///数量
        public UInt16 usCnt = new UInt16();
        ///掉落分类1货币2经验3碎片4卡牌5装备
        public byte cType = new byte();
        ///服务器id
        public UInt64 luiIdServer = new byte();

    }

    public class SEquipGroup : ExFormatterBinary
    {
        ///装备材料组id
        public UInt64 luiIdEquipGroup = new UInt64();
        ///主人Id
        public UInt32 uiIdMaster = new UInt32();
        ///装备材料类型
        public byte cType = new byte();
        ///材料数量
        public SMaterial[] vctSMaterial = new SMaterial[ 5 ];

    }

    public class SMaterialGroup : ExFormatterBinary
    {
        ///材料组id
        public UInt64 luiIdMateriaGroup = new UInt64();
        ///主人Id
        public UInt32 uiIdMaster = new UInt32();
        ///材料类型
        public byte cType = new byte();
        ///材料数量
        public SMaterial[] vctSMaterial = new SMaterial[ 3 ];

    }

    public class SEquipment : ExFormatterBinary
    {
        ///材装备的id server
        public UInt64 luiIdEquipment = new UInt64();
        ///csv id
        public UInt32 uiIdCsvEquipment = new UInt32();
        ///主人Id
        public UInt32 uiIdMaster = new UInt32();
        ///个数
        public int iCnt = new int();
        ///真实个数：包括身上穿戴的
        public int iRealCnt = new int();
    }

    public class SFBInfo : ExFormatterBinary
    {
        ///id server
        public UInt64 luiIdFB = new UInt64();
        ///csv id
        public UInt32 uiIdCsvFB = new UInt32();
        ///主人Id
        public UInt32 uiIdMaster = new UInt32();
        ///通关评级
        public byte cLvKo = new byte();
        ///今天打副本的次数
        public short sKoTodayTimes = new short();
        ///今天副本重置的次数
        public byte cResetTimes = new byte();
    }

    public class SEmail : ExFormatterBinary
    {
        ///id server
        public UInt64 luiIdEmail = new UInt64();
        ///csv id
        public UInt32 uiIdCsvEmail = new UInt32();
        ///主人Id
        public UInt32 uiIdMaster = new UInt32();
        ///获得时间
        public UInt32 uiIdTimeGet = new UInt32();
        ///开启时间
        public UInt32 uiIdTimeOpen = new UInt32();
        ///是否打开
        public byte cIsOpen = new byte();
        ///是否摧毁
        public byte cIsDestroy = new byte();
    }

    public class SAccountC2AC : ExFormatterBinary
    {
        public SAccountC2AC()
        {

        }
        public void setAccount( string psAccount )
        {
            byte[] accountByte = System.Text.Encoding.UTF8.GetBytes( psAccount );
            for( int i = 0; i < accountByte.Length; i++ )
            {
                cAccount[ i ] = accountByte[ i ];
            }
        }
        public void setPassword( string psAccount )
        {
            byte[] accountByte = System.Text.Encoding.UTF8.GetBytes( psAccount );
            for( int i = 0; i < accountByte.Length; i++ )
            {
                cPassword[ i ] = accountByte[ i ];
            }
        }
        public void setDeviceInfo( string device )
        {
            byte[] accountByte = System.Text.Encoding.UTF8.GetBytes( device );
            for( int i = 0; i < accountByte.Length; i++ )
            {
                cDeviceInfo[ i ] = accountByte[ i ];
            }
        }
        public void setSession( string session )
        {
            byte[] sessionByte = System.Text.Encoding.UTF8.GetBytes( session );
            for( int i = 0; i < sessionByte.Length; i++ )
            {
                cSessionId[ i ] = sessionByte[ i ];
            }
        }
        public string getAccount()
        {
            return System.Text.Encoding.UTF8.GetString( cAccount, 0, byteToStringIndex( cAccount ) );
        }
        public string getPassword()
        {
            return System.Text.Encoding.UTF8.GetString( cPassword, 0, byteToStringIndex( cPassword ) );
        }
        public string getDeviceInfo()
        {
            return System.Text.Encoding.UTF8.GetString( cDeviceInfo, 0, byteToStringIndex( cDeviceInfo ) );
        }

        public string getSessionId()
        {
            return System.Text.Encoding.UTF8.GetString( cSessionId, 0, byteToStringIndex( cSessionId ) );
        }
        public void setAccountIdStr( string psAccountId )
        {
            byte[] accountByte = System.Text.Encoding.UTF8.GetBytes( psAccountId );
            for( int i = 0; i < accountByte.Length; i++ )
            {
                cAccountId[ i ] = accountByte[ i ];
            }
        }
        public string getAccountIdStr()
        {
            return System.Text.Encoding.UTF8.GetString( cAccountId, 0, byteToStringIndex( cAccountId ) );
        }

        public void setChannelIdStr( string psChannelId )
        {
            byte[] channelByte = System.Text.Encoding.UTF8.GetBytes( psChannelId );
            for( int i = 0; i < channelByte.Length; i++ )
            {
                cChannelId[ i ] = channelByte[ i ];
            }
        }
        public string getChannelIdStr()
        {
            return System.Text.Encoding.UTF8.GetString( cChannelId, 0, byteToStringIndex( cChannelId ) );
        }

        public byte[] cAccount = new byte[ 64 ];
        public byte[] cPassword = new byte[ 33 ];
        public byte[] cDeviceInfo = new byte[ 150 ];
        ///public UInt64 liAccountId = new UInt64();	/// no use
        public byte[] cChannelId = new byte[ 32 ];
        public byte[] cAccountId = new byte[ 64 ];
        public byte[] cSessionId = new byte[ 128 ];
        ///x 1：内网测试 2:91
        public byte serverType = new byte();
    };

    public class SAccountAC2C : ExFormatterBinary
    {
        public void setAccountIdStr( string psAccountId )
        {
            byte[] accountByte = System.Text.Encoding.UTF8.GetBytes( psAccountId );
            for( int i = 0; i < accountByte.Length; i++ )
            {
                cAccountId[ i ] = accountByte[ i ];
            }
        }
        public string getAccountIdStr()
        {
            return System.Text.Encoding.UTF8.GetString( cAccountId, 0, byteToStringIndex( cAccountId ) );
        }

        public void setChannelIdStr( string psChannelId )
        {
            byte[] channelByte = System.Text.Encoding.UTF8.GetBytes( psChannelId );
            for( int i = 0; i < channelByte.Length; i++ )
            {
                cChannelId[ i ] = channelByte[ i ];
            }
        }
        public string getChannelIdStr()
        {
            return System.Text.Encoding.UTF8.GetString( cChannelId, 0, byteToStringIndex( cChannelId ) );
        }

        ///public UInt64 m_uiAccountId = new UInt64();	/// no use
        public byte[] cChannelId = new byte[ 32 ];
        public byte[] cAccountId = new byte[ 64 ];
        public UInt32[] uiServer = new UInt32[ 5 ];
    }

    public class SFriendClient : ExFormatterBinary
    {
        public string getName()
        {
            return System.Text.Encoding.UTF8.GetString( cName, 0, byteToStringIndex( cName ) );
        }
        public string getNickName()
        {
            return System.Text.Encoding.UTF8.GetString( cNickName, 0, byteToStringIndex( cNickName ) );
        }
        /// 好友的id
        public UInt32 uiFriendId = new UInt32();
        /// 主角经验
        public UInt64 ulExp = new UInt64();
        /// 爵位
        public byte cNobility = new byte();
        ///战斗力
        public UInt32 uiFight = new UInt32();
        /// 声望
        public UInt32 uiPrestige = new UInt32();
        /// 默认队伍的队长id
        public UInt32 uiLeadCsvId = new UInt32();
        /// 队长经验
        public UInt64 ulLeadExp = new UInt64();
        /// 加几
        public short sAddNum = new short();
        ///是否在线 0 :没在线 1：在线
        public byte cIsOnline = new byte();
        /// 队长的战斗力
        public UInt32 uiPetFight = new UInt32();
        /// 队长的server id
        public UInt64 uiPetId = new UInt64();
        /// 队长的星级
        public UInt32 uiStar = new UInt32();
        /// 手动释放技能
        public UInt32 uiSkillId = new UInt32();
        ///别名/备注姓名
        public byte[] cNickName = new byte[ 32 ];
        ///名字
        public byte[] cName = new byte[ 32 ];
        ///好友的类型
        public byte cType = new byte();
        ///成为好友的时间
        public UInt32 uiMakeTime = new UInt32();
    }

    public class SFriendFreshInfo : ExFormatterBinary
    {
        /// 好友的id
        public UInt32 uiFriendId = new UInt32();
        /// 主角经验
        public UInt64 ulExp = new UInt64();
        /// 爵位
        public byte cNobility = new byte();
        ///队伍的战斗力
        public UInt32 uiFight = new UInt32();
        /// 声望
        public UInt32 uiPrestige = new UInt32();
        /// 默认队伍的队长id
        public UInt32 uiLeadCsvId = new UInt32();
        /// 队长经验
        public UInt64 ulLeadExp = new UInt64();
        /// 加几
        public short sAddNum = new short();
        ///是否在线 0 :没在线 1：在线
        public byte cIsOnline = new byte();
        ///队长的战斗力
        public UInt32 uiLeadFight = new UInt32();
        ///队长的server id
        public UInt64 uiPetId = new UInt64();
        ///队长的星级
        public UInt32 uiStar = new UInt32();
        ///手动释放的技能
        public UInt32 uiSkill = new UInt32();
    }

    public class SFriendReInfo : ExFormatterBinary
    {
        ///这个好友的id server
        public UInt32 uiRoleId = new UInt32();
        ///名字
        public byte[] cName = new byte[ 32 ];
        /// 好友的经验
        public UInt64 uiExp = new UInt64();
        ///推荐好友的类型 0:会获得10点友情id，1：不会获得友情点的好友 2:会获得5点友情点的冒险者
        public byte cType = new byte();
        ///战斗宠物id
        public UInt64 uiPet = new UInt64();
        ///宠物csv id
        public UInt32 uiIdCsvPet = new UInt32();
        ///宠物经验值，换算等级
        public UInt64 uiPetExp = new UInt64();
        ///宠物战斗力
        public UInt32 uiFight = new UInt32();
        ///攻击力
        public int iAtk = new int();
        ///血量
        public int iHp = new int();
        ///队长技能
        public UInt32 uiIdCsv = new UInt32();
        /// 加几
        public int iAddNum = new int();


        public string getRoleName()
        {
            return System.Text.Encoding.UTF8.GetString( cName, 0, IndexByteToString( cName ) );
        }
    }

    public class SFriendFightInfo : ExFormatterBinary
    {
        /// 角色id
        public UInt32 uiRoleId = new UInt32();
        ///更新好友最新战斗时间
        public UInt32 uiLastTime = new UInt32();
    }

    public class SPetExpAdd : ExFormatterBinary
    {
        /// 宠物id
        public UInt64 luiIdPet = new UInt64();
        /// 之前经验
        public UInt64 luiExpBefore = new UInt64();
        /// 之后经验
        public UInt64 luiExpNow = new UInt64();
    }

    public class SFightReward : ExFormatterBinary
    {
        public SFightReward()
        {
            for( int i = 0; i < 3; ++i )
            {
                vctSMaterial[ i ] = new SRewardMaterial();
            }
        }
        ///掉落怪物位置
        public short m_sLoc = new short();
        ///掉落金钱
        public UInt64 m_luiMoney = new UInt64();
        ///副本波次
        public int m_iBatch = new int();
        ///掉落物品
        public SRewardMaterial[] vctSMaterial = new SRewardMaterial[ 3 ];
    }

    public class SFightingRound : ExFormatterBinary
    {
        ///被攻击怪物位置
        public byte m_cPetLoc = new byte();
        ///攻击怪物位置
        public byte m_cAckPetLoc = new byte();

        ///攻击技能/或者buff
        public UInt32 m_uiIdCsvSkill = new UInt32();

        ///出手次序
        public UInt16 m_usNumFight = new UInt16();
        ///技能的第几波伤害
        public UInt16 m_usBatchSkill = new UInt16();


        ///被攻击者状态（位操作111，闪避，暴击 死亡）
        public byte m_beAckedState = new byte();

        /// 伤害类型 0技能 1buff反弹 2buff掉血(仅死亡有) 3复活 4吸血技能 5吸血buff
        public byte m_cHarmType = new byte();
        ///正伤害量/负加血量
        public int m_iHarm = new int();
        ///时间 
        public float m_fTime = new float();
    }

    public class SMasterActivEvent : ExFormatterBinary
    {
        /// 活动server id
        public UInt64 m_luiIdServerActivEvent = new UInt64();
        /// 玩家id
        public UInt32 m_uiIdMaster = new UInt32();
        ///活动csv id
        public UInt32 m_uiIdCsvActivEvent = new UInt32();
        ///剩余次数
        public UInt16 m_usCntFight = new UInt16();
        ///已经重置次数
        public UInt16 m_usCntBuy = new UInt16();
    }

    public class SActivEventTime : ExFormatterBinary
    {
        /// 活动id
        public UInt32 m_uiIdCsv = new UInt32();
        ///开始世界
        public UInt32 m_uiBeginTime = new UInt32();
        ///结束时间
        public UInt32 m_uiEndTime = new UInt32();
        ///是否已经结束
        public byte cIsOver = new byte();
        ///最多可以打几次，小于0代表无限制
        public int iCntMaxFight = new int();
    }

    public class SWorldEventTime : ExFormatterBinary
    {
        /// 世界活动id
        public UInt32 m_uiIdCsv = new UInt32();
        ////世界boss csv id
        public UInt32 m_uiBossId = new UInt32();
        ///开始世界
        public UInt32 m_uiBeginTime = new UInt32();
        ///结束时间
        public UInt32 m_uiEndTime = new UInt32();
        ///是否已经结束
        public byte cIsOver = new byte();
        ///每天最大攻击次数
        public int iCntMaxFight = new int();
    }

    public class SWorldBossRank : ExFormatterBinary
    {
        ///玩家server Id
        public UInt32 uiIdMaster = new UInt32();
        ///昵称
        public byte[] sRoleName = new byte[ 32 ];
        ///伤害量
        public UInt64 luiHarm = new UInt64();
        ///排名
        public UInt16 usRank = new UInt16();

        public string getRoleName()
        {
            return System.Text.Encoding.UTF8.GetString( sRoleName, 0, IndexByteToString( sRoleName ) );
        }
    }

    public class SPkPetInfo : ExFormatterBinary
    {
        ///宠物server Id
        public UInt64 luiIdServer = new UInt64();
        ///csv id
        public UInt32 uiIdCsv = new UInt32();
        ///经验
        public UInt64 luiExp = new UInt64();
        ///星级
        public UInt32 uiStarPet = new UInt32();
    };

    public class SPkRankListBase : ExFormatterBinary
    {
        ///玩家server Id
        public UInt32 uiIdMaster = new UInt32();
        ///昵称
        public byte[] sRoleName = new byte[ 32 ];
        ///排名
        public UInt32 uiRank = new UInt32();
        ///匹配积分
        public UInt32 uiLvFight = new UInt32();
        ///队长宠物csv id
        public SPkPetInfo SPetLead = new SPkPetInfo();
        ///普通宠物csv id
        public SPkPetInfo[] vctPet = new SPkPetInfo[ 4 ];
        ///魂兽
        public SBeast SBeastInfo = new SBeast();

        public string getRoleName()
        {
            return System.Text.Encoding.UTF8.GetString( sRoleName, 0, IndexByteToString( sRoleName ) );
        }

    };

    public class SMoney : ExFormatterBinary
    {
        ///金币
        public int iQMoney = new int();
        ///游戏币
        public Int64 iSMoney = new Int64();
    }

    public class SPetEat : ExFormatterBinary
    {
        /// 卡牌的id
        public UInt64 uiPetId = new UInt64();
        ///卡牌的csv id
        public UInt32 uiCsvId = new UInt32();
        ///卡牌的经验
        public UInt64 uiExp = new UInt64();
        ///卡牌 加几
        public short sAddNum = new short();
    }

    public class SCostInfo : ExFormatterBinary
    {
        /// id server, 如果是物品就是csv id， 如果是卡牌则是id server 
        public UInt64 uiId = new UInt64();
        ///剩余的数量，如果是0，就把这个物品删除掉
        public int iCnt = new int();
        ///消耗的类型 0:物品 1：卡牌 2 卡牌碎片 3 魂兽
        public byte cType = new byte();
    }

    public class SSkillup : ExFormatterBinary
    {
        ///宠物
        public UInt64 uiPetId = new UInt64();
        ///技能升级前的技能skill id
        public UInt32 uiOldSkillId = new UInt32();
        ///技能升级后的技能skill id
        public UInt32 uiNewSkillId = new UInt32();
        ///手动释放技能
        public UInt32 uiHandSkillId = new UInt32();
    }

    public class SPiece : ExFormatterBinary
    {
        ///id server
        public UInt64 luiIdPiece = new UInt64();
        ///csv id
        public UInt32 uiCsvId = new UInt32();
        ///个数
        public int iCnt = new int();
    };

    public class SPetLoc : ExFormatterBinary
    {
        ///卡牌id
        public UInt64 uiPetId = new UInt64();
        ///位置类型 0：在身上 1：在酒馆
        public byte cTypeLoc = new byte();
    };

    public class STeamUpdate : ExFormatterBinary
    {
        ///队伍id server
        public UInt32 uiIdTeam = new UInt32();
        ///队长id
        public UInt64 uiIdLead = new UInt64();
        ///其他四个队员
        public UInt64[] uiIdPet = new UInt64[ 4 ];
        ///魂兽id
        public UInt64 luiIdBeast = new UInt64();
    };

    public class SBeastEquip : ExFormatterBinary
    {
        ///卡牌id server
        public UInt64 uiBeastId = new UInt64();
        ///身上的装备id csv
        public UInt32[] uiIdEquip = new UInt32[ 3 ];
    };

    public class SPetEquip : ExFormatterBinary
    {
        ///卡牌id server
        public UInt64 uiPetId = new UInt64();
        ///身上的装备id csv
        public UInt32[] uiIdEquip = new UInt32[ 6 ];
    };

    public class SPieceUpdate : ExFormatterBinary
    {
        ///碎片的csv id
        public UInt32 uiPieceid = new UInt32();
        ///数量
        public int iCnt = new int();
    };

    public class SEquipCsvIdCnt : ExFormatterBinary
    {
        ///物品的csv id
        public UInt32 uiCsvId = new UInt32();
        ///数量
        public int iCnt = new int();

    };

    public class STowerShopGood : ExFormatterBinary
    {
        ///商品csv id
        public UInt32 uiIdCsvGoods = new UInt32();
        ///是否已经购买
        public byte cIsBuy = new byte();
    };

    public class STowerInfo : ExFormatterBinary
    {

        public STowerInfo()
        {
            for( int i = 0; i < 8; ++i )
            {
                m_vctShopGoodsp[ i ] = new STowerShopGood();
            }
        }
        ///当前塔id
        public UInt16 usidTower = new UInt16();
        ///当前层数
        public UInt16 usLvTower = new UInt16();
        ///最高层数
        public UInt16 usMaxTower = new UInt16();
        ///当前获取奖励层数
        public UInt16 usLvReward = new UInt16();
        ///灵石
        public UInt32 uiMoneyTower = new UInt32();
        ///剩余扫荡次数
        public UInt16 usCntSweep = new UInt16();
        ///已经刷新商店次数
        public UInt16 usCntShop = new UInt16();
        ///剩余领取奖励次数
        public UInt16 usCntReward = new UInt16();
        ///剩余重新开始次数
        public UInt16 usCntAgain = new UInt16();
        ///商店物品信息
        public STowerShopGood[] m_vctShopGoodsp = new STowerShopGood[ 8 ];
        ///上次刷新时间
        public UInt32 uiLastRefershTime = new UInt32();
    }

    public class SLandReward : ExFormatterBinary
    {
        ///大陆的csv id
        public UInt32 uiCsvId = new UInt32();
        ///普通副本星数奖励领取次数
        public short sStarRewardDef = new short();
        ///精英副本星数奖励领取次数
        public short sStarRewardElit = new short();
        ///挑战副本星数奖励领取次数
        public short sStarRewardChalleng = new short();
    };

    public class SLand : ExFormatterBinary
    {
        public UInt64 uiIdServer = new UInt64();
        ///领取奖励的记录
        public SLandReward sLandReward = new SLandReward();
    };

    public class SFriend : ExFormatterBinary
    {
        /// 好友客户端用到的信息
        public SFriendClient sFriendClient = new SFriendClient();
        ///主角id， 客户端用不到
        public UInt32 uiRoleId = new UInt32();
        ///最近一次邀请战斗的时间，客户端用不到
        public UInt32 uiLastTime = new UInt32();
    };

    public class SFriendAsk : ExFormatterBinary
    {
        /// 好友客户端用到的信息
        public SFriendClient sFriendClient = new SFriendClient();
        ///主角id， 客户端用不到
        public UInt32 uiRoleId = new UInt32();
    };

    public class SFriendPower : ExFormatterBinary
    {
        ///好友id
        public UInt32 uiFriendId = new UInt32();
        ///赠送体力类型
        public byte cType = new byte();
        ///时间
        public UInt32 uiTime = new UInt32();
    };

    public class SFriendBePk : ExFormatterBinary
    {
        ///好友id 
        public UInt32 uiRoleId = new UInt32();
        ///切磋类型 0：被切磋了 1：被复仇了
        public byte cType = new byte();
    };

    public class SFriendNoFreshInfo : ExFormatterBinary
    {
        public string getPsign()
        {
            return System.Text.Encoding.UTF8.GetString( cPSign, 0, byteToStringIndex( cPSign ) );
        }
        ///好友id 
        public UInt32 uiRoleId = new UInt32();
        ///好友的个性签名
        public byte[] cPSign = new byte[ 130 ];
    };

    public class SFriendPkFight : ExFormatterBinary
    {
        public SFriendPkFight()
        {
            vctPetInfo = new SPetInfo[ 5 ];
            for( int i = 0; i < 5; ++i )
            {
                vctPetInfo[ i ] = new SPetInfo();
            }
        }
        /// 好友id
        public UInt32 uiFriendId = new UInt32();
        /// 战斗卡牌信息
        public SPetInfo[] vctPetInfo;
        ///队伍信息
        public STeam sTeam = new STeam();
        ///爵位
        public byte cLvNoBility = new byte();
        ///魂兽信息
        public SBeast SBeastInfo = new SBeast();
    }

    public class SMoneyFriendShip : ExFormatterBinary
    {
        /// 人民币
        public int iQMoney = new int();
        ///游戏币
        public Int64 iSMoney = new Int64();
        ///友情点
        public int iFriendShip = new int();
    }

    public class SOrders : ExFormatterBinary
    {
        public string getOrderId()
        {
            return System.Text.Encoding.UTF8.GetString( sOrderId, 0, IndexByteToString( sOrderId ) );
        }

        ///名字
        public byte[] sOrderId = new byte[ 128 ];
        ///id server
        public UInt32 uiMasterId = new UInt32();
        ///csv id 
        public UInt32 uiCsvId = new UInt32();
        ///订单时间
        public UInt32 uiTime = new UInt32();
        ///充值活动是否完成，防掉线
        public byte cIsActFinish = new byte();
        ///购买给与物品
        public UInt32 uiIdCsvGive = new UInt32();
        ///是否已经给与
        public UInt32 uiIsGive = new UInt32();
    }

    public class SCharge : ExFormatterBinary
    {
        ///充值记录id server
        public UInt64 uiChargeId = new UInt64();
        ///玩家id
        public UInt32 uiIdMaster = new UInt32();
        ///充值记录csv id
        public UInt32 uiCsvId = new UInt32();
        ///开始时间
        public UInt32 uiTime = new UInt32();
        ///结束时间
        public UInt32 uiOverTime = new UInt32();
        ///次数,只是为了记录冲了多少次
        public UInt32 uiTimes = new UInt32();
        ///上次领取钻石的时间， 用来每天领取像月卡这种功能
        public UInt32 uiLastTime = new UInt32();
        ///计算时间，同级月卡计算时导致优先级低的月卡时间右移
        public UInt32 uiCalcTime = new UInt32();
    }

    public class SFriendPkReward : ExFormatterBinary
    {
        ///奖励的金币
        public UInt32 uiSMoney = new UInt32();
        ///奖励的友情点
        public UInt32 uiFriendShip = new UInt32();
    }

    public class SRewardMoney : ExFormatterBinary
    {
        ///奖励的金币
        public UInt64 uiSMoney = new UInt32();
        ///奖励的钻石
        public UInt32 uiQMoney = new UInt32();
        ///奖励的万能碎片
        public UInt32 uiPiece = new UInt32();
        ///奖励的竞技积分
        public UInt32 uiScoreFight = new UInt32();
        ///奖励的灵石
        public UInt32 uiTiowerMoney = new UInt32();
        ///奖励的正义徽章
        public UInt32 uiBadge = new UInt32();
        ///体力
        public UInt16 uiPower = new UInt16();
        /// 友情点
        public UInt32 uiFriendShip = new UInt32();
        ///声望
        public UInt32 uiPrestige = new UInt32();
        ///玩家经验
        public UInt64 uiExp = new UInt64();
        ///海山币
        public UInt32 uiMot = new UInt32();
        ///Vip经验
        public UInt32 uiVipExp = new UInt32();
    }

    public class SLeadTask : ExFormatterBinary
    {
        ///完成的新任务(新手任务位操作)
        public UInt64 uiGuideTask = new UInt64();
        ///已领取的新手奖励（新手任务奖励位操作1为领取过）
        public UInt64 uiGuideReward = new UInt64();
        ///完成主线副本任务（任务ID）,所有副本自己监听
        public UInt32 uiFbTask = new UInt32();
        ///已经领的普通副本任务奖励（当前领到哪一个）
        public UInt32 uiFbReward = new UInt32();
        ///完成精英副本任务
        public UInt32 uiFbElitTask = new UInt32();
        ///已领的精英副本任务奖励
        public UInt32 uiFbElitReward = new UInt32();
        ///完成挑战副本任务
        public UInt32 uiFbChallengeTask = new UInt32();
        ///已经领取的挑战副本奖励
        public UInt32 uiFbChallengeReward = new UInt32();
        ///今天钻石抽的次数*****服务器告诉次数 ，自己判断次数是否完成
        public UInt32 uiLuckyShopTodayQ = new UInt32();
        ///今天友情抽的次数
        public UInt32 uiLuckyShopTodayFS = new UInt32();
        ///今天普通副本次数
        public UInt32 uiFBTodayDefTimes = new UInt32();
        ///今天精英副本的次数
        public UInt32 uiFbTodayElitTimes = new UInt32();
        ///今天打挑战副本的次数
        public UInt32 uiFbTodayChallengeTimes = new UInt32();
        ///今天爬塔次数
        public UInt32 uiTowerTodayTimes = new UInt32();
        ///今天打世界boss的次数
        public UInt32 uiWorldBossTodayTimes = new UInt32();
        ///竞技场次数******
        public UInt32 uiPkTodayTimes = new UInt32();
        ///每日任务奖励 （位操作）
        public UInt64 uiEveryDayReward = new UInt32();
        ///要领取的等级奖励（任务id）类似副本
        public UInt32 uiTaskLv = new UInt32();
        ///已经领取的等级奖励
        public UInt32 uiTaskLvReward = new UInt32();
        ///今天打的英雄试炼boss次数
        public UInt32 uiBossCnt = new UInt32();
        ///今天打秘境试炼的次数（打钱打羊副本）
        public UInt32 uiActiveEventCnt = new UInt32();
        ///今天技能升级次数
        public UInt32 uiSkillTimes = new UInt32();
        ///今天打远征的次数
        public UInt32 uiMotTimes = new UInt32();
        ///今天进入太阳井的次数
        public UInt32 uiSWTodayGotoTimes = new UInt32();
        ///今天搜索商队次数
        public UInt32 uiSearchFTodayTimes = new UInt32();
        ///每日任务数据备用1:矿洞寻宝
        public UInt32 uiTodayPreData1 = new UInt32();
        ///每日任务数据备用2
        public UInt32 uiTodayPreData2 = new UInt32();
        ///每日任务数据备用3
        public UInt32 uiTodayPreData3 = new UInt32();
        ///每日任务数据备用4
        public UInt32 uiTodayPreData4 = new UInt32();
        ///每日任务数据备用5
        public UInt32 uiTodayPreData5 = new UInt32();
    }

    public class SPetStarInfo : ExFormatterBinary
    {
        /// 卡牌id
        public UInt64 uiPetId = new UInt64();
        ///卡牌csv id
        public UInt32 uiCsvId = new UInt32();
        ///星级
        public UInt32 uiStar = new UInt32();
    }

    public class SFriendHelper : ExFormatterBinary
    {
        public string getRoleName()
        {
            return System.Text.Encoding.UTF8.GetString( cName, 0, IndexByteToString( cName ) );
        }
        /// 佣兵的server id
        public UInt32 uiRoleId = new UInt32();
        ///名字
        public byte[] cName = new byte[ 32 ];
        ///对战id server
        public UInt64 uiPetId = new UInt64();
        ///队长csv id
        public UInt32 uiPetCsvId = new UInt32();
        /// 队长的经验值
        public UInt64 uiPetExp = new UInt64();
        /// 队长的星级
        public UInt32 uiStar = new UInt32();
        ///战斗力
        public UInt32 uiFight = new UInt32();
        ///手动释放的技能
        public UInt32 uiSkill = new UInt32();
        ///佣兵的佣金
        public UInt32 uiSMoney = new UInt32();
        ///派驻时间
        public UInt32 uiHelperTime = new UInt32();
        ///雇佣收益(不包括时间收益)
        public UInt32 uiEmployIncome = new UInt32();
    }

    public class SWebGoodBase : ExFormatterBinary
    {
        ///物品类型1货币2经验3碎片4卡牌5装备6灵石7竞分8正义徽章9钻石10海山币
        public byte cTypeGoods = new byte();
        ///物品/英雄碎片id
        public UInt32 uiIdCsvGoods = new UInt32();
        public UInt64 m_liCnt = new UInt64();			///数量
    };

    public class SMidNightInfo : ExFormatterBinary
    {
        ///点金石次数
        public short sStoneTimes = new short();
        ///今天购买技能点次数
        public UInt32 uiSkillBuyCnt = new UInt32();
        ///剩余竞技次数
        public UInt16 usCntPk = new UInt16();
        ///剩余竞技购买次数
        public UInt16 usCntPkBuy = new UInt16();
        ///剩余爵位奖励次数
        public UInt16 usCntNotilityReward = new UInt16();
        ///本周竞技场胜场
        public UInt16 usCntWinWeek = new UInt16();
        ///本周竞技场匹配等级
        public UInt32 uiLvFightWeek = new UInt32();
        ///今天好友领取体力的次数
        public short sFriendPowerCnt = new short();
        ///购买体力次数
        public short sPowerBuyCnt = new short();
        ///已经刷新金币商店次数
        public UInt16 usCntSmoneyShop = new UInt16();
        ///已经刷新竞技场商店次数
        public UInt16 usCntPkShop = new UInt16();
        ///已经刷新海山商店次数
        public UInt16 usCntMotShop = new UInt16();
        ///海山重置次数
        public UInt32 uiMotReset = new UInt32();
        ///友情抽今天免费的次数
        public UInt32 uiFriendTodayFreeTimes = new UInt32();
        ///每日任务今天钻石抽的次数
        public UInt32 uiLuckyShopTodayQ = new UInt32();
        ///每日任务今天友情抽的次数
        public UInt32 uiLuckyShopTodayFS = new UInt32();
        ///每日任务今天普通副本次数
        public UInt32 uiFBTodayDefTimes = new UInt32();
        ///每日任务今天精英副本的次数
        public UInt32 uiFbTodayElitTimes = new UInt32();
        ///每日任务今天打挑战副本的次数
        public UInt32 uiFbTodayChallengeTimes = new UInt32();
        ///每日任务今天爬塔次数
        public UInt32 uiTowerTodayTimes = new UInt32();
        ///每日任务今天打世界boss的次数
        public UInt32 uiWorldBossTodayTimes = new UInt32();
        ///每日任务每日任务奖励
        public UInt64 luiEveryDayReward = new UInt64();
        ///每日任务竞技场次数
        public UInt32 uiPkTodayTimes = new UInt32();
        ///每日任务今天英雄试炼次数
        public UInt32 uiBossCnt = new UInt32();
        ///每日任务今天打钱打羊副本次数
        public UInt32 uiActiveEventTimes = new UInt32();
        ///每日任务升级技能
        public UInt32 uiSkillTimes = new UInt32();
        ///签到漏签的次数
        public UInt32 uiSignMiss = new UInt32();
        ///签到已签的次数
        public UInt32 uiSignSum = new UInt32();
        ///补签的次数
        public UInt32 uiSignRe = new UInt32();
        ///每日任务今天打远征的次数
        public UInt32 uiMotTimes = new UInt32();
        ///今天扫荡的次数
        public UInt32 uiSweepTimes = new UInt32();
        ///每日任务今天进入太阳井的次数
        public UInt32 uiSWTodayGotoTimes = new UInt32();
        ///每日任务今天搜索商队次数
        public UInt32 uiSearchFTodayTimes = new UInt32();
        ///签到送魂兽的日历
        public UInt32 uiSignBeastRecord = new UInt32();
        ///每日任务数据备用1:矿洞寻宝
        public UInt32 uiTodayPreData1 = new UInt32();
        ///每日任务数据备用2
        public UInt32 uiTodayPreData2 = new UInt32();
        ///每日任务数据备用3
        public UInt32 uiTodayPreData3 = new UInt32();
        ///每日任务数据备用4
        public UInt32 uiTodayPreData4 = new UInt32();
        ///每日任务数据备用5
        public UInt32 uiTodayPreData5 = new UInt32();
    };

    public class SWebEmail : ExFormatterBinary
    {
        public SWebEmail()
        {
            for( int i = 0; i < 10; ++i )
            {
                vctSWebGoodBase[ i ] = new SWebGoodBase();
            }
        }
        public string getTitle()
        {
            return System.Text.Encoding.UTF8.GetString( cTitle, 0, IndexByteToString( cTitle ) );
        }
        public string getSendName()
        {
            return System.Text.Encoding.UTF8.GetString( cNameSend, 0, IndexByteToString( cNameSend ) );
        }
        /// web email server id
        public UInt64 uiIdServer = new UInt64();
        ///网络邮件id(isLoc = 0后台控制, isLoc = 1本地csvid, isLoc = 2公会全员邮件)
        public UInt32 uiIdWebEmail = new UInt32();	///公会全员邮件时表示公会ID
        ///属于谁，玩家的id server
        public UInt32 uiRoleId = new UInt32();
        ///邮件物品
        public SWebGoodBase[] vctSWebGoodBase = new SWebGoodBase[ 10 ];
        ///过期时间
        public UInt32 uiOutTime = new UInt32();
        ///标题
        public byte[] cTitle = new byte[ 80 ];
        ///发送者名字
        public byte[] cNameSend = new byte[ 80 ];
        ///是否已经销毁
        public byte isDes = new byte();
        ///是否为本的邮件
        public byte isLoc = new byte();
        ///是否为特殊邮件（打开后特殊邮件不马上删除）
        public byte isSpe = new byte();
        ///是否打开
        public byte isOpen = new byte();
        ///限制等级
        public UInt16 usLimitLv = new UInt16();
        ///发放时间
        public UInt32 uiSendTime = new UInt32();
        ///父系统
        public byte cSystemFather;
        ///子系统
        public byte cbytecSystemSub;
    }

    public class SBoss : ExFormatterBinary
    {
        /// id server
        public UInt64 uiId = new UInt64();
        ///活动的csv id
        public UInt32 uiActiveId = new UInt32();
        ///开始时间
        public UInt32 uiBeginTime = new UInt32();
        ///结束时间
        public UInt32 uiEndTime = new UInt32();
        ///上次打时间boss的时间 + cd = 可以打的时间
        public UInt32 uiLastTime = new UInt32();
        ///今天剩余打这个boss 的次数
        public UInt32 uiCnt = new UInt32();
        ///第几个世界boss 第一个是0，第二个是1
        public UInt32 uiNum = new UInt32();
        ///当前世界boss的等级
        public UInt16 uiLv = new UInt16();
        ///当前血量
        public UInt64 uiHpCur = new UInt64();
        ///评价
        public UInt32 uiValue = new UInt32();
        ///释放手动技能的次数,评价用的
        public UInt32 uiSkillTimes = new UInt32();
        ///当前剩余重置次数
        public byte ucResetCnt = new byte();		///当前剩余重置次数
        ///累计攻击这个boss的次数
        public UInt32 uiTotalCnt = new UInt32();
    }

    public class SBossValue : ExFormatterBinary
    {
        ///总计伤害
        public UInt32 uiAtk = new UInt32();
        ///决胜场次
        public UInt32 uiCnt = new UInt32();
        ///技巧
        public UInt32 uiSkillTimes = new UInt32();
    }

    public class SBossHp : ExFormatterBinary
    {
        /// 满血
        public UInt64 uiMaxHp = new UInt64();
        ///本次伤害
        public UInt64 uiHarmCur = new UInt64();
        ///之前伤害统计
        public UInt64 uiHarmPre = new UInt64();
    }

    public class SLeadMot : ExFormatterBinary
    {
        ///当前所在关卡, 从1开始
        public UInt32 uiNum = new UInt32();
        ///要领的关卡宝箱奖励从1开始 跟关卡一一对应
        public UInt32 uiBoxNum = new UInt32();
        ///今天已经重置的次数
        public UInt32 uiReset = new UInt32();
        ///远征币
        public UInt32 uiMotMoney = new UInt32();
        ///助战币
        public UInt32 uiMotFtMoney = new UInt32();
    }

    public class SMotSkill : ExFormatterBinary
    {
        public UInt32 uiIdSkill = new UInt32();
        ///技能剩余cd 1是100% 
        public float fPorSkill = new float();
    }

    public class SMotTeamPet : ExFormatterBinary
    {
        public SMotTeamPet()
        {
            for( int i = 0; i < 5; ++i )
            {
                sMotSkill[ i ] = new SMotSkill();
            }
        }
        /// 如果是玩家，卡牌的id server
        public UInt64 uiIdPet = new UInt64();
        ///卡牌的csv id
        public UInt32 uiIdCsvPet = new UInt32();
        ///卡牌血量百分比
        public float fHp = new float();
        ///星级
        public UInt32 uiStar = new UInt32();
        ///手动释放技能id
        public UInt32 uiIdCsvSkillHand = new UInt32();
        ///所有技能
        public SMotSkill[] sMotSkill = new SMotSkill[ 5 ];
        ///装备
        public UInt32[] uiIdEquip = new UInt32[ 6 ];
        ///进阶石
        public UInt32 uiIdStone = new UInt32();
    }

    public class SMotBeastEnemy : ExFormatterBinary
    {
        public SMotBeast SMotBeastBase = new SMotBeast();
        ///魂兽csvid
        public UInt32 uiIdCsvBeast = new UInt32();
        ///魂兽经验
        public UInt64 luiExp = new UInt64();
        ///魂兽装备
        public UInt32[] vctuiIdEquip = new UInt32[ 3 ];
    }

    /// 远征的敌人队伍信息
    public class SMotTeam : ExFormatterBinary
    {
        public SMotTeam()
        {
            for( int i = 0; i < 4; ++i )
            {
                vctSPet[ i ] = new SMotTeamPet();
            }
        }
        public string GetName()
        {
            return System.Text.Encoding.UTF8.GetString( cName, 0, IndexByteToString( cName ) );
        }
        ///这只队伍的id server
        public UInt64 uiIdTeam = new UInt64();
        ///敌人玩家的的role id
        public UInt32 uiRoleId = new UInt32();
        /// 名字
        public byte[] cName = new byte[ 32 ];

        ///是否为机器人 0：否 1：是
        public byte isBox = new byte();
        ///场景id
        public UInt32 uiSceneId = new UInt32();
        ///队伍对应的关卡从1开始
        public UInt32 uiLocTeam = new UInt32();
        ///如果不是机器人,竞技场队伍的战斗力, 客户端没用，服务器用来排序的
        public UInt32 uiFight = new UInt32();
        ///卡牌的等级
        public UInt16 uiLv = new UInt16();
        ///随机出来的buffer
        public UInt32[] vctBufferIds = new UInt32[ 3 ];
        ///购买的npc buffer id
        public UInt32 uiBufferId = new UInt32();
        /// 累计战斗次数
        public UInt32 uiPkCnt = new UInt32();
        /// 星级
        public UInt32 uiStar = new UInt32();
        ///队长卡牌信息
        public SMotTeamPet sLead = new SMotTeamPet();
        ///队员卡牌信息
        public SMotTeamPet[] vctSPet = new SMotTeamPet[ 4 ];
        ///魂兽信息
        public SMotBeastEnemy SBeast = new SMotBeastEnemy();
    }

    /// 玩家的卡牌信息
    public class SMotPet : ExFormatterBinary
    {
        ///卡牌的id server
        public UInt64 uiIdPet = new UInt64();
        ///卡牌剩余血量的百分比
        public float fHp = new float();
        ///卡牌的技能cd百分比
        public float[] vctfPropSkill = new float[ 5 ];
        ///上次出战场次号, 暂时不用
        public UInt32 uiNumLastFight = new UInt32();
    }

    ///海山魂兽信息
    public class SMotBeast : ExFormatterBinary
    {
        ///魂兽的id server
        public UInt64 uiIdBeast = new UInt64();
        ///魂兽怒气百分比
        public float fAnger = new float();
    }

    /// 远征佣兵信息
    public class SMotHelper : ExFormatterBinary
    {
        public string GetName()
        {
            return System.Text.Encoding.UTF8.GetString( cName, 0, IndexByteToString( cName ) );
        }
        /// 这个佣兵的id server
        public UInt32 uiRoleId = new UInt32();
        /// 名字
        public byte[] cName = new byte[ 32 ];
        /// 是否在阵法上
        public byte cIsInTeam = new byte();
        /// 雇佣兵花的钱
        public UInt32 uiSMoney = new UInt32();
        /// 战斗力
        public UInt32 uiFight = new UInt32();
        ///卡牌信息
        public SPetInfo sPetInfo = new SPetInfo();
    }

    ///海山奖励关信息start
    /// 每日信息
    public class stMotPkUnitInfoUI : ExFormatterBinary
    {
        ///防守英雄
        public UInt32 m_uiIdCsvPet = new UInt32();
        // 防守英雄的星级
        public UInt32 m_uiPetStar = new UInt32();
        // 防守玩家的等级
        public UInt16 m_usLevel = new UInt16();
        ///排名
        public UInt16 m_usRank = new UInt16();
        ///胜利次数
        public UInt16 m_usWin = new UInt16();
        ///防守次数
        public UInt16 m_usPked = new UInt16();
    };
    //
    public class stMotPkPetInfo : ExFormatterBinary
    {
        public UInt64 m_luiIdPet = new UInt64();
        public UInt32 m_uiIdCsvPet = new UInt32();
        public UInt32 m_uiStar = new UInt32();
        public UInt64 m_luiExp = new UInt64();
        /// 技能
        public UInt32[] m_uiIdSkill = new UInt32[ 5 ];
        ///手动释放的技能
        public UInt32 m_uiIdCsvHandSkill = new UInt32();
        // 装备
        public UInt32[] m_luiIdEquip = new UInt32[ 6 ];
        ///防守BufferID
        public UInt32 m_uiGuardBufferID = new UInt32();
    };
    //敌方NPC信息
    public class stMotPkEnemyInfo : ExFormatterBinary
    {
        public string GetName()
        {
            return System.Text.Encoding.UTF8.GetString( m_sRoleName, 0, IndexByteToString( m_sRoleName ) );
        }

        public void setName( string psName )
        {
            byte[] nameByte = System.Text.Encoding.UTF8.GetBytes( psName );
            for( int i = 0; i < nameByte.Length; i++ )
            {
                m_sRoleName[ i ] = nameByte[ i ];
            }
        }

        public UInt32 m_uiIdMaster = new UInt32();
        public byte[] m_sRoleName = new byte[ 32 ];
        public stMotPkPetInfo m_PetInfo = new stMotPkPetInfo();
        public UInt16 m_usRoleLevel = new UInt16();
        ///场景id
        public UInt32 m_uiSceneId = new UInt32();
    };
    // 客户端显示的排行榜信息
    public class stMotPkRankInfo : ExFormatterBinary
    {
        public string GetName()
        {
            return System.Text.Encoding.UTF8.GetString( m_sRoleName, 0, IndexByteToString( m_sRoleName ) );
        }

        public void setName( string psName )
        {
            byte[] nameByte = System.Text.Encoding.UTF8.GetBytes( psName );
            for( int i = 0; i < nameByte.Length; i++ )
            {
                m_sRoleName[ i ] = nameByte[ i ];
            }
        }

        public UInt32 m_uiIdMaster = new UInt32();
        public byte[] m_sRoleName = new byte[ 32 ];
        public UInt32 m_uiIdCsvPet = new UInt32();
        public UInt32 m_uiStarPet = new UInt32();
        public UInt16 m_usWin = new UInt16();
        public UInt16 m_usPked = new UInt16();
        public UInt32 m_uiRankNum = new UInt32();
    };

    ///海山奖励关信息end

    /// <summary>
    /// 护送聊天框的基本信息
    /// </summary>
    public class SProtectItem : ExFormatterBinary
    {
        /// 护送的id
        public UInt64 uiProtectId = new UInt64();
        /// 护送的sessionid
        public UInt64 uiProtectSessionId = new UInt64();
        /// 0 金币 1经验 2钻石
        public byte cProtectType = new byte();
        ///0小 1中 2大
        public byte cProectType2 = new byte();
    }

    /// 聊天的基本信息
    public class SChatItem : ExFormatterBinary
    {
        public string GetName()
        {
            return System.Text.Encoding.UTF8.GetString( cName, 0, IndexByteToString( cName ) );
        }

        public string GetContent()
        {
            return System.Text.Encoding.UTF8.GetString( cContent, 0, IndexByteToString( cContent ) );
        }

        public string GetNameDst()
        {
            return System.Text.Encoding.UTF8.GetString( cNameDst, 0, IndexByteToString( cNameDst ) );
        }

        public void setName( string psName )
        {
            byte[] nameByte = System.Text.Encoding.UTF8.GetBytes( psName );
            for( int i = 0; i < nameByte.Length; i++ )
            {
                cName[ i ] = nameByte[ i ];
            }
        }

        public void setContent( string psContent )
        {
            byte[] contentByte = System.Text.Encoding.UTF8.GetBytes( psContent );
            for( int i = 0; i < contentByte.Length; i++ )
            {
                cContent[ i ] = contentByte[ i ];
            }
        }

        public void setNameDst( string psNameDst )
        {
            byte[] nameByte = System.Text.Encoding.UTF8.GetBytes( psNameDst );
            for( int i = 0; i < nameByte.Length; i++ )
            {
                cNameDst[ i ] = nameByte[ i ];
            }
        }

        /// <summary>
        /// 聊天的serverid
        /// </summary>
        public UInt64 ulChatID = new UInt64();

        ///发送聊天信息的角色id
        public UInt32 uiSrcRoleId = new UInt32();
        /// <summary>
        /// 队伍级别
        /// </summary>
        public UInt16 usTeamLv = new ushort();
        /// <summary>
        /// 队长的csvid
        /// </summary>
        public UInt32 uiTeamLeaderCsvID = new UInt32();
        ///发送聊天信息的角色name
        public byte[] cName = new byte[ 32 ];
        ///私聊或者指定人帮助护送的玩家id
        public UInt32 uiDstRoleId = new UInt32();
        ///聊天的时间
        public UInt32 uiTalkTime = new UInt32();
        /// 聊天的类型 0世界 1公会 2私聊 3所有的好友 4邀请所有好友(商队) 5邀请的个人(商队) 6邀请公会所有人（商队） 7Level
        public byte cType = new byte();
        /// 帮派id 帮派聊天和护送用
        public UInt32 uiGangServerId = new UInt32();
        /// 护送相关的信息
        public SProtectItem sProtectItem = new SProtectItem();
        /// 聊天的内容
        public byte[] cContent = new byte[ 181 ];

        public byte[] cNameDst = new byte[ 32 ];

    }

    /// <summary>
    /// 护送玩家基本信息
    /// </summary>
    public class SEscortInfoBase : ExFormatterBinary
    {
        ///玩家id
        public UInt32 uiIdMaster = new UInt32();
        ///面包数
        public UInt32 uiCntBread = new UInt32();
        ///面包小于上限的时间戳
        public UInt32 uiBreadLessTime = new UInt32();
        ///今天搜索商队次数
        public UInt32 uiCntSearch = new UInt32();
        ///正在进攻的商队id
        public UInt64 uiIdEGF = new UInt64();
        ///最近搜索到的商队id
        public UInt64 uiIdEGS = new UInt64();
        ///搜索到的商队sessionid
        public UInt32 uiIdSessionSearch = new UInt32();
        /// 今天购买面包的次数
        public UInt32 uiCntBreadBuy = new UInt32();
    }

    /// <summary>
    /// 护送商队基本信息
    /// </summary>
    public class SEscortGroupBase : ExFormatterBinary
    {
        ///守护的商队id
        public UInt64 luiIdEG = new UInt64();
        ///商队类型0：金小～8：钻石大
        public UInt32 uiEscortGroupType = new UInt32();
        ///该商队开始时间
        public UInt32 uiBeginTime = new UInt32();
        ///序列id，用此id来确认关于此商队的请求是否有效
        public UInt32 uiSessionId = new UInt32();
        ///0运输中1结束2被击败
        public byte cType = new byte();
    }

    /// <summary>
    /// 搜索的商队/队伍基本信息
    /// </summary>
    public class SEscortSearchBase : ExFormatterBinary
    {
        ///id商队/id队伍
        public UInt64 luiId = new UInt64();
        ///session id来确定本次请求是否有效
        public UInt32 uiSessionId = new UInt32();
    }

    /// <summary>
    ///守护商队显示基本信息
    /// </summary>
    public class SEscortTeamShowBase : ExFormatterBinary
    {
        ///守护队伍id
        public UInt64 luiIdETid = new UInt64();
        ///序列id，用此id来确认关于此队伍的请求是否有效
        public UInt32 uiSessionId = new UInt32();
        ///队长server id
        public UInt64 luiIdLeadServer = new UInt64();
        ///队长csvid
        public UInt32 uiIdLead = new UInt32();
        ///队伍开始时间
        public UInt32 uiBeginTime = new UInt32();
        ///是否领奖1已领0未领
        public byte cIsGetReward = new byte();
        ///0未打，1正在打，2已打败
        public byte cType = new byte();
        ///该队伍团队等级
        public short usLv = new short();
        ///该队伍战斗力
        public UInt32 uiFightValue = new UInt32();
    };
    /// <summary>
    ///守护商队
    /// </summary>
    public class SEscortGroup : ExFormatterBinary
    {
        public SEscortGroup()
        {
            for( int i = 0; i < 3; ++i )
            {
                vctTeamShow[ i ] = new SEscortTeamShowBase();
            }
        }
        /// 基本信息
        public SEscortGroupBase SInfoBase = new SEscortGroupBase();
        ///搜索到时间
        public UInt32 uiFindTime = new UInt32();
        ///开始战斗时间
        public UInt32 uiFightTime = new UInt32();
        ///结束或者被击溃时间
        public UInt32 uiOverTime = new UInt32();
        ///是否摧毁
        public byte cIsDestory = new byte();
        ///服务器id
        public UInt32 uiIdServer = new UInt32();
        ///商队中最强队伍战斗力
        public UInt32 uiFightValue = new UInt32();
        ///队伍显示信息
        public SEscortTeamShowBase[] vctTeamShow = new SEscortTeamShowBase[ 3 ];
    };

    /// <summary>
    ///护送队伍粗略信息
    /// </summary>
    public class SEscortTeamEasy : ExFormatterBinary
    {
        ///队伍id
        public UInt64 uiIdEscortTeam = new UInt64();
        ///主人Id
        public UInt32 uiIdMaster = new UInt32();
        ///队伍类型(客户端无用)
        public byte cType = new byte();
        ///队长id
        public UInt64 uiIdLead = new UInt64();
        ///其他四个队员
        public UInt64[] uiIdPet = new UInt64[ 4 ];
        ///魂兽id
        public UInt64 luiIdBeast = new UInt64();
    };

    /// <summary>
    ///战斗服务器护送队伍信息
    /// </summary>
    public class SEscortRoleInfoPks : ExFormatterBinary
    {
        public SEscortRoleInfoPks()
        {
            for( int i = 0; i < 5; ++i )
            {
                vctPetInfo[ i ] = new SPetInfo();
            }
        }

        public string GetName()
        {
            return System.Text.Encoding.UTF8.GetString( cName, 0, IndexByteToString( cName ) );
        }

        public void setName( string psName )
        {
            byte[] nameByte = System.Text.Encoding.UTF8.GetBytes( psName );
            for( int i = 0; i < nameByte.Length; i++ )
            {
                cName[ i ] = nameByte[ i ];
            }
        }
        ///队伍拥有者name
        public byte[] cName = new byte[ 32 ];
        ///守护队伍id
        public UInt64 luiIdETid = new UInt64();
        ///守护的商队id
        public UInt64 luiIdEG = new UInt64();
        ///爵位
        public byte cNobility = new byte();
        ///队伍拥有者id
        public UInt32 uiIdMaster = new UInt32();
        ///服务器id
        public UInt32 uiIdServer = new UInt32();
        ///宠物信息
        public SPetInfo[] vctPetInfo = new SPetInfo[ 5 ];
        ///队伍信息
        public SEscortTeamEasy Team = new SEscortTeamEasy();
        ///魂兽
        public SBeast Beast = new SBeast();
        ///该队伍开始时间
        public UInt32 uiBeginTime = new UInt32();
        ///战斗力
        public UInt32 uiFightValue = new UInt32();
        ///玩家经验
        public UInt64 luiExp = new UInt64();
        ///vpi等级
        public UInt32 uiVipLv = new UInt32();
        ///序列id，用此id来确认关于此队伍的请求是否有效
        public UInt32 uiSessionId = new UInt32();
        ///是否摧毁
        public byte cIsDestory = new byte();
    };
    /// <summary>
    ///防守记录
    /// </summary>
    public class SEscortLog : ExFormatterBinary
    {
        ///防守记录id
        public UInt64 luiIdServer = new UInt64();
        ///玩家id
        public UInt32 uiIdMaster = new UInt32();
        ///攻击时间
        public UInt32 uiFightTime = new UInt32();
        ///敌人服务器id
        public UInt32 uiIdServerEnemy = new UInt32();
        ///录像id
        public UInt64 luiIdVideo = new UInt64();
        ///是否摧毁（客户端无用）
        public byte cIsDestory = new byte();
        ///1胜利0失败
        public byte cIsWin = new byte();
        ///是否获取面包
        public byte cIsGetBread = new byte();
        ///敌人名字
        public byte[] vctNameEnemy = new byte[ 32 ];
        ///工会名字
        public byte[] vctNamefaction = new byte[ 64 ];
        public string GetNameEnemy()
        {
            return System.Text.Encoding.UTF8.GetString( vctNameEnemy, 0, IndexByteToString( vctNameEnemy ) );
        }
        public static int IndexByteToString( byte[] arr )
        {
            int index = ( GUtil.indexOf<byte>( 0, arr ) );
            if( index == -1 )
                index = arr.Length;
            else
                index = index + 0;
            return index;
        }
    };

    public class SEscortTeamUpdate : ExFormatterBinary
    {
        ///被布阵的商队
        public SEscortSearchBase SGroup = new SEscortSearchBase();
        ///被布阵的队伍(第一次布阵填0)
        public SEscortSearchBase STeam = new SEscortSearchBase();
        ///布阵位置0～2
        public UInt32 uiLoc = new UInt32();
        ///队长id
        public UInt64 uiIdLead = new UInt64();
        ///其他四个队员
        public UInt64[] uiIdPet = new UInt64[ 4 ];
        ///魂兽id
        public UInt64 luiIdBeast = new UInt64();
    };

    public class SEscortTeamPet : ExFormatterBinary
    {
        public SEscortTeamPet()
        {
            for( int i = 0; i < 5; ++i )
            {
                sMotSkill[ i ] = new SMotSkill();
            }
        }
        /// 如果是玩家，卡牌的id server
        public UInt64 uiIdPet = new UInt64();
        ///卡牌的csv id
        public UInt32 uiIdCsvPet = new UInt32();
        ///卡牌血量百分比
        public float fHp = new float();
        ///星级
        public UInt32 uiStar = new UInt32();
        ///手动释放技能id
        public UInt32 uiIdCsvSkillHand = new UInt32();
        ///所有技能
        public SMotSkill[] sMotSkill = new SMotSkill[ 5 ];

        ///装备信息
        public UInt32[] vctluiIdEquip = new UInt32[ 6 ];
        ///进阶石
        public UInt32 uiIdStone = new UInt32();
        ///等级
        public UInt64 luiExp = new UInt64();

    };

    public class SEscortBeastEnemy : ExFormatterBinary
    {
        ///魂兽的id server
        public UInt64 uiIdBeast = new UInt64();
        ///魂兽怒气百分比
        public float fAnger = new float();
        ///魂兽csvid
        public UInt32 uiIdCsvBeast = new UInt32();
        ///装备信息
        public UInt32[] vctluiIdEquip = new UInt32[ 3 ];
        ///魂兽经验
        public UInt64 luiExp = new UInt64();
    };

    public class SEscortPetSkill : ExFormatterBinary
    {
        ///卡牌id
        public UInt64 luiIdPet = new UInt64();
        ///1敌人1己方
        public byte cIsEnemy = new byte();
        public float[] m_vctfProSkill = new float[ 5 ];
    };

    public class SEscortTeamFight : ExFormatterBinary
    {
        public SEscortTeamFight()
        {
            for( int i = 0; i < 4; ++i )
            {
                VctSPet[ i ] = new SEscortTeamPet();
            }
        }
        public string GetName()
        {
            return System.Text.Encoding.UTF8.GetString( cName, 0, IndexByteToString( cName ) );
        }

        public void setName( string psName )
        {
            byte[] nameByte = System.Text.Encoding.UTF8.GetBytes( psName );
            for( int i = 0; i < nameByte.Length; i++ )
            {
                cName[ i ] = nameByte[ i ];
            }
        }
        ///守护队伍id
        public UInt64 luiIdET = new UInt64();
        ///守护的商队id
        public UInt64 luiIdEG = new UInt64();
        ///队伍所在服务器id
        public UInt32 uiIdServer = new UInt32();
        ///敌人玩家id
        public UInt32 uiIdMaster = new UInt32();
        ///敌人名字
        public byte[] cName = new byte[ 32 ];
        ///敌人守护队伍队长（英雄）
        public SEscortTeamPet sLead = new SEscortTeamPet();
        ///敌人守护队伍队员（英雄）
        public SEscortTeamPet[] VctSPet = new SEscortTeamPet[ 4 ];
        ///敌人魂兽
        public SEscortBeastEnemy SBeast = new SEscortBeastEnemy();
        ///经验
        public UInt64 luiExp = new UInt64();
        ///爵位
        public byte cNobility = new byte();
        ///0未打，1正在打，2已打败
        public byte cType = new byte();
    };

    public class SActInfo : ExFormatterBinary
    {
        public string getActName()
        {
            return System.Text.Encoding.UTF8.GetString( cActName, 0, IndexByteToString( cActName ) );
        }

        public string getActDes()
        {
            return System.Text.Encoding.UTF8.GetString( cActDes, 0, IndexByteToString( cActDes ) );
        }

        ///类型
        public UInt16 usType = new UInt16();
        ///开始时间
        public UInt32 uiStartTime = new UInt32();
        ///结束时间
        public UInt32 uiEndTime = new UInt32();
        ///活动条件，根据类型解析
        public byte[] arrActCondition = new byte[ 12 ];
        ///活动名字(20个汉字)
        public byte[] cActName = new byte[ 64 ];
        ///活动描述(50个汉字)
        public byte[] cActDes = new byte[ 151 ];
        ///角色完成情况
        public UInt32 uiPersonInfo1 = new UInt32();
        ///角色领奖情况或当前数据	
        public UInt32 uiPersonInfo2 = new UInt32();
    }

    public class SActReward : ExFormatterBinary
    {
        public UInt32 uiCsvId = new UInt32();
        ///3碎片4卡牌5装备
        public byte cType = new byte();
        ///数量
        public Int32 Cnt = new Int32();
        ///奖励属性标志
        public UInt32 uiFlag = new UInt32();
        ///活动类型
        public UInt16 usActType = new UInt16();
    }

    public class SActRank : ExFormatterBinary
    {
        public string getRoleName()
        {
            return System.Text.Encoding.UTF8.GetString( cRoleName, 0, IndexByteToString( cRoleName ) );
        }

        public UInt32 uiRoleID = new UInt32();
        public byte[] cRoleName = new byte[ 32 ];
        public UInt64 uiValue = new UInt64();
        public UInt32 uiRank = new UInt32();
    };

    public class SRankClientInfo : ExFormatterBinary
    {
        public string getName()
        {
            return System.Text.Encoding.UTF8.GetString( cName, 0, IndexByteToString( cName ) );
        }
        public UInt32 uiID = new UInt32();
        public byte[] cName = new byte[ 32 ];
        public UInt32 uiPic = new UInt32();
        public UInt16 usLevel = new UInt16();				///公会榜时该字段特殊意义表示背景框ID
        public UInt64 uiValue = new UInt64();
        public UInt16 usRank = new UInt16();
        public UInt16 usLastRank = new UInt16();			///上次排名
    };

    public class SGUActiveRankDetail : ExFormatterBinary
    {
        public string getGUName()
        {
            return System.Text.Encoding.UTF8.GetString( cName, 0, IndexByteToString( cName ) );
        }
        public string getLeadName()
        {
            return System.Text.Encoding.UTF8.GetString( cLeadName, 0, IndexByteToString( cLeadName ) );
        }
        public string getAnnouncement()
        {
            return System.Text.Encoding.UTF8.GetString( cAnnouncement, 0, IndexByteToString( cAnnouncement ) );
        }
        public UInt32 uiID = new UInt32();
        public byte[] cName = new byte[ 32 ];
        public UInt32 uiPic = new UInt32();
        public UInt32 uiPicFrame = new UInt32();
        public UInt16 usRank = new UInt16();
        public byte[] cLeadName = new byte[ 32 ];
        public byte ucCurMemberCount = new byte();		//公会当前人数
        public byte[] cAnnouncement = new byte[ 96 ];	//公告
    };

    public class SAllPetFightRankDetail : ExFormatterBinary
    {
        public string getRoleName()
        {
            return System.Text.Encoding.UTF8.GetString( cName, 0, IndexByteToString( cName ) );
        }
        public string getGUName()
        {
            return System.Text.Encoding.UTF8.GetString( cGUName, 0, IndexByteToString( cGUName ) );
        }
        public UInt32 uiRoleID = new UInt32();
        public byte[] cName = new byte[ 32 ];
        public UInt32 uiPic = new UInt32();
        public UInt16 usLevel = new UInt16();
        public UInt16 usRank = new UInt16();
        public byte[] cGUName = new byte[ 32 ];
    };

    public class SLevelRankDetail : ExFormatterBinary
    {
        public string getRoleName()
        {
            return System.Text.Encoding.UTF8.GetString( cName, 0, IndexByteToString( cName ) );
        }
        public string getGUName()
        {
            return System.Text.Encoding.UTF8.GetString( cGUName, 0, IndexByteToString( cGUName ) );
        }
        public UInt32 uiRoleID = new UInt32();
        public byte[] cName = new byte[ 32 ];
        public UInt32 uiPic = new UInt32();
        public UInt16 usLevel = new UInt16();
        public UInt16 usRank = new UInt16();
        public byte[] cGUName = new byte[ 32 ];
    };

    // 客户端显示的排行榜信息
    public class SRecentServerInfo : ExFormatterBinary
    {
        public string GetName()
        {
            return System.Text.Encoding.UTF8.GetString( m_sRoleName, 0, IndexByteToString( m_sRoleName ) );
        }

        public void setName( string psName )
        {
            byte[] nameByte = System.Text.Encoding.UTF8.GetBytes( psName );
            for( int i = 0; i < nameByte.Length; i++ )
            {
                m_sRoleName[ i ] = nameByte[ i ];
            }
        }

        public UInt32 m_uiServerID = new UInt32();
        public UInt32 m_uiServerTime = new UInt32();

        public UInt32 m_uiMasterID = new UInt32();
        public byte[] m_sRoleName = new byte[ 32 ];
        public UInt32 m_uiIdCsvPet = new UInt32();
        public UInt64 m_luiExp = new UInt64();
        ///Vip的经验等级
        public UInt32 m_uiVipExp = new UInt32();
    };

    ///封停基本信息
    public class SStopBaseInfo : ExFormatterBinary
    {
        public string GetReason()
        {
            return System.Text.Encoding.UTF8.GetString( m_cReason, 0, IndexByteToString( m_cReason ) );
        }

        public void setReason( string psReason )
        {
            byte[] ReasonByte = System.Text.Encoding.UTF8.GetBytes( psReason );
            for( int i = 0; i < ReasonByte.Length; i++ )
            {
                m_cReason[ i ] = ReasonByte[ i ];
            }
        }

        ///是否封停(1已经封停，0没封停)
        public UInt32 uiIsStop = new UInt32();
        ///封停解除时间
        public UInt32 uiStopTime = new UInt32();
        ///封停原因
        public byte[] m_cReason = new byte[ 200 ];
    };

    ///封停基本信息
    public class SMessageScreenBase : ExFormatterBinary
    {
        public string GetReason()
        {
            return System.Text.Encoding.UTF8.GetString( m_cvctInfo, 0, IndexByteToString( m_cvctInfo ) );
        }

        public void setReason( string pstr )
        {
            byte[] pbtte = System.Text.Encoding.UTF8.GetBytes( pstr );
            for( int i = 0; i < pbtte.Length; i++ )
            {
                m_cvctInfo[ i ] = pbtte[ i ];
            }
        }

        ///跑马灯信息id
        public UInt32 uiIdMessage = new UInt32();
        ///开始时间
        public UInt32 uiTimeBegin = new UInt32();
        ///结束时间
        public UInt32 uiTimeOver = new UInt32();
        ///间隔时间
        public UInt32 uiTimeRepet = new UInt32();
        ///跑马灯内容
        public byte[] m_cvctInfo = new byte[ 800 ];
    };

    public class SBgndDrop : ExFormatterBinary
    {
        ///掉率活动类型
        public UInt32 uiType = new UInt32();
        ///掉落活动id
        public UInt32 uiIdDorp = new UInt32();
        ///开始时间
        public UInt32 uiBeginTime = new UInt32();
        ///结束时间
        public UInt32 uiOverTime = new UInt32();
        ///掉落倍率
        public UInt32 uiMul = new UInt32();
    }

    public class SPetShowBase : ExFormatterBinary
    {
        ///csv id
        public UInt32 uiIdCsv = new UInt32();
        ///星级
        public UInt32 uiStar = new UInt32();
    };

}
