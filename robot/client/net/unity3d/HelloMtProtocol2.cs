using System;

using System.IO;

using ExFormatter;
using System.Diagnostics;

namespace net.unity3d
{
	[Serializable()]
    public class C2RM_CHECK_FB_PK_BEGIN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_CHECK_FB_PK_BEGIN;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_CHECK_FB_PK_BEGIN();
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
		
		///副本csv id
        public UInt32 uiIdCsvFb = new UInt32();
		///副本怪物波次
		public UInt16 uiBatchFb = new UInt16();
		//战斗开始时间戳
		public UInt32 uiBeginTime = new UInt32();
    };
	
	[Serializable()]
    public class C2RM_CHECK_FB_PK_MID : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_CHECK_FB_PK_MID;


        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_CHECK_FB_PK_MID();
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
    public class C2RM_CHECK_FB_PK_OVER : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_CHECK_FB_PK_OVER;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_CHECK_FB_PK_OVER();
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
		//己方死亡英雄数
		public int iCntDead = new int();					
    };
	
	[Serializable()]
    public class RM2C_CHECK_FB_PK : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_CHECK_FB_PK;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_CHECK_FB_PK();
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
    };
	
	[Serializable()]
    public class RM2C_FB_REWARD : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FB_REWARD;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctSReward = new SRewardMaterialDB[len];
                for (int i = 0; i < len; i++)
                {
                    vctSReward[i] = new SRewardMaterialDB();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FB_REWARD();
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
		public UInt32 uiMasterId = new UInt32();
		///副本csv id TODO: not use
		public UInt32 uiIdCsvFb = new UInt32();
		///副本波次
		public UInt64 luiExp = new UInt64();
		///主角id
		public UInt64 luiMoeny = new UInt64();
		/// 获得的友情点, 加法加上
		public UInt32 uiFriendShip = new UInt32();
		/// 升级给的体力
		public UInt16 uiPowerLv = new UInt16();
		///副本奖励
		public SRewardMaterialDB[] vctSReward;
    };
	
	[Serializable()]
    public class RM2C_CREATE_PET : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_CREATE_PET;

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
            return new RM2C_CREATE_PET();
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
		public SPetInfo[] vctSPetInfo;

    };
	
	[Serializable()]
    public class RM2C_CREATE_EQUIP : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_CREATE_EQUIP;

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
            return new RM2C_CREATE_EQUIP();
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
		public SEquipment[] sEquip;
    };
	
	[Serializable()]
    public class RM2C_CREATE_PIECE : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_CREATE_PIECE;

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
            return new RM2C_CREATE_PIECE();
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
		public SPiece[] vctSPiece;
    };
	
	[Serializable()]
    public class RM2C_RELOGIN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_RELOGIN;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_RELOGIN();
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
    public class C2RM_RE_PK_FB : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_RE_PK_FB;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_RE_PK_FB();
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
    public class RM2C_RE_PK_FB : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_RE_PK_FB;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_RE_PK_FB();
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
    };
	
	[Serializable()]
    public class C2RM_PING_PRO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_PING_PRO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_PING_PRO();
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
		
		///客户端监听
        public UInt32 uiListen = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_PING_PRO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_PING_PRO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_PING_PRO();
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

        public UInt32 uiCurTime = new UInt32();
    };
	
	[Serializable()]
    public class C2RM_PING_PRO_TWO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_PING_PRO_TWO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_PING_PRO_TWO();
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
		
		///客户端监听
        public UInt32 uiListen = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_PING_PRO_TWO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_PING_PRO_TWO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_PING_PRO_TWO();
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

        public UInt32 uiCurTime = new UInt32();
    };
	
	[Serializable()]
    public class LS2C_SERVER_STATE : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_LS2C_SERVER_STATE;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new LS2C_SERVER_STATE();
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
		public UInt32 uiServerTime = new UInt32();
		public UInt32 uiOpenTime = new UInt32();
    };
	
	[Serializable()]
    public class C2RM_BADGE_SHOP_BUY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_BADGE_SHOP_BUY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_BADGE_SHOP_BUY();
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
		///商店id
		public UInt32 uiId = new UInt32();
    };
	
	public class RM2C_BADGE_SHOP_BUY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_BADGE_SHOP_BUY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_BADGE_SHOP_BUY();
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
		///商店id
		public UInt32 uiId = new UInt32();
		///正义徽章消耗
		public int iBadgeCost = new int();
		///新买的物品数量更新
		public SEquipment SEquip = new SEquipment();	
		///新买的碎片数量更新
		public SPiece SPiece = new SPiece();
    };
	
	public class RM2C_GET_PHONE_INFO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_GET_PHONE_INFO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_GET_PHONE_INFO();
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
		
		public UInt32 uiIdMaster = new UInt32();
    };
	
	[Serializable()]
    public class C2RM_GET_PHONE_INFO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_GET_PHONE_INFO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_GET_PHONE_INFO();
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
		
		public void setSource(string psSource)
        {
            byte[] Byte = System.Text.Encoding.UTF8.GetBytes(psSource);
            for (int i = 0; i < Byte.Length; i++)
            {
                vctSource[i] = Byte[i];
            }
        }
        public void setAffiliate(string psAffiliate)
        {
            byte[] Byte = System.Text.Encoding.UTF8.GetBytes(psAffiliate);
            for (int i = 0; i < Byte.Length; i++)
            {
                vctAffiliate[i] = Byte[i];
            }
        }
        public void setIMEI(string IMEI)
        {
            byte[] Byte = System.Text.Encoding.UTF8.GetBytes(IMEI);
            for (int i = 0; i < Byte.Length; i++)
            {
                vctIMEI[i] = Byte[i];
            }
        }
		public void setCreative(string Creative)
        {
            byte[] Byte = System.Text.Encoding.UTF8.GetBytes(Creative);
            for (int i = 0; i < Byte.Length; i++)
            {
                vctCreative[i] = Byte[i];
            }
        }
		
		///手机品牌
        public byte[] vctSource = new byte[150];
		///手机型号
		public byte[] vctAffiliate = new byte[150];
		///手机识别码
		public byte[] vctIMEI = new byte[150];
		///设备操作系统版本号
		public byte[] vctCreative = new byte[150];
    };
	
		
	[Serializable()]
    public class RM2C_TEAM_EXP_ADD : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_TEAM_EXP_ADD;

        private void init(int len)
        {
            if (len >= 0)
            {
                vctPet = new SPetExpAdd[len];
                for (int i = 0; i < len; i++)
                {
                    vctPet[i] = new SPetExpAdd();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_TEAM_EXP_ADD();
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
		
		///队伍id
		public UInt32 uiIdTeam = new UInt32();
		///副本奖励
		public SPetExpAdd[] vctPet;
    };
	
	[Serializable()]
    public class C2RM_LOAD_STATUS : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_LOAD_STATUS;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_LOAD_STATUS();
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
		///1:loading界面，2：创建角色界面 3:登录成功
        public byte cType = new byte();
		///load 百分比1：25% 2：50% 3：75% 4：100%
		public int iLoadPersent = new int();
    };
	
	[Serializable()]
    public class C2RM_SUDDEN_OUT : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_SUDDEN_OUT;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_SUDDEN_OUT();
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
		/// <summary>
		/// 0: normal out
		/// 1: sudden out
		/// </summary>
        public UInt32 m_uiListen = new UInt32();

    };
	
	[Serializable()]
    public class C2RM_MIN_NIGHT_REFRESH : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MIN_NIGHT_REFRESH;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_MIN_NIGHT_REFRESH();
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
        public UInt32 m_uiListen = new UInt32();

    };
	
	public class RM2C_MIN_NIGHT_REFRESH : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MIN_NIGHT_REFRESH;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_MIN_NIGHT_REFRESH();
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
		public UInt32 uiServerTime = new UInt32();
		public UInt32 uiUpdateTime = new UInt32();
		public SMidNightInfo SMidNightInfo = new SMidNightInfo();
    };

	[Serializable()]
	public class C2RM_GET_ACTIVITY_INFO : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_GET_ACTIVITY_INFO;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_GET_ACTIVITY_INFO();
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
	public class RM2C_GET_ACTIVITY_INFO : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_GET_ACTIVITY_INFO;
		
		private void init(int len)
		{	
			if (len >= 0)
			{
				vctRewardInfo = new SActReward[len];
				for (int i = 0; i < len; i++)
				{
					vctRewardInfo[i] = new SActReward();
				}
			}
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_GET_ACTIVITY_INFO();
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

		public string getActName()
		{
			return System.Text.Encoding.UTF8.GetString(cActName, 0, IndexByteToString(cActName));
		}
		
		public string getActDes()
		{
			return System.Text.Encoding.UTF8.GetString(cActDes, 0, IndexByteToString(cActDes));
		}
		
		public UInt32 uiListen = new UInt32();
		public int iResult = new int();
		///类型
		public UInt16 usType = new UInt16();
		///开始时间
		public UInt32 uiStartTime = new UInt32();	
		///结束时间
		public UInt32 uiEndTime = new UInt32();	
		///活动条件，根据类型解析
		public byte[] arrActCondition = new byte[12];	
		///活动名字(20个汉字)
		public byte[] cActName = new byte[64];	
		///活动描述(50个汉字)
		public byte[] cActDes = new byte[151];	
		///角色完成情况
		public UInt32 uiPersonInfo1 = new UInt32();	
		///角色领奖情况或当前数据	
		public UInt32 uiPersonInfo2 = new UInt32();			
		////奖励
		public SActReward[] vctRewardInfo;
	};

	[Serializable()]
	public class C2RM_GET_ACTIVITY_REWARD : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ACTIVITY_REWARD;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_GET_ACTIVITY_REWARD();
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
		///类型
		public UInt16 usType = new UInt16();
		///要领取的奖励，根据活动类型赋值
		public UInt32 uiRewardTag = new UInt32();
	};

	[Serializable()]
	public class RM2C_GET_ACTIVITY_REWARD : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_GET_ACTIVITY_REWARD;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_GET_ACTIVITY_REWARD();
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
		///类型
		public UInt16 usType = new UInt16();
		///角色完成情况
		public UInt32 uiPersonInfo1 = new UInt32();	
		///角色领奖情况或当前数据	
		public UInt32 uiPersonInfo2 = new UInt32();
		///获得的金币
		public UInt32 uiGold = new UInt32();
		///获得的经验
		public UInt32 uiExp = new UInt32();
		///获得的钻石
		public UInt32 uiDiamond = new UInt32();				
	};

	[Serializable()]
	public class C2RM_GET_FIRST_DAY_BUY_POWER_ACT_INFO : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_GET_FIRST_DAY_BUY_POWER_INFO;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_GET_FIRST_DAY_BUY_POWER_ACT_INFO();
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
	public class RM2C_FIRST_DAY_BUY_POWER_ACT_INFO : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FIRST_DAY_BUY_POWER_ACT_INFO;
		
		private void init(int len)
		{	
			if (len >= 0)
			{
				vctRewardInfo = new SActReward[len];
				for (int i = 0; i < len; i++)
				{
					vctRewardInfo[i] = new SActReward();
				}
			}
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_FIRST_DAY_BUY_POWER_ACT_INFO();
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
		
		public string getActName()
		{
			return System.Text.Encoding.UTF8.GetString(cActName, 0, IndexByteToString(cActName));
		}
		
		public string getActDes()
		{
			return System.Text.Encoding.UTF8.GetString(cActDes, 0, IndexByteToString(cActDes));
		}
		
		public UInt32 uiListen = new UInt32();
		public int iResult = new int();
		///开始时间
		public UInt32 uiStartTime = new UInt32();	
		///活动目标购买体力值
		public UInt32 uiTarPowerBuy = new UInt32();	
		///当前购买体力值
		public UInt32 uiCurPowerBuy = new UInt32();				
		///活动名字(20个汉字)
		public byte[] cActName = new byte[64];	
		///活动描述(50个汉字)
		public byte[] cActDes = new byte[151];	
		///是否领奖	
		public byte ucIsReward = new byte();				
		////奖励
		public SActReward[] vctRewardInfo;
	};
	
	[Serializable()]
	public class C2RM_GET_FIRST_DAY_BUY_POWER_REWARD : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_GET_FIRST_DAY_BUY_POWER_REWARD;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_GET_FIRST_DAY_BUY_POWER_REWARD();
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
	public class RM2C_FIRST_DAY_BUY_POWER_ACT_REWARD : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FIRST_DAY_BUY_POWER_ACT_REWARD;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_FIRST_DAY_BUY_POWER_ACT_REWARD();
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
		///获得的金币
		public UInt32 uiGold = new UInt32();
		///获得的经验
		public UInt32 uiExp = new UInt32();
		///获得的钻石
		public UInt32 uiDiamond = new UInt32();				
	};

	[Serializable()]
	public class RM2C_SEND_SERVER_ID : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_SEND_SERVER_ID;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_SEND_SERVER_ID();
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
		
		public UInt32 uiIdServer = new UInt32();			
	};

	[Serializable()]
	public class RM2C_STOP_INFO : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_STOP_INFO;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_STOP_INFO();
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

		///封停信息类型0登录成功后通知，1封停信息变动通知（需要弹窗），2被禁止登录通知（需要弹窗）
		public UInt32 uiTypeInfo = new UInt32();
		///玩家id
		public UInt32 uiIdMaster = new UInt32();
		///禁言信息
		public SStopBaseInfo STalkStop = new SStopBaseInfo();
		///封停信息
		public SStopBaseInfo SLoginStop = new SStopBaseInfo();
	};

	
}