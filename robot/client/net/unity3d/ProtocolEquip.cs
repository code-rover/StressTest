using System;

using System.IO;

using ExFormatter;
using System.Diagnostics;

namespace net.unity3d
{
	[Serializable()]
    public class C2RM_EQUIP_COM : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_EQUIP_COM;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_EQUIP_COM();
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
		///期望合成的物品
		public UInt32 uiIdGoods = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_EQUIP_COM : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_EQUIP_COM;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctCostInfo = new SCostInfo[len];
                for (int i = 0; i < len; i++)
                {
                    vctCostInfo[i] = new SCostInfo();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_EQUIP_COM();
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
		///消耗的货币
		public SMoney SCostMoney = new SMoney();
		///合成后新生成的物品
		public SEquipment SEqu = new SEquipment();
		///合成该物品后被消耗的物品更新列表
		public SCostInfo[] vctCostInfo;
    };
	
	[Serializable()]
    public class C2RM_EQUIP_UP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_EQUIP_UP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_EQUIP_UP();
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
		///被强化的宠物id
		public UInt64 luiIdPet = new UInt64();
		///被强化的位置
		public int iLoc = new int();
    };
	
	[Serializable()]
    public class RM2C_EQUIP_UP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_EQUIP_UP;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctCostInfo = new SCostInfo[len];
                for (int i = 0; i < len; i++)
                {
                    vctCostInfo[i] = new SCostInfo();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_EQUIP_UP();
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
		///消耗的货币
		public SMoney SCostMoney = new SMoney();
		///被强化的宠物id
		public UInt64 luiIdPet = new UInt64();
		///被强化的位置
		public int iLoc = new int();
		///该位置上放置的物品csv id
		public UInt32 uiIdCsvEqu = new UInt32();
		///合成后新生成的物品
		public SEquipment SEqu = new SEquipment();
		///合成该物品后被消耗的物品更新列表
		public SCostInfo[] vctCostInfo;
    };
	
	[Serializable()]
    public class C2RM_EQUIP_UP_ONE_KEY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_EQUIP_UP_ONE_KEY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_EQUIP_UP_ONE_KEY();
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
		///被强化的宠物id
		public UInt64 luiIdPet = new UInt64();
		///被强化的位置
		public int iLoc = new int();
    };
	
	[Serializable()]
    public class RM2C_EQUIP_UP_ONE_KEY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_EQUIP_UP_ONE_KEY;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctCostInfo = new SCostInfo[len];
                for (int i = 0; i < len; i++)
                {
                    vctCostInfo[i] = new SCostInfo();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_EQUIP_UP_ONE_KEY();
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
		///消耗的货币
		public SMoney SCostMoney = new SMoney();
		///被强化的宠物id
		public UInt64 luiIdPet = new UInt64();
		///被强化的位置
		public int iLoc = new int();
		///该位置上放置的物品csv id
		public UInt32 uiIdCsvEqu = new UInt32();
		///合成后新生成的物品
		public SEquipment SEqu = new SEquipment();
		///合成该物品后被消耗的物品更新列表
		public SCostInfo[] vctCostInfo;
    };

	[Serializable()]
	public class C2RM_OPEN_GIFT_BOX : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_OPEN_GIFT_BOX;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_OPEN_GIFT_BOX();
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
		///被开启的礼盒csvid
		public UInt32 uiIdGiftBox = new UInt32();
		///开启的个数
		public UInt32 uiCnt = new UInt32();
	};

	[Serializable()]
	public class RM2C_OPEN_GIFT_BOX : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_OPEN_GIFT_BOX;
		
		private void init(int len)
		{
			if (len >= 0)
			{
				vctCostInfo = new SCostInfo[len];
				for (int i = 0; i < len; i++)
				{
					vctCostInfo[i] = new SCostInfo();
				}
			}
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_OPEN_GIFT_BOX();
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
		///获得的虚拟货币（真实，需要更新数据）
		public SRewardMoney SMoney = new SRewardMoney();
		///被消耗后的礼盒信息(真实)
		public SCostInfo SCostGiftBox = new SCostInfo();
		///获得的奖励(显示用)
		public SCostInfo[] vctCostInfo;
	};
}