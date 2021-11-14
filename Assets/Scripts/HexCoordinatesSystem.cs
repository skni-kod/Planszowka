using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexCoordinatesSystem
{

	public static Vector3 HexToCartesianCords(int x, int y, int z)
	{
		Vector3 position;
		position.x = (x - y) * (HexMetrics.innerRadius);
		position.y = 0f;
		position.z = z * (HexMetrics.outerRadius * 1.5f);
		return position;
	}


	public static Vector3 HexToCartesianCords(Vector3 hex_id)
    {
		return HexToCartesianCords((int)hex_id.x, (int)hex_id.y, (int)hex_id.z);
    }

	public static Vector3 CartesianToHexCords(Vector3 position)
	{
		Vector3 hex_cords = new Vector3(0, 0, 0);

		float x = position.x / (HexMetrics.innerRadius * 2f);
		float y = -x;
		float offset = position.z / (HexMetrics.outerRadius * 3f);
		x -= offset;
		y -= offset;
		int iX = Mathf.RoundToInt(x);
		int iY = Mathf.RoundToInt(y);
		int iZ = Mathf.RoundToInt(-x - y);

		if (iX + iY + iZ != 0)
		{
			float dX = Mathf.Abs(x - iX);
			float dY = Mathf.Abs(y - iY);
			float dZ = Mathf.Abs(-x - y - iZ);

			if (dX > dY && dX > dZ)
			{
				iX = -iY - iZ;
			}
			else if (dZ > dY)
			{
				iZ = -iX - iY;
			}
		}

		hex_cords.x = iX;
		hex_cords.y = -iX - iZ;
		hex_cords.z = iZ;


		return hex_cords;
	}

}
