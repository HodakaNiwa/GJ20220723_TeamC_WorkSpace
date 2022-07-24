using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{  
   /// <summary>
   /// Scene遷移を行う関数(引数はString)
   /// </summary>
   public void onClickSceneChangeBtn(string SceneName)
   {
      SceneManager.LoadScene(SceneName);
   }
}
   
