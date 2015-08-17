
using System.Collections;

public class TypeCsvBeastList : SuperType
{

	public int id;
	public int idCsv;
	public string infoLabel;
	/// <summary>
	/// 魂兽的名字对应的图集
	/// </summary>
	public string nameAtlas;
	/// <summary>
	/// 魂兽的名字对应图集的图片名
	/// </summary>
	public string nameSprite;
	/// <summary>
	/// 0为待机动画 后面为点击之后的随机动画
	/// 魂兽签到里面用
	/// </summary>
	public string[] aniShow;
	/// <summary>
	/// 展示音效
	/// </summary>
	public string audioHero;
	/// <summary>
	/// 推荐说明 魂兽签到tip里面拼接用
	/// 前面的为 beast里面的说明 + 主动技能的名字和说明
	/// </summary>
	public string betterArray;
	/// <summary>
	/// 签到用的背景图
	/// </summary>
	public string spriteName;
	/// <summary>
	/// 签到界面魂兽缩放比例
	/// </summary>
	public float scaleSign;
	/// <summary>
	/// 签到界面 魂兽特征对应的图片名字
	/// </summary>
	public string features;
	public float rotationY;
}
