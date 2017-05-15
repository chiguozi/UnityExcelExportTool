using System;
using System.Collections.Generic;
using UnityEngine;

namespace Config.JsonConfig
{
	[Serializable]
	public class CfgTest: ConfigJsonBase
	{
		public string B;
		public List<int> C;
		public int E;
	}
}
