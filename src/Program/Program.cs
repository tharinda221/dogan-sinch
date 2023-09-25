using System;
using Library;
using MessageEncoder;

class Program
{
    static void Main()
    {
        try {
            SimpleMessageCodec codec = new SimpleMessageCodec();
            Dictionary<String, String> headers = new System.Collections.Generic.Dictionary<string, string>
                {
                    {"Header1", "Value1"},
                    {"Header2", "Value2"}
                };
            byte[] payload = System.Text.Encoding.UTF8.GetBytes("Hello, World!");
            var message = new Message();
            message.SetPayload(payload);
            message.SetHeaders(headers);

            byte[] encodedMessage = codec.Encode(message);
            Console.WriteLine($"encodedMessage:  {BitConverter.ToString(encodedMessage)}");


            DecodedMessage decodedMessage = codec.Decode(encodedMessage);
            if(decodedMessage.status == DecodedMessage.MessageStatus.Sucess) {
                Console.WriteLine("Message Decoded.....");
                Console.WriteLine($"Headers: {string.Join(", ", decodedMessage.message.Headers)}");
                Console.WriteLine($"Payload: {System.Text.Encoding.UTF8.GetString(decodedMessage.message.Payload)}");
            }

        }

        catch(MessageHeadersLengthExceededException ex) {
            Console.WriteLine(ex.Message);
        }
        catch(HeaderLengthExceededException ex) {
            Console.WriteLine(ex.Message);
        }
        catch(PayloadLengthExceededException ex) {
            Console.WriteLine(ex.Message);
        }

    }
}
