using Client.DTOs;
using Client.Views;

namespace Client.Presenters
{
    public class StudentPresenter
    {
        private IMainView _view;
        private readonly ApiService _apiService;

        public StudentPresenter(ApiService apiService)
        {
            _apiService = apiService;
        }
        public void AttachView(IMainView view)
        {
            _view = view;
            _view.LoadDataRequested += OnLoadDataRequested;
            _view.AddStudentClicked += OnAddStudentClicked;
            _view.EditStudentClicked += OnEditStudentClicked;
            _view.DeleteStudentClicked += OnDeleteStudentClicked;
        }
        private async void OnLoadDataRequested(object sender, EventArgs e)
        {
            try
            {
                var students = await _apiService.GetStudentsAsync();

                _view.DisplayStudents(students);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Error loading students: {ex.Message}");
            }
        }
        private async void OnAddStudentClicked(object sender, EventArgs e)
        {
            try
            {
                var dto = new StudentDto
                {
                    StudentFirstName = _view.StudentFirstName,
                    StudentLastName = _view.StudentLastName
                };

                await _apiService.CreateStudentAsync(dto);
                _view.ClearInputs();
                await ReloadStudentsAsync();
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }
        private async void OnEditStudentClicked(object sender, EventArgs e)
        {
            if (_view.SelectedStudentId == null) return;

            try
            {
                var dto = new StudentDto
                {
                    StudentFirstName = _view.StudentFirstName,
                    StudentLastName = _view.StudentLastName
                };

                await _apiService.UpdateStudentAsync(_view.SelectedStudentId.Value, dto);
                _view.ClearInputs();
                await ReloadStudentsAsync();
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }
        private async void OnDeleteStudentClicked(object sender, EventArgs e)
        {
            if (_view.SelectedStudentId == null) return;

            try
            {
                await _apiService.DeleteStudentAsync(_view.SelectedStudentId.Value);
                _view.ClearInputs();
                await ReloadStudentsAsync();
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }

        private async Task ReloadStudentsAsync()
        {
            var students = await _apiService.GetStudentsAsync();
            _view.DisplayStudents(students);
        }

    }
}
