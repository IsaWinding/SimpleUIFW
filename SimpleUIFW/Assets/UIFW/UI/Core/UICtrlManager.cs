using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICtrlManager
{
	//ջʽ�򿪲��� ���н��涼��򿪣����ȹر�������Ҳ��������볡�Ľ���
	public static UICtrlPolicy BasePolicy = new UICtrlPolicy(GameObject.Find("Canvas"), 100);
	//����ʽ�򿪲��� ��ǰ����ֻ��һ���������������Ҫ��ǰ����رղŻ�ִ�д��߼�
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
