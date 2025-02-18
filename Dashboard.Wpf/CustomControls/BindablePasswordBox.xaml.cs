using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Wpf.CustomControls;

/// <summary>
/// Interaction logic for BindablePasswordBox.xaml
/// </summary>
public partial class BindablePasswordBox : UserControl
{
    public BindablePasswordBox()
    {
        InitializeComponent();

        this.txtPassword.PasswordChanged += TxtPassword_PasswordChanged;
    }

    private void TxtPassword_PasswordChanged(object sender, RoutedEventArgs e)
    {
        this.Password = this.txtPassword.SecurePassword;
    }

    public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register("Password", typeof(SecureString), typeof(BindablePasswordBox));

    public SecureString Password
    {
        get { return (SecureString)GetValue(PasswordProperty); }
        set
        {
            SetValue(PasswordProperty, value);
        }
    }
}
