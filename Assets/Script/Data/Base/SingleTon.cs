using System;

public class SingleTon<T> where T : new()
{
	private static T _instance = (default(T) == null)? Activator.CreateInstance<T>():default(T);
	public static T Instance { get {return _instance;}}
	
	protected SingleTon()
	{}
}