using Client.DTOs;

namespace Client.Views
{
    public interface IMainView
    {
        string StudentFirstName { get; set; }
        string StudentLastName { get; set; }
        Guid? SelectedStudentId { get; }
        void DisplayStudents(List<StudentDto> students);
        void ClearInputs();
        void ShowError(string message);
        event EventHandler LoadDataRequested;
        event EventHandler AddStudentClicked;
        event EventHandler DeleteStudentClicked;
        event EventHandler EditStudentClicked;
    }
}
