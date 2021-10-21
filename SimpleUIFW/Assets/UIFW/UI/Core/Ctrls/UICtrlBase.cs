using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICtrlBase
{
	protected virtual string Key { get { return "UICtrlBase"; } }//C ��key ��ֵ
	protected virtual string Path { get { return ""; } }//���ص���Դ·��
	private StringKeySender Sender ;

	/// <summary> ������
	/// OnInit --> OnPost  --> ResLoad --> OnForward
	/// </summary>

	/// <summary> �ر�����
	/// OnDispose --> UnLoadRes
	/// </summary>
	/// 
	private object data_;//Ctrl����е�����
	private int loadAssetId;//��Դ������
	private UIBase uiBase;//���
	public virtual void OnPost(System.Action<bool> pCb)//�첽�߼��Ĵ�����������ͨ�Ų�Ĵ�������true ������سɹ� ����false �����ʧ��
	{
		pCb.Invoke(true);
	}
	public void OnSetData(Object pData)//�������ݵ�����
	{
		data_ = pData;
	}

	//��ʼ��
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

	public void ResLoad(System.Action<UnityEngine.GameObject> pCB)//��Դ����
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
	public void UnLoadRes()//��Դж��
	{
		AddressLoadManager.UnLoadByAssetId(loadAssetId);
	}
	public void OnForward()//�����е�ǰ̨
	{
		uiBase.OnForward();
	}
	//ctrl �˳�
	protected virtual void OnDispose() {
		
	}
	public void DoDispose()
	{
		Sender.Clear();
		OnDispose();
	}
}
