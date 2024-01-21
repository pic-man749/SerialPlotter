using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace SerialPlotter {
    internal class DataParser {

        const char DELIMITER_VALUE_PAIR = ',';
        const char DELIMITER_KEY_VALUE = ':';

        public DataParser() {

        }

        public Dictionary<string, double> parse(string data) {
            Dictionary<string, double> ret = new Dictionary<string, double>();

            // split {key}:{val},{key};{val}, ...
            string[] valuePair = data.Split(DELIMITER_VALUE_PAIR);
            foreach(string vp in valuePair) {
                // split {key}:{value}
                string[] kv = vp.Split(DELIMITER_KEY_VALUE);
                if(kv.Length != 2) {
                    // format error
                    continue;
                }
                // get key and value
                ret[kv[0]] = getValueFromString(kv[1]);
            }

            return ret;
        }

        private double getValueFromString(string str) {
            double val = 0.0;
            try {
                val = Convert.ToDouble(str);
            } catch {
                ;
            }
            return val;
        }
    }
}
