using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DataController : Singleton<DataController>
{

    [SerializeField] Transform _btnManagerTrans;
    [SerializeField] Button _btnPrefab; 
    [SerializeField] PlayerController _playerController;
    Coroutine h;

    float timer = 3;
    bool isShowed = true;
    private int _score = 0;

    public int Score
    {
        get
        {
            return _score;
        }

        set {
            if (value < 0)
                return;
            _score = value;
            Observer.instant.NOtify("SCORE", this._score);

        }
    }
    // Start is called before the first frame update
    void Start()
    {
       
       // h = StartCoroutine(Test2("https://docs.unity3d.com/Manual/ExecutionOrder.html"));
        TextAsset[] files = Resources.LoadAll<TextAsset>("DATA");
        foreach (var i in files)
        {
            GunDataTest g = JsonUtility.FromJson<GunDataTest>(i.text);
            Sprite dataImage = Resources.Load<Sprite>(g.image); 

            Button b = Instantiate(_btnPrefab, _btnManagerTrans);
            b.image.sprite = dataImage;

            b.onClick.AddListener(() =>
            {
                GameObject gunPrefab = Resources.Load<GameObject>(g._gunController);

                GunControllerBase gunObj = _playerController.GetComponentInChildren<GunControllerBase>();
                if (gunObj)
                {
                    gunObj.gameObject.SetActive(false);
                    gunObj.transform.SetParent(null);
                }
                gunObj = LazyPooling.Instance.getObj<GunControllerBase>(gunPrefab);
                gunObj.transform.SetParent(_playerController.transform);
                 
                gunObj._gunDataDemo = g; 
            });
        }

        
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator Test2(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                  
                    break;
                case UnityWebRequest.Result.ProtocolError:
                  
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.LogError(request.downloadHandler.text);
                    break;
            }
        }
    }
}
