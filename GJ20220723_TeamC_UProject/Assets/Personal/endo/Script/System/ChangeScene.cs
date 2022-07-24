using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int StageIndex = 0;

   /// <summary>
   /// Scene遷移を行う関数(引数はString)
   /// </summary>
   public void onClickSceneChangeBtn(string SceneName)
   {
        if (ResidentVisualizeHolder.Instance != null)
        {
            ResidentVisualizeHolder.Instance.NextStageIndex = StageIndex;
        }
        SceneManager.LoadScene(SceneName);
   }
}
   
