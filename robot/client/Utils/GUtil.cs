using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUtil {
	
	/// <summary>
	/// 获得索引位置
	/// Indexs the of.
	/// </summary>
	/// <returns>
	/// The of.
	/// </returns>
	/// <param name='val'>
	/// Value.
	/// </param>
	/// <param name='array'>
	/// Array.
	/// </param>
	public static int indexOf<T>(T val, T[] array)
	{
		int result = -1;
		if(null != array)
		{
			
			for(int index = 0; index < array.Length; index++)
			{
//				if(array[index].ToString() == val.ToString())
//					return index;
				if(object.Equals(array[index], val))
				{
					return index;
				}
			}
		}
		return result;
	}
	/// 返回一个数据的枚举
	public static Dictionary<TKey, TValue> initDictionary<TKey, TValue>(params object[] args)
	{
		Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();
		for(int index = 0; (index + 1) < args.Length; index += 2)
		{
			result.Add((TKey)args[index], (TValue)args[index + 1]);
		}
		return result;
	}
	
	public static int indexOfStr(byte val, byte[] array)
	{
		int result = -1;
		if(null != array)
		{
			for(int index = 0; index < array.Length; index++)
			{
				if(array[index] == val)
					return index;
			}
		}
		return result;
		
	}
	/// <summary>
	/// 解析字符串
	/// </param>
	public static string formatString(string val)
	{
		string result = val;
		if(null != result)
		{
			result = result.Replace("\\n", "\n");
		}
		return result;
	}
	/// 补充空格
	public static string repaceStringSpace(string val, int len)
	{
		string result = val;
		while(result.Length < len)
		{
			result += " ";
		}
		return result;
	}
	/// 定长换行
	public static string repaceStringLine(string val, int lenWidth, int minLine)
	{
		if(null == val)
			val = "";
		string[] arr = val.Split("\n".ToCharArray());
		List<string> list = new List<string>();
		///遍历数组
		for(int index = 0; index < arr.Length; index++)
		{
			///生成数组长度
			while(arr[index].ToString().Length > lenWidth)
			{
				list.Add(arr[index].ToString().Substring(0, lenWidth));
				arr[index] = arr[index].ToString().Substring(lenWidth);
			}
			arr[index] = repaceStringSpace(arr[index], lenWidth);
			list.Add(arr[index]);
		}
		while(list.Count < minLine)
		{
			list.Add(repaceStringSpace("", lenWidth));
		}
		string result = "";
		for(int index = 0; index < list.Count; index++)
		{
			result += list[index];
			if((index + 1) != list.Count)
				result += "\n";
		}
		return result;
	}
	/// 换行
	public static string repaceStringLineMid(string val, int lenWidth, int minLine)
	{
		if(null == val)
			val = "";
		
		
		string[] arr = val.Split("\n".ToCharArray());
		List<string> list = new List<string>();
		///遍历数组
		for(int index = 0; index < arr.Length; index++)
		{
			///生成数组长度
			while(arr[index].ToString().Length > lenWidth)
			{
				list.Add(arr[index].ToString().Substring(0, lenWidth));
				arr[index] = arr[index].ToString().Substring(lenWidth);
			}
			arr[index] = repaceStringSpace("", (lenWidth - arr[index].Length)/2) + arr[index];
			arr[index] = repaceStringSpace(arr[index], lenWidth);
			list.Add(arr[index]);
		}
		while(list.Count < minLine)
		{
			list.Add(repaceStringSpace("", lenWidth));
		}
		string result = "";
		for(int index = 0; index < list.Count; index++)
		{
			result += list[index];
			if((index + 1) != list.Count)
				result += "\n";
		}
		return result;
	}	
	
	/// <summary>
	/// 对字符串进行限制,限制长度,限制内容
	/// </summary>
	public static string formatString(string val, string format)
	{
		if(null == format || null == val || format.Length <= 0 || val.Length == format.Length)
			return val;
		
		if(val.Length > format.Length)
			return val.Substring(val.Length - format.Length);
		
		return format.Substring(0, format.Length - val.Length) + val;
	}
	/// 保留n为消暑的值返回
	public static string numToString(float val, int dot)
	{
		string[] sub = val.ToString().Split(".".ToCharArray());
		if(float.IsNaN(val))val = 1f;
		///为空的情况下
		if(sub.Length == 0 && dot <= 0)
			return "0";
		if(sub.Length == 0)
			return "0" + "." + cloneString("0", dot);
		///返回整数
		if(dot <= 0)
			return sub[0];
		///加上0的消暑
		if(sub.Length == 1)
			return sub[0] + "." + cloneString("0", dot);
		/// 截取返回
		if(sub[1].Length >= dot)
			return sub[0] + "." + sub[1].Substring(0, dot);
		/// 返回数据
		return sub[0] + "." + sub[1] + cloneString("0", dot - sub[1].Length);
	}
	public static string cloneString(string val, int len)
	{
		string result = "";
		for(int i = 0; i < len; i++)
		{
			result += val;
		}
		return result;
	}
	
	/// <summary>
	/// 创建2维数组<测试通过>
	/// </returns>
	public static string[][] stringToArray(string val, string index1, string index2)
	{
		string[][] result = null;
		if(null != val)
		{
			string [] arr_1 = val.Split(index1.ToCharArray());
			int index;
			result = new string[arr_1.Length][];
			for(index = 0; index < arr_1.Length; index++)
			{
				result[index] = arr_1[index].Split(index2.ToCharArray());
			}
			
		}
		
		return result;
	}
	
	/// <summary>
	/// 生成一个数组
	/// </typeparam>
	public static T[] NewArray<T>(int len)
	{
		T[] result = new T[len];
		for(int index = 0; index < result.Length; index++)
		{
			//result[index] = GameObject.Instantiate(typeof(T));
		}
		return result;
	}
	
	/// <summary>
	/// 颜色转换
	/// </param>
	public static Color toColor(string color)
	{
		if(null == color)
			return Color.white;
		if("0x" != color.Substring(0, 2))
			color += "0x";
		return toColor(uint.Parse(color));
	}
	public static Color toColor(uint color)
	{
		Color result = new Color();
		result.r = (color >> 24) & 0xff ;
		result.g = (color >> 16) & 0xff ;
		result.b = (color >> 8) & 0xff ;
		result.a = (color) & 0xff ;
		return result;
	}
	
	
	/// <summary>
	/// 返回列
	/// </param>
	public static int maxRow(string text)
	{
		if(null == text)
			return 0;
		int result = 0;
		string[] texts = text.Split("\n".ToCharArray());
		foreach(string val in texts)
		{
			result = Mathf.Max(val.Length, result);
		}
		return result;
	}
	/// <summary>
	/// 返回行
	/// </param>
	public static int maxCol(string text)
	{
		if(null == text)
			return 0;
		return text.Split("\n".ToCharArray()).Length;
	}
	/// 正负查看
	public static int sign(float val)
	{
		if(val >= 0)
			return 1;
		else return -1;
	}
	
	/// <summary>
	/// Atos the B time.
	/// a到b的重力加速度时间
	/// </param>
	public static float AtoBTime(float s_start, float s_end, float s_speed, float s_gravity)
	{
		float result = 0f;
		float t_start = s_start;
		float t_end = s_end;
		float t_speed = s_speed;
		float t_gravity = s_gravity;
		float t_time = 0.03f;
		while(true)
		{
			///模拟出来的数据异步
			if(sign(t_speed) == sign(s_gravity))
//				if((s_start > s_end) != (t_start > t_end))
				if(Mathf.Abs(Mathf.Abs(t_start) - Mathf.Abs(t_end)) < Mathf.Abs(t_speed * t_time))
				{
					result += t_time;	
					break;
				}
//			if((s_start > s_end) != (t_start + t_speed > t_end))
//			{
//				result += Mathf.Abs(t_start - t_end) / t_speed;
//				break;
//			}
			t_start += t_speed * t_time;
			t_speed += t_gravity * t_time;
			result += t_time;
//			result += 1f;
//			t_start += t_speed;
//			t_speed += s_gravity;
//			
			if(result >= 100f)
				return 0;
		}
		return result;
	}
	
	/// <summary>
	/// Gets the local post.
	/// 获得鼠标相对位置
	/// </param>
	public static Vector2 getUILocalPost(Transform transform, Vector2 post)
	{
		Vector2 result = new Vector2();
		///如果不为空
		if(null != transform)
		{
			
			result.x =  post.x;
			result.y = -post.y;
			result.x -= transform.localPosition.x;
			result.y -= transform.localPosition.y;
		}
		return result;
	}
	/// 获得鼠标的窗口未回
	public static Vector3 getMousePost(float sz)
	{
		Vector3 result = new Vector3();
		result.x = Input.mousePosition.x;
		result.y = Input.mousePosition.y - Screen.height;
		result.z = sz;
		return result;
	}
	public static Vector3 getScreenPost(float mulX, float mulY, float sz)
	{
		Vector3 result = new Vector3();
		result.x = Screen.width * mulX;
		result.y = -Screen.height * (mulY);
		result.z = sz;
		return result;
	}
	/// 获得矩形
	public static BoundsForSelf getBounds(Transform obj)
	{
		Transform[] childs = obj.GetComponentsInChildren<Transform>();
		BoundsForSelf result = new BoundsForSelf();
		result.center = obj.position;
		result.max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
		result.min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
		///遍历子项
		foreach(Transform child in childs)
		{
			/// 自我不算子项
			if(child == obj)
				continue;
			/// changed by ssy 改为迭代，提高效率
			if(child.gameObject.GetComponent<Projector>() != null ||
			   child.gameObject.GetComponent<ParticleSystem>() != null ||
			   child.gameObject.GetComponent<TrailRenderer>() != null)
			{
				continue;
			}
//			if(child != obj)
//			{
//			result = getBounds(child, ref result);
				result.max.x = Mathf.Max(result.max.x, child.position.x);
				result.max.y = Mathf.Max(result.max.y, child.position.y);
				result.max.z = Mathf.Max(result.max.z, child.position.z);
				
				result.min.x = Mathf.Min(result.min.x, child.position.x);
				result.min.y = Mathf.Min(result.min.y, child.position.y);
				result.min.z = Mathf.Min(result.min.z, child.position.z);
//			}
			/// changed end
		}
        
//        // 遍历骨骼
//        List<Transform> trs = new List<Transform> ();
//        SkinnedMeshRenderer[] renders = obj.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
//        foreach(SkinnedMeshRenderer render in renders)
//        {
//            if(render != null)
//            {
//                trs.AddRange(render.bones);
//            }
//        }
//        
//         ///遍历子项
//         foreach(Transform child in trs)
//         {
//             /// 自我不算子项
//             if(child == obj)
//                 continue;
//
//                 result.max.x = Mathf.Max(result.max.x, child.position.x);
//                 result.max.y = Mathf.Max(result.max.y, child.position.y);
//                 result.max.z = Mathf.Max(result.max.z, child.position.z);
//                 
//                 result.min.x = Mathf.Min(result.min.x, child.position.x);
//                 result.min.y = Mathf.Min(result.min.y, child.position.y);
//                 result.min.z = Mathf.Min(result.min.z, child.position.z);
//
//         }
        
        
        
		if(result.max.x == float.MinValue)
			result.max.x = result.center.x;
		if(result.max.y == float.MinValue)
			result.max.y = result.center.y;
		if(result.max.z == float.MinValue)
			result.max.z = result.center.z;
		
		if(result.min.x == float.MaxValue)
			result.min.x = result.center.x;
		if(result.min.y == float.MaxValue)
			result.min.y = result.center.y;
		if(result.min.z == float.MaxValue)
			result.min.z = result.center.z;
		
		return result;
	}
	private static BoundsForSelf getBounds(Transform obj, ref BoundsForSelf result)
	{
		Transform[] childs = obj.GetComponentsInChildren<Transform>();
		///遍历子项
		foreach(Transform child in childs)
		{
			if(child != obj)
			{
				result.max.x = Mathf.Max(result.max.x, child.position.x);
				result.max.y = Mathf.Max(result.max.y, child.position.y);
				result.max.z = Mathf.Max(result.max.z, child.position.z);
				
				result.min.x = Mathf.Min(result.min.x, child.position.x);
				result.min.y = Mathf.Min(result.min.y, child.position.y);
				result.min.z = Mathf.Min(result.min.z, child.position.z);
				
				result = getBounds(child, ref result);
			}
		}
		return result;
	}
	/// 强转成数据类型
	public static int formatToInt(string val)
	{
		return 0;
	}
	/// 距离现在已经是多少天了
    //public static float days(double timesTamp)
    //{
    //    return (float)((timesTamp - Config.serverTimesTamp) / (24f * 60f * 60f));
    //}
	
	/// 校正角色在ui中应该存在的位置
	public static void setUIGameObjectSize(GameObject gameObject, float scale)
	{	
		gameObject.transform.localScale = new Vector3(scale, scale, scale);
		
		BoundsForSelf bounds = GUtil.getBounds(gameObject.transform);
		
//		float uiScale = 1f / (Screen.height / 2f);
		float uiScale = 0.003125f;
		
//		scale = scale / ((bounds.max.y - bounds.min.y) / 0.003125f / scale);
		scale = scale / ((bounds.max.y - bounds.min.y) / uiScale / scale);
		
		gameObject.transform.localScale = new Vector3(scale, scale, scale);
		
//		gameObject.transform.localPosition = gameObject.transform.localPosition + new Vector3(0f, Mathf.Min(bounds.center.y - bounds.min.y, 0f) / 0.003125f * (scale / 100f));
		gameObject.transform.localPosition = gameObject.transform.localPosition + new Vector3(0f, Mathf.Min(bounds.center.y - bounds.min.y, 0f) / uiScale * (scale / 100f));
	}
	
	/// 数字矫正
	public static string formatInt(int val)
	{
		if(val > 100000000)
			return (val / 100000000) + "亿";
		if(val > 1000000)
			return (val / 10000) + "万";
		return val.ToString();
	}
	
	
	
	
	
	
	
	
	
	///public Vector3[] Execute(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 point[i + 3], int divisions)
	public static Vector3 GetBspline(List<Vector3> point, float t)
	{
		if(point.Count <= 3)
			return getBezier3(point, t);
		
		Vector3 result = Vector3.zero;
		int divisions;
		
		// 计算4x4矩阵系数
		double[] a = new double[4];
		double[] b = new double[4];
		double[] c = new double[4];
		
		int i = Mathf.FloorToInt(point.Count * t);
		i = Mathf.Min(i, point.Count - 4);
		
		
//		for(int i = 0; i < point.Count - 3; i++)
//		{
		
//			a[0] = (-point[i + 0].x + 3 * point[i + 1].x - 3 * point[i + 2].x + point[i + 3].x);
//			a[1] = (3 * point[i + 0].x - 6 * point[i + 1].x + 3 * point[i + 2].x);
//			a[2] = (-3 * point[i + 0].x + 3 * point[i + 2].x);
//			a[3] = (point[i + 0].x + 4 * point[i + 1].x + point[i + 2].x);
//			
//			b[0] = (-point[i + 0].y + 3 * point[i + 1].y - 3 * point[i + 2].y + point[i + 3].y);
//			b[1] = (3 * point[i + 0].y - 6 * point[i + 1].y + 3 * point[i + 2].y);
//			b[2] = (-3 * point[i + 0].y + 3 * point[i + 2].y);
//			b[3] = (point[i + 0].y + 4 * point[i + 1].y + point[i + 2].y);
//			
//			c[0] = (-point[i + 0].z + 3 * point[i + 1].z - 3 * point[i + 2].z + point[i + 3].z);
//			c[1] = (3 * point[i + 0].z - 6 * point[i + 1].z + 3 * point[i + 2].z);
//			c[2] = (-3 * point[i + 0].z + 3 * point[i + 2].z);
//			c[3] = (point[i + 0].z + 4 * point[i + 1].z + point[i + 2].z);
			
			
			a[0] = (-point[i + 0].x + 3 * point[i + 1].x - 3 * point[i + 2].x + point[i + 3].x) / 6.0f;
			a[1] = (3 * point[i + 0].x - 6 * point[i + 1].x + 3 * point[i + 2].x) / 6.0f;
			a[2] = (-3 * point[i + 0].x + 3 * point[i + 2].x) / 6.0f;
			a[3] = (point[i + 0].x + 4 * point[i + 1].x + point[i + 2].x) / 6.0;
			
			b[0] = (-point[i + 0].y + 3 * point[i + 1].y - 3 * point[i + 2].y + point[i + 3].y) / 6.0f;
			b[1] = (3 * point[i + 0].y - 6 * point[i + 1].y + 3 * point[i + 2].y) / 6.0f;
			b[2] = (-3 * point[i + 0].y + 3 * point[i + 2].y) / 6.0f;
			b[3] = (point[i + 0].y + 4 * point[i + 1].y + point[i + 2].y) / 6.0f;
			
			c[0] = (-point[i + 0].z + 3 * point[i + 1].z - 3 * point[i + 2].z + point[i + 3].z) / 6.0f;
			c[1] = (3 * point[i + 0].z - 6 * point[i + 1].z + 3 * point[i + 2].z) / 6.0f;
			c[2] = (-3 * point[i + 0].z + 3 * point[i + 2].z) / 6.0f;
			c[3] = (point[i + 0].z + 4 * point[i + 1].z + point[i + 2].z) / 6.0f;
//		}
		
		
		
//		Vector3[] splinexy = new Vector3[divisions * 4];
		
//		if(t == 0f)
//		{
//			result.x = (float)a[3];
//			result.y = (float)b[3];
//			result.z = (float)c[3];
//		} else {
			// 计算
			result.x = (float)((a[2] + t * (a[1] + t * a[0])) * t + a[3]);
			result.y = (float)((b[2] + t * (b[1] + t * b[0])) * t + b[3]);
			result.z = (float)((c[2] + t * (c[1] + t * c[0])) * t + c[3]);
//		}
		
		return result;
//		double alpha = 0.0;
//		double delta = 1.0 / System.Convert.ToDouble(divisions);
//		for (int i = 1; i <= divisions - 1; i++)
//		{
//			splinexy[i].x = (a[2] + alpha * (a[1] + alpha * a[0])) * alpha + a[3];
//			splinexy[i].y = (b[2] + alpha * (b[1] + alpha * b[0])) * alpha + b[3];
//			alpha += delta;
//		}
//		return splinexy;
	}
	
//	public static Vector3 getB3Curves(List<Vector3> arrayCoordinate, float t)//绘制三次B样条曲线
//	{
//		int rate = 10;//rate是平滑程度
//		double F03;
//		double F13;
//		double F23;
//		double F33;
//		Vector3 result = new Vector3();
//		result.x = (arrayCoordinate[0].x + 4.0f * arrayCoordinate[1].x + arrayCoordinate[2].x)/6.0f;
//		result.y = (arrayCoordinate[0].x + 4.0f * arrayCoordinate[1].x + arrayCoordinate[2].x)/6.0f;
//		result.y = (xq[0][1] + 4.0f * xq[1][1] + xq[2][1])/6.0f;
//		pDC->MoveTo(lx,ly);
//		CPen MyPen2(PS_SOLID,3,RGB(255,0,0));//红笔画B样条
//		CPen *POldPen2=pDC->SelectObject(&MyPen2);
//		//6段样条曲线
//		for(int i=1; i < 7; i++)
//		{
//			for(double t=0;t<=1;t+=1.0/rate)
//			{
//				F03=(-t*t*t+3*t*t-3*t+1)/6;//计算F0,3(t)
//				F13=(3*t*t*t-6*t*t+4)/6;//计算F1,3(t)
//				F23=(-3*t*t*t+3*t*t+3*t+1)/6;//计算F2,3(t)
//				F33=t*t*t/6;//计算B3,3(t)
//				lx=ROUND(xq[i-1][0]*F03+xq[i][0]*F13+xq[i+1][0]*F23+xq[i+2][0]*F33);
//				ly=ROUND(xq[i-1][1]*F03+xq[i][1]*F13+xq[i+1][1]*F23+xq[i+2][1]*F33);
//				pDC->LineTo(lx,ly);
//			}
//		}
//
//	}
	
	
	
	
	
	/**
	 *获得贝塞尔曲线(De Casteljau's algorithm) 
	 * @param arrayCoordinate
	 * @param t
	 * @return 
	 * 
	 */
	static public Vector3 getBezier3(List<Vector3> arrayCoordinate, float t)      
	{        
		int n = arrayCoordinate.Count;  
		if (n < 2) return arrayCoordinate[0];
		
		float[] xarray = new float[arrayCoordinate.Count];        
		float[] yarray = new float[arrayCoordinate.Count];
		float[] zarray = new float[arrayCoordinate.Count];
		
		
		Vector3 result = new Vector3(arrayCoordinate[0].x, arrayCoordinate[0].y, arrayCoordinate[0].z);
	   
		int i;
		int j;
		//调整参数t,计算贝塞尔曲线上的点的坐标,t即为上述u        
		{
			for(i = 1; i < n; ++i)        
			{
				for(j = 0; j < n - i; ++j)        
				{
					if(i == 1) // i==1时,第一次迭代,由已知控制点计算       
					{
						xarray[j] = arrayCoordinate[j].x * (1 - t) + arrayCoordinate[j + 1].x * t;        
						yarray[j] = arrayCoordinate[j].y * (1 - t) + arrayCoordinate[j + 1].y * t;
						zarray[j] = arrayCoordinate[j].z * (1 - t) + arrayCoordinate[j + 1].z * t;
						continue;        
					}
					// i != 1时,通过上一次迭代的结果计算        
					xarray[j] = xarray[j] * (1 - t) + xarray[j + 1] * t;        
					yarray[j] = yarray[j] * (1 - t) + yarray[j + 1] * t;
					zarray[j] = zarray[j] * (1 - t) + zarray[j + 1] * t;
				}        
			}       
			result.x = xarray[0];        
			result.y = yarray[0];
			result.z = zarray[0];
		}
		return result;
	}
	//获得被赛尔曲线点
	static public Vector3 getBezier(List<Vector3> controlPoint, float t)
	{
		Vector3 result = new Vector3(controlPoint[0].x, controlPoint[0].y, controlPoint[0].z);
		List<float> xx = new List<float>();
		List<float> yy = new List<float>();
		List<float> zz = new List<float>();
		int index;
		float tmpX = 0;
		float tmpY = 0;
		float tmpZ = 0;
		
		float value = 0;
		//参数
		for (index = 0; index < controlPoint.Count; index++)
		{
			//
			if (index + 2 < controlPoint.Count)
			{
				value = 3 * (controlPoint[index + 1].x - controlPoint[index].x) - tmpX;
				tmpX += value;
			} else {
				value = controlPoint[controlPoint.Count - 1].x - controlPoint[0].x - tmpX;
			}
			xx.Add(value);
//			xx.Insert(0, value);
			
			//
			if (index + 2 < controlPoint.Count)
			{
				value = 3 * (controlPoint[index + 1].y - controlPoint[index].y) - tmpY;
				tmpY += value;
			} else {
				value = controlPoint[controlPoint.Count - 1].y - controlPoint[0].y - tmpY;
			}
			yy.Add(value);
//			yy.Insert(0, value);
			
			//
			if (index + 2 < controlPoint.Count)
			{
				value = 3 * (controlPoint[index + 1].z - controlPoint[index].z) - tmpZ;
				tmpZ += value;
			} else {
				value = controlPoint[controlPoint.Count - 1].z - controlPoint[0].z - tmpZ;
			}
			zz.Add(value);
//			zz.Insert(0, value);
			
			
			
			//
			if (index + 2 >= controlPoint.Count)
			{
				break;
			}
		}
		/////////////////////////////////////////////////////////////////////
		while(xx.Count > 0)
		{
			result.x += Mathf.Pow(t, xx.Count) * xx[0];
			xx.RemoveAt(0);
		}
		while (yy.Count > 0)
		{
			result.y += Mathf.Pow(t, yy.Count) * yy[0];
			yy.RemoveAt(0);
		}
		while (zz.Count > 0)
		{
			result.z += Mathf.Pow(t, zz.Count) * zz[0];
			zz.RemoveAt(0);
		}
		/*
		cx = 3 * (x1 - x0);
		bx = 3 * (x2 - x1) - cx;
		ax = x3 - x0 - cx - bx
		//
		cy = 3 * (y1 - y0);
		by = 3 * (y2 - y1) - cy;
		ay = y3 - y0 - cy - by;
		//
		x(t) = ax * t^3 + bx * t^2 + cx * t + x0;
		x(t) = ay * t^3 + by * t^2 + cy * t + y0;*/
		return result;
	}
    
    
    /// <summary>
    /// 移除字符串中的（clone）字段
    /// </summary>
    /// <returns>
    /// The clone.
    /// </returns>
    /// <param name='str'>
    /// String.
    /// </param>
    public static  string formateStrRemoveClone(string str)
    {
        string ret = str;
        
        if(!ret.Contains("(Clone)"))
        {
            return ret;
        }
        

         while(ret.Contains("(Clone)"))
         {
             ret = ret.Substring(0,  ret.LastIndexOf("(Clone)"));
         }
        
        return ret;
    }
	
	
	/// 格式化时间
	public static string formatTime(double time)
	{
		string result = "";
		//我的时间
		time += System.DateTimeOffset.Now.Offset.TotalSeconds;
		//时
		result += GUtil.formatString(((int)(time / 3600) % 24).ToString(), "00") + " : ";
		//分
		result += GUtil.formatString(((int)(time % 3600) / 60).ToString(), "00") + " : ";
//		result += GUtil.formatString((t / 60).ToString(), "00") + " : ";
		//秒
		result += GUtil.formatString(((int)(time % 60)).ToString(), "00") + ".";
		//毫秒
		result += GUtil.formatString(((int)((time % 1f) * 100)).ToString(), "00");
		
		//返回
		return result;
	}
	
	
	
	
	
	
	
	
	
	
//	void getBezier2(List<Vector3> originPoint, float t)
//	{  
//	    //控制点收缩系数 ，经调试0.6较好，CvPoint是opencv的，可自行定义结构体(x,y)  
//	    float scale = 0.6;  
//		int originCount = originPoint.Count;
//	    Vector3[] midpoints = new Vector3[originCount];  
//	    //生成中点       
//	    for(int i = 0 ; i < originCount ; i++)
//		{      
//	        int nexti = (i + 1) % originCount;
//			midpoints[i] = new Vector3(0f,0f,0f);
//	        midpoints[i].x = (originpoint[i + 0].x + originPoint[nexti].x)/2.0f;  
//	        midpoints[i].y = (originpoint[i + 0].y + originPoint[nexti].y)/2.0f;  
//			midpoints[i].z = (originpoint[i + 0].z + originPoint[nexti].z)/2.0f;  
//	    }      
//	      
//	    //平移中点  
//	    Vector3[] extrapoints = new Vector3[2 * originCount];   
//	    for(int i = 0 ;i < originCount ; i++){  
//	         int nexti = (i + 1) % originCount;  
//	         int backi = (i + originCount - 1) % originCount;  
//	         Vector3 midinmid = Vector3.zero;  
//	         midinmid.x = (midpoints[i].x + midpoints[backi].x)/2.0;  
//	         midinmid.y = (midpoints[i].y + midpoints[backi].y)/2.0;  
//	         int offsetx = originpoint[i + 0].x - midinmid.x;  
//	         int offsety = originpoint[i + 0].y - midinmid.y;  
//	         int extraindex = 2 * i;  
//			 extrapoints[extraindex] = Vector3.zero;
//	         extrapoints[extraindex].x = midpoints[backi].x + offsetx;  
//	         extrapoints[extraindex].y = midpoints[backi].y + offsety;  
//	         //朝 originpoint[i + 0]方向收缩   
//	         int addx = (extrapoints[extraindex].x - originpoint[i + 0].x) * scale;  
//	         int addy = (extrapoints[extraindex].y - originpoint[i + 0].y) * scale;  
//	         extrapoints[extraindex].x = originpoint[i + 0].x + addx;  
//	         extrapoints[extraindex].y = originpoint[i + 0].y + addy;  
//	           
//	         int extranexti = (extraindex + 1)%(2 * originCount);  
//			 extrapoints[extranexti] = Vector3.zero;
//	         extrapoints[extranexti].x = midpoints[i].x + offsetx;  
//	         extrapoints[extranexti].y = midpoints[i].y + offsety;  
//	         //朝 originpoint[i + 0]方向收缩   
//	         addx = (extrapoints[extranexti].x - originpoint[i + 0].x) * scale;  
//	         addy = (extrapoints[extranexti].y - originpoint[i + 0].y) * scale;  
//	         extrapoints[extranexti].x = originpoint[i + 0].x + addx;  
//	         extrapoints[extranexti].y = originpoint[i + 0].y + addy;  
//	           
//	    }      
//	      
//	    Vector3[] controlPoint = new Vector3[4];
//	    //生成4控制点，产生贝塞尔曲线  
//	    for(int i = 0 ;i < originCount ; i++)
//		{
//	           controlPoint[0] = originpoint[i + 0];  
//	           int extraindex = 2 * i;  
//	           controlPoint[1] = extrapoints[extraindex + 1];  
//	           int extranexti = (extraindex + 2) % (2 * originCount);  
//	           controlPoint[2] = extrapoints[extranexti];  
//	           int nexti = (i + 1) % originCount;  
//	           controlPoint[3] = originPoint[nexti];      
//	           float u = 1;  
//	           while(u >= 0){  
//	               int px = bezier3funcX(u,controlPoint);  
//	               int py = bezier3funcY(u,controlPoint);  
//	               //u的步长决定曲线的疏密  
//	               u -= 0.005f;  
//	               Vector3 tempP = Vector3(px,py);  
//	               //存入曲线点   
//	               curvePoint.push_back(tempP);  
//	           }      
//	    }  
//	}  
//  
//	//三次贝塞尔曲线  
//	float bezier3funcX(float uu,CvPoint *controlP){  
//	   float part0 = controlP[0].x * uu * uu * uu;  
//	   float part1 = 3 * controlP[1].x * uu * uu * (1 - uu);  
//	   float part2 = 3 * controlP[2].x * uu * (1 - uu) * (1 - uu);  
//	   float part3 = controlP[3].x * (1 - uu) * (1 - uu) * (1 - uu);     
//	   return part0 + part1 + part2 + part3;   
//	}      
//	  
//	float bezier3funcY(float uu,CvPoint *controlP){  
//	   float part0 = controlP[0].y * uu * uu * uu;  
//	   float part1 = 3 * controlP[1].y * uu * uu * (1 - uu);  
//	   float part2 = 3 * controlP[2].y * uu * (1 - uu) * (1 - uu);  
//	   float part3 = controlP[3].y * (1 - uu) * (1 - uu) * (1 - uu);     
//	   return part0 + part1 + part2 + part3;   
//	}


	// add by ssy
	/// <summary>
	/// ios not support good addRange 
	/// use myself
	/// </summary>
	/// <param name="dst">Dst.</param>
	/// <param name="src">Source.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static void addRange<T>(List<T> dst, T[] src)
	{
		foreach(T t in src)
		{
			dst.Add(t);
		}
	}
	// add end
	
	
	
	
	
	
	
	
}
/// 自己的矩形类
public class BoundsForSelf
{
	public Vector3 max = Vector3.zero;
	public Vector3 min = Vector3.zero;
	public Vector3 center = Vector3.zero;
}
