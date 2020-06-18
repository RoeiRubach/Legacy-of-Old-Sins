using System.Collections.Generic;
using UnityEngine;

public class TutorialPopUpsController : Singleton<TutorialPopUpsController>
{
    private Queue<Transform> _images = new Queue<Transform>();
    public Dictionary<string, bool> MyTutorialHandler = new Dictionary<string, bool>();
    private PlayerController _playerController;

    private void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            var curChild = transform.GetChild(i);
            _images.Enqueue(curChild);
            MyTutorialHandler.Add(curChild.name, false);
        }
        MyTutorialHandler["Movement"] = true;

        if (GameManager.Instance.IsReachedCheckPoint)
            CheckPointTutorialInitialize("Move bomb");
    }

    public void DestroyFirstChild()
    {
        MyTutorialHandler[_images.Peek().name] = false;
        Destroy(_images.Dequeue().gameObject);
        if(!_images.Peek().name.Equals("Move bomb"))
            MyTutorialHandler[_images.Peek().name] = true;
    }
    public void DisplayFirstChild()
    {
        _images.Peek().gameObject.SetActive(true);
    }
    public void HideFirstChild()
    {
        _images.Peek().gameObject.SetActive(false);
    }

    public void SetDouglasBombBoolTrue()
    {
        MyTutorialHandler[_images.Peek().name] = true;
        _playerController = GameObject.Find("Characters Manager").GetComponent<PlayerController>();
        if (_playerController.GetCurrentStateRef() is DouglasState)
            DisplayFirstChild();
    }

    public void CheckPointTutorialInitialize(string popup)
    {
        int queueCount = _images.Count;
        for (int i = 0; i < queueCount; i++)
        {
            if (_images.Peek().name.Equals(popup))
            {
                MyTutorialHandler[popup] = false;
                return;
            }
            DestroyFirstChild();
        }
    }
}
