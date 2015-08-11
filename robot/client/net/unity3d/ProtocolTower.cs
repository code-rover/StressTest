using System;

using System.IO;

using ExFormatter;
using System.Diagnostics;

namespace net.unity3d
{
	[Serializable()]
    public class C2RM_GET_TOWER_INFO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_GET_TOWER_INFO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_GET_TOWER_INFO();
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
    public class RM2C_GET_TOWER_INFO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_GET_TOWER_INFO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_GET_TOWER_INFO();
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
		public UInt32 uiRefershTimeNext = new UInt32();
		///系统时间
		public UInt32 uiServerTime = new UInt32();
		///爬塔信息
		public STowerInfo SInfoTower = new STowerInfo();
    };
	
	[Serializable()]
    public class C2RM_CHECK_TOWER_PK_BEGIN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_CHECK_TOWER_PK_BEGIN;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_CHECK_TOWER_PK_BEGIN();
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
		
		///塔id
        public UInt16 usIdTower = new UInt16();
		///塔层数
		public UInt16 uslvTower = new UInt16();
		//战斗开始时间戳
		public UInt32 uiBeginTime = new UInt32();
		///自己怪物站位
		public UInt64[] vctIdServerPetSelf = new UInt64[9];
    };
	
	[Serializable()]
    public class C2RM_CHECK_TOWER_PK_MID : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_CHECK_TOWER_PK_MID;


        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_CHECK_TOWER_PK_MID();
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
    public class C2RM_CHECK_TOWER_PK_OVER : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_CHECK_TOWER_PK_OVER;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_CHECK_TOWER_PK_OVER();
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
    public class RM2C_CHECK_TOWER_PK : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_CHECK_TOWER_PK;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctSPiece = new SPiece[len];
                for (int i = 0; i < len; i++)
                {
                    vctSPiece[i] = new SPiece();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_CHECK_TOWER_PK();
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
		///是否胜利
		public byte cIsWin = new byte();
		///玩家id
		public UInt32 uiMasterId = new UInt32();
		///获得金钱
		public Int64 liSMoney = new Int64();
		///当前塔id
        public UInt16 usIdTower = new UInt16();
		///当前塔层数
		public UInt16 uslvTower = new UInt16();
		///最高塔层数
		public UInt16 usMaxTower = new UInt16();
		///掉落碎片
		public SPiece[] vctSPiece;
    };
	
	[Serializable()]
    public class C2RM_TOWER_SWEEP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_TOWER_SWEEP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_TOWER_SWEEP();
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
    public class RM2C_TOWER_SWEEP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_TOWER_SWEEP;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctSPiece = new SPiece[len];
                for (int i = 0; i < len; i++)
                {
                    vctSPiece[i] = new SPiece();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_TOWER_SWEEP();
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
		
		public int iResult = new int();
		public UInt32 uiListen = new UInt32();
		///玩家id
		public UInt32 uiMasterId = new UInt32();
		///获得金钱
		public Int64 liSMoney = new Int64();
		///当前塔层数
		public UInt16 uslvTower = new UInt16();
		///掉落碎片
		public SPiece[] vctSPiece;
    };
	
	[Serializable()]
    public class C2RM_TOWER_REWARD : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_TOWER_REWARD;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_TOWER_REWARD();
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
    public class RM2C_TOWER_REWARD : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_TOWER_REWARD;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctSPiece = new SPiece[len];
                for (int i = 0; i < len; i++)
                {
                    vctSPiece[i] = new SPiece();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_TOWER_REWARD();
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
		
		public int iResult = new int();
		public UInt32 uiListen = new UInt32();
		///通塔奖励已领塔数
		public UInt16 usLvReward = new UInt16();
		///玩家id
		public UInt32 uiMasterId = new UInt32();
		///掉落碎片
		public SPiece[] vctSPiece;
    };
	
	[Serializable()]
    public class C2RM_TOWER_DAY_REWARD : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_TOWER_DAY_REWARD;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_TOWER_DAY_REWARD();
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
    public class RM2C_TOWER_DAY_REWARD : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_TOWER_DAY_REWARD;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_TOWER_DAY_REWARD();
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
		///获得灵石
		public int iTowerMoney = new int();
    };
	
	[Serializable()]
    public class C2RM_TOWER_AGAIN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_TOWER_AGAIN;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_TOWER_AGAIN();
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
    public class RM2C_TOWER_AGAIN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_TOWER_AGAIN;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_TOWER_AGAIN();
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
		///塔层数
		public UInt16 usLvTower = new UInt16();
		///剩余重置次数
		public UInt16 usCntAgain = new UInt16();
    };
	
	[Serializable()]
    public class C2RM_REFRESH_TOWER_SHOP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_REFRESH_TOWER_SHOP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_REFRESH_TOWER_SHOP();
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
		///刷新类型0系统定时自动刷新，1灵石刷新
		public byte cType = new byte();
    };
	
	public class RM2C_REFRESH_TOWER_SHOP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_REFRESH_TOWER_SHOP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_REFRESH_TOWER_SHOP();
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
		///消耗灵石
		public int iCostTowerMoney = new int();
		///爬塔信息
		public STowerInfo SInfoTower = new STowerInfo();
    };
	
	[Serializable()]
    public class C2RM_BUY_TOWER_SHOP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_BUY_TOWER_SHOP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_BUY_TOWER_SHOP();
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
		///购买位置
		public int iLoc = new int();
    };
	
	public class RM2C_BUY_TOWER_SHOP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_BUY_TOWER_SHOP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_BUY_TOWER_SHOP();
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
		///购买位置
		public int iLoc = new int();
		///消耗灵石
		public int iTowerMoney = new int();
		///新买的物品数量更新
		public SEquipment SEquip = new SEquipment();	
		///新买的碎片数量更新
		public SPiece SPiece = new SPiece();
		///购买的整卡
		public SPetInfo SPet = new SPetInfo();
    };
	
	[Serializable()]
    public class C2RM_GET_TOWER_TIME : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_GET_TOWER_TIME;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_GET_TOWER_TIME();
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
	
	public class RM2C_GET_TOWER_TIME : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_GET_TOWER_TIME;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_GET_TOWER_TIME();
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
		///开始时间
		public UInt32 uiBeginTime = new UInt32();
		///结束时间
		public UInt32 uiOverTime = new UInt32();
		///今天是否已经结束
		public byte cIsOver = new byte();
    };
}