
using System.Collections;

public class TypeCsvBullet : SuperType 
{
	/// id编号
	public int id;
	/// 视图
	public string view;
	/// 速度
	public float speed;
	/// 移动类型 [1直线 2抛物线 3目标点下落 4乱轨迹(魔法弹效果)]
	public int moveType;
	/// 开火时候效果
	public string shootEffect;
	public string shootAudio;
	/// 结束后效果
	public string endEffect;
	public string endAudio;
	/// 初始消毒
	public string[] offsetPostion;
	
}
