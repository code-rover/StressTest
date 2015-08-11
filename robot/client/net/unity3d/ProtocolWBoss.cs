using System;

using System.IO;

using ExFormatter;
using System.Diagnostics;

namespace net.unity3d
{
	[Serializable()]
    public class C2RM_WORLD_EVENT_TIME : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_WORLD_EVENT_TIME;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_WORLD_EVENT_TIME();
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
    public class RM2C_WORLD_EVENT_TIME : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_WORLD_EVENT_TIME;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctWorldEventTime = new SWorldEventTime[len];
                for (int i = 0; i < len; i++)
                {
                    vctWorldEventTime[i] = new SWorldEventTime();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_WORLD_EVENT_TIME();
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
            FromByteArray(realData, this);
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }

		public UInt32 uiListen = new UInt32();
		///当前系统时间
		public UInt32 uiCurServerTime = new UInt32();
		public SWorldEventTime[] vctWorldEventTime;
    };
	
	[Serializable()]
    public class C2RM_WROLD_BOSS_INFO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_WROLD_BOSS_INFO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_WROLD_BOSS_INFO();
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
		////世界活动id
		public UInt32 m_uiIdCsvWorldEnent = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_WROLD_BOSS_INFO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_WROLD_BOSS_INFO;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctWroldBossRank = new SWorldBossRank[len];
                for (int i = 0; i < len; i++)
                {
                    vctWroldBossRank[i] = new SWorldBossRank();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_WROLD_BOSS_INFO();
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
            FromByteArray(realData, this);
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }

		public UInt32 uiListen = new UInt32();
		public int iResult = new int();
		///世界boss当前血量
		public UInt64 luiHpCurBoss = new UInt64();	
		///世界boss等级
		public UInt16 usLvBoss = new UInt16();		
		///自己当前排名
		public UInt16 usCurRank = new UInt16();
		///自己的总伤害
		public UInt64 luiHrm = new UInt64();
		///所获金钱
		public UInt64 luiMoney = new UInt64();
		///剩余进攻次数
		public UInt16  m_usFightCnt = new UInt16();
		///剩余复活次数
		public UInt16  m_usCntBuy = new UInt16();
		///是否需要复活
		public byte  m_usIsDeadInWorldBoss = new byte();
		///世界活动id
		public UInt32 uiIdCsvWorldEnent;	
		public SWorldBossRank[] vctWroldBossRank;
    };
	
	[Serializable()]
    public class C2RM_GOTO_WORLD_BOSS : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_GOTO_WORLD_BOSS;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_GOTO_WORLD_BOSS();
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
		///世界活动id
		public UInt32 uiIdCsvWorldEnent;
		///玩家怪物站位
		public UInt64[] vctIdServerPet = new UInt64[9];
    };
	
	[Serializable()]
    public class RM2C_GOTO_WORLD_BOSS : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_GOTO_WORLD_BOSS;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_GOTO_WORLD_BOSS();
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
		///当前系统时间
		public UInt32 uiCurServerTime = new UInt32();
		///世界活动id
		public UInt32 uiIdCsvWorldEnent;
    };
	
	[Serializable()]
    public class C2RM_EXCITE_WORLD_BOSS : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_EXCITE_WORLD_BOSS;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_EXCITE_WORLD_BOSS();
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
    public class RM2C_EXCITE_WORLD_BOSS : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_EXCITE_WORLD_BOSS;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_EXCITE_WORLD_BOSS();
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
		/// 消耗游戏币
        public Int64 liSMoeny = new Int64();
    };
	
	[Serializable()]
    public class C2RM_CHECK_WROLD_BOSS_PK_BEGIN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_CHECK_WROLD_BOSS_PK_BEGIN;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_CHECK_WROLD_BOSS_PK_BEGIN();
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
		
        public UInt32 uiIdCsvWorldEvent = new UInt32();
		//战斗开始时间戳
		public UInt32 uiBeginTime = new UInt32();
    };
	
	[Serializable()]
    public class C2RM_CHECK_WROLD_BOSS_PK_MIN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_CHECK_WROLD_BOSS_PK_MIN;


        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_CHECK_WROLD_BOSS_PK_MIN();
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
    public class C2RM_CHECK_WROLD_BOSS_PK_OVER : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_CHECK_WROLD_BOSS_PK_OVER;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_CHECK_WROLD_BOSS_PK_OVER();
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
    public class RM2C_CHECK_WORLD_BOSS_PK : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_CHECK_WORLD_BOSS_PK;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_CHECK_WORLD_BOSS_PK();
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
		///当前系统时间
		public UInt32 uiCurServerTime = new UInt32();
		//是否胜利
		public byte cIsWin = new byte();
		//本次伤害
		public UInt64 luiHarmThis = new UInt64();			
		//总共伤害
		public UInt64 luiHarmAll = new UInt64();		
		//剩余复活次数
		public UInt16 usReviveCnt = new UInt16();	
		///是否需要复活
		public byte  m_usIsDeadInWorldBoss = new byte();
		///世界boss当前血量
		public UInt64 luiHpCurBoss = new UInt64();	
		///世界boss等级
		public UInt16 usLvBoss = new UInt16();	
    };
	
	[Serializable()]
    public class C2RM_WROLD_BOSS_PK_REVIVE : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_WROLD_BOSS_PK_REVIVE;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_WROLD_BOSS_PK_REVIVE();
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
		//复活类型0游戏币1人民币
		public byte cTypeRevive = new byte();
    };
	
	[Serializable()]
    public class RM2C_WROLD_BOSS_PK_REVIVE : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_WROLD_BOSS_PK_REVIVE;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_WROLD_BOSS_PK_REVIVE();
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
		///当前系统时间
		public UInt32 uiCurServerTime = new UInt32();
		//消耗游戏币
		public Int64 liCostSMoney = new Int64();			
		//消耗人民币
		public Int64 liCostQMoney = new Int64();		
		//剩余复活次数
		public UInt16 usReviveCnt = new UInt16();			
    };
	
	[Serializable()]
    public class C2RM_WORLD_BOSS_REWARD : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_WORLD_BOSS_REWARD;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_WORLD_BOSS_REWARD();
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
		////世界活动id
		public UInt32 m_uiIdCsvWorldEnent = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_WORLD_BOSS_REWARD : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_WORLD_BOSS_REWARD;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctRewardMaterial = new SRewardMaterial[len];
                for (int i = 0; i < len; i++)
                {
                    vctRewardMaterial[i] = new SRewardMaterial();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_WORLD_BOSS_REWARD();
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
            FromByteArray(realData, this);
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }

		public UInt32 uiListen = new UInt32();
		public int iResult = new int();
		////世界活动id
		public UInt32 uiIdCsvWorldEnent = new UInt32();
		////玩家server id
		public UInt32 uiIdMaster = new UInt32();
		public SRewardMaterial[] vctRewardMaterial;
    };
	
	[Serializable()]
    public class C2RM_LEAVE_WROLD_BOSS : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_LEAVE_WROLD_BOSS;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_LEAVE_WROLD_BOSS();
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
    public class RM2C_LEAVE_WROLD_BOSS : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_LEAVE_WROLD_BOSS;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_LEAVE_WROLD_BOSS();
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
		//剩余进攻次数
		public UInt16 usFightCnt = new UInt16();		
		///世界活动id
		public UInt32 m_uiIdCsvWorldEnent = new UInt32();
		///获得金币
		public Int64 liSMoney = new Int64();	
		///获得徽章
		public int iBadge = new int();				
    };
}