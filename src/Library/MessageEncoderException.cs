using System;

namespace Library {

    /*
     * MessageHeadersLengthExceededException class handle headers count  
     */
    public class MessageHeadersLengthExceededException : ArgumentException
    {
        private static string messageLengthError = "Message headers count cannot be exceed 63";
        public MessageHeadersLengthExceededException() : base(messageLengthError)
        {
        }
    }

    /*
     * HeaderLengthExceededException class handle header name and value size  
     */
    public class HeaderLengthExceededException : ArgumentException
    {
        private static string headerLengthError = "Header names and values should be limited to 1023 bytes";
        public HeaderLengthExceededException() : base(headerLengthError)
        {
        }
    }

    /*
     * PayloadLengthExceededException class handle payload size  
     */
    public class PayloadLengthExceededException : ArgumentException
    {
        private static string payloadLengthError = "Message Payload should be limited to 256 KiB";
        public PayloadLengthExceededException() : base(payloadLengthError)
        {
        }
    }
}
