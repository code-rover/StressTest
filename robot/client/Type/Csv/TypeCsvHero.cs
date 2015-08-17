
using System.Collections;

/// 数据属性类
public class TypeCsvHero : SuperType
{
	public int id;
	public int idNext;
	public int idSame;
	public string name;
	public string info;
	

	/// 碎片描述信息
	public string infoChip;
	/// 我的视图信息
	public int idView;
	public int idViewPainting;
	/// 普通攻击
	public int idAttack;
	/// 被动技能
	public string[] idSkill;
	/// 队长技能
	public int idSkillLeader;
	/// 我爆炸技能
	public int idSkillBomb;
	public string[] bombEffect;

	/// 经验类型
	public int expType;
	/// 我的长度
	public float distance;
	/// 职业
	public int job;
	///品质 
	public int grade;
	public int gradeLv;
	/// 最高的品质
	public int gradeMax;
	/// UI界面用的模型缩放比例 heightUI * height = 实际的大小
	public float heightUI = 1f;
	/// 我的高度
	public float height = 1f;
	/// 剧情中高度
	public float heightStory = -500f;
	
	public float width = 0f;
	public float scale = 1f;
	/// 领导力
	public int leadership;
	//卖的钱
	public int moneySell;
	
	/// 消耗的卡牌数 索引0为进化成1的材料 索引最后是进阶的材料
	public string[] idConsume;
	
	
	// add by ssy drop data
	/// <summary>
	/// 掉落的卡牌和碎片
	/// </summary>
	public int idCard;
	public int idChip;
	/// <summary>
	/// 掉路概率
	/// </summary>
	public string DropCard;
	public string DropChip;
	// add end
	
	/// 我的吼声
	public string audioHero;
	public string audioDie;
	/// 合成英雄需要的碎片数量
	public int cntChip;
	
	/// 类型(0:英雄 1：小怪 2：boss 3 : 世界boss)
	public int type;
	
	/// 移动类型
	public int moveType;
	
	/// 锁定移动
	public bool lockMove;
	/// 锁定旋转
	public bool lockRotation;
	
	/// 属性
	public float atk;
	public float atkThrough;
	public float atkDef;
	public float magic;
	public float magicThrough;
	public float magicDef;
	public float crit;
	public float critDef;
	public float duck;
	public float hp;
	
	/// 力量
	public float str;
	/// 敏捷
	public float agi;
	/// 智力
	public float intell;
	/// 体力
	public float vit;


	
	
	// add by ssy
	/// <summary>
	/// 英雄的持久特效
	/// </summary>
	public string effLasting;
	
	/// <summary>
	/// 绑定在休闲动作上的特效（只有获得卡牌的ui才会播放次特效）
	/// </summary>
	public string effRelax;
	
	
	/// <summary>
	/// 绑定在休闲动作上的音效
	/// </summary>
	public string audioRelax;
	// add end
	/// 能分解成万能碎片的数量 0 为不能分解
	public int breakChip;
	/// 碎片合成消耗的游戏币
	public int moneyChip;
	/// 防御的时候添加的怒气
	public float angerDef;

	/// 限制组免疫
	public string[] buffImmuneType;


	/// WILL TESTING Gus 2015.07.08
	public string[] idSkillAI;


	/// WILL TESTING Gus 2015.07.30 物理免疫或者魔法免疫
	public bool isDefAtk;
	public bool isDefMagic;
}
