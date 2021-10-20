using UnityEngine.UI;

public class LoginPanel : UIBase
{
    public Button ABtn;
    public override void OnInit()
    {
        ABtn.onClick.AddListener(() => {
            UICtrlManager.OpenBaseUI(new MainPanelCtrl(), () => { });
        });
    }
    public override void OnForward()
    {

    }
    public override void DoDestroy()
    {
        ABtn.onClick.RemoveAllListeners();

    }
}