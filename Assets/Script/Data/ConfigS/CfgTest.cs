using System;
using System.Collections.Generic;
using UnityEngine;

namespace Config.ScriptableConfig
{
	[Serializable]
	public class CfgTest: ConfigBase
	{
		public string B;
		public List<int> C;
		public int E;
	}
}
