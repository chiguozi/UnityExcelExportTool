using System;
using System.Collections.Generic;
using UnityEngine;

namespace Config.TextConfig
{
	public class CfgTest1 : ConfigTextBase
	{
		public string B;
		public List<int> C;
		public List<int> E;


		public override void Write(int i, string value)
		{
			switch (i)
			{
				case 0:
					ID = ParseInt(value);
					break;
				case 1:
					B = ParseString(value);
					break;
				case 2:
					C = ParseListInt(value);
					break;
				case 3:
					E = ParseListInt(value);
					break;
				default:
					UnityEngine.Debug.LogError(GetType().Name + "src i:" + i);
					break;
			}
		}
	}
}
