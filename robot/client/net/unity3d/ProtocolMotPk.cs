using System;

using System.IO;

using ExFormatter;
using System.Diagnostics;

namespace net.unity3d
{
	[Serializable()]
	public class C2RM_MY_MOT_PK_INFO : ExFormatterBinary, IProtocal
    {
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MY_MOT_PK_INFO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_MY_MOT_PK_INFO();
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
	public class RM2C_MY_MOT_PK_INFO : ExFormatterBinary, IProtocal
    {
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MY_MOT_PK_INFO;
		
		public RM2C_MY_MOT_PK_INFO()
		{
			for (int i = 0; i < 7; ++i )
			{
				m_MotPkUnitInfo[i] = new stMotPkUnitInfoUI();
			}
		}

		public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new RM2C_MY_MOT_PK_INFO();
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
		///当前赛季的报名日5点对应的时间
		public UInt32 m_uiCurSeasonStartTime = new UInt32();
		//当前赛季是否报名
		public byte m_ucIsJoinCur = new byte();						
		/////第几天的数据:0-报名日5点~次日5点，1...6
		public stMotPkUnitInfoUI[] m_MotPkUnitInfo = new stMotPkUnitInfoUI[7];	
		///我的防守英雄
		public UInt32 uiMyIdCsvPet = new UInt32();	
		///我的防守英雄星级
		public UInt32 uiMyStarPet = new UInt32();						
		///我的防守英雄等经验
		public UInt64 luiMyExpPet = new UInt64();
		///可领取的防守次数奖励
		public UInt32 uiCanRewardPkedMoney = new UInt32();
    };

	[Serializable()]
	public class C2RM_MOT_PK_ENEMY_INFO : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOT_PK_ENEMY_INFO;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_MOT_PK_ENEMY_INFO();
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
	public class RM2C_MOT_PK_ENEMY_INFO : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOT_PK_ENEMY;
		
		public RM2C_MOT_PK_ENEMY_INFO()
		{
			for (int i = 0; i < 5; ++i )
			{
				m_MotPkEnemy[i] = new stMotPkEnemyInfo();
			}
		}
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_MOT_PK_ENEMY_INFO();
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
		
		public UInt32 m_uiListen = new UInt32();
		public int iResult = new int();
		/////第几个NPC信息，index from 0~4
		public stMotPkEnemyInfo[] m_MotPkEnemy = new stMotPkEnemyInfo[5];	
		///挑战过第几个奖励关的标志信息(从低到高第几位是1表示挑战过第几个奖励关)
		public byte m_ucHadPkBit = new byte();
		///胜利过第几个奖励关的标志信息(从低到高第几位是1表示胜利过第几个奖励关)
		public byte m_ucWinBit = new byte();
		///领取宝箱奖励过第几个奖励关的标志信息(从低到高第几位是1表示领取宝箱奖励过第几个奖励关)
		public byte m_ucHadRewardBit = new byte();
	};
	
