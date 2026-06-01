using Client.Core;
using Client.DTOs;
using Client.Views;

namespace Client.Presenters
{
    public class LoginPresenter
    {
        private ILoginView _view;
        private readonly ApiService _apiService;
        private readonly IUserSession _userSession;

        public LoginPresenter(ApiService apiService,IUserSession userSession)
        {
            _apiService = apiService;
            _userSession = userSession;
        }
        public void AttachView(ILoginView view)
        {
            _view = view;
            _view.LoginClicked += OnLoginClicked;
        }
        public async void OnLoginClicked(object sender, EventArgs e)
        {
            try
            {
                var dto = new UserDto { Login = _view.Login, Password = _view.Password };
                string token = await _apiService.LoginAsync(dto);

                if (string.IsNullOrEmpty(token))
                {
                    _view.ShowError("The server did not return a token. Please try again.");
                    return;
                }

                _userSession.Token = token;

                _view.CloseWithSuccess();
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }
    }
}
