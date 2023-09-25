using NUnit.Framework;
using System.Collections.Generic;
using Library;
using MessageEncoder;
using Test.Utilities;

[TestFixture]
public class SimpleMessageCodecTests {
    private SimpleMessageCodec codec;
    private TestUtilities testUtilities;

    [SetUp]
    public void Setup() {
        codec = new SimpleMessageCodec();
        testUtilities = new TestUtilities();
    }

    /*
    * This test case is created for basic encoded and decoded message mapping 
    */
    [Test]
    public void TestEncodeAndDecode() {
        
        Dictionary<String, String> headers = new Dictionary<string, string> {
                { "Header1", "Value1" },
                { "Header2", "Value2" },
            };
        byte[] payload = new byte[1024];
        var originalMessage = new Message();
        originalMessage.SetPayload(payload);
        originalMessage.SetHeaders(headers);

        byte[] encodedData = codec.Encode(originalMessage);
        DecodedMessage decodedMessage = codec.Decode(encodedData);

        if(decodedMessage.status == DecodedMessage.MessageStatus.Sucess) {
            Assert.AreEqual(originalMessage.Headers.Count, decodedMessage.message.Headers.Count);
            foreach (var kvp in originalMessage.Headers) {
                Assert.IsTrue(decodedMessage.message.Headers.ContainsKey(kvp.Key));
                Assert.AreEqual(kvp.Value, decodedMessage.message.Headers[kvp.Key]);
            }
            Assert.AreEqual(originalMessage.Payload.Length, decodedMessage.message.Payload.Length);
            Assert.IsTrue(testUtilities.AreByteArraysEqual(originalMessage.Payload, decodedMessage.message.Payload));
        }
    }

    /*
    * This test case is created to check empty encoded and decoded message  
    */
    [Test]
    public void TestEncodeAndDecodeEmptyMessage() {

        Dictionary<String, String> headers = new Dictionary<string, string>();
        byte[] payload = new byte[0];
        var originalMessage = new Message();
        originalMessage.SetPayload(payload);
        originalMessage.SetHeaders(headers);

        byte[] encodedData = codec.Encode(originalMessage);
        DecodedMessage decodedMessage = codec.Decode(encodedData);
        if(decodedMessage.status == DecodedMessage.MessageStatus.Sucess) {
            Assert.IsEmpty(decodedMessage.message.Headers);
            Assert.IsEmpty(decodedMessage.message.Payload);
        }
    }

    /*
    * This test case is created to check maximum headersize   
    */
    [Test]
    public void TestMaximumHeaderSize() {
        string maxHeaderName = testUtilities.GenerateStringOfSize(1023);
        string maxHeaderValue = testUtilities.GenerateStringOfSize(1023);

        Dictionary<String, String> headers = new Dictionary<string, string> {
                { maxHeaderName, maxHeaderValue }};
        byte[] payload = new byte[1024];
        var originalMessage = new Message();
        originalMessage.SetPayload(payload);
        originalMessage.SetHeaders(headers);

        byte[] encodedData = codec.Encode(originalMessage);
        DecodedMessage decodedMessage = codec.Decode(encodedData);
        if(decodedMessage.status == DecodedMessage.MessageStatus.Sucess) {
            Assert.AreEqual(1, decodedMessage.message.Headers.Count);
            Assert.IsTrue(decodedMessage.message.Headers.ContainsKey(maxHeaderName));
            Assert.AreEqual(maxHeaderValue, decodedMessage.message.Headers[maxHeaderName]);
        }
    }

    /*
    * This test case is created to test exceeding the maximum size of header name and value  
    */
    [Test]
    public void TestMaximumHeaderNameSize() {
        string maxHeaderName = testUtilities.GenerateStringOfSize(2000);
        string maxHeaderValue = testUtilities.GenerateStringOfSize(2000);

        Dictionary<String, String> headers = new Dictionary<string, string> {
                { maxHeaderName, maxHeaderValue }};
        byte[] payload = new byte[1024];
        TestDelegate testDelegate = () => {
            var originalMessage = new Message();
            originalMessage.SetPayload(payload);
            originalMessage.SetHeaders(headers);
        };

        Assert.Throws<HeaderLengthExceededException>(testDelegate);
    }

    /*
    * This test case is created to test exceeding the maximum size of the payload  
    */
    [Test]
    public void TestMaximumPayloadSize() {
        string HeaderName = testUtilities.GenerateStringOfSize(1000);
        string HeaderValue = testUtilities.GenerateStringOfSize(1000);

        Dictionary<String, String> headers = new Dictionary<string, string> {
                { HeaderName, HeaderValue }};
        byte[] payload = new byte[1024 * 500];
        TestDelegate testDelegate = () => {
            var originalMessage = new Message();
            originalMessage.SetPayload(payload);
            originalMessage.SetHeaders(headers);
        };

        Assert.Throws<PayloadLengthExceededException>(testDelegate);
    }

    /*
    * This test case is created to test exceeding the maximum size of the payload  
    */
    [Test]
    public void TestMaximumHeadersSize() {
        string HeaderName = testUtilities.GenerateStringOfSize(1000);
        string HeaderValue = testUtilities.GenerateStringOfSize(1000);

        Dictionary<String, String> headers = new Dictionary<string, string> {
                { HeaderName, HeaderValue }};
        for(int i = 0; i < 70; i++) {
            HeaderName = testUtilities.GenerateStringOfSize(1000);
            HeaderValue = testUtilities.GenerateStringOfSize(1000);
            headers.Add(HeaderName, HeaderValue);
        }
        byte[] payload = new byte[1024];
        TestDelegate testDelegate = () => {
            var originalMessage = new Message();
            originalMessage.SetPayload(payload);
            originalMessage.SetHeaders(headers);
        };

        Assert.Throws<MessageHeadersLengthExceededException>(testDelegate);
    }
}
