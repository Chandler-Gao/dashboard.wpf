﻿using Dashboard.Wpf.Models;
using Dashboard.Wpf.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dashboard.Wpf.ViewModels;

public class LoginViewModel : ViewModelBase
{
    private string _username = "admin";
    private SecureString _password;
    private string _errorMessage;
    private bool _isViewVisible;

    private IUserRepository _userRepository;

    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
        }
    }
    public SecureString Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }
    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged(nameof(ErrorMessage));
        }
    }
    public bool IsViewVisible
    {
        get => _isViewVisible;
        set
        {
            _isViewVisible = value;
            OnPropertyChanged(nameof(IsViewVisible));
        }
    }

    public ICommand? LoginCommand { get; }
    public ICommand? RecoverPasswordCommand { get; }
    public ICommand? ShowPasswordCommand { get; }
    public ICommand? RememberPasswordCommand { get; }

    public LoginViewModel()
    {
        _userRepository = new UserRepository();
        LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPasswordCommand("", ""));
    }

    private void ExecuteRecoverPasswordCommand(string username, string password)
    {

    }

    private bool CanExecuteLoginCommand(object obj)
    {
        bool validData;
        if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 || Password == null || Password.Length < 3)
        {
            validData = false;
        }
        else validData = true;
        return validData;
    }

    private void ExecuteLoginCommand(object obj)
    {
        var isValidUser = _userRepository.AuthenticateUser(new NetworkCredential(Username, Password));
        if (isValidUser)
        {
            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), null);
            IsViewVisible = false;
        }
        else
        {
            ErrorMessage = "** Invalid username or password";
        }
    }
}
