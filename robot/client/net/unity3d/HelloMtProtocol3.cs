using System;

using System.IO;

using ExFormatter;
using System.Diagnostics;

namespace net.unity3d
{
	[Serializable()]
    public class C2RM_SIGN : ExFormatterBinary, IProtocal
    {
		//今日签到
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_SIGN;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_SIGN();
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
    public class RM2C_SIGN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_SIGN;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_SIGN();
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
		///签到信息
		public SLeadSign sLeadSign = new SLeadSign();
		///奖励id
		public UInt32 uiRewardId = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_REWARD_MONEY : ExFormatterBinary, IProtocal
    {
		//奖励的钱 ，正义徽章之类
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_REWARD_MONEY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_REWARD_MONEY();
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
		
		/// 同步奖励，直接相加
		public SRewardMoney sRewardMoney = new SRewardMoney();
    };
	
	[Serializable()]
    public class C2RM_SING_RE : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_SING_RE;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_SING_RE();
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
    public class RM2C_SIGN_RE : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_SIGN_RE;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_SIGN_RE();
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
		///签到信息
		public SLeadSign sLeadSign = new SLeadSign();
		///奖励id
		public UInt32 uiRewardId = new UInt32();
		///消耗的钱, 用加法
		public SMoney sCostMoney = new SMoney();
    };

	[Serializable()]
	public class C2RM_SIGN_BEAST_REQUEST : ExFormatterBinary, IProtocal
	{
		//今日签到
		public static readonly E_OPCODE OPCODE = net.unity3d.E_OPCODE.EP_C2RM_SIGN_BEAST_REQUEST;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_SIGN_BEAST_REQUEST();
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
		//请求类型：0-请求签到信息，1-领取奖励
		public byte ucType = new byte();	
		//领奖的索引
		public byte ucRewardIndex = new byte(); 
	};
	
