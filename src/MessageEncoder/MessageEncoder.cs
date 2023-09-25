using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Library;

namespace MessageEncoder {

    public class SimpleMessageCodec : MessageCodec
    {
        public byte[] Encode(Message message)
        {

            using (MemoryStream stream = new MemoryStream())
            {

                stream.WriteByte((byte)message.Headers.Count);

                foreach (var header in message.Headers)
                {
                    WriteString(stream, header.Key);
                    WriteString(stream, header.Value);
                }

                byte[] payloadLengthBytes = BitConverter.GetBytes(message.Payload.Length);
                stream.Write(payloadLengthBytes, 0, payloadLengthBytes.Length);

                stream.Write(message.Payload, 0, message.Payload.Length);

                return stream.ToArray();
            }
        }

        public DecodedMessage Decode(byte[] data)
        {
            try {
                using (MemoryStream stream = new MemoryStream(data))
                {
                    int headerCount = stream.ReadByte();

                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    for (int i = 0; i < headerCount; i++)
                    {
                        string key = ReadString(stream);
                        string value = ReadString(stream);
                        headers[key] = value;
                    }

                    byte[] payloadLengthBytes = new byte[4];
                    stream.Read(payloadLengthBytes, 0, payloadLengthBytes.Length);
                    int payloadLength = BitConverter.ToInt32(payloadLengthBytes, 0);

                    byte[] payload = new byte[payloadLength];
                    stream.Read(payload, 0, payload.Length);

                    Message message = new Message();
                    message.SetHeaders(headers);
                    message.SetPayload(payload);

                    return new DecodedMessage(message, DecodedMessage.MessageStatus.Sucess);
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
            return new DecodedMessage(null, DecodedMessage.MessageStatus.Falied);
        }

        private void WriteString(Stream stream, string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            byte[] lengthBytes = BitConverter.GetBytes((ushort)bytes.Length);
            stream.Write(lengthBytes, 0, lengthBytes.Length);
            stream.Write(bytes, 0, bytes.Length);
        }

        private string ReadString(Stream stream)
        {
            byte[] lengthBytes = new byte[2];
            stream.Read(lengthBytes, 0, lengthBytes.Length);
            ushort length = BitConverter.ToUInt16(lengthBytes, 0);

            byte[] stringBytes = new byte[length];
            stream.Read(stringBytes, 0, stringBytes.Length);
            return Encoding.UTF8.GetString(stringBytes);
        }
    }

}