	[Serializable()]
	public class C2RM_MOT_PK_RANK_LIST : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOT_PK_RANK_LIST;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_MOT_PK_RANK_LIST();
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
		public UInt16 usRankType = new UInt16();
    };
	
	[Serializable()]
	public class RM2C_MOT_PK_RANK_LIST : ExFormatterBinary, IProtocal
    {
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOT_PK_RANK_LIST;
		
		private void init(int len)
        {	
            if (len >= 0)
            {
				vctRankInfo = new stMotPkRankInfo[len];
                for (int i = 0; i < len; i++)
                {
					vctRankInfo[i] = new stMotPkRankInfo();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new RM2C_MOT_PK_RANK_LIST();
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
		////排行榜类型EMotPkRankType：0-昨日榜，8-当前累计榜
		public UInt16 usRankType = new UInt16();
		////玩家排名信息
		public stMotPkRankInfo SMyRankInfo = new stMotPkRankInfo();
		public stMotPkRankInfo[] vctRankInfo;
    };
	
	[Serializable()]
	public class C2RM_JOIN_MOT_PK : ExFormatterBinary, IProtocal
    {
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_JOIN_MOT_PK;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_JOIN_MOT_PK();
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
		//0-报名，1-更换
		public byte ucType = new byte();	
		//报名的英雄ID
		public UInt64 uiPetID = new UInt64();			
    };
	
	[Serializable()]
	public class RM2C_JOIN_MOT_PK : ExFormatterBinary, IProtocal
    {
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_JOIN_MOT_PK;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new RM2C_JOIN_MOT_PK();
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
		///0-报名，1-更换
		public byte	ucType = new byte();
		///防守英雄
		public UInt32  uiIdCsvPet = new UInt32();	
		///星级
		public UInt32  uiStarPet = new UInt32();
		///我的防守英雄经验
		public UInt64  luiExpPet = new UInt64();						
    };

	[Serializable()]
	public class C2RM_MOT_PK_HELPER : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOT_PK_HELPER;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_MOT_PK_HELPER();
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
	public class RM2C_MOT_PK_HELPER : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOT_PK_HELPER;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_MOT_PK_HELPER();
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
	public class C2RM_MOT_PK_BOX : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOT_PK_BOX;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_MOT_PK_BOX();
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
		public byte ucBoxIndex = new byte();
	};
	
	
	[Serializable()]
	public class RM2C_MOT_PK_BOX : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOT_PK_BOX;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_MOT_PK_BOX();
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
		public byte ucBoxIndex = new byte();
		///结果
		public int iResult = new int();
		/// 金币, 加法
		public SMoney sMoney = new SMoney();
		///远征币只是用来显示
		public UInt32 uiMotMoney = new UInt32();
		
	};

	[Serializable()]
	public class C2RM_MOT_PK_GOTO : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOT_PK_GOTO;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_MOT_PK_GOTO();
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
		//第几个奖励关
		public byte ucPkInde = new byte();	
	};
	
	[Serializable()]	
	public class RM2C_MOT_PK_GOTO : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOT_PK_GOTO;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_MOT_PK_GOTO();
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
	};
	
	[Serializable()]
	public class C2RM_MOT_PK_PK_BEGIN : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOT_PK_PK_BEGIN;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_MOT_PK_PK_BEGIN();
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
	public class C2RM_MOT_PK_PK_MIN : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOT_PK_PK_MIN;
		
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_MOT_PK_PK_MIN();
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
	public class C2RM_MOT_PK_PK_OVER : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOT_PK_PK_OVER;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_MOT_PK_PK_OVER();
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
		public UInt64[] vctHpPet = new UInt64[18];
		//伤害值，永远计算奖励金币
		public UInt32 uiHurt = new UInt32();				
	};
	
	
	[Serializable()]
	public class RM2C_MOT_PK_REWARD : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOT_PK_REWARD;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_MOT_PK_REWARD();
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
		///伤害获得的金币奖励
		public UInt32 uiHurtMoney = new UInt32();
		///是否胜利0：否 1：是
		public byte isWin = new byte();
		//评价星级
		public UInt32 uiStar = new UInt32();		
	};

	[Serializable()]
	public class RM2C_MOT_PK_PK_ERROR : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOT_PK_PK_ERROR;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_MOT_PK_PK_ERROR();
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
	public class C2RM_MOT_PK_LEAVE : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOT_PK_LEAVE;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_MOT_PK_LEAVE();
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
	public class RM2C_MOT_PK_LEAVE : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOT_PK_LEAVE;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_MOT_PK_LEAVE();
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
	};

	[Serializable()]
	public class C2RM_MOT_PK_REWARD_PKED : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOT_PK_REWARD_PKED;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_MOT_PK_REWARD_PKED();
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
	public class RM2C_MOT_PK_REWARD_PKED : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOT_PK_REWARD_PKED;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_MOT_PK_REWARD_PKED();
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
		public UInt32 uiMoney = new UInt32();		///奖励钱数
	};
	

}