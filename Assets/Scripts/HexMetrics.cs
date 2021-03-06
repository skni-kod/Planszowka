using UnityEngine;


public static class HexMetrics
{

	public const float outerRadius = 10f;

	public const float innerRadius = outerRadius * 0.866025404f;

	public const float depth = -3.0f;

	public static Vector3[] corners = {
		new Vector3(0f, 0f, 0f),
		new Vector3(0f, 0f, outerRadius),
		new Vector3(innerRadius, 0f, 0.5f * outerRadius),
		new Vector3(innerRadius, 0f, -0.5f * outerRadius),
		new Vector3(0f, 0f, -outerRadius),
		new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
		new Vector3(-innerRadius, 0f, 0.5f * outerRadius)
	};

	public static Vector3[] inside_places =
	{
		new Vector3(0f, 0f, 8.2f),
		new Vector3(7.1f, 0f, 0.5f * 8.2f),
		new Vector3(7.1f, 0f, -0.5f * 8.2f),
		new Vector3(0f, 0f, -8.2f),
		new Vector3(-7.1f, 0f, -0.5f * 8.2f),
		new Vector3(-7.1f, 0f, 0.5f * 8.2f),
		new Vector3(0f, 0f, 5.8f),
		new Vector3(5.02291f, 0f, 0.5f * 5.8f),
		new Vector3(5.02291f, 0f, -0.5f * 5.8f),
		new Vector3(0f, 0f, -5.8f),
		new Vector3(-5.02291f, 0f, -0.5f * 5.8f),
		new Vector3(-5.02291f, 0f, 0.5f * 5.8f),
		new Vector3(0f, 0f, 3),
		new Vector3(2.6f, 0f, 0.5f * 3),
		new Vector3(2.6f, 0f, -0.5f * 3),
		new Vector3(0f, 0f, -3),
		new Vector3(-2.6f, 0f, -0.5f * 3),
		new Vector3(-2.6f, 0f, 0.5f * 3),
		new Vector3(7.36f, 0f, 0f),
		new Vector3(-7.36f, 0f, 0f),
		new Vector3(-7.36f * 0.5f, 0f, 0.75f * 8.5f),
		new Vector3(7.36f * 0.5f, 0f, 0.75f * 8.5f),
		new Vector3(-7.36f * 0.5f, 0f, 0.75f * -8.5f),
		new Vector3(7.36f * 0.5f, 0f, 0.75f * -8.5f),
		new Vector3(5.2f, 0f, 0f),
		new Vector3(-5.2f, 0f, 0f),
		new Vector3(-5.2f * 0.5f, 0f, 0.75f * 6f),
		new Vector3(5.2f * 0.5f, 0f, 0.75f * 6f),
		new Vector3(-5.2f * 0.5f, 0f, 0.75f * -6f),
		new Vector3(5.2f * 0.5f, 0f, 0.75f * -6f),
	};

	public static Vector3[] ReturnHexCorners(float radius)
    {
		float inn_radius = radius * 0.866025404f;
		Vector3[] cornerss = {
							new Vector3(0f, 0f, radius),
							new Vector3(inn_radius, 0f, 0.5f * radius),
							new Vector3(inn_radius, 0f, -0.5f * radius),
							new Vector3(0f, 0f, -radius),
							new Vector3(-inn_radius, 0f, -0.5f * radius),
							new Vector3(-inn_radius, 0f, 0.5f * radius)
							};
		return cornerss;
	}

	public static Vector3[] ReturnHexSideMidPoints(float radius)
	{
		float inn_radius = radius * 0.866025404f;
		Vector3[] cornerss = {
							new Vector3(inn_radius, 0f, 0f),
							new Vector3(-inn_radius, 0f, 0f),
							new Vector3(-inn_radius * 0.5f, 0f, 0.75f * radius),
							new Vector3(inn_radius * 0.5f, 0f, 0.75f * radius),
							new Vector3(-inn_radius * 0.5f, 0f, 0.75f * -radius),
							new Vector3(inn_radius * 0.5f, 0f, 0.75f * -radius),
							};
		return cornerss;
	}

	public static int[] triangles = {
        1, 2, 0,
        2, 3, 0,
		3, 4, 0,
        4, 5, 0,
        5, 6, 0,
		6, 1, 0
	};

	public static Vector3[] bottom_corners = {
		new Vector3(0f, depth, outerRadius),
		new Vector3(innerRadius, depth, 0.5f * outerRadius),
		new Vector3(innerRadius, depth, -0.5f * outerRadius),
		new Vector3(0f, depth, -outerRadius),
		new Vector3(-innerRadius, depth, -0.5f * outerRadius),
		new Vector3(-innerRadius, depth, 0.5f * outerRadius)
	};

	public static int[] bottom_triangles = {
		7, 6, 8,
		9, 8, 10,
		11, 10, 6,
		10, 8, 6,
	};

	public static int[] side_triangles = {
		1, 0, 7,
		6, 7, 0,
		2, 1, 7,
		7, 8, 2,
		3, 2, 8,
		8 ,9 ,3,
		4, 3, 9,
		9, 10, 4,
		5, 4, 10,
		10, 11, 5,
		0, 5, 11,
		11, 6, 0
	};

}