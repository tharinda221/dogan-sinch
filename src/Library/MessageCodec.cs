using System;
using System.Collections.Generic;

namespace Library {

    public class DecodedMessage {
        public enum MessageStatus {Sucess, Falied};

        public Message message;
        public MessageStatus status;

        public DecodedMessage(Message message, MessageStatus status) {
            this.message = message;
            this.status = status;
        }
    }

    public interface MessageCodec {
        byte[] Encode(Message message);
        DecodedMessage Decode(byte[] data);
    }
}