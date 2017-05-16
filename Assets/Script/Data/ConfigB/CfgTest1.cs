using System;
using System.Collections.Generic;
using UnityEngine;

namespace Config.BinaryConfig
{
	[Serializable]
	public class CfgTest1: ConfigBinaryBase
	{
		public string B;
		public List<int> C;
		public List<int> E;
	}
}
