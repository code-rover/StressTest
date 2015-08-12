using System;
using System.IO;
using ExFormatter;
using System.Diagnostics;

namespace net.unity3d
{
	[Serializable()]
    public class RM2C_ON_REALM_SERVER_CLOSE : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ON_REALM_SERVER_CLOSE;

        public RM2C_ON_REALM_SERVER_CLOSE()
        {
            uiReason = new UInt32();
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_ON_REALM_SERVER_CLOSE();
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

        public UInt32 uiReason;
    };
	
	[Serializable()]
    public class RM2C_ON_ACCOUNT_SERVER_CLOSE : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ON_ACCOUNT_SERVER_CLOSE;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_ON_ACCOUNT_SERVER_CLOSE();
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

        public UInt32 uiReason = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_ON_WEB_CLOSE : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ON_WEB_CLOSE;

        public RM2C_ON_WEB_CLOSE()
        {
            uiReason = new UInt32();
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_ON_WEB_CLOSE();
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

        public UInt32 uiReason;
    };
	
	[Serializable()]
    public class RM2C_ON_REALM_SERVER_CONN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ON_REALM_SERVER_CONN;

        public RM2C_ON_REALM_SERVER_CONN()
        {
            uiReason = new UInt32();
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_ON_REALM_SERVER_CONN();
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

        public UInt32 uiReason;
    };
	
	[Serializable()]
    public class RM2G_ON_GUEST_SERVER_CONN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2G_ON_GUEST_SERVER_CONN;

        public RM2G_ON_GUEST_SERVER_CONN()
        {
            uiReason = new UInt32();
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2G_ON_GUEST_SERVER_CONN();
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

        public UInt32 uiReason;
    };
	
	[Serializable()]
    public class LS2C_ON_LOGIN_SERVER_CONN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_LS2C_ON_LOGIN_SERVER_CONN;

        public LS2C_ON_LOGIN_SERVER_CONN()
        {
            uiReason = new UInt32();
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new LS2C_ON_LOGIN_SERVER_CONN();
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

        public UInt32 uiReason;
    };
	
	[Serializable()]
    public class RM2C_ON_ACCOUNT_SERVER_CONN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ON_ACCOUNT_SERVER_CONN;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_ON_ACCOUNT_SERVER_CONN();
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

