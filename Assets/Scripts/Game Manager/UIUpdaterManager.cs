using Ebac.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpdaterManager : Singleton<UIUpdaterManager>
{
    [Header("Gun UI")]
    public List<UIFillUpdater> uIGunUpdaters;
}
