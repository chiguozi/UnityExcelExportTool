public class ConfigFactory
{
	public static ConfigBase Get(string configName)
	{
		switch(configName)
		{
			case "Test1":
				return new CfgTest1();
			default:
				UnityEngine.Debug.LogError(configName + "not found");
				return null;
		}
	}
}
