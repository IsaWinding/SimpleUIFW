using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICtrlBase
{
	protected virtual string Key { get { return "UICtrlBase"; } }//C 的key 数值
	protected virtual string Path { get { return ""; } }//加载的资源路径
	private StringKeySender Sender ;

	/// <summary> 打开流程
	/// OnInit --> OnPost  --> ResLoad --> OnForward
	/// </summary>

	/// <summary> 关闭流程
	/// OnDispose --> UnLoadRes
	/// </summary>
	/// 
	private object data_;//Ctrl层持有的数据
	private int loadAssetId;//资源加载者
	private UIBase uiBase;//面板
	public virtual void OnPost(System.Action<bool> pCb)//异步逻辑的处理（例如网络通信层的处理）返回true 界面加载成功 返回false 界面打开失败
	{
		pCb.Invoke(true);
	}
	public void OnSetData(Object pData)//基本数据的设置
	{
		data_ = pData;
	}

	//初始化
	protected virtual void OnInit() {
		
	}
	public virtual void DoInit()
	{
		Sender = new StringKeySender();
		OnInit();
	}
	public void AddEventListener(string pKey,System.Action<object> pAction)
    {
		Sender.AddListener(pKey, pAction);
	}
	public void RemoveListener(string pKey, System.Action<object> pAction)
	{
		Sender.RemoveListener(pKey, pAction);
	}

	public void ResLoad(System.Action<UnityEngine.GameObject> pCB)//资源加载
	{
		loadAssetId = AddressLoadManager.LoadAsync(Path,(oGO,pAssetId) => {
			if (oGO != null){
				var go = oGO as GameObject;
				uiBase = go.GetComponent<UIBase>();
				uiBase.SetSender(Sender);
			}
			pCB.Invoke(oGO as GameObject);
		});
	}
	public void UnLoadRes()//资源卸载
	{
		AddressLoadManager.UnLoadByAssetId(loadAssetId);
	}
	public void OnForward()//界面切到前台
	{
		uiBase.OnForward();
	}
	//ctrl 退出
	protected virtual void OnDispose() {
		
	}
	public void DoDispose()
	{
		Sender.Clear();
		OnDispose();
	}
}
