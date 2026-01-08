using MinesweeperProject.Models;
using MinesweeperProject.Services;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows.Input;

namespace MinesweeperProject.ViewModels
{
    // UI에서 보기 좋게 가공된 랭킹 아이템
    public class RankingDisplayItem
    {
        public string Medal { get; set; } = string.Empty;
        public string Nickname { get; set; } = string.Empty;
        public string TimeDisplay { get; set; } = string.Empty;
    }

    // 난이도별 그룹화를 위한 클래스
    public class RankingGroup
    {
        public string Difficulty { get; set; } = string.Empty;
        public List<RankingDisplayItem> Rankings { get; set; } = new();
    }

    public class RankingViewModel : ViewModelBase
    {
        private readonly MainViewModel _mainParent;
        public ObservableCollection<RankingGroup> GroupedRankings { get; } = new();
        public ICommand CloseCommand { get; }

        public RankingViewModel(MainViewModel mainParent)
        {
            _mainParent = mainParent;
            CloseCommand = new RelayCommand(o => _mainParent.ShowMainMenuView(_mainParent.Nickname!));

            LoadAndProcessRankings();
        }

        private void LoadAndProcessRankings()
        {
            string fileName = "rankings.json";
            if (!File.Exists(fileName)) return;

            try
            {
                string json = File.ReadAllText(fileName);
                var data = JsonSerializer.Deserialize<RankingData>(json);

                if (data != null)
                {
                    string[] difficulties = { "쉬움", "보통", "어려움", "극한" };

                    foreach (var diff in difficulties)
                    {
                        if (data.DifficultyRankings.ContainsKey(diff))
                        {
                            // 해당 난이도의 기록을 시간순으로 정렬 후 상위 3개 추출
                            var top3 = data.DifficultyRankings[diff]
                                .OrderBy(x => x.Time)
                                .Take(3)
                                .Select((entry, index) => new RankingDisplayItem
                                {
                                    // 인덱스에 따라 메달 부여
                                    Medal = index == 0 ? "🥇" : index == 1 ? "🥈" : "🥉",
                                    Nickname = entry.Nickname,
                                    TimeDisplay = entry.TimeDisplay
                                }).ToList();

                            if (top3.Count > 0)
                            {
                                GroupedRankings.Add(new RankingGroup
                                {
                                    Difficulty = diff,
                                    Rankings = top3
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("랭킹을 불러오는 중 오류 발생: " + ex.Message);
            }
        }
    }
}