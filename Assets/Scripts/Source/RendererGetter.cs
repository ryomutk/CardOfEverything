using Utility.ObjPool;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Utility;

public class RendererGetter:MonoBehaviour
{
    InstantPool<Image> imagePool;
    InstantPool<TMP_Text> textPool;

    [SerializeField] TMP_Text rawTextPref;
    [SerializeField] Image rawImageObj;

    void Start()
    {
        imagePool = new InstantPool<Image>();
        textPool = new InstantPool<TMP_Text>();
        
        textPool.CreatePool(rawTextPref,10);
        imagePool.CreatePool(rawImageObj,10);
    }

    public Image GetImageObj()
    {
        return imagePool.GetObj();
    }

    public TMP_Text GetTextObj()
    {
        return textPool.GetObj();
    }
}