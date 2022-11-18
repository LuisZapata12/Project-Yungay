using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Boss))]
public class BossRange : Editor
{
	void OnSceneGUI()
	{
		Boss boss = (Boss)target;
		Handles.color = Color.red;
		Handles.DrawWireArc(boss.transform.position, Vector3.up, Vector3.forward, 360, boss.nearDistance);
		Handles.DrawWireArc(boss.transform.position, Vector3.up, Vector3.forward, 360, boss.midDistance);
		Handles.DrawWireArc(boss.transform.position, Vector3.up, Vector3.forward, 360, boss.farDistance);

	}
}
