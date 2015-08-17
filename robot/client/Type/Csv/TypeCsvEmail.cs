
using System.Collections;
using System.Collections.Generic;

public class TypeCsvEmail : SuperType
{
	public int id;
	public string name;
	public string info;
	public int idview;
	///类型（1人民币，2游戏币，3体力，4英雄，5物品，6正义徽章, 7碎片, 8万能碎片 , 9竞技积分, 10 经验）
	public int type;
	public int id_hero;
	public int cnt_hero;
	public int id_pro;
	public int cnt_pro;
	public int smoney;
	public int qmoney;
	public ulong exp;
	/// 正义徽章
	public int badge;
	public int piece;
	public int time;
	public int time_open;
	/// 竞技积分
	public int socre_fight;
}
