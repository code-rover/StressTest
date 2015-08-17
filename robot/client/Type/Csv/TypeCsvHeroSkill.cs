
using System.Collections;

/// 英雄技能类
public class TypeCsvHeroSkill : TypeCsvHeroSkillBase
{
	/// 技能cd
	public float cd;
	public float cdInit = 10f;
	
	/// 动画
	public string anim;
	/// 攻击效果
	public int idEffect;
	/// 伤害特效的位置
	public float effectMul = 0.5f;
	/// 释放类型 0原地1冲过去2瞬移到身后
	public int releaseType;
	/// 我的子弹技(如果没有就没有)id|个数|时间|攻击次数
	public string[] bulletData;
	
	/// 下一个技能点
	public int idNext;
	/// 暴击相机
	public int cameraCrit;
	/// 释放距离
	public float releaseDistance;
	/// 攻击类型 1物理2魔法3加血4复活
	public int atkType;
	/// 攻击倍率
	public float atkMul;
	/// 攻击附加值
	public int atkAdd;
	/// 攻击总人数
	public int atkTotal;

	/// 目标选择1普通2后排3血少4死亡5自身范围
	public int atkTarget;
	/// 绑定特效
	public string effectBind;

	/// 命中敌人加入的buff
	public string[] idBuffSelfHit;

	/// 我自己中的buff
	public string[] idBuffSelf;
	/// 敌人命中方中的buff
	public string[] idBuffBeatener;
	/// 给全体同僚加
	public string[] idBuffTeam;
	/// 给全体敌人家
	public string[] idBuffEnemy;
	
	// add by ssy
	/// <summary>
	/// 绑定在技能上的音效
	/// </summary>
	public string audioBind;
	// add end
	
	
	/// 黑屏 从白到黑
	public float timeBegin = 0.0f * 30f;
	/// 黑屏 全黑
	public float timeMid = 0.9f * 30f;
	/// 黑屏 从黑到白
	public float timeEnd = 0.0f * 30f;
	
	/// 减速时间
	public float timeSlow = 0.1f * 30f;
	
	/// 高度和方向速度
	public float speedJump = 0f;
	public float speedDir = 0f;
	
	/// 闪避倍率
	public float duckMul = 1f;
	/// 暴击乘量
	public float critMul = 1f;
	
	/// 加上一个召唤技能
	public int idSummon;
	
	/// 引导技能的时间
	public float channelTime = -1f;
	/// 引导时候移动不移动 1 目标正反来回移动 2 跟随应该打的目标
	public int changeMoveType;

	/// 引导闪电链 [闪电特效明|持续时间（全部链接后开始计算）|伤害几次|伤害衰减]
	/// 持续时间 0 全部链接上后删除
	/// 伤害几次 0和1（或者时间为0） 为只有链接点时候产生伤害  > 1 单位时间内的伤害次数
	/// 链接类型 预留选项
	public string[] bulletLine;

	/// 技能
	public float angerMul;

	/// 是否是不能锁定的技能 0可以被锁定 1不可以被锁定的技能
	public bool isUnLimitSkill;


	/// WILL DONE Gus 2015.07.08
	/// 条件 对于主动被动和自动释放的技能无效 cd仍然是影响这个技能的条件 只有配置在 Hero.csv 表中 idSkillAI字段中有效

	/// AI （隐藏释放技能）：
	///1 Boss血量 							type|value 低于%
	///2 玩家无职业（或死亡）后释放 			type|value
	///3 玩家有职业后释放（死亡后不放）			type|value
	///4 玩家无距离（或死亡）后释放				type|value 1近战2中程3远程
	///5 玩家有距离后释放（死亡后不放）			type|value 1近战2中程3远程
	///6 职业差别释放							type|value 无效值
	///7 距离差别							type|value 无效值
	///8 战斗进行的时间 > (初始时间就可以实现)  type|value（无需配置，技能中放的自动的技能就ok了）
	public int AIType;
	public float AIValue;
	/// 打掉 目标总血量 比例 总血量
	public float atkHPTotalMul;

	/// 攻击范围
	public float atkArea;
	/// 攻击长
	public float atkArea2;
	
	/// 0 圆形（atkArea 为半径） 1 方形 （atkArea为宽 __ atkArea2为高 |） 2扇形（atkArea为半径 atkArea2为弧度）
	public int atkAreaType;

	/// 不能被打断的技能 fuck！！！！！
	public bool isUnBreak;

	/// -cd时间
	public float critAtkCD;
	public float critDefCD;
}

