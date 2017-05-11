namespace Config.TextConfig
{
	public class ConfigFactory
	{
		public static ConfigBase Get(string configName)
		{
			switch(configName)
			{
				case "Test":
					return new CfgTest();
				case "Test1":
					return new CfgTest1();
				default:
					UnityEngine.Debug.LogError(configName + "not found");
					return null;
			}
		}
	}
}
