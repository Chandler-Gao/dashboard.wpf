using Dashboard.Wpf.Models;
using Dashboard.Wpf.Repositories;
using FontAwesome.Sharp;
using System.Windows.Input;

namespace Dashboard.Wpf.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private UserAccount? _currentUserAccount;
    private ViewModelBase _currentChildView;
    private string _caption;
    private IconChar _icon;

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
    public ViewModelBase CurrentChildView
    {
        get => _currentChildView; set
        {
            _currentChildView = value;
            OnPropertyChanged(nameof(CurrentChildView));
        }
    }
    public string Caption
    {
        get => _caption;
        set
        {
            _caption = value;
            OnPropertyChanged(nameof(Caption));
        }
    }
    public IconChar Icon
    {
        get => _icon;
        set
        {
            _icon = value;
            OnPropertyChanged(nameof(Icon));
        }
    }

    public MainWindowViewModel()
    {
        _userRepository = new UserRepository();
        CurrentUserAccount = new UserAccount();

        ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
        ShowCustomerViewCommand = new ViewModelCommand(ExecuteShowCustomerViewCommand);

        //Default view
        ExecuteShowHomeViewCommand(null);

        LoadCurrentUserAccount();
    }

    private void ExecuteShowCustomerViewCommand(object obj)
    {
        CurrentChildView = new CustomerViewModel();
        Caption = "Customers";
        Icon = IconChar.UserGroup;
    }

    private void ExecuteShowHomeViewCommand(object obj)
    {
        CurrentChildView = new HomeViewModel();
        Caption = "Dashboard";
        Icon = IconChar.Home;
    }

    private void LoadCurrentUserAccount()
    {
        var user = _userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
        if (user != null)
        {
            CurrentUserAccount!.Username = user.Username;
            CurrentUserAccount!.DisplayName = $"{user.Name} {user.LastName}";
            CurrentUserAccount!.ProfilePicture = null;
        }
        else
        {
            CurrentUserAccount!.DisplayName = "Invalid user, not logged in.";
        }
    }


    public ICommand ShowHomeViewCommand { get; }
    public ICommand ShowCustomerViewCommand { get; }
}
