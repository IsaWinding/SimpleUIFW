using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionBehaviour : BaseBehaviour
{
    public Vector3 start;
    public Vector3 end;
    protected override void OnProgress(float pProgress)
    {
        target_.transform.localPosition = start + (end - start) * pProgress;

    }
}
