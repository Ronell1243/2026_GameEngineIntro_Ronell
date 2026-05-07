using System.Collections.Generic;
using System.IO;
using UnityEngine;


[System.Serializable]             // 1) 단일 기록

public class StageResult
{
    public string playerName;   //Player 이름 (PlayerPrefs에서 가져올거)
    public int stage;           //Stage 번호
    public int score;           //Score
}
[System.Serializable]
public class StageResultList
{
    public List<StageResult> results = new List<StageResult>();    //StageResult의 집합
}

public static class StageResultSaver
{
    private const string FILE = "stage_results.json";   //파일명
    private const string PLAYER_NAME = "PlayerName";    //PlayerPrefs에 사용할 플레이어네임 key
    private static string filePath = Path.Combine(Application.persistentDataPath, FILE);   //데이터 저장 위치
    public static void SaveStage(int stage, int score)
    {
        StageResultList list = LoadInternal();
        string playerName = PlayerPrefs.GetString(PLAYER_NAME, "");    // PlayerName키로 불러오기(PlayerPrefs)

        //StageResult 타입의 데이터 생성
        StageResult entry = new StageResult
        {
            playerName = playerName,
            stage = stage,
            score = score
        };
        list.results.Add(entry);    //기존 load한 데이터에 entry 추가
        string json = JsonUtility.ToJson(list, true);   //다시 Json으로 직렬화
        File.WriteAllText(filePath, json);   //filePath에 데이터 저장
    }
    public static StageResultList LoadRank()
    {
        return LoadInternal();
    }
    private static StageResultList LoadInternal()
    {
        if (!File.Exists(filePath))   //filePath에 파일이 없다면
            return new StageResultList();   //새로운 리스트 생성
        string json = File.ReadAllText(filePath);   //filePath에 있는 데이터 읽어오기
        StageResultList list = JsonUtility.FromJson<StageResultList>(json);   //json에서 StageResultList 타입으로 데이터 변환
        if (list == null)   //list가 비어있다면
            return new StageResultList();   //새로운 리스트 생성
        else   //list가 비어있지 않다면
            return list;   //list 돌려주기
    }
}
