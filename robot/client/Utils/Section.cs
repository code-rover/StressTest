using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace utils
{
	///******************
	//配置文件分组.
	//
	//********************
	public class Section
	{
		public Section (string grp)
		{
			_Group = grp;
			_Pairs = new Dictionary<string,List<ValuePair>>();
		}
		
		public ValuePair getPair(string key)
		{
			List<ValuePair> pairs = getPairs(key);
			if(pairs != null)
			{
				return pairs[0];
			}
			else
			{
				return null;
			}
		}
		
		public List<ValuePair> getPairs(string key)
		{
			if(_Pairs.ContainsKey(key))
			{
				return _Pairs[key];
			}
			else
			{
				return null;
			}
		}
		
		public void addValue(string key,string val)
		{
			List<ValuePair> pairs = getPairs(key);
			if(pairs == null)
			{
				pairs = new List<ValuePair>();
				pairs.Add( new ValuePair(key,val) );
				_Pairs[key] = pairs;
			}
			else
			{
				pairs.Add( new ValuePair(key,val) );
			}
		}
		
		public void removeValue(string key,string val)
		{
			List<ValuePair> pairs = getPairs(key);
			if( pairs != null )
			{
                try
                {
                    _Equ_Val = val;
                    pairs.RemoveAll( equ_ValuePair );
                }
                catch (Exception e)
                {
                    Logger.Error(e.Message);
                }
			}
		}
		
		public void removeValue(string key)
		{
			if(_Pairs.ContainsKey(key))
			{
				_Pairs.Remove(key);
			}
		}
		
		public bool trySetValue(string key, string val)
		{
			List<ValuePair> pairs = getPairs(key);
			if( pairs != null )
			{
				if(pairs.Count == 0)
				{
					return false;
				}
				else
				{
					pairs[0].Value = val;
					return true;
				}
			}
			else
			{
				return false;
			}
		}
		
		public bool trySetValue(string key,string val,int idx)
		{
			List<ValuePair> pairs = getPairs(key);
			if( pairs != null )
			{
				if( idx < pairs.Count )
				{
					pairs[idx].Value = val;
                    return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}
		
		public string Group
		{
			get { return _Group; }
			set { _Group = value; }
		}

        private static bool equ_ValuePair(ValuePair v)
        {
            return v.Value == _Equ_Val;
        }
		
		private string _Group;
        private static string _Equ_Val = "";
		private Dictionary<string,List<ValuePair>> _Pairs;
	}
}

