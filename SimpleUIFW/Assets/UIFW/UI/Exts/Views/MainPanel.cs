using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : UIBase
{
	public Button _BackBtn;
	public Button _Btn;
	public Button _HomeBtn;
	public override void OnInit()
	{
		_Btn.onClick.AddListener(() => {
			UICtrlManager.OpenBaseUI(new MainPanelCtrl(), () => { });
		});
		_BackBtn.onClick.AddListener(() =>{
			UICtrlManager.CloseTopBaseUI();
		});
		_HomeBtn.onClick.AddListener(() => {
			UICtrlManager.RevokeToHomeBaseUI();
		});
	}
}
