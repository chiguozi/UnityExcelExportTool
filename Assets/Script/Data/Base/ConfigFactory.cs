namespace Config.TextConfig
{
	public class ConfigFactory
	{
		public static ConfigTextBase Get(string configName)
		{
			switch(configName)
			{
				case "Test":
					return new CfgTest();
			}
			return null;
		}
	}
}
