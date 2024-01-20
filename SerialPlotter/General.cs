using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialPlotter {
    // ------------------------------------------------------------
    //  general
    // ------------------------------------------------------------
    class General {

        // show err message box
        public static void ShowErrMsgBox(string str) {
            MessageBox.Show(str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        // show message box
        public static void ShowMsgBox(string title, string str) {
            MessageBox.Show(str, title, MessageBoxButtons.OK);
        }

        // get payload data
        public static Byte[] String2Bytes(string str, bool isHexMode, bool needCr, bool needLn) {
            if(isHexMode) {
                // Hex mode
                List<byte> sendByteList = new List<byte>();
                for(int i = 0; i < str.Length; i += 2) {
                    try {
                        sendByteList.Add(Convert.ToByte(str.Substring(i, 2), 16));
                    } catch {
                        throw new Exception("Failed to convert str to hexbyte");
                    }
                }
                if(needCr) { sendByteList.Add(0x0D); }
                if(needLn) { sendByteList.Add(0x0A); }
                return sendByteList.ToArray();
            } else {
                // ASCII mode
                string tmp = str;
                if(needCr) { tmp += "\r"; }
                if(needLn) { tmp += "\n"; }
                Byte[] ret = Encoding.UTF8.GetBytes(tmp);
                return ret;
            }
        }

        public static string GetLogTime() {
            return String.Format("[{0:yyyy-MM-dd HH:mm:ss.fff}]", DateTime.Now);
        }
    }
}
