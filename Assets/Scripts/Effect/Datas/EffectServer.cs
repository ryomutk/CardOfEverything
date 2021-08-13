using Effects;
using UnityEngine;
using System.Collections;
using Utility;
using System.Collections.Generic;

//Staticのほうが向いているがOnInitializeで毎度初期化したいのでSingleton
public class EffectServer : Singleton<EffectServer>, IInteraptor
{
    List<GUIMotionData> gUIMotionDataList = new List<GUIMotionData>();

    List<ObjectEffectData> objectEffectDatas = new List<ObjectEffectData>();
    public bool finished { get; private set; }

    void Start()
    {
        GameManager.instance.onInitialize += () => LoadResources();

    }


    void LoadResources()
    {
        StartCoroutine(LoadResource());
    }

    IEnumerator LoadResource()
    {
        finished = false;
        yield return StartCoroutine(LoadRequest<GUIMotionData>("GUIMotionData",gUIMotionDataList));
        yield return StartCoroutine(LoadRequest<ObjectEffectData>("ObjectEffectData",objectEffectDatas));
        finished = true;
    }

    IEnumerator LoadRequest<T>(string fileName,List<T> targetList)
    where T:class
    {
        var fileNames = System.IO.Directory.GetFiles(Application.dataPath + "/Resources/Scriptables/Effects/"+fileName);
        for(int i = 0; i < fileNames.Length;i++)
        {
            var request = Resources.LoadAsync("Scriptables/Effects/"+fileName+"/"+fileNames[i]);
            yield return new WaitUntil(()=>request.isDone);
            targetList.Add(request.asset as T);
        }
    }

    public ObjectEffect GetObjEffect(EffectName name, GameObject target)
    {
        for (int i = 0; i < objectEffectDatas.Count; i++)
        {
            if (objectEffectDatas[i].name == name)
            {
                var effect = objectEffectDatas[i];
                return effect.GetMotion(target) as ObjectEffect;
            }
        }

        throw new System.DllNotFoundException();
    }

    public IVisualEffect GetGUIMotion(EffectName name, GameObject target)
    {
        for (int i = 0; i < objectEffectDatas.Count; i++)
        {
            if (objectEffectDatas[i].name == name)
            {
                var effect = objectEffectDatas[i];
                return effect.GetMotion(target);
            }
        }

        throw new System.DllNotFoundException();

    }

    public static void GetScreenEffect()
    {

    }

}
