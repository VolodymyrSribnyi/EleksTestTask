using Client.DTOs;
using Client.Views;

namespace Client.Presenters
{
    public class LoginPresenter
    {
        private ILoginView _view;
        private readonly ApiService _apiService;

        public LoginPresenter(ApiService apiService)
        {
            _apiService = apiService;
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
                Program.JwtToken = token;

                _view.CloseWithSuccess();
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }
    }
}
