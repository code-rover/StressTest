using System;

using System.IO;

using ExFormatter;
using System.Diagnostics;

namespace net.unity3d
{
	[Serializable()]
	public class C2RM_GET_GAME_UNION_INFO : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_GET_GAME_UNION_INFO;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_GET_GAME_UNION_INFO();
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
	public class RM2C_GU_MEMBER_LIST : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_GU_MEMBER_LIST;
		
		private void init(int len)
		{	
			if (len >= 0)
			{
				m_SGUMemberList = new stGameUnionMember[len];
				for (int i = 0; i < len; i++)
				{
					m_SGUMemberList[i] = new stGameUnionMember();
				}
			}
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_GU_MEMBER_LIST();
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
		public int iResult = new int();
		public byte m_cIsBegin = new byte();
		public byte m_cIsOver = new byte();
		public stGameUnionMember[] m_SGUMemberList;
	};

	[Serializable()]
	public class RM2C_GET_GAME_UNION_INFO : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_GET_GAME_UNION_INFO;
				
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_GET_GAME_UNION_INFO();
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
		public stGameUnion m_stGUInfo = new stGameUnion();
	};

	[Serializable()]
	public class C2RM_GET_GAME_UNION_LIST : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_GET_GAME_UNION_LIST;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_GET_GAME_UNION_LIST();
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
	public class RM2C_GAME_UNION_LIST : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_GAME_UNION_LIST;
		
		private void init(int len)
		{	
			if (len >= 0)
			{
				m_SGUList = new stGameUnion[len];
				for (int i = 0; i < len; i++)
				{
					m_SGUList[i] = new stGameUnion();
				}
			}
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_GU_MEMBER_LIST();
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
		public byte m_ucRequestCount = new byte();	///已申请的个数
		public stGameUnion[] m_SGUList;
	};

	[Serializable()]
	public class C2RM_SEARCH_GAME_UNION : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_SEARCH_GAME_UNION;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_SEARCH_GAME_UNION();
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
		public UInt32 m_uiSearchID = new UInt32();		//搜索的公会ID
	};

	[Serializable()]
	public class RM2C_SEARCH_GAME_UNION : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_SEARCH_GAME_UNION;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_SEARCH_GAME_UNION();
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
		public stGameUnion m_stGUInfo = new stGameUnion();
	};

	[Serializable()]
	public class C2RM_CREATE_GAME_UNION : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_CREATE_GAME_UNION;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_CREATE_GAME_UNION();
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
				m_cGUName[i] = nameByte[i];
			}
		}
		
		public UInt32 uiListen = new UInt32();
		public byte[] m_cGUName = new byte[32];
		public UInt32 m_uiGUPic = new UInt32();
	};
	
	[Serializable()]
	public class RM2C_CREATE_GAME_UNION : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_CREATE_GAME_UNION;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_CREATE_GAME_UNION();
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
		public byte	m_ucIsRequest = new byte();	//是否已申请
		public stGameUnion m_stGUInfo = new stGameUnion();
	};

	[Serializable()]
	public class C2RM_OPEN_SET_GAME_UNION : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_OPEN_SET_GAME_UNION;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_OPEN_SET_GAME_UNION();
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
		public byte m_ucIsOpen = new byte();	///0-关闭，1-打开
	};
	
	[Serializable()]
	public class RM2C_OPEN_SET_GAME_UNION : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_OPEN_SET_GAME_UNION;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_OPEN_SET_GAME_UNION();
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
		public int m_iResult = new int();
		public byte m_ucIsOpen = new byte();	///0-关闭，1-打开
	};

	[Serializable()]
	public class C2RM_SET_GAME_UNION : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_SET_GAME_UNION;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_SET_GAME_UNION();
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
		public UInt32 m_uiSetType = new UInt32();	///按位表示参见枚举EGU_SetType位标志
		public stGameUnion m_stGUInfo = new stGameUnion();
	};
	
	[Serializable()]
	public class RM2C_SET_GAME_UNION : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_SET_GAME_UNION;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_SET_GAME_UNION();
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
		public int m_iResult = new int();
		public UInt32 m_uiSetType = new UInt32();	///按位表示参见枚举EGU_SetType位标志
	};

	[Serializable()]
	public class C2RM_REQUEST_JOIN_GAME_UNION : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_REQUEST_JOIN_GAME_UNION;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_REQUEST_JOIN_GAME_UNION();
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
		public UInt32 m_uiJoinGUID = new UInt32();		///要加入的公会ID
	};
	
	[Serializable()]
	public class RM2C_REQUEST_JOIN_GAME_UNION : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_REQUEST_JOIN_GAME_UNION;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_REQUEST_JOIN_GAME_UNION();
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
		public int m_iResult = new int();
		public UInt32 m_uiJoinGUID = new UInt32();		///要加入的公会ID	
		public UInt32 m_uiLimitTime = new UInt32();		///可加入的时间
	};

	[Serializable()]
	public class C2RM_REQUEST_JOINER_LIST : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_REQUEST_JOINER_LIST;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_REQUEST_JOINER_LIST();
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
	public class RM2C_REQUEST_JOINER_LIST : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_REQUEST_JOINER_LIST;
		
		private void init(int len)
		{	
			if (len >= 0)
			{
				m_SJoinerList = new stGameUnionRequest[len];
				for (int i = 0; i < len; i++)
				{
					m_SJoinerList[i] = new stGameUnionRequest();
				}
			}
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_REQUEST_JOINER_LIST();
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
		public int m_iResult = new int();
		public byte m_cIsBegin = new byte();
		public byte m_cIsOver = new byte();
		public stGameUnionRequest[] m_SJoinerList;
	};

	[Serializable()]
	public class C2RM_AGREEN_JOIN : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_AGREEN_JOIN;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_AGREEN_JOIN();
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
		public byte m_ucAgreen = new byte();	///0-拒绝，1-同意
		public UInt32 m_uiRequestID = new UInt32();		//同意或拒绝的申请ID
	};
	
	[Serializable()]
	public class RM2C_AGREEN_JOIN : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_AGREEN_JOIN;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_AGREEN_JOIN();
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
		public int m_iResult = new int();
		public UInt32 m_uiRequestID = new UInt32();		//同意或拒绝的申请ID
	};

	[Serializable()]
	public class C2RM_APPOINT_JOB : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_APPOINT_JOB;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_APPOINT_JOB();
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
		public UInt32 m_uiDstRoleID = new UInt32();
		public byte m_cDstJob = new byte();			//-1：踢出公会，0：变为成员，1-变为会长，2-变为长老
	};
	
	[Serializable()]
	public class RM2C_APPOINT_JOB : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_APPOINT_JOB;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_APPOINT_JOB();
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
		public int m_iResult = new int();
		public UInt32 m_uiDstRoleID = new UInt32();
		public byte m_cDstJob = new byte();			//-1：踢出公会，0：变为成员，1-变为会长，2-变为长老
	};

	[Serializable()]
	public class C2RM_EMAIL_ALL_MEMBER : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_EMAIL_ALL_MEMBER;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_EMAIL_ALL_MEMBER();
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

		public void setEmailContent(string psContent)
		{
			byte[] contentByte = System.Text.Encoding.UTF8.GetBytes(psContent);
			for (int i = 0; i < contentByte.Length; i++)
			{
				m_cEmailContent[i] = contentByte[i];
			}
		}
		
		public UInt32 uiListen = new UInt32();
		public byte[] m_cEmailContent = new byte[200];
	};
	
	[Serializable()]
	public class RM2C_EMAIL_ALL_MEMBER : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_EMAIL_ALL_MEMBER;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_EMAIL_ALL_MEMBER();
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

		public string getEmailContent()
		{
			return System.Text.Encoding.UTF8.GetString(m_cEmailContent, 0, byteToStringIndex(m_cEmailContent));
		}
		
		public UInt32 uiListen = new UInt32();
		public int m_iResult = new int();
		public byte[] m_cEmailContent = new byte[200];
	};

	[Serializable()]
	public class C2RM_DISBAND_GAME_UNION : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_DISBAND_GAME_UNION;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_DISBAND_GAME_UNION();
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
		public UInt32 m_uiDisbandGUID = new UInt32();
	};
	
	[Serializable()]
	public class RM2C_DISBAND_GAME_UNION : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_DISBAND_GAME_UNION;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_DISBAND_GAME_UNION();
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
		public int m_iResult = new int();
	};

	[Serializable()]
	public class C2RM_GET_GAME_UNION_LOG : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_GET_GAME_UNION_LOG;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_GET_GAME_UNION_LOG();
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
	public class RM2C_GET_GAME_UNION_LOG : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_GET_GAME_UNION_LOG;
		
		private void init(int len)
		{	
			if (len >= 0)
			{
				m_SGULogList = new stGameUnionLog[len];
				for (int i = 0; i < len; i++)
				{
					m_SGULogList[i] = new stGameUnionLog();
				}
			}
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_GET_GAME_UNION_LOG();
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
		public int m_iResult = new int();
		public byte m_cIsBegin = new byte();
		public byte m_cIsOver = new byte();
		public stGameUnionLog[] m_SGULogList;
	};

	[Serializable()]
	public class RM2C_NOTIFY_LEAD_GU_INFO : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_NOTIFY_LEAD_GU_INFO;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_NOTIFY_LEAD_GU_INFO();
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

		public stGameUnionMemberBase m_sLeadGUMemberInfo = new stGameUnionMemberBase();
	};

	[Serializable()]
	public class C2RM_SET_GU_ANNOUNCEMENT : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_SET_GU_ANNOUNCEMENT;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_SET_GU_ANNOUNCEMENT();
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

		public void setAnnouncement(string psContent)
		{
			byte[] contentByte = System.Text.Encoding.UTF8.GetBytes(psContent);
			for (int i = 0; i < contentByte.Length; i++)
			{
				m_cAnnouncement[i] = contentByte[i];
			}
		}
		
		public UInt32 uiListen = new UInt32();
		public byte[] m_cAnnouncement = new byte[96];	//公告
	};
	
	[Serializable()]
	public class RM2C_SET_GU_ANNOUNCEMENT : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_SET_GU_ANNOUNCEMENT;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_SET_GU_ANNOUNCEMENT();
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

		public string getAnnouncement()
		{
			return System.Text.Encoding.UTF8.GetString(m_cAnnouncement, 0, byteToStringIndex(m_cAnnouncement));
		}
		
		public UInt32 uiListen = new UInt32();
		public int m_iResult = new int();
		public byte[] m_cAnnouncement = new byte[96];	//公告
	};



	

	

	
	
}
