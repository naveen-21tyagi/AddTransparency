using System.Diagnostics;
// using System.Runtime.InteropServices;
namespace AddTransparency{

    class App : Form{
        Label header;
        Label footer;
        Label alpha;
        TextBox value;
        Button makeIt;
        ComboBox programs;
        static Dictionary<string,int>? list;

        // [DllImport("user32.dll")]
        // private static extern bool IsWindowVisible(IntPtr hWnd);

        // [DllImport("user32.dll")]
        // private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        static Dictionary<string,int> GetList(){
            Process[] processes = Process.GetProcesses();
            list = new Dictionary<string, int>();
            foreach (var proc in processes)
            {
                if (!string.IsNullOrEmpty(proc.MainWindowTitle))
                {
                    string name=proc.ProcessName;
                    if (name=="TextInputHost") continue;
                    list[proc.MainWindowTitle]=proc.Id;
                }
            }
            // foreach(Process process in processes){
            //     IntPtr hWnd=process.MainWindowHandle;
            //     if (IsWindowVisible(hWnd)){
            //         uint id;
            //         GetWindowThreadProcessId(hWnd, out id);
            //         string name=process.ProcessName;
            //         if (name=="ApplicationFrameHost" ||  name=="RtkUWP" || name=="TextInputHost") continue;
            //         list[process.MainWindowTitle]=(int)id;
            //     }
            // }
            return list;
        }
        public App(){
            header =new Label();
            header.Anchor = AnchorStyles.Left | AnchorStyles.Top ;
            header.TextAlign = ContentAlignment.MiddleCenter;
            header.Size = new Size(500, 45);
            header.Text = "This can make your program transparent.";
            header.Font = new Font("Microsoft Sans Serif", 14);

            
            list = GetList();
            programs = new ComboBox();
            programs.Text = "Select program..";
            foreach(KeyValuePair<string, int> entry in list)
            {
                programs.Items.Add(entry.Key);
            }
            programs.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            programs.Location=new Point(100,75);
            programs.Size = new Size(300,21);
            programs.DropDownStyle = ComboBoxStyle.DropDownList;
            programs.DropDownHeight = programs.ItemHeight * 4;
            

            alpha =new Label();
            alpha.Location=new Point(150,190);
            alpha.Size = new Size(50,30);
            alpha.Text = "Alpha ";
            alpha.TextAlign = ContentAlignment.MiddleRight;


            value = new TextBox();
            value.Location=new Point(200,190);
            value.Size = new Size(50,30);
            value.Text = "0-255";
            value.MaxLength=3;

            makeIt = new Button();
            makeIt.Location=new Point(275,190);
            makeIt.Size = new Size(75,30);
            makeIt.Text = "Make It..";
            makeIt.Click += new EventHandler(DoIt);

            footer =new Label();
            footer.Location = new Point(280,230);
            footer.Size = new Size(200,40);
            footer.TextAlign = ContentAlignment.BottomRight;
            footer.Text= "created by naveen_21tyagi :) credits to hikarin";
            
            this.Controls.Add(header);
            this.Controls.Add(programs);
            this.Controls.Add(alpha);
            this.Controls.Add(value);
            this.Controls.Add(makeIt);
            this.Controls.Add(footer);
            this.Text = "AddTransparency";
            this.Size=new Size(500,320);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        void DoIt(Object? sender, EventArgs e){
            string program = (string)programs.SelectedItem;
            int pid=list[program];
            SetTransParency.SetTransparency(pid, byte.Parse(value.Text));
            // makeIt.Text="Done";
        }
    }
}