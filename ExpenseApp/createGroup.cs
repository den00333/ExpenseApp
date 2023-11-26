using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseApp
{
    public partial class createGroup : Form
    {
        private string username = FirebaseData.Instance.Username;
        public createGroup()
        {
            InitializeComponent();
        }

        private void closeBTN_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private async void Create()
        {
            string groupName = groupTB.Text.ToString();
            int numberOfParticipants = int.Parse(participantsTB.Text.Trim());
            string groupCode = generateCode();
            var db = otherFunc.FirestoreConn();

            CollectionReference groupsCollectionRef = db.Collection("Groups");
            DocumentReference groupDocRef = groupsCollectionRef.Document(groupCode);
            List<string> initialMembers = new List<string> { username };
            Dictionary<string, object> groupData = new Dictionary<string, object>()
            {
                {"GroupName", groupName},
                {"MaxParticipants", numberOfParticipants},
                {"GroupCode", groupCode},
                {"Members", initialMembers},
            };
            await groupDocRef.SetAsync(groupData);

            CollectionReference userCollectionRef = db.Collection("Users");
            DocumentReference userDocRef = userCollectionRef.Document(username);

            DocumentSnapshot userDocSnap = await userDocRef.GetSnapshotAsync();
            if (!userDocSnap.Exists)
            {
                await userDocRef.SetAsync(new Dictionary<string, object>
            {
                {"Groups", new List<string> { groupName }}
            });
            }
            else {
                await userDocRef.UpdateAsync("Groups", FieldValue.ArrayUnion(groupName));
            }
        }
        public string generateCode()
        {
            Random random = new Random();
            int length = 8;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            StringBuilder codeBuilder = new StringBuilder();

            for (int i = 0; i < length; i++) {
                int index = random.Next(chars.Length);
                codeBuilder.Append(chars[index]);
            }
            return codeBuilder.ToString();
        }
        bool flag = true;
        private void btnSwitchPanel_Click(object sender, EventArgs e)
        {
            if (flag) {
                Console.WriteLine("Hi");
                joinPanel.Visible = true;
                joinPanel.BringToFront();
                flag = false;
                btnSwitchPanel.Text = "Create Group";
            }
            else
            {
                Console.WriteLine("Hello");
                createPanel.Visible = true;
                createPanel.BringToFront();
                flag = true;
                btnSwitchPanel.Text = "Join with code";
            }
        }
        public async void Join()
        {
            string groupCode = groupCodeTB.Text.ToString();
            var db = otherFunc.FirestoreConn();
            try
            {
                DocumentReference groupDocRef = db.Collection("Groups").Document(groupCode);
                DocumentSnapshot groupDocSnap = await groupDocRef.GetSnapshotAsync();
                if (groupDocSnap.Exists)
                {
                    string groupName = groupDocSnap.GetValue<string>("GroupName");
                    List<string> groupMembers = groupDocSnap.GetValue<List<string>>("Members");
                    if (!groupMembers.Contains(username))
                    {
                        await groupDocRef.UpdateAsync("Members", FieldValue.ArrayUnion(username));
                        CollectionReference userCollectionRef = db.Collection("Users");
                        DocumentReference userDocRef = userCollectionRef.Document(username);

                        DocumentSnapshot userDocSnap = await userDocRef.GetSnapshotAsync();
                        if (!userDocSnap.Exists) {
                            await userDocRef.SetAsync(new Dictionary<string, object>
                            {
                                {"Groups", new List<string> { groupName }}
                            });
                            MessageBox.Show("Successfully joined group!", "Success", MessageBoxButtons.OK);
                        }
                        else {
                            await userDocRef.UpdateAsync("Groups", FieldValue.ArrayUnion(groupName));
                            MessageBox.Show("Successfully joined group!", "Success", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"You are already a member of group: {groupName}");
                    }
                }
                else {
                    MessageBox.Show("Group not found. Please check the group code and try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error joining group: {ex.Message}");
            }
        }
        private void btnJoin_Click(object sender, EventArgs e)
        {
            if (otherFunc.internetConn())
            {
                Join();
            }
            else MessageBox.Show("No Internet Connection!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (otherFunc.internetConn()){
                try{
                    Create();
                    MessageBox.Show("Group created Succesfully", "Success", MessageBoxButtons.OK);
                }
                catch{
                    MessageBox.Show("Error encountered during saving. Please try again or contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else{
                MessageBox.Show("No Internet Connection!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
