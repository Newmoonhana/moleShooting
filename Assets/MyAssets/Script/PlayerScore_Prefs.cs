using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerScore_Prefs : MonoBehaviour
{

    public GameObject HP;//체력이 실린더로되어있어서 매니저에선 못가져옴.
                         // public Text highScore;
    public Image RankImage;
    public Sprite[] RankSprites;
    // 이미지를 미리 넣어두고 함수로 해당 이미지에 주소에 맞는 이미지를 넣자!
    //스프라이트를 배열로 받은 뒤에 점수에 따라 생성하자!
    public GameObject ScoreText;

    public int PublicScore = 2500;//나중에 스테이지 매니저에서 값을 받아서하든 해서 스테이지마다
                                  //최고점을 다르게해야한다.
    public int[] Score = new int[3];

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Score[i] = PublicScore;//2500 2000 //1500
            PublicScore -= 500;//2000// 1500 //1000
            if (PublicScore < 0) PublicScore = 0;
        }
    }
    void Update()
    {
        // score.text = number.ToString();


        //
        //해당 씬으로 갔을 경우에만 if문 실행되며 랭크 이미지 배치, 점수적기를 한다.
        //
        if (SceneManager.GetActiveScene().name == "GameOver_Scene")
        {
            Rankcompare(PlayerPrefs.GetInt("MyScore", 0));//점수를 받고 랭크에 해당하는 스프라이트로 교체.
            ScoreText.GetComponent<Text>().text = PlayerPrefs.GetInt("MyScore", 0).ToString();

        }




        //
        //아래에 if문 없으면 게임오버씬에서도 실행되므로 안 되게 해야한다.
        //
        if (SceneManager.GetActiveScene().name != "GameOver_Scene")
        {
            PlayerPrefs.SetFloat("MyLifePoint", HP.GetComponent<Slider>().value);//현 체력을 적는다.  
            PlayerPrefs.SetInt("MyScore", GameManager.inst.ScoreM.count);//현 점수를 적는다.
        }

        //
        // 최고점수를 넣는 if문
        //
        if (PlayerPrefs.GetInt("MyScore", 0) > PlayerPrefs.GetInt("HighScore", 0)) //값 가져오기(없을 시 0 반환).
        {
            //스테이지에 따라서 해당 스테이지의 랭크를 올리기위해 필요한 점수의 정보를 받을 수 있는게 필요.
            PlayerPrefs.SetInt("HighScore", GameManager.inst.ScoreM.count);
            //   highScore.text = "최고점수 : " + Scorem.ScoreM.count; 
        }

    }

    void Rankcompare(int RankInt)
    {



        //highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();//get set 구별해라

        if (PlayerPrefs.GetFloat("MyLifePoint", 0) <= 1)
        {
            ScoreText.SetActive(false);
            RankImage.sprite = RankSprites[4];

        }
        else if (RankInt < Score[2]) RankImage.sprite = RankSprites[0];//2000<1500
        else if (Score[2] <= RankInt && RankInt < Score[1]) RankImage.sprite = RankSprites[1];//1500<=2000&& 2000<2000 
        else if (Score[1] <= RankInt && RankInt < Score[0]) RankImage.sprite = RankSprites[2];// 2000<=2000&&2000<2500
        else if (Score[0] <= RankInt) RankImage.sprite = RankSprites[3]; //2500<=2000


    }

    public void Reset()
    {
        //PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.DeleteAll();
        //  highScore.text = "0";
    }
}
