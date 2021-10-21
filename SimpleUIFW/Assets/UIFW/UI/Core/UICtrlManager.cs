using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICtrlManager
{
	public static UICtrlPolicy BasePolicy = new UICtrlPolicy(GameObject.Find("Canvas"), 100);

	public static void OpenBaseUI(UICtrlBase ctrl, System.Action pCB)
	{
		BasePolicy.Open(ctrl, pCB, new OpenData());
	}
	public static void CloseTopBaseUI()
	{
		BasePolicy.CloseTop();
	}
	public static void RevokeToHomeBaseUI()
	{
		BasePolicy.RevokeToHome();
	}
}
