
using System.Collections;

public class TypeCsvSign: SuperType
{
	public int id;
	public int year;
	public int month;
	public int day;
	//签到奖励
	public int idReward;
	//豪华签到
	public int idReward_vip;
	//双倍奖励需要的VIP等级
	public int lv_vip;
	//双倍奖励实际的奖励倍数
	public int mul_vip;
}
