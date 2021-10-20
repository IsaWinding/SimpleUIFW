using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    private bool isInit = false;
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
