using System;
using System.Collections.Generic;

namespace Library {

    /*
     * DecodedMessage class which will be used to output the decoded message  
     */
    public class DecodedMessage {
        public enum MessageStatus {Sucess, Falied};

        public Message message;
        public MessageStatus status;

        public DecodedMessage(Message message, MessageStatus status) {
            this.message = message;
            this.status = status;
        }
    }

    /*
     * MessageCodec interface which abstract Message encoding and decoding 
     */
    public interface MessageCodec {
        byte[] Encode(Message message);
        DecodedMessage Decode(byte[] data);
    }
}