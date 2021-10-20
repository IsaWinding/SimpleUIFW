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
    public virtual void OnInit() { }//面板的初始化
    public void DoForward() { OnForward(); }
    public virtual void OnForward() {
        
    } //面板被切到最上层显示
    public virtual void DoDestroy() { }//面板被销毁
    private void OnDestroy() { DoDestroy(); }
}
