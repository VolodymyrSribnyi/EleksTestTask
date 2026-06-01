using Client.DTOs;
using Client.Presenters;
using Client.Views;
using System.ComponentModel;
namespace Client
{
    public partial class MainForm : Form, IMainView
    {
        public event EventHandler LoadDataRequested;
        public event EventHandler AddStudentClicked;
        public event EventHandler DeleteStudentClicked;
        public event EventHandler EditStudentClicked;

        public MainForm(StudentPresenter presenter)
        {
            InitializeComponent();
            dgvStudents.ReadOnly = true;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            btnAdd.Click += (s, e) => AddStudentClicked?.Invoke(this, EventArgs.Empty);
            btnEdit.Click += (s, e) => EditStudentClicked?.Invoke(this, EventArgs.Empty);
            btnDelete.Click += (s, e) => DeleteStudentClicked?.Invoke(this, EventArgs.Empty);
            presenter.AttachView(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoadDataRequested.Invoke(this, EventArgs.Empty);
        }


        public void DisplayStudents(List<StudentDto> students)
        {
            dgvStudents.DataSource = students;

            if (dgvStudents.Columns["Id"] != null)
                dgvStudents.Columns["Id"].Visible = false;
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ClearInputs()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string StudentFirstName
        {
            get => txtFirstName.Text.Trim();
            set => txtFirstName.Text = value;
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string StudentLastName
        {
            get => txtLastName.Text.Trim();
            set => txtLastName.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Guid? SelectedStudentId
        {
            get
            {
                if (dgvStudents.SelectedRows.Count > 0)
                {
                    var selectedRow = dgvStudents.SelectedRows[0];

                    var cellValue = selectedRow.Cells["Id"].Value;

                    if (cellValue != null && Guid.TryParse(cellValue.ToString(), out Guid id))
                    {
                        return id;
                    }
                }
                return null;
            }
        }

    }
}

