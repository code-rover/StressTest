using System;

using System.IO;

using ExFormatter;
using System.Diagnostics;

namespace net.unity3d
{
	[Serializable()]
    public class C2RM_STONE_INLAY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_STONE_INLAY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_STONE_INLAY();
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
		///要镶嵌的宠物id
		public UInt64 luiIdPet = new UInt64();
		///要镶嵌的位置
		public UInt16 usLoc = new UInt16();
    };
	
	[Serializable()]
    public class RM2C_STONE_INLAY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_STONE_INLAY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_STONE_INLAY();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            FromByteArray(Buffer, this);
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }

        public UInt32 uiListen = new UInt32();
		///结果1成功，其他失败
		public int iResult = new int();
		///要镶嵌的宠物id
		public UInt64 luiIdPet = new UInt64();
		///要镶嵌的位置
		public UInt16 usLoc = new UInt16();
		///此宠物当前的进阶石状态（位操作）
		public UInt32 uiStoneInfo = new UInt32();
		///消耗的进阶石
		public SCostInfo SCostStone = new SCostInfo();
		///消耗的货币
		public SMoney SCostMoney = new SMoney();
    };
	
	[Serializable()]
    public class C2RM_PET_STONE_UP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_PET_STONE_UP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_PET_STONE_UP();
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
		///要镶嵌的宠物id
		public UInt64 luiIdPet = new UInt64();
    };
	
	[Serializable()]
    public class RM2C_PET_STONE_UP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_PET_STONE_UP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_PET_STONE_UP();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }

        public bool analysisBuffer(byte[] Buffer)
        {
            FromByteArray(Buffer, this);
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }

        public UInt32 uiListen = new UInt32();
		///结果1成功，其他失败
		public int iResult = new int();
		///要镶嵌的宠物id
		public UInt64 luiIdPet = new UInt64();
		///进化后的宠物CSV id 
		public UInt32 uiIdCsvPet = new UInt32();
		///消耗的货币
		public SMoney SCostMoney = new SMoney();
    };
}