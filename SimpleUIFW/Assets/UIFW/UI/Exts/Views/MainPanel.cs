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
			
		});
		_Btn.onClick.AddListener(() => {
			SendMessageToCtrl("BtnonClick");
			
		});
		_BackBtn.onClick.AddListener(() =>{
			SendMessageToCtrl("BackBtnonClick");
			
		});
		_HomeBtn.onClick.AddListener(() => {
			SendMessageToCtrl("HomeBtnonClick");
			
		});
	}
}
