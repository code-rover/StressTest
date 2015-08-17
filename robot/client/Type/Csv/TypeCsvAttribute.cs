
using System.Collections;

public class TypeCsvAttribute : SuperType
{
	/// 闪躲率 = duckA * 被击方闪躲等级 / (duckB + duckC * 攻击方角色等级)
	public float duckA;
	public float duckB;
	public float duckC;
	/// 暴击率 = critA * (攻击方暴击等级 - 被击抗暴等级) / (critB + critC * 被击方角色等级)
	public float critA;
	public float critB;
	public float critC;
	/// 远程
	public float combatDisNear = 3f;
	public float combatDisFar = 6f;
	/// 胜利
	public string winMusic;
	/// 体力恢复
	public int powerAdd = 5;
	public float powerAddTime = 300f;
	/// 技能点回复
	public int  cntSkillPoint;
	public float skillPointAddTime = 300f;

	
	/// 鼓舞消耗的游戏币
	public int qmoney_excite_boss = 10;
	/// 复活次数
	public int bossRelifeTimes;
	/// 复活世界boss需要消耗的符石
	public int bossRelifeQMoney;
	/// 复活世界boss需要消耗的银币
	public int bossRelifeSMoney;
	//未充值用户购买体力次数
	public int cntPowerBuy;
	//充值用户购买体力次数
	public int cntPowerBuyVip;
	///	10	一个格子的价格
	public int bagPrice;
	///	5	一次购买的数量
	public int bagCnt;
	///	280	卡牌仓库购买上限
	public int cntPetBar;
	///	70	卡牌背包购买上限
	public int cntPetBag;
	///	50	装备背包购买上限
	public int cntEquipBag;
	///	50	材料背包购买上限
	public int cntPropBag;
	///	20	每次购买体力增加的数量
	public int powerBuyCnt;
	///	10	购买体力话费钻石
	public int powerBuyCost;

	
	//钻石抽消耗的的钻石
	public int luckyShopQMoney_1;
	//钻石十连抽消耗的钻石
	public int luckyShopQMoney_10;
	//友情抽消耗的友情点
	public int luckyShopFriend_1;
	//友情十连抽消耗的友情点
	public int luckyShopFriend_10;
	
	/// buff系数
	public float buffCoefMul;
	public float buffCoefMulBeast;
	/// 当前版本最大等级
	public int limitLv;
	/// 大于百分之30的部分 合成英雄消耗的宝石
	public int pieceQMoney;
	
	/// 竞技积分消耗
	public int pkAddCnt = 20;
	/// 点金石最大次数
	public int stoneTimes;
	///每次购买的好友上限数
	public int friendBuyCnt;
	//购买好友上限每次花费的钻石
	public int fiendBuyQMoney;
	//卡牌背包上限
	public int bagCntMax;
	
	//队伍2和 队伍3 开启的等级
	public int team_2;
	public int team_3;
	
	//public string signDeclare;
	
	//友情抽免费cd（按s记）
	public int luckyShopFriend_cd;
	//钻石抽免费cd（按s记）
	public int luckyShopQMoney_cd;
	//友情抽每天免费次数
	public int luckyShopFriendFreeTimes;
	//每次抽魂匣消耗的钻石
	public int luckySoulQMoney;
	
	/// pk 时候血量控制翻倍
	public float combatPKHpTotalMul = 5f;

	/// 杀人
	public float angerKill;
	/// 被杀
	public float angerDeath;
	/// 打人系数
	public float angerAtk;
	/// 被打系数
	public float angerDef;
	/// 释放buff系数
	public float angerBuffAtk;
	/// 被增加buff系数
	public float angerBuffDef;
	/// 暴击系数
	public float angerCrit;








	/// 公式形式(aX^3+bX^2+cX+d)/e（10000）,abcde可配置，X表示分钟，结果为总产量
	public float escortTimeCoefA;
	public float escortTimeCoefB;
	public float escortTimeCoefC;
	public float escortTimeCoefD;
	public float escortTimeCoefE;

	/// 等级*系数
	public float escortLvCoef;

	/// 公式形式(aX^2+bX)/c + d  100+1,abcd可配置，X表示等级差
	public float escortLvSubCoefA;
	public float escortLvSubCoefB;
	public float escortLvSubCoefC;
	public float escortLvSubCoefD;
	/// 战斗力系数
	public float escortFightMul;

	/// 护送 护送每次购买面包个数
	public int escortCntBuyBread;
	/// 护送 护送购买面包次数上限
	public int escortMaxBuyBread;
	/// 护送 面包恢复时间
	public float escortCDBread;
	/// 护送 面包上限
	public int escortMaxBread;
	/// 护送 进入副本需要面包数
	public int escortFightBread;
	/// 护送 最大搜索次数
	public int escortMaxSearchCnt;
	/// 护送 log做大显示数
	public int escortMaxLogCnt;
	/// 护送 每次获得面包个数
	public int escortRecoverBreadCnt;

	/// 防守布阵限制等级
	public float escortSafeLimitLv;

	/// <summary>
	/// 海上奖励关累计防守奖励
	/// </summary>
	public float motPkA;
	/// 海上奖励关累计防守奖励
	public float motPkB;
	/// 海上奖励关累计防守奖励
	public float motPkC;


	/// 限制布阵等级
	public int levelBoss1;
	public int levelBoss2;
	public int levelBoss3;

	/// 海上奖励关 英雄缩放
	public float motPkHeroScale = 2f;
	/// 海加尔山防守奖励关报名限制等级
	public float motLv;

}
