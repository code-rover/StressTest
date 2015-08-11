using System;

using System.IO;

using ExFormatter;
using System.Diagnostics;

namespace net.unity3d
{
	[Serializable()]
	public class C2RM_BEAST_INFO : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_BEAST_INFO;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_BEAST_INFO();
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
	public class RM2C_BEAST_INFO : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_BEAST_INFO;
		
		private void init(int len)
		{
			if (len >= 0)
			{
				vctSInfo = new SBeast[len];
				for (int i = 0; i < len; i++)
				{
					vctSInfo[i] = new SBeast();
				}
			}
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_BEAST_INFO();
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
		public UInt32 uiMasterId = new UInt32();
		public SBeast[] vctSInfo;
	};

	[Serializable()]
	public class C2RM_BEAST_LV_UP : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_BEAST_LV_UP;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_BEAST_LV_UP();
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
		///要升级的魂兽 id
		public UInt64 uiBeastId = new UInt64();
		///经验药的csv id
		public UInt32 uiPropCsvId = new UInt32();
		///经验药的数量
		public UInt32 uiCnt = new UInt32();
	};
	
	[Serializable()]
	public class RM2C_BEAST_LV_UP : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_BEAST_LV_UP;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_BEAST_LV_UP();
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
		/// 获得的经验
		public UInt64 uiExpAdd = new UInt64();
		///涨经验后的魂兽
		public SBeast sBeastEat = new SBeast();
		///消耗剩余的经验药
		public SEquipment sProp = new SEquipment();
	};

	[Serializable()]
	public class RM2C_NEW_BEAST : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_NEW_BEAST;
		
		private void init(int len)
		{
			if (len >= 0)
			{
				vctSInfo = new SBeast[len];
				for (int i = 0; i < len; i++)
				{
					vctSInfo[i] = new SBeast();
				}
			}
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_NEW_BEAST();
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

		public UInt32 uiMasterId = new UInt32();
		public SBeast[] vctSInfo;
	};

	[Serializable()]
	public class C2RM_BEAST_EQUIP_WEAR : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_BEAST_EQUIP_WEAR;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_BEAST_EQUIP_WEAR();
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
		///更新后的卡牌装备信息
		public SBeastEquip sBeastEquip = new SBeastEquip();
	};
	
	[Serializable()]
	public class RM2C_BEAST_EQUIP_WEAR : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_BEAST_EQUIP_WEAR;
		
		private void init(int len)
		{
			if (len >= 0)
			{
				sEquip = new SEquipment[len];
				for (int i = 0; i < len; i++)
				{
					sEquip[i] = new SEquipment();
				}
			}
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_BEAST_EQUIP_WEAR();
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
		/// 魂兽装备信息
		public SBeastEquip sBeastEquip = new SBeastEquip();
		///改变的装备信息,重新赋值就好了
		public SEquipment[] sEquip;
	};

	[Serializable()]
	public class C2RM_BEAST_EQUIP_UP : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_BEAST_EQUIP_UP;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_BEAST_EQUIP_UP();
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
		///被强化的魂兽id
		public UInt64 luiIdBeast = new UInt64();
		///被强化的位置
		public int iLoc = new int();
    };
	
	[Serializable()]
	public class RM2C_BEAST_EQUIP_UP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_BEAST_EQUIP_UP;

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
			return new RM2C_BEAST_EQUIP_UP();
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
		///被强化的魂兽id
		public UInt64 luiIdBeast = new UInt64();
		///被强化的位置
		public int iLoc = new int();
		///该位置上放置的物品csv id
		public UInt32 uiIdCsvEqu = new UInt32();
		///合成该物品后被消耗的物品更新列表
		public SCostInfo[] vctCostInfo;
    };

	[Serializable()]
	public class C2RM_SOUL_SHOP : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_SOUL_SHOP;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_SOUL_SHOP();
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
	public class RM2C_SOUL_SHOP : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_SOUL_SHOP;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_SOUL_SHOP();
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
		///星际商人信息
		public SBeastShop sBeastShop = new SBeastShop();
		///服务器当前时间
		public UInt32 uiServerTime = new UInt32();
	};

	[Serializable()]
	public class C2RM_SOUL_SHOP_BUY : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_SOUL_SHOP_BUY;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_SOUL_SHOP_BUY();
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
		///购买的位置
		public byte cLoc = new byte();
		///被消耗的碎片id
		public UInt32 uiCostCsvId = new UInt32();
	};

	[Serializable()]
	public class RM2C_SOUL_SHOP_BUY : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_SOUL_SHOP_BUY;
		
		private void init(int len)
		{
			if (len >= 0)
			{
				vctBeastShopId = new UInt32[len];
				for (int i = 0; i < len; i++)
				{
					vctBeastShopId[i] = new UInt32();
				}
			}
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_SOUL_SHOP_BUY();
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
		///开箱类型0小，1中，2大
		public byte cType = new byte();
		///星际商人信息
		public SBeastShop sBeastShop = new SBeastShop();
		///消耗的魂兽碎片
		public SCostInfo CostChip = new SCostInfo();
		///获得的魂兽商店id
		public UInt32[] vctBeastShopId;
	};

	[Serializable()]
	public class C2RM_SOUL_COM : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_SOUL_COM;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_SOUL_COM();
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
		///使用的魂兽碎片csvid
		public UInt32 uiCostCsvId = new UInt32();
	};

	[Serializable()]
	public class RM2C_SOUL_COM : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_SOUL_COM;
		
		private void init(int len)
		{
			if (len >= 0)
			{
				CostChip = new SCostInfo[len];
				for (int i = 0; i < len; i++)
				{
					CostChip[i] = new SCostInfo();
				}
			}
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_SOUL_COM();
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
		///新生成的魂兽
		public SBeast sBeastEat = new SBeast();
		///消耗的魂兽碎片
		public SCostInfo[] CostChip;
	};

	[Serializable()]
	public class C2RM_SUN_WELL_INFO : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_SUN_WELL_INFO;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_SUN_WELL_INFO();
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
	public class RM2C_SUN_WELL_INFO : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_SUN_WELL_INFO;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_SUN_WELL_INFO();
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
		///太阳井已经重置次数
		public UInt32 uiCntReset = new UInt32();
	};

	[Serializable()]
	public class C2RM_SUN_WELL_RESET : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_SUN_WELL_RESET;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_SUN_WELL_RESET();
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
	public class RM2C_SUN_WELL_RESET : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_SUN_WELL_RESET;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_SUN_WELL_RESET();
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
		///太阳井已经重置次数
		public UInt32 uiCntReset = new UInt32();
	};
}