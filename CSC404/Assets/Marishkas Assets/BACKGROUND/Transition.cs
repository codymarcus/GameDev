using UnityEngine;
using System.Collections;

public class Transition : MonoBehaviour {
float duration = 30.0f;
public SpriteRenderer sRender;
// Update is called once per frame
Color HexToColor(string hex)
{
	byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
	byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
	byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
	return new Color32(r,g,b, 255);
}


void Update () {
	float lerp = Mathf.PingPong(Time.time, duration) / duration;
		sRender.material.SetColor("_Color", Color.Lerp(HexToColor("c0e4eb"),HexToColor("424450"), lerp)); //bottom
		sRender.material.SetColor("_Color2", Color.Lerp(HexToColor("c0e4eb"),HexToColor("424450"), lerp)); //top
}
}