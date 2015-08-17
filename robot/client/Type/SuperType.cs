
using System.Collections;

public class SuperType 
{
	/// <summary>
	/// Sets the init.
	/// </summary>
	/// <param name='value'>
	/// Value.
	/// </param>
	public void setInit(Hashtable value)
	{
		foreach(DictionaryEntry kv in value)
		{
			//为每个变量赋值
			//你的破绽太多
			GetType().GetProperty(kv.Key.ToString()).SetValue(this, kv.Value, null);
		}
	}
	/// <summary>
	/// Gets the hash.
	/// </summary>
	/// <returns>
	/// The hash.
	/// </returns>
	public Hashtable getHash()
	{
		//属性获取器
		System.Reflection.FieldInfo[] propertys = GetType().GetFields();
		
		
		Hashtable result = new Hashtable();
		foreach(System.Reflection.FieldInfo pro in propertys)
		{
			result.Add(pro.Name, pro.GetValue(this));
		}
		return result;
	}
	/// <summary>
	/// Clone this instance.
	/// </summary>
	public SuperType clone()
	{
		return (SuperType)this.MemberwiseClone();
	}
}
