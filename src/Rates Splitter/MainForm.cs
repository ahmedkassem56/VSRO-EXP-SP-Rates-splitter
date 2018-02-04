using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Rates_Splitter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnPatch_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] sp = BitConverter.GetBytes((float) (float.Parse(rate.Text) - 1.0));
                long JumpCodecave = 0xEA8CD;
                byte[] JmpCodeCave = {
                                     0xE9, 0xE8, 0xE3, 0x5E, 0x00, // JMP 00AD8CBA
                                     0x90, 0x90, 0x90              // NOP NOP NOP
                                 };
                long Codecave = 0x6D8CBA;
                byte[] Codecave_Func = {
                                       0xD9, 0x05, 0x80, 0x0C, 0xC6, 0x00,  // FLD DWORD PTR DS:[C60C80]
                                       0xDA, 0x4C, 0x24, 0x1C,              // FIMUL DWORD PTR SS:[ESP+1C]
                                       0x89, 0x44, 0x24, 0x40,              // MOV DWORD PTR SS:[ESP+40],EAX
                                       0xE9, 0x08, 0x1C, 0xA1, 0xFF         // JMP 004EA8D5
                                   };
                FileWriter fw = new FileWriter(Environment.CurrentDirectory + @"\SR_GameServer.exe");
                
                fw.Write(0x860C80, sp);
                fw.Write(Codecave, Codecave_Func);
                fw.Write(JumpCodecave, JmpCodeCave);
                fw.Close();
                MessageBox.Show("Patched successfuly");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while patching:\n" + ex.Message);
            }
        }
    }
}
