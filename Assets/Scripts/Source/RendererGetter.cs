using Utility.ObjPool;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class RendererGetter:MonoBehaviour
{
    InstantPool<Image> imagePool;
    InstantPool<TextMeshPro> textPool;

    [SerializeField] TextMeshPro rawTextPref;
    [SerializeField] Image rawImageObj;

    void Start()
    {
        imagePool = new InstantPool<Image>();
        textPool = new InstantPool<TextMeshPro>();
        
        textPool.CreatePool(rawTextPref,10);
        imagePool.CreatePool(rawImageObj,10);
    }

    public Image GetImageObj()
    {
        return imagePool.GetObj();
    }

    public TextMeshPro GetTextObj()
    {
        return textPool.GetObj();
    }
}