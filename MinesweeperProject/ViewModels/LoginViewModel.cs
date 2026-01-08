using System.Windows.Input;
using MinesweeperProject.Services; // RelayCommand가 있는 곳

namespace MinesweeperProject.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly MainViewModel _mainParent;
        private string _nickname = string.Empty;

        public string Nickname
        {
            get => _nickname;
            set => SetProperty(ref _nickname, value);
        }

        // 반드시 public이어야 하며, { get; } 형식이 권장됩니다.
        public ICommand LoginCommand { get; }

        public LoginViewModel(MainViewModel mainParent)
        {
            _mainParent = mainParent;

            // 생성 시점에 Command를 할당합니다.
            LoginCommand = new RelayCommand(
                execute: o => _mainParent.ShowMainMenuView(Nickname),
                canExecute: o => !string.IsNullOrWhiteSpace(Nickname)
            );
        }
    }
}