
using System.Collections;

public class TypeCsvActivEventTime : SuperType
{
	public int id;
	public string name;
	public string info;
	public int idView = 1;
	/// 周1 - 周日
	public int index0;
	public int index1;
	public int index2;
	public int index3;
	public int index4;
	public int index5;
	public int index6;
	/// 开始结束时间
	public float begin_hour;
	public float begin_min;
	public float end_hour;
	public float end_min;
	/// 我的副本信息
	public string[] id_event;
	
	public string infoTime;
	public int lv;

	public int limit;
	/// 限制哪个系统开启
	public int limitSystem;

}
