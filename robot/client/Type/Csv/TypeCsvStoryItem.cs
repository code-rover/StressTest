
using System.Collections;

public class TypeCsvStoryItem : SuperType
{
	/// 编号
	public int id;
	/// 下一个编号
	public int idNext;
	
	/// 0鼠标任意点击1技能指向下一件
	public int nextActive;

	/// 出场动画
	public string animShow;
	/// 离场动画
	public string animEnd;
	/// 视图id
	public int idView;
	/// 0形象1图标（看板娘的玩意）2魂兽
	public int viewType;
	/// 0左视图1右视图、/3位数组 数据直接给坐标
	public string[] viewPostion;
	/// 名字
	public string name;
	/// 说的话的内容|分割下一句话
	public string[] info;
}
