
using System.Collections;

public class TypeCsvFBReward : SuperType 
{

	public int id;

	/// <summary>
	/// Config.EFBRewardType 掉落分类1货币2经验3碎片4卡牌5装备
	/// </summary>
	public int type;

	/// <summary>
	/// item csv id
	/// </summary>
	public string id_goods;

	public string min;

	public string max;

	/// <summary>
	/// probability
	/// </summary>
	public string probability;

	/// <summary>
	/// is multi
	/// </summary>
	public string is_mulit;

	/// <summary>
	/// 0: guai
	/// 1: boss
	/// </summary>
	public string hero_type;

}
