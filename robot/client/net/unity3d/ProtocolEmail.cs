using System;

using System.IO;

using ExFormatter;
using System.Diagnostics;

namespace net.unity3d
{
	[Serializable()]
    public class C2RM_EMAIL_LIST : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_EMAIL_LIST;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_EMAIL_LIST();
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
    public class RM2C_EMAIL_LIST : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_EMAIL_LIST;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctSEmail = new SEmail[len];
                for (int i = 0; i < len; i++)
                {
                    vctSEmail[i] = new SEmail();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_EMAIL_LIST();
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
		public byte cIsBegin = new byte();
		public byte m_cIsOver = new byte();
		///邮件信息
		public SEmail[] vctSEmail;
    };
	
	[Serializable()]
    public class RM2C_CREATE_EMAIL : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_CREATE_EMAIL;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctSEmail = new SEmail[len];
                for (int i = 0; i < len; i++)
                {
                    vctSEmail[i] = new SEmail();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_CREATE_EMAIL();
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
		///邮件信息
		public SEmail[] vctSEmail;
    };
	
	public class C2RM_OPEN_EMAIL : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_OPEN_EMAIL;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_OPEN_EMAIL();
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
		public UInt64 luiIdEmail = new UInt64();
    };
	
	[Serializable()]
    public class RM2C_OPEN_EMAIL : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_OPEN_EMAIL;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctSPetInfo = new SPetInfo[len];
                for (int i = 0; i < len; i++)
                {
                    vctSPetInfo[i] = new SPetInfo();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_OPEN_EMAIL();
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
		public UInt32 uiMasterId = new UInt32();
		///邮件信息
		public UInt64 luiIdEmail = new UInt64();
		///打开邮件时间
		public UInt32 uiOpenTime = new UInt32();
		///当前系统时间
		public UInt32 uiCurServerTime = new UInt32();
		///获得游戏币
		public Int64 liSMoney = new Int64();
		///获得人民币
		public int liQMoney = new int();
		///获得正义徽章
		public int liBadge = new int();
		///万能碎片
        public int iPieceAll = new int();
		///竞技积分
        public int iSocreFight = new int();		
		///经验
        public UInt64 luiExp = new UInt64();	
		///更新碎片（总数）
		public SPiece SPie = new SPiece();
		///更新物品（总数）
		public SEquipment SEqu = new SEquipment();
		///得到的卡片（整卡）
		public SPetInfo[] vctSPetInfo;
    };
	
	[Serializable()]
    public class C2RM_DESTROY_EMAIL : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_DESTROY_EMAIL;


        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_DESTROY_EMAIL();
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
		public UInt32 uiListen = new UInt32();
		///要摧毁的邮件表
		public UInt64[] vctIdEmail;
    };
	
	[Serializable()]
    public class RM2C_DESTROY_EMAIL : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_DESTROY_EMAIL;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctIdEmail = new UInt64[len];
                for (int i = 0; i < len; i++)
                {
                    vctIdEmail[i] = new UInt64();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_DESTROY_EMAIL();
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
		///邮件信息
		public UInt64[] vctIdEmail;
    };
	
}