using System;
using System.Collections.Generic;

namespace MinesweeperProject.Models
{
    public class RankingEntry
    {
        public string Nickname { get; set; } = string.Empty;
        public int Time { get; set; }
        public DateTime Date { get; set; }

        // 초 단위 시간을 "01:20" 형태로 변환
        public string TimeDisplay => $"{Time / 60:D2}:{Time % 60:D2}";
    }

    public class RankingData
    {
        // 난이도별로 리스트 관리 (Key: 난이도명, Value: 상위 3명 리스트)
        public Dictionary<string, List<RankingEntry>> DifficultyRankings { get; set; } = new()
        {
            { "쉬움", new List<RankingEntry>() },
            { "보통", new List<RankingEntry>() },
            { "어려움", new List<RankingEntry>() },
            { "극한", new List<RankingEntry>() }
        };
    }
}
