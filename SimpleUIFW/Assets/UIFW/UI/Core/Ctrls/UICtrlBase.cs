using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICtrlBase
{
	protected virtual string Key { get { return "UICtrlBase"; } }//C ��key ��ֵ
	protected virtual string Path { get { return ""; } }//���ص���Դ·��

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
	public virtual void OnInit() { }//��ʼ��

	public void ResLoad(System.Action<UnityEngine.GameObject> pCB)//��Դ����
	{
		loadAssetId = AddressLoadManager.LoadAsync(Path,(oGO,pAssetId) => {
			if (oGO != null){
				var go = oGO as GameObject;
				uiBase = go.GetComponent<UIBase>();
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

	public virtual void OnDispose() { }//c �˳�
}
