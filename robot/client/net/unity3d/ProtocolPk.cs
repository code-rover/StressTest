using System;

using System.IO;

using ExFormatter;
using System.Diagnostics;

namespace net.unity3d
{
	[Serializable()]
    public class C2RM_PK_INFO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_PK_INFO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_PK_INFO();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            return false;
        }

        public byte[] getBuffer()
        {
            byte[] buffer = ToByteArray();
            return buffer;
        }
		
        public UInt32 uiListen = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_PK_INFO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_PK_INFO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_PK_INFO();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            FromByteArrayNew(Buffer, this);
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }

        public UInt32 uiListen = new UInt32();
		public int iResult = new int();
		///爵位
		public byte cLvNobility = new byte();
		///匹配等级
		public UInt32 uiLvFight = new UInt32();
		///竞技积分
		public UInt32 uiScoreFight = new UInt32();
		///声望
		public UInt32 uiPrestige = new UInt32();
		///本周胜场
		public UInt16 usCntWinWeek = new UInt16();
		///剩余竞技次数
		public UInt16 usCntPk = new UInt16();
		///剩余竞技购买次数
		public UInt16 usCntPkBuy = new UInt16();
		///上周竞技场排名
		public UInt16 usRankLastWeek = new UInt16();
		///本周竞技场排名
		public UInt16 usRankThisWeek = new UInt16();				
    };
	
	[Serializable()]
    public class C2RM_PK_RANK_LIST : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_PK_RANK_LIST;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_PK_RANK_LIST();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            return false;
        }

        public byte[] getBuffer()
        {
            byte[] buffer = ToByteArray();
            return buffer;
        }
		
        public UInt32 uiListen = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_PK_RANK_LIST : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_PK_RANK_LIST;

        private void init(int len)
        {
			for(int i = 0; i < 4; ++i)
			{
				SMyRankInfo.vctPet[i] = new SPkPetInfo();
			}
			
            if (len >= 0)
            {
                vctPkRankList = new SPkRankListBase[len];
                for (int i = 0; i < len; i++)
                {
                    vctPkRankList[i] = new SPkRankListBase();
					for(int m = 0; m < 4; ++m)
					{
						vctPkRankList[i].vctPet[m] = new SPkPetInfo();
					}
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_PK_RANK_LIST();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            MemoryStream msBuff = new MemoryStream(Buffer);
            BinaryReader br = new BinaryReader(msBuff);
            UInt32 len = br.ReadUInt32();
            init((int)len);
            MemoryStream realBuff = new MemoryStream(Buffer, 4, Buffer.Length - 4);
            ///USerialize.ConvertBytesToClass(realBuff);
            byte[] realData = realBuff.ToArray();
            FromByteArrayNew(realData, this);
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }

		public UInt32 uiListen = new UInt32();
		public int iResult = new int();
		////是否为本周排名1是0否
		public byte cIsThisWeek = new byte();
		////玩家排名信息
		public SPkRankListBase SMyRankInfo = new SPkRankListBase();
		public SPkRankListBase[] vctPkRankList;
    };
	
	[Serializable()]
    public class C2RM_ADD_PK_CNT : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ADD_PK_CNT;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_ADD_PK_CNT();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            return false;
        }

        public byte[] getBuffer()
        {
            byte[] buffer = ToByteArray();
            return buffer;
        }
		
        public UInt32 uiListen = new UInt32();
		public int iCnt = new int();
    };
	
	[Serializable()]
    public class RM2C_ADD_PK_CNT : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ADD_PK_CNT;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_ADD_PK_CNT();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            FromByteArrayNew(Buffer, this);
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }

        public UInt32 uiListen = new UInt32();
		public int iResult = new int();
		///消耗人民币
		public int iQMoneyCost = new int();	
		///剩余竞技次数
		public UInt16 usCntPk = new UInt16();		
		///剩余竞技购买次数
		public UInt16 usCntPkBuy = new UInt16();		
    };
	
	[Serializable()]
    public class C2RM_UP_LV_NOBILITY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_UP_LV_NOBILITY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_UP_LV_NOBILITY();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            return false;
        }

        public byte[] getBuffer()
        {
            byte[] buffer = ToByteArray();
            return buffer;
        }
		
        public UInt32 uiListen = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_UP_LV_NOBILITY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_UP_LV_NOBILITY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_UP_LV_NOBILITY();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            FromByteArrayNew(Buffer, this);
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }

        public UInt32 uiListen = new UInt32();
		public int iResult = new int();
		///爵位
		public byte cLvNobility = new byte();
		///消耗游戏币
		public Int64 liSMoenyCost = new Int64();
		///消耗人民币
		public int iQMoneyCost = new int();		
    };
	
	[Serializable()]
    public class C2RM_GET_REWARD_NOBILITY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_GET_REWARD_NOBILITY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_GET_REWARD_NOBILITY();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            return false;
        }

        public byte[] getBuffer()
        {
            byte[] buffer = ToByteArray();
            return buffer;
        }
		
        public UInt32 uiListen = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_GET_REWARD_NOBILITY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_GET_REWARD_NOBILITY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_GET_REWARD_NOBILITY();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            FromByteArrayNew(Buffer, this);
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }

        public UInt32 uiListen = new UInt32();
		public int iResult = new int();
		///获得游戏币
		public Int64 liSMoney = new Int64();
		///获得人民币
		public int iQMoney = new int();
		///获得万能碎片
		public int iCntPiece = new int();	
    };
	
	[Serializable()]
    public class C2RM_MATE_PK : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MATE_PK;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_MATE_PK();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            return false;
        }

        public byte[] getBuffer()
        {
            byte[] buffer = ToByteArray();
            return buffer;
        }
		
        public UInt32 uiListen = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_MATE_PK : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MATE_PK;
		
		RM2C_MATE_PK()
		{
			for(int i = 0; i < 5; ++i)
			{
				vctPetInfo[i] = new SPetInfo();
			}
		}

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_MATE_PK();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            FromByteArrayNew(Buffer, this);
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }

		///预计活动竞技积分
		public UInt32 uiGetLvFight = new UInt32();
		///敌人排名
		public UInt32 uiRankEnemy = new UInt32();
		///帮派名字
		public byte[]	sGangName = new byte[32];
        public UInt32 uiListen = new UInt32();
		public int iResult = new int();
		///剩余竞技次数
		public UInt16 usCntPk = new UInt16();
		///对手server id
		public UInt32 uiIdEnemy = new UInt32();
		///昵称
		public byte[]	sEnemyName = new byte[32];
		///对手宠物信息
		public SPetInfo[] vctPetInfo = new SPetInfo[5];
		///对手队伍信息
		public STeam STeamInfo = new STeam();
		///对手魂兽信息
		public SBeast SBeastInro = new SBeast();
		///对手爵位
		public byte cNobility = new byte();
		///对手等级
		public UInt16 usLv = new UInt16();
		///对手等级
		public UInt32 uiCntPkAll = new UInt32();
		
		public string getEnemyName()
        {
            return System.Text.Encoding.UTF8.GetString(sEnemyName, 0, IndexByteToString(sEnemyName));
        }

		public string getGangName()
		{
			return System.Text.Encoding.UTF8.GetString(sGangName, 0, IndexByteToString(sGangName));
		}
    };
	
	[Serializable()]
    public class C2RM_CHECK_PK_BEGIN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_CHECK_PK_BEGIN;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_CHECK_PK_BEGIN();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            return false;
        }

        public byte[] getBuffer()
        {
            byte[] buffer = ToByteArray();
            return buffer;
        }
		
		///对手id
        public UInt32 uiIdEnemy = new UInt32();
		//战斗开始时间戳
		public UInt32 uiBeginTime = new UInt32();
		///自己怪物站位
		public UInt64[] vctIdServerPetSelf = new UInt64[9];
		///对手怪物站位
		public UInt64[] vctIdServerPetEnemy = new UInt64[9];
    };
	
	[Serializable()]
    public class C2RM_CHECK_PK_MID : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_CHECK_PK_MID;


        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_CHECK_PK_MID();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            return false;
        }

        public byte[] getBuffer()
        {
            byte[] buffer = ToByteArray();
            return buffer;
        }
		
		public int iCnt = new int();
		public byte cNum = new byte();
		///战斗验证数据
		public SFightingRound[] vctSReward;
    };
	
	[Serializable()]
    public class C2RM_CHECK_PK_OVER : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_CHECK_PK_OVER;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_CHECK_PK_OVER();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            return false;
        }

        public byte[] getBuffer()
        {
            byte[] buffer = ToByteArray();
            return buffer;
        }
		
        public UInt32 uiListen = new UInt32();
		///停止时间戳
		public float fStopTime = new float();
		///是否胜利
		public byte cIsWin = new byte();
    };
	
	[Serializable()]
    public class RM2C_CHECK_PK : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_CHECK_PK;

        public static IProtocal Create( ushort msg, HeaderBase h )
        {
            return new RM2C_CHECK_PK();
        }

        public ushort Message
        {
            get
            {
                return ( ushort ) OPCODE;
            }
        }

        public bool analysisBuffer( byte[] Buffer )
        {
            FromByteArrayNew( Buffer, this );
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }

        public UInt32 uiListen = new UInt32();
        public int iResult = new int();
        public byte cIsWin = new byte();
        ///对手server Id
        public UInt32 uiIdEnemyMaster = new UInt32();
        ///对手昵称
        public byte[] sEnemyName = new byte[ 32 ];
        //获得游戏币
        public Int64 liSMoney = new Int64();
        ///获得匹配等级
        public UInt32 uiLvFight = new UInt32();
        ///获得竞技积分
        public int iScoreFight = new int();
        ///获得声望
        public UInt32 uiPrestige = new UInt32();
        ///本周胜场
        public UInt16 usCntWinWeek = new UInt16();
        ///本周匹配等级
        public UInt32 uiLvFightWeek = new UInt32();
        //己方战斗力
        public UInt32 uiMyFightNum = new UInt32();
        //对手战斗力
        public UInt32 uiEnemyFightNum = new UInt32();

        public string getRoleName()
        {
            return System.Text.Encoding.UTF8.GetString( sEnemyName, 0, IndexByteToString( sEnemyName ) );
        }
    };
	
	[Serializable()]
    public class C2RM_UPDATE_PK_TEAM : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_UPDATE_PK_TEAM;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_UPDATE_PK_TEAM();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            return false;
        }

        public byte[] getBuffer()
        {
            byte[] buffer = ToByteArray();
            return buffer;
        }
		
        public UInt32 uiListen = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_UPDATE_PK_TEAM : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_UPDATE_PK_TEAM;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_UPDATE_PK_TEAM();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            FromByteArrayNew(Buffer, this);
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }

        public UInt32 uiListen = new UInt32();
		public int iResult = new int();	
    };
	
	[Serializable()]
    public class C2RM_PK_SHOP_BUY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_PK_SHOP_BUY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_PK_SHOP_BUY();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            return false;
        }

        public byte[] getBuffer()
        {
            byte[] buffer = ToByteArray();
            return buffer;
        }
		
        public UInt32 uiListen = new UInt32();
		///商店物品位置
		public int iLoc = new int();
    };
	
	public class RM2C_PK_SHOP_BUY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_PK_SHOP_BUY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_PK_SHOP_BUY();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            FromByteArrayNew(Buffer, this);
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }
		
		public UInt32 uiListen = new UInt32();
		public int iResult = new int();	
		///商店物品位置
		public int iLoc = new int();
		///竞技积分花费
		public int iSocreFight = new int();
		///新买的物品数量更新
		public SEquipment SEquip = new SEquipment();	
		///新买的碎片数量更新
		public SPiece SPiece = new SPiece();
		///购买的整卡
		public SPetInfo SPet = new SPetInfo();
    };
	
	[Serializable()]
    public class C2RM_REFRESH_PK_SHOP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_REFRESH_PK_SHOP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_REFRESH_PK_SHOP();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            return false;
        }

        public byte[] getBuffer()
        {
            byte[] buffer = ToByteArray();
            return buffer;
        }
		
        public UInt32 uiListen = new UInt32();
		///刷新类型0系统定时自动刷新，1竞技积分刷新
		public byte cType = new byte();
    };
	
	public class RM2C_REFRESH_PK_SHOP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_REFRESH_PK_SHOP;
		
		public RM2C_REFRESH_PK_SHOP()
        {
            for (int i = 0; i < 8; ++i )
            {
                m_vctShopGoodsp[i] = new STowerShopGood();
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_REFRESH_PK_SHOP();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            FromByteArrayNew(Buffer, this);
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }
		
		public UInt32 uiListen = new UInt32();
		public int iResult = new int();	
		///下次刷新时间
		public UInt32 uiRefreshTime = new UInt32();
		///服务器当前时间
		public UInt32 uiServerTime = new UInt32();
		///消耗竞技积分
		public int iCostScoreFight = new int();
		///今天已经刷新次数
		public int iCntRefresh = new int();	
		///商店物品信息
		public STowerShopGood[] m_vctShopGoodsp = new STowerShopGood[8];
    };
	
	[Serializable()]
    public class C2RM_GET_PK_SHOP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_GET_PK_SHOP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_GET_PK_SHOP();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            return false;
        }

        public byte[] getBuffer()
        {
            byte[] buffer = ToByteArray();
            return buffer;
        }
		
        public UInt32 uiListen = new UInt32();
    };
	
	public class RM2C_GET_PK_SHOP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_GET_PK_SHOP;
		
		public RM2C_GET_PK_SHOP()
        {
            for (int i = 0; i < 8; ++i )
            {
                m_vctShopGoodsp[i] = new STowerShopGood();
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_GET_PK_SHOP();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            FromByteArrayNew(Buffer, this);
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }
		
		public UInt32 uiListen = new UInt32();
		public int iResult = new int();	
		///下次刷新时间
		public UInt32 uiRefreshTime = new UInt32();
		///服务器当前时间
		public UInt32 uiServerTime = new UInt32();
		///今天已经刷新次数
		public int iCntRefresh = new int();			
		///商店物品信息
		public STowerShopGood[] m_vctShopGoodsp = new STowerShopGood[8];
    };
	
	
	
	[Serializable()]
    public class C2RM_MOUNTAIN_SHOP_BUY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOUNTAIN_SHOP_BUY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_MOUNTAIN_SHOP_BUY();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            return false;
        }

        public byte[] getBuffer()
        {
            byte[] buffer = ToByteArray();
            return buffer;
        }
		
        public UInt32 uiListen = new UInt32();
		///商店物品位置
		public int iLoc = new int();
    };
	
	public class RM2C_MOUNTAIN_SHOP_BUY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOUNTAIN_SHOP_BUY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_MOUNTAIN_SHOP_BUY();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            FromByteArrayNew(Buffer, this);
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }
		
		public UInt32 uiListen = new UInt32();
		public int iResult = new int();	
		///商店物品位置
		public int iLoc = new int();
		///花费
		public int iCost = new int();
		///新买的物品数量更新
		public SEquipment SEquip = new SEquipment();	
		///新买的碎片数量更新
		public SPiece SPiece = new SPiece();
		///购买的整卡
		public SPetInfo SPet = new SPetInfo();
    };
	
	[Serializable()]
    public class C2RM_REFRESH_MOUNTAIN_SHOP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_REFRESH_MOUNTAIN_SHOP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_REFRESH_MOUNTAIN_SHOP();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            return false;
        }

        public byte[] getBuffer()
        {
            byte[] buffer = ToByteArray();
            return buffer;
        }
		
        public UInt32 uiListen = new UInt32();
		///刷新类型0系统定时自动刷新，1积分刷新
		public byte cType = new byte();
    };
	
	public class RM2C_REFRESH_MOUNTAIN_SHOP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_REFRESH_MOUNTAIN_SHOP;
		
		public RM2C_REFRESH_MOUNTAIN_SHOP()
        {
            for (int i = 0; i < 8; ++i )
            {
                m_vctShopGoodsp[i] = new STowerShopGood();
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_REFRESH_MOUNTAIN_SHOP();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            FromByteArrayNew(Buffer, this);
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }
		
		public UInt32 uiListen = new UInt32();
		public int iResult = new int();	
		///下次刷新时间
		public UInt32 uiRefreshTime = new UInt32();
		///服务器当前时间
		public UInt32 uiServerTime = new UInt32();
		///消耗
		public int iCost = new int();
		///今天已经刷新次数
		public int iCntRefresh = new int();	
		///商店物品信息
		public STowerShopGood[] m_vctShopGoodsp = new STowerShopGood[8];
    };
	
	[Serializable()]
    public class C2RM_GET_MOUNTAIN_SHOP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_GET_MOUNTAIN_SHOP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_GET_MOUNTAIN_SHOP();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            return false;
        }

        public byte[] getBuffer()
        {
            byte[] buffer = ToByteArray();
            return buffer;
        }
		
        public UInt32 uiListen = new UInt32();
    };
	
	public class RM2C_GET_MOUNTAIN_SHOP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_GET_MOUNTAIN_SHOP;
		
		public RM2C_GET_MOUNTAIN_SHOP()
        {
            for (int i = 0; i < 8; ++i )
            {
                m_vctShopGoodsp[i] = new STowerShopGood();
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_GET_MOUNTAIN_SHOP();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            FromByteArrayNew(Buffer, this);
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }
		
		public UInt32 uiListen = new UInt32();
		public int iResult = new int();	
		///下次刷新时间
		public UInt32 uiRefreshTime = new UInt32();
		///服务器当前时间
		public UInt32 uiServerTime = new UInt32();
		///今天已经刷新次数
		public int iCntRefresh = new int();			
		///商店物品信息 csvId对应的是Shop表里面的id
		public STowerShopGood[] m_vctShopGoodsp = new STowerShopGood[8];
    };
	
	[Serializable()]
    public class C2RM_SMONEY_SHOP_BUY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_SMONEY_SHOP_BUY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_SMONEY_SHOP_BUY();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            return false;
        }

        public byte[] getBuffer()
        {
            byte[] buffer = ToByteArray();
            return buffer;
        }
		
        public UInt32 uiListen = new UInt32();
		///商店物品位置
		public int iLoc = new int();
    };
	
	public class RM2C_SMONEY_SHOP_BUY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_SMONEY_SHOP_BUY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_SMONEY_SHOP_BUY();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            FromByteArrayNew(Buffer, this);
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }
		
		public UInt32 uiListen = new UInt32();
		public int iResult = new int();	
		///商店物品位置
		public int iLoc = new int();
		///花费
		public int iCost = new int();
		///新买的物品数量更新
		public SEquipment SEquip = new SEquipment();	
		///新买的碎片数量更新
		public SPiece SPiece = new SPiece();
		///购买的整卡
		public SPetInfo SPet = new SPetInfo();
    };
	
	[Serializable()]
    public class C2RM_REFRESH_SMONEY_SHOP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_REFRESH_SMONEY_SHOP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_REFRESH_SMONEY_SHOP();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            return false;
        }

        public byte[] getBuffer()
        {
            byte[] buffer = ToByteArray();
            return buffer;
        }
		
        public UInt32 uiListen = new UInt32();
		///刷新类型0系统定时自动刷新，1积分刷新
		public byte cType = new byte();
    };
	
	public class RM2C_REFRESH_SMONEY_SHOP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_REFRESH_SMONEY_SHOP;
		
		public RM2C_REFRESH_SMONEY_SHOP()
        {
            for (int i = 0; i < 8; ++i )
            {
                m_vctShopGoodsp[i] = new STowerShopGood();
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_REFRESH_SMONEY_SHOP();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            FromByteArrayNew(Buffer, this);
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }
		
		public UInt32 uiListen = new UInt32();
		public int iResult = new int();	
		///下次刷新时间
		public UInt32 uiRefreshTime = new UInt32();
		///服务器当前时间
		public UInt32 uiServerTime = new UInt32();
		///消耗
		public int iCost = new int();
		///今天已经刷新次数
		public int iCntRefresh = new int();	
		///商店物品信息
		public STowerShopGood[] m_vctShopGoodsp = new STowerShopGood[8];
    };
	
	[Serializable()]
    public class C2RM_GET_SMONEY_SHOP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_GET_SMONEY_SHOP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_GET_SMONEY_SHOP();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            return false;
        }

        public byte[] getBuffer()
        {
            byte[] buffer = ToByteArray();
            return buffer;
        }
		
        public UInt32 uiListen = new UInt32();
    };
	
	public class RM2C_GET_SMONEY_SHOP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_GET_SMONEY_SHOP;
		
		public RM2C_GET_SMONEY_SHOP()
        {
            for (int i = 0; i < 8; ++i )
            {
                m_vctShopGoodsp[i] = new STowerShopGood();
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_GET_SMONEY_SHOP();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            FromByteArrayNew(Buffer, this);
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }
		
		public UInt32 uiListen = new UInt32();
		public int iResult = new int();	
		///下次刷新时间
		public UInt32 uiRefreshTime = new UInt32();
		///服务器当前时间
		public UInt32 uiServerTime = new UInt32();
		///今天已经刷新次数
		public int iCntRefresh = new int();			
		///商店物品信息 csvId对应的是Shop表里面的id
		public STowerShopGood[] m_vctShopGoodsp = new STowerShopGood[8];
    };

    [Serializable()]
    public class C2RM_GET_NOBILITY_SHOP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_GET_NOBILITY_SHOP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_GET_NOBILITY_SHOP();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            return false;
        }

        public byte[] getBuffer()
        {
            byte[] buffer = ToByteArray();
            return buffer;
        }
		
        public UInt32 uiListen = new UInt32();
    };

    public class RM2C_GET_NOBILITY_SHOP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_GET_NOBILITY_SHOP;

        public RM2C_GET_NOBILITY_SHOP()
        {
            for( int i = 0; i < 8; ++i )
            {
                m_vctShopGoodsp[ i ] = new STowerShopGood();
            }
        }

        public static IProtocal Create( ushort msg, HeaderBase h )
        {
            return new RM2C_GET_NOBILITY_SHOP();
        }

        public ushort Message
        {
            get
            {
                return ( ushort ) OPCODE;
            }
        }

        public bool analysisBuffer( byte[] Buffer )
        {
            FromByteArrayNew( Buffer, this );
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }

        public UInt32 uiListen = new UInt32();
        public int iResult = new int();
        ///下次刷新时间
        public UInt32 uiRefreshTime = new UInt32();
        ///服务器当前时间
        public UInt32 uiServerTime = new UInt32();
        ///今天已经刷新次数
        public int iCntRefresh = new int();
        ///商店物品信息 csvId对应的是Shop表里面的id
        public STowerShopGood[] m_vctShopGoodsp = new STowerShopGood[ 8 ];
    };

    [Serializable()]
    public class C2RM_REFRESH_NOBILITY_SHOP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_REFRESH_NOBILITY_SHOP;

        public static IProtocal Create( ushort msg, HeaderBase h )
        {
            return new C2RM_REFRESH_NOBILITY_SHOP();
        }

        public ushort Message
        {
            get
            {
                return ( ushort ) OPCODE;
            }
        }

        public bool analysisBuffer( byte[] Buffer )
        {
            return false;
        }

        public byte[] getBuffer()
        {
            byte[] buffer = ToByteArray();
            return buffer;
        }

        public UInt32 uiListen = new UInt32();
        ///刷新类型0系统定时自动刷新，1竞技积分刷新
        public byte cType = new byte();
    };

    public class RM2C_REFRESH_NOBILITY_SHOP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_REFRESH_NOBILITY_SHOP;

        public RM2C_REFRESH_NOBILITY_SHOP()
        {
            for( int i = 0; i < 8; ++i )
            {
                m_vctShopGoodsp[ i ] = new STowerShopGood();
            }
        }

        public static IProtocal Create( ushort msg, HeaderBase h )
        {
            return new RM2C_REFRESH_NOBILITY_SHOP();
        }

        public ushort Message
        {
            get
            {
                return ( ushort ) OPCODE;
            }
        }

        public bool analysisBuffer( byte[] Buffer )
        {
            FromByteArrayNew( Buffer, this );
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }

        public UInt32 uiListen = new UInt32();
        public int iResult = new int();
        ///下次刷新时间
        public UInt32 uiRefreshTime = new UInt32();
        ///服务器当前时间
        public UInt32 uiServerTime = new UInt32();
        ///消耗钻石
        public int m_iCostQMoney = new int();
        ///今天已经刷新次数
        public int iCntRefresh = new int();
        ///商店物品信息
        public STowerShopGood[] m_vctShopGoodsp = new STowerShopGood[ 8 ];
    };

    [Serializable()]
    public class C2RM_NOBILITY_SHOP_BUY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_NOBILITY_SHOP_BUY;

        public static IProtocal Create( ushort msg, HeaderBase h )
        {
            return new C2RM_NOBILITY_SHOP_BUY();
        }

        public ushort Message
        {
            get
            {
                return ( ushort ) OPCODE;
            }
        }

        public bool analysisBuffer( byte[] Buffer )
        {
            return false;
        }

        public byte[] getBuffer()
        {
            byte[] buffer = ToByteArray();
            return buffer;
        }

        public UInt32 uiListen = new UInt32();
        ///商店物品位置
        public int iLoc = new int();
    };

    public class RM2C_NOBILITY_SHOP_BUY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_NOBILITY_SHOP_BUY;

        public static IProtocal Create( ushort msg, HeaderBase h )
        {
            return new RM2C_NOBILITY_SHOP_BUY();
        }

        public ushort Message
        {
            get
            {
                return ( ushort ) OPCODE;
            }
        }

        public bool analysisBuffer( byte[] Buffer )
        {
            FromByteArrayNew( Buffer, this );
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }

        public UInt32 uiListen = new UInt32();
        public int iResult = new int();
        ///商店物品位置
        public int iLoc = new int();
        ///钻石花费
        public int m_iQMoney = new int();
        ///新买的物品数量更新
        public SEquipment SEquip = new SEquipment();
        ///新买的碎片数量更新
        public SPiece SPiece = new SPiece();
        ///购买的整卡
        public SPetInfo SPet = new SPetInfo();
    };
}