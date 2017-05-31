using System;
using System.Collections.Generic;
using UnityEngine;

namespace Config.TextConfig
{
	public class CfgSubSkill : ConfigTextBase
	{
		public List<List<string>> skillActionList;


		public override void Write(int i, string value)
		{
			switch (i)
			{
				case 0:
					ID = ParseInt(value);
					break;
				case 1:
					skillActionList = ParseListListString(value);
					break;
				default:
					UnityEngine.Debug.LogError(GetType().Name + "src i:" + i);
					break;
			}
		}
	}
}
