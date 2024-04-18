using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class BallMovement : MonoBehaviour
{
    public float speed = 1f;
    public GameObject DeadText;
    public GameObject FinishText;
    public GameObject ScoreText;

    int score = 0;
    void Start()
    {
        Debug.Log("kod calistirildi");
        DeadText.SetActive(false);
        FinishText.SetActive(false);
    }


    void Update()
    {
        // z ekseninde ilri gitsin
        //transform.position = transform.position + new Vector3(0, 0, 0.01f);

        // baska bir yazim sekli
        //transform.position += new Vector3(0, 0, 0.01f);

        //daha da kisasi var
        transform.position += Vector3.forward * speed;

        if (speed != 0)
        {
            score += 1;
            ScoreText.GetComponent<TextMeshProUGUI>().text = score.ToString().PadLeft(6,'0');
            //burada sunu yaptik scoretext isimi tmp'nin yaz� olan k�sm�na eristik oran�n da yazisini aldik ve buraya score dedik orayi da stringe cevirdik
            //sonra da padleft dedik bu da su demek toplam alti hane olacak sekilde solunu sifirlarla doldur
        }


        //hareket icin 2 input sistemi var bu eskisi
        // getkey getkeydown getkeyup var ilki bas�l� tut ikincisi bas ucuncusu bast�ktan sonra cektigin zaman

        if (Input.GetKeyDown(KeyCode.LeftArrow)&& speed  != 0)
        {
            Debug.Log("Sol oka bas�ld�");
            transform.position = new Vector3(-0.280f, transform.position.y, transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && speed != 0)
        {
            Debug.Log("Sag oka bas�ld�");
            transform.position = new Vector3(0.280f, transform.position.y, transform.position.z);
        }

        //yukar�da belli bir tus atad�k fakat oyuncu bazen tuslar� degistirmek isteyebilir o zaman yeni input sistem daha mant�kl� ama sonra deginiriz
    }

    //simdi yazacag�m�z kod top bir engelle carptiginde algilayabilmemiz  icin
    private void OnCollisionEnter(Collision collision)
    {
        /*bu sc hangi nesneye takiliysa baska bir nesne ile carpistigi anda bu fonksiyon calistirilacak
        carpisma icin rigidbody ve collider lazim unutma
        carpisacagi nesnede collider olmas� yeterli*/

        //sadece engel olanlar� obstacle olarak tag'ledim podiuma carp�nca ayn� etkiyi g�stermesin oyun durmas�n diye
        if (collision.collider.tag=="Obstacle") 
        {
            Debug.Log("�u Nesne �le �arp��ma Ger�ekle�ti: " + collision.collider.name);
            //burada speed'i s�f�rlayacag�z ki carptigi zaman top hareete devam edemesin bitmis gibi gozuksun
            speed = 0f;
            DeadText.SetActive(true);

            //ama hala saga sola gitmeye devam ediyor o yuzden yukaridaki hareket if lerine && ekleyecegiz
            //bunu da speed 0 mi diye sorgulayarak yapacagiz
            //veya en basa bir bool yazip dead olarak isimlendirip onu basta false olarak set edebiliriz
            //speed 0 iken bunu true yapariz ve && den sonra !dead yazariz
            //ama biz en basta speed != 0 seklinde yapacagiz
        }

        if (collision.collider.tag == "Finish")
        {
            Debug.Log("Kazand�n�z");
            speed = 0f;
            FinishText.SetActive(true);
        }

        //burada speed'i s�f�rlayacag�z ki carptigi zaman top hareete devam edemesin bitmis gibi gozuksun
    }
}
