using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace utils
{
	/// <summary>
	/// Config file.
	/// 用于读写INI,CFG配置文件.
	/// </summary>
	/// <exception cref='Exception'>
	/// Is thrown when the exception.
	/// </exception>
	public class ConfigFile
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="utils.ConfigFile"/> class.
		/// </summary>
		/// <param name='file_path'>
		/// File_path.配置文件路径.
		/// </param>
		public ConfigFile(string file_path) 
		{
			_File_Path = file_path;
            _Sections = new Dictionary<string, Section>();
			
			try {
				using ( StreamReader reader = new StreamReader(_File_Path) ) 
				{
					load(reader);
				}
			} 
			catch(Exception e) 
			{
				Logger.Debug("the config file dose not exist.");
                Logger.Error(e.Message);
			}
			
		}
		
		public ConfigFile(string text,bool isText)
		{
			_Sections = new Dictionary<string, Section>();
            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            sw.Write(text);
            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            StreamReader text_reader = new StreamReader(ms);
            load(text_reader);
			
		}
		
		/// <summary>
		/// Load the specified reader.
		/// </summary>
		/// <param name='reader'>
		/// Reader.配置文件读取流.
		/// </param>
		/// <exception cref='Exception'>
		/// Is thrown when the exception.
		/// </exception>
		protected void load(StreamReader reader)
		{
			_Sections.Clear();
			Section sec = addSection("");
			
			while(!reader.EndOfStream)
			{
				string line = reader.ReadLine();
                line = line.Trim();
				if(line.Length > 0 && line[0] == '#')
					continue;
				
				if(line[0] == '[' && line[line.Length - 1] == ']')
				{
                    line = line.Remove(0, 1);
					line  = line.Remove(line.Length - 1,1);
					sec = addSection(line);
				}
				else
				{
					int equ_flage_pos = line.IndexOf('=');
					if(equ_flage_pos == -1)
					{
						equ_flage_pos = line.IndexOf(':');
						if(equ_flage_pos == -1)
							throw new Exception("the config file error.");
					}
					
					string k = line.Substring(0,equ_flage_pos);
                    string v = line.Substring(equ_flage_pos + 1);
					sec.addValue(k.Trim(),v.Trim());
				}
			}
		}
		
		/// <summary>
		/// Gets the section.
		/// 获取一个分组.
		/// </summary>
		/// <returns>
		/// The section.
		/// 返回配置节.
		/// </returns>
		/// <param name='grp'>
		/// Group.配置文件分组名.
		/// </param>
		public Section getSection(string grp)
		{
			if(_Sections.ContainsKey(grp))
			{
				return _Sections[grp];
			}
			else
			{
				return null;
			}
		}
		
		/// <summary>
		/// Adds the section.
		/// 添加一个分组.
		/// </summary>
		/// <returns>
		/// The section.返回添加的分组对象.
		/// </returns>
		/// <param name='grp'>
		/// Group.分组名.
		/// </param>
		public Section addSection(string grp)
		{
			if(_Sections.ContainsKey(grp))
			{
				return _Sections[grp];
			}
			else
			{
				Section sec = new Section(grp);
				_Sections.Add(grp,sec);
				return sec;
			}
		}
		
		/// <summary>
		/// Removes the section.
		///移除一个分组.
		/// </summary>
		/// <returns>
		/// The section.
		///返回移除的分组.
		/// </returns>
		/// <param name='grp'>
		/// Group.
		//分组名.
		/// </param>
		public Section removeSection(string grp)
		{
			if(_Sections.ContainsKey(grp))
			{
				Section sec = _Sections[grp];
				_Sections.Remove(grp);
				return sec;
			}
			else
			{
				return null;
			}
		}
		
		/// <summary>
		/// Gets the value.
		///配置文件中的键值获取.
		/// </summary>
		/// <returns>
		/// The value.
		///返回查询到的值.
		/// </returns>
		/// <param name='grp'>
		/// Group.
		/// </param>
		/// <param name='key'>
		/// Key.
		/// </param>
//		public string getValue(string grp,string key)
//		{
//			Section sec = getSection(grp);
//			if(sec == null)
//				return "";
//			ValuePair pair = sec.getPair(key);
//			if(pair == null)
//				return "";
//			//return pair.Value;
//			return Config.url_root + pair.Value;
//		}
//		
		/// <summary>
		/// Gets the values.
		///查询符合要求的值数组.
		/// </summary>
		/// <returns>
		/// The values.
		/// </returns>
		/// <param name='grp'>
		/// Group.
		/// </param>
		/// <param name='key'>
		/// Key.
		/// </param>
		public List<string> getValues(string grp,string key)
		{
			List<string> l = new List<string>();
			Section sec = getSection(grp);
			if(sec == null)
				return l;
			
			List<ValuePair> lvp = sec.getPairs(key);
			if(lvp == null)
				return l;
			foreach(ValuePair vp in lvp)
			{
				l.Add(vp.Value);
			}
			
			return l;
		}
		//配置文件路径.
		private string _File_Path; 
		//区块存储.
		private Dictionary<string,Section> _Sections;
		
	}
	
}
