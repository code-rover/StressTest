using System;
using System.Collections;
using System.Collections.Generic;

namespace utils
{
	///******************
	//配置文件键值对.
	//
	//********************
	public class ValuePair
	{
		public ValuePair (string key,string val)
		{
			_Key = key;
			_Value = val;
		}
		
		public string Value 
		{
            get { return _Value; }
            set { _Value = value; }
		}
		
		public string Key
		{
			get { return _Key; }
			set { _Key = value; }
		}
		
		public int asInt()
		{
            return int.Parse(_Value);
		}
		
		public float asFloat()
		{
			return float.Parse(_Value);
		}
		
		public double asDouble()
		{
			return double.Parse(_Value);
		}
		
		public long asLong()
		{
            return long.Parse(_Value);
		}
		
		
		private string _Key;
		private string _Value;
	}
}

