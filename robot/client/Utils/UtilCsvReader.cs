using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System;
using System.Text;

//TODO: 找到后需要删除一部分缓存的。

/// csv 表格读取
public class UtilCsvReader
{
	
	private static readonly string _sStrR = "\r";
	private static readonly string _sStrN = "\n";
	private static readonly string _sSplit = ",";
	
	/// 我的文本信息
	private string _text;
	/// 键值
	public Dictionary<string, int> _key = new Dictionary<string, int>();
	/// 我的值存放地点
	private List<string[]> _keyValue = new List<string[]>();
	/// 数据缓存 = 这个文本的数据进行 缓存
	private Dictionary<string, List<Dictionary<string, string>>> _searchs = new Dictionary<string, List<Dictionary<string, string>>>();
	
	/// 是否清除数据
	public bool isClearData = true;
	
	/// 构造函数
	public UtilCsvReader(string s_text)
	{
		
		// add by ssy
		// 去掉bom头
		string temp = s_text.Substring(0,3);
		Byte[] bytes = System.Text.UTF8Encoding.UTF8.GetBytes(temp);
		
		string str = "";
		
		if(bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
		{
			_text = s_text.Substring(1, s_text.Length - 1);
		}
		else
		{
			_text = s_text;
		} 

		// add end
		initText();
	}

	public UtilCsvReader(FileStream f_stream)
	{
		if(f_stream == null)
		{
			//UtilLog.LogError("csvReader construct error!!");
			return;
		}

		byte[] temp = new byte[f_stream.Length]; 
		f_stream.Read(temp, 0, (int)f_stream.Length);
		_text = System.Text.UTF8Encoding.UTF8.GetString(temp);
		initText();
	}


	/// 初始化文本>>数据转换处理
	private void initText()
	{
		/// 拆分数据
		_text = _text.Replace(_sStrR, string.Empty);
		string[] arr = _text.Split(_sStrN.ToCharArray());
		/// 检验数据有效性
		if(arr.Length < 2)
		{
			return;
		}
		
		/// 生成字段key值
		string[] arrLabel = arr[0].Split(_sSplit.ToCharArray());
		int index = 0;
		for(index = 0; index < arrLabel.Length; index++)
		{
			_key.Add(arrLabel[index], index);
			
		}
		
		/// 生成值内容
		for(index = 1; index < arr.Length; index++)
		{
			string[] arrValue = arr[index].Split(_sSplit.ToCharArray());
			/// 优化内存
			for(int i = 0; i < arrValue.Length; i++)
			{
				arrValue[i] = string.Intern(arrValue[i]);
			}
			_keyValue.Add(arrValue);
		}
		/// 原有文本清空
		_text = null;
		arr = null;
	}
	/// 寻找多行数据
	public List<Dictionary<string, string>> searchs(params object[] objs)
	{
		if(objs.Length < 2 || objs.Length % 2 != 0)
		{
			//UtilLog.LogError("参数传递不正确, 参数必须成对出现");
			return null;
		}
		StringBuilder key = new StringBuilder ();
		for(int index = 0; index < objs.Length; index++)
		{
			/// 检测字段有没有效
			if(index % 2 == 0)
			{
				/// 如果不存在
				if(!_key.ContainsKey(objs[index].ToString()))
				{
					//UtilLog.LogError("参数中出现的未找到的字段 " + objs[index].ToString());
					return null;
				}
			}
			key.Append(objs[index]);
			key.Append(_sSplit);
		}
		/// 检测缓存
		if(_searchs.ContainsKey(key.ToString()))
		{
			return _searchs[key.ToString()];
		}
		List<Dictionary<string, string>> result = null;
		
		/// 2014.07.28 我搜索到的内容信息
		List<int> searchIndex = null;
		/// 缓存不存在,搜索吧
		for(int i = 0; i < _keyValue.Count; i++)
		{
			bool isSearch = true;
			/// 检测数据
			for(int j = 0; j < objs.Length; j += 2)
			{
				/// 索引
				int index = _key[objs[j].ToString()];
				if(_keyValue[i].Length <= index)
				{
					isSearch = false;
					break;
				}
				if(_keyValue[i][index] != objs[j + 1].ToString())
				{
					isSearch = false;
					break;
				}
			}
			/// 如果找到了
			if(isSearch)
			{
				Dictionary<string, string> v = new Dictionary<string, string>();
				foreach(string k in _key.Keys)
				{
					/// 越界了不追究
					if(_keyValue[i].Length <= _key[k])
						continue;
					/// 装在数据
//					v.Add(k, _keyValue[i][_key[k]]);
					/// 优化内存
					v.Add(string.Intern(k), string.Intern(_keyValue[i][_key[k]]));
				}
				if(null == result)
					result = new List<Dictionary<string, string>>();
				result.Add(v);
				
				/// 2014.07.28 把我存的东西缓存出来
				if(isClearData)
				{
					if(null == searchIndex)
						searchIndex = new List<int>();
					searchIndex.Add(i);
				}
				
			}
		}
		
		/// 2014.07.28 清除掉没用的表格信息内容
		if(null != searchIndex)
		{
			for(int i = searchIndex.Count - 1; i >= 0; i--)
			{
				_keyValue.RemoveAt(searchIndex[i]);
			}
		}
			
		
		
		/// 装载缓存
		_searchs.Add(key.ToString(), result);
		
		return result;
	}
	/// 寻找1行数据
	public Dictionary<string, string> search(params object[] objs)
	{
		/// 效率优化
		if(objs.Length < 2 || objs.Length % 2 != 0)
		{
			//UtilLog.LogError("参数传递不正确, 参数必须成对出现");
			return null;
		}
		StringBuilder key = new StringBuilder();
		for(int index = 0; index < objs.Length; index++)
		{
			/// 检测字段有没有效
			if(index % 2 == 0)
			{
				/// 如果不存在
				if(!_key.ContainsKey(objs[index].ToString()))
				{
					//UtilLog.LogError("参数中出现的未找到的字段 " + objs[index].ToString());
					return null;
				}
			}
			key.Append(objs[index]);
			key.Append(_sSplit);
		}
		/// 检测缓存
		if(_searchs.ContainsKey(key.ToString()))
		{
			if(null == _searchs[key.ToString()] || _searchs[key.ToString()].Count <= 0)
				return null;
			return _searchs[key.ToString()][0];
		}
		key.Append("searchOne");
		/// 检测单独的缓存
		if(_searchs.ContainsKey(key.ToString()))
		{
			if(null == _searchs[key.ToString()] || _searchs[key.ToString()].Count <= 0)
				return null;
			return _searchs[key.ToString()][0];
		}
		
		Dictionary<string, string> result = null;
		/// 缓存不存在,搜索吧
		for(int i = 0; i < _keyValue.Count; i++)
		{
			bool isSearch = true;
			/// 检测数据
			for(int j = 0; j < objs.Length; j += 2)
			{
				/// 索引
				int index = _key[objs[j].ToString()];
				if(_keyValue[i].Length <= index)
				{
					isSearch = false;
					break;
				}
				
				if(_keyValue[i][index] != objs[j + 1].ToString())
				{
					isSearch = false;
					break;
				}
			}
			/// 如果找到了
			if(isSearch)
			{
				result = new Dictionary<string, string>();
				foreach(string k in _key.Keys)
				{
					/// 越界了不追究
					if(_keyValue[i].Length <= _key[k])
						continue;
					/// 装在数据
//					result.Add(k, _keyValue[i][_key[k]]);
					/// 优化内存
					result.Add(string.Intern(k), string.Intern(_keyValue[i][_key[k]]));
				}
				
				/// 2014.07.28 清除掉没用的表格信息内容
				if(isClearData)
				{
					_keyValue.RemoveAt(i);
				}
				/// 如果寻到了返回
				break;
			}
		}
		/// 装载缓存
		List<Dictionary<string, string>> save = null;
		if(null != result)
		{
			save = new List<Dictionary<string, string>>();
			save.Add(result);
		}
		_searchs.Add(key.ToString(), save);
		
		
		return result;
		
		
//		List<Dictionary<string, string>> result = (List<Dictionary<string, string>>)typeof(UtilCsvReader).GetMethod("searchs").Invoke(this, new object[]{objs});
//		if(null == result || result.Count <= 0)
//			return null;
//		return result[0];
	}
	
	/// 直接为差价的变量复制
	public bool searchAndSet<T>(T val, params object[] objs)
	{
		bool result = false;
		Dictionary<string, string> searchValue = (Dictionary<string, string>)this.GetType().GetMethod("search").Invoke(this, new object[]{objs});
		
		/// 获得所有字段,为字段复制
		if(null != val && null != searchValue)
		{
			FieldInfo[] fields = val.GetType().GetFields();
			/// 赋值
			foreach(FieldInfo field in fields)
			{
				setFieldValue(field, val, searchValue);
			}
			result = true;
		}
		return result;
	}
	
	/// 直接为差价的变量复制
	public T searchAndNew<T>(params object[] objs)
		where T : new()
	{
		T result = default(T);
		Dictionary<string, string> searchValue = (Dictionary<string, string>)this.GetType().GetMethod("search").Invoke(this, new object[]{objs});
		
		/// 获得所有字段,为字段复制
		if(null != searchValue)
		{
			result = new T();
			FieldInfo[] fields = result.GetType().GetFields();
			/// 赋值
			foreach(FieldInfo field in fields)
			{
				setFieldValue(field, result, searchValue);
			}
		}
		return result;
	}
	
	/// 直接获得一组数据,直接为差价的变量复制
	public List<T> searchsT<T>(params object[] objs) 
		where T : new()
	{
		List<T> result = new List<T>();
		List<Dictionary<string, string>> searchValues = (List<Dictionary<string, string>>)this.GetType().GetMethod("searchs").Invoke(this, new object[]{objs});
		/// 获得所有字段,为字段复制
		if(null != searchValues)
		{
//			foreach(Dictionary<string, string> searchValue in searchValues)
			for(int i = 0; i < searchValues.Count; i++)
			{
				Dictionary<string, string> searchValue = searchValues[i];
				T val = new T();
				FieldInfo[] fields = val.GetType().GetFields();
				/// 赋值
				foreach(FieldInfo field in fields)
				{
					setFieldValue(field, val, searchValue);
				}
				result.Add(val);
			}
		}
		return result;
	}
	/// 获得所有列的变量
	public T getAttribue<T>(string attribueName, string attribueValue)
		where T : new()
	{
		
		Dictionary<string, string> searchValue = new Dictionary<string, string>();
		if(!_key.ContainsKey(attribueName) || !_key.ContainsKey(attribueValue))
		{
			return default(T);
		}
		int indexName = _key[attribueName];
		int indexValue = _key[attribueValue];
		for(int index = 0; index < _keyValue.Count; index++)
		{
			if(_keyValue[index].Length <= indexName || _keyValue[index].Length <= indexValue)
				continue;
			if(searchValue.ContainsKey(_keyValue[index][indexName]))
				continue;
			searchValue.Add(_keyValue[index][indexName], _keyValue[index][indexValue]);
		}
		/// 只为变量进行解析
		T result = new T();
		FieldInfo[] fields = result.GetType().GetFields();
		/// 赋值
		foreach(FieldInfo field in fields)
		{
			setFieldValue(field, result, searchValue);
		}
		return result;
	}
	/// 获得整个报表
	public List<Dictionary<string, string>> getTable()
	{
		List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();
		/// 缓存不存在,搜索吧
		for(int i = 0; i < _keyValue.Count; i++)
		{
			Dictionary<string, string> v = new Dictionary<string, string>();
			foreach(string k in _key.Keys)
			{
				/// 越界了不追究
				if(_keyValue[i].Length <= _key[k])
					continue;
				/// 装在数据
				v.Add(k, _keyValue[i][_key[k]]);
			}
			result.Add(v);
		}
		return result;
	}
	/// 获得整体表的数据
	public List<T> getTableAndNew<T>()
		where T : new()
	{
		List<T> result = new List<T>();
		List<Dictionary<string, string>> searchValues = getTable();
		/// 缓存不存在,搜索吧
		/// 获得所有字段,为字段复制
		if(null != searchValues)
		{
//			foreach(Dictionary<string, string> searchValue in searchValues)
			for(int i = 1; i < searchValues.Count; i++)
			{
				Dictionary<string, string> searchValue = searchValues[i];
				try
				{
					T val = new T();
					FieldInfo[] fields = val.GetType().GetFields();
					/// 赋值
					foreach(FieldInfo field in fields)
					{
						setFieldValue(field, val, searchValue);
	
					}
					result.Add(val);
				} catch {continue;}
			}
		}
		return result;
	}
	
	
	
	/// 字段复制
	private void setFieldValue(FieldInfo field, object obj, Dictionary<string, string> hash)
	{
		if(obj == null) return;
		if(field == null) return;
		if(hash == null) return;
		if(!hash.ContainsKey(field.Name)) return;
		
		try
		{
			if(field.FieldType == typeof(string[][]))				
			{
				if(hash[field.Name] == null || hash[field.Name] == "" || hash[field.Name] == "#")
				{
					field.SetValue(obj, null);
					return;
				}
				//先看是不是用#作分割
				string[] arrSplit1 = hash[field.Name].Split("#".ToCharArray());			
				if(null != arrSplit1)														
				{
					List<string[]> lstSplit2 = new List<string[]>();
					for(int i = 0;i < arrSplit1.Length;i++)
					{
						//空的就不做考虑
						if(string.IsNullOrEmpty(arrSplit1[i]))continue;
						//再分
						string[] arrTemp = arrSplit1[i].Split("|".ToCharArray());
						List<string> list = new List<string>();
						foreach(string str in arrTemp)
						{
							if(string.IsNullOrEmpty(str))continue;
							//只加非空字符串
							list.Add(str);
						}
						lstSplit2.Add(list.ToArray());
					}
					if(lstSplit2.Count > 0)
					{
						//二维数组赋给二维数组
						field.SetValue(obj,lstSplit2.ToArray());
					}
				}
				return;
			}
			//如果不是二维数组，直接存起来
			if(field.FieldType.IsArray)
			{
				if(hash[field.Name] == null || hash[field.Name] == "" || hash[field.Name] == "#")
					field.SetValue(obj, null);
				else
					field.SetValue(obj, hash[field.Name].Split("|".ToCharArray()));
				return;
			}
			if(field.FieldType == typeof(bool))
			{
				field.SetValue(obj, hash[field.Name] == "1");
				return;
			}
			if(field.FieldType == typeof(int))
			{
				if(hash[field.Name] == "#" || string.IsNullOrEmpty(hash[field.Name]))
					field.SetValue(obj, -1);
				else
					field.SetValue(obj, int.Parse(hash[field.Name]));
				return;
				
			}
			if(field.FieldType == typeof(ulong))
			{
				if(hash[field.Name] == "#" || string.IsNullOrEmpty(hash[field.Name]))
					field.SetValue(obj, (ulong)0);
				else
					field.SetValue(obj, ulong.Parse(hash[field.Name]));
				return;
			}
			if(field.FieldType == typeof(float))
			{
				if(hash[field.Name] == "#" || string.IsNullOrEmpty(hash[field.Name]))
					field.SetValue(obj, 0f);
				else
					field.SetValue(obj, float.Parse(hash[field.Name]));
				return;
			}
			
//			field.SetValue(obj, hash[field.Name].Replace("\\n", "\n"));
			/// 优化内存
			field.SetValue(obj, string.Intern(hash[field.Name].Replace("\\n", "\n")));
		} catch (Exception se) {
			//if(hash.ContainsKey("id"))
				//UtilLog.LogError(hash["id"] + "//" + obj.GetType().Name + "/" + field.Name + " = " + hash[field.Name].ToString());
			//else
				//UtilLog.LogError(obj.GetType().Name + "/" + field.Name + " = " + hash[field.Name].ToString());
		}
	}
}
