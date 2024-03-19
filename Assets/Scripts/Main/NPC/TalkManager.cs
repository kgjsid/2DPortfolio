using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    [SerializeField] NPCPopUpUI nPCPopUpUI;
    public static TalkManager instance;

    [SerializeField] Dictionary<int, string[]> talkData;

    public static TalkManager Talk { get => instance; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(0, new string[] { "Test Dialog", "Test Pokemon!!" });
        talkData.Add(5, new string[] {"TestTest", "Pokemon"});
        talkData.Add(10, new string[] { "The number of Pokemon you currently possess is 0.", "Go to Professor Oak lab and get the Pokemon." });
        talkData.Add(1, new string[] {"Here Red!", "There are three Pokemon here.", "Haha!", "The Pokemon are held inside these Pokes Balls.", "When I was young, I was a serious Pokemon Traniner.", "But now, in my old age, I have only these three left.", "You can have one. Go on, choose!", });
        talkData.Add(100, new string[] {"I see! BULBASAUR is your choice. It's very easy to raise.", "So, Red, you want to go with the Grass Pokemon Bulbasaur?"});
        talkData.Add(101, new string[] {"Hm! Squirtle is your choice. It's one worth raising.", "So, Red, you've decided on the Water Pokemon Squirtle?" });
        talkData.Add(102, new string[] { "Ah! Charmander is your choice. You should raise it patiently.", "So, Red, you've claiming the Fire Pokemon Charmander?" });
        talkData.Add(1001, new string[] { "It's a wooden board" });
        talkData.Add(1000, new string[] { "It's a sign" });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public void ShowUI(int id)
    {
        StartCoroutine(ShowLogRoutine(id));
    }

    IEnumerator ShowLogRoutine(int id)
    {
        NPCPopUpUI instance = Manager.UI.ShowPopUpUI(nPCPopUpUI).GetComponent<NPCPopUpUI>();

        for(int i = 0; i < talkData[id].Length; i++)
        {
            string text = GetTalk(id, i);
            yield return instance.DisplayLog(text);

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        Manager.UI.ClearPopUpUI();
    }
}
