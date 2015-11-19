using System;

using System.IO;

using ExFormatter;
using System.Diagnostics;

namespace net.unity3d
{
	[Serializable()]
	public class C2RM_ESCORT_INFO : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_INFO;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_ESCORT_INFO();
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
	public class RM2C_ESCORT_INFO : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_INFO;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_ESCORT_INFO();
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
		///护送玩家基本信息
		public SEscortInfoBase EscortInfo = new SEscortInfoBase();
	};

	[Serializable()]
	public class C2RM_ESCORT_GROUP_LIST : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_GROUP_LIST;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_ESCORT_GROUP_LIST();
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
	public class RM2C_EESCORT_GROUP_LIST : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_EESCORT_GROUP_LIST;
		
		private void init(int len)
		{
			if (len >= 0)
			{
				vctSInfo = new SEscortGroupBase[len];
				for (int i = 0; i < len; i++)
				{
					vctSInfo[i] = new SEscortGroupBase();
				}
			}
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_EESCORT_GROUP_LIST();
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
		public SEscortGroupBase[] vctSInfo;
	};

	[Serializable()]
	public class C2RM_ESCORT_GET_GROUP : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_GET_GROUP;
		public void init(int len)
		{			
			if (len >= 0)
			{
				vctId = new SEscortSearchBase[len];
				for (int i = 0; i < len; i++)
				{
					vctId[i] = new SEscortSearchBase();
				}
			}			
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_ESCORT_GET_GROUP();
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
		/// 被吞噬的卡牌id
		public SEscortSearchBase[] vctId;     		
	};
	
	[Serializable()]
	public class RM2C_ESCORT_GET_GROUP : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_GET_GROUP;
		
		private void init(int len)
		{
			if (len >= 0)
			{
				vctInfo = new SEscortGroup[len];
				for (int i = 0; i < len; i++)
				{
					vctInfo[i] = new SEscortGroup();
				}
			}
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_ESCORT_GET_GROUP();
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
		///商队信息
		public SEscortGroup[] vctInfo;
	};

	[Serializable()]
    public class C2RM_ESCORT_GET_TEAM : ExFormatterBinary, IProtocal
    {
        //补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_GET_TEAM;

        public static IProtocal Create( ushort msg, HeaderBase h )
        {
            return new C2RM_ESCORT_GET_TEAM();
        }

        public ushort Message
        {
            get
            {
                return ( ushort ) OPCODE;
            }
        }

        public bool analysisBuffer( byte[] Buffer )
        {
            return false;
        }

        public byte[] getBuffer()
        {
            byte[] buffer = ToByteArray();
            return buffer;
        }

        public UInt32 uiListen = new UInt32();
        public SEscortSearchBase STeam = new SEscortSearchBase();
    };

    [Serializable()]
    public class RM2C_ESCORT_GET_TEAM : ExFormatterBinary, IProtocal
    {
        //补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_GET_TEAM;

        public static IProtocal Create( ushort msg, HeaderBase h )
        {
            return new RM2C_ESCORT_GET_TEAM();
        }

        public ushort Message
        {
            get
            {
                return ( ushort ) OPCODE;
            }
        }

        public bool analysisBuffer( byte[] Buffer )
        {
            FromByteArrayNew( Buffer, this );
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }

        public UInt32 uiListen = new UInt32();
        public int iResult = new int();
        ///护送玩家基本信息
        public SEscortRoleInfoPks EscortInfo = new SEscortRoleInfoPks();
    };

	[Serializable()]
	public class C2RM_ESCORT_BEAT_SELF_PET : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_BEAT_SELF_PET;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_ESCORT_BEAT_SELF_PET();
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
	public class RM2C_ESCORT_BEAT_SELF_PET : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_BEAT_SELF_PET;
		
		private void init(int len)
		{
			if (len >= 0)
			{
				sMotPet = new SMotPet[len];
				for (int i = 0; i < len; i++)
				{
					sMotPet[i] = new SMotPet();
				}
			}
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_ESCORT_BEAT_SELF_PET();
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
		/// 卡牌出战信息
		public SMotPet[] sMotPet;
	};

	[Serializable()]
	public class C2RM_ESCORT_BEAT_SELF_BEAST : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_BEAT_SELF_BEAST;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_ESCORT_BEAT_SELF_BEAST();
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
	public class RM2C_ESCORT_BEAT_SELF_BEAST : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_BEAT_SELF_BEAST;
		
		private void init(int len)
		{
			if (len >= 0)
			{
				sMotBeast = new SMotBeast[len];
				for (int i = 0; i < len; i++)
				{
					sMotBeast[i] = new SMotBeast();
				}
			}
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_ESCORT_BEAT_SELF_BEAST();
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
		/// 卡牌出战信息
		public SMotBeast[] sMotBeast;
	};

	[Serializable()]
    public class C2RM_ESCORT_BEAT_GROUP : ExFormatterBinary, IProtocal
    {
        //补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_BEAT_GROUP;

        public static IProtocal Create( ushort msg, HeaderBase h )
        {
            return new C2RM_ESCORT_BEAT_GROUP();
        }

        public ushort Message
        {
            get
            {
                return ( ushort ) OPCODE;
            }
        }

        public bool analysisBuffer( byte[] Buffer )
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
    public class RM2C_ESCORT_BEAT_GROUP : ExFormatterBinary, IProtocal
    {
        //补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_BEAT_GROUP;

        public static IProtocal Create( ushort msg, HeaderBase h )
        {
            return new RM2C_ESCORT_BEAT_GROUP();
        }

        public ushort Message
        {
            get
            {
                return ( ushort ) OPCODE;
            }
        }

        public bool analysisBuffer( byte[] Buffer )
        {
            FromByteArrayNew( Buffer, this );
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }

        public UInt32 uiListen = new UInt32();
        public int iResult = new int();
        ///正在进攻的商队信息
        public SEscortGroup EscortInfo = new SEscortGroup();
        ///可获得货币
        public SMoney Money = new SMoney();
        ///可获得药水
        public SEquipment Equ = new SEquipment();
    };


	[Serializable()]
    public class C2RM_ESCORT_BEAT_TEAM : ExFormatterBinary, IProtocal
    {
        //补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_BEAT_TEAM;

        public static IProtocal Create( ushort msg, HeaderBase h )
        {
            return new C2RM_ESCORT_BEAT_TEAM();
        }

        public ushort Message
        {
            get
            {
                return ( ushort ) OPCODE;
            }
        }

        public bool analysisBuffer( byte[] Buffer )
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
    public class RM2C_ESCORT_BEAT_TEAM : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_BEAT_TEAM;

        private void init( int len )
        {
            if( len >= 0 )
            {
                vctInfo = new SEscortTeamFight[ len ];
                for( int i = 0; i < len; i++ )
                {
                    vctInfo[ i ] = new SEscortTeamFight();
                }
            }
        }

        public static IProtocal Create( ushort msg, HeaderBase h )
        {
            return new RM2C_ESCORT_BEAT_TEAM();
        }

        public ushort Message
        {
            get
            {
                return ( ushort ) OPCODE;
            }
        }

        public bool analysisBuffer( byte[] Buffer )
        {
            MemoryStream msBuff = new MemoryStream( Buffer );
            BinaryReader br = new BinaryReader( msBuff );
            UInt32 len = br.ReadUInt32();
            init( ( int ) len );
            MemoryStream realBuff = new MemoryStream( Buffer, 4, Buffer.Length - 4 );
            ///USerialize.ConvertBytesToClass(realBuff);
            byte[] realData = realBuff.ToArray();
            FromByteArray( realData, this );
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }

        public UInt32 uiListen = new UInt32();
        public int iResult = new int();
        /// 卡牌出战信息
        public SEscortTeamFight[] vctInfo;
    };

	[Serializable()]
    public class C2RM_ESCORT_FIND_GROUP : ExFormatterBinary, IProtocal
    {
        //补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_FIND_GROUP;

        public static IProtocal Create( ushort msg, HeaderBase h )
        {
            return new C2RM_ESCORT_FIND_GROUP();
        }

        public ushort Message
        {
            get
            {
                return ( ushort ) OPCODE;
            }
        }

        public bool analysisBuffer( byte[] Buffer )
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
    public class RM2C_ESCORT_FIND_GROUP : ExFormatterBinary, IProtocal
    {
        //补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_FIND_GROUP;

        public static IProtocal Create( ushort msg, HeaderBase h )
        {
            return new RM2C_ESCORT_FIND_GROUP();
        }

        public ushort Message
        {
            get
            {
                return ( ushort ) OPCODE;
            }
        }

        public bool analysisBuffer( byte[] Buffer )
        {
            FromByteArrayNew( Buffer, this );
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }

        public UInt32 uiListen = new UInt32();
        public int iResult = new int();
        ///花费的货币
        public SMoney SCostMoney = new SMoney();
        ///可获得货币
        public SMoney Money = new SMoney();
        ///可获得药水
        public SEquipment Equ = new SEquipment();
        ///护送玩家基本信息
        public SEscortGroup EscortGroup = new SEscortGroup();
        ///自己的护送基本信息
        public SEscortInfoBase EscortInfo = new SEscortInfoBase();
    };

	[Serializable()]
	public class C2RM_ESCORT_SET_SELF_TEAM : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_SET_SELF_TEAM;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_ESCORT_SET_SELF_TEAM();
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
		public SEscortTeamUpdate TeamUpdate = new SEscortTeamUpdate();
	};
	
	[Serializable()]
	public class RM2C_ESCORT_SET_SELF_TEAM : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_SET_SELF_TEAM;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_ESCORT_SET_SELF_TEAM();
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
		public UInt32 uiValueFight = new UInt32();
		///更新队伍信息
		public SEscortTeamUpdate TeamUpdate = new SEscortTeamUpdate();
		///商队开始时间
		public UInt32 uiGroupTime = new UInt32();
		///队伍开始时间
		public UInt32 uiTeamTime = new UInt32();
	};

	[Serializable()]
	public class C2RM_ESCORT_GO_TO : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_GO_TO;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_ESCORT_GO_TO();
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
		public UInt64 luiIdTeam = new UInt64();
	};
	
	[Serializable()]
	public class RM2C_ESCORT_GO_TO : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_GO_TO;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_ESCORT_GO_TO();
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
		///护送基本信息
		public SEscortInfoBase SInfo = new SEscortInfoBase();			
	};

	[Serializable()]
	public class C2RM_ESCORT_LEAVE : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_LEAVE;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_ESCORT_LEAVE();
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
	public class RM2C_ESCORT_LEAVE : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_LEAVE;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_ESCORT_LEAVE();
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
	};

	[Serializable()]
	public class C2RM_ESCORT_HELPER : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_HELPER;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_ESCORT_HELPER();
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
		///选择的佣兵id server
		public UInt32 uiRoleId = new UInt32();
	};
	
	
	[Serializable()]
	public class RM2C_ESCORT_HELPER : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_HELPER;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_ESCORT_HELPER();
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
		
		/// 金币
		public UInt32 uiListen = new UInt32();
		///结果
		public int iResult = new int();
		///花的钱 加法
		public SMoney sMoney = new SMoney();
		///佣兵信息
		public SMotHelper sMotHelper = new SMotHelper();
	};
	
	[Serializable()]
	public class C2RM_ESCORT_HELPER_INFO : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_HELPER_INFO;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_ESCORT_HELPER_INFO();
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
	public class RM2C_ESCORT_HELPER_INFO : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_HELPER_INFO;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_ESCORT_HELPER_INFO();
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
		///佣兵信息
		public SMotHelper sMotHelper = new SMotHelper();
	};

	[Serializable()]
	public class C2RM_ESCORT_BEAT_GROUP_ACT : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_BEAT_GROUP_ACT;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_ESCORT_BEAT_GROUP_ACT();
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
		public SEscortSearchBase EscortGroupId = new SEscortSearchBase();
	};
	
	
	[Serializable()]
	public class RM2C_ESCORT_BEAT_GROUP_ACT : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_BEAT_GROUP_ACT;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_ESCORT_BEAT_GROUP_ACT();
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
	};

	[Serializable()]
	public class C2RM_ESCORT_PK_BEGIN : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_PK_BEGIN;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_ESCORT_PK_BEGIN();
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
		
		///战斗开始时间戳
		public UInt32 uiBeginTime = new UInt32();
		/// 佣兵id
		public UInt32 uiFriendId = new UInt32();
		///宠物站位信息
		public UInt64[] vctPet = new UInt64[9];
		///怪宠物站位信息
		public UInt64[] vctTeamPet = new UInt64[9];
		/// 魂兽id
		public UInt64 luiIdBeast = new UInt64();
	};
	
	
	[Serializable()]
	public class C2RM_ESCORT_PK_MIN : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_PK_MIN;
		
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_ESCORT_PK_MIN();
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
    public class C2RM_ESCORT_PK_OVER : ExFormatterBinary, IProtocal
    {
        //补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_PK_OVER;

        public C2RM_ESCORT_PK_OVER()
        {
            for( int i = 0; i < 18; ++i )
            {
                vctPetSkill[ i ] = new SEscortPetSkill();
            }
        }

        public static IProtocal Create( ushort msg, HeaderBase h )
        {
            return new C2RM_ESCORT_PK_OVER();
        }

        public ushort Message
        {
            get
            {
                return ( ushort ) OPCODE;
            }
        }

        public bool analysisBuffer( byte[] Buffer )
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
        public float[] vctHpPet = new float[ 18 ];
        public SEscortPetSkill[] vctPetSkill = new SEscortPetSkill[ 18 ];
        /// 魂兽id
        public UInt64 luiIdBeast = new UInt64();
        ///魂兽剩余怒气百分比
        public float fBeastAnger = new float();
        ///敌人魂兽剩余怒气百分比
        public float fBeastAngerEnemy = new float();
    };

	[Serializable()]
	public class RM2C_ESCORT_PK_OVER : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_PK_OVER;
		
		private void init(int len)
		{
			if (len >= 0)
			{
				vctInfo = new SEscortGroup[len];
				for (int i = 0; i < len; i++)
				{
					vctInfo[i] = new SEscortGroup();
				}
			}
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_ESCORT_PK_OVER();
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
		///1胜利0失败
		public UInt32 uiType = new UInt32();
		///获得药水
		public SEquipment Equ = new SEquipment();
		///获得货币
		public SMoney Money = new SMoney();
		/// 攻打的商队信息
		public SEscortGroup AckEscortGroup = new SEscortGroup();
		/// (新创建的商队信息)
		public SEscortGroup[] vctInfo;
	};

	[Serializable()]
	public class C2RM_ESCORT_ADD_BREAD : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_ADD_BREAD;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_ESCORT_ADD_BREAD();
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
	public class RM2C_ESCORT_ADD_BREAD : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_ADD_BREAD;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_ESCORT_ADD_BREAD();
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
		/// 护送基本信息
		public SEscortInfoBase EscortInfo = new SEscortInfoBase();
	};

	[Serializable()]
	public class C2RM_ESCORT_BUY_BREAD : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_BUY_BREAD;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_ESCORT_BUY_BREAD();
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
	public class RM2C_ESCORT_BUY_BREAD : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_BUY_BREAD;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_ESCORT_BUY_BREAD();
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
		///花费货币
		public SMoney CostMoney = new SMoney();
		/// 护送基本信息
		public SEscortInfoBase EscortInfo = new SEscortInfoBase();
	};

	[Serializable()]
	public class C2RM_ESCORT_LOG_GET_BREAD : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_LOG_GET_BREAD;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_ESCORT_LOG_GET_BREAD();
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
		/// 防守log id
		public UInt64 luiIdLog = new UInt64();
	};
	
	[Serializable()]
	public class RM2C_ESCORT_LOG_GET_BREAD : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_LOG_GET_BREAD;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_ESCORT_LOG_GET_BREAD();
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
		/// 更新防守记录
		public SEscortLog Escortlog = new SEscortLog();
		/// 护送基本信息
		public SEscortInfoBase EscortInfo = new SEscortInfoBase();
	};

	[Serializable()]
	public class C2RM_ESCORT_MAKE_SURE_FAIL : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_MAKE_SURE_FAIL;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_ESCORT_MAKE_SURE_FAIL();
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
		/// 被打败的商队
		public SEscortSearchBase luiIdGroup = new SEscortSearchBase();
	};
	
	[Serializable()]
	public class RM2C_ESCORT_MAKE_SURE_FAIL : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_MAKE_SURE_FAIL;

		private void init(int len)
		{
			if (len >= 0)
			{
				vctInfo = new SEscortGroupBase[len];
				for (int i = 0; i < len; i++)
				{
					vctInfo[i] = new SEscortGroupBase();
				}
			}
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_ESCORT_MAKE_SURE_FAIL();
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
		/// 获得的经验药
		public SEquipment Equ = new SEquipment();
		/// 获得的money
		public SMoney GetMoney = new SMoney();
		/// 自己护送的商队基本信息（已经失败并且领奖的商队不在这里出现）
		public SEscortGroupBase[] vctInfo;
	};

	[Serializable()]
	public class C2RM_ESCORT_LOG : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_LOG;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_ESCORT_LOG();
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
	public class RM2C_ESCORT_LOG : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_LOG;
		
		private void init(int len)
		{
			if (len >= 0)
			{
				vctInfo = new SEscortLog[len];
				for (int i = 0; i < len; i++)
				{
					vctInfo[i] = new SEscortLog();
				}
			}
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_ESCORT_LOG();
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
		/// 卡牌出战信息
		public SEscortLog[] vctInfo;
	};

	[Serializable()]
	public class RM2C_ESCORT_PK_GROUP : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_PK_GROUP;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_ESCORT_PK_GROUP();
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

		///被攻下的商队信息
		public SEscortGroup EscortGroup = new SEscortGroup();							
	};

	[Serializable()]
	public class C2RM_ESCORT_GIVE_UP : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_GIVE_UP;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_ESCORT_GIVE_UP();
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
		///被放弃的商队
		public SEscortSearchBase SGroup = new SEscortSearchBase();	
		///被放弃的队伍
		public SEscortSearchBase STeam = new SEscortSearchBase();
	};
	
	[Serializable()]
	public class RM2C_ESCORT_GIVE_UP : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_GIVE_UP;
		
		private void init(int len)
		{
			if (len >= 0)
			{
				vctInfo = new SEscortGroupBase[len];
				for (int i = 0; i < len; i++)
				{
					vctInfo[i] = new SEscortGroupBase();
				}
			}
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_ESCORT_GIVE_UP();
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
		/// 获得的经验药
		public SEquipment Equ = new SEquipment();
		/// 获得的money
		public SMoney GetMoney = new SMoney();
		/// 自己护送的商队基本信息（已经放弃的商队不在这里出现）
		public SEscortGroupBase[] vctInfo;
	};

	[Serializable()]
	public class RM2C_ESCORT_CREAT_LOG : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_CREAT_LOG;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_ESCORT_CREAT_LOG();
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
		
		///被攻下的商队信息
		public SEscortLog EscortLog = new SEscortLog();							
	};

	[Serializable()]
	public class C2RM_ESCORT_SUCCEED : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_SUCCEED;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_ESCORT_SUCCEED();
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
		///护送成功的商队
		public SEscortSearchBase SGroup = new SEscortSearchBase();	
		///护送成功队伍
		public SEscortSearchBase STeam = new SEscortSearchBase();
	};
	
	[Serializable()]
	public class RM2C_ESCORT_SUCCEED : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_SUCCEED;
		
		private void init(int len)
		{
			if (len >= 0)
			{
				vctInfo = new SEscortGroupBase[len];
				for (int i = 0; i < len; i++)
				{
					vctInfo[i] = new SEscortGroupBase();
				}
			}
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_ESCORT_SUCCEED();
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
		/// 获得的经验药
		public SEquipment Equ = new SEquipment();
		/// 获得的money
		public SMoney GetMoney = new SMoney();
		/// 自己护送的商队基本信息（已经护送成功的商队不在这里出现）
		public SEscortGroupBase[] vctInfo;
	};

	[Serializable()]
	public class C2RM_ESCORT_BEAT_GROUP_TIME_OUT : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_ESCORT_BEAT_GROUP_TIME_OUT;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_ESCORT_BEAT_GROUP_TIME_OUT();
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
		///超时的进攻商队
		public SEscortSearchBase SGroup = new SEscortSearchBase();
	};
	
	[Serializable()]
	public class RM2C_ESCORT_BEAT_GROUP_TIME_OUT : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_ESCORT_BEAT_GROUP_TIME_OUT;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_ESCORT_BEAT_GROUP_TIME_OUT();
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
		///超时的进攻商队
		public SEscortSearchBase SGroup = new SEscortSearchBase();						
	};

}