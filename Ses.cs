    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Windows.Speech;

    public class Ses : MonoBehaviour
    {
    //Kelimeleri barındaran bir tane dizi oluşturalım.
    //Bunun sebebi çok fazla kelime olduğu için bir birine yakın olan kelimeleri yanlış anlamasıdır. Az kelime her zaman daha iyidir.
    public string[] keywords = new string[] { "üst", "alt", "sol", "sağ" ,"gizlen","gözük"}; 
    //Konuşma seviyemizi belirtelim.
    public ConfidenceLevel confidence = ConfidenceLevel.Low;
    //Hiz değişkeni oluşturalım.
    public float speed = 1;

    //Söylediklerimizi ekrana yazdırmak için text oluşturdum.
    public Text results;
    //Karakterimizi tanımlayalım.
    public Image target;

    //Bir tane tanıyıcı oluşturalım.
    protected PhraseRecognizer recognizer;
    //Kelimelerimizi tutan string değişkeni belirleyelim.
    protected string word = "";

    private void Start()
    {
        //Eğer keywords null değilse
        if (keywords != null) 
        {
            recognizer = new KeywordRecognizer(keywords, confidence);
            recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
            recognizer.Start();
        }
    }

    //Söylediğimiz kelimeyi word değişkenine atayalım.
    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        word = args.text;
        //Söylediğimiz kelimeyi ekranda yazdıralım.
        results.text = "Söylediğiniz kelime: <b>" + word + "</b>";
    }

    private void Update()
    { 
        //Karakterimizin x ve y posizyon değerlerini alalım.

        var x = target.transform.position.x;
        var y = target.transform.position.y;
        //Kelimemizi söylediğimizde olacak şeyleri belirtelim.

        switch (word)
        {
            case "üst":
                y += speed;
                break;
            case "alt":
                y -= speed;
                break;
            case "sol":
                x -= speed;
                break;
            case "sağ":
                x += speed;
                break;
            case "gizlen":
               target.enabled=false;
                break;
            case "gözük":
                target.enabled = true;
                break;
        }
        //Karakterimizi haraket ettirelim.
        target.transform.position = new Vector3(x, y, 0);
    }
    //Eğer kişi uygulamayı kapattıysa tanıyıcıyı durduralım.
    private void OnApplicationQuit()
    {
        //Eğer tanıyıcı nul değilse ve çalışıyorsa
        if (recognizer != null && recognizer.IsRunning)
        {
            recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
            recognizer.Stop();
        }
    }
}