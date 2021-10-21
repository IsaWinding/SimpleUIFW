using System.Collections.Generic;
using UnityEngine;

public class UICtrlPolicy
{
	public GameObject SubRoot;
	public int BaseLayer;
	private List<UICtrlBase> ctrls = new List<UICtrlBase>();
	public UICtrlPolicy(GameObject pSubRoot, int pBaseLayer)
	{
		SubRoot = pSubRoot;
		BaseLayer = pBaseLayer;
	}

	public void Open(UICtrlBase pCtrl, System.Action pFinishCB, OpenData pOpenData)
	{
		Flow flow = new Flow(1);
		flow.AddStep(1, (pOnFinish) => {
			pCtrl.OnInit();
			pOnFinish.Invoke(true);
		});
		flow.AddStep(2, (pOnFinish) => {
			pCtrl.OnPost(pOnFinish);
		});
		flow.AddStep(3, (pOnFinish) => {
			pCtrl.ResLoad((oGO) => {
				if (oGO != null){
					oGO.transform.SetParent(SubRoot.transform);
					oGO.transform.localPosition = Vector3.zero;
					pOnFinish.Invoke(true);
				}
				else
				{
					pOnFinish.Invoke(false);
				}
			});
		});
		flow.AddStep(4, (pOnFinish) => {
			pCtrl.OnForward();
			pOnFinish.Invoke(true);
		});
		flow.RunAllStep((pIsFinish) => {
			ctrls.Add(pCtrl);
			pFinishCB.Invoke();
		});
	}
	public void RevokeToHome()
	{
		var totalCount = ctrls.Count;
		if (totalCount > 1)
		{ 
			for(var i = 0;i< totalCount -1;i++)
			{
				CloseTop(i == totalCount - 2);
			}
		}
	}
	public void CloseTop(bool pNeedForward = true)
	{
		if (ctrls.Count > 0)
		{
			UICtrlBase ctrl = ctrls[ctrls.Count - 1];
			Flow flow = new Flow(2);
			flow.AddStep(1, (pOnFinish) => {
				ctrl.OnDispose();
				pOnFinish.Invoke(true);
			});
			flow.AddStep(2, (pOnFinish) => {
				ctrl.UnLoadRes();
				pOnFinish.Invoke(true);
			});
			flow.AddStep(3, (pOnFinish) => {
				var preCtrl = ctrls[ctrls.Count - 2];
				if (preCtrl != null && pNeedForward)
				{
					preCtrl.OnForward();
					pOnFinish.Invoke(true);
				}
				else
				{
					pOnFinish.Invoke(true);
				}
			});
			flow.RunAllStep((pIsFinish) => {
				ctrls.Remove(ctrl);
			});
		}
	}
}

public enum OpenType
{
	None = 0,
	DisablePre = 1,
	DestoryPre = 2,
}
public class OpenData
{
	public OpenType OpenType = OpenType.None;
}
