using System;

namespace Library {

    public class MessageHeadersLengthExceededException : ArgumentException
    {
        private static string messageLengthError = "Message headers count cannot be exceed 63";
        public MessageHeadersLengthExceededException() : base(messageLengthError)
        {
        }
    }

    public class HeaderLengthExceededException : ArgumentException
    {
        private static string headerLengthError = "Header names and values should be limited to 1023 bytes";
        public HeaderLengthExceededException() : base(headerLengthError)
        {
        }
    }

    public class PayloadLengthExceededException : ArgumentException
    {
        private static string payloadLengthError = "Message Payload should be limited to 256 KiB";
        public PayloadLengthExceededException() : base(payloadLengthError)
        {
        }
    }
}
