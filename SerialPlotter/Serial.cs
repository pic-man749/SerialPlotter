using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SerialPlotter {
    internal class Serial : SerialPort {

        IDictionary<string, string> comPorts = new Dictionary<string, string>();

        public Serial() { }

        public static List<string> getBoudrates() {
            List<string> tmp = new List<string>();
            tmp.Add("4800");
            tmp.Add("9600");
            tmp.Add("38400");
            tmp.Add("115200");
            tmp.Add("921600");
            tmp.Add("3125000");
            return tmp;
        }

        public static List<string> getDataBits() {
            List<string> tmp = new List<string>();
            tmp.Add("8 bit");
            tmp.Add("7 bit");
            return tmp;
        }

        public static List<string> getParity() {
            List<string> tmp = new List<string>(Enum.GetNames(typeof(Parity)));
            return tmp;
        }

        public static List<string> getStopBits() {
            List<string> tmp = new List<string>(Enum.GetNames(typeof(StopBits)));
            return tmp;
        }

        public static List<string> getHandshake() {
            List<string> tmp = new List<string>(Enum.GetNames(typeof(Handshake)));
            return tmp;
        }

        public static List<string> getNewLines() {
            List<string> tmp = new List<string>();
            tmp.Add("\\n");
            tmp.Add("\\r");
            tmp.Add("\\r\\n");
            return tmp;
        }

        public void setNewLine(string nl) {
            this.NewLine = nl;
        }

        public Dictionary<string, string> getComPorts() {

            Dictionary<string, string> comPorts = new Dictionary<string, string>();

            // get COMs with device name
            ManagementClass mcW32SerPort = new ManagementClass("Win32_SerialPort");
            foreach(ManagementObject port in mcW32SerPort.GetInstances()) {
                comPorts.Add(
                    (string)port.GetPropertyValue("DeviceID"),  // key
                    (string)port.GetPropertyValue("Caption")    // value
                );
            }

            // add com name that cannot got from Win32_SerialPort
            foreach(string port in Serial.GetPortNames()) {
                if(!comPorts.ContainsKey(port)) {
                    comPorts.Add(port, port);
                }
            }

            return comPorts;
        }
    }
}