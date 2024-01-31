using RecycleGame;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    
    public RectTransform Transform;  //sürüklenecek nesnenin bilgilerini saklý tutmak için
    private enum DragState { DogruKumbara, YanlýsKumbara }
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
        Destroy(gameObject,15f); //10f 10 saniye sonra nesnenin yok edileceðini belirtir

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
        /*1)sürüklenecek objenin dünya kordinatýný piksel kordinata dönüþtürüyoruz çünkü farenin kordinatý piksel(screen) olarak iþleniyor, fareyle objenin transform bilgileri piksel(screen) cinsinden uyumlu olsun diye
         * 2)yeni bir vektör oluþturmamýzýn sebebi x ve y kordinatlarýna sürükleme eventini eklemek*/
        vec.x += eventData.delta.x; 
        vec.y += eventData.delta.y; 
        /* x ve y kordinatlarýnda sürükleme eventini ekmeyi  saðladý bu iki satýr
         * piksel vectörüne eventleri ekliyoruz çünkü fare piksel bilgisiyle çalýþýyor*/
        Transform.position = Camera.main.ScreenToWorldPoint(vec);
        /* transform bilgisini burada tekrardan dünya kordinatýna dönüþtürüyoruz çünkü oyun objeleri piksel(screen) olarak deðil merkezi kordinata göre çalýþýyor
        // piksel(screen) kordinat sol alt (0,0) origin noktasý olarak alýyor, dünya kordinatý ekranýn tam ortasýný (0,0) origin noktasý olarak alýyor*/
        EventManager.Instance.OnSpeechBubble("Eþyayý tuttun");
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
        EventManager.Instance.OnSpeechBubble("Eþyayý Býraktýn");
        

        //DragEndRoutine(); => buraya yazýp collisionda tek tek yazmazsam diðer kutularýn içinden görütüp býrakabilirim fakat eþyayý illaki kutunun tam üstünde býrakmam gerekir uzaðýndan býrakýrsam çöp kutusunun içinden geçip aþaðýya düþer.
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
            DragEndRoutine(); //=> böyle oluðunda temas olunca bu metoda yönelndirir yukardaki gibi olursa çarpýþma olunca içinden geçip yere düþer
            //_deger = DragState.DogruKumbara;
            //StartCoroutine("DogruKumbaraRutini");
            //DogruKumbaraRutini();

            
        }
        else if(!collision.CompareTag(this.tag))
        {
            _deger = DragState.YanlýsKumbara;
            Score -= 50;
            _bonusCount = 0;
            DragEndRoutine();

            //StartCoroutine("YanlýsKumbaraRutini");
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
        else if (_deger == DragState.YanlýsKumbara)
        {
            StartCoroutine("YanlýsKumbaraRutini");
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
        else if (_deger == DragState.YanlýsKumbara)
        {
            YanlýsKumbaraRutini();
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
             SpeechList.Add("Çok iyisin");
             SpeechList.Add("Böyle Devam Et!");
             SpeechList.Add("Harikasýn");
             SpeechList.Add("sen bu iþi biliyorsun");
             SpeechList.Add("dünaynýn en çevreci insanýsýnn!!");
             SpeechList.Add("mükemmel eþleþme");

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
        SpeechList.Add("Çok iyisin");
        SpeechList.Add("Böyle Devam Et!");
        SpeechList.Add("Harikasýn");
        SpeechList.Add("sen bu iþi biliyorsun");
        SpeechList.Add("dünaynýn en çevreci insanýsýnn!!");
        SpeechList.Add("mükemmel eþleþme");
        int stringimiz = Random.Range(0, SpeechList.Count);
        _preefSpeechData = SpeechList[stringimiz];
        EventManager.Instance.OnSpeechBubble(_preefSpeechData);
        EventManager.Instance.OnColorBubble(Color.green);
        EventManager.Instance.PuanArttýrma(Score,_bonusCount);

        Destroy(gameObject);
        
    }

    /*public IEnumerator YanlýsKumbaraRutini()
    {
        List<string> SpeechList = new List<string>();
        SpeechList.Add("Yanlýþ Yere Attýn");
        SpeechList.Add("dikkat et yanlýþ oldu");
        SpeechList.Add("bir anlýðýna dikkatin daðýldý galiba yanlýþ kutu");
        int stringimiz = Random.Range(0, SpeechList.Count);
        _preefSpeechData = SpeechList[stringimiz];
        EventManager.Instance.OnSpeechBubble( _preefSpeechData);
        Destroy(gameObject);
        yield return new WaitForSeconds(0.5f);
    }*/
    public void YanlýsKumbaraRutini()
    {
        List<string> SpeechList = new List<string>();
        SpeechList.Add("Yanlýþ Yere Attýn");
        SpeechList.Add("dikkat et yanlýþ oldu");
        SpeechList.Add("bir anlýðýna dikkatin daðýldý galiba yanlýþ kutu");
        int stringimiz = Random.Range(0, SpeechList.Count);
        _preefSpeechData = SpeechList[stringimiz];
        EventManager.Instance.OnSpeechBubble(_preefSpeechData);
        EventManager.Instance.OnColorBubble(Color.red);
        EventManager.Instance.PuanArttýrma(Score,_bonusCount);
        
        Destroy(gameObject);

    }

    /*public IEnumerator BosaGittiRutini()
    {
        _preefSpeechData = "Atýða Yazýk Oldu";
        yield return new WaitForSeconds(0.1f);
        //Destroy(gameObject);
        EventManager.Instance.OnSpeechBubble(_preefSpeechData);

    }*/
    /*public void BosaGittiRutini()
    {
        _preefSpeechData = "Atýðý Býraktýn";
        //yield return new WaitForSeconds(0.1f);
        //Destroy(gameObject);
        EventManager.Instance.OnColorBubble(Color.red);
        EventManager.Instance.OnSpeechBubble(_preefSpeechData);

    }*/
}
    


    