	[Serializable()]
	public class RM2C_SIGN_BEAST_REQUEST : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_SIGN_BEAST_REQUEST;

		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_SIGN_BEAST_REQUEST();
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
		///请求类型：0-请求签到信息，1-领取奖励
		public byte ucType = new byte();	
		/// 结果
		public int iResult = new int();
		///签到位
		public UInt32 uiSignBit = new UInt32();	
		///领奖位
		public UInt32 uiRewardBit = new UInt32();	
		///领取的奖励ID
		public UInt32 uiRewardID  = new UInt32();	
	};
	
	[Serializable()]
    public class C2RM_TASK : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_TASK;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_TASK();
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
    public class RM2C_TASK : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_TASK;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_TASK();
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
		/// 任务信息
		public SLeadTask sLeadTask = new SLeadTask();
    };
	
	[Serializable()]
    public class C2RM_TASK_GUIDE_REWARD : ExFormatterBinary, IProtocal
    {
		//领取新手任务奖励
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_TASK_GUIDE_REWARD;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_TASK_GUIDE_REWARD();
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
		///任务id
		public UInt32 uiTaskId = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_TASK_GUIDE_REWARD : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_TASK_GUIDE_REWARD;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_TASK_GUIDE_REWARD();
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
		///任务id
		public UInt32 uiTaskId = new UInt32();
		///奖励csv id
		public UInt32 uiRewardId = new UInt32();
    };
	
	[Serializable()]
    public class C2RM_TASK_FB_REWARD : ExFormatterBinary, IProtocal
    {
		//领取主线，支线，挑战副本奖励
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_TASK_FB_REWARD;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_TASK_FB_REWARD();
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
		///4：主线普通副本任务 5:支线精英副本任务 6：支线挑战副本任务
		public byte cType = new byte();
    };
	
	[Serializable()]
    public class RM2C_TASK_FB_REWARD : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_TASK_FB_REWARD;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_TASK_FB_REWARD();
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
		/// 奖励id
		public UInt32 uiRewardId = new UInt32();
		///任务id
		public UInt32 uiTaskId = new UInt32();
		///4：主线普通副本任务 5:支线精英副本任务 6：支线挑战副本任务
		public byte cType = new byte();
    };
	
	[Serializable()]
    public class C2RM_TASK_EVERYDAY_REWARD : ExFormatterBinary, IProtocal
    {
		//领取每日任务的奖励
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_TASK_EVERYDAY_REWARD;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_TASK_EVERYDAY_REWARD();
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
		///任务id
		public UInt32 uiTaskId = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_TASK_EVERYDAY_REWARD : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_TASK_EVERYDAY_REWARD;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_TASK_EVERYDAY_REWARD();
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
		///任务奖励id
		public UInt32 uiTaskId = new UInt32();
		///奖励csv id
		public UInt32 uiRewardId = new UInt32();
    };
	
	[Serializable()]
    public class C2RM_TASK_LV : ExFormatterBinary, IProtocal
    {
		//领取升级任务的奖励
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_TASK_LV;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_TASK_LV();
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
    public class RM2C_TASK_LV : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_TASK_LV;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_TASK_LV();
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
		///奖励csv id
		public UInt32 uiRewardId = new UInt32();
    };
	
	[Serializable()]
    public class C2RM_PET_LV_UP : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_PET_LV_UP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_PET_LV_UP();
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
		///要升级的卡牌 id
		public UInt64 uiPetId = new UInt64();
		///经验药的csv id
		public UInt32 uiPropCsvId = new UInt32();
		///经验药的数量
		public UInt32 uiCnt = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_PET_LV_UP : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_PET_LV_UP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_PET_LV_UP();
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
		/// 升级后的卡牌信息
		public SPetEat sPetEat = new SPetEat();
		/// 消耗后剩余的经验药信息
		public SEquipment sProp = new SEquipment();
    };
	
	[Serializable()]
    public class C2RM_PET_STAR_UP : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_PET_STAR_UP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_PET_STAR_UP();
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
		///要升星的卡牌 id
		public UInt64 uiPetId = new UInt64();
    };
	
	[Serializable()]
    public class RM2C_PET_STAR_UP : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_PET_STAR_UP;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_PET_STAR_UP();
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
		/// 升星后信息
		public SPetStarInfo sPetStarInfo = new SPetStarInfo();
		/// 升星后碎片变化
		public SPiece sPiece = new SPiece();
		///花的游戏币, 加法
		public SMoney sMoney = new SMoney();
    };
	
	[Serializable()]
    public class C2RM_SKILL_UP_NEW : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_SKILL_UP_NEW;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_SKILL_UP_NEW();
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
		///要升星的卡牌 id
		public UInt64 uiPetId = new UInt64();
		///要升级或者学习的技能csv id
		public UInt32 uiCsvSkillId = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_SKILL_UP_NEW : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_SKILL_UP_NEW;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_SKILL_UP_NEW();
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
		///花的游戏币，加法
		public SMoney sMoney = new SMoney();
		///升级的技能信息
		public SSkillup sKillUp = new SSkillup();
		///技能点信息
		public SLeadSkill sLeadSkill = new SLeadSkill();
    };
	
	[Serializable()]
    public class C2RM_SKILL_POINT_ADD : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_SKILL_POINT_ADD;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_SKILL_POINT_ADD();
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
    public class RM2C_SKILL_POINT_ADD : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_SKILL_POINT_ADD;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_SKILL_POINT_ADD();
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
		///技能点信息
		public SLeadSkill sLeadSkill = new SLeadSkill();
    };
	
	[Serializable()]
    public class C2RM_PET_PIECE_TO_PET : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_PET_PIECE_TO_PET;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_PET_PIECE_TO_PET();
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
		///要合成的碎片的csv id即 same id
		public UInt32 uiCsvId = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_PET_PIECE_TO_PET : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_PET_PIECE_TO_PET;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_PET_PIECE_TO_PET();
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
		/// 花的游戏币,加法
		public SMoney sMoney = new SMoney();
		/// 合成后碎片信息
		public SPiece sPiece = new SPiece();
		///合成的卡牌信息
		public SPetInfo sPetInfo = new SPetInfo();
    };
	
	[Serializable()]
    public class C2RM_SKILL_POINT_BUY : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_SKILL_POINT_BUY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_SKILL_POINT_BUY();
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
    public class RM2C_SKILL_POINT_BUY : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_SKILL_POINT_BUY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_SKILL_POINT_BUY();
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
		///花钻石，加法
		public SMoney sMoney = new SMoney();
		///技能点信息
		public SLeadSkill sLeadSkill = new SLeadSkill();
    };
	
	[Serializable()]
    public class C2RM_LUCKY_SOUL : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_LUCKY_SOUL;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_LUCKY_SOUL();
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
    public class RM2C_LUCKY_SOUL : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_LUCKY_SOUL;

        private void init(int len)
        {
            if (len >= 0)
            {
                uiLuckySoulId = new UInt32[len];
                for (int i = 0; i < len; i++)
                {
                    uiLuckySoulId[i] = new UInt32();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_LUCKY_SOUL();
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
		/// 消耗的钻石，加法
		public SMoney sCostMoney = new SMoney();
		/// 魂侠抽次数变化
		public UInt32 uiLuckySoul = new UInt32();
		/// 魂侠表 id
		public UInt32[] uiLuckySoulId;
    };
	
	[Serializable()]
    public class C2RM_PET_PIECE_SELL : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_PET_PIECE_SELL;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_PET_PIECE_SELL();
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
		///碎片csv id
		public UInt32 uiCsvId = new UInt32();
		///出售的数量
		public UInt32 uiCnt = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_PET_PIECE_SELL : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_PET_PIECE_SELL;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_PET_PIECE_SELL();
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
		///卖得的游戏币，加法
		public SMoney sMoney = new SMoney();
		///碎片出售后的信息
		public SPiece sPiece = new SPiece();
    };
	
	[Serializable()]
    public class C2RM_FRIEND_FIGHT_HISTORY : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FRIEND_FIGHT_HISTORY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_FRIEND_FIGHT_HISTORY();
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
    public class RM2C_FRIEND_FIGHT_HISTORY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_FIGHT_HISTORY;

        private void init(int len)
        {
            if (len >= 0)
            {
                uiFriendId = new UInt32[len];
                for (int i = 0; i < len; i++)
                {
                    uiFriendId[i] = new UInt32();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_FIGHT_HISTORY();
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
		public UInt32[] uiFriendId;
    };
	
	[Serializable()]
    public class C2RM_FRIEND_HELPER : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_FRIEND_HELPER;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_FRIEND_HELPER();
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
    public class RM2C_FRIEND_HELPER : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_FRIEND_HELPER;

        private void init(int len)
        {
            if (len >= 0)
            {
                sFriendHelper = new SFriendHelper[len];
                for (int i = 0; i < len; i++)
                {
                    sFriendHelper[i] = new SFriendHelper();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_FRIEND_HELPER();
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
		/// 结果
		public int iResult = new int();
		/// 佣兵信息
		public SFriendHelper[] sFriendHelper;
    };
	
	[Serializable()]
    public class C2RM_WEB_EMAIL : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_WEB_EMAIL;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_WEB_EMAIL();
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
    public class RM2C_WEB_EMAIL : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_WEB_EMAIL;

        private void init(int len)
        {
            if (len >= 0)
            {
                sWebEmail = new SWebEmail[len];
                for (int i = 0; i < len; i++)
                {
                    sWebEmail[i] = new SWebEmail();
					
					for(int j = 0; j < 10; ++j)
					{
						sWebEmail[i].vctSWebGoodBase[j] = new SWebGoodBase();
					}
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_WEB_EMAIL();
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
		/// 网络邮件信息
		public SWebEmail[] sWebEmail;
    };
	
	[Serializable()]
    public class C2RM_WEB_EMAIL_OPEN : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_WEB_EMAIL_OPEN;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_WEB_EMAIL_OPEN();
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
		///web email id server
		public UInt64 uiWebEmailId = new UInt64();
    };
	
	[Serializable()]
    public class RM2C_WEB_EMAIL_OPEN : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_WEB_EMAIL_OPEN;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_WEB_EMAIL_OPEN();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }
		
		private void init()
        {
            for (int i = 0; i < 10; i++)
            {
                vctSEquip[i] = new SEquipment();
				vctSPiece[i] =  new SPiece();
            }
        }
		
        public bool analysisBuffer(byte[] Buffer)
        {
			init();
            FromByteArrayNew(Buffer, this);
            return true;
        }

        public byte[] getBuffer()
        {
            return null;
        }
		
		public UInt32 uiListen = new UInt32();
		public int iResult = new int();
		///web email id server
		public UInt64 uiWebEmailId = new UInt64();
		public UInt32 uiOutTime = new UInt32();
		public SRewardMoney SMoney = new SRewardMoney();
		public SEquipment[] vctSEquip = new SEquipment[10];
		public SPiece[] vctSPiece = new SPiece[10];
    };
	
	[Serializable()]
    public class RM2C_WEB_EMAIL_NOTIFY_NEW : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_WEB_EMAIL_NOTIFY_NEW;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_WEB_EMAIL_NOTIFY_NEW();
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
		
		public SWebEmail sWebEmail = new SWebEmail();
    };
	
	[Serializable()]
    public class C2RM_WEB_EMAIL_TEXT : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_WEB_EMAIL_TEXT;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_WEB_EMAIL_TEXT();
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
		///web email id server
		public UInt32 uiWebEmailId = new UInt32();
		public UInt64 luiIdServer = new UInt64();
    };
	
	
	[Serializable()]
    public class RM2C_WEB_EMAIL_TEXT : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_WEB_EMAIL_TEXT;

        private void init(int len)
        {
            if (len >= 0)
            {
                cText = new byte[len];
                for (int i = 0; i < len; i++)
                {
                    cText[i] = new byte();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_WEB_EMAIL_TEXT();
        }

        public ushort Message
        {
            get { return (ushort)OPCODE; }
        }
		
		public string getSendName()
        {
            return System.Text.Encoding.UTF8.GetString(cSendName, 0, byteToStringIndex(cSendName));
        }
        public string getTitle()
        { 
            return System.Text.Encoding.UTF8.GetString(cTitle, 0, byteToStringIndex(cTitle));
        }
		public string getText()
        { 
            return System.Text.Encoding.UTF8.GetString(cText, 0, byteToStringIndex(cText));
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
		public UInt32 uiIdWeb = new UInt32();
		public UInt64 luiIdServer = new UInt64();
		public byte[] cSendName = new byte[80];
		public byte[] cTitle = new byte[80];
		/// 内容信息
		public byte[] cText;
    };
	
	
	[Serializable()]
    public class RM2C_NOTIFY_LEAD_FB : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_NOTIFY_LEAD_FB;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_NOTIFY_LEAD_FB();
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

		
		/// 同步副本信息
		public SLeadFbInfo sLeadFbInfo = new SLeadFbInfo();
    };
	
	[Serializable()]
    public class RM2C_TEAM_NOTIFY : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_TEAM_NOTIFY;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_TEAM_NOTIFY();
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

		
		/// 同步战队信息
		public STeam sTeam = new STeam();
    };
	
	[Serializable()]
    public class C2RM_BOSS_INFO : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_BOSS_INFO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_BOSS_INFO();
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
    public class RM2C_BOSS_INFO : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_BOSS_INFO;

        private void init(int len)
        {
            if (len >= 0)
            {
                sBoss = new SBoss[len];
                for (int i = 0; i < len; i++)
                {
                    sBoss[i] = new SBoss();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_BOSS_INFO();
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
		/// 佣兵信息
		public SBoss[] sBoss;
    };

	[Serializable()]
	public class C2RM_BOSS_RESET : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_BOSS_RESET;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_BOSS_RESET();
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
		///boss id server
		public UInt64 uiId = new UInt64();
	};
	
	[Serializable()]
	public class RM2C_BOSS_RESET : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_BOSS_RESET;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_BOSS_RESET();
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
		///boss id server
		public UInt64 uiId = new UInt64();
	};
	
	[Serializable()]
    public class C2RM_BOSS_TIME_OUT : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_BOSS_TIME_OUT;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_BOSS_TIME_OUT();
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
		///boss id server
		public UInt64 uiId = new UInt64();
    };
	
	[Serializable()]
    public class RM2C_BOSS_TIME_OUT : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_BOSS_TIME_OUT;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_BOSS_TIME_OUT();
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
		///boss id server
		public UInt64 uiId = new UInt64();
    };
	
	[Serializable()]
    public class RM2C_BOSS_UPDATE : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_BOSS_UPDATE;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_BOSS_UPDATE();
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
		
		public SBoss sBoss = new SBoss();
    };
	
	[Serializable()]
    public class C2RM_BOSS_PK_BEGIN : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_BOSS_PK_BEGIN;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_BOSS_PK_BEGIN();
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
		///战斗开始时间戳
		public UInt32 uiBeginTime = new UInt32();
    };
	
	[Serializable()]
    public class C2RM_BOSS_GOTO : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_BOSS_GOTO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_BOSS_GOTO();
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
		///boss id server
		public UInt64 uiId = new UInt64();
		///宠物站位信息
		public UInt64[] vctPet = new UInt64[9];
    };
	
	[Serializable()]
    public class RM2C_BOSS_GOTO : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_BOSS_GOTO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_BOSS_GOTO();
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
		///boss id server
		public UInt64 uiId = new UInt64();
    };
	
	[Serializable()]
    public class C2RM_BOSS_PK_MIN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_BOSS_PK_MIN;


        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_BOSS_PK_MIN();
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
    public class C2RM_BOSS_PK_OVER : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_BOSS_PK_OVER;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_BOSS_PK_OVER();
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
		///本次给世界boss造成的伤害
		public UInt64 uiHarm = new UInt64();
		
    };
	
	[Serializable()]
    public class RM2C_BOSS_MONEY_REWARD : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_BOSS_MONEY_REWARD;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_BOSS_MONEY_REWARD();
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
		
		public SMoney sMoney = new SMoney();
		///奖励vip倍数
		public UInt32 uiVipMuilt = new UInt32();
		///打的boss id server
		public UInt64 uiBossId = new UInt64();
		///boss血量伤害统计
		public SBossHp sBossHp = new SBossHp();
		///胜利失败0:失败 1：胜利
		public byte cIsWIn = new byte();
    };
	
	[Serializable()]
    public class RM2C_BOSS_VALUE : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_BOSS_VALUE;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_BOSS_VALUE();
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
		
		public SBossValue sBossValue = new SBossValue();
    };
	
	[Serializable()]
    public class RM2C_BOSS_CHECK_ERROR : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_BOSS_CHECK_ERROR;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_BOSS_CHECK_ERROR();
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
		
		public SBossValue sBossValue = new SBossValue();
    };
	
	[Serializable()]
    public class RM2C_BOSS_VALUE_REWARD_BEGIN : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_BOSS_VALUE_REWARD_BEGIN;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_BOSS_VALUE_REWARD_BEGIN();
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
		
		public UInt64 uiBossId = new UInt64();
    };
	
	[Serializable()]
    public class RM2C_BOSS_VALUE_REWARD_END : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_BOSS_VALUE_REWARD_END;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_BOSS_VALUE_REWARD_END();
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
		
		public UInt64 uiBossId = new UInt64();
    };
	
	[Serializable()]
    public class C2RM_MOT_INFO : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOT_INFO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_MOT_INFO();
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
    public class RM2C_MOT_INFO : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOT_INFO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_MOT_INFO();
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
		///远征信息
		public SLeadMot sLeadMot = new SLeadMot();
    };
	
	[Serializable()]
    public class C2RM_MOT_TEAM : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOT_TEAM;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_MOT_TEAM();
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
    public class RM2C_MOT_TEAM : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOT_TEAM;

        private void init(int len)
        {
            if (len >= 0)
            {
                sMotTeam = new SMotTeam[len];
                for (int i = 0; i < len; i++)
                {
                    sMotTeam[i] = new SMotTeam();
                }
            }
        }

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_MOT_TEAM();
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
		/// 佣兵信息
		public SMotTeam[] sMotTeam;
    };
	
	[Serializable()]
    public class C2RM_MOT_PET : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOT_PET;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_MOT_PET();
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
    public class RM2C_MOT_PET : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOT_PET;

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
            return new RM2C_MOT_PET();
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
		/// 卡牌出战信息
		public SMotPet[] sMotPet;
    };

	[Serializable()]
	public class C2RM_MOT_BEAST : ExFormatterBinary, IProtocal
	{
		//补签
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOT_BEAST;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new C2RM_MOT_BEAST();
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
	public class RM2C_MOT_BEAST : ExFormatterBinary, IProtocal
	{
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOT_BEAST;
		
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
			return new RM2C_MOT_BEAST();
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
		/// 卡牌出战信息
		public SMotBeast[] sMotBeast;
	};

	[Serializable()]
    public class C2RM_MOT_RESET : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOT_RESET;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_MOT_RESET();
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
    public class RM2C_MOT_RESET : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOT_RESET;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_MOT_RESET();
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
    };
	
	[Serializable()]
    public class C2RM_MOT_GOTO : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOT_GOTO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_MOT_GOTO();
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
    public class RM2C_MOT_GOTO : ExFormatterBinary, IProtocal
    {
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOT_GOTO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_MOT_GOTO();
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
    public class C2RM_MOT_PK_BEGIN : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOT_PK_BEGIN;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_MOT_PK_BEGIN();
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
    public class C2RM_MOT_PK_MIN : ExFormatterBinary, IProtocal
    {
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOT_PK_MIN;


        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new C2RM_MOT_PK_MIN();
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
    public class C2RM_MOT_PK_OVER : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOT_PK_OVER;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_MOT_PK_OVER();
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
		/// 魂兽id
		public UInt64 luiIdBeast = new UInt64();
		///魂兽剩余怒气百分比
		public float fBeastAnger = new float();
		///敌人魂兽剩余怒气百分比
		public float fBeastAngerEnemy = new float();
    };
	
	
	[Serializable()]
    public class RM2C_MOT_REWARD : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOT_REWARD;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_MOT_REWARD();
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
		///助战币
		public UInt32 uiMotFightMoney = new UInt32();
		///是否胜利0：否 1：是
		public byte isWin = new byte();
    };
	
    [Serializable()]
    public class C2RM_MOT_BOX : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOT_BOX;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_MOT_BOX();
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
    public class RM2C_MOT_BOX : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOT_BOX;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_MOT_BOX();
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
		/// 金币, 加法
		public SMoney sMoney = new SMoney();
		///远征币只是用来显示
		public UInt32 uiMotMoney = new UInt32();
		
    };
	
	[Serializable()]
    public class C2RM_MOT_BUY_BUFFER : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOT_BUY_BUFFER;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_MOT_BUY_BUFFER();
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
		///buffer id
		public UInt32 uiBufferId = new UInt32();
    };
	
	
	[Serializable()]
    public class RM2C_MOT_BUY_BUFFER : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOT_BUY_BUFFER;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_MOT_BUY_BUFFER();
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
		
		///
		public UInt32 uiListen = new UInt32();
		///结果
		public int iResult = new int();
		///只用来显示
		public UInt32 uiMotFtMoney = new UInt32();
		///购买的buffer id用来赋值，修改数据模型
		public UInt32 uiBufferId = new UInt32();
    };
	
	[Serializable()]
    public class RM2C_MOT_TEAM_UPDATE : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOT_TEAM_UPDATE;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_MOT_TEAM_UPDATE();
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
		
		///更新远征敌人队伍信息
		public SMotTeam sMotTeam = new SMotTeam();
    };
	
	[Serializable()]
    public class RM2C_MOT_PK_ERROR : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOT_PK_ERROR;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_MOT_PK_ERROR();
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
		
		///更新远征敌人队伍信息
		public int iResult;
    };
	
	[Serializable()]
    public class C2RM_MOT_HELPER : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOT_HELPER;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_MOT_HELPER();
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
    public class RM2C_MOT_HELPER : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOT_HELPER;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_MOT_HELPER();
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
    public class C2RM_MOT_HELPER_INFO : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOT_HELPER_INFO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_MOT_HELPER_INFO();
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
    public class RM2C_MOT_HELPER_INFO : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOT_HELPER_INFO;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_MOT_HELPER_INFO();
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
    public class C2RM_MOT_LEAVE : ExFormatterBinary, IProtocal
    {
		//补签
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_MOT_LEAVE;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_MOT_LEAVE();
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
    public class RM2C_MOT_LEAVE : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOT_LEAVE;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_MOT_LEAVE();
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
    public class RM2C_MOT_HELPER_IS_IN_TEAM : ExFormatterBinary, IProtocal
    {
		//补签返回
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_MOT_HELPER_IS_IN_TEAM;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
            return new RM2C_MOT_HELPER_IS_IN_TEAM();
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
		/// 佣兵是否在阵法上0:否 1：是
		public byte cIsInTeam = new byte();
    };
	
	[Serializable()]
	public class C2RM_GUIDE_STR_YXH : ExFormatterBinary, IProtocal
	{
        public static readonly E_OPCODE OPCODE = E_OPCODE.EP_C2RM_GUIDE_STR_YXH;

        public static IProtocal Create(ushort msg, HeaderBase h)
        {
			return new C2RM_GUIDE_STR_YXH();
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
		
		public void setGuideStr(string psGuideStr)
        {
            byte[] strByte = System.Text.Encoding.UTF8.GetBytes(psGuideStr);
            for (int i = 0; i < strByte.Length; i++)
            {
                cGuideStrYXH[i] = strByte[i];
            }
        }
		
		public byte[] cGuideStrYXH = new byte[300];	
	};

	[Serializable()]
	public class RM2C_POWER_LV_ADD : ExFormatterBinary, IProtocal
	{
		//补签返回
		public static readonly E_OPCODE OPCODE = E_OPCODE.EP_RM2C_POWER_LV_ADD;
		
		public static IProtocal Create(ushort msg, HeaderBase h)
		{
			return new RM2C_POWER_LV_ADD();
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
		/// 升级给的体力，只是用来显示
		public UInt16 cIsInTeam = new byte();
	};

}