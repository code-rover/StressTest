using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using utils;

/// 我的数据表格存放地址
public class ManagerCsv
{	
	
	public static bool isCsvFile = true;
	/// 获得csv全部文件加载
	protected static UtilCsvReader _info;
	/// csv文件列表信息
	protected static Dictionary<string, UtilCsvReader> _csvTables = new Dictionary<string, UtilCsvReader>();
	/// 加载锁定
	protected static bool _isLoad = false;
	/// 数据缓存
	protected static Dictionary<string, object> _csvItemMemory = new Dictionary<string, object>();
	/// 完成回调
	private static Function _complete;

    private delegate void Function();
	
	/// 读取csv文件路径
	public static void load(Function complete)
	{
		if(_isLoad)
		{
			Logger.Error("只能调一次");
			return;
		}
		_isLoad = true;
		_complete = complete;
		//LoadMngr.getInstance().load(ConfigUrl.URL_INFO, completeInfo);
	}

	/// 完成的调用
	private static void completeInfo(double loader_id)
	{
		_info = new UtilCsvReader(LoadMngr.getInstance().GetText(ConfigUrl.URL_INFO));
		_info.isClearData = false;
		/// 地址表
		List<Dictionary<string, string>> urlTable = null;
		/// 用来读取 csv源文件还是assetbound的判断
		#region csv selcet
		if(isCsvFile)
			urlTable = _info.searchs("type", "csv");
		else
			urlTable = _info.searchs("type", "csvAsset");
		#endregion
		/// 地址列表
		List<string> urls = new List<string>();
		/// 遍历地址
		foreach(Dictionary<string, string> item in urlTable)
		{
			urls.Add(ConfigUrl.ROOT + item["value"]);

		}
		//add by yxh
		urls.Add(ConfigUrl.UI_ILLEGALWORD);
		/// 加载
		LoadMngr.getInstance().load(urls.ToArray(), completeCsv);
	}
	/// csv 文件调用
	private static void completeCsv(double loader_id)
	{
		/// 地址表
		List<Dictionary<string, string>> urlTable = _info.searchs("type", "csv");
		
		List<Dictionary<string, string>> urlTableAsset = _info.searchs("type", "csvAsset");
		CsvAsset csvAsset = null;
		if(!isCsvFile)
		{
#if UNITY_EDITOR && UNITY_WEBPLAYER
			csvAsset = LoadMngr.getInstance().getObjectGame(ConfigUrl.ROOT + urlTableAsset[0]["value"].Replace("csv/", "csvUnityEditor/")).GetComponent<CsvAsset>();
#else
			csvAsset = LoadMngr.getInstance().getObjectGame(ConfigUrl.ROOT + urlTableAsset[0]["value"]).GetComponent<CsvAsset>();
#endif
		}
		/// 遍历地址
		foreach(Dictionary<string, string> item in urlTable)
		{
			try
			{
				
				UtilCsvReader csvReader = null;
				if(isCsvFile)
				{

					csvReader = new UtilCsvReader(LoadMngr.getInstance().GetText(ConfigUrl.ROOT + item["value"]));

				} else {
					csvReader = new UtilCsvReader(csvAsset.getText(item["value"].Replace("eff/", "").Replace("audio/bind/", "").Replace("eff2/", "")));
				}
				
				
				string key = item["value"].Substring(item["value"].LastIndexOf("/") + 1);
				key = key.Replace(".csv", "");
				
				_csvTables.Add(key, csvReader);
			} catch(Exception e) {Logger.Error(item["value"] + " 表格错误\n" + e.ToString());}
		}
		/// 完成回调
		if(null != _complete)
			_complete();
	}
	
	public static void loadCsv(string csvName,string csvUrl)
	{
		try
		{
			UtilCsvReader csvReader = new UtilCsvReader(LoadMngr.getInstance().GetText(csvUrl));
			_csvTables.Add(csvName, csvReader);
		} 
		catch(Exception e) 
		{
			Logger.Error(e.Message);
			Logger.Error(csvName + ".csv 表格错误");
		}
	}
	
	/// ###############################################################################################################
	/// 
	/// 加载关卡csv表格
	/// 
	
	/// 地址信息
	private static string _urlCsvSence;
	/// 下载索引
	private static int _loadCsvSenceIndex;


	/// 获得配置文件信息
	public static UtilCsvReader getCsvInfo()
	{
		return _info;
	}

	
	/// <summary>
	///  get any csv you want
	/// </summary>
	/// <returns>The csv reader.</returns>
	/// <param name="csv_name">Csv_name.</param>
	public static UtilCsvReader getCsvReader(string csv_name)
	{
		if(!_csvTables.ContainsKey(csv_name))
		{
			Logger.Error(" the csv table you want have not been loaded!!");
			return null;
		}

		return _csvTables[csv_name];
	}
	

}
