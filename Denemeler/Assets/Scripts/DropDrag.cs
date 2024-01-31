using RecycleGame;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    
    public RectTransform Transform;  //s�r�klenecek nesnenin bilgilerini sakl� tutmak i�in
    private enum DragState { DogruKumbara, Yanl�sKumbara }
    private DragState _deger;  
    private Rigidbody2D _effectedObjectRigidbody2D;
    /*Vector2 _originalScale, _interactObject;
    //[SerializeField] RectTransform OrganikAtiklar,MetalAtiklar,KagitAtiklar;
    //public int _collidingCount;
    private Vector2 _startPosition;
    private Vector2 _lastPosition;
     
     */
    string _preefSpeechData, _wrongSpeech;
    internal static int _bonusCount, Score;
    


    private void Awake()
    {
        _effectedObjectRigidbody2D = GetComponent<Rigidbody2D>();
        /*_originalScale = new Vector2(gameObject.transform.localScale.x,gameObject.transform.localScale.y);
        //_collidingCount = 0;*/
    }

    private void Start()
    {
        //_deger = DragState.Belirsiz;
        //_lastPosition = transform.position;
        Destroy(gameObject,15f); //10f 10 saniye sonra nesnenin yok edilece�ini belirtir

        /*OrganikAtiklar = GameObject.Find("KO").GetComponent<RectTransform>();
        MetalAtiklar = GameObject.Find("ME").GetComponent<RectTransform>();
        KagitAtiklar = GameObject.Find("KP").GetComponent<RectTransform>();*/
    }



    public void OnBeginDrag(PointerEventData eventData)
    {
        //_startPosition = transform.position;
        _effectedObjectRigidbody2D.velocity = Vector3.zero;
        //_effectedObjectRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    public void OnDrag(PointerEventData eventData)
    {
        _effectedObjectRigidbody2D.gravityScale = 0;
        _effectedObjectRigidbody2D.velocity = Vector3.zero;
        Vector3 vec = Camera.main.WorldToScreenPoint(Transform.position); 
        /*1)s�r�klenecek objenin d�nya kordinat�n� piksel kordinata d�n��t�r�yoruz ��nk� farenin kordinat� piksel(screen) olarak i�leniyor, fareyle objenin transform bilgileri piksel(screen) cinsinden uyumlu olsun diye
         * 2)yeni bir vekt�r olu�turmam�z�n sebebi x ve y kordinatlar�na s�r�kleme eventini eklemek*/
        vec.x += eventData.delta.x; 
        vec.y += eventData.delta.y; 
        /* x ve y kordinatlar�nda s�r�kleme eventini ekmeyi  sa�lad� bu iki sat�r
         * piksel vect�r�ne eventleri ekliyoruz ��nk� fare piksel bilgisiyle �al���yor*/
        Transform.position = Camera.main.ScreenToWorldPoint(vec);
        /* transform bilgisini burada tekrardan d�nya kordinat�na d�n��t�r�yoruz ��nk� oyun objeleri piksel(screen) olarak de�il merkezi kordinata g�re �al���yor
        // piksel(screen) kordinat sol alt (0,0) origin noktas� olarak al�yor, d�nya kordinat� ekran�n tam ortas�n� (0,0) origin noktas� olarak al�yor*/
        EventManager.Instance.OnSpeechBubble("E�yay� tuttun");
        Debug.Log(vec);//=> objenin piksel bilgisini veriyor
        Debug.Log(Camera.main.ScreenToWorldPoint(vec));//=> objenin kordinat bilgisini veriyor

        /*iTween.ScaleTo(gameObject,iTween.Hash("x",_originalScale.x*300 / Input.mousePosition.y,"y", _originalScale.y*300 
         * 
         * Input.mousePosition.y,"time",0.01f));*/
        EventManager.Instance.OnColorBubble(Color.black);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _effectedObjectRigidbody2D.velocity = Vector3.zero;
        _effectedObjectRigidbody2D.gravityScale = 1;
        //_lastPosition = Transform.position;
        EventManager.Instance.OnSpeechBubble("E�yay� B�rakt�n");
        

        //DragEndRoutine(); => buraya yaz�p collisionda tek tek yazmazsam di�er kutular�n i�inden g�r�t�p b�rakabilirim fakat e�yay� illaki kutunun tam �st�nde b�rakmam gerekir uza��ndan b�rak�rsam ��p kutusunun i�inden ge�ip a�a��ya d��er.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //_collidingCount++;

        if(collision.CompareTag(this.tag))
        {
            _bonusCount++;
            _deger = DragState.DogruKumbara;
            if (_bonusCount > 0 && _bonusCount <= 5)
            {
                Score += 50;
                GameManager.Instance.IsGameFast = false;
                
            }
            if (_bonusCount > 5)
            {
                Score += 200;
                GameManager.Instance.IsGameFast = true;
                
            }
            DragEndRoutine(); //=> b�yle olu�unda temas olunca bu metoda y�nelndirir yukardaki gibi olursa �arp��ma olunca i�inden ge�ip yere d��er
            //_deger = DragState.DogruKumbara;
            //StartCoroutine("DogruKumbaraRutini");
            //DogruKumbaraRutini();

            
        }
        else if(!collision.CompareTag(this.tag))
        {
            _deger = DragState.Yanl�sKumbara;
            Score -= 50;
            _bonusCount = 0;
            DragEndRoutine();

            //StartCoroutine("Yanl�sKumbaraRutini");
            //_wrongSpeech = collision.gameObject.name;
        }
        /*else if(_collidingCount ==0)
        {
            //StartCoroutine("BosaGittiRutini");
            DragEndRoutine();

        }

        //_interactObject = collision.GetComponent<RectTransform>().anchoredPosition;*/
    }
    /*public IEnumerator DragEndRoutine()
    {
        if(_deger==DragState.DogruKumbara)
        {
            DogruKumbaraRutini();
            //StartCoroutine("DogruKumbaraRutini");
        }
        else if (_deger == DragState.Yanl�sKumbara)
        {
            StartCoroutine("Yanl�sKumbaraRutini");
        }
        else if (_collidingCount==0)
        {
            StartCoroutine("BosaGittiRutini");
        }
        yield return null;

    }*/
    private void DragEndRoutine()
    {
        if (_deger == DragState.DogruKumbara)
        {
            DogruKumbaraRutini();
            //StartCoroutine("DogruKumbaraRutini");
        }
        else if (_deger == DragState.Yanl�sKumbara)
        {
            Yanl�sKumbaraRutini();
        }
        /*else if (_collidingCount == 0)
        {
            BosaGittiRutini();
        }*/
    }
        /* public IEnumerator DogruKumbaraRutini()
         {
             List<string> SpeechList = new List<string>();
             SpeechList.Add("Bravo Harika Gidiyorsun");
             SpeechList.Add("�ok iyisin");
             SpeechList.Add("B�yle Devam Et!");
             SpeechList.Add("Harikas�n");
             SpeechList.Add("sen bu i�i biliyorsun");
             SpeechList.Add("d�nayn�n en �evreci insan�s�nn!!");
             SpeechList.Add("m�kemmel e�le�me");

             int stringimiz = Random.Range(0, SpeechList.Count);
             _preefSpeechData = SpeechList[stringimiz];
             EventManager.Instance.OnSpeechBubble(_preefSpeechData);
             Destroy(gameObject);
             yield return new WaitForSeconds(0.5f);
         }*/
        private void DogruKumbaraRutini()
    {
        List<string> SpeechList = new List<string>();
        SpeechList.Add("Bravo Harika Gidiyorsun");
        SpeechList.Add("�ok iyisin");
        SpeechList.Add("B�yle Devam Et!");
        SpeechList.Add("Harikas�n");
        SpeechList.Add("sen bu i�i biliyorsun");
        SpeechList.Add("d�nayn�n en �evreci insan�s�nn!!");
        SpeechList.Add("m�kemmel e�le�me");
        int stringimiz = Random.Range(0, SpeechList.Count);
        _preefSpeechData = SpeechList[stringimiz];
        EventManager.Instance.OnSpeechBubble(_preefSpeechData);
        EventManager.Instance.OnColorBubble(Color.green);
        EventManager.Instance.PuanArtt�rma(Score,_bonusCount);

        Destroy(gameObject);
        
    }

    /*public IEnumerator Yanl�sKumbaraRutini()
    {
        List<string> SpeechList = new List<string>();
        SpeechList.Add("Yanl�� Yere Att�n");
        SpeechList.Add("dikkat et yanl�� oldu");
        SpeechList.Add("bir anl���na dikkatin da��ld� galiba yanl�� kutu");
        int stringimiz = Random.Range(0, SpeechList.Count);
        _preefSpeechData = SpeechList[stringimiz];
        EventManager.Instance.OnSpeechBubble( _preefSpeechData);
        Destroy(gameObject);
        yield return new WaitForSeconds(0.5f);
    }*/
    public void Yanl�sKumbaraRutini()
    {
        List<string> SpeechList = new List<string>();
        SpeechList.Add("Yanl�� Yere Att�n");
        SpeechList.Add("dikkat et yanl�� oldu");
        SpeechList.Add("bir anl���na dikkatin da��ld� galiba yanl�� kutu");
        int stringimiz = Random.Range(0, SpeechList.Count);
        _preefSpeechData = SpeechList[stringimiz];
        EventManager.Instance.OnSpeechBubble(_preefSpeechData);
        EventManager.Instance.OnColorBubble(Color.red);
        EventManager.Instance.PuanArtt�rma(Score,_bonusCount);
        
        Destroy(gameObject);

    }

    /*public IEnumerator BosaGittiRutini()
    {
        _preefSpeechData = "At��a Yaz�k Oldu";
        yield return new WaitForSeconds(0.1f);
        //Destroy(gameObject);
        EventManager.Instance.OnSpeechBubble(_preefSpeechData);

    }*/
    /*public void BosaGittiRutini()
    {
        _preefSpeechData = "At��� B�rakt�n";
        //yield return new WaitForSeconds(0.1f);
        //Destroy(gameObject);
        EventManager.Instance.OnColorBubble(Color.red);
        EventManager.Instance.OnSpeechBubble(_preefSpeechData);

    }*/
}
    


    

