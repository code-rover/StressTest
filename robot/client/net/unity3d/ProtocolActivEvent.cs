using System;

using System.IO;

using ExFormatter;
using System.Diagnostics;

namespace net.unity3d
{
	[Serializable()]
    public class C2RM_ACTIV_EVENT_TIME : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ACTIV_EVENT_TIME;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_ACTIV_EVENT_TIME();
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
    public class RM2C_ACTIV_EVENT_TIME : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ACTIV_EVENT_TIME;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctActivEventTime = new SActivEventTime[len];
                for (int i = 0; i < len; i++)
                {
                    vctActivEventTime[i] = new SActivEventTime();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_ACTIV_EVENT_TIME();
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
		public SActivEventTime[] vctActivEventTime;
    };
	
	[Serializable()]
    public class C2RM_ACTIV_EVENT_INFO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ACTIV_EVENT_INFO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_ACTIV_EVENT_INFO();
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
    public class RM2C_ACTIV_EVENT_INFO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ACTIV_EVENT_INFO;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctActivEvent = new SMasterActivEvent[len];
                for (int i = 0; i < len; i++)
                {
                    vctActivEvent[i] = new SMasterActivEvent();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_ACTIV_EVENT_INFO();
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
		public SMasterActivEvent[] vctActivEvent;
    };
	
	[Serializable()]
    public class C2RM_ACTIV_EVENT_BEGIN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ACTIV_EVENT_BEGIN;

		public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_ACTIV_EVENT_BEGIN();
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
		///活动id
		public UInt32 uiActicEventCsvId = new UInt32();	
		///基友id
		public UInt32 uiIdMaster = new UInt32();	
		///基友id宠物
		public UInt64 uiFriendPetid = new UInt64();
		///玩家怪物站位
		public UInt64[] vctIdServerPet = new UInt64[9];
    };	
	
	[Serializable()]
    public class RM2C_ACTIV_EVENT_BEGIN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ACTIV_EVENT_BEGIN;

        private void init(int len)
        {
            if (len >= 0)
            {
				vctIdEnemy = new UInt32[3];
				for(int i = 0; i < 3; i++)
				{
					vctIdEnemy[i] = new UInt32();
				}
                vctSReward = new SFightReward[len];
                for (int i = 0; i < len; i++)
                {
                    vctSReward[i] = new SFightReward();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_ACTIV_EVENT_BEGIN();
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
		
		public int iResult = new int();
		public UInt32 uiListen = new UInt32();
		///csv id
		public UInt32 uiIdCsvFb = new UInt32();
		///主角id
		public UInt32 uiMasterId = new UInt32();
		/// 奖励的友情点数
		public UInt32 uiFriendShip = new UInt32();
		///基友id
		public UInt32 uiIdMaster = new UInt32();	
		///基友宠物id
		public UInt64 uiFriendPetid = new UInt64();
		///每波敌人id
		public UInt32[] vctIdEnemy;
		///基友战斗信息
		public SPetInfo sPetInfo = new SPetInfo();
		///副本怪物奖励，战斗之前给客户端显示用，没有通关奖励，通关奖励战斗之后才有
		public SFightReward[] vctSReward;
    };
	
	[Serializable()]
    public class C2RM_CHECK_ACTIV_EVENT_PK_BEGIN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_CHECK_ACTIV_EVENT_PK_BEGIN;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_CHECK_ACTIV_EVENT_PK_BEGIN();
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
		//战斗开始时间戳
		public UInt32 uiBeginTime = new UInt32();
		///自己怪物站位
		public UInt64[] vctIdServerPetSelf = new UInt64[9];
    };
	
	[Serializable()]
    public class C2RM_CHECK_ACTIV_EVENT_MID : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_CHECK_ACTIV_EVENT_MID;


        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_CHECK_ACTIV_EVENT_MID();
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
    public class C2RM_CHECK_ACTIV_EVENT_OVER : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_CHECK_ACTIV_EVENT_OVER;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_CHECK_ACTIV_EVENT_OVER();
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
    public class RM2C_CHECK_ACTIV_EVENT : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_CHECK_ACTIV_EVENT;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_CHECK_ACTIV_EVENT();
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
		public byte cIsWin = new byte();
		public UInt32 uiFriendShip = new UInt32();
		public SMasterActivEvent ActivEvent = new SMasterActivEvent();
    };
	
	[Serializable()]
    public class C2RM_ADD_ACTIV_EVENT_CNT : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ADD_ACTIV_EVENT_CNT;

		public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_ADD_ACTIV_EVENT_CNT();
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
		///活动csv id
		public UInt32 uiIdCsv = new UInt32();
		public int iCnt = new int();
    };	
	
	[Serializable()]
    public class RM2C_ADD_ACTIV_EVENT_CNT : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ADD_ACTIV_EVENT_CNT;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_ADD_ACTIV_EVENT_CNT();
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
		///活动csv id
		public UInt32 uiIdCsv = new UInt32();
		public int iCostQMoney = new int();
		public SMasterActivEvent ActivEvent = new SMasterActivEvent();
    };
	
	[Serializable()]
    public class C2RM_GO_TO_ACTIV_EVENT : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EE_C2RM_GO_TO_ACTIV_EVENT;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_GO_TO_ACTIV_EVENT();
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
		///活动csv id
		public UInt32 uiIdCsv = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_GO_TO_ACTIV_EVENT : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_GO_TO_ACTIV_EVENT;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_GO_TO_ACTIV_EVENT();
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

        public int iResult = new int();
		public UInt32 uiListen = new UInt32();
		///活动csv id
		public UInt32 uiIdCsv = new UInt32();
    };
	
	[Serializable()]
    public class C2RM_LEAVE_ACTIV_EVENT : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EE_C2RM_LEAVE_ACTIV_EVENT;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_LEAVE_ACTIV_EVENT();
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
    public class RM2C_LEAVE_ACTIV_EVENT : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_LEAVE_ACTIV_EVENT;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_LEAVE_ACTIV_EVENT();
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

        public int iResult = new int();
		public UInt32 uiListen = new UInt32();
    };
	
	[Serializable()]
    public class C2RM_RE_ACTIV_EVENT : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_RE_ACTIV_EVENT;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_RE_ACTIV_EVENT();
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
    public class RM2C_RE_ACTIV_EVENT : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_RE_ACTIV_EVENT;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_RE_ACTIV_EVENT();
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

        public int iResult = new int();
		public UInt32 uiListen = new UInt32();
    };
	
}