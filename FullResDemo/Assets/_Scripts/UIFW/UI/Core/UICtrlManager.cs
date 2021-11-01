using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICtrlManager
{
	//栈式打开策略 所有界面都会打开，优先关闭最顶层界面也就是最后入场的界面
	public static UICtrlPolicy BasePolicy = new UICtrlPolicy(GameObject.Find("Canvas"), 100);
	//队列式打开策略 当前界面只有一个，其他界面打开需要当前界面关闭才会执行打开逻辑
	public static UICtrlPolicyQueue QueuePolicy = new UICtrlPolicyQueue(GameObject.Find("Canvas"), 100);

	public static OpenData DefaultOpen = new OpenData() { OpenType = OpenType.None };
	public static OpenData DiableOpen = new OpenData() { OpenType = OpenType.DisablePre };
	public static OpenData DestoryOpen = new OpenData() { OpenType = OpenType.DestoryPre };

	public static void QueueOpen(UICtrlBase ctrl, System.Action pCB, OpenData pOpenData = null)
	{
		QueuePolicy.Open(ctrl, pCB, pOpenData);
	}
	public static void CloseTopQueueUI()
	{
		QueuePolicy.CloseTop();
	}

	public static void OpenBaseUI(UICtrlBase ctrl, System.Action pCB, OpenData pOpenData = null)
	{
		BasePolicy.Open(ctrl, pCB, pOpenData);
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
