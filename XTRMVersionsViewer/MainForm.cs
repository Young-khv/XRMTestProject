using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XTRMTestProject.Model;
using XTRMTestProject.Version_Control;

namespace XTRMVersionsViewer
{
    public partial class MainForm : Form
    {
        private VersionViewer vv;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitListBoxes();
        }

        private void InitListBoxes()
        {
            vv = new VersionViewer();

            var classes = vv.GetAllControlledClasses();
            var users = vv.GetAllUsers();

            classesListBox.DisplayMember = "name";
            classesListBox.ValueMember = "id";
            classesListBox.Items.Clear();

            usersListBox.DisplayMember = "login";
            usersListBox.ValueMember = "id";
            usersListBox.Items.Clear();
            
            versionsList.DisplayMember = "date";
            versionsList.ValueMember = "id";
            versionsList.Items.Clear();

            foreach (var controlledClass in classes)
            {
                classesListBox.Items.Add(controlledClass);
            }

            foreach (var user in users)
            {
                usersListBox.Items.Add(user);
            }
        }

        private void classesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
           var selectedClass = classesListBox.SelectedItem as ControlledClass;
            
           var versions = vv.GetVersionsForClass(selectedClass.id);
            
            versionsList.Items.Clear();

            foreach (var version in versions)
            {
                versionsList.Items.Add(version);
            }
        }

        private void usersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedUser = usersListBox.SelectedItem as User;

            var versions = vv.GetVersionsByUser(selectedUser.id);

            versionsList.Items.Clear();

            foreach (var version in versions)
            {
                versionsList.Items.Add(version);
            }
        }

        private void versionsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            commentRTB.Clear();
            sourceCodeRTB.Clear();

            var selectedVersion = versionsList.SelectedItem as XTRMTestProject.Model.Version;
            commentRTB.Text = String.Format("[{0}]: {1}", selectedVersion.user.login, selectedVersion.comment);

            var sourceCodeLines = vv.GetVersionSourceCode(selectedVersion);
            for (int i = 0; i < sourceCodeLines.Count; i++)
            {
                if (sourceCodeLines[i].Length > 0)
                {
                    if (sourceCodeLines[i][0] == '+')
                    {
                        AppendText(sourceCodeRTB,sourceCodeLines[i], Color.Green);
                    }

                    else if (sourceCodeLines[i][0] == '-')
                    {
                        AppendText(sourceCodeRTB, sourceCodeLines[i], Color.Red);
                    }

                    else
                    {
                        AppendText(sourceCodeRTB, sourceCodeLines[i], Color.Black);
                    }
                }
                else
                {
                    sourceCodeRTB.AppendText(sourceCodeLines[i] + Environment.NewLine);
                }
            }

            sourceCodeRTB.Update();
        }

        private static void AppendText( RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text + Environment.NewLine);
            box.SelectionColor = box.ForeColor;
        }
    }
}
