using System;

using System.IO;

using ExFormatter;
using System.Diagnostics;

namespace net.unity3d
{
	[Serializable()]
    public class C2RM_CHARGE : ExFormatterBinary, IProtocal
    {
		//充值
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_CHARGE;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_CHARGE();
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
		/// 充值charge表的对应id
		public UInt32 uiCsvId = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_CHARGE : ExFormatterBinary, IProtocal
    {
		//服务器生成订单，但订单未完成，这里需要把订单跟uiDBId发给充值的网站现在是test。。。
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_CHARGE;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_CHARGE();
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
		/// 结果
		public int iResult = new int();
		///订单
		public SOrders sOrder = new SOrders();
		///db 连接的 id, 这个需要网页服务器告诉account 服务器
		public UInt32 uiDBId = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_CHARGE_ORDER_FINISH : ExFormatterBinary, IProtocal
    {
		//服务器订单完成 ， 根据result，提示完成，添加钻石。。。
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_CHARGE_ORDER_FINISH;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_CHARGE_ORDER_FINISH();
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
		
		/// 结果
		public int iResult = new int();
		///充值记录，如果以前有这个csv id 的充值记录，则覆盖，没有则创建
		public SCharge sCharge = new SCharge();
		///获得的钻石, 用加法
		public UInt32 uiQMoney = new UInt32();
		///需要删除的订单id server
		public UInt64 uiOrderId = new UInt64();
    };
	
	[Serializable()]
    public class C2RM_CHARGE_LIST : ExFormatterBinary, IProtocal
    {
		//登录的时候，请求一下之前的充值记录，所有的充值记录，月卡当前这次。。。
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_CHARGE_LIST;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_CHARGE_LIST();
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
    public class RM2C_CHARGE_LIST : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_CHARGE_LIST;

        private void init(int len)
        {
            if (len >= 0)
            {
                sCharge = new SCharge[len];
                for (int i = 0; i < len; i++)
                {
                    sCharge[i] = new SCharge();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_CHARGE_LIST();
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
		public SCharge[] sCharge;
    };
	
	[Serializable()]
    public class C2RM_ORDER_LIST : ExFormatterBinary, IProtocal
    {
		//请求所有当前没有完成的订单
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ORDER_LIST;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_ORDER_LIST();
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
    public class RM2C_ORDER_LIST : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ORDER_LIST;

        private void init(int len)
        {
            if (len >= 0)
            {
                sOrder = new SOrders[len];
                for (int i = 0; i < len; i++)
                {
                    sOrder[i] = new SOrders();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_ORDER_LIST();
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
		public SOrders[] sOrder;
    };
	
	[Serializable()]
    public class C2RM_CHARGE_TEST : ExFormatterBinary, IProtocal
    {
	  //在获取oder的时候模拟充值网站
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_CHARGE_TEST;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_CHARGE_TEST();
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
		
		public UInt32 uiDbId = new UInt32();
		public UInt32 uiRoleId = new UInt32();
		public int iResult = new int();
		public UInt64 uiOrderId = new UInt64();
    };
	
	[Serializable()]
    public class C2RM_CHARGE_CARD : ExFormatterBinary, IProtocal
    {
		//领月卡，季卡的协议
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_CHARGE_CARD;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_CHARGE_CARD();
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
		/// 充值的csv id,即月卡一类的csv id
		public UInt32 uiChargeCsvId = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_CHARGE_CARD : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_CHARGE_CARD;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_CHARGE_CARD();
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
		///获得的钻石
		public UInt32 uiQMoney = new UInt32();
		///充值信息修改，重新赋值
		public SCharge sCharge = new SCharge();
    };
	
	
}