using System;
using System.Collections.Generic;
using System.Text;

namespace Library {
    /*
     * Message class which stores headers and payload data
     */
    public class Message {
        private Dictionary<String, String> headers;
        private byte[] payload;
        private static int maxHeaderCount = 63;
        private static int maxStringByteCount = 1023;
        private static int maxPayloadByteCount = 256 * 1024;

        public Dictionary<String, String> Headers {
            get {return headers;}
        }

        public byte[] Payload {
            get {return payload;}
        }

        private int GetByteSize(String str) {
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(str);
            return utf8Bytes.Length;
        }

        private bool CheckByteLength(String str) {
            int byteCount = GetByteSize(str);
            if (byteCount <= maxStringByteCount)
            {
                return true;
            }
            return false;
        }

        public void SetHeaders(Dictionary<String, String> headers) {
            if(headers.Count > maxHeaderCount) {
                throw new MessageHeadersLengthExceededException();
            }

            foreach (var header in headers)
            {
                if(CheckByteLength(header.Key) && CheckByteLength(header.Value)) {
                    continue;
                }
                else {
                    throw new HeaderLengthExceededException();
                }
            }
            this.headers = headers;
        }

        public void SetPayload(byte[] payload) {
            if(payload.Length > maxPayloadByteCount) {
                throw new PayloadLengthExceededException();
            }
            this.payload = payload;
        }
    }
}
