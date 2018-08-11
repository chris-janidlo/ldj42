using UnityEngine;

public static class ExtensionMethods
{
	// confounded unity devs not implementing division!
	public static Vector2Int DivideBy (this Vector2Int dividend, int divisor)
	{
		return new Vector2Int(dividend.x / divisor, dividend.y / divisor);
	}
}