        public UInt32 uiReason = new UInt32();
    };
	
	[Serializable()]
    public class C2RM_LOGIN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_LOGIN;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_LOGIN();
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

		public void setAccountIdStr(string psAccountId)
		{
			byte[] accountByte = System.Text.Encoding.UTF8.GetBytes(psAccountId);
			for (int i = 0; i < accountByte.Length; i++)
			{
				cAccountId[i] = accountByte[i];
			}
		}
		public string getAccountIdStr()
		{
			return System.Text.Encoding.UTF8.GetString(cAccountId, 0, byteToStringIndex(cAccountId));
		}

		public void setChannelIdStr(string psChannelId)
		{
			byte[] channelByte = System.Text.Encoding.UTF8.GetBytes(psChannelId);
			for (int i = 0; i < channelByte.Length; i++)
			{
				cChannelId[i] = channelByte[i];
			}
		}
		public string getChannelIdStr()
		{
			return System.Text.Encoding.UTF8.GetString(cChannelId, 0, byteToStringIndex(cChannelId));
		}
		
		///public UInt64 uiAccountId = new UInt64();	/// no use
		public byte[] cChannelId = new byte[32];
		public byte[] cAccountId= new byte[64];
		public SAccountC2AC sAccountC2AC = new SAccountC2AC();
		public byte cIsReLogin = new byte();	//是否是重新连接 0 代表用户正常退出后登陆，1代表用户意外断网后登陆
    };
	
	[Serializable()]
    public class RM2C_LOGIN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_LOGIN;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_LOGIN();
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
    };
	
	[Serializable()]
    public class RM2C_MASTER_BASE_INFO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MASTER_BASE_INFO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_MASTER_BASE_INFO();
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

        public SPlayerInfo SPlayerInfo = new SPlayerInfo();
    };
	
	[Serializable()]
    public class C2RM_PET_INFO_BAG : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_PET_INFO_BAG;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_PET_INFO_BAG();
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
    public class RM2C_PET_INFO_BAG : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_PET_INFO_BAG;

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
            return new RM2C_PET_INFO_BAG();
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
		///是否是宠物协议组第一个协议
		public byte cIsBegin = new byte();
		///是否是宠物协议组最后一个协议
		public byte cIsOver = new byte();
		public SPetInfo[] vctSPetInfo;

    };
	
	[Serializable()]
    public class C2RM_PET_INFO_BAR : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_PET_INFO_BAR;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_PET_INFO_BAR();
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
    public class RM2C_PET_INFO_BAR : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_PET_INFO_BAR;

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
            return new RM2C_PET_INFO_BAR();
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
		///是否是宠物协议组第一个协议
		public byte cIsBegin = new byte();
		///是否是宠物协议组最后一个协议
		public byte cIsOver = new byte();
		public SPetInfo[] vctSPetInfo;

    };
	
	[Serializable()]
    public class C2RM_TEAM_INFO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_TEAM_INFO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_TEAM_INFO();
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
    public class RM2C_TEAM_INFO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_TEAM_INFO;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctSTeamInfo = new STeam[len];
                for (int i = 0; i < len; i++)
                {
                    vctSTeamInfo[i] = new STeam();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_TEAM_INFO();
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
		public STeam[] vctSTeamInfo;

    };
	
	[Serializable()]
    public class C2RM_EQUIP_GROUP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_EQUIP_GROUP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_EQUIP_GROUP();
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
    public class RM2C_EQUIP_GROUP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_EQUIP_GROUP;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctSEquipGroup = new SEquipGroup[len];
                for (int i = 0; i < len; i++)
                {
                    vctSEquipGroup[i] = new SEquipGroup();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_EQUIP_GROUP();
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
		public SEquipGroup[] vctSEquipGroup;

    };
	
	[Serializable()]
    public class C2RM_MATERIAL_GROUP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MATERIAL_GROUP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_MATERIAL_GROUP();
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
    public class RM2C_MATERIAL_GROUP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MATERIAL_GROUP;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctSMaterialGroup = new SMaterialGroup[len];
                for (int i = 0; i < len; i++)
                {
                    vctSMaterialGroup[i] = new SMaterialGroup();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_MATERIAL_GROUP();
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
		public SMaterialGroup[] vctSMaterialGroup;
    };
	
	[Serializable()]
    public class C2RM_EQUIPMENT : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_EQUIPMENT;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_EQUIPMENT();
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
    public class RM2C_EQUIPMENT : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_EQUIPMENT;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctSEquipment = new SEquipment[len];
                for (int i = 0; i < len; i++)
                {
                    vctSEquipment[i] = new SEquipment();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_EQUIPMENT();
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
		public byte cIsOver = new byte();
		public SEquipment[] vctSEquipment;
    };
	
	[Serializable()]
    public class C2RM_FB : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FB;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_FB();
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
    public class RM2C_FB : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FB;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctSFBInfo = new SFBInfo[len];
                for (int i = 0; i < len; i++)
                {
                    vctSFBInfo[i] = new SFBInfo();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FB();
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
		public SFBInfo[] vctSFBInfo;
    };
	
	[Serializable()]
    public class C2RM_GOTO_FB : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_GOTO_FB;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_GOTO_FB();
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
		public UInt32 uiCsvFBid = new UInt32();
		public UInt32 uiMasterid = new UInt32();
		public UInt64 uiFriendPetid = new UInt64();
		///玩家怪物站位
		public UInt64[] vctIdServerPet = new UInt64[9];
    };
	
	[Serializable()]
    public class RM2C_GOTO_FB : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_GOTO_FB;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_GOTO_FB();
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
		public UInt32 uiIdCsvFB = new UInt32();
		public UInt32 uiMasterid = new UInt32();
		public UInt64 uiFriendPetid = new UInt64();
		public SPetInfo sPetInfo = new SPetInfo();
    };
	
	
		
	[Serializable()]
    public class C2RM_NEXT_FB : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_NEXT_FB;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_NEXT_FB();
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
		public UInt32 uiCsvFBid = new UInt32();
		public UInt32 uiMasterid = new UInt32();
		public UInt64 uiFriendPetid = new UInt64();
		///玩家怪物站位
		public UInt64[] vctIdServerPet = new UInt64[9];
    };
	
	[Serializable()]
    public class RM2C_NEXT_FB : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_NEXT_FB;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_NEXT_FB();
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
		public UInt32 uiIdCsvFB = new UInt32();
		public UInt32 uiMasterid = new UInt32();
		public UInt64 uiFriendPetid = new UInt64();
		public SPetInfo sPetInfo = new SPetInfo();
    };
	
	[Serializable()]
    public class C2RM_LEAVE_FB : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_LEAVE_FB;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_LEAVE_FB();
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
    public class RM2C_LEAVE_FB : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_LEAVE_FB;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_LEAVE_FB();
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
		public UInt16 usIdTown = new UInt16();
    };
	
	[Serializable()]				
    public class C2AC_ACCOUNT_INFO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2AC_ACCOUNT_INFO;

        public C2AC_ACCOUNT_INFO()
        {
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2AC_ACCOUNT_INFO();
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
        public SAccountC2AC sAccountC2Ac = new SAccountC2AC();
        
    };


    [Serializable()]
    public class AC2C_ACCOUNT_INFO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_AC2C_ACCOUNT_INFO;

        public AC2C_ACCOUNT_INFO()
        {
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new AC2C_ACCOUNT_INFO();
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
        public int iResult = new int();
        public SAccountAC2C sAccountAC2C = new SAccountAC2C();
    };

	[Serializable()]				
	public class C2AC_RECENT_SERVER_INFO : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2AC_RECENT_SERVER_INFO;
		
		public C2AC_RECENT_SERVER_INFO()
		{
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2AC_RECENT_SERVER_INFO();
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
		public SAccountC2AC sAccountC2Ac = new SAccountC2AC();
		
	};

	[Serializable()]
	public class AC2C_RECENT_SERVER_INFO : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_AC2C_RECENT_SERVER_INFO;
		
		private void init(int len)
		{	
			if (len >= 0)
			{
				vctRecentInfo = new SRecentServerInfo[len];
				for (int i = 0; i < len; i++)
				{
					vctRecentInfo[i] = new SRecentServerInfo();
				}
			}
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new AC2C_RECENT_SERVER_INFO();
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
		public byte cIsBegin = new byte();	
		public byte cIsOver = new byte();
		public SRecentServerInfo[] vctRecentInfo;
	};
	
	[Serializable()]
    public class C2LS_LOGIN_REQUEST : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2LS_LOGIN_REQUEST;
                                                          
        public C2LS_LOGIN_REQUEST()
        {
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2LS_LOGIN_REQUEST();
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

		public void setAccountIdStr(string psAccountId)
		{
			byte[] accountByte = System.Text.Encoding.UTF8.GetBytes(psAccountId);
			for (int i = 0; i < accountByte.Length; i++)
			{
				cAccountId[i] = accountByte[i];
			}
		}
		public string getAccountIdStr()
		{
			return System.Text.Encoding.UTF8.GetString(cAccountId, 0, byteToStringIndex(cAccountId));
		}

		public void setChannelIdStr(string psChannelId)
		{
			byte[] channelByte = System.Text.Encoding.UTF8.GetBytes(psChannelId);
			for (int i = 0; i < channelByte.Length; i++)
			{
				cChannelId[i] = channelByte[i];
			}
		}
		public string getChannelIdStr()
		{
			return System.Text.Encoding.UTF8.GetString(cChannelId, 0, byteToStringIndex(cChannelId));
		}

		///public UInt64 uiAccountId = new UInt64();	/// no use
		public byte[] cChannelId = new byte[32];
		public byte[] cAccountId= new byte[64];
    };

    [Serializable()]
    public class LS2C_LOGIN_RESPONSE : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_LS2C_LOGIN_RESPONSE;

        public LS2C_LOGIN_RESPONSE()
        {
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new LS2C_LOGIN_RESPONSE();
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
		public string getIp()
        {
            return System.Text.Encoding.UTF8.GetString(ip, 0, byteToStringIndex(ip));
        }
        public int iResult;
        public byte[] ip = new byte[32];
        public ushort port = new ushort();
        public ushort guestPort = new ushort();
        public UInt32 timeOut = new UInt32();
    };
	
	[Serializable()]
    public class C2RM_CREAT_ROLE : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_CREAT_ROLE;

        public C2RM_CREAT_ROLE()
        {
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_CREAT_ROLE();
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

        public UInt32 uiPetCsvId = new UInt32();
    };
	
	[Serializable()]
    public class C2RM_FRIEND_RECOMMEND : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FRIEND_RECOMMEND;
		public C2RM_FRIEND_RECOMMEND()
        {
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_FRIEND_RECOMMEND();
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
    public class RM2C_FRIEND_RECOMMEND : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_RECOMMEND;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctSFriendInfo = new SFriendReInfo[len];
                for (int i = 0; i < len; i++)
                {
                    vctSFriendInfo[i] = new SFriendReInfo();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_RECOMMEND();
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
		/// 特殊推荐的id
		public UInt32 uiSRoleId = new UInt32();
		public SFriendReInfo[] vctSFriendInfo;
    };
	
	[Serializable()]
    public class C2RM_FB_PK_BEGIN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FB_PK_BEGIN;

		public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_FB_PK_BEGIN();
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
    public class RM2C_FRIEND_NOTIFY_TIME : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_NOTIFY_TIME;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_NOTIFY_TIME();
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

        public SFriendFightInfo sInfo = new SFriendFightInfo();
    };
	
	[Serializable()]
    public class RM2C_FB_PK_BEGIN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FB_PK_BEGIN;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctSReward = new SFightReward[len];
                for (int i = 0; i < len; i++)
                {
                    vctSReward[i] = new SFightReward();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FB_PK_BEGIN();
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
		///副本csv id
		public UInt32 uiIdCsvFb = new UInt32();
		///主角id
		public UInt32 uiMasterId = new UInt32();
		///当前经验
		public UInt64 luiCurExp = new UInt64();	
		/// 奖励的友情点数
		public UInt32 uiFriendShip = new UInt32();
		///副本怪物奖励，战斗之前给客户端显示用，没有通关奖励，通关奖励战斗之后才有
		public SFightReward[] vctSReward;
    };
	
	[Serializable()]
    public class C2RM_FRIEND : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FRIEND;
		public C2RM_FRIEND()
        {
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_FRIEND();
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
    public class RM2C_FRIEND : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctSFriendInfo = new SFriendClient[len];
                for (int i = 0; i < len; i++)
                {
                    vctSFriendInfo[i] = new SFriendClient();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND();
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
		public SFriendClient[] vctSFriendInfo;
    };
	
	[Serializable()]
    public class C2RM_FRIEND_FIGHT_SELECT : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FRIEND_FIGHT_SELECT;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_FRIEND_FIGHT_SELECT();
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
		///选择的角色的主角id
		public UInt32 uiRoleId = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_FRIEND_FIGHT_SELECT : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_FIGHT_SELECT;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_FIGHT_SELECT();
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
		public UInt32 uiRoleId = new UInt32();
		public SPetInfo sPetInfo = new SPetInfo();
		///花的游戏币
		public SMoney sMoney = new SMoney();
    };
	
	[Serializable()]
    public class C2RM_PET_EAT : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_PET_EAT;
		public C2RM_PET_EAT()
		{			
		}
		public void init(int len)
        {			
			if (len >= 0)
            {
                uiPetId = new UInt64[len];
                for (int i = 0; i < len; i++)
                {
                    uiPetId[i] = new UInt64();
                }
            }			
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_PET_EAT();
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
		
		//初始化被吞噬卡牌的id
		public void initPetId(int len)
		{
			init(len);
		}
		
		/// 长度
		public int iCnt;					
		public UInt32 uiListen = new UInt32();
		/// 升级的卡牌
		public UInt64 uiPetUpId = new UInt64();
		/// 被吞噬的卡牌id
		public UInt64[] uiPetId;     		
    };
	
	[Serializable()]
    public class RM2C_PET_EAT : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_PET_EAT;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctPetId = new UInt64[len];
                for (int i = 0; i < len; i++)
                {
                    vctPetId[i] = new UInt64();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_PET_EAT();
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
		/// 消耗的钱，用加法
		public SMoney sMoney = new SMoney();
		/// 升级的卡牌信息
		public SPetEat sPetEat = new SPetEat();
		/// 被吞噬的卡牌
		public UInt64[] vctPetId;
    };
	
	[Serializable()]
    public class C2RM_PET_UP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_PET_UP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_PET_UP();
        }
		
		public void init(int len)
        {			
			if (len >= 0)
            {
                sCostInfo = new SCostInfo[len];
                for (int i = 0; i < len; i++)
                {
                    sCostInfo[i] = new SCostInfo();
                }
            }			
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
		///选择进化的卡牌
		public UInt64 uiPetId = new UInt64();
		///消耗的装备和卡牌信息
		public SCostInfo[] sCostInfo;
		
    };
	
	[Serializable()]
    public class RM2C_PET_UP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_PET_UP;

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
            return new RM2C_PET_UP();
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
		/// 消耗的钱，用加法
		public SMoney sMoney = new SMoney();
		/// 升级的卡牌信息
		public SPetEat sPetEat = new SPetEat();
		/// 消耗的物品信息
		public SCostInfo[] vctCostInfo;
    };
	
	[Serializable()]
    public class C2RM_SKILL_UP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_SKILL_UP;
		
		public void init(int len)
		{			
			if (len >= 0)
            {
                sCostInfo = new SCostInfo[len];
                for (int i = 0; i < len; i++)
                {
                    sCostInfo[i] = new SCostInfo();
                }
            }			
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_SKILL_UP();
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
		///要升级技能的卡牌id
		public UInt64 uiPetId = new UInt64();
		///要升级技能的csv id
		public UInt32 uiCsvSkillId = new UInt32();
		///要消耗的装备/卡牌信息
		public SCostInfo[] sCostInfo;
		
    };
	
	[Serializable()]
    public class RM2C_SKILL_UP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_SKILL_UP;

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
            return new RM2C_SKILL_UP();
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
		/// 消耗的钱，用加法
		public SMoney sMoney = new SMoney();
		/// 升级的卡牌信息
		public SSkillup sSkillUp = new SSkillup();
		/// 消耗的物品信息
		public SCostInfo[] vctCostInfo;
    };
	
	[Serializable()]
    public class C2RM_POWER_ADD : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_POWER_ADD;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_POWER_ADD();
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
    public class RM2C_POWER_ADD : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_POWER_ADD;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_POWER_ADD();
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
		/// 主角体力信息
		public SLeadPowerInfo sLeadPowerInfo = new SLeadPowerInfo();
    };
	
	[Serializable()]
    public class C2RM_PIECE : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_PIECE;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_PIECE();
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
    public class RM2C_PIECE : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_PIECE;

        private void init(int len)
        {
            if (len >= 0)
            {
                sPiece = new SPiece[len];
                for (int i = 0; i < len; i++)
                {
                    sPiece[i] = new SPiece();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_PIECE();
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
		/// 所有的卡牌碎片
		public SPiece[] sPiece;
    };
	
	[Serializable()]
    public class C2RM_PET_BAG_TO_BAR : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_PET_BAG_TO_BAR;
		
		public void init(int len)
		{			
			if (len >= 0)
            {
                uiPetId = new UInt64[len];
                for (int i = 0; i < len; i++)
                {
                    uiPetId[i] = new UInt64();
                }
            }			
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_PET_BAG_TO_BAR();
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
		/// 数量
		public int iCnt = new int();
        public UInt32 uiListen = new UInt32();
		///要移动的卡牌id
		public UInt64[] uiPetId;
    };
	
	[Serializable()]
    public class RM2C_PET_BAG_TO_BAR : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_PET_BAG_TO_BAR;

        private void init(int len)
        {
            if (len >= 0)
            {
                sPetLoc = new SPetLoc[len];
                for (int i = 0; i < len; i++)
                {
                    sPetLoc[i] = new SPetLoc();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_PET_BAG_TO_BAR();
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
		///移动的卡牌信息
		public SPetLoc[] sPetLoc;
    };
	
	[Serializable()]
    public class C2RM_PET_BAR_TO_BAG : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_PET_BAR_TO_BAG;
		
		public void init(int len)
		{			
			if (len >= 0)
            {
                uiPetId = new UInt64[len];
                for (int i = 0; i < len; i++)
                {
                    uiPetId[i] = new UInt64();
                }
            }			
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_PET_BAR_TO_BAG();
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
		/// 数量
		public int iCnt = new int();
        public UInt32 uiListen = new UInt32();
		///要移动的卡牌id
		public UInt64[] uiPetId;
    };
	
	[Serializable()]
    public class RM2C_PET_BAR_TO_BAG : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_PET_BAR_TO_BAG;

        private void init(int len)
        {
            if (len >= 0)
            {
                sPetLoc = new SPetLoc[len];
                for (int i = 0; i < len; i++)
                {
                    sPetLoc[i] = new SPetLoc();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_PET_BAR_TO_BAG();
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
		///移动的卡牌信息
		public SPetLoc[] sPetLoc;
    };
	
	[Serializable()]
    public class RM2C_POWER_INFO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_POWER_INFO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_POWER_INFO();
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

		/// 主角体力信息
		public SLeadPowerInfo sLeadPowerInfo = new SLeadPowerInfo();
    };
	
	[Serializable()]
    public class C2RM_TEAM_SET : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_TEAM_SET;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_TEAM_SET();
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
		///更新队伍信息
		public STeamUpdate sTeam = new STeamUpdate();
    };
	
	[Serializable()]
    public class RM2C_TEAM_SET : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_TEAM_SET;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_TEAM_SET();
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
		/// 队伍信息更新
		public STeamUpdate sTeam = new STeamUpdate();
    };
	
	[Serializable()]
    public class C2RM_EQUIP_PET_NOTIFY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_EQUIP_PET_NOTIFY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_EQUIP_PET_NOTIFY();
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
		public SPetEquip sPetEquip = new SPetEquip();
    };
	
	[Serializable()]
    public class RM2C_EQUIP_PET_NOTIFY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_EQUIP_PET_NOTIFY;

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
            return new RM2C_EQUIP_PET_NOTIFY();
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
		/// 卡牌装备信息
		public SPetEquip sPetEquip = new SPetEquip();
		///改变的装备信息,重新赋值就好了
		public SEquipment[] sEquip;
    };
	
	[Serializable()]
    public class C2RM_PIECE_TO_PET : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_PIECE_TO_PET;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_PIECE_TO_PET();
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
		///碎片的csv id
		public UInt32 uiPieceId = new UInt32();
		///消耗万能碎片的个数
		public int iPieceCnt = new int();
    };
	
	[Serializable()]
    public class RM2C_PIECE_TO_PET : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_PIECE_TO_PET;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_PIECE_TO_PET();
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
		///花的钱，用加法
		public SMoney costMoney = new SMoney();
		///生成的卡牌信息
		public SPetInfo sPet = new SPetInfo();
		///消耗的碎片信息,剩余的碎片个数
		public SPieceUpdate sPiece = new SPieceUpdate();
		///消耗万能碎片的数量
		public int iPieceCnt = new int();
    };
	
	[Serializable()]
    public class C2RM_PIECE_FIRST : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_PIECE_FIRST;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_PIECE_FIRST();
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
		
		/// 设置优先使用万能碎片，0：否 1：是
        public byte cPieceFirst = new byte();
    };
	
	[Serializable()]
    public class C2RM_PIECE_FROM_PET : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_PIECE_FROM_PET;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_PIECE_FROM_PET();
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
		///要分解的卡牌
		public UInt64 uiPetId = new UInt64();
    };
	
	[Serializable()]
    public class RM2C_PIECE_FROM_PET : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_PIECE_FROM_PET;

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
            return new RM2C_PIECE_FROM_PET();
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
		///分解的卡牌id server
		public UInt64 uiPetId = new UInt64();
		///生成的万能碎片
		public int iPieceCnt = new int();
		///脱下来的装备信息
		public SEquipment[] sEquip;
    };
	
	[Serializable()]
    public class C2RM_PET_SELL : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_PET_SELL;
		
		public void init(int len)
		{			
			if (len >= 0)
            {
                uiPetId = new UInt64[len];
                for (int i = 0; i < len; i++)
                {
                    uiPetId[i] = new UInt64();
                }
            }			
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_PET_SELL();
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
		/// 数量
		public int iCnt = new int();
        public UInt32 uiListen = new UInt32();
		///要出售的卡牌id server
		public UInt64[] uiPetId;
    };
	
	[Serializable()]
    public class RM2C_PET_SELL : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_PET_SELL;

        private void init(int len)
        {
            if (len >= 0)
            {
                uiPetId = new UInt64[len];
                for (int i = 0; i < len; i++)
                {
                    uiPetId[i] = new UInt64();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_PET_SELL();
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
		/// 卖的钱
		public SMoney sMoney = new SMoney();
		///卖的卡牌id server
		public UInt64[] uiPetId;
    };
	
	[Serializable()]
    public class C2RM_PET_SET_SKILL : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_PET_SET_SKILL;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_PET_SET_SKILL();
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
		///卡牌的id server
		public UInt64 uiPetId = new UInt64();
		///设置的技能的 id csv
		public UInt32 uiSkillId = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_PET_SET_SKILL : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_PET_SET_SKILL;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_PET_SET_SKILL();
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
		///设置技能的卡牌id server
		public UInt64 uiPetId = new UInt64();
		///技能csv id
		public UInt32 uiSkillId = new UInt32();
    };
	
	[Serializable()]
    public class C2RM_TEAM_WORK_SET : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_TEAM_WORK_SET;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_TEAM_WORK_SET();
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
		///队伍的 id server
		public UInt32 uiTeamId = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_TEAM_WORK_SET : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_TEAM_WORK_SET;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_TEAM_WORK_SET();
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
		///设置的队伍 id server
		public UInt32 uiTeamId = new UInt32();
    };
	
	[Serializable()]
    public class C2RM_FB_SET_FLAG : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FB_SET_FLAG;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_FB_SET_FLAG();
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
		
        public int iFbFlag = new int();
    };
	
	[Serializable()]
    public class C2RM_EQUIP_SELL : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_EQUIP_SELL;
		
		public void init(int len)
		{			
			if (len >= 0)
            {
                sEquip = new SEquipCsvIdCnt[len];
                for (int i = 0; i < len; i++)
                {
                    sEquip[i] = new SEquipCsvIdCnt();
                }
            }			
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_EQUIP_SELL();
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
		/// 数量
		public int iCnt = new int();
        public UInt32 uiListen = new UInt32();
		///要出售的物品的csv id和要出售的数量
		public SEquipCsvIdCnt[] sEquip;
    };
	
	[Serializable()]
    public class RM2C_EQUIP_SELL : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_EQUIP_SELL;

        private void init(int len)
        {
            if (len >= 0)
            {
                sEquip = new SEquipCsvIdCnt[len];
                for (int i = 0; i < len; i++)
                {
                    sEquip[i] = new SEquipCsvIdCnt();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_EQUIP_SELL();
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
		/// 卖的钱
		public SMoney sMoney = new SMoney();
		///卖出的物品的csv id和剩余的数量
		public SEquipCsvIdCnt[] sEquip;
    };
	
	[Serializable()]
    public class RM2C_FB_NOTIFY_INFO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FB_NOTIFY_INFO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FB_NOTIFY_INFO();
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
		
		/// 更新的副本信息
		public SFBInfo sFb = new SFBInfo();
    };
	
	[Serializable()]
    public class RM2C_SHOW_SWEEP_REWARD : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_SHOW_SWEEP_REWARD;

        private void init(int len)
        {
            if (len >= 0)
            {
                sEquip = new SEquipCsvIdCnt[len];
                for (int i = 0; i < len; i++)
                {
                    sEquip[i] = new SEquipCsvIdCnt();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_SHOW_SWEEP_REWARD();
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
		
		public SEquipCsvIdCnt[] sEquip;
    };
	
	[Serializable()]
    public class RM2C_SHOW_EQUIP_REWARD : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_SHOW_EQUIP_REWARD;

        private void init(int len)
        {
            if (len >= 0)
            {
                sEquip = new SEquipCsvIdCnt[len];
                for (int i = 0; i < len; i++)
                {
                    sEquip[i] = new SEquipCsvIdCnt();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_SHOW_EQUIP_REWARD();
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
		
		public SEquipCsvIdCnt[] sEquip;
    };
	
	[Serializable()]
    public class RM2C_SHOW_PIECE_REWARD : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_SHOW_PIECE_REWARD;

        private void init(int len)
        {
            if (len >= 0)
            {
                sPiece = new SPieceUpdate[len];
                for (int i = 0; i < len; i++)
                {
                    sPiece[i] = new SPieceUpdate();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_SHOW_PIECE_REWARD();
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
		
		
		public SPieceUpdate[] sPiece;
    };
	
	[Serializable()]
    public class RM2C_SHOW_PET_REWARD : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_SHOW_PET_REWARD;

        private void init(int len)
        {
            if (len >= 0)
            {
                uiPetId = new UInt32[len];
                for (int i = 0; i < len; i++)
                {
                    uiPetId[i] = new UInt32();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_SHOW_PET_REWARD();
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
		
		
		public UInt32[] uiPetId;
    };
	
	[Serializable()]
    public class C2RM_FB_SWEEP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FB_SWEEP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_FB_SWEEP();
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
		///要扫荡的副本csv id 
		public UInt32 uiFbId = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_FB_SWEEP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FB_SWEEP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FB_SWEEP();
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
		///副本的 csv id
		public UInt32 uiFbCsvId = new UInt32();
		///奖励的经验，只用来显示
		public UInt64 uiExp = new UInt64();
		///奖励的游戏币，只用来显示
		public UInt64 uiSMoney = new UInt64();
		///之前的经验值
		public UInt64 uiPreExp = new UInt64();
    };
	
	[Serializable()]
    public class RM2C_FB_NEW : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FB_NEW;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FB_NEW();
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
		
		public SFBInfo sFb = new SFBInfo();
    };
	
	[Serializable()]
    public class C2RM_LAND_LIST : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_LAND_LIST;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_LAND_LIST();
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
    public class RM2C_LAND_LIST : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_LAND_LIST;

        private void init(int len)
        {
            if (len >= 0)
            {
                sLand = new SLand[len];
                for (int i = 0; i < len; i++)
                {
                    sLand[i] = new SLand();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_LAND_LIST();
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
		
		
		public SLand[] sLand;
    };
	
	[Serializable()]
    public class C2RM_LAND_STAR_REWARD : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_LAND_STAR_REWARD;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_LAND_STAR_REWARD();
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
		///大陆的csv id
		public UInt32 uiLandCsvId = new UInt32();
		///奖励副本类型 1：普通 2：精英 3：挑战
		public byte cType = new byte();
    };
	
	[Serializable()]
    public class RM2C_LAND_STAR_REWARD : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_LAND_STAR_REWARD;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_LAND_STAR_REWARD();
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
		///结果
		public int iResult = new int();
		///奖励统计更新
		public SLandReward sLandReward = new SLandReward();
		///奖励的经验，只用来显示
		public UInt64 uiExp = new UInt64();
		///奖励的游戏币，只用来显示
		public UInt64 uiSMoney = new UInt64();
		///之前的经验值
		public UInt64 uiPreExp = new UInt64();
		///奖励的钻石
		public UInt32 uiQMoney = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_LAND_NEW : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_LAND_NEW;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_LAND_NEW();
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
		
		public SLand sLand = new SLand();
    };
	
	[Serializable()]
    public class C2RM_PET_PROTECT : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_PET_PROTECT;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_PET_PROTECT();
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
		
        ///要设置的卡牌id server
		public UInt64 uiPetId = new UInt64();
		///要设置的保护类型0:不保护 1：保护
		public byte isProtect = new byte();
    };
	
	[Serializable()]
    public class C2RM_FRIEND_ASK : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FRIEND_ASK;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_FRIEND_ASK();
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
        ///好友id
		public UInt32 uiFriendId = new UInt32();
		///好友类型 0 : 普通 1：基友
		public byte cType = new byte();
    };
	
	[Serializable()]
    public class RM2C_FRIEND_ASK : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_ASK;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_ASK();
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
		///好友id
		public UInt32 uiFriendId = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_FRIEND_ASK_NOTIFY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_ASK_NOTIFY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_ASK_NOTIFY();
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
		
		public SFriendAsk sFriendAsk = new SFriendAsk();
    };
	
	[Serializable()]
    public class C2RM_FRIEND_ASK_ANSWER : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FRIEND_ASK_ANSWER;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_FRIEND_ASK_ANSWER();
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
        ///好友id
		public UInt32 uiFriendId = new UInt32();
		///类型 0 : 拒绝 1：通过
		public byte cType = new byte();
    };
	
	[Serializable()]
    public class RM2C_FRIEND_ASK_ANSWER : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_ASK_ANSWER;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_ASK_ANSWER();
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
		///好友id, 不管是拒绝还是通过，都把这个从申请列表删除
		public UInt32 uiFriendId = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_FRIEND_NEW : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_NEW;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_NEW();
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
		
		/// 好友信息
		public SFriend sFriend = new SFriend();
    };
	
	[Serializable()]
    public class C2RM_FRIEND_DELETE : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FRIEND_DELETE;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_FRIEND_DELETE();
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
		
		
        ///要删除好友的id, 只发送不返回，默认100%删除成功
		public UInt32 uiFriendId = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_FRIEND_DELETE : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_DELETE;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_DELETE();
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
		
		/// 服务器通知不再是好友了，把这个人从好友列表删除
		public UInt32 uiFriendId = new UInt32();
    };
	
	[Serializable()]
    public class C2RM_FRIEND_NICK_NAME : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FRIEND_NICK_NAME;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_FRIEND_NICK_NAME();
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
		
		public void setNickName(string nickName)
        {
            byte[] Byte = System.Text.Encoding.UTF8.GetBytes(nickName);
            for (int i = 0; i < Byte.Length; i++)
            {
                cName[i] = Byte[i];
            }
        }
		
		public UInt32 uiListen = new UInt32();
		/// 好友id
		public UInt32 uiFriendId = new UInt32();
        ///好友别名
		public byte[] cName = new byte[32];
    };
	
	[Serializable()]
    public class RM2C_FRIEND_NICK_NAME : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_NICK_NAME;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_NICK_NAME();
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
		
		public string getName()
        {
            return System.Text.Encoding.UTF8.GetString(cName, 0, byteToStringIndex(cName));
        }
		
		public UInt32 uiListen = new UInt32();
		public int iResult = new int();
		/// 好友id
		public UInt32 uiFriendId = new UInt32();
		///备注
		public byte[] cName = new byte[32];
    };
	
	[Serializable()]
    public class C2RM_FRIEND_SEARCH : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FRIEND_SEARCH;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_FRIEND_SEARCH();
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
        ///要查找的好友id
		public UInt32 uiFriendId = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_FRIEND_SEARCH : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_SEARCH;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_SEARCH();
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
		///好友信息
		public SFriendClient sFriendClient = new SFriendClient();
    };
	
	[Serializable()]
    public class RM2C_FRIEND_ASK_NOTIFY_DELETE : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_ASK_NOTIFY_DELETE;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_ASK_NOTIFY_DELETE();
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
		
		/// 把这个好友从申请列表删除
		public UInt32 uiFriendId = new UInt32();
    };
	
	[Serializable()]
    public class C2RM_FRIEND_BUY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FRIEND_BUY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_FRIEND_BUY();
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
    public class RM2C_FRIEND_BUY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_BUY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_BUY();
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
		///买后的数量，直接赋值
		public int iFriendBuy = new int();
		/// 花的钱,用加法
		public SMoney sMoney = new SMoney();
    };
	
	[Serializable()]
    public class C2RM_FRIEND_POWER_SEND : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FRIEND_POWER_SEND;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_FRIEND_POWER_SEND();
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
		///送体力的好友id
		public UInt32 uiFriendId = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_FRIEND_POWER_SEND : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_POWER_SEND;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_POWER_SEND();
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
		///送体力好友id,重新赋值即可
		public UInt32[] uiFriendPower = new UInt32[7];
    };
	
	[Serializable()]
    public class RM2C_FRIEND_POWER_NEW : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_POWER_NEW;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_POWER_NEW();
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
		
		public SFriendPower sFriendPower = new SFriendPower();
    };
	
	[Serializable()]
    public class C2RM_FRIEND_POWER : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FRIEND_POWER;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_FRIEND_POWER();
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
    public class RM2C_FRIEND_POWER : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_POWER;

        private void init(int len)
        {
            if (len >= 0)
            {
                sFriendPower = new SFriendPower[len];
                for (int i = 0; i < len; i++)
                {
                    sFriendPower[i] = new SFriendPower();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_POWER();
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
		public SFriendPower[] sFriendPower;
    };
	
	[Serializable()]
    public class C2RM_FRIEND_POWER_GET : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FRIEND_POWER_GET;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_FRIEND_POWER_GET();
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
		///送体力的好友id
		public UInt32 uiFriendId = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_FRIEND_POWER_GET : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_POWER_GET;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_POWER_GET();
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
		///领取体力的好友id
		public UInt32 uiFriendId = new UInt32();
		///体力信息
		public SLeadPowerInfo sLeadPowerInfo = new SLeadPowerInfo();
    };
	
	[Serializable()]
    public class C2RM_PK_CHECK_TEAM : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_PK_CHECK_TEAM;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_PK_CHECK_TEAM();
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
    public class RM2C_PK_CHECK_TEAM : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_PK_CHECK_TEAM;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_PK_CHECK_TEAM();
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
		public STeam sTeam = new STeam();
    };
	
	[Serializable()]
    public class C2RM_FRIEND_BE_PK_LIST : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FRIEND_BE_PK_LIST;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_FRIEND_BE_PK_LIST();
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
    public class RM2C_FRIEND_BE_PK_LIST : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_BE_PK_LIST;

        private void init(int len)
        {
            if (len >= 0)
            {
                sFriendBePk = new SFriendBePk[len];
                for (int i = 0; i < len; i++)
                {
                    sFriendBePk[i] = new SFriendBePk();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_BE_PK_LIST();
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
		public SFriendBePk[] sFriendBePk;
    };
	
	[Serializable()]
    public class C2RM_FRIEND_ASK_LIST : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FRIEND_ASK_LIST;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_FRIEND_ASK_LIST();
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
    public class RM2C_FRIEND_ASK_LIST : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_ASK_LIST;

        private void init(int len)
        {
            if (len >= 0)
            {
                sFriendAsk = new SFriendAsk[len];
                for (int i = 0; i < len; i++)
                {
                    sFriendAsk[i] = new SFriendAsk();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_ASK_LIST();
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
		public SFriendAsk[] sFriendAsk;
    };
	
	[Serializable()]
    public class RM2C_FRIEND_NOTIFY_BE_PK : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_NOTIFY_BE_PK;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_NOTIFY_BE_PK();
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
		
		/// 被切磋或者被复仇了
		public SFriendBePk uiFriendBePk = new SFriendBePk();
    };
	
	[Serializable()]
    public class C2RM_ROLE_PSIGN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ROLE_PSIGN;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_ROLE_PSIGN();
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
		public void setPSign(string pSign)
        {
            byte[] Byte = System.Text.Encoding.UTF8.GetBytes(pSign);
            for (int i = 0; i < Byte.Length; i++)
            {
                cPSign[i] = Byte[i];
            }
        }
		
		public string getPSign()
        {
            return System.Text.Encoding.UTF8.GetString(cPSign, 0, IndexByteToString(cPSign));
        }
		
		public UInt32 uiListen = new UInt32();
		///个性签名
		public byte[] cPSign = new byte[130];
        
    };
	
	[Serializable()]
    public class RM2C_ROLE_PSIGN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ROLE_PSIGN;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_ROLE_PSIGN();
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
		
		public string getPSign()
        {
            return System.Text.Encoding.UTF8.GetString(cPSign, 0, IndexByteToString(cPSign));
        }
		
		public UInt32 uiListen = new UInt32();
		public int iResult = new int();
		///个性签名
		public byte[] cPSign = new byte[130];
    };
	
	[Serializable()]
    public class C2RM_BIND : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_BIND;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_BIND();
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
		///绑定账户信息
		public SAccountC2AC sSAccountC2AC = new SAccountC2AC();
    };
	
	[Serializable()]
    public class RM2C_BIND : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_BIND;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_BIND();
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

		public void setAccountIdStr(string psAccountId)
		{
			byte[] accountByte = System.Text.Encoding.UTF8.GetBytes(psAccountId);
			for (int i = 0; i < accountByte.Length; i++)
			{
				cAccountId[i] = accountByte[i];
			}
		}
		public string getAccountIdStr()
		{
			return System.Text.Encoding.UTF8.GetString(cAccountId, 0, byteToStringIndex(cAccountId));
		}

		public void setChannelIdStr(string psChannelId)
		{
			byte[] channelByte = System.Text.Encoding.UTF8.GetBytes(psChannelId);
			for (int i = 0; i < channelByte.Length; i++)
			{
				cChannelId[i] = channelByte[i];
			}
		}
		public string getChannelIdStr()
		{
			return System.Text.Encoding.UTF8.GetString(cChannelId, 0, byteToStringIndex(cChannelId));
		}
		
		public UInt32 uiListen = new UInt32();
		public int iResult = new int();
		///绑定的账号id
		///public UInt64 uiAccountId = new UInt64(); 	/// no use
		public byte[] cChannelId = new byte[32];
		public byte[] cAccountId= new byte[64];
		///是否绑定 0：否 1:是
		public byte isBind = new byte();
    };
	
	[Serializable()]
    public class C2RM_FRIEND_NOFRESH_INFO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FRIEND_NOFRESH_INFO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_FRIEND_NOFRESH_INFO();
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
		///好友id
		public UInt32 uiFriendId = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_FRIEND_NOFRESH_INFO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_NOFRESH_INFO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_NOFRESH_INFO();
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
		public SFriendNoFreshInfo sInfo = new SFriendNoFreshInfo();
    };
	
	[Serializable()]
    public class C2RM_FRIEND_FRESH_INFO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FRIEND_FRESH_INFO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_FRIEND_FRESH_INFO();
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
		///好友列表类型 0:好友 1：申请
		public byte cType = new byte();
        
    };
	
	[Serializable()]
    public class RM2C_FRIEND_FRESH_INFO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_FRESH_INFO;

        private void init(int len)
        {
            if (len >= 0)
            {
                sInfo = new SFriendFreshInfo[len];
                for (int i = 0; i < len; i++)
                {
                    sInfo[i] = new SFriendFreshInfo();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_FRESH_INFO();
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
		/// 类型0:好友 1：申请
		public byte cType = new byte();
		public SFriendFreshInfo[] sInfo;
    };
	
	[Serializable()]
    public class C2RM_FRIEND_PK : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FRIEND_PK;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_FRIEND_PK();
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
		///好友id
		public UInt32 uiFriendId = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_FRIEEND_PK : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEEND_PK;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEEND_PK();
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
		/// 战斗数据
		public SFriendPkFight sFriend = new SFriendPkFight();
		///花的钱, 加法
		public SMoney sMoney = new SMoney();
    };
	
	[Serializable()]
    public class C2RM_FRIEND_PK_BACK : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FRIEND_PK_BACK;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_FRIEND_PK_BACK();
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
		///好友id
		public UInt32 uiFriendId = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_FRIEND_PK_BACK : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_PK_BACK;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_PK_BACK();
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
		/// 战斗数据
		public SFriendPkFight sFriend = new SFriendPkFight();
    };
	
	[Serializable()]
    public class C2RM_FRIEND_PK_BEGIN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FRIEND_PK_BEGIN;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_FRIEND_PK_BEGIN();
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
		
		/// 好友id
		public UInt32 uiFriendId = new UInt32();
		//战斗开始时间戳
		public UInt32 uiBeginTime = new UInt32();	
		///自己卡牌站位信息
		public UInt64[] uiPetSelf = new UInt64[9];
		///对手卡牌站位信息
		public UInt64[] uiPetEnemy = new UInt64[9];
        
    };
	
	[Serializable()]
    public class C2RM_FRIEND_PK_MID : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FRIEND_PK_MID;
		public C2RM_FRIEND_PK_MID()
		{			
		}
		public void init(int len)
        {			
			if (len >= 0)
            {
                sFightRound = new SFightingRound[len];
                for (int i = 0; i < len; i++)
                {
                    sFightRound[i] = new SFightingRound();
                }
            }			
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_FRIEND_PK_MID();
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
		
		//初始化被吞噬卡牌的id
		public void initPetId(int len)
		{
			init(len);
		}
		
		/// 长度
		public int iCnt;					
		/// 序号
		public byte cNum = new byte();
		/// 战斗过程
		public SFightingRound[] sFightRound;
    };
	
	[Serializable()]
    public class C2RM_FRIEND_PK_OVER : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FRIEND_PK_OVER;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_FRIEND_PK_OVER();
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
		///结束的好友战斗类型 0:切磋 1：复仇
		public byte cType = new byte();
		///停止时间
		public float fStopTime = new float();
		///是否胜利
		public byte isWin = new byte();
    };
	
	[Serializable()]
    public class RM2C_FRIEND_PK_OVER : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_PK_OVER;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_PK_OVER();
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
		///切磋复仇信息
		public SLeadFriendPk sFriendPk = new SLeadFriendPk();
		///输赢后奖励的钱
		public SFriendPkReward pkReward = new SFriendPkReward();
    };
	
	[Serializable()]
    public class RM2C_FRIEND_PK_BACK_OVER : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_PK_BACK_OVER;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_PK_BACK_OVER();
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
		///好友id
		public UInt32 uiFriendId = new UInt32();
    };
	
	[Serializable()]
    public class C2RM_POWER_BUY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_POWER_BUY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_POWER_BUY();
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
    public class RM2C_POWER_BUY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_POWER_BUY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_POWER_BUY();
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
		///体力信息
		public SLeadPowerInfo sLeadPowerInfo = new SLeadPowerInfo();
		///花的钱， 加法
		public SMoney sMoney = new SMoney();
    };
	
	[Serializable()]
    public class C2RM_ROLE_NAME : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ROLE_NAME;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_ROLE_NAME();
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
		
		public void setName(string psName)
        {
            byte[] nameByte = System.Text.Encoding.UTF8.GetBytes(psName);
            for (int i = 0; i < nameByte.Length; i++)
            {
                cName[i] = nameByte[i];
            }
        }
		
		public UInt32 uiListen = new UInt32();
		///名字
		public byte[] cName = new byte[32];
    };
	
	[Serializable()]
    public class RM2C_ROLE_NAME : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ROLE_NAME;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_ROLE_NAME();
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
		
		public string getName()
        {
            return System.Text.Encoding.UTF8.GetString(cName, 0, byteToStringIndex(cName));
        }
		
		public UInt32 uiListen = new UInt32();
		/// 结果
		public int iResult = new int();
		///名字
		public byte[] cName = new byte[32];
    };
	
	[Serializable()]
    public class RM2C_TICK_BY_OTHER : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_TICK_BY_OTHER;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_TICK_BY_OTHER();
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
		
		public int iReason = new int();
    };
	
	[Serializable()]
    public class C2RM_NEW_GUIDE : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_NEW_GUIDE;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_NEW_GUIDE();
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
		
		public UInt64 uiNewGuide = new UInt64();
    };
	
	[Serializable()]
    public class C2RM_NAME_RAND : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_NAME_RAND;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_NAME_RAND();
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
    public class RM2C_NAME_RAND : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_NAME_RAND;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_NAME_RAND();
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
		
		public string getName()
        {
            return System.Text.Encoding.UTF8.GetString(cName, 0, byteToStringIndex(cName));
        }
		
		public UInt32 uiListen = new UInt32();
		/// 结果
		public int iResult = new int();
		///名字
		public byte[] cName = new byte[32];
    };
	
	[Serializable()]
    public class C2RM_LUCKY_SHOP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_LUCKY_SHOP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_LUCKY_SHOP();
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
		///0:钻石抽 1：钻石十连抽 2：友情抽 3：友情十连抽, 4:钻石免费抽 5：友情抽免费抽
		public byte cType = new byte();
        
    };
	
	[Serializable()]
    public class RM2C_LUCKY_SHOP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_LUCKY_SHOP;

        private void init(int len)
        {
            if (len >= 0)
            {
                iLuckyShopId = new int[len];
                for (int i = 0; i < len; i++)
                {
                    iLuckyShopId[i] = new int();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_LUCKY_SHOP();
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
		/// 1成功 
		public int iResult = new int();
		///0:钻石抽 1：钻石十连抽 2：友情抽 3：友情十连抽, 4:钻石免费抽 5：友情抽免费抽
		public byte cType = new byte();
		///消耗的钻石和友情点，用加法
		public SMoneyFriendShip sMoneyFriendShip = new SMoneyFriendShip();
		/// 抽奖后次数统计
		public SLeadLuckyShop sLuckyShop = new SLeadLuckyShop();
		/// 万能碎片数量，直接赋值
		public UInt32 uiPieceCnt = new UInt32();
		/// 抽奖获得的万能碎片数量，用来显示
		public UInt32 uiPieceAdd = new UInt32();
		/// 抽出来的id
		public int[] iLuckyShopId;
    };
	
	[Serializable()]
    public class C2RM_FIRST_GUIDE : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FIRST_GUIDE;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_FIRST_GUIDE();
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
		
		///新手非强制引导
		public UInt64 uiFristGuide = new UInt64();
        
    };
	
	[Serializable()]
    public class C2RM_FB_RESET : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FB_RESET;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_FB_RESET();
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
		///要重置的副本 csv id
		public UInt32 uiFbCsvId = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_FB_RESET : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FB_RESET;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FB_RESET();
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
		///花的钱，用加法
		public SMoney sMoney = new SMoney();
		///修改后的副本信息
		public SFBInfo sFb = new SFBInfo();
		
    };
	
	[Serializable()]
    public class C2RM_STONE : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_STONE;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_STONE();
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
    public class RM2C_STONE : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_STONE;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_STONE();
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
		///花的钱元宝和得到的游戏币，都用加法
		public SMoney sMoney = new SMoney();
		///今天用的点金石次数
		public short sStoneTimes = new short();
		///暴击的倍数1-10
		public int iProb = new int();
		
    };
    
	[Serializable()]
    public class C2RM_BAG_BUY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_BAG_BUY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_BAG_BUY();
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
		///0-卡牌背包 1-卡牌仓库 2-装备背包 3-材料背包 
		public byte cType = new byte();
		
    };
	
	[Serializable()]
    public class RM2C_BAG_BUY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_BAG_BUY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_BAG_BUY();
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
		///花的钱元宝和游戏币，都用加法
		public SMoney sMoney = new SMoney();
		///买的背包
		public SLeadBagInfo sLeadBag = new SLeadBagInfo();
		///0-卡牌背包 1-卡牌仓库 2-装备背包 3-材料背包 
		public byte cType = new byte();
    };
	
	[Serializable()]				
    public class C2AC_MAC_ID : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2AC_MAC_ID;

        public C2AC_MAC_ID()
        {
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2AC_ACCOUNT_INFO();
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
    public class AC2C_MAC_ID : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_AC2C_MAC_ID;

        public AC2C_MAC_ID()
        {
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new AC2C_MAC_ID();
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
		public byte[] cDeviceInfo = new byte[150];
    };
	
	[Serializable()]
    public class C2RM_SERVER_STOP_NOTE : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_SERVER_STOP_NOTE;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_SERVER_STOP_NOTE();
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
		
		/// 随便加个字段
		public int iReason = new int();
    };
	
	[Serializable()]
    public class RM2C_SERVER_STOP_NOTE : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_SERVER_STOP_NOTE;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_SERVER_STOP_NOTE();
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

		
		public int iReason = new int();		
    };
	
	[Serializable()]
    public class C2RM_GUIDE_DDN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_GUIDE_DDN;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_GUIDE_DDN();
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
		
		/// DDN引导
		public UInt64 uiGuideDDN = new UInt64();
    };
	
	[Serializable()]
    public class C2RM_ADMIN_REWARD : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ADMIN_REWARD;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_ADMIN_REWARD();
        }
		
		public void init(int len)
        {			
			if (len >= 0)
            {
                uiEmailId = new UInt32[len];
                for (int i = 0; i < len; i++)
                {
                    uiEmailId[i] = new UInt32();
                }
            }			
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
		///奖励角色id
		public UInt32 uiRoleId = new UInt32();
		/// 发奖励的时间
		public UInt32 uiTime = new UInt32();
		/// 是否列表开始
		public byte cIsBegin = new byte();
		/// 是否结束
		public byte cIsOver = new byte();
		///奖励的邮件列表
		public UInt32[] uiEmailId;
    };
	
	[Serializable()]
    public class C2RM_POWER_MEAL : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_POWER_MEAL;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_POWER_MEAL();
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
		
		/// 随便加个字段
		public UInt32 uiListen = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_POWER_MEAL : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_POWER_MEAL;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_POWER_MEAL();
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
		/// 领取的体力
		public UInt32 uiPowerAdd = new UInt32();
		///新的体力信息
		public SLeadPowerInfo sLeadPower = new SLeadPowerInfo();
    };
	
	[Serializable()]
    public class C2RM_POWER_MEAL_TIME : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_POWER_MEAL_TIME;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_POWER_MEAL_TIME();
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
		
		/// 随便加个字段
		public UInt32 uiListen = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_POWER_MEAL_TIME : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_POWER_MEAL_TIME;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_POWER_MEAL_TIME();
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
		///新的体力信息
		public SLeadPowerInfo sLeadPower = new SLeadPowerInfo();
    };
	
	[Serializable()]
	public class C2RM_CHAT : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_CHAT;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_CHAT();
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

		/// 随便加个字段
		public UInt32 uiListen = new UInt32();
		/// 聊天内容
		public SChatItem sChatItem = new SChatItem();
	};

	[Serializable()]
	public class RM2C_CHAT : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_CHAT;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_CHAT();
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
		public int iQMoney = new int();
		/// 聊天内容
		public SChatItem sChatItem = new SChatItem();
	};

	[Serializable()]
	public class C2RM_CHAT_RENCENT_REQUEST : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_CHAT_RECENT_REQUEST;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_CHAT_RENCENT_REQUEST();
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
		
		/// 随便加个字段
		public UInt32 uiListen = new UInt32();
		/// 请求聊天的内容的类型
		public byte cType = new byte();
	};

	[Serializable()]
	public class RM2C_CHAT_RECENT_RESPONSE : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_CHAT_RECENT_RESPONSE;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_CHAT_RECENT_RESPONSE();
		}

		public void init(int len)
		{			
			if (len >= 0)
			{
				for (int i = 0; i < len; i++)
				{
					sChatItem[i] = new SChatItem();
				}
			}			
		}
		
		public ushort Message
		{
			get { return (ushort)OPCODE; }
		}
		
		public bool analysisBuffer(byte[] Buffer)
		{
			init(10);
			FromByteArrayNew(Buffer, this);
			return true;
		}
		
		public byte[] getBuffer()
		{
			return null;
		}
		
		public UInt32 uiListen = new UInt32();
		public int iResult = new int();

		/// 请求聊天的内容的类型
		public byte cType = new byte();
		public UInt32 uiChatCnt = new UInt32();
		/// 聊天内容
		public SChatItem[] sChatItem = new SChatItem[10];
	};
}