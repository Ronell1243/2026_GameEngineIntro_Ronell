using UnityEngine;
using System.Linq;
using TMPro;

public class RankPage : MonoBehaviour
{
    [SerializeField] Transform contentRoot;    //content 오브젝트
    [SerializeField] GameObject rowPrefab;     //RankRow 프리팹

    StageResultList allData;

    void Awake()
    {
        allData = StageResultSaver.LoadRank();
        RefreshRankList(1);
    }

    void RefreshRankList(int stageIndex)
    {
        //기존의 모든 자식 오브젝트 삭제
        foreach (Transform child in contentRoot)
        {
            Destroy(child.gameObject);
        }

        //랭크 데이터 정렬
        var sortedData = allData.results.Where(r => r.stage == stageIndex).OrderByDescending(x => x.score).ToList();

        //랭크 데이터 생성
        for (int i = 0; i < sortedData.Count; i++)
        {
            GameObject row = Instantiate(rowPrefab, contentRoot);
            TMP_Text rankText = row.GetComponentInChildren<TMP_Text>();
            rankText.text = $"{i + 1}. {sortedData[i].playerName} - {sortedData[i].score}";
        }
    }

    public void Stage1ButtonClicked()
    {
        RefreshRankList(1);
    }
    public void Stage2ButtonClicked()
    {
        RefreshRankList(2);
    }
    public void Stage3ButtonClicked()
    {
        RefreshRankList(3);
    }
    public void Stage4ButtonClicked()
    {
        RefreshRankList(4);
    }
    public void Stage5ButtonClicked()
    {
        RefreshRankList(5);
    }
}
