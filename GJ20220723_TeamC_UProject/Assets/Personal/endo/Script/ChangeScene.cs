using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{  
   // ステージ遷移のボタン
   public void onClickSceneChangeBtn(string SceneName)
   {
      SceneManager.LoadScene(SceneName);
   }
}
   
