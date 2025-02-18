using Dashboard.Wpf.Models;
using Dashboard.Wpf.Repositories;
using System.Windows;

namespace Dashboard.Wpf.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private UserAccount? _currentUserAccount;
    private IUserRepository? _userRepository;

    public UserAccount? CurrentUserAccount
    {
        get => _currentUserAccount;
        set
        {
            _currentUserAccount = value;
            OnPropertyChanged(nameof(CurrentUserAccount));
        }
    }

    public MainWindowViewModel()
    {
        _userRepository = new UserRepository();
        CurrentUserAccount = new UserAccount();
        LoadCurrentUserAccount();
    }

    private void LoadCurrentUserAccount()
    {
        var user = _userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
        if (user != null)
        {
            CurrentUserAccount!.Username = user.Username;
            CurrentUserAccount!.DisplayName = $"Welcome {user.Name} {user.LastName}";
            CurrentUserAccount!.ProfilePicture = null;
        }
        else
        {
            CurrentUserAccount!.DisplayName = "Invalid user, not logged in.";
        }
    }

}
