using MinesweeperProject.Services;
using System.Windows;
using System.Windows.Input;
using System.IO;

namespace MinesweeperProject.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        private readonly MainViewModel _mainParent;
        public ICommand StartGameCommand { get; }
        public ICommand LoadGameCommand { get; }
        public ICommand ExitCommand { get; }

        public ICommand ShowRankingCommand { get; }
        public string DisplayName => $"현재 사용자: {_mainParent.Nickname ?? "Guest"}";
        public MainMenuViewModel(MainViewModel mainParent)
        {
            _mainParent = mainParent;

            // 3. 버튼 클릭 시 MainViewModel의 ShowGameView를 호출하도록 연결합니다.
            // CommandParameter로 전달된 난이도 문자열(o)을 넘겨줍니다.
            StartGameCommand = new RelayCommand(o =>
            {
                // 1. 기존 저장 파일이 있다면 삭제
                string saveFilePath = "savegame.json";
                if (File.Exists(saveFilePath))
                {
                    try
                    {
                        File.Delete(saveFilePath);
                    }
                    catch (Exception ex)
                    {
                        // 파일이 사용 중이거나 권한 문제가 있을 경우를 대비
                        System.Diagnostics.Debug.WriteLine("저장 파일 삭제 실패: " + ex.Message);
                    }
                }

                // 2. 새 게임 화면으로 이동
                string difficulty = o?.ToString() ?? "쉬움";
                _mainParent.ShowGameView(difficulty);
            });

            LoadGameCommand = new RelayCommand(
                // 실행 로직 (Execute)
                o => {
                    if (File.Exists("savegame.json"))
                    {
                        string json = File.ReadAllText("savegame.json");
                        var saveData = System.Text.Json.JsonSerializer.Deserialize<Models.SaveData>(json);
                        if (saveData != null)
                        {
                            _mainParent.RestoreGame(saveData);
                        }
                    }
                },
                // 활성화 조건 (CanExecute)
                o => File.Exists("savegame.json") // 파일이 있을 때만 버튼이 활성화됨
            );


            ExitCommand = new RelayCommand(o =>
            {
                // 사용자에게 한 번 더 확인을 요청할 수도 있습니다.
                if (MessageBox.Show("게임을 종료하시겠습니까?", "종료 확인",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown(); // 전체 프로그램 종료
                }
            });

            // 생성자 내부
            ShowRankingCommand = new RelayCommand(o => _mainParent.ShowRankingView());
        }
    }
}