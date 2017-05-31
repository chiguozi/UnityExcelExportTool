using System;
using System.Collections.Generic;
using UnityEngine;

namespace Config.TextConfig
{
	public class CfgEffect : ConfigTextBase
	{
		public string url;
		public Vector3 posOffset;
		public Vector3 eulerOffset;
		public string bonePath;


		public override void Write(int i, string value)
		{
			switch (i)
			{
				case 0:
					ID = ParseInt(value);
					break;
				case 1:
					url = ParseString(value);
					break;
				case 2:
					posOffset = ParseVector3(value);
					break;
				case 3:
					eulerOffset = ParseVector3(value);
					break;
				case 4:
					bonePath = ParseString(value);
					break;
				default:
					UnityEngine.Debug.LogError(GetType().Name + "src i:" + i);
					break;
			}
		}
	}
}
