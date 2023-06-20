using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class StartSceneScript : MonoBehaviour
{
    [SerializeField] Image progressBar;
    float _target;

    void Awake()
    {

    }

    public void Start()
    {
        LoadSceneMenu(1);
    }

    public async void LoadSceneMenu(int sceneID)
    {
        _target = 0;
        progressBar.fillAmount = 0;

        var scene = SceneManager.LoadSceneAsync(sceneID);
        Time.timeScale = 1;
        scene.allowSceneActivation = false;

        do
        {
            await Task.Delay(100);
            _target = scene.progress;
        }
        while (scene.progress < 0.9f);

        await Task.Delay(2000);

        scene.allowSceneActivation = true;
    }

    void Update()
    {
        progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, _target, 3 * Time.deltaTime);
    }

}
