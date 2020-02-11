using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
  void Awake()
  {
    SetupSingleton();
  }

  private void SetupSingleton()
  {
    if (FindSceneObjectsOfType(GetType()).Length > 1)
    {
      Destroy(gameObject);
    }
    else
    {
      DontDestroyOnLoad(gameObject);
    }
  }

  void Update()
  {

  }
}
