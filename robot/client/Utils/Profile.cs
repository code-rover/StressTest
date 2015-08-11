using UnityEngine;
using System.Collections;

public class Profile 
{
	public static void init()
	{
		Profiler.enabled = true;
	}

	public static string getMemState()
	{
		string str = "";

		str += " mono heap momory = " + formateNum(Profiler.GetMonoHeapSize());
		str += " mono used memory = " + formateNum(Profiler.GetMonoUsedSize());
//		str += " runtime memory  = " + formateNum(Profiler.GetRuntimeMemorySize());
		str += " used heap memory = " + formateNum(Profiler.usedHeapSize);
		str += " total allocate memory = " + formateNum(Profiler.GetTotalAllocatedMemory());
		str += " total reserved memory = " + formateNum(Profiler.GetTotalReservedMemory());
		str += " total unsed reserved memory = " + formateNum(Profiler.GetTotalUnusedReservedMemory());
		return str;
	}


	private static string formateNum(uint size)
	{
		if(size < 1024)
		{
			return size.ToString();
		}
		else if(size > 1024 && (size < (1024 * 1024)))
		{
			return (size/1024).ToString() + "K";
		}
		else
		{
			return (size/(1024 * 1024)).ToString() + "M";
		}


	}

	public static void logMemState()
	{
		//UtilLog.LogError(getMemState());
	}



}
