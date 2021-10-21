using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    private bool isInit = false;
    protected StringKeySender Sender;

    public void SendMessageToCtrl(string pString,object pParams = null) {
        if (Sender != null)
        {
            Sender.SendMessage(pString, pParams);
        }
        else
        {
            var sender = GetParentSender(this.transform);
            if (sender != null)
            {
                Sender = sender;
                Sender.SendMessage(pString, pParams);
            }
            else
            {
                Debug.LogError("is not have ctrl event sender!!");
            }
        }
    }
    private StringKeySender GetParentSender(Transform pTarget)
    {
        var uiBase = pTarget.gameObject.GetComponentInParent<UIBase>();
        if (uiBase != null )
        {
            if (uiBase.Sender != null)
                return uiBase.Sender;
            else
            {
                if (uiBase.transform.parent == null)
                    return null;
                else
                    return GetParentSender(uiBase.transform.parent);
            }
        }
        else
        {
            return null;
        }
    }
    public void SetSender(StringKeySender pSender)
    {
        Sender = pSender;
    }
    private void Awake() { Init(); }
    private void Init(){
        if (!isInit){
            isInit = true;
            OnInit();
        }
    }
    public virtual void OnInit() { }//���ĳ�ʼ��
    public void DoForward() { OnForward(); }
    public virtual void OnForward() {
        
    } //��屻�е����ϲ���ʾ
    public virtual void DoDestroy() { }//��屻����
    private void OnDestroy() { DoDestroy(); }
}
