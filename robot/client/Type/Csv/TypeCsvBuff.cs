
using System.Collections;

public class TypeCsvBuff : SuperType
{
	/// id编号
	public int id;
	/// buff在身体上的位置
	public string view;
	public string viewBoss;
	
	/// 变成的模型样子
	public int idViewChange = 2027;
	/// 尺寸缩放
	public float viewScale = 1f;
	
	public int idView;
	public float viewHeightMul;
	/// 名字以及描述
	public string name;
	public string info;
	/// 执行时间
	public float time;
	/// 执行次数
	public float doneTimes;
	/// 减速
	public float slow;
	/// 眩晕
	public bool isLockAttack;
	/// 限制移动
	public bool isLockMove;
	/// 禁止技能
	public bool isLockSkill;
	/// 生命 扣除
	public float hpSub;
	/// 生命 护罩
	public float hpDef;
	/// 生命 反弹
	public float hpBacklashMul;
	/// 生命 操作类型(反弹或者吸血的时候 要看是魔法还是物理)
	public int hpDoneType;
	/// 是否嘲讽
	public bool isTaunt;
	/// 生命 掠夺
	public float hpSuckMul;
	/// buff 结束效果
	public string effectEnd;
	/// buff 中断效果
	public string effectBreak;
	/// 是否打断 +Buff时候
	public bool isBreak;
	
	/// 是否增益
	public bool isGood;
	/// 是否免疫邪恶buff
	public bool isImmune;
	/// 是否免疫增益buff
	public bool isImmuneGood;
	/// 是否能被驱散
	public bool isCanImmune;

	/// buff 类型
	public string type;


	/// buff 效果
	public int lvGroup;
	public int lvPriority;
	/// shader 效果
	public int idShader = 4;
	
	/// buff 增加属性
	public float strAdd;
	public float agiAdd;
	public float intellAdd;
	public float vitAdd;
	public float hpAdd;
	public float atkAdd;
	public float atkDefAdd;
	public float magicAdd;
	public float magicDefAdd;
	//	力量	敏捷	智力	体力	血量	物理攻击	物理防御	魔法攻击	魔法防御

	/// 为光环添加类型 1 暴怒光环（怒气增加加上我的暴怒光环） 2015.03.25 WILL DONE
	public int typeValue;
	public float typeValueAdd;


	/// 释放者添加的 怒气
	public float angerAtk;
	/// 防御者添加的 怒气
	public float angerDef;

	/// WILL DONE Gus 2015.07.08
	public int idSkillTimeOver;

	/// WILL TESTING Gus 2015.07.30 物理免疫或者魔法免疫
	public bool isDefAtk;
	public bool isDefMagic;
}
