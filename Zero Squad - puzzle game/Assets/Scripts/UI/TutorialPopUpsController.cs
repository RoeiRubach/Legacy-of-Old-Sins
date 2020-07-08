using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPopUpsController : Singleton<TutorialPopUpsController>
{
    public Dictionary<string, bool> MyTutorialHandler = new Dictionary<string, bool>();
    private Queue<Transform> _images = new Queue<Transform>();
    private PlayerController _playerController;
    private Image currentPopup;

    private void Start()
    {
        _playerController = GameObject.Find("Characters Manager").GetComponent<PlayerController>();
        if (GameManager.Instance.IsReachedFinalCheckPoint)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            return;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            var curChild = transform.GetChild(i);
            _images.Enqueue(curChild);
            MyTutorialHandler.Add(curChild.name, false);
        }
        if (_images.Count == 0) return;

        currentPopup = _images.Peek().GetComponent<Image>();
        MyTutorialHandler["Movement"] = true;

        if (GameManager.Instance.IsReachedCheckPoint)
            CheckPointTutorialInitialize("Move bomb");
    }

    public void DestroyFirstChild()
    {
        if (_images.Count == 0) return;

        MyTutorialHandler[_images.Peek().name] = false;
        Destroy(_images.Dequeue().gameObject);

        if (_images.Count == 0) return;
        currentPopup = _images.Peek().GetComponent<Image>();
        if (!_images.Peek().name.Equals("Move bomb"))
            MyTutorialHandler[_images.Peek().name] = true;
    }
    public void DisplayFirstChild()
    {
        if (_images.Count == 0) return;
        _images.Peek().gameObject.SetActive(true);
        if (currentPopup == null) return;

        currentPopup.enabled = true;
    }
    public void HideFirstChild()
    {
        if (_images.Count == 0) return;
        if (!_images.Peek().gameObject.activeSelf) return;
        if (currentPopup == null)
        {
            _images.Peek().gameObject.SetActive(false);
            return;
        }

        currentPopup.enabled = false;
    }

    public void SetDouglasBombBoolTrue()
    {
        MyTutorialHandler[_images.Peek().name] = true;
        if (_playerController.GetCurrentStateRef() is DouglasState)
            DisplayFirstChild();
    }

    public void CheckPointTutorialInitialize(string popup)
    {
        if (_images.Count == 0) return;

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
