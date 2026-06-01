namespace Client.Views
{
    public interface ILoginView
    {
        string Login { get; }
        string Password { get; }
        void ShowError(string message);
        void CloseWithSuccess();
        event EventHandler LoginClicked;
    }
}
